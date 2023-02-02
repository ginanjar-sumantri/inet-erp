using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class ProductAssemblyDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(byte _prmProductAssemblyStatus)
        //{
        //    string _result = "";

        //    switch (_prmProductAssemblyStatus)
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

        //public static ProductAssemblyStatus GetStatus(byte _prmProductAssemblyStatus)
        //{
        //    ProductAssemblyStatus _result;

        //    switch (_prmProductAssemblyStatus)
        //    {
        //        case 0:
        //            _result = ProductAssemblyStatus.OnHold;
        //            break;
        //        case 1:
        //            _result = ProductAssemblyStatus.WaitingForApproval;
        //            break;
        //        case 2:
        //            _result = ProductAssemblyStatus.Approved;
        //            break;
        //        case 3:
        //            _result = ProductAssemblyStatus.Posting;
        //            break;
        //        default:
        //            _result = ProductAssemblyStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static byte GetStatus(ProductAssemblyStatus _prmProductAssemblyStatus)
        //{
        //    byte _result;

        //    switch (_prmProductAssemblyStatus)
        //    {
        //        case ProductAssemblyStatus.OnHold:
        //            _result = 0;
        //            break;
        //        case ProductAssemblyStatus.WaitingForApproval:
        //            _result = 1;
        //            break;
        //        case ProductAssemblyStatus.Approved:
        //            _result = 2;
        //            break;
        //        case ProductAssemblyStatus.Posting:
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