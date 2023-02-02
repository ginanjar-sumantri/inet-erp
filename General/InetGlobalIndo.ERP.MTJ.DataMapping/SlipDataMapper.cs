using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class SlipDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmSlipStatus)
        //{
        //    string _result = "";

        //    switch (_prmSlipStatus)
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

        //public static SlipStatus GetStatus(Char _prmSlipStatus)
        //{
        //    SlipStatus _result;

        //    switch (_prmSlipStatus)
        //    {
        //        case 'H':
        //            _result = SlipStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = SlipStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = SlipStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = SlipStatus.Posted;
        //            break;
        //        default:
        //            _result = SlipStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(SlipStatus _prmSlipStatus)
        //{
        //    Char _result;

        //    switch (_prmSlipStatus)
        //    {
        //        case SlipStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case SlipStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case SlipStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case SlipStatus.Posted:
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
