using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.CogsProcess
{
    public abstract class CogsProcessBase : StockControlBase
    {
        protected short _menuID = 560;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "CogsProcess.aspx";
        protected string _addPage = "CogsProcessAdd.aspx";
        protected string _editPage = "CogsProcessEdit.aspx";
        protected string _detailPage = "CogsProcessDetail.aspx";


        protected string _codeKey = "code";
        protected string _warning = "warning";

        protected string _pageTitleLiteral = "COGS Process";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CogsProcessBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~CogsProcessBase()
        {
        }
    }
}