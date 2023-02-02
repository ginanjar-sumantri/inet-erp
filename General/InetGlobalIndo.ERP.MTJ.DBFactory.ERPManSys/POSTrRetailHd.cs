using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSTrRetailHd
    {
        private String _customerName = "";

        public POSTrRetailHd(String _prmTransNmbr, String _prmFileNmbr, String _prmTransType, DateTime _prmTransDate,
            String _prmReferenceNo, String _prmMemberID, Byte _prmStatus, Char _prmDoneSettlement, String _prmRemark, Boolean _prmDeliveryStatus)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransType = _prmTransType;
            this.TransDate = _prmTransDate;
            this.ReferenceNo = _prmReferenceNo;
            this.MemberID = _prmMemberID;
            this.Status = _prmStatus;
            this.DoneSettlement = _prmDoneSettlement;
            this.Remark = _prmRemark;
            this.DeliveryStatus = _prmDeliveryStatus;
        }

        public String CustomerName
        {
            get
            {
                return this._customerName;
            }
            set
            {
                this._customerName = value;
            }
        }
    }
}