using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockRejectOut
{
    public abstract class StockRejectOutBase : StockControlBase
    {
        protected short _menuID = 595;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose, _permTaxPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockRejectOut.aspx";
        protected string _addPage = "StockRejectOutAdd.aspx";
        protected string _editPage = "StockRejectOutEdit.aspx";
        protected string _detailPage = "StockRejectOutDetail.aspx";
        protected string _addDetailPage = "StockRejectOutDetailAdd.aspx";
        protected string _editDetailPage = "StockRejectOutDetailEdit.aspx";
        protected string _viewDetailPage = "StockRejectOutDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _referenceKey = "refCode";
        protected string _locationKey = "LocationCode";

        protected string _pageTitleLiteral = "Stock - Reject Out";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockRejectOutBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockRejectOutBase()
        {
        }
    }
}