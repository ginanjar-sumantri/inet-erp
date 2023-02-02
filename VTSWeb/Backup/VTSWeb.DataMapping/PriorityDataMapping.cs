using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTSWeb.Enum;

namespace VTSWeb.DataMapping
{

    public static class PriorityDataMapping
    {
        public static String GetPriority(Byte _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "Low";
                    break;
                case 1:
                    _result = "Medium";
                    break;
                case 2:
                    _result = "High";
                    break;
                default:
                    _result = "Low";
                    break;
            }

            return _result;
        }

        public static Byte GetStatus(AssignmentPriority _prmValue)
        {
            Byte _result = 0;

            switch (_prmValue)
            {
                case AssignmentPriority.Low:
                    _result = 0;
                    break;
                case AssignmentPriority.Medium:
                    _result = 1;
                    break;
                case AssignmentPriority.High:
                    _result = 2;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }
    }
}