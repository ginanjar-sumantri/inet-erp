using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAService
{
    public class FAServiceBase : AccountingBase
    {
        protected short _menuID = 1022;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FAService.aspx";
        protected string _addPage = "FAServiceAdd.aspx";
        protected string _editPage = "FAServiceEdit.aspx";
        protected string _detailPage = "FAServiceDetail.aspx";
        protected string _addDetailPage = "FAServiceDetailAdd.aspx";
        protected string _editDetailPage = "FAServiceDetailEdit.aspx";
        protected string _viewDetailPage = "FAServiceDetailView.aspx";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Fixed Asset Service";

        public FAServiceBase()
        {

        }

        ~FAServiceBase()
        {

        }
    }
}
