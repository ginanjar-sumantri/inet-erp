using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class EmpGroupAssignDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char? _prmEmpGroupAssignStatus)
        //{
        //    string _result = "";

        //    switch (_prmEmpGroupAssignStatus)
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

        //public static EmpGroupAssignStatus GetStatus(Char _prmEmpGroupAssignStatus)
        //{
        //    EmpGroupAssignStatus _result;

        //    switch (_prmEmpGroupAssignStatus)
        //    {
        //        case 'H':
        //            _result = EmpGroupAssignStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = EmpGroupAssignStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = EmpGroupAssignStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = EmpGroupAssignStatus.Posted;
        //            break;
        //        default:
        //            _result = EmpGroupAssignStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(EmpGroupAssignStatus _prmEmpGroupAssignStatus)
        //{
        //    Char _result;

        //    switch (_prmEmpGroupAssignStatus)
        //    {
        //        case EmpGroupAssignStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case EmpGroupAssignStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case EmpGroupAssignStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case EmpGroupAssignStatus.Posted:
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
