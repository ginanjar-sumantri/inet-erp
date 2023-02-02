using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class EmpRewardDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmEmpRewardStatus)
        //{
        //    string _result = "";

        //    switch (_prmEmpRewardStatus)
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

        //public static EmpRewardStatus GetStatus(Char _prmEmpRewardStatus)
        //{
        //    EmpRewardStatus _result;

        //    switch (_prmEmpRewardStatus)
        //    {
        //        case 'H':
        //            _result = EmpRewardStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = EmpRewardStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = EmpRewardStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = EmpRewardStatus.Posting;
        //            break;
        //        default:
        //            _result = EmpRewardStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(EmpRewardStatus _prmEmpRewardStatus)
        //{
        //    Char _result;

        //    switch (_prmEmpRewardStatus)
        //    {
        //        case EmpRewardStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case EmpRewardStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case EmpRewardStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case EmpRewardStatus.Posting:
        //            _result = 'P';
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