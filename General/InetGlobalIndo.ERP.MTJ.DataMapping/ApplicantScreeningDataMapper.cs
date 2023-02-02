using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ApplicantScreeningDataMapper
    {
        public static string GetStatusText(byte _prmScreeningStatus)
        {
            string _result = "";

            switch (_prmScreeningStatus)
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

        public static ScreeningStatus GetStatus(byte _prmAppScreeningStatus)
        {
            ScreeningStatus _result;

            switch (_prmAppScreeningStatus)
            {
                case 0:
                    _result = ScreeningStatus.Draft;
                    break;
                case 1:
                    _result = ScreeningStatus.WaitingForApproval;
                    break;
                case 2:
                    _result = ScreeningStatus.Approved;
                    break;
                default:
                    _result = ScreeningStatus.Draft;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(ScreeningStatus _prmScreeningStatus)
        {
            byte _result;

            switch (_prmScreeningStatus)
            {
                case ScreeningStatus.Draft:
                    _result = 0;
                    break;
                case ScreeningStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case ScreeningStatus.Approved:
                    _result = 2;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static string GetStatusTextDetail(bool _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case true:
                    _result = "Closed";
                    break;
                case false:
                    _result = "Open";
                    break;
                default:
                    _result = "Open";
                    break;
            }

            return _result;
        }

        public static bool GetStatusDetail(ScreeningStatusDt _prmScreeningStatusDt)
        {
            bool _result = false;

            switch (_prmScreeningStatusDt)
            {
                case ScreeningStatusDt.Closed:
                    _result = true;
                    break;
                case ScreeningStatusDt.Open:
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }
    }
}
