using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockServiceOut
{
    public class StockServiceOutBase : StockControlBase
    {
        protected short _menuID = 2496;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockServiceOut.aspx";
        protected string _detailPage = "StockServiceOutDetail.aspx";
        protected string _editPage = "StockServiceOutEdit.aspx";
        protected string _addPage = "StockServiceOutAdd.aspx";
        protected string _addDetailPage = "StockServiceOutDetailAdd.aspx";
        protected string _editDetailPage = "StockServiceOutDetailEdit.aspx";
        protected string _viewDetailPage = "StockServiceOutDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";
        protected string _imeiNoKey = "ImeiNo";

        protected string _pageTitleLiteral = "Stock Service Out";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockServiceOutBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockServiceOutBase()
        {
        }
    }
}