using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class LoanChangeDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmLoanChangeStatus)
        //{
        //    string _result = "";

        //    switch (_prmLoanChangeStatus)
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

        //public static LoanChangeStatus GetStatus(Char _prmLoanChangeStatus)
        //{
        //    LoanChangeStatus _result;

        //    switch (_prmLoanChangeStatus)
        //    {
        //        case 'H':
        //            _result = LoanChangeStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = LoanChangeStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = LoanChangeStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = LoanChangeStatus.Posted;
        //            break;
        //        default:
        //            _result = LoanChangeStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(LoanChangeStatus _prmLoanChangeStatus)
        //{
        //    Char _result;

        //    switch (_prmLoanChangeStatus)
        //    {
        //        case LoanChangeStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case LoanChangeStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case LoanChangeStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case LoanChangeStatus.Posted:
        //            _result = 'P';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}
    }
}
