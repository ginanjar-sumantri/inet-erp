using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroReceipt
{
    public abstract class GiroReceiptBase : FinanceBase
    {
        protected short _menuID = 401;
        protected PermissionLevel _permAccess, _permView, _permEdit;

        protected string _homePage = "GiroReceipt.aspx";
        protected string _viewPage = "GiroReceiptView.aspx";
        protected string _depositedAllViewPage = "GiroDepositedAllView.aspx";
        protected string _depositedAllPage = "GiroDepositedAll.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;
        
        protected string _pageTitleLiteral = "Giro Receipt Status";
       
        public GiroReceiptBase()
        {

        }

        ~GiroReceiptBase()
        {

        }
    }
}