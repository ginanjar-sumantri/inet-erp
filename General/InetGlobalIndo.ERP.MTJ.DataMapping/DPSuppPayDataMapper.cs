using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class DPSuppPayDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmDPSuppPayStatus)
        //{
        //    string _result = "";

        //    switch (_prmDPSuppPayStatus)
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

        //public static DPSuppPayStatus GetStatus(char _prmDPSuppPayStatus)
        //{
        //    DPSuppPayStatus _result;

        //    switch (_prmDPSuppPayStatus)
        //    {
        //        case 'H':
        //            _result = DPSuppPayStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = DPSuppPayStatus.Posted;
        //            break;
        //        case 'A':
        //            _result = DPSuppPayStatus.Approved;
        //            break;
        //        case 'G':
        //            _result = DPSuppPayStatus.WaitingForApproval;
        //            break;
        //        default:
        //            _result = DPSuppPayStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(DPSuppPayStatus _prmDPSuppPayStatus)
        //{
        //    char _result;

        //    switch (_prmDPSuppPayStatus)
        //    {
        //        case DPSuppPayStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case DPSuppPayStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case DPSuppPayStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case DPSuppPayStatus.WaitingForApproval:
        //            _result = 'G';
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
    }
}