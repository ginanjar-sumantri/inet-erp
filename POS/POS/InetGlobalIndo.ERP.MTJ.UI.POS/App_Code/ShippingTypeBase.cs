using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for ShippingTypeBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingType
{
    public abstract class ShippingTypeBase : POSBase
    {
        protected short _menuID = 2520;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "ShippingType.aspx";
        protected string _addPage = "ShippingTypeAdd.aspx";
        protected string _editPage = "ShippingTypeEdit.aspx";
        protected string _viewPage = "ShippingTypeView.aspx";
        protected string _pageTitleLiteral = "Shipping Type";

        protected string _addDetailPage = "ShippingTypeDtAdd.aspx";
        protected string _editDetailPage = "ShippingTypeDtEdit.aspx";
        protected string _viewDetailPage = "ShippingTypeDtView.aspx";
        protected string _pageTitleDetailLiteral = "Shipping Type Detail";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ShippingTypeBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
