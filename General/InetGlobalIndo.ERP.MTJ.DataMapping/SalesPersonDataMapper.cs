using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class SalesPersonDataMapper 
    {
        public static string IsActive(Boolean _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case true:
                    _result = "Active";
                    break;
                case false:
                    _result = "Not Active";
                    break;
            }
            return _result;
        }

        public static Boolean IsActive(string _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue)
            {
                case "Active":
                    _result = true;
                    break;
                case "Not Active":
                    _result = false;
                    break;
            }
            return _result;
        }
    }
}