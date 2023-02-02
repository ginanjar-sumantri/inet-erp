using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class GoodsInOutHd
    {
        public GoodsInOutHd(String _prmTransNmbr, String _prmFileNmbr, String _prmTransType, String _prmCustCode, DateTime _prmTransDate,
            String _prmRemark, Byte _prmStatus, String _prmCarryBy, String _prmRequestedBy , String _prmApprovedBy,
            String _prmPostedBy, DateTime? _prmEntryDate, String _prmEntryUserName, DateTime? _prmEditDate, String _prmEditUserName
            )
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransType = _prmTransType;
            this.CustCode = _prmCustCode;
            this.TransDate = _prmTransDate;
            this.Remark = _prmRemark;
            this.Status = _prmStatus;
            this.CarryBy = _prmCarryBy;
            this.RequestedBy = _prmRequestedBy;
            this.ApprovedBy = _prmApprovedBy;
            this.PostedBy = _prmPostedBy;
            this.EntryDate = _prmEntryDate;
            this.EntryUserName = _prmEntryUserName;
            this.EditDate = _prmEditDate;
            this.EditUserName = _prmEditUserName;

        }
    }
    
}