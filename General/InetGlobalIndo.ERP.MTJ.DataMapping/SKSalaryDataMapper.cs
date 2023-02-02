using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class SKSalaryDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmSKSalaryStatus)
        //{
        //    string _result = "";

        //    switch (_prmSKSalaryStatus)
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

        //public static SKSalaryStatus GetStatus(Char _prmSKSalaryStatus)
        //{
        //    SKSalaryStatus _result;

        //    switch (_prmSKSalaryStatus)
        //    {
        //        case 'H':
        //            _result = SKSalaryStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = SKSalaryStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = SKSalaryStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = SKSalaryStatus.Posted;
        //            break;
        //        default:
        //            _result = SKSalaryStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(SKSalaryStatus _prmSKSalaryStatus)
        //{
        //    Char _result;

        //    switch (_prmSKSalaryStatus)
        //    {
        //        case SKSalaryStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case SKSalaryStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case SKSalaryStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case SKSalaryStatus.Posted:
        //            _result = 'P';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static String GetModeTypeText(ModeType _prmModeType)
        {
            String _result = "";

            switch (_prmModeType)
            {
                case ModeType.Percentage:
                    _result = "Percentage";
                    break;
                case ModeType.Amount:
                    _result = "Amount";
                    break;
                case ModeType.FixedAmount:
                    _result = "Fixed Amount";
                    break;
            }

            return _result;
        }
    }
}
