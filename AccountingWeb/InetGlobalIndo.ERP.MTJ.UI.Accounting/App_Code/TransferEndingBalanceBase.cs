using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.TransferEndingBalance
{
    public abstract class TransferEndingBalanceBase:AccountingBase 
    {
        protected short _menuID = 222;
        protected PermissionLevel _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _pageTitleLiteral = "Transfer Ending Balance";

        public TransferEndingBalanceBase()
        {

        }

        ~TransferEndingBalanceBase()
        {

        }
    }
}