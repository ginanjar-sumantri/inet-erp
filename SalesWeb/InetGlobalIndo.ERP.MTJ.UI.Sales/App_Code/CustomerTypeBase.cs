using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.CustomerType
{
    public abstract class CustomerTypeBase : SalesBase
    {
        protected short _menuID = 49;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "CustomerType.aspx";
        protected string _addPage = "CustomerTypeAdd.aspx";
        protected string _editPage = "CustomerTypeEdit.aspx";
        protected string _viewPage = "CustomerTypeView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.SalesWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Customer Type";

        public CustomerTypeBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~CustomerTypeBase()
        {

        }
    }
}