using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Warehouse
{
    public class WarehouseBase : StockControlBase
    {
        protected short _menuID = 364;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClosem, _permTaxPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "Warehouse.aspx";
        protected string _addPage = "WarehouseAdd.aspx";
        protected string _editPage = "WarehouseEdit.aspx";
        protected string _detailPage = "WarehouseDetail.aspx";
        protected string _addDetailPage = "WarehouseDetailAdd.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Warehouse";

        protected NameValueCollectionExtractor _nvcExtractor;

        public WarehouseBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~WarehouseBase()
        {
        }
    }
}