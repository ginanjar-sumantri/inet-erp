using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class TaxDataMapper
    {
        public static string GetStatusTaxTransactionCode(TaxTransactionCode _prmValue)
        {
            string _result;

            switch (_prmValue)
            {
                case TaxTransactionCode.One:
                    _result = "01";
                    break;
                case TaxTransactionCode.Two:
                    _result = "02";
                    break;
                case TaxTransactionCode.Three:
                    _result = "03";
                    break;
                case TaxTransactionCode.Four:
                    _result = "04";
                    break;
                case TaxTransactionCode.Five:
                    _result = "05";
                    break;
                case TaxTransactionCode.Six:
                    _result = "06";
                    break;
                case TaxTransactionCode.Seven:
                    _result = "07";
                    break;
                case TaxTransactionCode.Eight:
                    _result = "08";
                    break;
                case TaxTransactionCode.Nine:
                    _result = "09";
                    break;
                default:
                    _result = "01";
                    break;
            }

            return _result;
        }

        public static char GetStatusTaxStatus(TaxStatus _prmValue)
        {
            char _result;

            switch (_prmValue)
            {
                case TaxStatus.Zero:
                    _result = '0';
                    break;
                case TaxStatus.One:
                    _result = '1';
                    break;
                default:
                    _result = '0';
                    break;
            }

            return _result;
        }
    }
}