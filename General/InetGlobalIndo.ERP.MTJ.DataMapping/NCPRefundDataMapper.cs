using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class NCPRefundDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(byte _prmBankReconStatus)
        //{
        //    string _result = "";

        //    switch (_prmBankReconStatus)
        //    {
        //        case 0:
        //            _result = "Hold";
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
        //            _result = "Hold";
        //            break;
        //    }

        //    return _result;
        //}

        //public static NCPRefundStatus GetStatus(byte _prmBankReconStatus)
        //{
        //    NCPRefundStatus _result;

        //    switch (_prmBankReconStatus)
        //    {
        //        case 0:
        //            _result = NCPRefundStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = NCPRefundStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = NCPRefundStatus.Approved;
        //            break;
        //        case 3:
        //            _result = NCPRefundStatus.Posted;
        //            break;
        //        default:
        //            _result = NCPRefundStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(NCPRefundStatus _prmBankReconStatus)
        //{
        //    byte _result;

        //    switch (_prmBankReconStatus)
        //    {
        //        case NCPRefundStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case NCPRefundStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case NCPRefundStatus.Approved:
        //            _result = 2;
        //            break;
        //        case NCPRefundStatus.Posted:
        //            _result = 3;
        //            break;
        //        default:
        //            _result = 0;
        //            break;
        //    }

        //    return _result;
        //}
    }
}