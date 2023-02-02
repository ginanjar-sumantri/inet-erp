using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public class FAServiceDataMapper : TransactionDataMapper
    {
        //public static string GetStatusText(char _prmStatus)
        //{
        //    string _result = "";

        //    switch (_prmStatus)
        //    {
        //        case 'H':
        //            _result = "On  Hold";
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

        //public static FAServiceStatus GetStatus(char _prmStatus)
        //{
        //    FAServiceStatus _result;

        //    switch (_prmStatus)
        //    {
        //        case 'H':
        //            _result = FAServiceStatus.OnHold;
        //            break;
        //        case 'P':
        //            _result = FAServiceStatus.Posted;
        //            break;
        //        case 'G':
        //            _result = FAServiceStatus.WaitingForApproval;
        //            break;
        //        case 'A':
        //            _result = FAServiceStatus.Approved;
        //            break;
        //        default:
        //            _result = FAServiceStatus.OnHold;
        //            break;
        //    }

        //    return _result;
        //}

        //public static char GetStatus(FAServiceStatus _prmStatus)
        //{
        //    char _result;

        //    switch (_prmStatus)
        //    {
        //        case FAServiceStatus.OnHold:
        //            _result = 'H';
        //            break;
        //        case FAServiceStatus.Posted:
        //            _result = 'P';
        //            break;
        //        case FAServiceStatus.WaitingForApproval:
        //            _result = 'G';
        //            break;
        //        case FAServiceStatus.Approved:
        //            _result = 'A';
        //            break;
        //        default:
        //            _result = 'H';
        //            break;
        //    }

        //    return _result;
        //}

        public static char GetAddValue(bool _prmValue)
        {
            char _result = 'N';

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static bool GetAddValue(char _prmValue)
        {
            bool _result;

            switch (_prmValue.ToString().ToLower())
            {
                case "y":
                    _result = true;
                    break;
                case "n":
                    _result = false;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }
    }
}
