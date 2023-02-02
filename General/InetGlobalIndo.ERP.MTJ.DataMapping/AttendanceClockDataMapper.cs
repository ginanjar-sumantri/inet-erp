using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class AttendanceClockDataMapper
    {
        public static string GetStatusText(char _prmAttendanceClockStatus)
        {
            string _result = "";

            switch (_prmAttendanceClockStatus)
            {
                case 'A':
                    _result = "Automatic";
                    break;
                case 'M':
                    _result = "Manual";
                    break;
            }

            return _result;
        }

        public static AttendanceClockStatus GetStatus(char _prmAttendanceClockStatus)
        {
            AttendanceClockStatus _result = AttendanceClockStatus.Automatic;

            switch (_prmAttendanceClockStatus)
            {
                case 'A':
                    _result = AttendanceClockStatus.Automatic;
                    break;
                case 'M':
                    _result = AttendanceClockStatus.Manual;
                    break;
            }

            return _result;
        }

        public static Char GetStatus(AttendanceClockStatus _prmAttendanceClockStatus)
        {
            Char _result = ' ';

            switch (_prmAttendanceClockStatus)
            {
                case AttendanceClockStatus.Automatic:
                    _result = 'A';
                    break;
                case AttendanceClockStatus.Manual:
                    _result = 'M';
                    break;
            }

            return _result;
        }
    }
}
