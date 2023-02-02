using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class FixedAssetPurchaseOrderDataMapper : TransactionDataMapper
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

        //public static FixedAssetPurchaseOrderStatus GetStatus(char _prmStatus)
        //{
        //    FixedAssetPurchaseOrderStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 'H':
        //            _result = FixedAssetPurchaseOrderStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = FixedAssetPurchaseOrderStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = FixedAssetPurchaseOrderStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = FixedAssetPurchaseOrderStatus.Approved;
        //            break;
        //        default:
        //            _result = FixedAssetPurchaseOrderStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(FixedAssetPurchaseOrderStatus _prmStatus)
        //{
        //    char _result;

        //    switch (_prmStatus)
        //    {
        //        case FixedAssetPurchaseOrderStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case FixedAssetPurchaseOrderStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case FixedAssetPurchaseOrderStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case FixedAssetPurchaseOrderStatus.Approved:
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

        public static char GetStatusDetail(FixedAssetPurchaseOrderStatusDt _prmSalesOrderStatusDt)
        {
            char _result;

            switch (_prmSalesOrderStatusDt)
            {
                case FixedAssetPurchaseOrderStatusDt.Closed:
                    _result = 'Y';
                    break;
                case FixedAssetPurchaseOrderStatusDt.Open:
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