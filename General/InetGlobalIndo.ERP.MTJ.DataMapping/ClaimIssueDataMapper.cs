using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class ClaimIssueDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmFinishingStatus)
        //{
        //    string _result = "";

        //    switch (_prmFinishingStatus)
        //    {
        //        case 'H':
        //            _result = "Hold";
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
        //            _result = "Hold";
        //            break;
        //    }

        //    return _result;
        //}

        //public static ClaimIssueStatus GetStatus(Char _prmAppFinishingStatus)
        //{
        //    ClaimIssueStatus _result;

        //    switch (_prmAppFinishingStatus)
        //    {
        //        case 'H':
        //            _result = ClaimIssueStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = ClaimIssueStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = ClaimIssueStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = ClaimIssueStatus.Posted;
        //            break;
        //        default:
        //            _result = ClaimIssueStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(ClaimIssueStatus _prmFinishingStatus)
        //{
        //    Char _result;

        //    switch (_prmFinishingStatus)
        //    {
        //        case ClaimIssueStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case ClaimIssueStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case ClaimIssueStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case ClaimIssueStatus.Posted:
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
