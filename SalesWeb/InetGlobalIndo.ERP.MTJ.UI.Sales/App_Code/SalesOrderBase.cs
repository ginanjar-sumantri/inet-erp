using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.SalesOrder
{
    public abstract class SalesOrderBase : SalesBase
    {
        protected short _menuID = 690;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose, _permRevisi;
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "SalesOrder.aspx";
        protected string _addPage = "SalesOrderAdd.aspx";
        protected string _editPage = "SalesOrderEdit.aspx";
        protected string _detailPage = "SalesOrderDetail.aspx";
        protected string _importPage = "SalesOrderImport.aspx";
        protected string _addDetailPage = "SalesOrderDetailAdd.aspx";
        protected string _editDetailPage = "SalesOrderDetailEdit.aspx";
        protected string _viewDetailPage = "SalesOrderDetailView.aspx";
        protected string _viewProductDetailPage = "SalesOrderDetailProductView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.SalesWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeRevisiKey = "revisi";
        protected string _productKey = "ProductCode";
        protected string _itemID = "ItemID";

        protected string _pageTitleLiteral = "Sales Order";

        protected NameValueCollectionExtractor _nvcExtractor;

        public SalesOrderBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~SalesOrderBase()
        {

        }
    }
}