using EntityFramework.BulkInsert.Extensions;
using Jazb.Datalayer.Context;
using LinqToExcel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace Jazb.Web
{
    public class ExternalData
    {
        private readonly IUnitOfWork _uow;
        public ExternalData(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void ExportToExcel<T>(List<T> DataModel, string Output)
        {
            DataTable table = ToDataTable<T>(DataModel);
            var newFile = new FileInfo(Output);
            if (newFile.Exists)
            {
                newFile.Delete();
            }

            using (var package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
               
                // worksheet.View.PageLayoutView = true;
                //worksheet.View.RightToLeft = true;

                // ذخیر سازی کلیه موارد اعمالی در فایل
                package.Save();
            }
        }
        public List<T> ReadFromExcel<T>(string SheetName, string pathToExcelFile)
        {
            var excel = new ExcelQueryFactory(pathToExcelFile);
            var OutQuery = from a in excel.Worksheet<T>(SheetName) select a;
            return OutQuery.ToList();
        }

        public void ImportToTable<T>(List<T> Model) where T :class
        {
            _uow.BulkInsertAllData(Model.AsEnumerable());
        
        }
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
             PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var properties = typeof(T).GetProperties().Where(p => p.IsDefined(typeof(DisplayAttribute), false)).ToList();
            foreach (var prop in properties)
            {
                //Setting column names as Property names
                // dataTable.Columns.Add(prop.Name);
                var str1 = prop.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().Single().Name;

                dataTable.Columns.Add(str1);
            }
            foreach (T item in items)
            {
                var values = new object[properties.Count()];
                for (int i = 0; i < properties.Count(); i++)
                {
                    //inserting property values to datatable rows
                    values[i] = properties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        public static string GetDisplayName<TModel>(Expression<Func<TModel, object>> expression)
        {

            Type type = typeof(TModel);

            string propertyName = null;
            string[] properties = null;
            IEnumerable<string> propertyList;
            //unless it's a root property the expression NodeType will always be Convert
            switch (expression.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expression.Body as UnaryExpression;
                    propertyList = (ue != null ? ue.Operand : null).ToString().Split(".".ToCharArray()).Skip(1); //don't use the root property
                    break;
                default:
                    propertyList = expression.Body.ToString().Split(".".ToCharArray()).Skip(1);
                    break;
            }

            //the propert name is what we're after
            propertyName = propertyList.Last();
            //list of properties - the last property name
            properties = propertyList.Take(propertyList.Count() - 1).ToArray(); //grab all the parent properties

            Expression expr = null;
            foreach (string property in properties)
            {
                PropertyInfo propertyInfo = type.GetProperty(property);
                expr = Expression.Property(expr, type.GetProperty(property));
                type = propertyInfo.PropertyType;
            }

            DisplayAttribute attr;
            attr = (DisplayAttribute)type.GetProperty(propertyName).GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            // Look for [MetadataType] attribute in type hierarchy
            // http://stackoverflow.com/questions/1910532/attribute-isdefined-doesnt-see-attributes-applied-with-metadatatype-class
            if (attr == null)
            {
                MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(propertyName);
                    if (property != null)
                    {
                        attr = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();
                    }
                }
            }
            return (attr != null) ? attr.Name : String.Empty;



        }


    }
}