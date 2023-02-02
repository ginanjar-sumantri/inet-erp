using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class EmployeeAuthorDataMapper 
    {
        public static string IsSetup(bool _prmValue)
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
    }
}