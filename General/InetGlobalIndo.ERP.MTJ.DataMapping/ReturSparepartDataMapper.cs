using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class ReturSparepartDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmReturSparepartStatus)
        //{
        //    string _result = "";

        //    switch (_prmReturSparepartStatus)
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

        //public static string GetStatusText(byte? _prmReturSparepartStatus)
        //{
        //    string _result = "";

        //    switch (_prmReturSparepartStatus)
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

        //public static string GetStatusTextDetail(char? _prmReturSparepartStatusDt)
        //{
        //    string _result = "";

        //    switch (_prmReturSparepartStatusDt)
        //    {
        //        case 'Y':
        //            _result = "Closed";
        //            break;
        //        case 'N':
        //            _result = "Open";
        //            break;
        //        default:
        //            _result = "Open";
        //            break;
        //    }

        //    return _result;
        //}

        //public static ReturSparepartStatus GetStatus(char _prmReturSparepartStatus)
        //{
        //    ReturSparepartStatus _result;

        //    switch (_prmReturSparepartStatus)
        //    {
        //        case 'H':
        //            _result = ReturSparepartStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = ReturSparepartStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = ReturSparepartStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = ReturSparepartStatus.Posted;
        //            break;
        //        default:
        //            _result = ReturSparepartStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte? GetStatus(ReturSparepartStatus _prmReturSparepartStatus)
        //{
        //    byte? _result;

        //    switch (_prmReturSparepartStatus)
        //    {
        //        case ReturSparepartStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case ReturSparepartStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case ReturSparepartStatus.Approved:
        //            _result = 2;
        //            break;
        //        case ReturSparepartStatus.Posted:
        //            _result = 3;
        //            break;
        //        default:
        //            _result = 0;
        //            break;
        //    }

        //    return _result;
        //}

        public static char GetStatusFlag(bool _prmStatus)
        {
            char _result;

            switch (_prmStatus)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static bool GetStatusFlag(char _prmStatus)
        {
            bool _result;

            switch (_prmStatus.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

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
    }
}