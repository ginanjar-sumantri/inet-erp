using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.ShipmentType
{
    public abstract class ShipmentTypeBase : PurchasingBase
    {
        protected short _menuID = 671;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;

        protected string _homePage = "ShipmentType.aspx";
        protected string _addPage = "ShipmentTypeAdd.aspx";
        protected string _editPage = "ShipmentTypeEdit.aspx";
        protected string _viewPage = "ShipmentTypeView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "ShipmentCode";

        protected string _pageTitleLiteral = "Shipment Type";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ShipmentTypeBase()
        {

        }

        ~ShipmentTypeBase()
        {

        }
    }
}