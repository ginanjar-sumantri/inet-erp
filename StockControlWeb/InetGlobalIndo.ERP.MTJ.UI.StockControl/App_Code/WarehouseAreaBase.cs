using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.WarehouseArea
{
    public abstract class WarehouseAreaBase : StockControlBase
    {
        protected short _menuID = 368;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "WrhsArea.aspx";
        protected string _addPage = "WrhsAreaAdd.aspx";
        protected string _editPage = "WrhsAreaEdit.aspx";
        protected string _viewPage = "WrhsAreaView.aspx";

        protected string _pageTitleLiteral = "Warehouse Area";

        protected NameValueCollectionExtractor _nvcExtractor;

        public WarehouseAreaBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~WarehouseAreaBase()
        {
        }
    }
}