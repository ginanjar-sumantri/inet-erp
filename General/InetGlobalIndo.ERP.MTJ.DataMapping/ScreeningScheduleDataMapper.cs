using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ScreeningScheduleDataMapper
    {
        public static string GetStatusText(byte _prmScreeningScheduleStatus)
        {
            string _result = "";

            switch (_prmScreeningScheduleStatus)
            {
                case 0:
                    _result = "Draft";
                    break;
                case 1:
                    _result = "Waiting For Confirmation";
                    break;
                case 2:
                    _result = "Confirmed";
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

        public static ScreeningScheduleStatus GetStatus(byte _prmScreeningScheduleStatus)
        {
            ScreeningScheduleStatus _result;

            switch (_prmScreeningScheduleStatus)
            {
                case 0:
                    _result = ScreeningScheduleStatus.Draft;
                    break;
                case 1:
                    _result = ScreeningScheduleStatus.WaitingForConfirmation;
                    break;
                case 2:
                    _result = ScreeningScheduleStatus.Confirmed;
                    break;
                case 3:
                    _result = ScreeningScheduleStatus.Closed;
                    break;
                default:
                    _result = ScreeningScheduleStatus.Draft;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(ScreeningScheduleStatus _prmScreeningScheduleStatus)
        {
            byte _result;

            switch (_prmScreeningScheduleStatus)
            {
                case ScreeningScheduleStatus.Draft:
                    _result = 0;
                    break;
                case ScreeningScheduleStatus.WaitingForConfirmation:
                    _result = 1;
                    break;
                case ScreeningScheduleStatus.Confirmed:
                    _result = 2;
                    break;
                case ScreeningScheduleStatus.Closed:
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