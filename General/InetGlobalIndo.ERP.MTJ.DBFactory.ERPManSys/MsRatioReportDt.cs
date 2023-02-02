using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsRatioReportDt
    {
        public MsRatioReportDt(string _prmReportType, int _prmRatioIndex, string _prmValueType,
            string _prmRatioData1, string _prmRatioData2, string _prmRatioData3, string _prmOperator,
            string _prmOperator1)
        {
            this.ReportType = _prmReportType;
            this.RatioIndex = _prmRatioIndex;
            this.ValueType = _prmValueType;
            this.RatioData1 = _prmRatioData1;
            this.RatioData2 = _prmRatioData2;
            this.RatioData3 = _prmRatioData3;
            this.Operator = _prmOperator;
            this.Operator1 = _prmOperator1;
        }
    }
}
