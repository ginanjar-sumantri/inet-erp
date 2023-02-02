using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class BillOfLadingDataMapper : TransactionDataMapper
    {
        //public static char GetStatus(BillOfLadingStatus _prmStatus)
        //{
        //    char _result = ' ';

        //    switch (_prmStatus)
        //    {
        //        case BillOfLadingStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case BillOfLadingStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case BillOfLadingStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case BillOfLadingStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        //public static string GetStatusText(BillOfLadingStatus _prmStatus)
        //{
        //    string _result = "";

        //    switch (_prmStatus)
        //    {
        //        case BillOfLadingStatus.OnHold:
        //            _result = "On Hold";
        //            break;
        //        case BillOfLadingStatus.WaitingForApproval:
        //            _result = "Waiting For Approval";
        //            break;
        //        case BillOfLadingStatus.Approved:
        //            _result = "Approved";
        //            break;
        //        case BillOfLadingStatus.Posted:
        //            _result = "Posted";
        //            break;
        //        default:
        //            _result = "On Hold";
        //            break;
        //    }

        //    return _result;
        //}

        //public static string GetStatusText(char _prmStatus)
        //{
        //    string _result = "";

        //    switch (_prmStatus)
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