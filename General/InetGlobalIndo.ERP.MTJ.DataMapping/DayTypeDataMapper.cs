using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class DayTypeDataMapper
    {
        public static String GetDayType(DayTypeStatus _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case DayTypeStatus.Work:
                    _result = "Work";
                    break;
                case DayTypeStatus.Holiday:
                    _result = "Holiday";
                    break;
                case DayTypeStatus.PublicHoliday:
                    _result = "Public Holiday";
                    break;
            }

            return _result;
        }
    }
}