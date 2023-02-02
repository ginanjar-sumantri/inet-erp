using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINGiroOut
    {
        public FINGiroOut(string _prmGiroNo, string _prmFileNmbr, char _prmStatus, string _prmPaymentNo, DateTime? _prmPaymentDate, DateTime? _prmDueDate)
        {
            this.GiroNo = _prmGiroNo;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.PaymentNo = _prmPaymentNo;
            this.PaymentDate = _prmPaymentDate;
            this.DueDate = _prmDueDate;
        }

        public FINGiroOut(string _prmGiroNo)
        {
            this.GiroNo = _prmGiroNo;
        }
    }
}
