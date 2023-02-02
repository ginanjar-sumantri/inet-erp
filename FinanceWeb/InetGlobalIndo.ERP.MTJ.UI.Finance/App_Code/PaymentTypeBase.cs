using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentType
{
    public abstract class PaymentTypeBase : FinanceBase
    {
        protected short _menuID = 51;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "PaymentType.aspx";
        protected string _addPage = "PaymentTypeAdd.aspx";
        protected string _editPage = "PaymentTypeEdit.aspx";
        protected string _viewPage = "PaymentTypeView.aspx";

        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Payment Type"; 

        public PaymentTypeBase()
        {

        }

        ~PaymentTypeBase()
        {

        }
    }
}