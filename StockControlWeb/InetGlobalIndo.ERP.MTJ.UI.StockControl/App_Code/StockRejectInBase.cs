using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockRejectIn
{
    public abstract class StockRejectInBase : StockControlBase
    {
        protected short _menuID = 602;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockRejectIn.aspx";
        protected string _addPage = "StockRejectInAdd.aspx";
        protected string _editPage = "StockRejectInEdit.aspx";
        protected string _detailPage = "StockRejectInDetail.aspx";
        protected string _addDetailPage = "StockRejectInDetailAdd.aspx";
        protected string _editDetailPage = "StockRejectInDetailEdit.aspx";
        protected string _viewDetailPage = "StockRejectInDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";

        protected string _pageTitleLiteral = "Stock - Reject In";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockRejectInBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockRejectInBase()
        {
        }
    }
}