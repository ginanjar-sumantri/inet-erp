using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.PriceGroup
{
    public abstract class PriceGroupBase : StockControlBase
    {
        protected short _menuID = 1725;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "PriceGroup.aspx";
        protected string _addPage = "PriceGroupAdd.aspx";
        protected string _editPage = "PriceGroupEdit.aspx";
        protected string _viewPage = "PriceGroupView.aspx";
        protected string _importPage = "PriceGroupImport.aspx";

        protected string _codeKey = "code";
        protected string _yearKey = "YearCode";

        protected string _pageTitleLiteral = "Price Group";

        protected NameValueCollectionExtractor _nvcExtractor;


        public PriceGroupBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~PriceGroupBase()
        {
        }
    }
}