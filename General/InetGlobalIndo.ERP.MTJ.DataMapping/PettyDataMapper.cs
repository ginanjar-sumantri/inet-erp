using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class PettyDataMapper
    {
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

        public static string GetType(byte _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "Receipt";
                    break;
                case 1:
                    _result = "Payment";
                    break;
            }

            return _result;
        }

        public static byte GetType(PettyType _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case PettyType.Receipt:
                    _result = 0;
                    break;
                case PettyType.Payment:
                    _result = 1;
                    break;
            }

            return _result;
        }
    }
}