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
namespace InetGlobalIndo.ERP.MTJ.UI.POS.POSConfiguration
{
    public abstract class POSConfigurationBase : POSBase
    {
        protected short _menuID = 2338;
        protected PermissionLevel _permAccess, _permEdit;  //_permAdd, _permDelete, _permView,
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "MemberType.aspx";
        protected string _addPage = "MemberTypeAdd.aspx";
        protected string _editPage = "MemberTypeEdit.aspx";
        protected string _viewPage = "MemberTypeView.aspx";

        protected string _pageTitleLiteral = "POS Configuration";

        protected NameValueCollectionExtractor _nvcExtractor;

        public POSConfigurationBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
