using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class DPCustomerDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmDPCustomerStatus)
        //{
        //    string _result = "";

        //    switch (_prmDPCustomerStatus)
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

        //public static DPCustomerStatus GetStatus(char _prmDPCustomerStatus)
        //{ 
        //    DPCustomerStatus _result;

        //    switch (_prmDPCustomerStatus)
        //    {
        //        case 'H':
        //            _result = DPCustomerStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = DPCustomerStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = DPCustomerStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = DPCustomerStatus.Approved;
        //            break;
        //        default:
        //            _result = DPCustomerStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(DPCustomerStatus _prmDPCustomerStatus)
        //{
        //    char _result;

        //    switch (_prmDPCustomerStatus)
        //    {
        //        case DPCustomerStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case DPCustomerStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case DPCustomerStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case DPCustomerStatus.Approved:
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