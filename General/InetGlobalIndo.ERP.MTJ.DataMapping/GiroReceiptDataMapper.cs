using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class GiroReceiptDataMapper
    {
        public static string GetStatusText(char _prmGiroReceiptStatus)
        {
            string _result = "";

            switch (_prmGiroReceiptStatus)
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
                case 'S':
                    _result = "Setor";
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

        public static GiroReceiptStatus GetStatus(char _prmGiroReceiptStatus)
        {
            GiroReceiptStatus _result;

            switch (_prmGiroReceiptStatus)
            {
                case 'H':
                    _result = GiroReceiptStatus.OnHold;
                    break;
                case 'C':
                    _result = GiroReceiptStatus.Cancelled;
                    break;
                case 'D':
                    _result = GiroReceiptStatus.Drawn;
                    break;
                case 'S':
                    _result = GiroReceiptStatus.Deposit;
                    break;
                case 'E':
                    _result = GiroReceiptStatus.Change;
                    break;
                default:
                    _result = GiroReceiptStatus.OnHold;
                    break;
            }

            return _result;
        }

        public static char GetStatus(GiroReceiptStatus _prmGiroReceiptStatus)
        {
            char _result;

            switch (_prmGiroReceiptStatus)
            {
                case GiroReceiptStatus.OnHold:
                    _result = 'H';
                    break;
                case GiroReceiptStatus.Cancelled:
                    _result = 'C';
                    break;
                case GiroReceiptStatus.Drawn:
                    _result = 'D';
                    break;
                case GiroReceiptStatus.Deposit:
                    _result = 'S';
                    break;
                case GiroReceiptStatus.Change:
                    _result = 'E';
                    break;
                default:
                    _result = 'H';
                    break;
            }

            return _result;
        }

        public static char GetYesNo(YesNo _prmYesNo)
        {
            char _result;

            switch (_prmYesNo)
            {
                case YesNo.Yes:
                    _result = 'Y';
                    break;
                case YesNo.No:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }
    }
}