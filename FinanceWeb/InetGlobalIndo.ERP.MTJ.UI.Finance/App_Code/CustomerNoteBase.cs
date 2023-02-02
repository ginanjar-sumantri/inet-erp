using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerNote
{
    public class CustomerNoteBase : FinanceBase
    {
        protected short _menuID = 828;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "CustomerNote.aspx";
        protected string _addPage = "CustomerNoteAdd.aspx";
        protected string _editPage = "CustomerNoteEdit.aspx";
        protected string _detailPage = "CustomerNoteDetail.aspx";
        protected string _addDetailPage = "CustomerNoteDetailSJAdd.aspx";
        protected string _editDetailPage = "CustomerNoteDetailEdit.aspx";

        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeItem = "CodeItem";
        protected string _productKey = "ProductKey";

        protected string _pageTitleLiteral = "Customer Note";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CustomerNoteBase()
        {

        }

        ~CustomerNoteBase()
        {

        }
    }
}