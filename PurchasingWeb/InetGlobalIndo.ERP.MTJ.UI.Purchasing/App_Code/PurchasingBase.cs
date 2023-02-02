using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing
{
    public abstract class PurchasingBase : System.Web.UI.Page
    {
        private string _pageTitle = ApplicationConfig.MembershipAppName + " :: Purchasing";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected short _moduleID = 21;
        protected PermissionLevel _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        public PurchasingBase()
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