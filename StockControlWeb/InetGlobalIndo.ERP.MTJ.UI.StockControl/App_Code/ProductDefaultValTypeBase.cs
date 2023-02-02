using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductDefaultValType
{
    public abstract class ProductDefaultValTypeBase : StockControlBase
    {
        protected short _menuID = 1724;
        protected PermissionLevel _permAccess,_permEdit;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "ProductDefaultValType.aspx";
        protected string _editPage = "ProductDefaultValTypeEdit.aspx";
        
        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Product Default Value Type";

        protected NameValueCollectionExtractor _nvcExtractor;


        public ProductDefaultValTypeBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~ProductDefaultValTypeBase()
        {
        }
    }
}