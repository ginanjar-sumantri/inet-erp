using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockServiceIn
{
    public class StockServiceInBase : StockControlBase
    {
        protected short _menuID = 2495;
        protected PermissionLevel _permAccess = PermissionLevel.EntireOU, _permAdd = PermissionLevel.EntireOU, _permDelete = PermissionLevel.EntireOU, _permView = PermissionLevel.EntireOU, _permEdit = PermissionLevel.EntireOU, _permGetApproval = PermissionLevel.EntireOU, _permApprove = PermissionLevel.EntireOU, _permPosting = PermissionLevel.EntireOU, _permUnposting = PermissionLevel.EntireOU, _permPrintPreview = PermissionLevel.EntireOU;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme = "", _rowColor = "", _rowColorAlternate = "", _rowColorHover = "";

        protected string _homePage = "StockServiceIn.aspx";
        protected string _detailPage = "StockServiceInDetail.aspx";
        protected string _editPage = "StockServiceInEdit.aspx";
        protected string _addPage = "StockServiceInAdd.aspx";
        protected string _addDetailPage = "StockServiceInDetailAdd.aspx";
        protected string _editDetailPage = "StockServiceInDetailEdit.aspx";
        protected string _viewDetailPage = "StockServiceInDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";
        protected string _imeiNoKey = "ImeiNo";

        protected string _pageTitleLiteral = "Stock Service In";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockServiceInBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockServiceInBase()
        {
        }
    }
}