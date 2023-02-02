using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockChangeGoodDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStockChangeGoodStatus)
        //{
        //    string _result = "";

        //    switch (_prmStockChangeGoodStatus)
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

        //public static StockChangeGoodStatus GetStatus(char _prmStockChangeGoodStatus)
        //{
        //    StockChangeGoodStatus _result;

        //    switch (_prmStockChangeGoodStatus)
        //    {
        //        case 'H':
        //            _result = StockChangeGoodStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockChangeGoodStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockChangeGoodStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockChangeGoodStatus.Approved;
        //            break;
        //        default:
        //            _result = StockChangeGoodStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(StockChangeGoodStatus _prmStockChangeGoodStatus)
        //{
        //    char _result;

        //    switch (_prmStockChangeGoodStatus)
        //    {
        //        case StockChangeGoodStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockChangeGoodStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockChangeGoodStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockChangeGoodStatus.Approved:
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