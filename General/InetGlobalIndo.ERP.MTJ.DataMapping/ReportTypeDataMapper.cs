using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ReportTypeDataMapper 
    {
        public static byte GetReportType(ReportType _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case ReportType.Report:
                    _result = 1;
                    break;
                case ReportType.PrintPreview:
                    _result = 2;
                    break;
            }

            return _result;
        }
    }
}