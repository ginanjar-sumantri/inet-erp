using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTypeFile
{
    public abstract class StockTypeFileBase : StockControlBase
    {
        protected short _menuID = 546;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClosem, _permTaxPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "StockTypeFile.aspx";
        protected string _addPage = "StockTypeFileAdd.aspx";
        protected string _editPage = "StockTypeFileEdit.aspx";
        protected string _viewPage = "StockTypeFileView.aspx";
        protected string _addDtPage = "StockTypeFileDetailAdd.aspx";

        protected string _codeKey = "StockTypeCode";

        protected string _pageTitleLiteral = "Stock Type File";

        protected NameValueCollectionExtractor _nvcExtractor;
        
        public StockTypeFileBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);

        }

        ~StockTypeFileBase()
        {
        }
    }
}