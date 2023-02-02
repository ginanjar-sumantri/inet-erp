using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class ChangeShiftDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmChangeShiftStatus)
        //{
        //    string _result = "";

        //    switch (_prmChangeShiftStatus)
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

        //public static ChangeShiftStatus GetStatus(Char _prmChangeShiftStatus)
        //{
        //    ChangeShiftStatus _result;

        //    switch (_prmChangeShiftStatus)
        //    {
        //        case 'H':
        //            _result = ChangeShiftStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = ChangeShiftStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = ChangeShiftStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = ChangeShiftStatus.Posting;
        //            break;
        //        default:
        //            _result = ChangeShiftStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(ChangeShiftStatus _prmChangeShiftStatus)
        //{
        //    Char _result = ' ';

        //    switch (_prmChangeShiftStatus)
        //    {
        //        case ChangeShiftStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case ChangeShiftStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case ChangeShiftStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case ChangeShiftStatus.Posting:
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