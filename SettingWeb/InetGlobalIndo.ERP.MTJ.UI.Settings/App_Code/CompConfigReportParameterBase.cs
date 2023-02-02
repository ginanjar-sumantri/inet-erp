using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.CompConfigReportParameter
{
    public class CompanyConfigReportParameterBase : SettingsBase
    {
        protected short _menuID = 2493;
        protected PermissionLevel _permAccess, _permView;
        protected string _codeKey = "code";
        protected string _errorPermissionPage = ApplicationConfig.SettingsWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "CompanyConfigReportParameter.aspx";
        
        protected string _pageTitleLiteral = "Company Configuration Report Parameter";

        public CompanyConfigReportParameterBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~CompanyConfigReportParameterBase()
        {
        }
    }
}