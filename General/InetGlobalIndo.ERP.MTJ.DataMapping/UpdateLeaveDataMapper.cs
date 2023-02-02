using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class UpdateLeaveDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(byte _prmUpdateLeaveStatus)
        //{
        //    string _result = "";

        //    switch (_prmUpdateLeaveStatus)
        //    {
        //        case 0:
        //            _result = "Draft";
        //            break;
        //        case 1:
        //            _result = "Waiting For Approval";
        //            break;
        //        case 2:
        //            _result = "Approved";
        //            break;
        //        default:
        //            _result = "Draft";
        //            break;
        //    }

        //    return _result;
        //}

        //public static UpdateLeaveStatus GetStatus(byte _prmUpdateLeaveStatus)
        //{
        //    UpdateLeaveStatus _result;

        //    switch (_prmUpdateLeaveStatus)
        //    {
        //        case 0:
        //            _result = UpdateLeaveStatus.Draft;
        //            break;
        //        case 1:
        //            _result = UpdateLeaveStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = UpdateLeaveStatus.Approved;
        //            break;
        //        default:
        //            _result = UpdateLeaveStatus.Draft;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(UpdateLeaveStatus _prmUpdateLeaveStatus)
        //{
        //    byte _result;

        //    switch (_prmUpdateLeaveStatus)
        //    {
        //        case UpdateLeaveStatus.Draft:
        //            _result = 0;
        //            break;
        //        case UpdateLeaveStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case UpdateLeaveStatus.Approved:
        //            _result = 2;
        //            break;
        //        default:
        //            _result = 0;
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