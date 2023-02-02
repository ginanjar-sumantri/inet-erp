using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class ReceiptTradeDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmReceiptTradeStatus)
        //{
        //    string _result = "";

        //    switch (_prmReceiptTradeStatus)
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

        //public static ReceiptTradeStatus GetStatus(char _prmReceiptTradeStatus)
        //{
        //    ReceiptTradeStatus _result;

        //    switch (_prmReceiptTradeStatus)
        //    {
        //        case 'H':
        //            _result = ReceiptTradeStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = ReceiptTradeStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = ReceiptTradeStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = ReceiptTradeStatus.Approved;
        //            break;
        //        default:
        //            _result = ReceiptTradeStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(ReceiptTradeStatus _prmPaymentTradeStatus)
        //{
        //    char _result;

        //    switch (_prmPaymentTradeStatus)
        //    {
        //        case ReceiptTradeStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case ReceiptTradeStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case ReceiptTradeStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case ReceiptTradeStatus.Approved:
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