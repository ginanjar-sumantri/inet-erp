using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class MonthMapper
    {
        public static String GetMonthName(int _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 1:
                    _result = "January";
                    break;
                case 2:
                    _result = "February";
                    break;
                case 3:
                    _result = "March";
                    break;
                case 4:
                    _result = "April";
                    break;
                case 5:
                    _result = "May";
                    break;
                case 6:
                    _result = "June";
                    break;
                case 7:
                    _result = "July";
                    break;
                case 8:
                    _result = "August";
                    break;
                case 9:
                    _result = "September";
                    break;
                case 10:
                    _result = "October";
                    break;
                case 11:
                    _result = "November";
                    break;
                case 12:
                    _result = "December";
                    break;
            }

            return _result;
        }

        public static String GetMonthName3Letter(int _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 1:
                    _result = "JAN";
                    break;
                case 2:
                    _result = "FEB";
                    break;
                case 3:
                    _result = "MAR";
                    break;
                case 4:
                    _result = "APR";
                    break;
                case 5:
                    _result = "MAY";
                    break;
                case 6:
                    _result = "JUN";
                    break;
                case 7:
                    _result = "JUL";
                    break;
                case 8:
                    _result = "AUG";
                    break;
                case 9:
                    _result = "SEP";
                    break;
                case 10:
                    _result = "OCT";
                    break;
                case 11:
                    _result = "NOV";
                    break;
                case 12:
                    _result = "DEC";
                    break;
            }

            return _result;
        }

        public static String GetMonthNameINA(int _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 1:
                    _result = "Januari";
                    break;
                case 2:
                    _result = "Februari";
                    break;
                case 3:
                    _result = "Maret";
                    break;
                case 4:
                    _result = "April";
                    break;
                case 5:
                    _result = "Mei";
                    break;
                case 6:
                    _result = "Juni";
                    break;
                case 7:
                    _result = "Juli";
                    break;
                case 8:
                    _result = "Agustus";
                    break;
                case 9:
                    _result = "September";
                    break;
                case 10:
                    _result = "Oktober";
                    break;
                case 11:
                    _result = "November";
                    break;
                case 12:
                    _result = "Desember";
                    break;
            }

            return _result;
        }
    }
}