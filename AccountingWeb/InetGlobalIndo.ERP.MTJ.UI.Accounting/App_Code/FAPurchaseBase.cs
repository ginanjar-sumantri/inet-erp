using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAPurchase
{
    public abstract class FAPurchaseBase : AccountingBase
    {
        protected short _menuID = 486;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FAPurchase.aspx";
        protected string _addPage = "FAPurchaseAdd.aspx";
        protected string _editPage = "FAPurchaseEdit.aspx";
        protected string _detailPage = "FAPurchaseDetail.aspx";
        protected string _addDetailPage = "FAPurchaseDetailAdd.aspx";
        protected string _editDetailPage = "FAPurchaseDetailEdit.aspx";
        protected string _viewDetailPage = "FAPurchaseDetailView.aspx";

        protected string _codeKey = "code";
        protected string _codeItem = "CodeItem";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Fixed Asset Purchase Receive";

        public FAPurchaseBase()
        {

        }

        ~FAPurchaseBase()
        {

        }
    }
}