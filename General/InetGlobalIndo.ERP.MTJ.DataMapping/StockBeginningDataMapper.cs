using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockBeginningDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStockBeginningStatus)
        //{
        //    string _result = "";

        //    switch (_prmStockBeginningStatus)
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

        //public static StockBeginningStatus GetStatus(char _prmStockBeginningStatus)
        //{
        //    StockBeginningStatus _result;

        //    switch (_prmStockBeginningStatus)
        //    {
        //        case 'H':
        //            _result = StockBeginningStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockBeginningStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockBeginningStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockBeginningStatus.Approved;
        //            break;
        //        default:
        //            _result = StockBeginningStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(StockBeginningStatus _prmStockBeginningStatus)
        //{
        //    char _result;

        //    switch (_prmStockBeginningStatus)
        //    {
        //        case StockBeginningStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockBeginningStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockBeginningStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockBeginningStatus.Approved:
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