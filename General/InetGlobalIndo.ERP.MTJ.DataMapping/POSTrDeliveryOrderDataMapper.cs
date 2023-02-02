using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSTrDeliveryOrderDataMapper
    {
        public static POSDeliveryOrderStatus GetStatus(Byte _prmStatus)
        {
            POSDeliveryOrderStatus _result;

            switch (_prmStatus)
            {
                case 1:
                    _result = POSDeliveryOrderStatus.Open;
                    break;
                case 2:
                    _result = POSDeliveryOrderStatus.Process;
                    break;
                case 3:
                    _result = POSDeliveryOrderStatus.Done;
                    break;
                case 4:
                    _result = POSDeliveryOrderStatus.Delivering;
                    break;
                case 5:
                    _result = POSDeliveryOrderStatus.Deliver;
                    break;
                case 6:
                    _result = POSDeliveryOrderStatus.Close;
                    break;
                default:
                    _result = POSDeliveryOrderStatus.Open;
                    break;
            }

            return _result;
        }

        public static Byte GetStatus(POSDeliveryOrderStatus _prmStatus)
        {
            Byte _result = 0;

            switch (_prmStatus)
            {
                case POSDeliveryOrderStatus.Open:
                    _result = 1;
                    break;
                case POSDeliveryOrderStatus.Process:
                    _result = 2;
                    break;
                case POSDeliveryOrderStatus.Done:
                    _result = 3;
                    break;
                case POSDeliveryOrderStatus.Delivering:
                    _result = 4;
                    break;
                case POSDeliveryOrderStatus.Deliver:
                    _result = 5;
                    break;
                case POSDeliveryOrderStatus.Close:
                    _result = 6;
                    break;
                case POSDeliveryOrderStatus.Cancel:
                    _result = 7;
                    break;
                case POSDeliveryOrderStatus.Paid:
                    _result = 8;
                    break;
                default:
                    _result = 1;
                    break;
            }
            return _result;
        }
        
        public static string GetStatusText(Byte _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case 1:
                    _result = "Open";
                    break;
                case 2:
                    _result = "Process";
                    break;
                case 3:
                    _result = "Done";
                    break;
                case 4:
                    _result = "Delivering";
                    break;
                case 5:
                    _result = "Deliver";
                    break;
                case 6:
                    _result = "Close";
                    break;
                case 7:
                    _result = "Cancel";
                    break;
                case 8:
                    _result = "Paid";
                    break;
                default:
                    _result = "Open";
                    break;
            }
            return _result;
        }

        public static string GetStatusText(POSDeliveryOrderStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSDeliveryOrderStatus.Open:
                    _result = "Open";
                    break;
                case POSDeliveryOrderStatus.Process:
                    _result = "Process";
                    break;
                case POSDeliveryOrderStatus.Done:
                    _result = "Done";
                    break;
                case POSDeliveryOrderStatus.Delivering:
                    _result = "Delivering";
                    break;
                case POSDeliveryOrderStatus.Deliver:
                    _result = "Deliver";
                    break;
                case POSDeliveryOrderStatus.Close:
                    _result = "Close";
                    break;
                case POSDeliveryOrderStatus.Cancel:
                    _result = "Cancel";
                    break;
                case POSDeliveryOrderStatus.Paid:
                    _result = "Paid";
                    break;
                default:
                    _result = "Open";
                    break;
            }

            return _result;
        }

        public static List<String> _DOType = new List<String>()
        {
            //id, text
            "1,Open",
            "2,Process",
            "3,Done",
            "4,Delivering",
            "5,Deliver",
            "6,Close",
            "7,Cancel",
            "8,Paid"
        };
        public static List<String> DOTypes
        {
            get { return _DOType; }
        }
    }
}