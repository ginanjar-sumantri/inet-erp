using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PRCRequestHd
    {
        public PRCRequestHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmOrgUnit, string _prmRequestBy, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.OrgUnit = _prmOrgUnit;
            this.RequestBy = _prmRequestBy;
            this.Remark = _prmRemark;
        }

        public PRCRequestHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmOrgUnit, string _prmRequestBy, string _prmRemark, string _prmCurrCode, string _prmCreatedBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.OrgUnit = _prmOrgUnit;
            this.RequestBy = _prmRequestBy;
            this.Remark = _prmRemark;
            this.CurrCode = _prmCurrCode;
            this.CreatedBy = _prmCreatedBy;
        }

        public PRCRequestHd(string _prmFileNmbr)
        {
            this.FileNmbr = _prmFileNmbr;
        }

        public PRCRequestHd(string _prmTransNmbr, string _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public PRCRequestHd(string _prmTransNmbr, string _prmFileNmbr, string _prmRequestBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.RequestBy = _prmRequestBy;
        }
    }
}
