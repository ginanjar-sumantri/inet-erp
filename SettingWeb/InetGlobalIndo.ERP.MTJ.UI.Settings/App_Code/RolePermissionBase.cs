using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.RolePermission
{
    public abstract class RolePermissionBase : SettingsBase
    {
        protected string _userKey = "code";
        protected string _menuKey = "MenuCode";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "RolePermission.aspx";
        protected string _addPage = "RolePermissionAdd.aspx";
        protected string _editPage = "RolePermissionEdit.aspx";

        protected string _pageTitleLiteral = "Role Permission";

        protected NameValueCollectionExtractor _nvcExtractor;

        public RolePermissionBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~RolePermissionBase()
        {
        }
    }
}