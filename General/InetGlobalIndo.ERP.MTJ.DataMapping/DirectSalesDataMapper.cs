using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class DirectSalesDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Byte _prmDirectSalesStatus)
        //{
        //    string _result = "";

        //    switch (_prmDirectSalesStatus)
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

        //public static DirectSalesStatus GetStatus(Byte _prmDirectSalesStatus)
        //{
        //    DirectSalesStatus _result;

        //    switch (_prmDirectSalesStatus)
        //    {
        //        case 0:
        //            _result = DirectSalesStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = DirectSalesStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = DirectSalesStatus.Approved;
        //            break;
        //        case 3:
        //            _result = DirectSalesStatus.Posting;
        //            break;
        //        default:
        //            _result = DirectSalesStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Byte GetStatus(DirectSalesStatus _prmDirectSalesStatus)
        //{
        //    Byte _result;

        //    switch (_prmDirectSalesStatus)
        //    {
        //        case DirectSalesStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case DirectSalesStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case DirectSalesStatus.Approved:
        //            _result = 2;
        //            break;
        //        case DirectSalesStatus.Posting:
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