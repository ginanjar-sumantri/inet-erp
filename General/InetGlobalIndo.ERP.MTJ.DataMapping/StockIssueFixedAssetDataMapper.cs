using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockIssueFixedAssetDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStockIssueFixedAssetStatus)
        //{
        //    string _result = "";

        //    switch (_prmStockIssueFixedAssetStatus)
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

        //public static StockIssueFixedAssetStatus GetStatus(char _prmStockIssueFixedAssetStatus)
        //{
        //    StockIssueFixedAssetStatus _result;

        //    switch (_prmStockIssueFixedAssetStatus)
        //    {
        //        case 'H':
        //            _result = StockIssueFixedAssetStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockIssueFixedAssetStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockIssueFixedAssetStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockIssueFixedAssetStatus.Approved;
        //            break;
        //        default:
        //            _result = StockIssueFixedAssetStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(StockIssueFixedAssetStatus _prmStockIssueFixedAssetStatus)
        //{
        //    char _result;

        //    switch (_prmStockIssueFixedAssetStatus)
        //    {
        //        case StockIssueFixedAssetStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockIssueFixedAssetStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockIssueFixedAssetStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockIssueFixedAssetStatus.Approved:
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