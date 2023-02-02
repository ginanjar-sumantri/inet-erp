using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Company
{
    public abstract class CompanyBase : SettingsBase
    {
        protected string _compKey = "code";
        protected string _codeKey = "UserCode";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "Company.aspx";
        protected string _addPage = "CompanyAdd.aspx";
        protected string _editPage = "CompanyEdit.aspx";
        protected string _viewPage = "CompanyView.aspx";
        protected string _addDetailPage = "CompanyDatabaseAdd.aspx";
        protected string _addDetailPage2 = "CompanyUserAdd.aspx";
        protected string _editDetailPage2 = "CompanyUserEdit.aspx";
        protected string _viewDetailPage2 = "CompanyUserView.aspx";
        protected string _addDetailPage3 = "CompanyRoleAdd.aspx";
        protected string _editDetailPage3 = "CompanyRoleEdit.aspx";
        protected string _viewDetailPage3 = "CompanyRoleView.aspx";

        protected string _pageTitleLiteral = "Company";
        protected string _pageTitleLiteral2 = "Company - Database";
        protected string _pageTitleLiteral3 = "Company - User";
        protected string _pageTitleLiteral4 = "Company - Role";
        
        protected NameValueCollectionExtractor _nvcExtractor;


        public CompanyBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~CompanyBase()
        {

        }
    }
}