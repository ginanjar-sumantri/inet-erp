using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.EmailNotificationSetup
{
    public abstract class EmailNotificationSetupBase : System.Web.UI.Page
    {
        private string _pageTitle = ApplicationConfig.MembershipAppName + " :: EmailNotificationSetup";

        protected short _moduleID = 1820;
        protected PermissionLevel _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "EmailNotificationSetup.aspx";
        protected string _editPage = "EmailNotificationSetupEdit.aspx";
        protected string _viewPage = "EmailNotificationSetupView.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Email Notification Setup";

        protected NameValueCollectionExtractor _nvcExtractor;

        public EmailNotificationSetupBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);

            this.Title = this._pageTitle;
        }
    }
}