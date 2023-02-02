using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSTrShippingDataMapper
    {
        public static POSTrShippingStatus GetStatus(Byte _prmStatus)
        {
            POSTrShippingStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = POSTrShippingStatus.SendToCashier;
                    break;
                case 1:
                    _result = POSTrShippingStatus.Posted;
                    break;
                case 2:
                    _result = POSTrShippingStatus.DeliveryOrder;
                    break;
                default:
                    _result = POSTrShippingStatus.SendToCashier;
                    break;
            }

            return _result;
        }

        public static Byte GetStatus(POSTrShippingStatus _prmStatus)
        {
            Byte _result = 0;

            switch (_prmStatus)
            {
                case POSTrShippingStatus.SendToCashier:
                    _result = 0;
                    break;
                case POSTrShippingStatus.Posted:
                    _result = 1;
                    break;
                case POSTrShippingStatus.DeliveryOrder:
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

        public static string GetStatusText(POSTrShippingStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSTrShippingStatus.SendToCashier:
                    _result = "Send To Cashier";
                    break;
                case POSTrShippingStatus.Posted:
                    _result = "Posted";
                    break;
                case POSTrShippingStatus.DeliveryOrder:
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