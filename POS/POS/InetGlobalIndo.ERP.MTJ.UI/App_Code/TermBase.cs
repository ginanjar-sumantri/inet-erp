using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Home.Term
{
    public abstract class TermBase : HomeBase
    {
        protected short _menuID = 26;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "Term.aspx";
        protected string _addPage = "TermAdd.aspx";
        protected string _viewPage = "TermView.aspx";
        protected string _editDetailPage = "TermDtEdit.aspx";

        protected string _codeKey = "code";
        protected string _termCodeKey = "TermCode";

        protected string _pageTitleLiteral = "Term";

        protected NameValueCollectionExtractor _nvcExtractor;

        public TermBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~TermBase()
        {

        }
    }
}