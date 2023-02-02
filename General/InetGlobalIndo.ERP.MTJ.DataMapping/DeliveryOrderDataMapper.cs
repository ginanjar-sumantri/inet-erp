using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class DeliveryOrderDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmDeliveryOrderStatus)
        //{
        //    string _result = "";

        //    switch (_prmDeliveryOrderStatus)
        //    {
        //        case 'H':
        //            _result = "On Hold";
        //            break;
        //        case 'G':
        //            _result = "Waiting For Approval";
        //            break;
        //        case 'A':
        //            _result = "Approve";
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

        public static string GetStatusTextDetail(char _prmDeliveryOrderStatusDt)
        {
            string _result = "";

            switch (_prmDeliveryOrderStatusDt)
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

        //public static DeliveryOrderStatus GetStatus(char _prmDeliveryOrderStatus)
        //{
        //    DeliveryOrderStatus _result;

        //    switch (_prmDeliveryOrderStatus)
        //    {
        //        case 'H':
        //            _result = DeliveryOrderStatus.OnHold;
        //            break;
        //        case 'A':
        //            _result = DeliveryOrderStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = DeliveryOrderStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = DeliveryOrderStatus.WaitingForApproval;
        //            break;
        //        default:
        //            _result = DeliveryOrderStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        public static DeliveryOrderStatusDt GetStatusDetail(char _prmDeliveryOrderStatusDt)
        {
            DeliveryOrderStatusDt _result;

            switch (_prmDeliveryOrderStatusDt)
            {
                case 'Y':
                    _result = DeliveryOrderStatusDt.Closed;
                    break;
                case 'N':
                    _result = DeliveryOrderStatusDt.Open;
                    break;
                default:
                    _result = DeliveryOrderStatusDt.Open;
                    break;
            }

            return _result;
        }

        //public static char GetStatus(DeliveryOrderStatus _prmDeliveryOrderStatus)
        //{
        //    char _result;

        //    switch (_prmDeliveryOrderStatus)
        //    {
        //        case DeliveryOrderStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case DeliveryOrderStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case DeliveryOrderStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case DeliveryOrderStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static char GetStatusDetail(DeliveryOrderStatusDt _prmDeliveryOrderStatusDt)
        {
            char _result;

            switch (_prmDeliveryOrderStatusDt)
            {
                case DeliveryOrderStatusDt.Closed:
                    _result = 'Y';
                    break;
                case DeliveryOrderStatusDt.Open:
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