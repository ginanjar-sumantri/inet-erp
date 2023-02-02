using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductDisAssembly
{
    public abstract class ProductDisAssemblyBase : StockControlBase
    {
        protected short _menuID = 1803;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "ProductDisAssembly.aspx";
        protected string _addPage = "ProductDisAssemblyAdd.aspx";
        protected string _editPage = "ProductDisAssemblyEdit.aspx";
        protected string _viewPage = "ProductDisAssemblyView.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Product DisAssembly";

        protected NameValueCollectionExtractor _nvcExtractor;


        public ProductDisAssemblyBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~ProductDisAssemblyBase()
        {
        }
    }
}