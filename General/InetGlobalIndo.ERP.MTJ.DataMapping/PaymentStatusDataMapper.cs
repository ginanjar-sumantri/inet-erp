using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class PaymentStatusDataMapper
    {
        public static String GetPaymentStatusText(PaymentStatus _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case PaymentStatus.After:
                    _result = "After";
                    break;
                case PaymentStatus.Before:
                    _result = "Before";
                    break;
            }

            return _result;
        }

        public static String GetPaymentStatusValue(PaymentStatus _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case PaymentStatus.After:
                    _result = "After";
                    break;
                case PaymentStatus.Before:
                    _result = "Before";
                    break;
            }

            return _result;
        }
    }
}