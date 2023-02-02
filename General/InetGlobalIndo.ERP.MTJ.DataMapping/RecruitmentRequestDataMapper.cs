using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class RecruitmentRequestDataMapper
    {
        public static string GetStatusText(byte _prmRecruitmentRequestStatus)
        {
            string _result = "";

            switch (_prmRecruitmentRequestStatus)
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

        public static RecruitmentRequestStatus GetStatus(byte _prmRecruitmentRequestStatus)
        {
            RecruitmentRequestStatus _result;

            switch (_prmRecruitmentRequestStatus)
            {
                case 0:
                    _result = RecruitmentRequestStatus.Draft;
                    break;
                case 1:
                    _result = RecruitmentRequestStatus.WaitingForApproval;
                    break;
                case 2:
                    _result = RecruitmentRequestStatus.Approved;
                    break;
                case 3:
                    _result = RecruitmentRequestStatus.Closed;
                    break;
                default:
                    _result = RecruitmentRequestStatus.Draft;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(RecruitmentRequestStatus _prmRecruitmentRequestStatus)
        {
            byte _result;

            switch (_prmRecruitmentRequestStatus)
            {
                case RecruitmentRequestStatus.Draft:
                    _result = 0;
                    break;
                case RecruitmentRequestStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case RecruitmentRequestStatus.Approved:
                    _result = 2;
                    break;
                case RecruitmentRequestStatus.Closed:
                    _result = 3;
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