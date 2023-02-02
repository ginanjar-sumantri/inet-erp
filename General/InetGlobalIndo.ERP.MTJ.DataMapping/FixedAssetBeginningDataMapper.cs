using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class FixedAssetBeginningDataMapper : TransactionDataMapper
    {
        //public static FABeginningStatus IsStatus(byte _prmFABeginningStatus)
        //{
        //    FABeginningStatus _result;

        //    switch (_prmFABeginningStatus)
        //    {
        //        case 0:
        //            _result = FABeginningStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = FABeginningStatus.Posted;
        //            break;
        //        case 2:
        //            _result = FABeginningStatus.WaitingForApproval;
        //            break;
        //        case 3:
        //            _result = FABeginningStatus.Approved;
        //            break;
        //        default:
        //            _result = FABeginningStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte IsStatus(FABeginningStatus _prmFABeginningStatus)
        //{
        //    byte _result;

        //    switch (_prmFABeginningStatus)
        //    {
        //        case FABeginningStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case FABeginningStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case FABeginningStatus.Approved:
        //            _result = 2;
        //            break;
        //        case FABeginningStatus.Posted:
        //            _result = 3;
        //            break;
        //        default:
        //            _result = 0;
        //            break;
        //    }

        //    return _result;
        //}

        //public static string StatusText(byte _prmFABeginningStatus)
        //{
        //    string _result;

        //    switch (_prmFABeginningStatus)
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
        //            _result = "Hold";
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte StatusText(string _prmFABeginningStatus)
        //{
        //    byte _result;

        //    switch (_prmFABeginningStatus)
        //    {
        //        case "On Hold":
        //            _result = 0;
        //            break;
        //        case "Waiting For Approval":
        //            _result = 1;
        //            break;
        //        case "Approved":
        //            _result = 2;
        //            break;
        //        case "Posted":
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