using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class ClinicDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmClinicStatus)
        //{
        //    string _result = "";

        //    switch (_prmClinicStatus)
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

        public static string GetStatusTextDetail(char? _prmClinicStatusDt)
        {
            string _result = "";

            switch (_prmClinicStatusDt)
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

        //public static ClinicStatus GetStatus(char _prmClinicStatus)
        //{
        //    ClinicStatus _result;

        //    switch (_prmClinicStatus)
        //    {
        //        case 'H':
        //            _result = ClinicStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = ClinicStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = ClinicStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = ClinicStatus.Approved;
        //            break;
        //        default:
        //            _result = ClinicStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(ClinicStatus _prmClinicStatus)
        //{
        //    char _result;

        //    switch (_prmClinicStatus)
        //    {
        //        case ClinicStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case ClinicStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case ClinicStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case ClinicStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
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