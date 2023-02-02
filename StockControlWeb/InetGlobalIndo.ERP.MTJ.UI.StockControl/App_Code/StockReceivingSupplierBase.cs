using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockReceivingSupplier
{
    public class StockReceivingSupplierBase : StockControlBase
    {
        protected short _menuID = 660;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockReceivingSupplier.aspx";
        protected string _detailPage = "StockReceivingSupplierDetail.aspx";
        protected string _editPage = "StockReceivingSupplierEdit.aspx";
        protected string _addPage = "StockReceivingSupplierAdd.aspx";
        protected string _addDetailPage = "StockReceivingSupplierDetailAdd.aspx";
        protected string _editDetailPage = "StockReceivingSupplierDetailEdit.aspx";
        protected string _viewDetailPage = "StockReceivingSupplierDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";

        protected string _pageTitleLiteral = "Stock Receiving Supplier";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockReceivingSupplierBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockReceivingSupplierBase()
        {
        }
    }
}