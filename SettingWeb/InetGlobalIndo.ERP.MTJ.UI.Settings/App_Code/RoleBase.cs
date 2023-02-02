using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Role
{
    public class RoleBase : SettingsBase
    {
        protected string _codeKey = "code";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "Role.aspx";
        protected string _addPage = "RoleAdd.aspx";
        protected string _editPage = "RoleEdit.aspx";
        protected string _viewPage = "RoleView.aspx";

        protected string _pageTitleLiteral = "Role";

        protected NameValueCollectionExtractor _nvcExtractor;

        public RoleBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~RoleBase()
        {

        }
    }
}