using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class CompanyConfigReportParameter
    {
        public CompanyConfigReportParameter(String _prmReportID, String _prmReportParameter,
            String _prmReportName, String _prmValue, String _prmRemark)
        {
            this.ReportID = _prmReportID;
            this.ReportParameter = _prmReportParameter;
            this.ReportName = _prmReportName;
            this.Value = _prmValue;
            this.Remark = _prmRemark;
        }

        public CompanyConfigReportParameter(String _prmReportID, String _prmReportName)
        {
            this.ReportID = _prmReportID;
            this.ReportName = _prmReportName;
        }
    }
}
