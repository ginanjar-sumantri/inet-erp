using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockIssueRequest
{
    public class StockIssueRequestBase : StockControlBase
    {
        protected short _menuID = 503;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockIssueRequest.aspx";
        protected string _addPage = "StockIssueRequestAdd.aspx";
        protected string _editPage = "StockIssueRequestEdit.aspx";
        protected string _detailPage = "StockIssueRequestDetail.aspx";
        protected string _addDetailPage = "StockIssueRequestDetailAdd.aspx";
        protected string _editDetailPage = "StockIssueRequestDetailEdit.aspx";
        protected string _viewDetailPage = "StockIssueRequestDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";

        protected string _pageTitleLiteral = "Stock - Issue Request";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockIssueRequestBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockIssueRequestBase()
        {
        }
    }
}