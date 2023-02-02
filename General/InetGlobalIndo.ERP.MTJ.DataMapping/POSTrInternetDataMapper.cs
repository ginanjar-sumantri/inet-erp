using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSTrInternetDataMapper
    {
        public static POSMsInternetTableStatus GetStatusTable(Byte _prmStatus)
        {
            POSMsInternetTableStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = POSMsInternetTableStatus.Available;
                    break;
                case 1:
                    _result = POSMsInternetTableStatus.Booking;
                    break;
                case 2:
                    _result = POSMsInternetTableStatus.NotAvailable;
                    break;
                default:
                    _result = POSMsInternetTableStatus.NotAvailable;
                    break;
            }

            return _result;
        }

        public static Byte GetStatusTable(POSMsInternetTableStatus _prmStatus)
        {
            Byte _result = 0;

            switch (_prmStatus)
            {
                case POSMsInternetTableStatus.Available:
                    _result = 0;
                    break;
                case POSMsInternetTableStatus.Booking:
                    _result = 1;
                    break;
                case POSMsInternetTableStatus.NotAvailable:
                    _result = 2;
                    break;
                default:
                    _result = 2;
                    break;
            }

            return _result;
        }
        
        public static string GetStatusTableText(int _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case 0:
                    _result = "Available";
                    break;
                case 1:
                    _result = "Booking";
                    break;
                case 2:
                    _result = "Not Available";
                    break;
                default:
                    _result = "Not Available";
                    break;
            }

            return _result;
        }

        public static string GetStatusTableText(POSMsInternetTableStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSMsInternetTableStatus.Available:
                    _result = "Available";
                    break;
                case POSMsInternetTableStatus.Booking:
                    _result = "Booking";
                    break;
                case POSMsInternetTableStatus.NotAvailable:
                    _result = "NotAvailable";
                    break;
                default:
                    _result = "NotAvailable";
                    break;
            }

            return _result;
        }

        //POSInternetHd
        public static POSTrInternetStatus GetStatus(Byte _prmStatus)
        {
            POSTrInternetStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = POSTrInternetStatus.SendToCashier;
                    break;
                case 1:
                    _result = POSTrInternetStatus.Posted;
                    break;
                case 2:
                    _result = POSTrInternetStatus.DeliveryOrder;
                    break;
                default:
                    _result = POSTrInternetStatus.SendToCashier;
                    break;
            }

            return _result;
        }

        public static Byte GetStatus(POSTrInternetStatus _prmStatus)
        {
            Byte _result = 0;

            switch (_prmStatus)
            {
                case POSTrInternetStatus.SendToCashier:
                    _result = 0;
                    break;
                case POSTrInternetStatus.Posted:
                    _result = 1;
                    break;
                case POSTrInternetStatus.DeliveryOrder:
                    _result = 2;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static string GetStatusText(int _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case 0:
                    _result = "Send To Cashier";
                    break;
                case 1:
                    _result = "Posted";
                    break;
                case 2:
                    _result = "Delivery Order";
                    break;
                default:
                    _result = "Send To Cashier";
                    break;
            }

            return _result;
        }

        public static string GetStatusText(POSTrInternetStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSTrInternetStatus.SendToCashier:
                    _result = "Send To Cashier";
                    break;
                case POSTrInternetStatus.Posted:
                    _result = "Posted";
                    break;
                case POSTrInternetStatus.DeliveryOrder:
                    _result = "Delivery Order";
                    break;
                default:
                    _result = "Send To Cashier";
                    break;
            }

            return _result;
        }
    }
}