using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ExitInterviewDataMapper
    {
        public static string GetStatusText(byte _prmExitInterviewStatus)
        {
            string _result = "";

            switch (_prmExitInterviewStatus)
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
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }

        public static ExitInterviewStatus GetStatus(byte _prmExitInterviewStatus)
        {
            ExitInterviewStatus _result;

            switch (_prmExitInterviewStatus)
            {
                case 0:
                    _result = ExitInterviewStatus.Draft;
                    break;
                case 1:
                    _result = ExitInterviewStatus.WaitingForApproval;
                    break;
                case 2:
                    _result = ExitInterviewStatus.Approved;
                    break;
                default:
                    _result = ExitInterviewStatus.Draft;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(ExitInterviewStatus _prmExitInterviewStatus)
        {
            byte _result;

            switch (_prmExitInterviewStatus)
            {
                case ExitInterviewStatus.Draft:
                    _result = 0;
                    break;
                case ExitInterviewStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case ExitInterviewStatus.Approved:
                    _result = 2;
                    break;
                default:
                    _result = 0;
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