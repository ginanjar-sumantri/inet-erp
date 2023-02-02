using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class GiroReceiptChangeDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmPCOStatus)
        //{
        //    string _result = "";

        //    switch (_prmPCOStatus)
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

        //public static GiroReceiptChangeStatus GetStatus(char _prmGiroReceiptChangeStatus)
        //{
        //    GiroReceiptChangeStatus _result;

        //    switch (_prmGiroReceiptChangeStatus)
        //    {
        //        case 'H':
        //            _result = GiroReceiptChangeStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = GiroReceiptChangeStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = GiroReceiptChangeStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = GiroReceiptChangeStatus.Approved;
        //            break;
        //        default:
        //            _result = GiroReceiptChangeStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(GiroReceiptChangeStatus _prmGiroReceiptChangeStatus)
        //{
        //    char _result;

        //    switch (_prmGiroReceiptChangeStatus)
        //    {
        //        case GiroReceiptChangeStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case GiroReceiptChangeStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case GiroReceiptChangeStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case GiroReceiptChangeStatus.Approved:
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