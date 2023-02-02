using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class TerminationFinishingDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(byte _prmTerminationFinishingStatus)
        //{
        //    string _result = "";

        //    switch (_prmTerminationFinishingStatus)
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
        //            _result = "Draft";
        //            break;
        //    }

        //    return _result;
        //}

        //public static TerminationFinishingStatus GetStatus(byte _prmTerminationFinishingStatus)
        //{
        //    TerminationFinishingStatus _result;

        //    switch (_prmTerminationFinishingStatus)
        //    {
        //        case 0:
        //            _result = TerminationFinishingStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = TerminationFinishingStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = TerminationFinishingStatus.Approved;
        //            break;
        //        case 3:
        //            _result = TerminationFinishingStatus.Posting;
        //            break;
        //        default:
        //            _result = TerminationFinishingStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(TerminationFinishingStatus _prmTerminationFinishingStatus)
        //{
        //    byte _result;

        //    switch (_prmTerminationFinishingStatus)
        //    {
        //        case TerminationFinishingStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case TerminationFinishingStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case TerminationFinishingStatus.Approved:
        //            _result = 2;
        //            break;
        //        case TerminationFinishingStatus.Posting:
        //            _result = 3;
        //            break;
        //        default:
        //            _result = 0;
        //            break;
        //    }

        //    return _result;
        //}

        public static string IsAllowCertification(Boolean _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case true:
                    _result = "Yes";
                    break;
                case false:
                    _result = "No";
                    break;
                default:
                    _result = "No";
                    break;
            }

            return _result;
        }

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