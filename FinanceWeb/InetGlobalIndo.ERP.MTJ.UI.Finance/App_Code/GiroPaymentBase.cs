using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroPayment
{
    public abstract class GiroPaymentBase : FinanceBase
    {
        protected short _menuID = 403;
        protected PermissionLevel _permAccess, _permView, _permEdit;

        protected string _homePage = "GiroPayment.aspx";
        protected string _addPage = "GiroPaymentAdd.aspx";
        protected string _editPage = "GiroPaymentEdit.aspx";
        protected string _viewPage = "GiroPaymentView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Giro Payment Status";

        protected NameValueCollectionExtractor _nvcExtractor;

        public GiroPaymentBase()
        {

        }

        ~GiroPaymentBase()
        {

        }
    }
}