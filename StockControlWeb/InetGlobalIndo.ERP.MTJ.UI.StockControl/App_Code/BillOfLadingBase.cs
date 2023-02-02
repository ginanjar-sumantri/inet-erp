using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.BillOfLading
{
    public abstract class BillOfLadingBase : StockControlBase
    {
        protected short _menuID = 704;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "BillOFLading.aspx";
        protected string _addPage = "BillOFLadingAdd.aspx";
        protected string _addDetailRefPage = "BillOfLadingDetailAddRef.aspx";
        protected string _editPage = "BillOFLadingEdit.aspx";
        protected string _viewPage = "BillOFLadingDetail.aspx";
        protected string _addDetailPage = "BillOfLadingDetailAdd.aspx";
        protected string _editDetailPage = "BillOfLadingDetailEdit.aspx";
        protected string _viewDetailPage = "BillOfLadingDetailView.aspx";

        protected string _addPageSo = "BillOfLadingSOAdd.aspx";
        protected string _editPageSo = "BillOFLadingSOEdit.aspx";
        protected string _viewPageSo = "BillOFLadingSODetail.aspx";
        protected string _addDetailSoPage = "BillOfLadingDetailSOAdd.aspx";
        protected string _editDetailSoPage = "BillOfLadingDetailSOEdit.aspx";
        //protected string _viewDetailSoPage = "BillOfLadingDetailView.aspx";

        protected string _codeKey = "TransNmbr";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";
        protected string _DOKey = "DONo";
        protected string _wrhsCode = "WrhsCode";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Delivery Order";//"Bill Of Lading";

        public BillOfLadingBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~BillOfLadingBase()
        {
        }
    }
}