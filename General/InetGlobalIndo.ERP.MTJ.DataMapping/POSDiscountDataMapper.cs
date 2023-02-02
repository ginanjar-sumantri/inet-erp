using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSDiscountDataMapper
    {
        public static byte GetStatus(POSDiscountStatus _prmStatus)
        {
            byte _result = 0;

            switch (_prmStatus)
            {
                case POSDiscountStatus.OnHold:
                    _result = 0;
                    break;
                case POSDiscountStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case POSDiscountStatus.Approved:
                    _result = 2;
                    break;
                case POSDiscountStatus.Posting:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static POSDiscountStatus GetStatus(int _prmStatus)
        {
            POSDiscountStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = POSDiscountStatus.OnHold;
                    break;
                case 1:
                    _result = POSDiscountStatus.WaitingForApproval;
                    break;
                case 2:
                    _result = POSDiscountStatus.Approved;
                    break;
                case 3:
                    _result = POSDiscountStatus.Posting;
                    break;
                default:
                    _result = POSDiscountStatus.OnHold;
                    break;
            }

            return _result;
        }

        public static string GetStatusText(POSDiscountStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSDiscountStatus.OnHold:
                    _result = "On Hold";
                    break;
                case POSDiscountStatus.WaitingForApproval:
                    _result = "Waiting For Approval";
                    break;
                case POSDiscountStatus.Approved:
                    _result = "Approved";
                    break;
                case POSDiscountStatus.Posting:
                    _result = "Posting";
                    break;
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }

        public static string GetStatusText(char _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case 'H':
                    _result = "On Hold";
                    break;
                case 'G':
                    _result = "Waiting For Approval";
                    break;
                case 'A':
                    _result = "Approved";
                    break;
                case 'P':
                    _result = "Posting";
                    break;
                default:
                    _result = "On Hold";
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
                    _result = "On Hold";
                    break;
                case 1:
                    _result = "Waiting For Approval";
                    break;
                case 2:
                    _result = "Approved";
                    break;
                case 3:
                    _result = "Posting";
                    break;
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }

        public static char GetYesNo(YesNo _prmYesNo)
        {
            char _result;

            switch (_prmYesNo)
            {
                case YesNo.Yes:
                    _result = 'Y';
                    break;
                case YesNo.No:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static int GetNumericStatus(POSDiscountStatus _prmStatus)
        {
            int _result;

            switch (_prmStatus)
            {
                case POSDiscountStatus.OnHold:
                    _result = 0;
                    break;
                case POSDiscountStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case POSDiscountStatus.Approved:
                    _result = 2;
                    break;
                case POSDiscountStatus.Posting:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static String GetAmountType(POSDiscountAmountType _prmStatus)
        {
            String _result = "";

            switch (_prmStatus)
            {
                case POSDiscountAmountType.Amount:
                    _result = "A";
                    break;
                case POSDiscountAmountType.Percentage:
                    _result = "P";
                    break;
                default:
                    _result = "A";
                    break;
            }

            return _result;
        }

        public static POSDiscountAmountType GetAmountType(String _prmStatus)
        {
            POSDiscountAmountType _result;

            switch (_prmStatus)
            {
                case "A":
                    _result = POSDiscountAmountType.Amount;
                    break;
                case "P":
                    _result = POSDiscountAmountType.Percentage;
                    break;
                default:
                    _result = POSDiscountAmountType.Amount;
                    break;
            }

            return _result;
        }

        public static String GetAmountTypeText(String _prmStatus)
        {
            String _result = "";

            switch (_prmStatus)
            {
                case "A":
                    _result = "Amount";
                    break;
                case "P":
                    _result = "Percentage";
                    break;
                default:
                    _result = "Amount";
                    break;
            }

            return _result;
        }

        public static String GetAmountTypeText(POSDiscountAmountType _prmStatus)
        {
            String _result = "";

            switch (_prmStatus)
            {
                case POSDiscountAmountType.Amount:
                    _result = "Amount";
                    break;
                case POSDiscountAmountType.Percentage:
                    _result = "Percentage";
                    break;
                default:
                    _result = "Amount";
                    break;
            }

            return _result;
        }

        public static String GetCalcType(POSDiscountCalcType _prmStatus)
        {
            String _result = "";

            switch (_prmStatus)
            {
                case POSDiscountCalcType.Item:
                    _result = "I";
                    break;
                case POSDiscountCalcType.Subtotal:
                    _result = "S";
                    break;
                default:
                    _result = "I";
                    break;
            }

            return _result;
        }

        public static POSDiscountCalcType GetCalcType(String _prmStatus)
        {
            POSDiscountCalcType _result;

            switch (_prmStatus)
            {
                case "I":
                    _result = POSDiscountCalcType.Item;
                    break;
                case "S":
                    _result = POSDiscountCalcType.Subtotal;
                    break;
                default:
                    _result = POSDiscountCalcType.Item;
                    break;
            }

            return _result;
        }

        public static String GetCalcTypeText(String _prmStatus)
        {
            String _result = "";

            switch (_prmStatus)
            {
                case "I":
                    _result = "Item";
                    break;
                case "S":
                    _result = "Subtotal";
                    break;
                default:
                    _result = "Item";
                    break;
            }

            return _result;
        }

        public static String GetCalcTypeText(POSDiscountCalcType _prmStatus)
        {
            String _result = "";

            switch (_prmStatus)
            {
                case POSDiscountCalcType.Item:
                    _result = "Item";
                    break;
                case POSDiscountCalcType.Subtotal:
                    _result = "Subtotal";
                    break;
                default:
                    _result = "Item";
                    break;
            }

            return _result;
        }
    }
}