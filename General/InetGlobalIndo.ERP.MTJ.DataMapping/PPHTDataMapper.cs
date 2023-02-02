using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class PPHTDataMapper
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

        public static byte GetStatus(PPHTStatus _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case PPHTStatus.OnHold:
                    _result = 0;
                    break;
                case PPHTStatus.InProgress:
                    _result = 1;
                    break;
                case PPHTStatus.Cancelled:
                    _result = 2;
                    break;
                case PPHTStatus.Done:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static PPHTStatus GetStatus(byte _prmValue)
        {
            PPHTStatus _result;

            switch (_prmValue)
            {
                case 0:
                    _result = PPHTStatus.OnHold;
                    break;
                case 1:
                    _result = PPHTStatus.InProgress;
                    break;
                case 2:
                    _result = PPHTStatus.Cancelled;
                    break;
                case 3:
                    _result = PPHTStatus.Done;
                    break;
                default:
                    _result = PPHTStatus.OnHold;
                    break;
            }

            return _result;
        }
    }
}