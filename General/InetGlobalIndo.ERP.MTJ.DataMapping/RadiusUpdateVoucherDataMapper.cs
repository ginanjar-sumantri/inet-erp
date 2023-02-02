using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class RadiusUpdateVoucherDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmRadiusUpdateVoucherStatus)
        //{
        //    string _result = "";

        //    switch (_prmRadiusUpdateVoucherStatus)
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

        //public static RadiusUpdateVoucherStatus GetStatus(char _prmRadiusUpdateVoucherStatus)
        //{
        //    RadiusUpdateVoucherStatus _result;

        //    switch (_prmRadiusUpdateVoucherStatus)
        //    {
        //        case 'H':
        //            _result = RadiusUpdateVoucherStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = RadiusUpdateVoucherStatus.Posting;
        //            break;
        //        case 'G':
        //            _result = RadiusUpdateVoucherStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = RadiusUpdateVoucherStatus.Approved;
        //            break;
        //        default:
        //            _result = RadiusUpdateVoucherStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(RadiusUpdateVoucherStatus _prmRadiusUpdateVoucherStatus)
        //{
        //    char _result = ' ';

        //    switch (_prmRadiusUpdateVoucherStatus)
        //    {
        //        case RadiusUpdateVoucherStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case RadiusUpdateVoucherStatus.Posting:
        //            _result = 'P';
        //            break;
        //        case RadiusUpdateVoucherStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case RadiusUpdateVoucherStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static int GetExpiredTimeUnit(RadiusUpdateVoucherExpiredTimeUnit _prmRadiusUpdateVoucherExpiredTimeUnit)
        {
            int _result = 0;

            switch (_prmRadiusUpdateVoucherExpiredTimeUnit)
            {
                case RadiusUpdateVoucherExpiredTimeUnit.Hour:
                    _result = 0;
                    break;
                case RadiusUpdateVoucherExpiredTimeUnit.Day:
                    _result = 1;
                    break;
                case RadiusUpdateVoucherExpiredTimeUnit.Month:
                    _result = 2;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static RadiusUpdateVoucherExpiredTimeUnit GetExpiredTimeUnit(int _prmRadiusUpdateVoucherExpiredTimeUnit)
        {
            RadiusUpdateVoucherExpiredTimeUnit _result;

            switch (_prmRadiusUpdateVoucherExpiredTimeUnit)
            {
                case 0:
                    _result = RadiusUpdateVoucherExpiredTimeUnit.Hour;
                    break;
                case 1:
                    _result = RadiusUpdateVoucherExpiredTimeUnit.Day;
                    break;
                case 2:
                    _result = RadiusUpdateVoucherExpiredTimeUnit.Month;
                    break;
                default:
                    _result = RadiusUpdateVoucherExpiredTimeUnit.Hour;
                    break;
            }

            return _result;
        }

        public static String GetExpiredTimeUnitText(int _prmRadiusUpdateVoucherExpiredTimeUnit)
        {
            String _result = "";

            switch (_prmRadiusUpdateVoucherExpiredTimeUnit)
            {
                case 0:
                    _result = "Hour";
                    break;
                case 1:
                    _result = "Day";
                    break;
                case 2:
                    _result = "Month";
                    break;
                default:
                    _result = "Hour";
                    break;
            }

            return _result;
        }
    }
}