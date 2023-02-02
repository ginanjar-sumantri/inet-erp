using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class CurrencyDefaultAccount
    {
        public static byte CurrDefAcc(Common.Enum.CurrDefAcc _prmValue)
        {
            byte _result;

            switch (_prmValue)
            {
                case Common.Enum.CurrDefAcc.AccountPayable:
                    _result = 0;
                    break;
                case Common.Enum.CurrDefAcc.AccountReceiveable:
                    _result = 1;
                    break;
                case Common.Enum.CurrDefAcc.RealizedGainOrLoss:
                    _result = 3;
                    break;
                case Common.Enum.CurrDefAcc.SalesDiscount:
                    _result = 2;
                    break;
                case Common.Enum.CurrDefAcc.UnRealizedGainOrLoss:
                    _result = 4;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static Common.Enum.CurrDefAcc CurrDefAcc(byte? _prmValue)
        {
            Common.Enum.CurrDefAcc _result;

            switch (_prmValue)
            {
                case 0:
                    _result = Common.Enum.CurrDefAcc.AccountPayable;
                    break;
                case 1:
                    _result = Common.Enum.CurrDefAcc.AccountReceiveable;
                    break;
                case 2:
                    _result = Common.Enum.CurrDefAcc.SalesDiscount;
                    break;
                case 3:
                    _result = Common.Enum.CurrDefAcc.RealizedGainOrLoss;
                    break;
                case 4:
                    _result = Common.Enum.CurrDefAcc.UnRealizedGainOrLoss;
                    break;
                default:
                    _result = Common.Enum.CurrDefAcc.AccountPayable;
                    break;
            }

            return _result;
        }
    }
}