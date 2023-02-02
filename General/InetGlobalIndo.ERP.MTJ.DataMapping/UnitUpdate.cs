using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class UnitUpdate 
    {
        public static bool IsUpdate(char _prmValue)
        {
            bool _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static char IsUpdate(bool _prmValue)
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
    }
}