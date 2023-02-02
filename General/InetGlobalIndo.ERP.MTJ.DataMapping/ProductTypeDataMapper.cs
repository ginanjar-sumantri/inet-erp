using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ProductTypeDataMapper
    {
        public static string UsingPG(Boolean _prmValue)
        {
            string _result = "No";

            switch (_prmValue)
            {
                case true:
                    _result = "Yes";
                    break;
                case false:
                    _result = "No";
                    break;
            }

            return _result;
        }

        public static char IsActive(YesNo _prmValue)
        {
            char _result = 'N';

            switch (_prmValue)
            {
                case YesNo.Yes:
                    _result = 'Y';
                    break;
                case YesNo.No:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static YesNo IsActive(char _prmValue)
        {
            YesNo _result;

            if (_prmValue == null)
                _result = YesNo.No;
            else
                _result = IsActive((char)_prmValue);

            return _result;
        }

        public static YesNo IsActive(char? _prmValue)
        {
            YesNo _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = YesNo.Yes;
                    break;
                case "n":
                    _result = YesNo.No;
                    break;
                default:
                    _result = YesNo.No;
                    break;
            }

            return _result;
        }

        public static bool IsTrue(char? _prmValue)
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

        public static string GetActiveText(char? _prmValue)
        {
            string _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = "Yes";
                    break;
                case "n":
                    _result = "No";
                    break;
                default:
                    _result = "No";
                    break;
            }

            return _result;
        }
    }
}