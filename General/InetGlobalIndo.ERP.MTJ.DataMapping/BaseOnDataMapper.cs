using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class BaseOnDataMapper
    {
        public static String GetBaseOnText(BaseOn _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case BaseOn.Salary:
                    _result = "Salary";
                    break;
                case BaseOn.Nominal:
                    _result = "Nominal";
                    break;
            }

            return _result;
        }
    }
}