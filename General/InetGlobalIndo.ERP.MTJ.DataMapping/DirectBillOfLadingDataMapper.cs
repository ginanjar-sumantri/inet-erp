using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class DirectBillOfLadingDataMapper : TransactionDataMapper
    {
        //public static byte GetStatus(DirectBillOfLadingStatus _prmStatus)
        //{
        //    byte _result = 0;

        //    switch (_prmStatus)
        //    {
        //        case DirectBillOfLadingStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case DirectBillOfLadingStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case DirectBillOfLadingStatus.Approved:
        //            _result = 2;
        //            break;
        //        case DirectBillOfLadingStatus.Posted:
        //            _result = 3;
        //            break;
        //        default:
        //            _result = 0;
        //            break;
        //    }

        //    return _result;
        //}

        //public static string GetStatusText(DirectBillOfLadingStatus _prmStatus)
        //{
        //    string _result = "";

        //    switch (_prmStatus)
        //    {
        //        case DirectBillOfLadingStatus.OnHold:
        //            _result = "On Hold";
        //            break;
        //        case DirectBillOfLadingStatus.WaitingForApproval:
        //            _result = "Waiting For Approval";
        //            break;
        //        case DirectBillOfLadingStatus.Approved:
        //            _result = "Approved";
        //            break;
        //        case DirectBillOfLadingStatus.Posted:
        //            _result = "Posted";
        //            break;
        //        default:
        //            _result = "On Hold";
        //            break;
        //    }

        //    return _result;
        //}

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

        //public static DirectBillOfLadingStatus GetStatus(int _prmStatus)
        //{
        //    DirectBillOfLadingStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 0:
        //            _result = DirectBillOfLadingStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = DirectBillOfLadingStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = DirectBillOfLadingStatus.Approved;
        //            break;
        //        case 3:
        //            _result = DirectBillOfLadingStatus.Posted;
        //            break;
        //        default:
        //            _result = DirectBillOfLadingStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        public static int GetNumericStatus(DirectBillOfLadingStatus _prmStatus)
        {
            int _result;

            switch (_prmStatus)
            {
                case DirectBillOfLadingStatus.OnHold:
                    _result = 0;
                    break;
                case DirectBillOfLadingStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case DirectBillOfLadingStatus.Approved:
                    _result = 2;
                    break;
                case DirectBillOfLadingStatus.Posted:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }


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

    }
}