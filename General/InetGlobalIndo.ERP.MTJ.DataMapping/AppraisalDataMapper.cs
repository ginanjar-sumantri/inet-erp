using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class AppraisalDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmAppraisalStatus)
        //{
        //    string _result = "";

        //    switch (_prmAppraisalStatus)
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

        //public static AppraisalStatus GetStatus(Char _prmAppraisalStatus)
        //{
        //    AppraisalStatus _result;

        //    switch (_prmAppraisalStatus)
        //    {
        //        case 'H':
        //            _result = AppraisalStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = AppraisalStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = AppraisalStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = AppraisalStatus.Posting;
        //            break;
        //        default:
        //            _result = AppraisalStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(AppraisalStatus _prmAppraisalStatus)
        //{
        //    Char _result;

        //    switch (_prmAppraisalStatus)
        //    {
        //        case AppraisalStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case AppraisalStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case AppraisalStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case AppraisalStatus.Posting:
        //            _result = 'P';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static Char GetResultDt(int _prmResult)
        {
            Char _result = 'E';

            switch (_prmResult)
            {
                case 0:
                    _result = 'E';
                    break;
                case 1:
                    _result = 'D';
                    break;
                case 2:
                    _result = 'C';
                    break;
                case 3:
                    _result = 'B';
                    break;
                case 4:
                    _result = 'A';
                    break;
            }

            return _result;
        }

        public static Char GetCharResultDt(AppraisalResult _prmResult)
        {
            Char _result = 'E';

            switch (_prmResult)
            {
                case AppraisalResult.E:
                    _result = 'E';
                    break;
                case AppraisalResult.D:
                    _result = 'D';
                    break;
                case AppraisalResult.C:
                    _result = 'C';
                    break;
                case AppraisalResult.B:
                    _result = 'B';
                    break;
                case AppraisalResult.A:
                    _result = 'A';
                    break;
            }

            return _result;
        }

        public static Decimal GetDecimalResultDt(AppraisalResult _prmResult)
        {
            Decimal _result = 0;

            switch (_prmResult)
            {
                case AppraisalResult.E:
                    _result = 0;
                    break;
                case AppraisalResult.D:
                    _result = 1;
                    break;
                case AppraisalResult.C:
                    _result = 2;
                    break;
                case AppraisalResult.B:
                    _result = 3;
                    break;
                case AppraisalResult.A:
                    _result = 4;
                    break;
            }

            return _result;
        }

        public static Decimal GetResultDt(Char _prmResult)
        {
            Decimal _result = 0;

            switch (_prmResult)
            {
                case 'E':
                    _result = 0;
                    break;
                case 'D':
                    _result = 1;
                    break;
                case 'C':
                    _result = 2;
                    break;
                case 'B':
                    _result = 3;
                    break;
                case 'A':
                    _result = 4;
                    break;
            }

            return _result;
        }
    }
}
