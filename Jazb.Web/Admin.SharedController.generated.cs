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
namespace T4MVC.Admin
{
    public class SharedController
    {

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
                public readonly string _Alert = "_Alert";
                public readonly string _Layout = "_Layout";
                public readonly string _MINIPROFILER_UPDATED_Layout = "_MINIPROFILER UPDATED Layout";
                public readonly string _Toastr = "_Toastr";
                public readonly string _ValidationSummery = "_ValidationSummery";
                public readonly string Error = "Error";
            }
            public readonly string _Alert = "~/Areas/Admin/Views/Shared/_Alert.cshtml";
            public readonly string _Layout = "~/Areas/Admin/Views/Shared/_Layout.cshtml";
            public readonly string _MINIPROFILER_UPDATED_Layout = "~/Areas/Admin/Views/Shared/_MINIPROFILER UPDATED Layout.cshtml";
            public readonly string _Toastr = "~/Areas/Admin/Views/Shared/_Toastr.cshtml";
            public readonly string _ValidationSummery = "~/Areas/Admin/Views/Shared/_ValidationSummery.cshtml";
            public readonly string Error = "~/Areas/Admin/Views/Shared/Error.cshtml";
            static readonly _DisplayTemplatesClass s_DisplayTemplates = new _DisplayTemplatesClass();
            public _DisplayTemplatesClass DisplayTemplates { get { return s_DisplayTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _DisplayTemplatesClass
            {
                public readonly string Enum = "Enum";
            }
            static readonly _EditorTemplatesClass s_EditorTemplates = new _EditorTemplatesClass();
            public _EditorTemplatesClass EditorTemplates { get { return s_EditorTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _EditorTemplatesClass
            {
                public readonly string Enum = "Enum";
            }
        }
    }

}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108
