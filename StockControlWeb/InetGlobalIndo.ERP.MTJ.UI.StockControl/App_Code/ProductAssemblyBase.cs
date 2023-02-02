using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductAssembly
{
    public abstract class ProductAssemblyBase : StockControlBase
    {
        protected short _menuID = 2543;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "ProductAssembly.aspx";
        protected string _addPage = "ProductAssemblyAdd.aspx";
        protected string _editPage = "ProductAssemblyEdit.aspx";
        protected string _viewPage = "ProductAssemblyView.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Product Assembly";

        protected NameValueCollectionExtractor _nvcExtractor;


        public ProductAssemblyBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~ProductAssemblyBase()
        {
        }
    }
}