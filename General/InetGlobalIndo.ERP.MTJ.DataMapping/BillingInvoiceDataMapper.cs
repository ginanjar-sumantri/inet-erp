using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class BillingInvoiceDataMapper : TransactionDataMapper
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
        //            _result = "on Hold";
        //            break;
        //    }

        //    return _result;
        //}

        //public static BillingInvoiceStatus GetStatus(char _prmStatus)
        //{
        //    BillingInvoiceStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 'H':
        //            _result = BillingInvoiceStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = BillingInvoiceStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = BillingInvoiceStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = BillingInvoiceStatus.Approved;
        //            break;
        //        default:
        //            _result = BillingInvoiceStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}
        
        //public static char GetStatus(BillingInvoiceStatus _prmStatus)
        //{
        //    char _result;

        //    switch (_prmStatus)
        //    {
        //        case BillingInvoiceStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case BillingInvoiceStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case BillingInvoiceStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case BillingInvoiceStatus.Approved:
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