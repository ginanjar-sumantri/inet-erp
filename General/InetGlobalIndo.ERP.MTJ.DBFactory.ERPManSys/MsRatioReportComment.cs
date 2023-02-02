using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsRatioReportComment
    {
        public MsRatioReportComment(string _prmReportType, int _prmRatioIndex, decimal _prmStartVariance, decimal _prmEndVariance, string _prmRatioComment)
        {
            this.ReportType = _prmReportType;
            this.RatioIndex = _prmRatioIndex;
            this.StartVariance = _prmStartVariance;
            this.EndVariance = _prmEndVariance;
            this.RatioComment = _prmRatioComment;
        }
    }
}
