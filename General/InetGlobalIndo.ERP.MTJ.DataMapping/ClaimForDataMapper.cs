using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ClaimForDataMapper 
    {
        public static String GetClaimFor(ClaimForStatus _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case ClaimForStatus.All:
                    _result = "All";
                    break;
                case ClaimForStatus.Employee:
                    _result = "Employee";
                    break;
                case ClaimForStatus.Family:
                    _result = "Family";
                    break;
            }

            return _result;
        }
    }
}