using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class THRDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmTHRStatus)
        //{
        //    string _result = "";

        //    switch (_prmTHRStatus)
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

        //public static THRStatus GetStatus(Char _prmTHRStatus)
        //{
        //    THRStatus _result;

        //    switch (_prmTHRStatus)
        //    {
        //        case 'H':
        //            _result = THRStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = THRStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = THRStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = THRStatus.Posting;
        //            break;
        //        default:
        //            _result = THRStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(THRStatus _prmTHRStatus)
        //{
        //    Char _result;

        //    switch (_prmTHRStatus)
        //    {
        //        case THRStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case THRStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case THRStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case THRStatus.Posting:
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
