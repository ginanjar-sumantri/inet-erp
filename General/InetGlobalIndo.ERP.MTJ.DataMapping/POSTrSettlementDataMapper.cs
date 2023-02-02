using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSTrSettlementDataMapper
    {
        public static POSTrSettlementStatus GetStatus(Byte _prmStatus)
        {
            POSTrSettlementStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = POSTrSettlementStatus.OnHold;
                    break;
                case 1:
                    _result = POSTrSettlementStatus.VOID;
                    break;
                case 2:
                    _result = POSTrSettlementStatus.Posted;
                    break;
                default:
                    _result = POSTrSettlementStatus.OnHold;
                    break;
            }

            return _result;
        }

        public static Byte GetStatus(POSTrSettlementStatus _prmStatus)
        {
            Byte _result = 0;

            switch (_prmStatus)
            {
                case POSTrSettlementStatus.OnHold:
                    _result = 1;
                    break;
                case POSTrSettlementStatus.Posted:
                    _result = 2;
                    break;
                case POSTrSettlementStatus.VOID:
                    _result = 3;
                    break;
                default:
                    _result = 1;
                    break;
            }

            return _result;
        }

        public static string GetStatusText(int _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case 1:
                    _result = "OnHold";
                    break;
                case 2:
                    _result = "Posted";
                    break;
                case 3:
                    _result = "VOID";
                    break;
                default:
                    _result = "OnHold";
                    break;
            }

            return _result;
        }

        public static string GetStatusText(POSTrSettlementStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSTrSettlementStatus.OnHold:
                    _result = "1";
                    break;
                case POSTrSettlementStatus.Posted:
                    _result = "2";
                    break;
                case POSTrSettlementStatus.VOID:
                    _result = "3";
                    break;
                default:
                    _result = "1";
                    break;
            }

            return _result;
        }

        public static POSDoneSettlementStatus GetDoneSettlement(Char _prmValue)
        {
            POSDoneSettlementStatus _result;

            switch (_prmValue)
            {
                case 'N':
                    _result = POSDoneSettlementStatus.NotYet;
                    break;
                case 'Y':
                    _result = POSDoneSettlementStatus.Done;
                    break;                
                default:
                    _result = POSDoneSettlementStatus.NotYet;
                    break;
            }

            return _result;
        }

        public static Char GetDoneSettlement(POSDoneSettlementStatus _prmValue)
        {
            Char _result = ' ';

            switch (_prmValue)
            {
                case POSDoneSettlementStatus.NotYet:
                    _result = 'N';
                    break;
                case POSDoneSettlementStatus.Done:
                    _result = 'Y';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static String GetDoneSettlementText(Char _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case 'N':
                    _result = "Not Yet";
                    break;
                case 'Y':
                    _result = "Done";
                    break;
                default:
                    _result = "Not Yet";
                    break;
            }

            return _result;
        }

        public static POSDeliveryStatus GetDeliveryStatus(Boolean _prmValue)
        {
            POSDeliveryStatus _result;

            switch (_prmValue)
            {
                case false:
                    _result = POSDeliveryStatus.NotYetDelivered;
                    break;
                case true:
                    _result = POSDeliveryStatus.Delivered;
                    break;
                default:
                    _result = POSDeliveryStatus.NotYetDelivered;
                    break;
            }

            return _result;
        }

        public static Boolean GetDeliveryStatus(POSDeliveryStatus _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue)
            {
                case POSDeliveryStatus.NotYetDelivered:
                    _result = false;
                    break;
                case POSDeliveryStatus.Delivered:
                    _result = true;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static String GetDeliveryStatusText(Boolean _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case false:
                    _result = "Not Yet Delivered";
                    break;
                case true:
                    _result = "Delivered";
                    break;
                default:
                    _result = "Not Yet Delivered";
                    break;
            }

            return _result;
        }

        public static String GetSettleType(POSTrSettleType _prmStatus)
        {
            String _result = "";

            switch (_prmStatus)
            {
                case POSTrSettleType.Paid:
                    _result = "Paid";
                    break;
                case POSTrSettleType.DP:
                    _result = "DP";
                    break;
                default:
                    _result = "Paid";
                    break;
            }

            return _result;
        }

        public static POSTrSettleType GetSettleType(String _prmStatus)
        {
            POSTrSettleType _result;

            switch (_prmStatus)
            {
                case "Paid":
                    _result = POSTrSettleType.Paid;
                    break;
                case "DP":
                    _result = POSTrSettleType.DP;
                    break;
                default:
                    _result = POSTrSettleType.Paid;
                    break;
            }

            return _result;
        }
    }
}