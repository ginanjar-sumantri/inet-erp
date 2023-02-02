using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales
{
    public abstract class DirectSalesBase : SalesBase
    {
        protected short _menuID = 1844;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose, _permRevisi;
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "DirectSales.aspx";
        protected string _addPage = "DirectSalesAdd.aspx";
        protected string _editPage = "DirectSalesEdit.aspx";
        protected string _detailPage = "DirectSalesDetail.aspx";
        protected string _addDetailPage = "DirectSalesDetailAdd.aspx";
        protected string _editDetailPage = "DirectSalesDetailEdit.aspx";
        protected string _viewDetailPage = "DirectSalesDetailView.aspx";
        protected string _addDetailPage2 = "DirectSalesDetailAdd2.aspx";
        protected string _importPage = "SerialNumberImport.aspx";
        protected string _errorPermissionPage = ApplicationConfig.SalesWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeCurrKey = "curr";
        protected string _productKey = "ProductCode";
        protected string _Warehouse = "Warehouse";
        protected string _Location = "Location";
        protected string _serialNumber = "SerialNumber";

        protected string _pageTitleLiteral = "Direct Sales";

        protected NameValueCollectionExtractor _nvcExtractor;

        public DirectSalesBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~DirectSalesBase()
        {

        }
    }
}