using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSTrGraphicDataMapper
    {
        public static POSTrGraphicStatus GetStatus(Byte _prmStatus)
        {
            POSTrGraphicStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = POSTrGraphicStatus.SendToCashier;
                    break;
                case 1:
                    _result = POSTrGraphicStatus.Posted;
                    break;
                case 2:
                    _result = POSTrGraphicStatus.DeliveryOrder;
                    break;
                default:
                    _result = POSTrGraphicStatus.SendToCashier;
                    break;
            }

            return _result;
        }

        public static Byte GetStatus(POSTrGraphicStatus _prmStatus)
        {
            Byte _result = 0;

            switch (_prmStatus)
            {
                case POSTrGraphicStatus.SendToCashier:
                    _result = 0;
                    break;
                case POSTrGraphicStatus.Posted:
                    _result = 1;
                    break;
                case POSTrGraphicStatus.DeliveryOrder:
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

        public static string GetStatusText(POSTrGraphicStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSTrGraphicStatus.SendToCashier:
                    _result = "Send To Cashier";
                    break;
                case POSTrGraphicStatus.Posted:
                    _result = "Posted";
                    break;
                case POSTrGraphicStatus.DeliveryOrder:
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