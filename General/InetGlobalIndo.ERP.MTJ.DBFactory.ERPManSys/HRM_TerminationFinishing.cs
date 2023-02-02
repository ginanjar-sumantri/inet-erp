using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_TerminationFinishing
    {
        public HRM_TerminationFinishing(Guid _prmTerminationRequestCode, string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, byte _prmStatus, Boolean _prmIsAllowCertification, string _prmRemark)
        {
            this.TerminationRequestCode = _prmTerminationRequestCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.IsAllowCertification = _prmIsAllowCertification;
            this.Remark = _prmRemark;
        }
    }
}
