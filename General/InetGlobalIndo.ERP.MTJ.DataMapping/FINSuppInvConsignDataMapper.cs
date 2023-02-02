using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class FINSuppInvConsignDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmDPCustomerReturStatus)
        //{
        //    string _result = "";

        //    switch (_prmDPCustomerReturStatus)
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

        //public static ConsignmentStatus GetStatus(char _prmDPCustomerReturStatus)
        //{
        //    ConsignmentStatus _result;

        //    switch (_prmDPCustomerReturStatus)
        //    {
        //        case 'H':
        //            _result = ConsignmentStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = ConsignmentStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = ConsignmentStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = ConsignmentStatus.Approved;
        //            break;
        //        default:
        //            _result = ConsignmentStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(ConsignmentStatus _prmDPCustomerReturStatus)
        //{
        //    char _result;

        //    switch (_prmDPCustomerReturStatus)
        //    {
        //        case ConsignmentStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case ConsignmentStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case ConsignmentStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case ConsignmentStatus.Approved:
        //            _result = 'A';
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