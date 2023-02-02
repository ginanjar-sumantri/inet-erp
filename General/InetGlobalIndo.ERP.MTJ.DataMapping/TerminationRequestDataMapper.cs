using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class TerminationRequestDataMapper
    {
        public static string GetStatusText(byte _prmTerminationRequestStatus)
        {
            string _result = "";

            switch (_prmTerminationRequestStatus)
            {
                case 0:
                    _result = "On Hold";
                    break;
                case 1:
                    _result = "Waiting For Approval";
                    break;
                case 2:
                    _result = "Approved";
                    break;
                case 3:
                    _result = "Posted";
                    break;
                case 4:
                    _result = "Cancelled";
                    break;
                case 5:
                    _result = "Rejected";
                    break;
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }

        public static TerminationRequestStatus GetStatus(byte _prmTerminationRequestStatus)
        {
            TerminationRequestStatus _result;

            switch (_prmTerminationRequestStatus)
            {
                case 0:
                    _result = TerminationRequestStatus.Draft;
                    break;
                case 1:
                    _result = TerminationRequestStatus.WaitingForApproval;
                    break;
                case 2:
                    _result = TerminationRequestStatus.Approved;
                    break;
                case 3:
                    _result = TerminationRequestStatus.Closed;
                    break;
                case 4:
                    _result = TerminationRequestStatus.Cancelled;
                    break;
                case 5:
                    _result = TerminationRequestStatus.Rejected;
                    break;
                default:
                    _result = TerminationRequestStatus.Draft;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(TerminationRequestStatus _prmTerminationRequestStatus)
        {
            byte _result;

            switch (_prmTerminationRequestStatus)
            {
                case TerminationRequestStatus.Draft:
                    _result = 0;
                    break;
                case TerminationRequestStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case TerminationRequestStatus.Approved:
                    _result = 2;
                    break;
                case TerminationRequestStatus.Closed:
                    _result = 3;
                    break;
                case TerminationRequestStatus.Cancelled:
                    _result = 4;
                    break;
                case TerminationRequestStatus.Rejected:
                    _result = 5;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static bool GetIsCommentClose(YesNo _prmYesNo)
        {
            bool _result;

            switch (_prmYesNo)
            {
                case YesNo.No:
                    _result = false;
                    break;
                case YesNo.Yes:
                    _result = true;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static string GetIsCommentClose(bool _prmYesNo)
        {
            string _result;

            switch (_prmYesNo)
            {
                case false:
                    _result = "No";
                    break;
                case true:
                    _result = "Yes";
                    break;
                default:
                    _result = "No";
                    break;
            }

            return _result;
        }
    }
}