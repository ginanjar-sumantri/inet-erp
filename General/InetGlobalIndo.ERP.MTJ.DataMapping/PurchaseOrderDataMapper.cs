using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class PurchaseOrderDataMapper : TransactionDataMapper
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

        //public static PurchaseOrderStatus GetStatus(char _prmStatus)
        //{
        //    PurchaseOrderStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 'H':
        //            _result = PurchaseOrderStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = PurchaseOrderStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = PurchaseOrderStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = PurchaseOrderStatus.Approved;
        //            break;
        //        default:
        //            _result = PurchaseOrderStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(PurchaseOrderStatus _prmStatus)
        //{
        //    char _result;

        //    switch (_prmStatus)
        //    {
        //        case PurchaseOrderStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case PurchaseOrderStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case PurchaseOrderStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case PurchaseOrderStatus.Approved:
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

        public static string GetStatusTextDetail(char? _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
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

        public static char GetStatusDetail(PurchaseOrderStatusDt _prmSalesOrderStatusDt)
        {
            char _result;

            switch (_prmSalesOrderStatusDt)
            {
                case PurchaseOrderStatusDt.Closed:
                    _result = 'Y';
                    break;
                case PurchaseOrderStatusDt.Open:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }
    }
}