using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class CustBeginningDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmCustBeginningStatus)
        //{
        //    string _result = "";

        //    switch (_prmCustBeginningStatus)
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

        //public static CustBeginningStatus GetStatus(char _prmCustBeginningStatus)
        //{
        //    CustBeginningStatus _result;

        //    switch (_prmCustBeginningStatus)
        //    {
        //        case 'H':
        //            _result = CustBeginningStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = CustBeginningStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = CustBeginningStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = CustBeginningStatus.Approved;
        //            break;
        //        default:
        //            _result = CustBeginningStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(CustBeginningStatus _prmCustBeginningStatus)
        //{
        //    char _result;

        //    switch (_prmCustBeginningStatus)
        //    {
        //        case CustBeginningStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case CustBeginningStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case CustBeginningStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case CustBeginningStatus.Approved:
        //            _result = 'A';
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