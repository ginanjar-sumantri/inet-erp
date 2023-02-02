using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_ExitInterviewAttachment
    {
        public HRM_ExitInterviewAttachment(Guid _prmTerminationReqAttachCode, Guid _prmTerminationRequestCode, string _prmFile, string _prmRemark)
        {
            this.ExitInterviewAttachmentCode = _prmTerminationReqAttachCode;
            this.TerminationRequestCode = _prmTerminationRequestCode;
            this.File = _prmFile;
            this.Remark = _prmRemark;
        }
    }
}
