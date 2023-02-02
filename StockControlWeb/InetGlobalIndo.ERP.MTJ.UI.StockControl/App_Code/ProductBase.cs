using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Product
{
    public class ProductBase : StockControlBase
    {
        protected short _menuID = 376;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "Product.aspx";
        protected string _addPage = "ProductAdd.aspx";
        protected string _editPage = "ProductEdit.aspx";
        protected string _detailPage = "ProductDetail.aspx";
        protected string _addDetailPage = "ProductDetailAdd.aspx";
        protected string _editDetailPage = "ProductDetailEdit.aspx";
        protected string _addSalesPricePage = "ProductSalesPriceAdd.aspx";
        protected string _editSalesPricePage = "ProductSalesPriceEdit.aspx";
        protected string _changePhotoPage = "ProductChangePhoto.aspx";
        protected string _importPage = "ProductImport.aspx";
        protected string _importPage2 = "ProductSalesPriceImport.aspx";

        protected string _addProductAlternatifPage = "ProductAlternatifAdd.aspx";
        protected string _editProductAlternatifPage = "ProductAlternatifEdit.aspx";

        protected string _codeKey = "code";
        protected string _unitBLKey = "UnitCode";
        protected string _currKey = "CurrCode";
        protected string _codeKeyAlternatif = "AlterCode";

        protected string _pageTitleLiteral = "Product";
        protected string _pageTitleLiteral1 = "Product Sales Price";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ProductBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~ProductBase()
        {
        }
    }
}