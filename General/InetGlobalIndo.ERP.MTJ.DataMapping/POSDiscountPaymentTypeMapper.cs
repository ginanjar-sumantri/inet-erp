using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSDiscountPaymentTypeMapper
    {
        public static byte GetStatus(POSDiscountPaymentType _prmStatus)
            {
                byte _result = 0;

                switch (_prmStatus)
                {
                    case POSDiscountPaymentType.All:
                        _result = 0;
                        break;
                    case POSDiscountPaymentType.Kredit:
                        _result = 1;
                        break;
                    case POSDiscountPaymentType.Debit:
                        _result = 2;
                        break;
                    case POSDiscountPaymentType.Voucher:
                        _result = 3;
                        break;
                    case POSDiscountPaymentType.Cash:
                        _result = 4;
                        break;
                    default:
                        _result = 4;
                        break;
                }

                return _result;
            }

        public static string GetStatusText(POSDiscountPaymentType _prmStatus)
            {
                string _result = "";

                switch (_prmStatus)
                {
                    case POSDiscountPaymentType.All:
                        _result = "All";
                        break;
                    case POSDiscountPaymentType.Kredit:
                        _result = "Kredit";
                        break;
                    case POSDiscountPaymentType.Debit:
                        _result = "Debit";
                        break;
                    case POSDiscountPaymentType.Voucher:
                        _result = "Voucher";
                        break;
                    case POSDiscountPaymentType.Cash:
                        _result = "Cash";
                        break;
                    default:
                        _result = "Cash";
                        break;
                }

                return _result;
            }

        public static POSDiscountPaymentType GetStatus(int _prmStatus)
            {
                POSDiscountPaymentType _result;

                switch (_prmStatus)
                {
                    case 0:
                        _result = POSDiscountPaymentType.All;
                        break;
                    case 1:
                        _result = POSDiscountPaymentType.Kredit;
                        break;
                    case 2:
                        _result = POSDiscountPaymentType.Debit;
                        break;
                    case 3:
                        _result = POSDiscountPaymentType.Voucher;
                        break;
                    case 4:
                        _result = POSDiscountPaymentType.Cash;
                        break;
                    default:
                        _result = POSDiscountPaymentType.Cash;
                        break;
                }

                return _result;
            }

        public static int GetNumericStatus(POSDiscountPaymentType _prmStatus)
            {
                int _result;

                switch (_prmStatus)
                {
                    case POSDiscountPaymentType.All:
                        _result = 0;
                        break;
                    case POSDiscountPaymentType.Kredit:
                        _result = 1;
                        break;
                    case POSDiscountPaymentType.Debit:
                        _result = 2;
                        break;
                    case POSDiscountPaymentType.Voucher:
                        _result = 3;
                        break;
                    case POSDiscountPaymentType.Cash:
                        _result = 4;
                        break;
                    default:
                        _result = 4;
                        break;
                }

                return _result;
            }


            public static string GetStatusText(int _prmStatus)
            {
                string _result = "";

                switch (_prmStatus)
                {
                    case 0:
                        _result = "All";
                        break;
                    case 1:
                        _result = "Kredit";
                        break;
                    case 2:
                        _result = "Debit";
                        break;
                    case 3:
                        _result = "Voucher";
                        break;
                    case 4:
                        _result = "Cash";
                        break;
                    default:
                        _result = "Cash";
                        break;
                }

                return _result;
            }
    }
}