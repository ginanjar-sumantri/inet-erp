using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.GenerateVoucher
{
    public abstract class GenerateVoucherBase : BillingBase
    {
        protected short _menuID = 1809;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClosem, _permTaxPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Generate Voucher";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _homePage = "GenerateVoucher.aspx";
        protected string _addPage = "GenerateVoucherAdd.aspx";
        protected string _viewPage = "GenerateVoucherView.aspx";

        public GenerateVoucherBase()
        {

        }

        ~GenerateVoucherBase()
        {

        }
    }
}