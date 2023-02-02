using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class PurchaseReturDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(byte _prmPurchaseReturStatus)
        //{
        //    string _result = "";

        //    switch (_prmPurchaseReturStatus)
        //    {
        //        case 0:
        //            _result = "On Hold";
        //            break;
        //        case 1:
        //            _result = "Waiting For Approval";
        //            break;
        //        case 2:
        //            _result = "Approved";
        //            break;
        //        case 3:
        //            _result = "Posted";
        //            break;
        //        default:
        //            _result = "On Hold";
        //            break;
        //    }

        //    return _result;
        //}

        //public static PurchaseReturStatus GetStatus(byte _prmPurchaseReturStatus)
        //{
        //    PurchaseReturStatus _result;

        //    switch (_prmPurchaseReturStatus)
        //    {
        //        case 0:
        //            _result = PurchaseReturStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = PurchaseReturStatus.Posted;
        //            break;
        //        case 2:
        //            _result = PurchaseReturStatus.WaitingForApproval;
        //            break;
        //        case 3:
        //            _result = PurchaseReturStatus.Approved;
        //            break;
        //        default:
        //            _result = PurchaseReturStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(PurchaseReturStatus _prmPurchaseReturStatus)
        //{
        //    byte _result;

        //    switch (_prmPurchaseReturStatus)
        //    {
        //        case PurchaseReturStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case PurchaseReturStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case PurchaseReturStatus.Approved:
        //            _result = 2;
        //            break;
        //        case PurchaseReturStatus.Posted:
        //            _result = 3;
        //            break;
        //        default:
        //            _result = 0;
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

        //public static YesNo GetYesNo(char _prmYesNo)
        //{
        //    YesNo _result = YesNo.No;

        //    switch (_prmYesNo)
        //    {
        //        case 'Y':
        //            _result = YesNo.Yes;
        //            break;
        //        case 'N':
        //            _result = YesNo.No;
        //            break;
        //        default:
        //            _result = YesNo.No;
        //            break;
        //    }

        //    return _result;
        //}
    }
}