using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetYear
{
    public abstract class GLBudgetYearBase : AccountingBase
    {
        protected short _menuID = 502;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "BudgetYear.aspx";
        protected string _addPage = "BudgetYearAdd.aspx";
        protected string _editPage = "BudgetYearEdit.aspx";
        protected string _viewPage = "BudgetYearDetail.aspx";
        //protected string _detailAdd = "BudgetYearDetailAdd.aspx";
        //protected string _detailEdit = "BudgetYearDetailEdit.aspx";
        //protected string _detailView = "BudgetYearDetailView.aspx";

        protected string _codeKey = "Code";
        protected string _codeDt = "DetailCode";

        protected NameValueCollectionExtractor _nvcExtractor = null;

        protected string _pageTitleLiteral = "Budget Year";

        public GLBudgetYearBase()
        {

        }

        ~GLBudgetYearBase()
        {

        }
    }
}