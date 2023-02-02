using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class PriceGroupDataMapper
    {
        public static String IsActive(Boolean _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case true:
                    _result = "Active";
                    break;
                case false:
                    _result = "Inactive";
                    break;
            }

            return _result;
        }

        public static Boolean IsActive(String _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue)
            {
                case "Active":
                    _result = true;
                    break;
                case "Inactive":
                    _result = false;
                    break;
            }

            return _result;
        }

    }
}