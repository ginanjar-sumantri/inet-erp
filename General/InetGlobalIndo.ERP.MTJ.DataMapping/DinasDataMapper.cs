using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class DinasDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmDinasStatus)
        //{
        //    string _result = "";

        //    switch (_prmDinasStatus)
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

        //public static DinasStatus GetStatus(Char _prmDinasStatus)
        //{
        //    DinasStatus _result;

        //    switch (_prmDinasStatus)
        //    {
        //        case 'H':
        //            _result = DinasStatus.Draft;
        //            break;
        //        case 'G':
        //            _result = DinasStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = DinasStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = DinasStatus.Posted;
        //            break;
        //        default:
        //            _result = DinasStatus.Draft;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(DinasStatus _prmDinasStatus)
        //{
        //    Char _result = ' ';

        //    switch (_prmDinasStatus)
        //    {
        //        case DinasStatus.Draft:
        //            _result = 'H';
        //            break;
        //        case DinasStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case DinasStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case DinasStatus.Posted:
        //            _result = 'P';
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