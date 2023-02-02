using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTSWeb.Enum;

namespace VTSWeb.DataMapping
{
    public static class ClearanceCompleteDataMapping
    {
        public static String GetStatus(Byte _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "No Complete";
                    break;
                case 1:
                    _result = "Complete";
                    break;
                default:
                    _result = "No Complete";
                    break;
            }

            return _result;
        }

        public static Byte GetStatus(ClearanceStatus _prmValue)
        {
            Byte _result = 0;

            switch (_prmValue)
            {
                case ClearanceStatus.NoComplete:
                    _result = 0;
                    break;
                case ClearanceStatus.Complete:
                    _result = 1;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }
    }
}
