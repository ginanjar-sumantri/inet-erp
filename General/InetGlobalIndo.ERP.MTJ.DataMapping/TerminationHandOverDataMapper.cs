using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class TerminationHandOverDataMapper
    {
        public static string GetStatusText(byte _prmTerminationHandOverStatus)
        {
            string _result = "";

            switch (_prmTerminationHandOverStatus)
            {
                case 0:
                    _result = "Opened";
                    break;
                case 1:
                    _result = "On Progress";
                    break;
                case 2:
                    _result = "Closed";
                    break;
                default:
                    _result = "Opened";
                    break;
            }

            return _result;
        }

        public static string GetStatusText(TerminationHandOverStatus _prmTerminationHandOverStatus)
        {
            string _result = "";

            switch (_prmTerminationHandOverStatus)
            {
                case TerminationHandOverStatus.Open:
                    _result = "Opened";
                    break;
                case TerminationHandOverStatus.OnProgress:
                    _result = "On Progress";
                    break;
                case TerminationHandOverStatus.Closed:
                    _result = "Closed";
                    break;
                default:
                    _result = "Opened";
                    break;
            }

            return _result;
        }

        public static TerminationHandOverStatus GetStatus(byte _prmTerminationHandOverStatus)
        {
            TerminationHandOverStatus _result;

            switch (_prmTerminationHandOverStatus)
            {
                case 0:
                    _result = TerminationHandOverStatus.Open;
                    break;
                case 1:
                    _result = TerminationHandOverStatus.OnProgress;
                    break;
                case 2:
                    _result = TerminationHandOverStatus.Closed;
                    break;
                default:
                    _result = TerminationHandOverStatus.Open;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(TerminationHandOverStatus _prmTerminationHandOverStatus)
        {
            byte _result;

            switch (_prmTerminationHandOverStatus)
            {
                case TerminationHandOverStatus.Open:
                    _result = 0;
                    break;
                case TerminationHandOverStatus.OnProgress:
                    _result = 1;
                    break;
                case TerminationHandOverStatus.Closed:
                    _result = 2;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        //public static char GetYesNo(YesNo _prmYesNo)
        //{
        //    char _result;

        //    switch (_prmYesNo)
        //    {
        //        case YesNo.Yes:
        //            _result = 'Y';
        //            break;
        //        case YesNo.No:
        //            _result = 'N';
        //            break;
        //        default:
        //            _result = 'N';
        //            break;
        //    }

        //    return _result;
        //}
    }
}