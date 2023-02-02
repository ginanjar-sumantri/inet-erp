using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.MenuServiceType
{
    public abstract class MenuServiceTypeBase : POSBase
    {
        protected short _menuID = 2350;
        //protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        
        protected string _errorPermissionPage = ApplicationConfig.POSWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";
        protected string _codeKey2 = "code2";
        protected string _codeItemKey = "CodeItem";

        protected string _homePage = "MenuServiceType.aspx";
        protected string _viewPage = "MenuServiceTypeView.aspx";
        protected string _addDetailPage = "MenuServiceTypeDtAdd.aspx";
        protected string _editDetailPage = "MenuServiceTypeDtEdit.aspx";
        protected string _editPage = "MenuServiceTypeEdit.aspx";

        protected string _pageTitleLiteral = "Menu Service Type";

        protected NameValueCollectionExtractor _nvcExtractor;

        public MenuServiceTypeBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~MenuServiceTypeBase()
        {
        }
    }
}
