using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class EmployeeDataMapper
    {
        public static Boolean IsHaveLeave(char _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsHaveLeave(Boolean _prmValue)
        {
            char _result;

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static Boolean IsOperator(char _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsOperator(Boolean _prmValue)
        {
            char _result;

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static Boolean IsDriver(char _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsDriver(Boolean _prmValue)
        {
            char _result;

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static Boolean IsTeknisi(char _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsTeknisi(Boolean _prmValue)
        {
            char _result;

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static Boolean IsPPh21(char _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsPPh21(Boolean _prmValue)
        {
            char _result;

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static Boolean IsSales(char _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsSales(Boolean _prmValue)
        {
            char _result;

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static Boolean IsActive(char _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsActive(Boolean _prmValue)
        {
            char _result;

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static Boolean GetGender(string _prmGender)
        {
            Boolean _result = false;

            switch (_prmGender.ToLower())
            {
                case "male":
                    _result = true;
                    break;
                case "female":
                    _result = false;
                    break;
            }

            return _result;
        }

        public static string GetGender(Boolean _prmGender)
        {
            string _result = "";

            switch (_prmGender)
            {
                case true:
                    _result = "Male";
                    break;
                case false:
                    _result = "Female";
                    break;
            }

            return _result;
        }

        public static Boolean IsPrimary(String _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue.ToString().ToLower())
            {
                case "Main":
                    _result = true;
                    break;
                case "Sub Main":
                    _result = false;
                    break;
            }

            return _result;
        }

        public static String IsPrimary(Boolean _prmValue)
        {
            String _result;

            switch (_prmValue)
            {
                case true:
                    _result = "Main";
                    break;
                case false:
                    _result = "Sub Main";
                    break;
                default:
                    _result = "Sub Main";
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