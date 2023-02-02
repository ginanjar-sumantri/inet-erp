using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockChangeGood
{
    public abstract class StockChangeGoodBase : StockControlBase
    {
        protected short _menuID = 653;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockChangeGood.aspx";
        protected string _addPage = "StockChangeGoodAdd.aspx";
        protected string _editPage = "StockChangeGoodEdit.aspx";
        protected string _detailPage = "StockChangeGoodDetail.aspx";
        protected string _addDetailPage = "StockChangeGoodDetailAdd.aspx";
        protected string _editDetailPage = "StockChangeGoodDetailEdit.aspx";
        protected string _viewDetailPage = "StockChangeGoodDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";
        protected string _locationKey2 = "LocationCode2";

        protected string _pageTitleLiteral = "Stock - Change Good";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockChangeGoodBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockChangeGoodBase()
        {
        }
    }
}