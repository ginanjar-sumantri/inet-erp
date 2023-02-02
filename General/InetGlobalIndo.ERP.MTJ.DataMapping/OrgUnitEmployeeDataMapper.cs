using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class OrgUnitEmployeeDataMapper
    {
        public static string GetPICStatusString(bool _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case false:
                    _result = "No";
                    break;
                case true:
                    _result = "Yes";
                    break;
                default:
                    _result = "No";
                    break;
            }

            return _result;
        }

        public static bool GetPICStatusBool(IsPIC _prmValue)
        {
            bool _result;

            switch (_prmValue)
            {
                case IsPIC.No:
                    _result = false;
                    break;
                case IsPIC.Yes:
                    _result = true;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }
    }
}
