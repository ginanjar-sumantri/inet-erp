using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockOpnameDataMapper : TransactionDataMapper
    {
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

        //public static StockOpnameStatus GetStatus(char _prmStatus)
        //{
        //    StockOpnameStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 'H':
        //            _result = StockOpnameStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockOpnameStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockOpnameStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockOpnameStatus.Approved;
        //            break;
        //        default:
        //            _result = StockOpnameStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(StockOpnameStatus _prmStatus)
        //{
        //    char _result;

        //    switch (_prmStatus)
        //    {
        //        case StockOpnameStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockOpnameStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockOpnameStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockOpnameStatus.Approved:
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