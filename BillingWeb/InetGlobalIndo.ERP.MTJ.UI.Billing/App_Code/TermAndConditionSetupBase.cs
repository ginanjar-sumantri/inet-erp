﻿using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.TermAndConditionSetup
{
    public abstract class TermAndConditionSetupBase : System.Web.UI.Page
    {
        protected short _menuID = 1831;
        protected PermissionLevel _permAccess, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "TermAndConditionSetup.aspx";
        protected string _editPage = "TermAndConditionSetupEdit.aspx";
        protected string _viewPage = "TermAndConditionSetupView.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Terms and Conditions Setup";

        protected NameValueCollectionExtractor _nvcExtractor;

        public TermAndConditionSetupBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }
    }
}