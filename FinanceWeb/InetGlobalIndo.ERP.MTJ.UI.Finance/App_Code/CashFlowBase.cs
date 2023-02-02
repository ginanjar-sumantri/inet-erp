using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CashFlowFile
{
    public abstract class CashFlowBase:FinanceBase
    {
        protected short _menuID = 1195;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "CashFlowFile.aspx";
        protected string _addPage = "CashFlowFileAdd.aspx";
       
        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Cash Flow File";
      
        protected NameValueCollectionExtractor _nvcExtractor;

        public CashFlowBase()
        { 
        
        }

        ~CashFlowBase()
        { 
        
        }
    }
}