using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class BenefitDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(Char _prmBenefitStatus)
        //{
        //    string _result = "";

        //    switch (_prmBenefitStatus)
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

        //public static BenefitStatus GetStatus(Char _prmBenefitStatus)
        //{
        //    BenefitStatus _result;

        //    switch (_prmBenefitStatus)
        //    {
        //        case 'H':
        //            _result = BenefitStatus.OnHold;
        //            break;
        //        case 'G':
        //            _result = BenefitStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = BenefitStatus.Approved;
        //            break;
        //        case 'P':
        //            _result = BenefitStatus.Posted;
        //            break;
        //        default:
        //            _result = BenefitStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static Char GetStatus(BenefitStatus _prmBenefitStatus)
        //{
        //    Char _result;

        //    switch (_prmBenefitStatus)
        //    {
        //        case BenefitStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case BenefitStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case BenefitStatus.Approved:
        //            _result = 'A';
        //            break;
        //        case BenefitStatus.Posted:
        //            _result = 'P';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}
    }
}
