using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class PackingListDataMapper
    {
        public static string GetStatusText(byte _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "OnHold";
                    break;
                case 1:
                    _result = "InProgress";
                    break;
                case 2:
                    _result = "Cancelled";
                    break;
                case 3:
                    _result = "Done";
                    break;
                default:
                    _result = "OnHold";
                    break;
            }

            return _result;
        }

        public static byte GetStatus(PackingListStatus _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case PackingListStatus.OnHold:
                    _result = 0;
                    break;
                case PackingListStatus.InProgress:
                    _result = 1;
                    break;
                case PackingListStatus.Cancelled:
                    _result = 2;
                    break;
                case PackingListStatus.Done:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static PackingListStatus GetStatus(byte _prmValue)
        {
            PackingListStatus _result;

            switch (_prmValue)
            {
                case 0:
                    _result = PackingListStatus.OnHold;
                    break;
                case 1:
                    _result = PackingListStatus.InProgress;
                    break;
                case 2:
                    _result = PackingListStatus.Cancelled;
                    break;
                case 3:
                    _result = PackingListStatus.Done;
                    break;
                default:
                    _result = PackingListStatus.OnHold;
                    break;
            }

            return _result;
        }
    }
}
