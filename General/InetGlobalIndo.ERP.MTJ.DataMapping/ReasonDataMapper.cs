using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ReasonDataMapper
    {
        public static Boolean IsActive(string _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue.ToString().ToLower())
            {
                case "Yes":
                    _result = true;
                    break;
                case "No":
                    _result = false;
                    break;
            }

            return _result;
        }

        public static String IsActive(Boolean _prmValue)
        {
            String _result="";

            switch (_prmValue)
            {
                case true:
                    _result = "Yes";
                    break;
                case false:
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