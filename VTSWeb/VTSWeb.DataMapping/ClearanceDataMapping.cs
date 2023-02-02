using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTSWeb.Enum;

namespace VTSweb.DataMapping
{
    public static class ClearanceDataMapping
    {
        public static Byte GetCompleteStatus(Boolean _prmValue)
        {
            Byte _result = 0;

            switch (_prmValue)
            {
                case true:
                    _result = 1;
                    break;
                case false:
                    _result = 0;
                    break;
                default:
                    _result = 0;
                    break;
            }
            return _result;
        }

        public static Boolean GetCompleteStatus(Byte _prmValue)
        {
            Boolean _result = false;
            switch (_prmValue)
            {
                case 0:
                    _result = false;
                    break;
                case 1:
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
