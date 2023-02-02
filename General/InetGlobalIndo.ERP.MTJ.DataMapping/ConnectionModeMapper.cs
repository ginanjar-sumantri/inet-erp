using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ConnectionModeMapper
    {
        public static ConnectionType MapThis(byte _prmConnMode)
        {
            ConnectionType _result = ConnectionType.Development;

            switch (_prmConnMode)
            {
                case 0:
                    _result = ConnectionType.Development;
                    break;
                case 1:
                    _result = ConnectionType.Testing;
                    break;
                case 2:
                    _result = ConnectionType.Production;
                    break;
                default:
                    _result = ConnectionType.Development;
                    break;
            }

            return _result;
        }

        public static String GetLabel(byte _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "{Development}";
                    break;
                case 1:
                    _result = "{Testing}";
                    break;
                case 2:
                    _result = "{Production}";
                    break;
                default:
                    _result = "{Development}";
                    break;
            }

            return _result;
        }

        public static String GetLabel(ConnectionType _prmConnMode)
        {
            String _result = "";

            switch (_prmConnMode)
            {
                case ConnectionType.Development:
                    _result = "{Development}";
                    break;
                case ConnectionType.Testing:
                    _result = "{Testing}";
                    break;
                case ConnectionType.Production:
                    _result = "{Production}";
                    break;
                default:
                    _result = "{Development}";
                    break;
            }

            return _result;
        }

        public static byte MapThis(ConnectionType _prmConnMode)
        {
            byte _result = 0;

            switch (_prmConnMode)
            {
                case ConnectionType.Development:
                    _result = 0;
                    break;
                case ConnectionType.Testing:
                    _result = 1;
                    break;
                case ConnectionType.Production:
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