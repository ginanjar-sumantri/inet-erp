using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class ARRateDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmARRateStatus)
        //{
        //    string _result = "";

        //    switch (_prmARRateStatus)
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

        //public static ARRateStatus GetStatus(char _prmARRateStatus)
        //{
        //    ARRateStatus _result;

        //    switch (_prmARRateStatus)
        //    {
        //        case 'H':
        //            _result = ARRateStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = ARRateStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = ARRateStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = ARRateStatus.Approved;
        //            break;
        //        default:
        //            _result = ARRateStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(ARRateStatus _prmARRateStatus)
        //{
        //    char _result;

        //    switch (_prmARRateStatus)
        //    {
        //        case ARRateStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case ARRateStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case ARRateStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case ARRateStatus.Approved:
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

        public static string GetIsApplyToPPN(Boolean _prmValue)
        {
            string _result;

            switch (_prmValue)
            {
                case true:
                    _result = "Yes";
                    break;
                case false:
                    _result = "No";
                    break;
                default:
                    _result = "No";
                    break;
            }

            return _result;
        }
    }
}