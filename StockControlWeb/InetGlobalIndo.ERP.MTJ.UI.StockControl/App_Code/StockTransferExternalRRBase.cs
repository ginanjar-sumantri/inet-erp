using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransferExternalRR
{
    public abstract class StockTransferExternalRRBase : StockControlBase
    {
        protected short _menuID = 623;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose, _permTaxPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockTransferExternalRR.aspx";
        protected string _addPage = "StockTransferExternalRRAdd.aspx";
        protected string _editPage = "StockTransferExternalRREdit.aspx";
        protected string _detailPage = "StockTransferExternalRRDetail.aspx";
        protected string _addDetailPage = "StockTransferExternalRRDetailAdd.aspx";
        protected string _editDetailPage = "StockTransferExternalRRDetailEdit.aspx";
        protected string _viewDetailPage = "StockTransferExternalRRDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationSrc";

        protected string _pageTitleLiteral = "Stock - Transfer Receiving";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockTransferExternalRRBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockTransferExternalRRBase()
        {
        }
    }
}