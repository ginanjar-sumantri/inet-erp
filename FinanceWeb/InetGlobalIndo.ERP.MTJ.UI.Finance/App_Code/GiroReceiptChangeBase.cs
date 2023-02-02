using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroReceiptChange
{
    public class GiroReceiptChangeBase : FinanceBase
    {
        protected short _menuID = 404;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "GiroReceiptChange.aspx";
        protected string _detailPage = "GiroReceiptChangeDetail.aspx";
        protected string _editPage = "GiroReceiptChangeEdit.aspx";
        protected string _addPage = "GiroReceiptChangeAdd.aspx";
        protected string _viewDetailPage = "GiroReceiptChangeDetailView.aspx";
        protected string _editDetailPage = "GiroReceiptChangeDetailEdit.aspx";
        protected string _addDetailPage = "GiroReceiptChangeDetailAdd.aspx";
        protected string _viewPayPage = "GiroReceiptChangePayView.aspx";
        protected string _editPayPage = "GiroReceiptChangePayEdit.aspx";
        protected string _addPayPage = "GiroReceiptChangePayAdd.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _pageTitleLiteral = "Giro Receipt Change";

        protected string _codeKey = "code";
        protected string _giroKey = "GiroNo";
        protected string _itemKey = "ItemNo";

        protected NameValueCollectionExtractor _nvcExtractor;

        public GiroReceiptChangeBase()
        {

        }
    }
}