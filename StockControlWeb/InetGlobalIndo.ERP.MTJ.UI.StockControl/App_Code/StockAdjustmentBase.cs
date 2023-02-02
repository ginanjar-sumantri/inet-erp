using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockAdjustment
{
    public abstract class StockAdjustmentBase : StockControlBase
    {
        protected short _menuID = 560;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockAdjustment.aspx";
        protected string _addPage = "StockAdjustmentAdd.aspx";
        protected string _editPage = "StockAdjustmentEdit.aspx";
        protected string _detailPage = "StockAdjustmentDetail.aspx";
        protected string _addDetailPage = "StockAdjustmentDetailAdd.aspx";
        protected string _editDetailPage = "StockAdjustmentDetailEdit.aspx";
        protected string _viewDetailPage = "StockAdjustmentDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";

        protected string _pageTitleLiteral = "Stock - Adjustment";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockAdjustmentBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockAdjustmentBase()
        {
        }
    }
}