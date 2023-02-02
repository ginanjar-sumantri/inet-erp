using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class SalesOrderDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmSalesOrderStatus)
        //{
        //    string _result = "";

        //    switch (_prmSalesOrderStatus)
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

        public static string GetStatusTextDetail(char? _prmSalesOrderStatusDt)
        {
            string _result = "";

            switch (_prmSalesOrderStatusDt)
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

        //public static SalesOrderStatus GetStatus(char _prmSalesOrderStatus)
        //{
        //    SalesOrderStatus _result;

        //    switch (_prmSalesOrderStatus)
        //    {
        //        case 'H':
        //            _result = SalesOrderStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = SalesOrderStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = SalesOrderStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = SalesOrderStatus.Approved;
        //            break;
        //        default:
        //            _result = SalesOrderStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        public static SalesOrderStatusDt GetStatusDetail(char _prmSalesOrderStatusDt)
        {
            SalesOrderStatusDt _result;

            switch (_prmSalesOrderStatusDt)
            {
                case 'Y':
                    _result = SalesOrderStatusDt.Closed;
                    break;
                case 'N':
                    _result = SalesOrderStatusDt.Open;
                    break;
                default:
                    _result = SalesOrderStatusDt.Open;
                    break;
            }

            return _result;
        }

        //public static char GetStatus(SalesOrderStatus _prmSalesOrderStatus)
        //{
        //    char _result;

        //    switch (_prmSalesOrderStatus)
        //    {
        //        case SalesOrderStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case SalesOrderStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case SalesOrderStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case SalesOrderStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static char GetStatusFlag(bool _prmStatus)
        {
            char _result;

            switch (_prmStatus)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static bool GetStatusFlag(char _prmStatus)
        {
            bool _result;

            switch (_prmStatus.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char GetStatusDetail(SalesOrderStatusDt _prmSalesOrderStatusDt)
        {
            char _result;

            switch (_prmSalesOrderStatusDt)
            {
                case SalesOrderStatusDt.Closed:
                    _result = 'Y';
                    break;
                case SalesOrderStatusDt.Open:
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