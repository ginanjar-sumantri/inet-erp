using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;


namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.CashflowGroupSub
{
    public abstract class CashflowGroupSubBase : AccountingBase
    {
        protected short _menuID = 2441;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        //protected string _homePage = "CashflowGroupSubDetail.aspx";
        protected string _addPage = "CashflowGroupSubDetailAdd.aspx";
        protected string _transHomePage = "CashflowGroupSub.aspx";
        protected string _transAddPage = "CashflowGroupSubAdd.aspx";
        protected string _transEditPage = "CashflowGroupSubEdit.aspx";
        protected string _transViewPage = "CashflowGroupSubDetail.aspx";

        protected string _codeKey = "code";
        protected string _codeKey2 = "code2";
        protected string _codeKey3 = "code3";

        protected string _authorPageTitleLiteral = "Cashflow Group Sub Account";
        protected string _transPageTitleLiteral = "Cashflow Group Sub";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CashflowGroupSubBase()
        {

        }

        ~CashflowGroupSubBase()
        {

        }
    }
}