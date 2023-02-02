using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.WarehouseGroup
{
    public abstract class WarehouseGroupBase : StockControlBase
    {
        protected short _menuID = 360;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "WrhsGroup.aspx";
        protected string _addPage = "WrhsGroupAdd.aspx";
        protected string _editPage = "WrhsGroupEdit.aspx";
        protected string _viewPage = "WrhsGroupView.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Warehouse Group";

        protected NameValueCollectionExtractor _nvcExtractor;

        public WarehouseGroupBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~WarehouseGroupBase()
        {
        }
    }
}