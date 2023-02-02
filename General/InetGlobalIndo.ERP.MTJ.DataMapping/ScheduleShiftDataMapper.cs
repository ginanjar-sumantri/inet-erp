using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class ScheduleShiftDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmScheduleShiftStatus)
        //{
        //    string _result = "";

        //    switch (_prmScheduleShiftStatus)
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

        //public static ScheduleShiftStatus GetStatus(Char _prmScheduleShiftStatus)
        //{
        //    ScheduleShiftStatus _result;

        //    switch (_prmScheduleShiftStatus)
        //    {
        //        case 'H':
        //            _result = ScheduleShiftStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = ScheduleShiftStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = ScheduleShiftStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = ScheduleShiftStatus.Posted;
        //            break;
        //        default:
        //            _result = ScheduleShiftStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(ScheduleShiftStatus _prmScheduleShiftStatus)
        //{
        //    Char _result;

        //    switch (_prmScheduleShiftStatus)
        //    {
        //        case ScheduleShiftStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case ScheduleShiftStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case ScheduleShiftStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case ScheduleShiftStatus.Posted:
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
