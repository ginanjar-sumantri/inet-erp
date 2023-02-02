using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class RetailDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(byte _prmRetailStatus)
        //{
        //    string _result = "";

        //    switch (_prmRetailStatus)
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
        //        default:
        //            _result = "On Hold";
        //            break;
        //    }

        //    return _result;
        //}

        //public static RetailStatus GetStatus(byte _prmRetailStatus)
        //{
        //    RetailStatus _result;

        //    switch (_prmRetailStatus)
        //    {
        //        case 0:
        //            _result = RetailStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = RetailStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = RetailStatus.Approved;
        //            break;
        //        default:
        //            _result = RetailStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(RetailStatus _prmRetailStatus)
        //{
        //    byte _result;

        //    switch (_prmRetailStatus)
        //    {
        //        case RetailStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case RetailStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case RetailStatus.Approved:
        //            _result = 2;
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