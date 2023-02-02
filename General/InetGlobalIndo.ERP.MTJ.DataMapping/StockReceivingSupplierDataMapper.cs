using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockReceivingSupplierDataMapper : TransactionDataMapper
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

        //public static StockReceivingSupplierStatus GetStatus(char _prmStatus)
        //{
        //    StockReceivingSupplierStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 'H':
        //            _result = StockReceivingSupplierStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockReceivingSupplierStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockReceivingSupplierStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockReceivingSupplierStatus.Approved;
        //            break;
        //        default:
        //            _result = StockReceivingSupplierStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}


        //public static char GetStatus(StockReceivingSupplierStatus _prmStatus)
        //{
        //    char _result;

        //    switch (_prmStatus)
        //    {
        //        case StockReceivingSupplierStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockReceivingSupplierStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockReceivingSupplierStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockReceivingSupplierStatus.Approved:
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