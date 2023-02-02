using System;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class AccountMapper
    {
        public static String GetValue(AccountType _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case AccountType.BalanceSheet:
                    _result = "BS";
                    break;
                case AccountType.ProfitLost:
                    _result = "PL";
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