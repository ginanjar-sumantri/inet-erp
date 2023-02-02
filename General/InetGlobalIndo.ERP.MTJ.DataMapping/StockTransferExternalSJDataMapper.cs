using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockTransferExternalSJDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStockTransferExternalSJStatus)
        //{
        //    string _result = "";

        //    switch (_prmStockTransferExternalSJStatus)
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

        //public static StockTransferExternalSJStatus GetStatus(char _prmStockTransferExternalSJStatus)
        //{
        //    StockTransferExternalSJStatus _result;

        //    switch (_prmStockTransferExternalSJStatus)
        //    {
        //        case 'H':
        //            _result = StockTransferExternalSJStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockTransferExternalSJStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockTransferExternalSJStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockTransferExternalSJStatus.Approved;
        //            break;
        //        default:
        //            _result = StockTransferExternalSJStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(StockTransferExternalSJStatus _prmStockTransferExternalSJStatus)
        //{
        //    char _result;

        //    switch (_prmStockTransferExternalSJStatus)
        //    {
        //        case StockTransferExternalSJStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockTransferExternalSJStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockTransferExternalSJStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockTransferExternalSJStatus.Approved:
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