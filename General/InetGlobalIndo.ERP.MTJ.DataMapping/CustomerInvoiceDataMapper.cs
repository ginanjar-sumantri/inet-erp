using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class CustomerInvoiceDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStatus)
        //{
        //    string _result = "";

        //    switch (_prmStatus)
        //    {
        //        case 'H':
        //            _result = "On  Hold";
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

        //public static CustomerInvoiceStatus GetStatus(char _prmStatus)
        //{
        //    CustomerInvoiceStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 'H':
        //            _result = CustomerInvoiceStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = CustomerInvoiceStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = CustomerInvoiceStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = CustomerInvoiceStatus.Approved;
        //            break;
        //        default:
        //            _result = CustomerInvoiceStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(CustomerInvoiceStatus _prmStatus)
        //{
        //    char _result;

        //    switch (_prmStatus)
        //    {
        //        case CustomerInvoiceStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case CustomerInvoiceStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case CustomerInvoiceStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case CustomerInvoiceStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static byte GetType(CustomerInvoiceType _prmValue)
        {
            byte _result;

            switch (_prmValue)
            {
                case CustomerInvoiceType.Other:
                    _result = 0;
                    break;
                case CustomerInvoiceType.Postpone:
                    _result = 1;
                    break;
                case CustomerInvoiceType.Register:
                    _result = 2;
                    break;
                case CustomerInvoiceType.SecurityDeposit:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static CustomerInvoiceType GetType(byte _prmValue)
        {
            CustomerInvoiceType _result;

            switch (_prmValue)
            {
                case 0:
                    _result = CustomerInvoiceType.Other;
                    break;
                case 1:
                    _result = CustomerInvoiceType.Postpone;
                    break;
                case 2:
                    _result = CustomerInvoiceType.Register;
                    break;
                case 3:
                    _result = CustomerInvoiceType.SecurityDeposit;
                    break;
                default:
                    _result = CustomerInvoiceType.Other;
                    break;
            }

            return _result;
        }

        public static string GetTypeText(CustomerInvoiceType _prmValue)
        {
            string _result;

            switch (_prmValue)
            {
                case CustomerInvoiceType.Other:
                    _result = "Other";
                    break;
                case CustomerInvoiceType.Postpone:
                    _result = "Postpone";
                    break;
                case CustomerInvoiceType.Register:
                    _result = "Register";
                    break;
                case CustomerInvoiceType.SecurityDeposit:
                    _result = "SecurityDeposit";
                    break;
                default:
                    _result = "Other";
                    break;
            }

            return _result;
        }
    }
}