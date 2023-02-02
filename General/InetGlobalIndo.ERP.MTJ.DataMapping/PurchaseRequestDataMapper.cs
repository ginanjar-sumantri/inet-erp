using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class PurchaseRequestDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStatus)
        //{
        //    string _result = "";

        //    switch (_prmStatus)
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

        //public static string GetStatusText(int _prmStatus)
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
        //            _result = "On Hold";
        //            break;
        //    }

        //    return _result;
        //}

        //public static PurchaseRequestStatus GetStatus(char _prmStatus)
        //{
        //    PurchaseRequestStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 'H':
        //            _result = PurchaseRequestStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = PurchaseRequestStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = PurchaseRequestStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = PurchaseRequestStatus.Approved;
        //            break;
        //        default:
        //            _result = PurchaseRequestStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static PurchaseRequestStatus GetStatus(int _prmStatus)
        //{
        //    PurchaseRequestStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 0:
        //            _result = PurchaseRequestStatus.OnHold;
        //            break;
        //        case 3:
        //            _result = PurchaseRequestStatus.Posted;
        //            break;
        //        case 1:
        //            _result = PurchaseRequestStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = PurchaseRequestStatus.Approved;
        //            break;
        //        default:
        //            _result = PurchaseRequestStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}


        //public static char GetStatus(PurchaseRequestStatus _prmStatus)
        //{
        //    char _result;

        //    switch (_prmStatus)
        //    {
        //        case PurchaseRequestStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case PurchaseRequestStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case PurchaseRequestStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case PurchaseRequestStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static int GetNumericStatus(PurchaseRequestStatus _prmStatus)
        {
            int _result;

            switch (_prmStatus)
            {
                case PurchaseRequestStatus.OnHold:
                    _result = 0;
                    break;
                case PurchaseRequestStatus.Posted:
                    _result = 3;
                    break;
                case PurchaseRequestStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case PurchaseRequestStatus.Approved:
                    _result = 2;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static PurchaseRequestStatusDt GetStatusDetail(char _prmStatus)
        {
            PurchaseRequestStatusDt _result;

            switch (_prmStatus)
            {
                case 'Y':
                    _result = PurchaseRequestStatusDt.Closed;
                    break;
                case 'N':
                    _result = PurchaseRequestStatusDt.Open;
                    break;
                default:
                    _result = PurchaseRequestStatusDt.Open;
                    break;
            }

            return _result;
        }

        public static char GetStatusDetail(PurchaseRequestStatusDt _prmStatus)
        {
            char _result;

            switch (_prmStatus)
            {
                case PurchaseRequestStatusDt.Closed:
                    _result = 'Y';
                    break;
                case PurchaseRequestStatusDt.Open:
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