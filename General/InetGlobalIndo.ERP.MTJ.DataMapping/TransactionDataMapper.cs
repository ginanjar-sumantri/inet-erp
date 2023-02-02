using System;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public abstract class TransactionDataMapper
    {
        public static string GetStatusText(Byte _prmTransStatus)
        {
            string _result = "";

            switch (_prmTransStatus)
            {
                case 0:
                    _result = "On Hold";
                    break;
                case 1:
                    _result = "Waiting For Approval";
                    break;
                case 2:
                    _result = "Approved";
                    break;
                case 3:
                    _result = "Posted";
                    break;
                case 4:
                    _result = "Deleted";
                    break;
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }

        public static TransStatus GetStatus(Byte _prmTransStatus)
        {
            TransStatus _result;

            switch (_prmTransStatus)
            {
                case 0:
                    _result = TransStatus.OnHold;
                    break;
                case 1:
                    _result = TransStatus.WaitingForApproval;
                    break;
                case 2:
                    _result = TransStatus.Approved;
                    break;
                case 3:
                    _result = TransStatus.Posted;
                    break;
                case 4:
                    _result = TransStatus.Deleted;
                    break;
                default:
                    _result = TransStatus.OnHold;
                    break;
            }

            return _result;
        }

        public static Byte GetStatusByte(TransStatus _prmTransStatus)//Dari GetStatus
        {
            Byte _result;

            switch (_prmTransStatus)
            {
                case TransStatus.OnHold:
                    _result = 0;
                    break;
                case TransStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case TransStatus.Approved:
                    _result = 2;
                    break;
                case TransStatus.Posted:
                    _result = 3;
                    break;
                case TransStatus.Deleted:
                    _result = 4;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static string GetStatusText(char _prmTransStatus)
        {
            string _result = "";

            switch (_prmTransStatus)
            {
                case 'H':
                    _result = "On Hold";
                    break;
                case 'G':
                    _result = "Waiting For Approval";
                    break;
                case 'A':
                    _result = "Approved";
                    break;
                case 'P':
                    _result = "Posted";
                    break;
                case 'D':
                    _result = "Deleted";
                    break;
                case 'F':
                    _result = "Complete";
                    break;
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }

        public static TransStatus GetStatus(char _prmTransStatus)
        {
            TransStatus _result;

            switch (_prmTransStatus)
            {
                case 'H':
                    _result = TransStatus.OnHold;
                    break;
                case 'P':
                    _result = TransStatus.Posted;
                    break;
                case 'G':
                    _result = TransStatus.WaitingForApproval;
                    break;
                case 'A':
                    _result = TransStatus.Approved;
                    break;
                case 'D':
                    _result = TransStatus.Deleted;
                    break;
                case 'F':
                    _result = TransStatus.Complete;
                    break;
                default:
                    _result = TransStatus.OnHold;
                    break;
            }

            return _result;
        }

        public static char GetStatus(TransStatus _prmTransStatus)
        {
            char _result;

            switch (_prmTransStatus)
            {
                case TransStatus.OnHold:
                    _result = 'H';
                    break;
                case TransStatus.Posted:
                    _result = 'P';
                    break;
                case TransStatus.WaitingForApproval:
                    _result = 'G';
                    break;
                case TransStatus.Approved:
                    _result = 'A';
                    break;
                case TransStatus.Deleted:
                    _result = 'D';
                    break;
                case TransStatus.Complete:
                    _result = 'F';
                    break;
                default:
                    _result = 'H';
                    break;
            }

            return _result;
        }

        public static string GetStatusText(TransStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case TransStatus.OnHold:
                    _result = "On Hold";
                    break;
                case TransStatus.WaitingForApproval:
                    _result = "Waiting For Approval";
                    break;
                case TransStatus.Approved:
                    _result = "Approved";
                    break;
                case TransStatus.Posted:
                    _result = "Posted";
                    break;
                case TransStatus.Deleted:
                    _result = "Deleted";
                    break;
                case TransStatus.Complete:
                    _result = "Complete";
                    break;
                default:
                    _result = "On Hold";
                    break;
            }
            return _result;
        }


    }
}