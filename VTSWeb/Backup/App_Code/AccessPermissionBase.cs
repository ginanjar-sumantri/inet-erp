using System;
using VTSWeb.Enum;
using VTSWeb.Common;
using VTSWeb.SystemConfig;

namespace VTSWeb.UI
{
    public abstract class AccessPermissionBase : System.Web.UI.Page
    {
        protected bool _permission;
        protected bool _status;

        protected String _homePage = "AccessPermission.aspx";

        protected String _pageTitleLiteral = "Access Permission Form";

        protected NameValueCollectionExtractor _nvcExtractor;

        public AccessPermissionBase()
        {
        }

        ~AccessPermissionBase()
        {
        }
    }
}
