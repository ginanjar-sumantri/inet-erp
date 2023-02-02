using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class LeaveAddDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Byte? _prmLeaveAddStatus)
        //{
        //    string _result = "";

        //    switch (_prmLeaveAddStatus)
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

        //public static LeaveAddStatus GetStatus(Byte? _prmLeaveAddStatus)
        //{
        //    LeaveAddStatus _result;

        //    switch (_prmLeaveAddStatus)
        //    {
        //        case 0:
        //            _result = LeaveAddStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = LeaveAddStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = LeaveAddStatus.Approved;
        //            break;
        //        case 3:
        //            _result = LeaveAddStatus.Posted;
        //            break;
        //        default:
        //            _result = LeaveAddStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Byte? GetStatus(LeaveAddStatus _prmLeaveAddStatus)
        //{
        //    Byte? _result = 0;

        //    switch (_prmLeaveAddStatus)
        //    {
        //        case LeaveAddStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case LeaveAddStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case LeaveAddStatus.Approved:
        //            _result = 2;
        //            break;
        //        case LeaveAddStatus.Posted:
        //            _result = 3;
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

        public static string GetTypeText(LeaveType _prmLeaveType)
        {
            string _result = "";

            switch (_prmLeaveType)
            {
                case LeaveType.Add:
                    _result = "ADD";
                    break;
                case LeaveType.Temp:
                    _result = "TEMP";
                    break;
                default:
                    _result = "ADD";
                    break;
            }

            return _result;
        }

        public static LeaveType GetType(string _prmLeaveType)
        {
            LeaveType _result;

            switch (_prmLeaveType)
            {
                case "ADD":
                    _result = LeaveType.Add;
                    break;
                case "TEMP":
                    _result = LeaveType.Temp;
                    break;
                default:
                    _result = LeaveType.Add;
                    break;
            }

            return _result;
        }
    }
}