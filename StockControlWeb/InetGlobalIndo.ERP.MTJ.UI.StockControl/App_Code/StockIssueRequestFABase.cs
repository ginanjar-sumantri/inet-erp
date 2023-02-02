using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockIssueRequestFA
{
    public abstract class StockIssueRequestFABase : StockControlBase
    {
        protected short _menuID = 589;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockIssueRequestFA.aspx";
        protected string _addPage = "StockIssueRequestFAAdd.aspx";
        protected string _editPage = "StockIssueRequestFAEdit.aspx";
        protected string _detailPage = "StockIssueRequestFADetail.aspx";
        protected string _addDetailPage = "StockIssueRequestFADetailAdd.aspx";
        protected string _editDetailPage = "StockIssueRequestFADetailEdit.aspx";
        protected string _viewDetailPage = "StockIssueRequestFADetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";

        protected string _pageTitleLiteral = "Stock - Issue Request FA";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockIssueRequestFABase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockIssueRequestFABase()
        {
        }
    }
}