using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSTrPrintingDataMapper
    {
        public static POSTrPrintingStatus GetStatus(Byte _prmStatus)
        {
            POSTrPrintingStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = POSTrPrintingStatus.SendToCashier;
                    break;
                case 1:
                    _result = POSTrPrintingStatus.Posted;
                    break;
                default:
                    _result = POSTrPrintingStatus.SendToCashier;
                    break;
            }

            return _result;
        }

        public static Byte GetStatus(POSTrPrintingStatus _prmStatus)
        {
            Byte _result = 0;

            switch (_prmStatus)
            {
                case POSTrPrintingStatus.SendToCashier:
                    _result = 0;
                    break;
                case POSTrPrintingStatus.Posted:
                    _result = 1;
                    break;
                case POSTrPrintingStatus.DeliveryOrder:
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

        public static string GetStatusText(POSTrPrintingStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSTrPrintingStatus.SendToCashier:
                    _result = "Send To Cashier";
                    break;
                case POSTrPrintingStatus.Posted:
                    _result = "Posted";
                    break;
                case POSTrPrintingStatus.DeliveryOrder:
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