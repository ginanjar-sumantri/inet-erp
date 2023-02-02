using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Database
{
    public abstract class DatabaseBase : SettingsBase
    {
        protected string _homePage = "Database.aspx";
        protected string _addPage = "DatabaseAdd.aspx";
        protected string _editPage = "DatabaseEdit.aspx";
        protected string _viewPage = "DatabaseView.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Database";

        protected NameValueCollectionExtractor _nvcExtractor;

        public DatabaseBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~DatabaseBase()
        {

        }
    }
}