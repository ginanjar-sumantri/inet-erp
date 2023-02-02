using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.WarehouseLocation
{
    public abstract class WarehouseLocationBase : StockControlBase
    {
        protected short _menuID = 551;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "WLocationCode";

        protected string _homePage = "WarehouseLocation.aspx";
        protected string _addPage = "WarehouseLocationAdd.aspx";
        protected string _editPage = "WarehouseLocationEdit.aspx";
        protected string _viewPage = "WarehouseLocationView.aspx";

        protected string _pageTitleLiteral = "Warehouse Location";

        protected NameValueCollectionExtractor _nvcExtractor;


        public WarehouseLocationBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~WarehouseLocationBase()
        {
        }
    }
}