using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl
{
    public abstract class ProductTypeBase : StockControlBase
    {
        protected short _menuID = 356;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "ProductType.aspx";
        protected string _addPage = "ProductTypeAdd.aspx";
        protected string _editPage = "ProductTypeEdit.aspx";
        protected string _detailPage = "ProductTypeDetail.aspx";
        protected string _addDetailPage = "ProductTypeDetailAdd.aspx";
        protected string _editDetailPage = "ProductTypeDetailEdit.aspx";
        protected string _viewDetailPage = "ProductTypeDetailView.aspx";
        //protected string _addDetailPage2 = "ProdType_PGAdd.aspx";

        protected string _codeKey = "code";
        protected string _wrhsKey = "WrhsCode";

        protected string _pageTitleLiteral = "Product Type";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ProductTypeBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~ProductTypeBase()
        {

        }
    }
}