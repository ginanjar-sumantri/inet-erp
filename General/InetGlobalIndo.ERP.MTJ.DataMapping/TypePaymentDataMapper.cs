using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class TypePaymentDataMapper
    {
        public static string GetTypePayment(byte _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "Pra Bayar";
                    break;
                case 1:
                    _result = "Pasca Bayar";
                    break;
            }

            return _result;
        }
    }
}