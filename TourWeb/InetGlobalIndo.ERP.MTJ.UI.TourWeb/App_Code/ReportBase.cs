using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

/// <summary>
/// Summary description for TicketingBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.Tour.Report
{
    public class ReportBase : TourBase
    {
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected short _menuIDTransactionByDate = 2479;

        protected PermissionLevel _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.TourWebAppURL + "ErrorPermission.aspx";

        public ReportBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~ReportBase()
        {

        }
    }
}
