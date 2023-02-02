using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsRatioReport
    {
        public MsRatioReport(string _prmReportType, int _prmRatioIndex, string _prmRatioName, int _prmRatioAverage)
        {
            this.ReportType = _prmReportType;
            this.RatioIndex = _prmRatioIndex;
            this.RatioName = _prmRatioName;
            this.RatioAverage = _prmRatioAverage;
        }
    }
}
