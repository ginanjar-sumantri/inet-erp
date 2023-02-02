using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FABeginning
{
    public abstract class FABeginningBase : AccountingBase
    {
        protected short _menuID = 1543;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FABeginning.aspx";
        protected string _addPage = "FABeginningAdd.aspx";
        protected string _editPage = "FABeginningEdit.aspx";
        protected string _detailPage = "FABeginningDetail.aspx";
        protected string _addDetailPage = "FABeginningDetailAdd.aspx";
        protected string _editDetailPage = "FABeginningDetailEdit.aspx";

        protected string _codeKey = "code";
        protected string _codeDt = "codedt";
        //protected string _codeFA = "FACode";

        protected string _pageTitleLiteral = "Fixed Asset Beginning";

        protected NameValueCollectionExtractor _nvcExtractor;

        public FABeginningBase()
        {
        }

        ~FABeginningBase()
        {
        }
    }
}