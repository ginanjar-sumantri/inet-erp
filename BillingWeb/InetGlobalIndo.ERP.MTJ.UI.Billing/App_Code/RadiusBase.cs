using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Radius
{
    public abstract class RadiusBase : BillingBase
    {
        protected short _menuID = 2116;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Radius";

        protected string _codeKey = "code";

        protected string _homePage = "Radius.aspx";
        protected string _addPage = "RadiusAdd.aspx";
        protected string _editPage = "RadiusEdit.aspx";
        protected string _viewPage = "RadiusView.aspx";

        public RadiusBase()
        {
        }

        ~RadiusBase()
        {
        }
    }
}
