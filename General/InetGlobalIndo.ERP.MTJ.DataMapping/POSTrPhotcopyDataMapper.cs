using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSTrPhotocopyDataMapper
    {
        public static POSTrPhotocopyStatus GetStatus(Byte _prmStatus)
        {
            POSTrPhotocopyStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = POSTrPhotocopyStatus.SendToCashier;
                    break;
                case 1:
                    _result = POSTrPhotocopyStatus.Posted;
                    break;
                case 2:
                    _result = POSTrPhotocopyStatus.DeliveryOrder;
                    break;
                default:
                    _result = POSTrPhotocopyStatus.SendToCashier;
                    break;
            }

            return _result;
        }

        public static Byte GetStatus(POSTrPhotocopyStatus _prmStatus)
        {
            Byte _result = 0;

            switch (_prmStatus)
            {
                case POSTrPhotocopyStatus.SendToCashier:
                    _result = 0;
                    break;
                case POSTrPhotocopyStatus.Posted:
                    _result = 1;
                    break;
                case POSTrPhotocopyStatus.DeliveryOrder:
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

        public static string GetStatusText(POSTrPhotocopyStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSTrPhotocopyStatus.SendToCashier:
                    _result = "Send To Cashier";
                    break;
                case POSTrPhotocopyStatus.Posted:
                    _result = "Posted";
                    break;
                case POSTrPhotocopyStatus.DeliveryOrder:
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