using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class MoneyChangerDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(byte _prmStatus)
        //{
        //    string _result = "";

        //    switch (_prmStatus)
        //    {
        //        case 0:
        //            _result = "On Hold";
        //            break;
        //        case 1:
        //            _result = "Waiting For Approval";
        //            break;
        //        case 2:
        //            _result = "Approved";
        //            break;
        //        case 3:
        //            _result = "Posted";
        //            break;
        //        default:
        //            _result = "Draft";
        //            break;
        //    }

        //    return _result;
        //}

        //public static MoneyChangerStatus GetStatus(byte _prmStatus)
        //{
        //    MoneyChangerStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 0:
        //            _result = MoneyChangerStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = MoneyChangerStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = MoneyChangerStatus.Approved;
        //            break;
        //        case 3:
        //            _result = MoneyChangerStatus.Posted;
        //            break;
        //        default:
        //            _result = MoneyChangerStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(MoneyChangerStatus _prmStatus)
        //{
        //    byte _result;

        //    switch (_prmStatus)
        //    {
        //        case MoneyChangerStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case MoneyChangerStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case MoneyChangerStatus.Approved:
        //            _result = 2;
        //            break;
        //        case MoneyChangerStatus.Posted:
        //            _result = 3;
        //            break;
        //        default:
        //            _result = 0;
        //            break;
        //    }

        //    return _result;
        //}

        public static string GetTypeText(MoneyChangerType _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case MoneyChangerType.Petty:
                    _result = "Petty";
                    break;
                case MoneyChangerType.Payment:
                    _result = "Payment";
                    break;
                default:
                    _result = "Petty";
                    break;
            }

            return _result;
        }

        public static MoneyChangerType GetType(byte _prmValue)
        {
            MoneyChangerType _result;

            switch (_prmValue)
            {
                case 0:
                    _result = MoneyChangerType.Petty;
                    break;
                case 1:
                    _result = MoneyChangerType.Payment;
                    break;
                default:
                    _result = MoneyChangerType.Petty;
                    break;
            }

            return _result;
        }

        public static byte GetType(MoneyChangerType _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case MoneyChangerType.Petty:
                    _result = 0;
                    break;
                case MoneyChangerType.Payment:
                    _result = 1;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }
    }
}