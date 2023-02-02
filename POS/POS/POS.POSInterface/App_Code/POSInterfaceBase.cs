using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace POS.POSInterface
{
    public class POSInterfaceBase : System.Web.UI.Page
    {
        private string _pageTitle = ApplicationConfig.MembershipAppName + " :: POS Interface";

        protected string _errorPermissionPage = ApplicationConfig.POSWebAppURL + "ErrorPermission.aspx";
        
        protected short _menuIDCashier = 2424;
        //protected short _menuIDMember = 2414;
        protected short _menuIDDeliveryOrder = 2414;        
        protected short _menuIDPrinting = 2416;
        protected short _menuIDPhotocopy = 2417;
        protected short _menuIDShipping = 2418;
        protected short _menuIDInternet = 2419;
        protected short _menuIDStationary = 2420;
        protected short _menuIDGraphicDesign = 2421;
        protected short _menuIDEVoucher = 2422;
        protected short _menuIDCafe = 2423;
        protected short _menuIDSendToCashier = 2424;
        protected short _menuIDMonitoring = 2425;
        protected short _menuIDTicketing = 2415;
        protected short _menuIDHotel = 2490;
        protected DateTime _defaultdate;

        protected PermissionLevel _permAccessMember, _permAccessPrinting, _permAccessPhotocopy, 
            _permAccessShipping, _permAccessInternet, _permAccessStationary, _permAccessGraphicDesign,
            _permAccessEVoucher, _permAccessCafe, _permAccessSendToCashier, _permAccessMonitoring,
            _permAccessTicketing, _permAccessHotel, _permDeliveryOrder, _permAccess;


        public POSInterfaceBase()
        {
            this._defaultdate = Convert.ToDateTime("1/1/1900 00:00:00 PM");
        }
    }
}