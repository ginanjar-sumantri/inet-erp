using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class NCPSalesDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(byte _prmNCPSalesStatus)
        //{
        //    string _result = "";

        //    switch (_prmNCPSalesStatus)
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

        //public static NCPSalesStatus GetStatus(byte _prmNCPSalesStatus)
        //{
        //    NCPSalesStatus _result;

        //    switch (_prmNCPSalesStatus)
        //    {
        //        case 0:
        //            _result = NCPSalesStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = NCPSalesStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = NCPSalesStatus.Approved;
        //            break;
        //        case 3:
        //            _result = NCPSalesStatus.Posting;
        //            break;
        //        default:
        //            _result = NCPSalesStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(NCPSalesStatus _prmNCPSalesStatus)
        //{
        //    byte _result;

        //    switch (_prmNCPSalesStatus)
        //    {
        //        case NCPSalesStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case NCPSalesStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case NCPSalesStatus.Approved:
        //            _result = 2;
        //            break;
        //        case NCPSalesStatus.Posting:
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