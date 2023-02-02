using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockTransRequestDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStockTransRequestStatus)
        //{
        //    string _result = "";

        //    switch (_prmStockTransRequestStatus)
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

        public static string GetStatusTextDetail(char _prmStockTransRequestStatusDt)
        {
            string _result = "";

            switch (_prmStockTransRequestStatusDt)
            {
                case 'Y':
                    _result = "Closed";
                    break;
                case 'N':
                    _result = "Open";
                    break;
                default:
                    _result = "Open";
                    break;
            }

            return _result;
        }

        //public static StockTransRequestStatus GetStatus(char _prmStockTransRequestStatus)
        //{
        //    StockTransRequestStatus _result;

        //    switch (_prmStockTransRequestStatus)
        //    {
        //        case 'H':
        //            _result = StockTransRequestStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockTransRequestStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockTransRequestStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockTransRequestStatus.Approved;
        //            break;
        //        default:
        //            _result = StockTransRequestStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        public static StockTransRequestStatusDt GetStatusDetail(char _prmStockTransRequestStatusDt)
        {
            StockTransRequestStatusDt _result;

            switch (_prmStockTransRequestStatusDt)
            {
                case 'Y':
                    _result = StockTransRequestStatusDt.Closed;
                    break;
                case 'N':
                    _result = StockTransRequestStatusDt.Open;
                    break;
                default:
                    _result = StockTransRequestStatusDt.Open;
                    break;
            }

            return _result;
        }

        //public static char GetStatus(StockTransRequestStatus _prmStockTransRequestStatus)
        //{
        //    char _result;

        //    switch (_prmStockTransRequestStatus)
        //    {
        //        case StockTransRequestStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockTransRequestStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockTransRequestStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockTransRequestStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static char GetStatusDetail(StockTransRequestStatusDt _prmStockTransRequestStatusDt)
        {
            char _result;

            switch (_prmStockTransRequestStatusDt)
            {
                case StockTransRequestStatusDt.Closed:
                    _result = 'Y';
                    break;
                case StockTransRequestStatusDt.Open:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

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