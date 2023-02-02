using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class FAPurchaseDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStatus)
        //{
        //    string _result = "";

        //    switch (_prmStatus)
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

        //public static char GetStatus(FAPurchaseStatus _prmStatus)
        //{
        //    char _result;

        //    switch (_prmStatus)
        //    {
        //        case FAPurchaseStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case FAPurchaseStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case FAPurchaseStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case FAPurchaseStatus.Approved:
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