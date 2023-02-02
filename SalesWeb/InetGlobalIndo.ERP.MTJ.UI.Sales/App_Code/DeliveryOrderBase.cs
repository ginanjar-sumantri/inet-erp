using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;


namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DeliveryOrder
{
    public abstract class DeliveryOrderBase : SalesBase
    {
        protected short _menuID = 697;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose;
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "DeliveryOrder.aspx";
        protected string _addPage = "DeliveryOrderAdd.aspx";
        protected string _editPage = "DeliveryOrderEdit.aspx";
        protected string _detailPage = "DeliveryOrderDetail.aspx";
        protected string _addDetailPage = "DeliveryOrderDetailAdd.aspx";
        protected string _editDetailPage = "DeliveryOrderDetailEdit.aspx";
        protected string _viewDetailPage = "DeliveryOrderDetailView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.SalesWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _itemIDKey = "ItemID";

        protected string _pageTitleLiteral = "Request Delivery Order";


        protected NameValueCollectionExtractor _nvcExtractor;

        public DeliveryOrderBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~DeliveryOrderBase()
        {

        }
    }
}