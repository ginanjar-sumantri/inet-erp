using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductFormula
{
    public abstract class ProductFormulaBase : StockControlBase
    {
        protected short _menuID = 1800;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "ProductFormula.aspx";
        protected string _addPage = "ProductFormulaAdd.aspx";
        protected string _editPage = "ProductFormulaEdit.aspx";
        protected string _detailPage = "ProductFormulaView.aspx";
        protected string _addDetailPage = "ProductFormulaDetailAdd.aspx";
        protected string _editDetailPage = "ProductFormulaDetailEdit.aspx";
        protected string _viewDetailPage = "ProductFormulaDetailView.aspx";
        protected string _importPage = "NCPImport.aspx";
        protected string _addDetailPage2 = "ProductFormulaDetailAdd2.aspx";
        protected string _editDetailPage2 = "ProductFormulaDetailEdit2.aspx";
        protected string _codeKey = "code";
        protected string _detailKey = "DetailCode";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Product Formula";

        public ProductFormulaBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~ProductFormulaBase()
        {
        }
    }
}