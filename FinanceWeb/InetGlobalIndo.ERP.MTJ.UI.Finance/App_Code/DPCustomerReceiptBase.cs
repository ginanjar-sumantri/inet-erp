using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerReceipt
{
    public class DPCustomerReceiptBase : FinanceBase
    {
        protected short _menuID = 450;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "DPCustReceipt.aspx";
        protected string _addPage = "DPCustReceiptAdd.aspx";
        protected string _editPage = "DPCustReceiptEdit.aspx";
        protected string _detailPage = "DPCustReceiptDetail.aspx";
        protected string _viewDetailPage = "DPCustReceiptDetailView.aspx";
        protected string _editDetailPage = "DPCustReceiptDetailEdit.aspx";
        protected string _addDetailPage = "DPCustReceiptDetailAdd.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _itemKey = "ItemNo";

        protected string _pageTitleLiteral = "DP Customer Receipt";

        protected NameValueCollectionExtractor _nvcExtractor;

        public DPCustomerReceiptBase()
        {

        }
    }
}