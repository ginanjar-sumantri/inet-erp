using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_TerminationFinishingAttachment
    {
        public HRM_TerminationFinishingAttachment(Guid _prmTerminationReqAttachCode, String _prmTransNmbr, string _prmFile, string _prmRemark)
        {
            this.TerminationFinishingAttachmentCode = _prmTerminationReqAttachCode;
            this.TransNmbr = _prmTransNmbr;
            this.File = _prmFile;
            this.Remark = _prmRemark;
        }
    }
}
