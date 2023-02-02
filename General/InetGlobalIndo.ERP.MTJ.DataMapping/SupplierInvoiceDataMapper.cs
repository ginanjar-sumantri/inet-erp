using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class SupplierInvoiceDataMapper : TransactionDataMapper
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

        //public static SupplierInvoiceStatus GetStatus(char _prmStatus)
        //{
        //    SupplierInvoiceStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 'H':
        //            _result = SupplierInvoiceStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = SupplierInvoiceStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = SupplierInvoiceStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = SupplierInvoiceStatus.Approved;
        //            break;
        //        default:
        //            _result = SupplierInvoiceStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(SupplierInvoiceStatus _prmStatus)
        //{
        //    char _result;

        //    switch (_prmStatus)
        //    {
        //        case SupplierInvoiceStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case SupplierInvoiceStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case SupplierInvoiceStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case SupplierInvoiceStatus.Approved:
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