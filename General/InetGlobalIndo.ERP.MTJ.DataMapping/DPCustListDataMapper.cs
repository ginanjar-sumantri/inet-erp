using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class DPCustListDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmDPCustomerListStatus)
        //{
        //    string _result = "";

        //    switch (_prmDPCustomerListStatus)
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

        //public static DPCustomerListStatus GetStatus(char _prmDPCustomerListStatus)
        //{
        //    DPCustomerListStatus _result;

        //    switch (_prmDPCustomerListStatus)
        //    {
        //        case 'H':
        //            _result = DPCustomerListStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = DPCustomerListStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = DPCustomerListStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = DPCustomerListStatus.Approved;
        //            break;
        //        default:
        //            _result = DPCustomerListStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(DPCustomerListStatus _prmDPCustomerListStatus)
        //{
        //    char _result;

        //    switch (_prmDPCustomerListStatus)
        //    {
        //        case DPCustomerListStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case DPCustomerListStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case DPCustomerListStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case DPCustomerListStatus.Approved:
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