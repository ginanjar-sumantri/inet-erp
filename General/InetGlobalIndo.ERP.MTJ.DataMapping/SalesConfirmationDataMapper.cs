using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class SalesConfirmationDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(byte _prmSalesConfirmationStatus)
        //{
        //    string _result = "";

        //    switch (_prmSalesConfirmationStatus)
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

        //public static SalesConfirmationStatus GetStatus(byte _prmSalesConfirmationStatus)
        //{
        //    SalesConfirmationStatus _result;

        //    switch (_prmSalesConfirmationStatus)
        //    {
        //        case 0:
        //            _result = SalesConfirmationStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = SalesConfirmationStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = SalesConfirmationStatus.Approved;
        //            break;
        //        case 3:
        //            _result = SalesConfirmationStatus.Posting;
        //            break;
        //        default:
        //            _result = SalesConfirmationStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(SalesConfirmationStatus _prmSalesConfirmationStatus)
        //{
        //    byte _result;

        //    switch (_prmSalesConfirmationStatus)
        //    {
        //        case SalesConfirmationStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case SalesConfirmationStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case SalesConfirmationStatus.Approved:
        //            _result = 2;
        //            break;
        //        case SalesConfirmationStatus.Posting:
        //            _result = 3;
        //            break;
        //        default:
        //            _result = 0;
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

        public static String GetTrueFalse(Boolean _prmValue)
        {
            String _result;

            switch (_prmValue)
            {
                case true:
                    _result = "True";
                    break;
                case false:
                    _result = "False";
                    break;
                default:
                    _result = "False";
                    break;
            }

            return _result;
        }

        public static Boolean GetPending(SalesConfirmationPendingStatus _prmValue)
        {
            Boolean _result = false;

            switch (_prmValue)
            {
                case SalesConfirmationPendingStatus.NotPending:
                    _result = false;
                    break;
                case SalesConfirmationPendingStatus.Pending:
                    _result = true;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }
    }
}