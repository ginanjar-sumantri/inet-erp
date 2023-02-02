using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class AttendanceDailyDataMapper
    {
        public static string GetStatusText(char _prmAttendanceDailyStatus)
        {
            string _result = "";

            switch (_prmAttendanceDailyStatus)
            {
                case 'A':
                    _result = "Automatic";
                    break;
                case 'M':
                    _result = "Manual / Change";
                    break;
                case 'T':
                    _result = "Transfer";
                    break;
            }

            return _result;
        }

        public static AttendanceDailyStatus GetStatus(char _prmAttendanceDailyStatus)
        {
            AttendanceDailyStatus _result = AttendanceDailyStatus.Automatic;

            switch (_prmAttendanceDailyStatus)
            {
                case 'A':
                    _result = AttendanceDailyStatus.Automatic;
                    break;
                case 'M':
                    _result = AttendanceDailyStatus.ManualChange;
                    break;
                case 'T':
                    _result = AttendanceDailyStatus.Transfer;
                    break;
            }

            return _result;
        }

        public static Char GetStatus(AttendanceDailyStatus _prmAttendanceDailyStatus)
        {
            Char _result = ' ';

            switch (_prmAttendanceDailyStatus)
            {
                case AttendanceDailyStatus.Automatic:
                    _result = 'A';
                    break;
                case AttendanceDailyStatus.ManualChange:
                    _result = 'M';
                    break;
                case AttendanceDailyStatus.Transfer:
                    _result = 'T';
                    break;
            }

            return _result;
        }
    }
}
