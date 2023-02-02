using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class SalaryPaymentDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmSalaryPaymentStatus)
        //{
        //    string _result = "";

        //    switch (_prmSalaryPaymentStatus)
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

        //public static SalaryPaymentStatus GetStatus(char _prmSalaryPaymentStatus)
        //{
        //    SalaryPaymentStatus _result;

        //    switch (_prmSalaryPaymentStatus)
        //    {
        //        case 'H':
        //            _result = SalaryPaymentStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = SalaryPaymentStatus.Posting;
        //            break;
        //        case 'A':
        //            _result = SalaryPaymentStatus.Approved;
        //            break;
        //        case 'G':
        //            _result = SalaryPaymentStatus.WaitingForApproval;
        //            break;
        //        default:
        //            _result = SalaryPaymentStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(SalaryPaymentStatus _prmSalaryPaymentStatus)
        //{
        //    char _result;

        //    switch (_prmSalaryPaymentStatus)
        //    {
        //        case SalaryPaymentStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case SalaryPaymentStatus.Posting:
        //            _result = 'P';
        //            break;
        //        case SalaryPaymentStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case SalaryPaymentStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetYesNo(YesNo _prmYesNo)
        //{
        //    char _result;

        //    switch (_prmYesNo)
        //    {
        //        case YesNo.Yes:
        //            _result = 'Y';
        //            break;
        //        case YesNo.No:
        //            _result = 'N';
        //            break;
        //        default:
        //            _result = 'N';
        //            break;
        //    }

        //    return _result;
        //}
    }
}