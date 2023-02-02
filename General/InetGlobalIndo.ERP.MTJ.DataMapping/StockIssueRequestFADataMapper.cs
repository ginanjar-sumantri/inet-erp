using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockIssueRequestFADataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStockIssueRequestFAStatus)
        //{
        //    string _result = "";

        //    switch (_prmStockIssueRequestFAStatus)
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

        public static string GetStatusTextDetail(char _prmStockIssueRequestFAStatusDt)
        {
            string _result = "";

            switch (_prmStockIssueRequestFAStatusDt)
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

        //public static StockIssueRequestFAStatus GetStatus(char _prmStockIssueRequestFAStatus)
        //{
        //    StockIssueRequestFAStatus _result;

        //    switch (_prmStockIssueRequestFAStatus)
        //    {
        //        case 'H':
        //            _result = StockIssueRequestFAStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockIssueRequestFAStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockIssueRequestFAStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockIssueRequestFAStatus.Approved;
        //            break;
        //        default:
        //            _result = StockIssueRequestFAStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        public static StockIssueRequestFAStatusDt GetStatusDetail(char _prmStockIssueRequestFAStatusDt)
        {
            StockIssueRequestFAStatusDt _result;

            switch (_prmStockIssueRequestFAStatusDt)
            {
                case 'Y':
                    _result = StockIssueRequestFAStatusDt.Closed;
                    break;
                case 'N':
                    _result = StockIssueRequestFAStatusDt.Open;
                    break;
                default:
                    _result = StockIssueRequestFAStatusDt.Open;
                    break;
            }

            return _result;
        }

        //public static char GetStatus(StockIssueRequestFAStatus _prmStockIssueRequestFAStatus)
        //{
        //    char _result;

        //    switch (_prmStockIssueRequestFAStatus)
        //    {
        //        case StockIssueRequestFAStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockIssueRequestFAStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockIssueRequestFAStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockIssueRequestFAStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static char GetStatusDetail(StockIssueRequestFAStatusDt _prmStockIssueRequestFAStatusDt)
        {
            char _result;

            switch (_prmStockIssueRequestFAStatusDt)
            {
                case StockIssueRequestFAStatusDt.Closed:
                    _result = 'Y';
                    break;
                case StockIssueRequestFAStatusDt.Open:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static char GetYesNo(YesNo _prmYesNo)
        {
            char _result;

            switch (_prmYesNo)
            {
                case YesNo.Yes:
                    _result = 'Y';
                    break;
                case YesNo.No:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

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