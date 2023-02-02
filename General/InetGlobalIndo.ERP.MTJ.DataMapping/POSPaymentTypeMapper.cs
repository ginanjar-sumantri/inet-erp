using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSPaymentTypeMapper
    {
        public static string GetStatusText(POSPaymentType _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case POSPaymentType.Cash:
                    _result = "CASH";
                    break;
                case POSPaymentType.Kredit:
                    _result = "CREDIT";
                    break;
                case POSPaymentType.Debit:
                    _result = "DEBIT";
                    break;
                case POSPaymentType.Voucher:
                    _result = "VOUCHER";
                    break;
                default:
                    _result = "CASH";
                    break;
            }

            return _result;
        }
    }
}