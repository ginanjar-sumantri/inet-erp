using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTSWeb.Enum;
using VTSWeb.SystemConfig;

namespace VTSWeb.DataMapping
{
    public static class DateFormMapping
    {
        private static DateForm _dateForm = DateFormMapping.GetFormat(ApplicationConfig.DateForm);

        private static DateForm GetFormat(String _prmDateForm)
        {
            DateForm _result = DateForm.SQL;

            switch (_prmDateForm.Trim())
            {
                case "0":
                    _result = DateForm.SQL;
                    break;
                case "1":
                    _result = DateForm.English;
                    break;
                case "2":
                    _result = DateForm.Indonesian;
                    break;
            }

            return _result;
        }

        public static String GetValue(DateTime? _prmValue)
        {
            String _result = "";

            if (_prmValue != null)
            {
                _result = DateFormMapping.GetValue((DateTime)_prmValue);
            }

            return _result;
        }

        public static String GetValue(DateTime _prmValue)
        {
            String _result = "";

            String sYear = _prmValue.Year.ToString();
            String sMonth = _prmValue.Month.ToString().PadLeft(2, '0');
            String sDay = _prmValue.Day.ToString().PadLeft(2, '0');

            switch (_dateForm)
            {
                case DateForm.English: //NB: MM-DD-YYYY
                    _result = sMonth + "-" + sDay + "-" + sYear;
                    break;
                case DateForm.Indonesian: //NB: DD-MM-YYYY
                    _result = sDay + "-" + sMonth + "-" + sYear;
                    break;
                case DateForm.SQL: //NB: YYYY-MM-DD
                    _result = sYear + "-" + sMonth + "-" + sDay;
                    break;
            }

            return _result;
        }

        public static DateTime GetValue(string _prmValue)
        {
            DateTime _result = new DateTime();

            string[] date = _prmValue.Split('-');

            switch (_dateForm)
            {
                case DateForm.English: //NB: MM-DD-YYYY
                    _result = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[0]), Convert.ToInt32(date[1]));
                    break;
                case DateForm.Indonesian: //NB: DD-MM-YYYY
                    _result = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]));
                    break;
                case DateForm.SQL: //NB: YYYY-MM-DD
                    _result = new DateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2]));
                    break;
            }

            return _result;
        }
    }
}