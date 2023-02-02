using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockReceivingCustomer
{
    public class StockReceivingCustomerBase : StockControlBase
    {
        protected short _menuID = 646;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockReceivingCustomer.aspx";
        protected string _detailPage = "StockReceivingCustomerDetail.aspx";
        protected string _editPage = "StockReceivingCustomerEdit.aspx";
        protected string _addPage = "StockReceivingCustomerAdd.aspx";
        protected string _addDetailPage = "StockReceivingCustomerDetailAdd.aspx";
        protected string _editDetailPage = "StockReceivingCustomerDetailEdit.aspx";
        protected string _viewDetailPage = "StockReceivingCustomerDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";

        protected string _pageTitleLiteral = "Stock Receiving Customer";

        protected NameValueCollectionExtractor _nvcExtractor;

        public StockReceivingCustomerBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~StockReceivingCustomerBase()
        {
        }
    }
}