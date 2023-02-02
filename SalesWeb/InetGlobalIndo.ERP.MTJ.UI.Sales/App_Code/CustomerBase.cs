using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Customer
{
    public abstract class CustomerBase : SalesBase
    {
        protected short _menuID = 57;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "Customer.aspx";
        protected string _addPage = "CustomerAdd.aspx";
        protected string _editPage = "CustomerEdit.aspx";
        protected string _viewPage = "CustomerView.aspx";
        protected string _addDetailPage = "CustomerContactAdd.aspx";
        protected string _viewDetailPage = "CustomerContactView.aspx";
        protected string _editDetailPage = "CustomerContactEdit.aspx";
        protected string _editAddressPage = "CustomerAddressEdit.aspx";
        protected string _AddAddressPage = "CustomerAddressAdd.aspx";
        protected string _errorPermissionPage = ApplicationConfig.SalesWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _itemCodeKey = "itemCode";
        protected string _deliveryCodeKey = "deliveryCode";

        protected string _pageTitleLiteral = "Customer";
        protected string _pageTitleLiteral2 = "Customer Contact";
        protected string _pageTitleLiteral3 = "Customer Address";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CustomerBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~CustomerBase()
        {

        }
    }
}