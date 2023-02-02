using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAProcess
{
    public abstract class FAProcessBase : AccountingBase
    {
        protected short _menuID = 223;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FAProcess.aspx";
        protected string _addPage = "FAProcessAdd.aspx";
        protected string _editPage = "FAProcessEdit.aspx";
        protected string _detailPage = "FAProcessDetail.aspx";
        protected string _addDetailPage = "FAProcessDetailAdd.aspx";
        protected string _editDetailPage = "FAProcessDetailEdit.aspx";

        protected string _codeKey = "code";
        protected string _codePeriod = "period";
        protected string _codeFA = "FACode";

        protected string _pageTitleLiteral = "Fixed Asset Process";

        protected NameValueCollectionExtractor _nvcExtractor;

        public FAProcessBase()
        {

        }

        ~FAProcessBase()
        {

        }
    }
}