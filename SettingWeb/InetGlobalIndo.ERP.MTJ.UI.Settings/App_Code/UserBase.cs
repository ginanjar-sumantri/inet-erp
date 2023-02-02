using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.User
{
    public abstract class UserBase : SettingsBase
    {
        protected short _menuID = 312;

        protected PermissionLevel _permAccess, _permView, _permEdit, _permAdd, _permDelete;
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "User.aspx";
        protected string _addPage = "UserAdd.aspx";
        protected string _editPage = "UserEdit.aspx";
        protected string _viewPage = "UserView.aspx";
        protected string _changePassPage = "UserChangePassword.aspx";
        protected string _qAndAPage = "UserQuestionAndAnswer.aspx";
        protected string _addDetailPage = "UserMethodAdd.aspx";

        protected string _pageTitleLiteral = "User";

        protected NameValueCollectionExtractor _nvcExtractor;

        public UserBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~UserBase()
        {

        }
    }
}