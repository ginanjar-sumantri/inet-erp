using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class APRateDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmAPRateStatus)
        //{
        //    string _result = "";

        //    switch (_prmAPRateStatus)
        //    {
        //        case 'H':
        //            _result = "On Hold";
        //            break;
        //        case 'G':
        //            _result = "Waiting For Approval";
        //            break;
        //        case 'P':
        //            _result = "Posted";
        //            break;
        //        case 'A':
        //            _result = "Approved";
        //            break;
        //        default:
        //            _result = "On Hold";
        //            break;
        //    }

        //    return _result;
        //}

        //public static APRateStatus GetStatus(char _prmAPRateStatus)
        //{
        //    APRateStatus _result;

        //    switch (_prmAPRateStatus)
        //    {
        //        case 'H':
        //            _result = APRateStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = APRateStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = APRateStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = APRateStatus.Approved;
        //            break;
        //        default:
        //            _result = APRateStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(APRateStatus _prmAPRateStatus)
        //{
        //    char _result;

        //    switch (_prmAPRateStatus)
        //    {
        //        case APRateStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case APRateStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case APRateStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case APRateStatus.WaitingForApproval:
        //            _result = 'G';
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