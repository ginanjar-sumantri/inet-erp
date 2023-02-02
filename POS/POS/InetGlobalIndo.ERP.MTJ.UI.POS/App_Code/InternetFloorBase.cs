using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for MemberTypeBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.Member
{
    public abstract class InternetFloorBase : POSBase
    {
        protected short _menuID = 2375;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code", _tableAndFloorKey = "tableandfloor" ;

        protected string _homePage = "InternetFloor.aspx";
        protected string _addPage = "InternetFloorAdd.aspx";
        protected string _editPage = "InternetFloorEdit.aspx";
        protected string _viewPage = "InternetFloorView.aspx";
        protected string _editDetailPage = "InternetTableEdit.aspx";

        protected string _pageTitleLiteral = "Internet Floor";

        protected NameValueCollectionExtractor _nvcExtractor;

        public InternetFloorBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }
    }
}
