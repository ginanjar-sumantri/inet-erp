using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class V_POSReferenceNotYetPayList
    {
        public V_POSReferenceNotYetPayList(String _prmTransNmbr, String _prmTransType, String _prmReferenceNo)
        {
            this.TransNmbr = _prmTransNmbr;
            this.TransType = _prmTransType;
            this.ReferenceNo = _prmReferenceNo;
        }

        public V_POSReferenceNotYetPayList(String _prmTransNmbr, String _prmTransType, String _prmReferenceNo,
            Char _prmDoneSettlement, String _prmMemberID, String _prmCustName, String _prmCustPhone)
        {
            this.TransNmbr = _prmTransNmbr;
            this.TransType = _prmTransType;
            this.ReferenceNo = _prmReferenceNo;
            this.DoneSettlement = _prmDoneSettlement;
            this.MemberID = _prmMemberID;
            this.CustName = _prmCustName;
            this.CustPhone = _prmCustPhone;
        }

        ~V_POSReferenceNotYetPayList()
        {
        }
    }
}