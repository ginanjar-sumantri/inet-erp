using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrSalaryPaymentHd
    {
        public PAYTrSalaryPaymentHd(String _prmTransNo, string _prmFileNmbr, DateTime _prmTransDate, Char _prmStatus, String _prmSalaryNo, String _prmRemark)
        {
            this.TransNmbr = _prmTransNo;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.SalaryNo = _prmSalaryNo;
            this.Remark = _prmRemark;
        }
    }
}
