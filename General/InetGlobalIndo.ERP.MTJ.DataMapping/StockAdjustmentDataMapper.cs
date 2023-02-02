using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class StockAdjustmentDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStockAdjustmentStatus)
        //{
        //    string _result = "";

        //    switch (_prmStockAdjustmentStatus)
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

        //public static StockAdjustmentStatus GetStatus(char _prmStockAdjustmentStatus)
        //{
        //    StockAdjustmentStatus _result;

        //    switch (_prmStockAdjustmentStatus)
        //    {
        //        case 'H':
        //            _result = StockAdjustmentStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = StockAdjustmentStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = StockAdjustmentStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = StockAdjustmentStatus.Approved;
        //            break;
        //        default:
        //            _result = StockAdjustmentStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(StockAdjustmentStatus _prmStockAdjustmentStatus)
        //{
        //    char _result;

        //    switch (_prmStockAdjustmentStatus)
        //    {
        //        case StockAdjustmentStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case StockAdjustmentStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case StockAdjustmentStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case StockAdjustmentStatus.Approved:
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

        public static string GetAdjustType(AdjustType _prmAdjustType)
        {
            string _result = "";

            switch (_prmAdjustType)
            {
                case AdjustType.Saldo:
                    _result = "Saldo";
                    break;
                case AdjustType.Adjust:
                    _result = "Adjust";
                    break;
                default:
                    _result = "Saldo";
                    break;
            }

            return _result;
        }

        public static AdjustType GetAdjustType(string _prmAdjustType)
        {
            AdjustType _result;

            switch (_prmAdjustType)
            {
                case "Saldo":
                    _result = AdjustType.Saldo;
                    break;
                case "Adjust":
                    _result = AdjustType.Adjust;
                    break;
                default:
                    _result = AdjustType.Saldo;
                    break;
            }

            return _result;
        }
    }
}