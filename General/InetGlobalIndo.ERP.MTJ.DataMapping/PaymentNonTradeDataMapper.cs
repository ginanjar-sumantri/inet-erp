using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class PaymentNonTradeDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmPaymentNonTradeStatus)
        //{
        //    string _result = "";

        //    switch (_prmPaymentNonTradeStatus)
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

        //public static PaymentNonTradeStatus GetStatus(char _prmPaymentNonTradeStatus)
        //{
        //    PaymentNonTradeStatus _result;

        //    switch (_prmPaymentNonTradeStatus)
        //    {
        //        case 'H':
        //            _result = PaymentNonTradeStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = PaymentNonTradeStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = PaymentNonTradeStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = PaymentNonTradeStatus.Approved;
        //            break;
        //        default:
        //            _result = PaymentNonTradeStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(PaymentNonTradeStatus _prmPaymentNonTradeStatus)
        //{
        //    char _result;

        //    switch (_prmPaymentNonTradeStatus)
        //    {
        //        case PaymentNonTradeStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case PaymentNonTradeStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case PaymentNonTradeStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case PaymentNonTradeStatus.Approved:
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