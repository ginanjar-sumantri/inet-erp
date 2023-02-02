using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class STCReturDataMapper : TransactionDataMapper
    {
        //public static char GetStatus(StockReceivingReturStatus _prmStatus)
        //{
        //    char _result = ' ';

        //    switch (_prmStatus)
        //    {
        //        case StockReceivingReturStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockReceivingReturStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockReceivingReturStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockReceivingReturStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        //public static string GetStatusText(StockReceivingReturStatus _prmStatus)
        //{
        //    string _result = "";

        //    switch (_prmStatus)
        //    {
        //        case StockReceivingReturStatus.OnHold:
        //            _result = "On Hold";
        //            break;
        //        case StockReceivingReturStatus.WaitingForApproval:
        //            _result = "Waiting For Approval";
        //            break;
        //        case StockReceivingReturStatus.Approved:
        //            _result = "Approved";
        //            break;
        //        case StockReceivingReturStatus.Posted:
        //            _result = "Posted";
        //            break;
        //        default:
        //            _result = "On Hold";
        //            break;
        //    }

        //    return _result;
        //}

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