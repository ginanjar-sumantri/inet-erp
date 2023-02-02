using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class SupplierReturDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmCustomerNoteStatus)
        //{
        //    string _result = "";

        //    switch (_prmCustomerNoteStatus)
        //    {
        //        case 'H':
        //            _result = "On Hold";
        //            break;p
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

        //public static CustomerNoteStatus GetStatus(char _prmCustomerNoteStatus)
        //{
        //    CustomerNoteStatus _result;

        //    switch (_prmCustomerNoteStatus)
        //    {
        //        case 'H':
        //            _result = CustomerNoteStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = CustomerNoteStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = CustomerNoteStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = CustomerNoteStatus.Approved;
        //            break;
        //        default:
        //            _result = CustomerNoteStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(CustomerNoteStatus _prmCustomerNoteStatus)
        //{
        //    char _result;

        //    switch (_prmCustomerNoteStatus)
        //    {
        //        case CustomerNoteStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case CustomerNoteStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case CustomerNoteStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case CustomerNoteStatus.Approved:
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