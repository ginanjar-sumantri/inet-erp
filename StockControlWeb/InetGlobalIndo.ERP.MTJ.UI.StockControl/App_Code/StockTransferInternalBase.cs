using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransferInternal
{
    public abstract class StockTransferInternalBase : StockControlBase
    {
        protected short _menuID = 631;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose, _permTaxPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockTransferInternal.aspx";
        protected string _addPage = "StockTransferInternalAdd.aspx";
        protected string _editPage = "StockTransferInternalEdit.aspx";
        protected string _detailPage = "StockTransferInternalDetail.aspx";
        protected string _addDetailPage = "StockTransferInternalDetailAdd.aspx";
        protected string _editDetailPage = "StockTransferInternalDetailEdit.aspx";
        protected string _viewDetailPage = "StockTransferInternalDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";
        protected string _locationKey2 = "LocationCode2";

        protected string _pageTitleLiteral = "Stock - Transfer Internal";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockTransferInternalBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockTransferInternalBase()
        {
        }
    }
}