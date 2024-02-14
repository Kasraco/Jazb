using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model.AdminModel;
using Jazb.Servicelayer.EFServices.Enums;
using Jazb.Servicelayer.Interfaces;
using Jazb.Utilities.DateAndTime;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Servicelayer.EFServices
{
    public class AzmoonService : IAzmoon
    {
        private static int _searchTakeCount;
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Azmoon> _azmoon;

        public AzmoonService(IUnitOfWork uow)
        {
            _uow = uow;
            _azmoon = _uow.Set<Azmoon>();
            _searchTakeCount = 10;
        }

        public IList<AzmoonDataTableModel> GetDataTable(string term, int page, int count, Order order, AzmoonOrderBy orderBy, AzmoonSearchBy searchBy)
        {
            IQueryable<Azmoon> SelectAzmoons = _azmoon.Include(x => x.Valentears).AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case AzmoonSearchBy.Active:
                        SelectAzmoons = SelectAzmoons.Where(x => x.Active.Equals(true)).AsQueryable();
                        break;
                    case AzmoonSearchBy.NotActive:
                        SelectAzmoons.Where(x => x.Active.Equals(false)).AsQueryable();
                        break;
                    case AzmoonSearchBy.BoxTitle:
                        SelectAzmoons = SelectAzmoons.Where(x => x.Name.Contains(term)).AsQueryable();
                        break;
                    case AzmoonSearchBy.EndDate:
                        {
                            DateTime dte = Convert.ToDateTime(term);
                            SelectAzmoons = SelectAzmoons.Where(x => x.EndDate <= dte).AsQueryable();
                            break;
                        }

                    case AzmoonSearchBy.StartDate:
                        {
                            DateTime dts = Convert.ToDateTime(term);
                            SelectAzmoons = SelectAzmoons.Where(x => x.StartDate <= dts).AsQueryable();
                            break;
                        }
                    case AzmoonSearchBy.Title:
                        SelectAzmoons = SelectAzmoons.Where(x => x.Title.Contains(term)).AsQueryable();
                        break;

                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {
                    case AzmoonOrderBy.AcceptCount:
                        SelectAzmoons = SelectAzmoons.OrderBy(x => x.Valentears.Count(c => c.Accept == (int)ValentearStatuse.Aceept)).AsQueryable();
                        break;
                    case AzmoonOrderBy.RejectCount:
                        SelectAzmoons = SelectAzmoons.OrderBy(x => x.Valentears.Count(c => c.Accept == (int)ValentearStatuse.Reject)).AsQueryable();
                        break;
                    case AzmoonOrderBy.WinCount:
                        SelectAzmoons = SelectAzmoons.OrderBy(x => x.Valentears.Count(c => c.Accept == (int)ValentearStatuse.Win)).AsQueryable();
                        break;
                    case AzmoonOrderBy.Active:
                        SelectAzmoons = SelectAzmoons.OrderBy(x => x.Active).AsQueryable();
                        break;
                    case AzmoonOrderBy.BoxTitle:
                        SelectAzmoons = SelectAzmoons.OrderBy(x => x.Name).AsQueryable();
                        break;
                    case AzmoonOrderBy.EndDate:
                        SelectAzmoons = SelectAzmoons.OrderBy(x => x.EndDate).AsQueryable();
                        break;

                    case AzmoonOrderBy.StartDate:
                        SelectAzmoons = SelectAzmoons.OrderBy(x => x.StartDate).AsQueryable();
                        break;
                    case AzmoonOrderBy.Title:
                        SelectAzmoons = SelectAzmoons.OrderBy(x => x.Title).AsQueryable();
                        break;
                    case AzmoonOrderBy.ValentearCount:
                        SelectAzmoons = SelectAzmoons.OrderBy(x => x.Valentears.Count()).AsQueryable();
                        break;
                    case AzmoonOrderBy.ID:
                        SelectAzmoons = SelectAzmoons.OrderBy(x => x.AzmoonId).AsQueryable();
                        break;

                }

            }
            else
            {
                switch (orderBy)
                {
                    case AzmoonOrderBy.AcceptCount:
                        SelectAzmoons = SelectAzmoons.OrderByDescending(x => x.Valentears.Count(c => c.Accept == (int)ValentearStatuse.Aceept)).AsQueryable();
                        break;
                    case AzmoonOrderBy.RejectCount:
                        SelectAzmoons = SelectAzmoons.OrderByDescending(x => x.Valentears.Count(c => c.Accept == (int)ValentearStatuse.Reject)).AsQueryable();
                        break;
                    case AzmoonOrderBy.WinCount:
                        SelectAzmoons = SelectAzmoons.OrderByDescending(x => x.Valentears.Count(c => c.Accept == (int)ValentearStatuse.Win)).AsQueryable();
                        break;
                    case AzmoonOrderBy.Active:
                        SelectAzmoons = SelectAzmoons.OrderByDescending(x => x.Active).AsQueryable();
                        break;
                    case AzmoonOrderBy.BoxTitle:
                        SelectAzmoons = SelectAzmoons.OrderByDescending(x => x.Name).AsQueryable();
                        break;
                    case AzmoonOrderBy.EndDate:
                        SelectAzmoons = SelectAzmoons.OrderByDescending(x => x.EndDate).AsQueryable();
                        break;
                    case AzmoonOrderBy.StartDate:
                        SelectAzmoons = SelectAzmoons.OrderByDescending(x => x.StartDate).AsQueryable();
                        break;
                    case AzmoonOrderBy.Title:
                        SelectAzmoons = SelectAzmoons.OrderByDescending(x => x.Title).AsQueryable();
                        break;
                    case AzmoonOrderBy.ValentearCount:
                        SelectAzmoons = SelectAzmoons.OrderByDescending(x => x.Valentears.Count()).AsQueryable();
                        break;
                    case AzmoonOrderBy.ID:
                        SelectAzmoons = SelectAzmoons.OrderByDescending(x => x.AzmoonId).AsQueryable();
                        break;
                }
            }


            return SelectAzmoons.Skip(page * count).Take(count).Select(azmoon =>
              new AzmoonDataTableModel
              {
                  AcceptCount = azmoon.Valentears.Where(c => c.Accept == (int)ValentearStatuse.Aceept).Count(),
                  RejectCount = azmoon.Valentears.Where(c => c.Accept == (int)ValentearStatuse.Reject).Count(),
                  ValentearCount = azmoon.Valentears.Count(),
                  WinCount = azmoon.Valentears.Where(c => c.Accept == (int)ValentearStatuse.Win).Count(),
                  AzmoonBoxTitle = azmoon.Name,
                  AzmoonDescription = azmoon.Discription,
                  AzmoonId = azmoon.AzmoonId,
                  AzmoonTitle = azmoon.Title,
                  EndDate = azmoon.EndDate,
                  StartDate = azmoon.StartDate,
                  AghiFileData = azmoon.AghiFileData,
                  PicturAzmoon = azmoon.PicturAzmoon,
                  ResultEndDate = azmoon.ResultEndDate,
                  ResultStartDate = azmoon.ResultStartDate,
                  SignTextValentear = azmoon.SignTextValentear,
                  AmountValue=azmoon.AmountValue,
                  AzoonPaymentTypes=azmoon.AzmoonPaymentType,
                  EveryOneSee = azmoon.EveryOneSee
              }).ToList();

        }

        public int Count
        {
            get { return _azmoon.Count(); }
        }


        public AddAzmoonStatus AddAzmoon(AzmoonModel model)
        {
            try
            {
                Azmoon a = new Azmoon
                {
                    Active = false,
                    EveryOneSee=true,
                    AghiFileData = model.AghiFileData,
                    Discription = model.Discription,
                    EndDate = model.EndDate.Value,
                    Name = model.Name,
                    PicturAzmoon = model.PicturAzmoon,
                    ResultEndDate = model.ResultEndDate,
                    ResultStartDate = model.ResultStartDate,
                    SignTextValentear = model.SignTextValentear,
                    StartDate = model.StartDate.Value,
                    Title = model.Title,
                    AghiFileDataContentType = model.AghiFileDataContentType,
                    AghiFileDataFileName = model.AghiFileDataFileName,
                    PicturAzmoonContentType = model.PicturAzmoonContentType,
                    PicturAzmoonFileName = model.PicturAzmoonFileName,
                    AmountValue = model.AmountValue,
                    AzmoonPaymentType = model.AzPaymentType,
                    DateExecuteAzmoon = model.DateExecuteAzmoon.Value,
                    InputCardTitle = model.InputCardTitle,
                    LocationExecuteAzmoon = model.LocationExecuteAzmoon,
                    ShowAzmoon = false,
                    ShowCanEditResult = false,
                    ShowCanPrintResult = false,
                    ShowDownloadAgahi = false,
                    ShowPrintCard = false,
                    ShowRegisters = false,
                    TimeExecuteAzmoon = model.TimeExecuteAzmoon,
                    EndRegister = true


                };


                _azmoon.Add(a);
                
            }
            catch (Exception e)
            {
                return AddAzmoonStatus.Faild;

            }

            return AddAzmoonStatus.AddingAzmoonSuccessfully;
        }
        public AddAzmoonStatus EditAzmoon(AzmoonEditModel model)
        {
            try
            {
                var az = _azmoon.Find(model.AzmoonId);

                az.Active = model.Active;
                az.EveryOneSee = model.EveryOneSee;
                az.Discription = model.Discription;
                az.EndDate = model.EndDate.ToGetorgian();
                az.Name = model.Name;

               
               if(!string.IsNullOrEmpty(model.AghiFileDataContentType))
                {
                    az.AghiFileData = model.AghiFileData;
                    az.AghiFileDataContentType = model.AghiFileDataContentType;
                    az.AghiFileDataFileName = model.AghiFileDataFileName;
                }

                if (!string.IsNullOrEmpty(model.PicturAzmoonContentType))
                {
                    az.PicturAzmoon = model.PicturAzmoon;
                    az.PicturAzmoonContentType = model.PicturAzmoonContentType;
                    az.PicturAzmoonFileName = model.PicturAzmoonFileName;
                }

                az.ResultEndDate = model.ResultEndDate;
                az.ResultStartDate = model.ResultStartDate;
                az.SignTextValentear = model.SignTextValentear;
                az.StartDate = model.StartDate.ToGetorgian();
                az.Title = model.Title;

                az.AmountValue = model.AmountValue;
                az.AzmoonPaymentType = model.AzPaymentType;
                az.DateExecuteAzmoon = model.DateExecuteAzmoon.ToGetorgian();
                az.InputCardTitle = model.InputCardTitle;
                az.LocationExecuteAzmoon = model.LocationExecuteAzmoon;
                az.TimeExecuteAzmoon = model.TimeExecuteAzmoon;

            }
            catch (Exception e)
            {
                return AddAzmoonStatus.Faild;

            }

            return AddAzmoonStatus.AddingAzmoonSuccessfully;
        }

        public AddAzmoonStatus SettingCardAzmoon(AzmoonCardModel model)
        {

            try
            {
                var az = _azmoon.Find(model.AzmoonId);
              
                az.DateExecuteAzmoon = model.DateExecuteAzmoon.ToGetorgian();
                az.InputCardTitle = model.InputCardTitle;
                az.LocationExecuteAzmoon = model.LocationExecuteAzmoon;
                az.TimeExecuteAzmoon = model.TimeExecuteAzmoon;
            }

            catch (Exception e)
            {
                return AddAzmoonStatus.Faild;

            }

            return AddAzmoonStatus.AddingAzmoonSuccessfully;
        }


        public void Active(int Id)
        {
            var q = _azmoon.Find(Id);
            q.Active = true;
            _uow.SaveChanges();
        }

        public void DeActive(int Id)
        {
            var q = _azmoon.Find(Id);
            q.Active = false;
            _uow.SaveChanges();
        }


        public bool GetStatus(int Id)
        {
            return _azmoon.Find(Id).Active;
        }
    }
}
