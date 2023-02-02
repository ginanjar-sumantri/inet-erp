using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.DeliveryType
{
    public abstract class DeliveryTypeBase : PurchasingBase
    {
        protected short _menuID = 667;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;

        protected string _homePage = "DeliveryType.aspx";
        protected string _addPage = "DeliveryTypeAdd.aspx";
        protected string _editPage = "DeliveryTypeEdit.aspx";
        protected string _viewPage = "DeliveryTypeView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Delivery Site";

        public DeliveryTypeBase()
        {

        }

        ~DeliveryTypeBase()
        {

        }
    }
}