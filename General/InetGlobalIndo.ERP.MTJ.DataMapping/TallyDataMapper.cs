using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class TallyDataMapper
    {
        public static byte GetStatus(TallyStatus _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case TallyStatus.OnHold:
                    _result = 0;
                    break;
                case TallyStatus.InProgress:
                    _result = 1;
                    break;
                case TallyStatus.Cancelled:
                    _result = 2;
                    break;
                case TallyStatus.Done:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static TallyStatus GetStatus(byte _prmValue)
        {
            TallyStatus _result;

            switch (_prmValue)
            {
                case 0:
                    _result = TallyStatus.OnHold;
                    break;
                case 1:
                    _result = TallyStatus.InProgress;
                    break;
                case 2:
                    _result = TallyStatus.Cancelled;
                    break;
                case 3:
                    _result = TallyStatus.Done;
                    break;
                default:
                    _result = TallyStatus.OnHold;
                    break;
            }

            return _result;
        }

        public static string GetStatusText(byte _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "On Hold";
                    break;
                case 1:
                    _result = "In Progress";
                    break;
                case 2:
                    _result = "Cancelled";
                    break;
                case 3:
                    _result = "Done";
                    break;
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }

        public static int GetTimeZone(string _prmValue)
        {
            int _result = 0;

            switch (_prmValue)
            {
                case "0":
                    _result = 0;
                    break;
                case "1":
                    _result = 1;
                    break;
                case "2":
                    _result = 2;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static string GetTimeZone(int _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "0";
                    break;
                case 1:
                    _result = "1";
                    break;
                case 2:
                    _result = "2";
                    break;
                default:
                    _result = "0";
                    break;
            }

            return _result;
        }

        public static string GetTimeZoneText(int _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "WIB";
                    break;
                case 1:
                    _result = "WITA";
                    break;
                case 2:
                    _result = "WIT";
                    break;
                default:
                    _result = "WIB";
                    break;
            }

            return _result;
        }
    }
}
