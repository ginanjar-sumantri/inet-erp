using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class ContractDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(byte _prmContractStatus)
        //{
        //    string _result = "";

        //    switch (_prmContractStatus)
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

        //public static ContractStatus GetStatus(byte _prmContractStatus)
        //{
        //    ContractStatus _result;

        //    switch (_prmContractStatus)
        //    {
        //        case 0:
        //            _result = ContractStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = ContractStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = ContractStatus.Approved;
        //            break;
        //        case 3:
        //            _result = ContractStatus.Posting;
        //            break;
        //        default:
        //            _result = ContractStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(ContractStatus _prmContractStatus)
        //{
        //    byte _result;

        //    switch (_prmContractStatus)
        //    {
        //        case ContractStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case ContractStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case ContractStatus.Approved:
        //            _result = 2;
        //            break;
        //        case ContractStatus.Posting:
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
    }
}