using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_ApplicantAttachment
    {
        public HRM_ApplicantAttachment(string _prmRemark, Guid _prmAttachmentCode, string _prmFile)
        {
            this.Remark = _prmRemark;
            this.ApplicantAttachmentCode = _prmAttachmentCode;
            this.File = _prmFile;
        }
    }
}
