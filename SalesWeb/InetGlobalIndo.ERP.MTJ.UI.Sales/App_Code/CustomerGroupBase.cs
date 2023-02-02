using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.CustomerGroup
{
    public abstract class CustomerGroupBase : SalesBase
    {
        protected short _menuID = 48;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "CustomerGroup.aspx";
        protected string _addPage = "CustomerGroupAdd.aspx";
        protected string _editPage = "CustomerGroupEdit.aspx";
        protected string _viewPage = "CustomerGroupView.aspx";
        protected string _addDetailPage = "CustomerGroupAccountAdd.aspx";
        protected string _editDetailPage = "CustomerGroupAccountEdit.aspx";
        protected string _viewDetailPage = "CustomerGroupAccountView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.SalesWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _currCodeKey = "currcode";

        protected NameValueCollectionExtractor _nvcExtractor; 
              
        protected string _pageTitleLiteral = "Customer Group";

        public CustomerGroupBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~CustomerGroupBase()
        { 
        
        }
    }
}