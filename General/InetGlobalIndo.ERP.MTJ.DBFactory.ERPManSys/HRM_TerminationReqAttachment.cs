using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_TerminationReqAttachment
    {
        public string _terminationRequestFileNmbr = "";

        public HRM_TerminationReqAttachment(Guid _prmTerminationReqAttachCode, Guid _prmTerminationRequestCode, string _prmTerminationRequestFileNmbr, string _prmFile, string _prmRemark)
        {
            this.TerminationReqAttachCode = _prmTerminationReqAttachCode;
            this.TerminationRequestCode = _prmTerminationRequestCode;
            this.TerminationRequestFileNmbr = _prmTerminationRequestFileNmbr;
            this.File = _prmFile;
            this.Remark = _prmRemark;
        }

        public string TerminationRequestFileNmbr
        {
            get
            {
                return this._terminationRequestFileNmbr;
            }
            set
            {
                this._terminationRequestFileNmbr = value;
            }
        }
    }
}
