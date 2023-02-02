using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class RequestSalesReturDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmRequestSalesReturStatus)
        //{
        //    string _result = "";

        //    switch (_prmRequestSalesReturStatus)
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

        //public static string GetStatusTextDetail(char _prmRequestSalesReturStatusDt)
        //{
        //    string _result = "";

        //    switch (_prmRequestSalesReturStatusDt)
        //    {
        //        case 'Y':
        //            _result = "Closed";
        //            break;
        //        case 'N':
        //            _result = "Opened";
        //            break;
        //        default:
        //            _result = "Opened";
        //            break;
        //    }

        //    return _result;
        //}

        //public static RequestSalesReturStatus GetStatus(char _prmRequestSalesReturStatus)
        //{
        //    RequestSalesReturStatus _result;

        //    switch (_prmRequestSalesReturStatus)
        //    {
        //        case 'H':
        //            _result = RequestSalesReturStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = RequestSalesReturStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = RequestSalesReturStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = RequestSalesReturStatus.Approved;
        //            break;
        //        default:
        //            _result = RequestSalesReturStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static RequestSalesReturStatusDt GetStatusDetail(char _prmRequestSalesReturStatusDt)
        //{
        //    RequestSalesReturStatusDt _result;

        //    switch (_prmRequestSalesReturStatusDt)
        //    {
        //        case 'Y':
        //            _result = RequestSalesReturStatusDt.Closed;
        //            break;
        //        case 'N':
        //            _result = RequestSalesReturStatusDt.Open;
        //            break;
        //        default:
        //            _result = RequestSalesReturStatusDt.Open;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(RequestSalesReturStatus _prmRequestSalesReturStatus)
        //{
        //    char _result;

        //    switch (_prmRequestSalesReturStatus)
        //    {
        //        case RequestSalesReturStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case RequestSalesReturStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case RequestSalesReturStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case RequestSalesReturStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatusDetail(RequestSalesReturStatusDt _prmRequestSalesReturStatusDt)
        //{
        //    char _result;

        //    switch (_prmRequestSalesReturStatusDt)
        //    {
        //        case RequestSalesReturStatusDt.Closed:
        //            _result = 'Y';
        //            break;
        //        case RequestSalesReturStatusDt.Open:
        //            _result = 'N';
        //            break;
        //        default:
        //            _result = 'N';
        //            break;
        //    }

        //    return _result;
        //}

        public static string GetStatusTextDetail(char _prmRequestSalesReturStatusDt)
        {
            string _result = "";

            switch (_prmRequestSalesReturStatusDt)
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

        public static RequestSalesReturStatusDt GetStatusDetail(char _prmRequestSalesReturStatusDt)
        {
            RequestSalesReturStatusDt _result;

            switch (_prmRequestSalesReturStatusDt)
            {
                case 'Y':
                    _result = RequestSalesReturStatusDt.Closed;
                    break;
                case 'N':
                    _result = RequestSalesReturStatusDt.Open;
                    break;
                default:
                    _result = RequestSalesReturStatusDt.Open;
                    break;
            }

            return _result;
        }

        public static char GetStatusDetail(RequestSalesReturStatusDt _prmRequestSalesReturStatusDt)
        {
            char _result;

            switch (_prmRequestSalesReturStatusDt)
            {
                case RequestSalesReturStatusDt.Closed:
                    _result = 'Y';
                    break;
                case RequestSalesReturStatusDt.Open:
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