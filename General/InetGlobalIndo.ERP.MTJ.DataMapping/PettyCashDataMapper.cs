using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class PettyCashDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmPCOStatus)
        //{
        //    string _result = "";

        //    switch (_prmPCOStatus)
        //    {
        //        case 'H':
        //            _result = "On Hold";
        //            break;
        //        case 'G':
        //            _result = "Waiting For Approval";
        //            break;
        //        case 'A':
        //            _result = "Approved";
        //            break;
        //        case 'P':
        //            _result = "Posted";
        //            break;
        //        default:
        //            _result = "On Hold";
        //            break;
        //    }

        //    return _result;
        //}

        //public static PCOStatus GetStatus(char _prmPCOStatus)
        //{
        //    PCOStatus _result;

        //    switch (_prmPCOStatus)
        //    {
        //        case 'H':
        //            _result = PCOStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = PCOStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = PCOStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = PCOStatus.Approved;
        //            break;
        //        default:
        //            _result = PCOStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(PCOStatus _prmPCOStatus)
        //{
        //    char _result;

        //    switch (_prmPCOStatus)
        //    {
        //        case PCOStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case PCOStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case PCOStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case PCOStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static string GetTypeText(PettyCashReceiveType _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case PettyCashReceiveType.Petty:
                    _result = "Petty";
                    break;
                case PettyCashReceiveType.Payment:
                    _result = "Payment";
                    break;
                default:
                    _result = "Petty";
                    break;
            }

            return _result;
        }

        public static PettyCashReceiveType GetType(byte _prmValue)
        {
            PettyCashReceiveType _result;

            switch (_prmValue)
            {
                case 0:
                    _result = PettyCashReceiveType.Petty;
                    break;
                case 1:
                    _result = PettyCashReceiveType.Payment;
                    break;
                default:
                    _result = PettyCashReceiveType.Petty;
                    break;
            }

            return _result;
        }

        public static byte GetType(PettyCashReceiveType _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case PettyCashReceiveType.Petty:
                    _result = 0;
                    break;
                case PettyCashReceiveType.Payment:
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