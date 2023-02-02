using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class MethodRangeTypeDataMapper 
    {
        public static String GetMethodRangeText(MethodRangeType _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case MethodRangeType.Weekly:
                    _result = "Weekly";
                    break;
                case MethodRangeType.Monthly:
                    _result = "Monthly";
                    break;
                case MethodRangeType.Biweekly:
                    _result = "Biweekly";
                    break;
            }

            return _result;
        }

        public static String GetMethodRangeText(String _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case "1WEEK":
                    _result = "Weekly";
                    break;
                case "1MONTH":
                    _result = "Monthly";
                    break;
                case "HMONTH":
                    _result = "Biweekly";
                    break;
            }

            return _result;
        }

        public static String GetMethodRangeValue(MethodRangeType _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case MethodRangeType.Weekly:
                    _result = "1WEEK";
                    break;
                case MethodRangeType.Monthly:
                    _result = "1MONTH";
                    break;
                case MethodRangeType.Biweekly:
                    _result = "HMONTH";
                    break;
            }

            return _result;
        }
    }
}