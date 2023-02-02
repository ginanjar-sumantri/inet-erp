using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

/// <summary>
/// Summary description for TourBase
/// </summary>

namespace InetGlobalIndo.ERP.MTJ.UI.Tour
{
    public abstract class TourBase : System.Web.UI.Page
    {
        private string _pageTitle = ApplicationConfig.MembershipAppName + " :: Tour";

        protected short _moduleID = 2456;
        protected PermissionLevel _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        public TourBase()
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
