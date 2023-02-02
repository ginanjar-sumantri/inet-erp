using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class DirectPurchaseDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Byte _prmDirectPurchaseStatus)
        //{
        //    string _result = "";

        //    switch (_prmDirectPurchaseStatus)
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

        //public static DirectPurchaseStatus GetStatus(Byte _prmDirectPurchaseStatus)
        //{
        //    DirectPurchaseStatus _result;

        //    switch (_prmDirectPurchaseStatus)
        //    {
        //        case 0:
        //            _result = DirectPurchaseStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = DirectPurchaseStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = DirectPurchaseStatus.Approved;
        //            break;
        //        case 3:
        //            _result = DirectPurchaseStatus.Posting;
        //            break;
        //        default:
        //            _result = DirectPurchaseStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Byte GetStatus(DirectPurchaseStatus _prmDirectPurchaseStatus)
        //{
        //    Byte _result;

        //    switch (_prmDirectPurchaseStatus)
        //    {
        //        case DirectPurchaseStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case DirectPurchaseStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case DirectPurchaseStatus.Approved:
        //            _result = 2;
        //            break;
        //        case DirectPurchaseStatus.Posting:
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
    }
}