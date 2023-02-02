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
namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingCOD
{
    public abstract class ShippingCODBase : POSBase
    {
        protected short _menuID = 2563;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "ShippingCOD.aspx";
        //protected string _addPage = "ShippingCODAdd.aspx";
        //protected string _editPage = "ShippingCODEdit.aspx";
        protected string _viewPage = "ShippingCODView.aspx";
        protected string _pageTitleLiteral = "Shipping Cash On Delivery";

        //protected string _addDetailPage = "ShippingCODDtAdd.aspx";
        //protected string _editDetailPage = "ShippingCODDtEdit.aspx";
        protected string _viewDetailPage = "ShippingCODDtView.aspx";
        protected string _pageTitleDetailLiteral = "Shipping Cash On Delivery Detail";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ShippingCODBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
