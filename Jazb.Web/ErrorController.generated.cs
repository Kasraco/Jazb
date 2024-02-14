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
namespace Jazb.Web.Controllers
{
    public partial class ErrorController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ErrorController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ErrorController(Dummy d) { }

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


        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ErrorController Actions { get { return MVC.Error; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Error";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Error";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string BadRequest = "BadRequest";
            public readonly string Forbidden = "Forbidden";
            public readonly string InternalServerError = "InternalServerError";
            public readonly string MethodNotAllowed = "MethodNotAllowed";
            public readonly string NotFound = "NotFound";
            public readonly string Unauthorized = "Unauthorized";
            public readonly string LockOut = "LockOut";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string BadRequest = "BadRequest";
            public const string Forbidden = "Forbidden";
            public const string InternalServerError = "InternalServerError";
            public const string MethodNotAllowed = "MethodNotAllowed";
            public const string NotFound = "NotFound";
            public const string Unauthorized = "Unauthorized";
            public const string LockOut = "LockOut";
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
                public readonly string BadRequest = "BadRequest";
                public readonly string Forbidden = "Forbidden";
                public readonly string Index = "Index";
                public readonly string InternalServerError = "InternalServerError";
                public readonly string LockOut = "LockOut";
                public readonly string MethodNotAllowed = "MethodNotAllowed";
                public readonly string NotFound = "NotFound";
                public readonly string Unauthorized = "Unauthorized";
            }
            public readonly string BadRequest = "~/Views/Error/BadRequest.cshtml";
            public readonly string Forbidden = "~/Views/Error/Forbidden.cshtml";
            public readonly string Index = "~/Views/Error/Index.cshtml";
            public readonly string InternalServerError = "~/Views/Error/InternalServerError.cshtml";
            public readonly string LockOut = "~/Views/Error/LockOut.cshtml";
            public readonly string MethodNotAllowed = "~/Views/Error/MethodNotAllowed.cshtml";
            public readonly string NotFound = "~/Views/Error/NotFound.cshtml";
            public readonly string Unauthorized = "~/Views/Error/Unauthorized.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ErrorController : Jazb.Web.Controllers.ErrorController
    {
        public T4MVC_ErrorController() : base(Dummy.Instance) { }

        [NonAction]
        partial void BadRequestOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult BadRequest()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.BadRequest);
            BadRequestOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ForbiddenOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Forbidden()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Forbidden);
            ForbiddenOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void InternalServerErrorOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult InternalServerError()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InternalServerError);
            InternalServerErrorOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void MethodNotAllowedOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult MethodNotAllowed()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MethodNotAllowed);
            MethodNotAllowedOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void NotFoundOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult NotFound()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NotFound);
            NotFoundOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void UnauthorizedOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Unauthorized()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Unauthorized);
            UnauthorizedOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void LockOutOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult LockOut()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LockOut);
            LockOutOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108