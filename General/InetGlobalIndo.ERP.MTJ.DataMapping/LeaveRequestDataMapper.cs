using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class LeaveRequestDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Byte? _prmLeaveRequestStatus)
        //{
        //    string _result = "";

        //    switch (_prmLeaveRequestStatus)
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

        //public static LeaveRequestStatus GetStatus(Byte? _prmLeaveRequestStatus)
        //{
        //    LeaveRequestStatus _result;

        //    switch (_prmLeaveRequestStatus)
        //    {
        //        case 0:
        //            _result = LeaveRequestStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = LeaveRequestStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = LeaveRequestStatus.Approved;
        //            break;
        //        case 3:
        //            _result = LeaveRequestStatus.Posted;
        //            break;
        //        default:
        //            _result = LeaveRequestStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Byte? GetStatus(LeaveRequestStatus _prmLeaveRequestStatus)
        //{
        //    Byte? _result;

        //    switch (_prmLeaveRequestStatus)
        //    {
        //        case LeaveRequestStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case LeaveRequestStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case LeaveRequestStatus.Approved:
        //            _result = 2;
        //            break;
        //        case LeaveRequestStatus.Posted:
        //            _result = 3;
        //            break;
        //        default:
        //            _result = 0;
        //            break;
        //    }

        //    return _result;
        //}
    }
}
