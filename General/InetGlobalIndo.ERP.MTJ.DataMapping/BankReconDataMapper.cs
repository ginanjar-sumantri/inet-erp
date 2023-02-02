using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class BankReconDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(byte _prmBankReconStatus)
        //{
        //    string _result = "";

        //    switch (_prmBankReconStatus)
        //    {
        //        case 0:
        //            _result = "On Hold";
        //            break;
        //        case 1:
        //            _result = "Waiting For Approval";
        //            break;
        //        case 2:
        //            _result = "Approved";
        //            break;
        //        case 3:
        //            _result = "Posted";
        //            break;
        //        default:
        //            _result = "On Hold";
        //            break;
        //    }

        //    return _result;
        //}

        //public static BankReconStatus GetStatus(byte _prmBankReconStatus)
        //{
        //    BankReconStatus _result;

        //    switch (_prmBankReconStatus)
        //    {
        //        case 0:
        //            _result = BankReconStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = BankReconStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = BankReconStatus.Approved;
        //            break;
        //        case 3:
        //            _result = BankReconStatus.Posted;
        //            break;
        //        default:
        //            _result = BankReconStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(BankReconStatus _prmBankReconStatus)
        //{
        //    byte _result;

        //    switch (_prmBankReconStatus)
        //    {
        //        case BankReconStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case BankReconStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case BankReconStatus.Approved:
        //            _result = 2;
        //            break;
        //        case BankReconStatus.Posted:
        //            _result = 3;
        //            break;
        //        default:
        //            _result = 0;
        //            break;
        //    }

        //    return _result;
        //}

        public static string GetFgValue(bool _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case true:
                    _result = "+";
                    break;
                case false:
                    _result = "-";
                    break;
                default:
                    _result = "+";
                    break;
            }

            return _result;
        }

        public static bool GetFgValue(string _prmValue)
        {
            bool _result = false;

            switch (_prmValue)
            {
                case "+":
                    _result = true;
                    break;
                case "-":
                    _result = false;
                    break;
                default:
                    _result = true;
                    break;
            }

            return _result;
        }
    }
}