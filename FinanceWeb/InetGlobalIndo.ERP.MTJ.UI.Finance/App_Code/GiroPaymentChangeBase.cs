using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroPaymentChange
{
    public abstract class GiroPaymentChangeBase : FinanceBase
    {
        protected short _menuID = 445;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "GiroPaymentChange.aspx";
        protected string _detailPage = "GiroPaymentChangeDetail.aspx";
        protected string _editPage = "GiroPaymentChangeEdit.aspx";
        protected string _addPage = "GiroPaymentChangeAdd.aspx";
        protected string _addDetailPage = "GiroPaymentChangeDetailAdd.aspx";
        protected string _viewDetailPage = "GiroPaymentChangeDetailView.aspx";
        protected string _addDetailPage2 = "GiroPaymentChangePaymentAdd.aspx";
        protected string _viewDetailPage2 = "GiroPaymentChangePaymentView.aspx";
        protected string _editDetailPage2 = "GiroPaymentChangePaymentEdit.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeItem = "CodeItem";

        protected string _pageTitleLiteral = "Giro Payment Change";

        protected NameValueCollectionExtractor _nvcExtractor;



        public GiroPaymentChangeBase()
        {

        }

        ~GiroPaymentChangeBase()
        {

        }
    }
}