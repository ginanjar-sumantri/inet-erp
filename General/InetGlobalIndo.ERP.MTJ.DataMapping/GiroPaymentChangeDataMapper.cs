using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class GiroPaymentChangeDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmGiroPaymentChangeStatus)
        //{
        //    string _result = "";

        //    switch (_prmGiroPaymentChangeStatus)
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

        //public static GiroPaymentChangeStatus GetStatus(char _prmGiroPaymentChangeStatus)
        //{
        //    GiroPaymentChangeStatus _result;

        //    switch (_prmGiroPaymentChangeStatus)
        //    {
        //        case 'H':
        //            _result = GiroPaymentChangeStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = GiroPaymentChangeStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = GiroPaymentChangeStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = GiroPaymentChangeStatus.Approved;
        //            break;
        //        default:
        //            _result = GiroPaymentChangeStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}


        //public static char GetStatus(GiroPaymentChangeStatus _prmGiroPaymentChangeStatus)
        //{
        //    char _result;

        //    switch (_prmGiroPaymentChangeStatus)
        //    {
        //        case GiroPaymentChangeStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case GiroPaymentChangeStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case GiroPaymentChangeStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case GiroPaymentChangeStatus.Approved:
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