using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for MemberTypeBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.Reason
{
    public abstract class ReasonBase : POSBase
    {
        protected short _menuID = 2430;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "Reason.aspx";
        protected string _addPage = "ReasonAdd.aspx";
        protected string _editPage = "ReasonEdit.aspx";
        protected string _viewPage = "ReasonView.aspx";

        protected string _pageTitleLiteral = "Reason";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ReasonBase()
        {
        }
    }
}
