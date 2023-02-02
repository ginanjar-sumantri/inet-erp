using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class POSPromoPaymentTypeMapper
    {
        public static byte GetStatus(POSPromoPaymentType _prmStatus)
            {
                byte _result = 0;

                switch (_prmStatus)
                {
                    case POSPromoPaymentType.All:
                        _result = 0;
                        break;
                    case POSPromoPaymentType.Kredit:
                        _result = 1;
                        break;
                    case POSPromoPaymentType.Debit:
                        _result = 2;
                        break;
                    case POSPromoPaymentType.Voucher:
                        _result = 3;
                        break;
                    case POSPromoPaymentType.Cash:
                        _result = 4;
                        break;
                    default:
                        _result = 4;
                        break;
                }

                return _result;
            }

        public static string GetStatusText(POSPromoPaymentType _prmStatus)
            {
                string _result = "";

                switch (_prmStatus)
                {
                    case POSPromoPaymentType.All:
                        _result = "All";
                        break;
                    case POSPromoPaymentType.Kredit:
                        _result = "Kredit";
                        break;
                    case POSPromoPaymentType.Debit:
                        _result = "Debit";
                        break;
                    case POSPromoPaymentType.Voucher:
                        _result = "Voucher";
                        break;
                    case POSPromoPaymentType.Cash:
                        _result = "Cash";
                        break;
                    default:
                        _result = "Cash";
                        break;
                }

                return _result;
            }

        public static POSPromoPaymentType GetStatus(int _prmStatus)
            {
                POSPromoPaymentType _result;

                switch (_prmStatus)
                {
                    case 0:
                        _result = POSPromoPaymentType.All;
                        break;
                    case 1:
                        _result = POSPromoPaymentType.Kredit;
                        break;
                    case 2:
                        _result = POSPromoPaymentType.Debit;
                        break;
                    case 3:
                        _result = POSPromoPaymentType.Voucher;
                        break;
                    case 4:
                        _result = POSPromoPaymentType.Cash;
                        break;
                    default:
                        _result = POSPromoPaymentType.Cash;
                        break;
                }

                return _result;
            }

        public static int GetNumericStatus(POSPromoPaymentType _prmStatus)
            {
                int _result;

                switch (_prmStatus)
                {
                    case POSPromoPaymentType.All:
                        _result = 0;
                        break;
                    case POSPromoPaymentType.Kredit:
                        _result = 1;
                        break;
                    case POSPromoPaymentType.Debit:
                        _result = 2;
                        break;
                    case POSPromoPaymentType.Voucher:
                        _result = 3;
                        break;
                    case POSPromoPaymentType.Cash:
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