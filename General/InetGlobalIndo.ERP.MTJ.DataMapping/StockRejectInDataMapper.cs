using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockRejectInDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStockRejectInStatus)
        //{
        //    string _result = "";

        //    switch (_prmStockRejectInStatus)
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

        //public static StockRejectInStatus GetStatus(char _prmStockRejectInStatus)
        //{
        //    StockRejectInStatus _result;

        //    switch (_prmStockRejectInStatus)
        //    {
        //        case 'H':
        //            _result = StockRejectInStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockRejectInStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockRejectInStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockRejectInStatus.Approved;
        //            break;
        //        default:
        //            _result = StockRejectInStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(StockRejectInStatus _prmStockRejectInStatus)
        //{
        //    char _result;

        //    switch (_prmStockRejectInStatus)
        //    {
        //        case StockRejectInStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockRejectInStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockRejectInStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockRejectInStatus.Approved:
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