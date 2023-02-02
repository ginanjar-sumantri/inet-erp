using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class LoanInDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmLoanInStatus)
        //{
        //    string _result = "";

        //    switch (_prmLoanInStatus)
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

        //public static LoanInStatus GetStatus(Char _prmLoanInStatus)
        //{
        //    LoanInStatus _result;

        //    switch (_prmLoanInStatus)
        //    {
        //        case 'H':
        //            _result = LoanInStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = LoanInStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = LoanInStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = LoanInStatus.Posted;
        //            break;
        //        default:
        //            _result = LoanInStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(LoanInStatus _prmLoanInStatus)
        //{
        //    Char _result;

        //    switch (_prmLoanInStatus)
        //    {
        //        case LoanInStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case LoanInStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case LoanInStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case LoanInStatus.Posted:
        //            _result = 'P';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static string GetStatusTextDetail(char? _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case 'Y':
                    _result = "Closed";
                    break;
                case 'N':
                    _result = "Open";
                    break;
                default:
                    _result = "Open";
                    break;
            }

            return _result;
        }

        public static char GetStatusDetail(LoanInStatusDt _prmLoanInStatusDt)
        {
            char _result;

            switch (_prmLoanInStatusDt)
            {
                case LoanInStatusDt.Closed:
                    _result = 'Y';
                    break;
                case LoanInStatusDt.Open:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }
    }
}
