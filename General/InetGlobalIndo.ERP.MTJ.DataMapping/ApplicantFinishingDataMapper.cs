using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ApplicantFinishingDataMapper
    {
        public static string GetStatusText(byte _prmFinishingStatus)
        {
            string _result = "";

            switch (_prmFinishingStatus)
            {
                case 0:
                    _result = "Draft";
                    break;
                case 1:
                    _result = "Waiting For Approval";
                    break;
                case 2:
                    _result = "Approved";
                    break;
                case 3:
                    _result = "Closed";
                    break;
                default:
                    _result = "Draft";
                    break;
            }

            return _result;
        }

        public static ApplicantFinishingStatus GetStatus(byte _prmAppFinishingStatus)
        {
            ApplicantFinishingStatus _result;

            switch (_prmAppFinishingStatus)
            {
                case 0:
                    _result = ApplicantFinishingStatus.Draft;
                    break;
                case 1:
                    _result = ApplicantFinishingStatus.WaitingForApproval;
                    break;
                case 2:
                    _result = ApplicantFinishingStatus.Approved;
                    break;
                case 3:
                    _result = ApplicantFinishingStatus.Closed;
                    break;
                default:
                    _result = ApplicantFinishingStatus.Draft;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(ApplicantFinishingStatus _prmFinishingStatus)
        {
            byte _result;

            switch (_prmFinishingStatus)
            {
                case ApplicantFinishingStatus.Draft:
                    _result = 0;
                    break;
                case ApplicantFinishingStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case ApplicantFinishingStatus.Approved:
                    _result = 2;
                    break;
                case ApplicantFinishingStatus.Closed:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }
    }
}
