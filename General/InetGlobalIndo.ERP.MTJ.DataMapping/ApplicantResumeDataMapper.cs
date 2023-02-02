using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ApplicantResumeDataMapper
    {
        public static string GetStatusText(byte _prmAppResumeStatus)
        {
            string _result = "";

            switch (_prmAppResumeStatus)
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
                default:
                    _result = "Draft";
                    break;
            }

            return _result;
        }

        public static AppResumeStatus GetStatus(byte _prmAppResumeStatus)
        {
            AppResumeStatus _result;

            switch (_prmAppResumeStatus)
            {
                case 0:
                    _result = AppResumeStatus.Draft;
                    break;
                case 1:
                    _result = AppResumeStatus.WaitingForApproval;
                    break;
                case 2:
                    _result = AppResumeStatus.Approved;
                    break;
                default:
                    _result = AppResumeStatus.Draft;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(AppResumeStatus _prmAppResumeStatus)
        {
            byte _result;

            switch (_prmAppResumeStatus)
            {
                case AppResumeStatus.Draft:
                    _result = 0;
                    break;
                case AppResumeStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case AppResumeStatus.Approved:
                    _result = 2;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }
    }
}
