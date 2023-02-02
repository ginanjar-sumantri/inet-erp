using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSPromoDataMapper
    {
        public static byte GetStatus(POSPromoStatus _prmStatus)
        {
            byte _result = 0;

            switch (_prmStatus)
            {
                case POSPromoStatus.OnHold:
                    _result = 0;
                    break;
                case POSPromoStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case POSPromoStatus.Approved:
                    _result = 2;
                    break;
                case POSPromoStatus.Posting:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static POSPromoStatus GetStatus(int _prmStatus)
        {
            POSPromoStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = POSPromoStatus.OnHold;
                    break;
                case 1:
                    _result = POSPromoStatus.WaitingForApproval;
                    break;
                case 2:
                    _result = POSPromoStatus.Approved;
                    break;
                case 3:
                    _result = POSPromoStatus.Posting;
                    break;
                default:
                    _result = POSPromoStatus.OnHold;
                    break;
            }

            return _result;
        }

        public static string GetStatusText(POSPromoStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSPromoStatus.OnHold:
                    _result = "On Hold";
                    break;
                case POSPromoStatus.WaitingForApproval:
                    _result = "Waiting For Approval";
                    break;
                case POSPromoStatus.Approved:
                    _result = "Approved";
                    break;
                case POSPromoStatus.Posting:
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

        public static int GetNumericStatus(POSPromoStatus _prmStatus)
        {
            int _result;

            switch (_prmStatus)
            {
                case POSPromoStatus.OnHold:
                    _result = 0;
                    break;
                case POSPromoStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case POSPromoStatus.Approved:
                    _result = 2;
                    break;
                case POSPromoStatus.Posting:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static String GetAmountType(POSPromoAmountType _prmStatus)
        {
            String _result = "";

            switch (_prmStatus)
            {
                case POSPromoAmountType.Amount:
                    _result = "A";
                    break;
                case POSPromoAmountType.Percentage:
                    _result = "P";
                    break;
                default:
                    _result = "A";
                    break;
            }

            return _result;
        }

        public static POSPromoAmountType GetAmountType(String _prmStatus)
        {
            POSPromoAmountType _result;

            switch (_prmStatus)
            {
                case "A":
                    _result = POSPromoAmountType.Amount;
                    break;
                case "P":
                    _result = POSPromoAmountType.Percentage;
                    break;
                default:
                    _result = POSPromoAmountType.Amount;
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

        public static String GetAmountTypeText(POSPromoAmountType _prmStatus)
        {
            String _result = "";

            switch (_prmStatus)
            {
                case POSPromoAmountType.Amount:
                    _result = "Amount";
                    break;
                case POSPromoAmountType.Percentage:
                    _result = "Percentage";
                    break;
                default:
                    _result = "Amount";
                    break;
            }

            return _result;
        }
    }
}