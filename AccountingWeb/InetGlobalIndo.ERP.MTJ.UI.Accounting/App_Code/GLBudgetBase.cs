using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetEntry
{
    public abstract class GLBudgetBase : AccountingBase
    {
        protected short _menuID = 502;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "GLBudgetHd.aspx";
        protected string _addPage = "GLBudgetAdd.aspx";
        protected string _editPage = "GLBudgetEdit.aspx";
        protected string _viewPage = "GLBudgetDetail.aspx";
        protected string _detailAdd = "GLBudgetDetailAdd.aspx";
        protected string _detailEdit = "GLBudgetDetailEdit.aspx";
        protected string _detailView = "GLBudgetDetailView.aspx";

        protected string _codeKey = "Code";
        protected string _codeDt = "DetailCode";

        protected NameValueCollectionExtractor _nvcExtractor = null;

        protected string _pageTitleLiteral = "Budget Entry";

        public GLBudgetBase()
        {

        }

        ~GLBudgetBase()
        {

        }
    }
}