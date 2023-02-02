using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockTransferInternalDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStockTransferInternalStatus)
        //{
        //    string _result = "";

        //    switch (_prmStockTransferInternalStatus)
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

        //public static StockTransferInternalStatus GetStatus(char _prmStockTransferInternalStatus)
        //{
        //    StockTransferInternalStatus _result;

        //    switch (_prmStockTransferInternalStatus)
        //    {
        //        case 'H':
        //            _result = StockTransferInternalStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockTransferInternalStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockTransferInternalStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockTransferInternalStatus.Approved;
        //            break;
        //        default:
        //            _result = StockTransferInternalStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(StockTransferInternalStatus _prmStockTransferInternalStatus)
        //{
        //    char _result;

        //    switch (_prmStockTransferInternalStatus)
        //    {
        //        case StockTransferInternalStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockTransferInternalStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockTransferInternalStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockTransferInternalStatus.Approved:
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