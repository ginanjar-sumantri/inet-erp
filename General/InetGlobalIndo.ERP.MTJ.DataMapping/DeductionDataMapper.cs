using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class DeductionDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmDeductionStatus)
        //{
        //    string _result = "";

        //    switch (_prmDeductionStatus)
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

        //public static DeductionStatus GetStatus(Char _prmDeductionStatus)
        //{
        //    DeductionStatus _result;

        //    switch (_prmDeductionStatus)
        //    {
        //        case 'H':
        //            _result = DeductionStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = DeductionStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = DeductionStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = DeductionStatus.Posted;
        //            break;
        //        default:
        //            _result = DeductionStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(DeductionStatus _prmDeductionStatus)
        //{
        //    Char _result;

        //    switch (_prmDeductionStatus)
        //    {
        //        case DeductionStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case DeductionStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case DeductionStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case DeductionStatus.Posted:
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
