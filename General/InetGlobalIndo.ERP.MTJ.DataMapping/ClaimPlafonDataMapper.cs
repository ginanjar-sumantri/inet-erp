using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class ClaimPlafonDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmClaimPlafonStatus)
        //{
        //    string _result = "";

        //    switch (_prmClaimPlafonStatus)
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

        //public static ClaimPlafonStatus GetStatus(Char _prmClaimPlafonStatus)
        //{
        //    ClaimPlafonStatus _result;

        //    switch (_prmClaimPlafonStatus)
        //    {
        //        case 'H':
        //            _result = ClaimPlafonStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = ClaimPlafonStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = ClaimPlafonStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = ClaimPlafonStatus.Posted;
        //            break;
        //        default:
        //            _result = ClaimPlafonStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(ClaimPlafonStatus _prmClainPlafonStatus)
        //{
        //    Char _result;

        //    switch (_prmClainPlafonStatus)
        //    {
        //        case ClaimPlafonStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case ClaimPlafonStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case ClaimPlafonStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case ClaimPlafonStatus.Posted:
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
