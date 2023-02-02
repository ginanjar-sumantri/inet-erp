using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierNote
{
    public class SupplierNoteBase : FinanceBase
    {
        protected short _menuID = 835;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "SupplierNote.aspx";
        protected string _addPage = "SupplierNoteAdd.aspx";
        protected string _editPage = "SupplierNoteEdit.aspx";
        protected string _detailPage = "SupplierNoteDetail.aspx";
        protected string _addDetailPage = "SupplierNoteDetailAdd.aspx";
        protected string _editDetailPage = "SupplierNoteDetailEdit.aspx";
        protected string _viewDetailPage = "SupplierNoteDetailView.aspx";
        //protected string _addDetailPage2 = "SupplierNoteDetail2Add.aspx";
        protected string _addDetailPage2 = "SupplierNoteDetailRRAdd.aspx";        
        protected string _editDetailPage2 = "SupplierNoteDetail2Edit.aspx";
        protected string _viewDetailPage2 = "SupplierNoteDetail2View.aspx";

        protected string _codeKey = "code";
        protected string _rrKey = "RRCode";
        protected string _productKey = "ProductCode";
        protected string _wrhsKey = "WrhsCode";
        protected string _subledKey = "WrhsSubledCode";
        protected string _locationKey = "LocCode";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Supplier Note";

        public SupplierNoteBase()
        {

        }

        ~SupplierNoteBase()
        {

        }
    }
}