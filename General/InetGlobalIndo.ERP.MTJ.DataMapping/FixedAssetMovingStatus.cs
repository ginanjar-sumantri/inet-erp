using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class FixedAssetMovingStatus : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmFAMovingStatus)
        //{
        //    string _result = "";

        //    switch (_prmFAMovingStatus)
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

        //public static FAMoving GetStatus(char _prmFAMoving)
        //{
        //    FAMoving _result;

        //    switch (_prmFAMoving)
        //    {
        //        case 'H':
        //            _result = FAMoving.OnHold;
        //            break;
        //        case 'P':
        //            _result = FAMoving.Posted;
        //            break;
        //        case 'G':
        //            _result = FAMoving.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = FAMoving.Approved;
        //            break;
        //        default:
        //            _result = FAMoving.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(FAMoving _prmFAMoving)
        //{
        //    char _result;

        //    switch (_prmFAMoving)
        //    {
        //        case FAMoving.OnHold:
        //            _result = 'H';
        //            break;
        //        case FAMoving.Posted:
        //            _result = 'P';
        //            break;
        //        case FAMoving.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case FAMoving.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}
    }
}