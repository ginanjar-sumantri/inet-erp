using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class HRMTermDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmHRMTermStatus)
        //{
        //    string _result = "";

        //    switch (_prmHRMTermStatus)
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

        //public static HRMTermStatus GetStatus(Char _prmHRMTermStatus)
        //{
        //    HRMTermStatus _result;

        //    switch (_prmHRMTermStatus)
        //    {
        //        case 'H':
        //            _result = HRMTermStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = HRMTermStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = HRMTermStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = HRMTermStatus.Posted;
        //            break;
        //        default:
        //            _result = HRMTermStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(HRMTermStatus _prmHRMTermStatus)
        //{
        //    Char _result;

        //    switch (_prmHRMTermStatus)
        //    {
        //        case HRMTermStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case HRMTermStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case HRMTermStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case HRMTermStatus.Posted:
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
