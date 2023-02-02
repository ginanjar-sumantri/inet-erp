using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSTrCafeDataMapper
    {
        //POSTrCafeHd
        public static POSTrCafeStatus GetStatus(Byte _prmStatus)
        {
            POSTrCafeStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = POSTrCafeStatus.SendToCashier;
                    break;
                case 1:
                    _result = POSTrCafeStatus.Posted;
                    break;
                case 2:
                    _result = POSTrCafeStatus.DeliveryOrder;
                    break;
                default:
                    _result = POSTrCafeStatus.SendToCashier;
                    break;
            }

            return _result;
        }

        public static Byte GetStatus(POSTrCafeStatus _prmStatus)
        {
            Byte _result = 0;

            switch (_prmStatus)
            {
                case POSTrCafeStatus.SendToCashier:
                    _result = 0;
                    break;
                case POSTrCafeStatus.Posted:
                    _result = 1;
                    break;
                case POSTrCafeStatus.DeliveryOrder:
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

        public static string GetStatusText(POSTrCafeStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSTrCafeStatus.SendToCashier:
                    _result = "Send To Cashier";
                    break;
                case POSTrCafeStatus.Posted:
                    _result = "Posted";
                    break;
                case POSTrCafeStatus.DeliveryOrder:
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