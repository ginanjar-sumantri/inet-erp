using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransRequest
{
    public abstract class StockTransRequestBase : StockControlBase
    {
        protected short _menuID = 510;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose, _permTaxPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockTransRequest.aspx";
        protected string _addPage = "StockTransRequestAdd.aspx";
        protected string _editPage = "StockTransRequestEdit.aspx";
        protected string _detailPage = "StockTransRequestDetail.aspx";
        protected string _addDetailPage = "StockTransRequestDetailAdd.aspx";
        protected string _editDetailPage = "StockTransRequestDetailEdit.aspx";
        protected string _viewDetailPage = "StockTransRequestDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";

        protected string _pageTitleLiteral = "Stock - Transfer Request";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockTransRequestBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);

        }

        ~StockTransRequestBase()
        {
        }
    }
}