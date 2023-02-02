using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrEmpGroupAssignHd
    {
        private string _EmpGroupAssignTypeName = "";

        public HRMTrEmpGroupAssignHd(string _prmTransNmbr, String _prmFileNmbr, char? _prmStatus, DateTime? _prmTransDate, DateTime? _prmEffectiveDate, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.EffectiveDate = _prmEffectiveDate;
            this.Remark = _prmRemark;
        }

        public HRMTrEmpGroupAssignHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }
    }
}
