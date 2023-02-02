using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Discount
{
    public abstract class DiscountBase : StockControlBase
    {
        protected short _menuID = 1731;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "Discount.aspx";
        protected string _addPage = "DiscountAdd.aspx";
        protected string _editPage = "DiscountEdit.aspx";
        protected string _viewPage = "DiscountView.aspx";
        protected string _editDetailPage = "DiscountDetailEdit.aspx";
        protected string _addDetailPage = "DiscountDetailAdd.aspx";

        protected string _codeKey = "code";
        protected string _levelKey = "levelCode";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Discount";

        public DiscountBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~DiscountBase()
        {
        }
    }
}