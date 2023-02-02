using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Retail
{
    public abstract class RetailBase : SalesBase
    {
        protected short _menuID = 690;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose, _permRevisi;
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "Retail.aspx";
        protected string _addPage = "RetailAdd.aspx";
        protected string _editPage = "RetailEdit.aspx";
        protected string _detailPage = "RetailDetail.aspx";
        protected string _addDetailPage = "RetailDetailAdd.aspx";
        protected string _editDetailPage = "RetailDetailEdit.aspx";
        protected string _viewDetailPage = "RetailDetailView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.SalesWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeDetail = "DetailCode";

        protected string _pageTitleLiteral = "Retail";

        protected NameValueCollectionExtractor _nvcExtractor;

        public RetailBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~RetailBase()
        {

        }
    }
}