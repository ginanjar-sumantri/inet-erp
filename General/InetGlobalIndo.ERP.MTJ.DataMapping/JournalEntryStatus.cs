using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class JournalEntryStatus : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmJEStatus)
        //{
        //    string _result = "";

        //    switch (_prmJEStatus)
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

        //public static JEStatus GetStatus(char _prmJEStatus)
        //{
        //    JEStatus _result;

        //    switch (_prmJEStatus)
        //    {
        //        case 'H':
        //            _result = JEStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = JEStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = JEStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = JEStatus.Approved;
        //            break;
        //        default:
        //            _result = JEStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(JEStatus _prmJEStatus)
        //{
        //    char _result;

        //    switch (_prmJEStatus)
        //    {
        //        case JEStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case JEStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case JEStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case JEStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}
    }
}