using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.CustomerSellingPrice
{
    public abstract class CustomerSellingPriceBase : SalesBase
    {
        protected short _menuID = 57;
        protected PermissionLevel _permAccess, _permView;
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;
  
        protected string _errorPermissionPage = ApplicationConfig.SalesWebAppURL + "ErrorPermission.aspx";

        protected string _pageTitleLiteral = "Customer Selling Price";
 
        protected NameValueCollectionExtractor _nvcExtractor;

        public CustomerSellingPriceBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~CustomerSellingPriceBase()
        {

        }
    }
}