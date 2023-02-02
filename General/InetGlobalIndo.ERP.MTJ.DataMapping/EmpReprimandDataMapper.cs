using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class EmpReprimandDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmEmpReprimandStatus)
        //{
        //    string _result = "";

        //    switch (_prmEmpReprimandStatus)
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

        //public static EmpReprimandStatus GetStatus(Char _prmEmpReprimandStatus)
        //{
        //    EmpReprimandStatus _result;

        //    switch (_prmEmpReprimandStatus)
        //    {
        //        case 'H':
        //            _result = EmpReprimandStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = EmpReprimandStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = EmpReprimandStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = EmpReprimandStatus.Posting;
        //            break;
        //        default:
        //            _result = EmpReprimandStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(EmpReprimandStatus _prmEmpReprimandStatus)
        //{
        //    Char _result;

        //    switch (_prmEmpReprimandStatus)
        //    {
        //        case EmpReprimandStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case EmpReprimandStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case EmpReprimandStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case EmpReprimandStatus.Posting:
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