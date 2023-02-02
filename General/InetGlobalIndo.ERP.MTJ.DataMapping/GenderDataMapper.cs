using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class GenderDataMapper 
    {
        public static String GetGender(Byte _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "Male";
                    break;
                case 1:
                    _result = "Female";
                    break;
                default:
                    _result = "Male";
                    break;
            }

            return _result;
        }

        public static Byte GetGender(String _prmValue)
        {
            Byte _result = 0;

            switch (_prmValue)
            {
                case "Male":
                    _result = 0;
                    break;
                case "Female":
                    _result = 1;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }
    }
}