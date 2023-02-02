using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class PaymentTradeDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmPaymentTradeStatus)
        //{
        //    string _result = "";

        //    switch (_prmPaymentTradeStatus)
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

        //public static PaymentTradeStatus GetStatus(char _prmPaymentTradeStatus)
        //{
        //    PaymentTradeStatus _result;

        //    switch (_prmPaymentTradeStatus)
        //    {
        //        case 'H':
        //            _result = PaymentTradeStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = PaymentTradeStatus.Posted;
        //            break;
        //        case 'A':
        //            _result = PaymentTradeStatus.Approved;
        //            break;
        //        case 'G':
        //            _result = PaymentTradeStatus.WaitingForApproval;
        //            break;
        //        default:
        //            _result = PaymentTradeStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(PaymentTradeStatus _prmPaymentTradeStatus)
        //{
        //    char _result;

        //    switch (_prmPaymentTradeStatus)
        //    {
        //        case PaymentTradeStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case PaymentTradeStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case PaymentTradeStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case PaymentTradeStatus.WaitingForApproval:
        //            _result = 'G';
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