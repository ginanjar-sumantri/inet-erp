using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductSubGroup
{
    public abstract class ProductSubGroupBase : StockControlBase
    {
        protected short _menuID = 430;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "ProductSubGroup.aspx";
        protected string _addPage = "ProductSubGroupAdd.aspx";
        protected string _editPage = "ProductSubGroupEdit.aspx";
        protected string _viewPage = "ProductSubGroupView.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Product Sub Group";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ProductSubGroupBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~ProductSubGroupBase()
        {
        }
    }
}