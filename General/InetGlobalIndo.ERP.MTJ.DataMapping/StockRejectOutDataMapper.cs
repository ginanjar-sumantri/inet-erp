using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockRejectOutDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStockRejectOutStatus)
        //{
        //    string _result = "";

        //    switch (_prmStockRejectOutStatus)
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

        //public static StockRejectOutStatus GetStatus(char _prmStockRejectOutStatus)
        //{
        //    StockRejectOutStatus _result;

        //    switch (_prmStockRejectOutStatus)
        //    {
        //        case 'H':
        //            _result = StockRejectOutStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockRejectOutStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockRejectOutStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockRejectOutStatus.Approved;
        //            break;
        //        default:
        //            _result = StockRejectOutStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(StockRejectOutStatus _prmStockRejectOutStatus)
        //{
        //    char _result;

        //    switch (_prmStockRejectOutStatus)
        //    {
        //        case StockRejectOutStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockRejectOutStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockRejectOutStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockRejectOutStatus.Approved:
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