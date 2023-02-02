using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.RequestSalesRetur
{
    public abstract class RequestSalesReturBase : SalesBase
    {
        protected short _menuID = 517;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permPrintPreview, _permUnposting, _permClose;
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "RequestSalesRetur.aspx";
        protected string _addPage = "RequestSalesReturAdd.aspx";
        protected string _editPage = "RequestSalesReturEdit.aspx";
        protected string _detailPage = "RequestSalesReturDetail.aspx";
        protected string _addDetailPage = "RequestSalesReturDetailAdd.aspx";
        protected string _editDetailPage = "RequestSalesReturDetailEdit.aspx";
        protected string _viewDetailPage = "RequestSalesReturDetailView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.SalesWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";

        protected string _pageTitleLiteral = "Sales Retur Request";

        protected NameValueCollectionExtractor _nvcExtractor;

        public RequestSalesReturBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~RequestSalesReturBase()
        {

        }
    }
}