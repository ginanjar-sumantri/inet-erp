using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class master_ReportList
    {

        public master_ReportList(String _prmName, String _prmPath)
        {
            this.ReportName = _prmName;
            this.ReportPath = _prmPath;
        }

        public master_ReportList(byte _prmReportType, String _prmReportGroupID, int _prmSortNo, String _prmReportName, Guid _prmCompanyID)
        {
            this.ReportType = _prmReportType;
            this.ReportGroupID = _prmReportGroupID;
            this.SortNo = _prmSortNo;
            this.ReportName = _prmReportName;
            this.CompanyID = _prmCompanyID;
        }

        public master_ReportList(Byte _prmReportType, string _prmReportGroupID, int _prmSortNo, string _prmReportName, String _prmReportPath, Guid _prmCompanyID, Boolean? _prmfgActive)
        {
            this.ReportType = _prmReportType;
            this.ReportGroupID = _prmReportGroupID;
            this.SortNo = _prmSortNo;
            this.ReportName = _prmReportName;
            this.ReportPath = _prmReportPath;
            this.CompanyID = _prmCompanyID;
            this.fgActive = _prmfgActive;
        }

        public master_ReportList(Byte _prmReportType, string _prmReportGroupID, int _prmSortNo, string _prmReportName, String _prmReportPath, Guid _prmCompanyID, Boolean? _prmfgActive, Boolean _prmEnabled)
        {
            this.ReportType = _prmReportType;
            this.ReportGroupID = _prmReportGroupID;
            this.SortNo = _prmSortNo;
            this.ReportName = _prmReportName;
            this.ReportPath = _prmReportPath;
            this.CompanyID = _prmCompanyID;
            this.fgActive = _prmfgActive;
            this.Enabled = _prmEnabled;
        }
    }
}
