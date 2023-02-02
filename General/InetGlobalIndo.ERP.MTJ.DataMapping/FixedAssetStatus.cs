using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class FixedAssetStatus : TransactionDataMapper
    {
       //public static FAStatus IsStatus(char _prmFAStatus)
       //{
       //    FAStatus _result;

       //    switch (_prmFAStatus)
       //    {
       //        case 'H':
       //            _result = FAStatus.OnHold;
       //            break;
       //        case 'P':
       //            _result = FAStatus.Posted;
       //            break;
       //        case 'G':
       //            _result = FAStatus.WaitingForApproval;
       //            break;
       //        case 'A':
       //            _result = FAStatus.Approved;
       //            break;
       //        default:
       //            _result = FAStatus.OnHold;
       //            break;
       //    }

       //    return _result;
       //}

       //public static char IsStatus(FAStatus _prmFAStatus)
       //{
       //    char _result;

       //    switch (_prmFAStatus)
       //    {
       //        case FAStatus.OnHold:
       //            _result = 'H';
       //            break;
       //        case FAStatus.Posted:
       //            _result = 'P';
       //            break;
       //        case FAStatus.WaitingForApproval:
       //            _result = 'G';
       //            break;
       //        case FAStatus.Approved:
       //            _result = 'A';
       //            break;
       //        default:
       //            _result = 'H';
       //            break;
       //    }

       //    return _result;
       //}

       //public static string StatusText(char _prmFAStatus)
       //{
       //    string _result;

       //    switch (_prmFAStatus)
       //    {
       //        case 'H':
       //            _result = "On Hold";
       //            break;
       //        case 'P':
       //            _result = "Posted";
       //            break;
       //        case 'G':
       //            _result = "Waiting For Approval";
       //            break;
       //        case 'A':
       //            _result = "Approved";
       //            break;
       //        default:
       //            _result = "Hold";
       //            break;
       //    }

       //    return _result;
       //}

       //public static char StatusText(string _prmFAStatus)
       //{
       //    char _result;

       //    switch (_prmFAStatus)
       //    {
       //        case "On Hold":
       //            _result = 'H';
       //            break;
       //        case "Posted":
       //            _result = 'P';
       //            break;
       //        case "Waiting For Approval":
       //            _result = 'G';
       //            break;
       //        case "Approved":
       //            _result = 'A';
       //            break;
       //        default:
       //            _result = 'H';
       //            break;
       //    }

       //    return _result;
       //}

       public static char CreatedFromStatus(FixedAssetCreatedFrom _prmFACreatedFrom)
       {
           char _result;

           switch (_prmFACreatedFrom)
           {
               case FixedAssetCreatedFrom.FAAddStock:
                   _result = 'S';
                   break;
               case FixedAssetCreatedFrom.FAPurchase:
                   _result = 'P';
                   break;
               case FixedAssetCreatedFrom.Manual:
                   _result = 'M';
                   break;
               default:
                   _result = 'M';
                   break;
           }

           return _result;
       }

       public static FixedAssetCreatedFrom CreatedFromStatus(char _prmFACreatedFrom)
       {
           FixedAssetCreatedFrom _result;

           switch (_prmFACreatedFrom)
           {
               case 'S':
                   _result = FixedAssetCreatedFrom.FAAddStock;
                   break;
               case 'P':
                   _result = FixedAssetCreatedFrom.FAPurchase;
                   break;
               case 'M':
                   _result = FixedAssetCreatedFrom.Manual;
                   break;
               default:
                   _result = FixedAssetCreatedFrom.Manual;
                   break;
           }

           return _result;
       }

       public static string CreatedFromText(char _prmFACreatedFrom)
       {
           string _result;

           switch (_prmFACreatedFrom)
           {
               case 'S':
                   _result = "Fixed Asset Add Stock";
                   break;
               case 'P':
                   _result = "Fixed Asset Purchase";
                   break;
               case 'M':
                   _result = "Manual";
                   break;
               default:
                   _result = "Manual";
                   break;
           }

           return _result;
       }
    }
}