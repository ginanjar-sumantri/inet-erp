using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.BankRecon
{
    public abstract class BankReconBase : FinanceBase
    {
        protected short _menuID = 1505;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected String _homePage = "BankRecon.aspx";
        protected String _addPage = "BankReconAdd.aspx";
        protected String _editPage = "BankReconEdit.aspx";
        protected String _viewPage = "BankReconDetail.aspx";
        protected String _detailPage = "BankReconDetail.aspx";
        protected String _addDetailPage = "BankReconDetailAdd.aspx";
        protected String _viewDetailPage = "BankReconDetailView.aspx";
        protected String _editDetailPage = "BankReconDetailEdit.aspx";

        protected String _codeKey = "code";
        protected String _codeItem = "account";

        protected string _pageTitleLiteral = "Bank Reconciliation";

        protected NameValueCollectionExtractor _nvcExtractor;

        public BankReconBase()
        {
        }

        ~BankReconBase()
        {
        }
    }
}