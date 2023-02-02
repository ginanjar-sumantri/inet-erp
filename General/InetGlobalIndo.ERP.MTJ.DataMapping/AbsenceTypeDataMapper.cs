using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class AbsenceTypeDataMapper 
    {
        public static string IsFlagText(bool _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case false:
                    _result = "False";
                    break;
                case true:
                    _result = "True";
                    break;
                default:
                    _result = "False";
                    break;
            }

            return _result;
        }
    }
}