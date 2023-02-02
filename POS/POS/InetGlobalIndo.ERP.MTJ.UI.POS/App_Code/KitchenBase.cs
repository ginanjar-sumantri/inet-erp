using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Kitchen
{
    public abstract class KitchenBase : POSBase
    {
        protected short _menuID = 2346;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.POSWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _homePage = "Kitchen.aspx";
        protected string _addPage = "KitchenAdd.aspx";
        protected string _editPage = "KitchenEdit.aspx";
        protected string _viewPage = "KitchenView.aspx";

        protected string _addDetailPage = "KitchenDtAdd.aspx";
        protected string _editDetailPage = "KitchenDtEdit.aspx";
        protected string _viewDetailPage = "KitchenDtView.aspx";

        protected string _pageTitleLiteral = "Kitchen";
        protected string _pageTitleDetailLiteral = "Kitchen";

        protected NameValueCollectionExtractor _nvcExtractor;

        public KitchenBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~KitchenBase()
        {
        }
    }
}
