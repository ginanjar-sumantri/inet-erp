using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockReceivingCustomerDataMapper : TransactionDataMapper
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

        //public static StockReceivingCustomerStatus GetStatus(char _prmStatus)
        //{
        //    StockReceivingCustomerStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 'H':
        //            _result = StockReceivingCustomerStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockReceivingCustomerStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockReceivingCustomerStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockReceivingCustomerStatus.Approved;
        //            break;
        //        default:
        //            _result = StockReceivingCustomerStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}


        //public static char GetStatus(StockReceivingCustomerStatus _prmStatus)
        //{
        //    char _result;

        //    switch (_prmStatus)
        //    {
        //        case StockReceivingCustomerStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockReceivingCustomerStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockReceivingCustomerStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockReceivingCustomerStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetYesNo(YesNo _prmYesNo)
        //{
        //    char _result;

        //    switch (_prmYesNo)
        //    {
        //        case YesNo.Yes:
        //            _result = 'Y';
        //            break;
        //        case YesNo.No:
        //            _result = 'N';
        //            break;
        //        default:
        //            _result = 'N';
        //            break;
        //    }

        //    return _result;
        //}

        public static char GetRRTypeStatus(RRTypeStatus _prmStatus)
        {
            char _result;

            switch (_prmStatus)
            {
                case RRTypeStatus.Other:
                    _result = 'O';
                    break;
                case RRTypeStatus.Customer:
                    _result = 'C';
                    break;
                case RRTypeStatus.Supplier:
                    _result = 'S';
                    break;
                default:
                    _result = 'O';
                    break;
            }

            return _result;
        }
    }
}