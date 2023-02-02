using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockBeginning
{
    public class StockBeginningBase : StockControlBase
    {
        protected short _menuID = 567;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockBeginning.aspx";
        protected string _addPage = "StockBeginningAdd.aspx";
        protected string _editPage = "StockBeginningEdit.aspx";
        protected string _detailPage = "StockBeginningDetail.aspx";
        protected string _addDetailPage = "StockBeginningDetailAdd.aspx";
        protected string _editDetailPage = "StockBeginningDetailEdit.aspx";
        protected string _viewDetailPage = "StockBeginningDetailView.aspx";
        protected string _addDetailPage2 = "StockBeginningDetailAdd2.aspx";
        protected string _editDetailPage2 = "StockBeginningDetailEdit2.aspx";
        protected string _importPage = "NCPImport.aspx";
        protected string _stockBeginningImportPage = "StockBeginningImport.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";
        protected string _detailKey = "DetailCode";

        protected string _pageTitleLiteral = "Stock - Beginning";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockBeginningBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockBeginningBase()
        {
        }
    }
}