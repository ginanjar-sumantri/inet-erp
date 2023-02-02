using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ReceivingPO
{
    public abstract class ReceivingPOBase : StockControlBase
    {
        protected short _menuID = 821;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "ReceivingPO.aspx";
        protected string _addPage = "ReceivingPOAdd.aspx";
        protected string _editPage = "ReceivingPOEdit.aspx";
        protected string _detailPage = "ReceivingPODetail.aspx";
        protected string _addDetailPage = "ReceivingPODetailAdd.aspx";
        protected string _editDetailPage = "ReceivingPODetailEdit.aspx";
        protected string _viewDetailPage = "ReceivingPODetailView.aspx";
        protected string _importPage = "NCPImport.aspx";
        protected string _addDetailPage2 = "ReceivingPODetailAdd2.aspx";
        protected string _editDetailPage2 = "ReceivingPODetailEdit2.aspx";
        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";
        protected string _detailKey = "DetailCode";
        protected string _codeKey2 = "code2";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Receiving PO";

        public ReceivingPOBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~ReceivingPOBase()
        {
        }
    }
}