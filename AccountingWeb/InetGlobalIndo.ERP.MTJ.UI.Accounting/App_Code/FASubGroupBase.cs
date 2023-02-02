using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FASubGroup
{
    public abstract class FASubGroupBase : AccountingBase
    {
        protected short _menuID = 55;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FAGroupSub.aspx";
        protected string _addPage = "FAGroupSubAdd.aspx";
        protected string _editPage = "FAGroupSubEdit.aspx";
        protected string _viewPage = "FAGroupSubView.aspx";
        protected string _addDetailPage = "FAGroupSubAccAdd.aspx";
        protected string _editDetailPage = "FAGroupSubAccEdit.aspx";
        protected string _viewDetailPage = "FAGroupSubAccView.aspx";

        protected string _currCodeKey = "currcode";
        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Fixed Asset Sub Group";

        public FASubGroupBase()
        {

        }

        ~FASubGroupBase()
        {

        }
    }
}
