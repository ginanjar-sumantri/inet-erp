using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class BILTrUnSubscriptionHd
    {
        private string _companyName = "";

        public BILTrUnSubscriptionHd(String _prmTransNmbr, String _prmFileNmbr, String _prmCustCode, DateTime _prmTransDate, Byte _prmStatus,
            String _prmRemark, String _prmCreatedBy, DateTime _prmCreatedDate, String _prmEditBy, DateTime _prmEditDate)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.CustCode = _prmCustCode;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.Remark = _prmRemark;
            this.CreatedBy = _prmCreatedBy;
            this.CreatedDate = _prmCreatedDate;
            this.EditBy = _prmEditBy;
            this.EditDate = _prmEditDate;
        }

        public BILTrUnSubscriptionHd(String _prmTransNmbr, String _prmFileNmbr, String _prmCustCode, DateTime _prmTransDate, Byte _prmStatus, String _prmCompanyName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.CustCode = _prmCustCode;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.CompanyName = _prmCompanyName;
        }

        public string CompanyName
        {
            get
            {
                return this._companyName;
            }
            set
            {
                this._companyName = value;
            }
        }
    }
}
