using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class CustBillAccountDataMapper
    {
        public static char GetActive(Boolean _prmValue)
        {
            char _result = 'N';

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static CustomerBillAccount GetTypePayment(byte _prmValue)
        {
            CustomerBillAccount _result;

            switch (_prmValue)
            {
                case 0:
                    _result = CustomerBillAccount.PraBayar;
                    break;
                case 1:
                    _result = CustomerBillAccount.PascaBayar;
                    break;
                default:
                    _result = CustomerBillAccount.PraBayar;
                    break;
            }

            return _result;
        }

        public static string GetTypePaymentText(CustomerBillAccount _prmValue)
        {
            string _result;

            switch (_prmValue)
            {
                case CustomerBillAccount.PraBayar:
                    _result = "Pra bayar";
                    break;
                case CustomerBillAccount.PascaBayar:
                    _result = "Pasca bayar";
                    break;
                default:
                    _result = "Pra bayar";
                    break;
            }

            return _result;
        }
    }
}