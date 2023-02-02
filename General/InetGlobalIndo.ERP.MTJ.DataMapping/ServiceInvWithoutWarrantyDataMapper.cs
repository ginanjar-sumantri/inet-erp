using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class ServiceInvWithoutWarrantyDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmServiceInvStatus)
        //{
        //    string _result = "";

        //    switch (_prmServiceInvStatus)
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

        //public static string GetStatusText(byte? _prmServiceInvStatus)
        //{
        //    string _result = "";

        //    switch (_prmServiceInvStatus)
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

        public static string GetStatusTextDetail(char? _prmServiceInvStatusDt)
        {
            string _result = "";

            switch (_prmServiceInvStatusDt)
            {
                case 'Y':
                    _result = "Closed";
                    break;
                case 'N':
                    _result = "Open";
                    break;
                default:
                    _result = "Open";
                    break;
            }

            return _result;
        }

        //public static ServiceInvStatus GetStatus(char _prmServiceInvStatus)
        //{
        //    ServiceInvStatus _result;

        //    switch (_prmServiceInvStatus)
        //    {
        //        case 'H':
        //            _result = ServiceInvStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = ServiceInvStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = ServiceInvStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = ServiceInvStatus.Approved;
        //            break;
        //        default:
        //            _result = ServiceInvStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(ServiceInvStatus _prmSparepartRequestStatus)
        //{
        //    byte _result;

        //    switch (_prmSparepartRequestStatus)
        //    {
        //        case ServiceInvStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case ServiceInvStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case ServiceInvStatus.Approved:
        //            _result = 2;
        //            break;
        //        case ServiceInvStatus.Posted:
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