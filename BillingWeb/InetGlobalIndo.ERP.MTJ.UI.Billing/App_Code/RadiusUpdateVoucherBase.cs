
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
namespace InetGlobalIndo.ERP.MTJ.UI.Billing.RadiusUpdateVoucher
{
    public class RadiusUpdateVoucherBase : BillingBase
    {
        protected short _menuID = 2120;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Radius Update Voucher";

        protected string _codeKey = "code";

        protected string _homePage = "RadiusUpdateVoucher.aspx";
        protected string _addPage = "RadiusUpdateVoucherAdd.aspx";
        protected string _editPage = "RadiusUpdateVoucherEdit.aspx";
        protected string _detailPage = "RadiusUpdateVoucherDetail.aspx";
        
        public RadiusUpdateVoucherBase()
        {
        }

        ~RadiusUpdateVoucherBase()
        {
        }
    }
}