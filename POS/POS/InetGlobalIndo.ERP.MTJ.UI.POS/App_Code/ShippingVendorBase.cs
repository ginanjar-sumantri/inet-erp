using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for VendorBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingVendor
{
    public abstract class ShippingVendorBase : POSBase
    {
        protected short _menuID = 2527;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";
        
        protected string _homePage = "ShippingVendor.aspx";
        protected string _addPage = "ShippingVendorAdd.aspx";
        protected string _editPage = "ShippingVendorEdit.aspx";
        protected string _editPricePage = "ShippingVendorEditPrice.aspx";
        protected string _editPriceZonePage = "ShippingVendorEditPriceZone.aspx";
        protected string _viewPage = "ShippingVendorView.aspx";
        protected string _pageTitleLiteral = "Shipping Vendor";

        protected string _addDetailPage = "ShippingVendorDtAdd.aspx";
        protected string _editDetailPage = "ShippingVendorDtEdit.aspx";
        protected string _viewDetailPage = "ShippingVendorDtView.aspx";
        protected string _pageTitleDetailLiteral = "Shipping Vendor Detail";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ShippingVendorBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
