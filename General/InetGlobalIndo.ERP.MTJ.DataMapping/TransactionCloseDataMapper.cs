using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class TransactionCloseDataMapper
    {
        public static byte IsLocked(TransCloseStatus _prmValue)
        {
            byte _result;

            switch (_prmValue)
            {
                case TransCloseStatus.Locked:
                    _result = 1;
                    break;
                case TransCloseStatus.Open:
                    _result = 0;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static string GetStatusTransCloseStatus(int _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "Open";
                    break;
                case 1:
                    _result = "Locked";
                    break;
                default:
                    _result = "Open";
                    break;
            }

            return _result;
        }

        public static int GetTransCloseStatus(bool _prmValue)
        {
            int _result = 0;

            switch (_prmValue)
            {
                case false:
                    _result = 0;
                    break;
                case true:
                    _result = 1;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static bool GetTransCloseStatusBool(int _prmValue)
        {
            bool _result = false;

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

        public static int GetTransCloseStatusForTrans(TransCloseStatus _prmValue)
        {
            int _result = 0;

            switch (_prmValue)
            {
                case TransCloseStatus.Open:
                    _result = 0;
                    break;
                case TransCloseStatus.Locked:
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