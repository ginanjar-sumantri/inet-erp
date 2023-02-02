using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class AbsenceRequestDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(byte _prmAbsenceRequestStatus)
        //{
        //    string _result = "";

        //    switch (_prmAbsenceRequestStatus)
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

        //public static AbsenceRequestStatus GetStatus(byte _prmAbsenceRequestStatus)
        //{
        //    AbsenceRequestStatus _result;

        //    switch (_prmAbsenceRequestStatus)
        //    {
        //        case 0:
        //            _result = AbsenceRequestStatus.Draft;
        //            break;
        //        case 1:
        //            _result = AbsenceRequestStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = AbsenceRequestStatus.Approved;
        //            break;
        //        case 3:
        //            _result = AbsenceRequestStatus.Posted;
        //            break;
        //        default:
        //            _result = AbsenceRequestStatus.Draft;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(AbsenceRequestStatus _prmFinishingStatus)
        //{
        //    byte _result;

        //    switch (_prmFinishingStatus)
        //    {
        //        case AbsenceRequestStatus.Draft:
        //            _result = 0;
        //            break;
        //        case AbsenceRequestStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case AbsenceRequestStatus.Approved:
        //            _result = 2;
        //            break;
        //        case AbsenceRequestStatus.Posted:
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
