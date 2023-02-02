using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class GiroPaymentDataMapper
    {
        public static string GetStatusText(char _prmGiroPaymentStatus)
        {
            string _result = "";

            switch (_prmGiroPaymentStatus)
            {
                case 'H':
                    _result = "Hold";
                    break;
                case 'C':
                    _result = "Cancel";
                    break;
                case 'D':
                    _result = "Drawn";
                    break;
                case 'E':
                    _result = "Change";
                    break;
                default:
                    _result = "Hold";
                    break;
            }

            return _result;
        }

        public static char GetStatus(GiroPaymentStatus _prmGiroPaymentStatus)
        {
            char _result;

            switch (_prmGiroPaymentStatus)
            {
                case GiroPaymentStatus.OnHold:
                    _result = 'H';
                    break;
                case GiroPaymentStatus.Cancelled:
                    _result = 'C';
                    break;
                case GiroPaymentStatus.Drawn:
                    _result = 'D';
                    break;
                case GiroPaymentStatus.Change:
                    _result = 'E';
                    break;
                default:
                    _result = 'H';
                    break;
            }

            return _result;
        }
    }
}