using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class PayrollDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmPayrollStatus)
        //{
        //    string _result = "";

        //    switch (_prmPayrollStatus)
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

        //public static PayrollStatus GetStatus(Char _prmPayrollStatus)
        //{
        //    PayrollStatus _result;

        //    switch (_prmPayrollStatus)
        //    {
        //        case 'H':
        //            _result = PayrollStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = PayrollStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = PayrollStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = PayrollStatus.Posted;
        //            break;
        //        default:
        //            _result = PayrollStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(PayrollStatus _prmPayrollStatus)
        //{
        //    Char _result;

        //    switch (_prmPayrollStatus)
        //    {
        //        case PayrollStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case PayrollStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case PayrollStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case PayrollStatus.Posted:
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
