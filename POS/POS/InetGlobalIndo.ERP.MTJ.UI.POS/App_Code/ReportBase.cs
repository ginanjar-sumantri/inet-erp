using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for MemberTypeBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.Report
{
    public abstract class ReportBase : POSBase
    {
        protected short _menuIDPOSPerTransaction = 2452;
        protected short _menuIDPOSPerProduct = 2454; 
       

        protected PermissionLevel _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        public ReportBase()
        {
        }

        ~ReportBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
