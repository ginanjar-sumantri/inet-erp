using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.DirectBillOfLading
{
    public abstract class DirectBillOfLadingBase : StockControlBase
    {
        protected short _menuID = 2108;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "DirectBillOfLading.aspx";
        protected string _addPage = "DirectBillOFLadingAdd.aspx";
        protected string _editPage = "DirectBillOFLadingEdit.aspx";
        protected string _viewPage = "DirectBillOFLadingDetail.aspx";
        protected string _addDetailPage = "DirectBillOfLadingDetailAdd.aspx";
        protected string _editDetailPage = "DirectBillOfLadingDetailEdit.aspx";
        protected string _viewDetailPage = "DirectBillOfLadingDetailView.aspx";

        protected string _codeKey = "TransNmbr";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";
        protected string _SOKey = "SONo";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Direct Bill Of Lading";

        public DirectBillOfLadingBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~DirectBillOfLadingBase()
        {
        }
    }
}