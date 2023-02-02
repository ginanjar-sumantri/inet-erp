using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ScheduleTypeDataMapper 
    {
        public static String GetScheduleType(ScheduleType _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case ScheduleType.Shift:
                    _result = "Shift";
                    break;
                case ScheduleType.Office:
                    _result = "Office";
                    break;
            }

            return _result;
        }
    }
}