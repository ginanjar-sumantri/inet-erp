using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockIssueRequestDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStockIssueRequestStatus)
        //{
        //    string _result = "";

        //    switch (_prmStockIssueRequestStatus)
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

        public static string GetStatusTextDetail(char _prmStockIssueRequestStatusDt)
        {
            string _result = "";

            switch (_prmStockIssueRequestStatusDt)
            {
                case 'Y':
                    _result = "Closed";
                    break;
                case 'N':
                    _result = "Opened";
                    break;
                default:
                    _result = "Opened";
                    break;
            }

            return _result;
        }

        //public static StockIssueRequestStatus GetStatus(char _prmStockIssueRequestStatus)
        //{
        //    StockIssueRequestStatus _result;

        //    switch (_prmStockIssueRequestStatus)
        //    {
        //        case 'H':
        //            _result = StockIssueRequestStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockIssueRequestStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockIssueRequestStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockIssueRequestStatus.Approved;
        //            break;
        //        default:
        //            _result = StockIssueRequestStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        public static StockIssueRequestStatusDt GetStatusDetail(char _prmStockIssueRequestStatusDt)
        {
            StockIssueRequestStatusDt _result;

            switch (_prmStockIssueRequestStatusDt)
            {
                case 'Y':
                    _result = StockIssueRequestStatusDt.Closed;
                    break;
                case 'N':
                    _result = StockIssueRequestStatusDt.Open;
                    break;
                default:
                    _result = StockIssueRequestStatusDt.Open;
                    break;
            }

            return _result;
        }

        //public static char GetStatus(StockIssueRequestStatus _prmStockIssueRequestStatus)
        //{
        //    char _result;

        //    switch (_prmStockIssueRequestStatus)
        //    {
        //        case StockIssueRequestStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockIssueRequestStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockIssueRequestStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockIssueRequestStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static char GetStatusDetail(StockIssueRequestStatusDt _prmStockIssueRequestStatusDt)
        {
            char _result;

            switch (_prmStockIssueRequestStatusDt)
            {
                case StockIssueRequestStatusDt.Closed:
                    _result = 'Y';
                    break;
                case StockIssueRequestStatusDt.Open:
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

        public static string GetFgType(StockRequestType _prmType)
        {
            string _result = "";

            switch (_prmType)
            {
                case StockRequestType.RequestStock:
                    _result = "SIR";
                    break;
                case StockRequestType.RequestFA:
                    _result = "SIF";
                    break;
                default:
                    _result = "SIR";
                    break;
            }

            return _result;
        }

        public static StockRequestType GetFgType(string _prmType)
        {
            StockRequestType _result;

            switch (_prmType)
            {
                case "SIR":
                    _result = StockRequestType.RequestStock;
                    break;
                case "SIF":
                    _result = StockRequestType.RequestFA;
                    break;
                default:
                    _result = StockRequestType.RequestStock;
                    break;
            }

            return _result;
        }
    }
}