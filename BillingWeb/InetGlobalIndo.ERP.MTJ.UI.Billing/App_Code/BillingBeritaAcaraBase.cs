using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.BillingBeritaAcara
{
    public class BillingBeritaAcaraBase : BillingBase
    {
        protected short _menuID = 1816;

        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClosem, _permTaxPreview;
        protected string _homePage = "BeritaAcara.aspx";
        protected string _addPage = "BeritaAcaraAdd.aspx";
        protected string _editPage = "BeritaAcaraEdit.aspx";
        protected string _viewPage = "BeritaAcaraView.aspx";

        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Berita Acara";

        protected NameValueCollectionExtractor _nvcExtractor;

        public BillingBeritaAcaraBase()
        {
        }

        ~BillingBeritaAcaraBase()
        {
        }
    }
}