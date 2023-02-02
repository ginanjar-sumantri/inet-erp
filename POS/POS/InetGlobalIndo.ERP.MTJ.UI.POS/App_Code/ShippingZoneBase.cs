using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for ShippingZoneBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingZone
{
    public abstract class ShippingZoneBase : POSBase
    {
        protected short _menuID = 2520;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "ShippingZone.aspx";
        protected string _addPage = "ShippingZoneAdd.aspx";
        protected string _editPage = "ShippingZoneEdit.aspx";
        protected string _viewPage = "ShippingZoneView.aspx";
        protected string _pageTitleLiteral = "Shipping Zone";

        protected string _addCountryDetailPage = "ShippingZoneCountryAdd.aspx";
        protected string _editCountryDetailPage = "ShippingZoneCountryEdit.aspx";
        protected string _viewCountryDetailPage = "ShippingZoneCountryView.aspx";
        protected string _pageTitleCountryDetailLiteral = "Shipping Zone Country Detail";

        protected string _addPriceDetailPage = "ShippingZonePriceAdd.aspx";
        protected string _editPriceDetailPage = "ShippingZonePriceEdit.aspx";
        protected string _viewPriceDetailPage = "ShippingZonePriceView.aspx";
        protected string _pageTitlePriceDetailLiteral = "Shipping Zone Price Detail";

        protected string _addMultipleDetailPage = "ShippingZoneMultipleAdd.aspx";
        protected string _editMultipleDetailPage = "ShippingZoneMultipleEdit.aspx";
        protected string _viewMultipleDetailPage = "ShippingZoneMultipleView.aspx";
        protected string _pageTitleMultipleDetailLiteral = "Shipping Zone Multiple Detail";
        
        protected NameValueCollectionExtractor _nvcExtractor;

        public ShippingZoneBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
