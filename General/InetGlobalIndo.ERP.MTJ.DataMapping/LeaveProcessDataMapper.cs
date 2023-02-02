using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class LeaveProcessDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmLeaveProcessStatus)
        //{
        //    string _result = "";

        //    switch (_prmLeaveProcessStatus)
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

        //public static LeaveProcessStatus GetStatus(Char _prmLeaveProcessStatus)
        //{
        //    LeaveProcessStatus _result;

        //    switch (_prmLeaveProcessStatus)
        //    {
        //        case 'H':
        //            _result = LeaveProcessStatus.Draft;
        //            break;
        //        case 'G':
        //            _result = LeaveProcessStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = LeaveProcessStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = LeaveProcessStatus.Posted;
        //            break;
        //        default:
        //            _result = LeaveProcessStatus.Draft;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(LeaveProcessStatus _prmLeaveProcessStatus)
        //{
        //    Char _result = ' ';

        //    switch (_prmLeaveProcessStatus)
        //    {
        //        case LeaveProcessStatus.Draft:
        //            _result = 'H';
        //            break;
        //        case LeaveProcessStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case LeaveProcessStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case LeaveProcessStatus.Posted:
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