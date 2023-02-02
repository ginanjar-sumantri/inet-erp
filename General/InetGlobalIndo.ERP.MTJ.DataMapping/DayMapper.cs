using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class DayMapper
    {
        public static String GetDayName(int _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "Sunday";
                    break;
                case 1:
                    _result = "Monday";
                    break;
                case 2:
                    _result = "Tuesday";
                    break;
                case 3:
                    _result = "Wednesday";
                    break;
                case 4:
                    _result = "Thursday";
                    break;
                case 5:
                    _result = "Friday";
                    break;
                case 6:
                    _result = "Saturday";
                    break;
            }

            return _result;
        }

        public static int GetDayName(String _prmValue)
        {
            int _result = 0;

            switch (_prmValue)
            {
                case "Sunday":
                    _result = 0;
                    break;
                case "Monday":
                    _result = 1;
                    break;
                case "Tuesday":
                    _result = 2;
                    break;
                case "Wednesday":
                    _result = 3;
                    break;
                case "Thursday":
                    _result = 4;
                    break;
                case "Friday":
                    _result = 5;
                    break;
                case "Saturday":
                    _result = 6;
                    break;
            }

            return _result;
        }

        public static String GetDayNameINA(DayOfWeek _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case DayOfWeek.Sunday:
                    _result = "Minggu";
                    break;
                case DayOfWeek.Monday:
                    _result = "Senin";
                    break;
                case DayOfWeek.Tuesday:
                    _result = "Selasa";
                    break;
                case DayOfWeek.Wednesday:
                    _result = "Rabu";
                    break;
                case DayOfWeek.Thursday:
                    _result = "Kamis";
                    break;
                case DayOfWeek.Friday:
                    _result = "Jumat";
                    break;
                case DayOfWeek.Saturday:
                    _result = "Sabtu";
                    break;
            }

            return _result;
        }
    }
}