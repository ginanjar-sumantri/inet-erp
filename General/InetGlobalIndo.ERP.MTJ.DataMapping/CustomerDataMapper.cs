using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class CustomerDataMapper
    {
        public static char IsLimit(Boolean _prmValue)
        {
            char _result = 'N';

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static Boolean IsLimit(char _prmValue)
        {
            Boolean _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
                case null:
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsPPN(Boolean _prmValue)
        {
            char _result = 'N';

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static Boolean IsPPN(char _prmValue)
        {
            Boolean _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
                case null:
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char? IsActiveText(string _prmValue)
        {
            char? _result = 'N';

            switch (_prmValue)
            {
                case "Active":
                    _result = 'Y';
                    break;
                case "Inactive":
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static string IsActiveText(char? _prmValue)
        {
            string _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = "Active";
                    break;
                case "n":
                    _result = "Inactive";
                    break;
                case null:
                    _result = "Inactive";
                    break;
                default:
                    _result = "Inactive";
                    break;
            }

            return _result;
        }

        public static char? IsActive(Boolean _prmValue)
        {
            char? _result = 'N';

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static Boolean IsActive(char? _prmValue)
        {
            Boolean _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
                case null:
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsFP(Boolean? _prmValue)
        {
            char _result = 'N';

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static Boolean? IsFP(char _prmValue)
        {
            Boolean? _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
                case null:
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char GetYesNo(YesNo _prmYesNo)
        {
            char _result;

            switch (_prmYesNo)
            {
                case YesNo.Yes:
                    _result = 'Y';
                    break;
                case YesNo.No:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static bool IsChecked(char _prmValue)
        {
            bool _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsChecked(bool _prmValue)
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
    }
}