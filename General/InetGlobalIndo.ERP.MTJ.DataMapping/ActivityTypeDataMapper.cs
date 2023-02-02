using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ActivityTypeDataMapper 
    {
        public static byte GetActivityType(ActivityType _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case ActivityType.Add:
                    _result = 0;
                    break;
                case ActivityType.Edit:
                    _result = 1;
                    break;
                case ActivityType.Delete:
                    _result = 2;
                    break;
                case ActivityType.GetApproval:
                    _result = 3;
                    break;
                case ActivityType.Approve:
                    _result = 4;
                    break;
                case ActivityType.Posting:
                    _result = 5;
                    break;
                case ActivityType.UnPosting:
                    _result = 6;
                    break;
                case ActivityType.Close:
                    _result = 7;
                    break;
                case ActivityType.Deleted:
                    _result = 8;
                    break;
            }

            return _result;
        }

        public static String GetActivityType(byte _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "Add";
                    break;
                case 1:
                    _result = "Edit";
                    break;
                case 2:
                    _result = "Delete";
                    break;
                case 3:
                    _result = "GetApproval";
                    break;
                case 4:
                    _result = "Approve";
                    break;
                case 5:
                    _result = "Posting";
                    break;
                case 6:
                    _result = "UnPosting";
                    break;
                case 7:
                    _result = "Close";
                    break;
                case 8:
                    _result = "Deleted";
                    break;
            }

            return _result;
        }
    }
}