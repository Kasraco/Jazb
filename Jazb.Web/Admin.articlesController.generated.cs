// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
#pragma warning disable 1591, 3008, 3009, 0108
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Jazb.Web.Areas.Admin.Controllers
{
    public partial class articlesController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected articlesController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Index()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult DataTable()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DataTable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult NavBar()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NavBar);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult Details()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Details);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Create()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Delete()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Delete);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult DeleteConfirmed()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DeleteConfirmed);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public articlesController Actions { get { return MVC.Admin.articles; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "articles";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "articles";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string DataTable = "DataTable";
            public readonly string NavBar = "NavBar";
            public readonly string Details = "Details";
            public readonly string Create = "Create";
            public readonly string Edit = "Edit";
            public readonly string Delete = "Delete";
            public readonly string DeleteConfirmed = "Delete";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string DataTable = "DataTable";
            public const string NavBar = "NavBar";
            public const string Details = "Details";
            public const string Create = "Create";
            public const string Edit = "Edit";
            public const string Delete = "Delete";
            public const string DeleteConfirmed = "Delete";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string AzmoonId = "AzmoonId";
        }
        static readonly ActionParamsClass_DataTable s_params_DataTable = new ActionParamsClass_DataTable();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DataTable DataTableParams { get { return s_params_DataTable; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DataTable
        {
            public readonly string azmoonid = "azmoonid";
            public readonly string term = "term";
            public readonly string page = "page";
            public readonly string count = "count";
            public readonly string order = "order";
            public readonly string orderBy = "orderBy";
            public readonly string searchBy = "searchBy";
        }
        static readonly ActionParamsClass_NavBar s_params_NavBar = new ActionParamsClass_NavBar();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_NavBar NavBarParams { get { return s_params_NavBar; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_NavBar
        {
            public readonly string aid = "aid";
        }
        static readonly ActionParamsClass_Details s_params_Details = new ActionParamsClass_Details();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Details DetailsParams { get { return s_params_Details; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Details
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Create s_params_Create = new ActionParamsClass_Create();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Create CreateParams { get { return s_params_Create; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Create
        {
            public readonly string AzmoonId = "AzmoonId";
            public readonly string article = "article";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string id = "id";
            public readonly string article = "article";
        }
        static readonly ActionParamsClass_Delete s_params_Delete = new ActionParamsClass_Delete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Delete DeleteParams { get { return s_params_Delete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Delete
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_DeleteConfirmed s_params_DeleteConfirmed = new ActionParamsClass_DeleteConfirmed();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DeleteConfirmed DeleteConfirmedParams { get { return s_params_DeleteConfirmed; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DeleteConfirmed
        {
            public readonly string id = "id";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _CreateOrEdit = "_CreateOrEdit";
                public readonly string _DataTable = "_DataTable";
                public readonly string _Nav = "_Nav";
                public readonly string Create = "Create";
                public readonly string Delete = "Delete";
                public readonly string Details = "Details";
                public readonly string Edit = "Edit";
                public readonly string Index = "Index";
            }
            public readonly string _CreateOrEdit = "~/Areas/Admin/Views/articles/_CreateOrEdit.cshtml";
            public readonly string _DataTable = "~/Areas/Admin/Views/articles/_DataTable.cshtml";
            public readonly string _Nav = "~/Areas/Admin/Views/articles/_Nav.cshtml";
            public readonly string Create = "~/Areas/Admin/Views/articles/Create.cshtml";
            public readonly string Delete = "~/Areas/Admin/Views/articles/Delete.cshtml";
            public readonly string Details = "~/Areas/Admin/Views/articles/Details.cshtml";
            public readonly string Edit = "~/Areas/Admin/Views/articles/Edit.cshtml";
            public readonly string Index = "~/Areas/Admin/Views/articles/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_articlesController : Jazb.Web.Areas.Admin.Controllers.articlesController
    {
        public T4MVC_articlesController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int AzmoonId);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(int AzmoonId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "AzmoonId", AzmoonId);
            IndexOverride(callInfo, AzmoonId);
            return callInfo;
        }

        [NonAction]
        partial void DataTableOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int azmoonid, string term, int page, int count, Jazb.Servicelayer.EFServices.Enums.Order order, Jazb.Servicelayer.EFServices.Enums.ArticleOrderBy orderBy, Jazb.Servicelayer.EFServices.Enums.ArticleSearchBy searchBy);

        [NonAction]
        public override System.Web.Mvc.ActionResult DataTable(int azmoonid, string term, int page, int count, Jazb.Servicelayer.EFServices.Enums.Order order, Jazb.Servicelayer.EFServices.Enums.ArticleOrderBy orderBy, Jazb.Servicelayer.EFServices.Enums.ArticleSearchBy searchBy)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DataTable);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "azmoonid", azmoonid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "count", count);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "order", order);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "orderBy", orderBy);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchBy", searchBy);
            DataTableOverride(callInfo, azmoonid, term, page, count, order, orderBy, searchBy);
            return callInfo;
        }

        [NonAction]
        partial void NavBarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int aid);

        [NonAction]
        public override System.Web.Mvc.ActionResult NavBar(int aid)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NavBar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "aid", aid);
            NavBarOverride(callInfo, aid);
            return callInfo;
        }

        [NonAction]
        partial void DetailsOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ViewResult Details(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Details);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DetailsOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string AzmoonId);

        [NonAction]
        public override System.Web.Mvc.ActionResult Create(string AzmoonId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "AzmoonId", AzmoonId);
            CreateOverride(callInfo, AzmoonId);
            return callInfo;
        }

        [NonAction]
        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Jazb.DomainClasses.Entities.article article);

        [NonAction]
        public override System.Web.Mvc.ActionResult Create(Jazb.DomainClasses.Entities.article article)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "article", article);
            CreateOverride(callInfo, article);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Jazb.DomainClasses.Entities.article article);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(Jazb.DomainClasses.Entities.article article)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "article", article);
            EditOverride(callInfo, article);
            return callInfo;
        }

        [NonAction]
        partial void DeleteOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Delete(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Delete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DeleteOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void DeleteConfirmedOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult DeleteConfirmed(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DeleteConfirmed);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DeleteConfirmedOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108
