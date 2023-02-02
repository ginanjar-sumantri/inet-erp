using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockIssueFixedAsset
{
    public abstract class StockIssueFixedAssetBase : StockControlBase
    {
        protected short _menuID = 581;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockIssueFA.aspx";
        protected string _addPage = "StockIssueFAAdd.aspx";
        protected string _editPage = "StockIssueFAEdit.aspx";
        protected string _detailPage = "StockIssueFADetail.aspx";
        protected string _addDetailPage = "StockIssueFADetailAdd.aspx";
        protected string _editDetailPage = "StockIssueFADetailEdit.aspx";
        protected string _viewDetailPage = "StockIssueFADetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";

        protected string _pageTitleLiteral = "Stock - Issue Fixed Asset";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockIssueFixedAssetBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockIssueFixedAssetBase()
        {
        }
    }
}