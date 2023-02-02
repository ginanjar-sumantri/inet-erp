using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class LemburDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmLemburStatus)
        //{
        //    string _result = "";

        //    switch (_prmLemburStatus)
        //    {
        //        case 'H':
        //            _result = "On Hold";
        //            break;
        //        case 'G':
        //            _result = "Waiting For Approval";
        //            break;
        //        case 'A':
        //            _result = "Approved";
        //            break;
        //        case 'P':
        //            _result = "Posted";
        //            break;
        //        default:
        //            _result = "On Hold";
        //            break;
        //    }

        //    return _result;
        //}

        //public static LemburStatus GetStatus(Char _prmLemburStatus)
        //{
        //    LemburStatus _result;

        //    switch (_prmLemburStatus)
        //    {
        //        case 'H':
        //            _result = LemburStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = LemburStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = LemburStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = LemburStatus.Posted;
        //            break;
        //        default:
        //            _result = LemburStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(LemburStatus _prmLemburStatus)
        //{
        //    Char _result;

        //    switch (_prmLemburStatus)
        //    {
        //        case LemburStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case LemburStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case LemburStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case LemburStatus.Posted:
        //            _result = 'P';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static Char GetStatusDt(LemburDtStatus _prmLemburStatus)
        {
            Char _result;

            switch (_prmLemburStatus)
            {
                case LemburDtStatus.OnHold:
                    _result = 'H';
                    break;
                case LemburDtStatus.Cancel:
                    _result = 'C';
                    break;
                case LemburDtStatus.Complete:
                    _result = 'Y';
                    break;
                default:
                    _result = 'H';
                    break;
            }

            return _result;
        }

        public static string GetStatusDtText(Char _prmLemburStatus)
        {
            string _result = "";

            switch (_prmLemburStatus)
            {
                case 'H':
                    _result = "On Hold";
                    break;
                case 'C':
                    _result = "Cancelled";
                    break;
                case 'Y':
                    _result = "Completed";
                    break;
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }
    }
}
