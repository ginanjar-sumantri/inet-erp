using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINGiroIn
    {
        public FINGiroIn(string _prmGiroNo, string _prmFileNmbr, char _prmStatus, string _prmReceiptNo, DateTime _prmReceiptDate, DateTime _prmDueDate)
        {
            this.GiroNo = _prmGiroNo;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.ReceiptNo = _prmReceiptNo;
            this.ReceiptDate = _prmReceiptDate;
            this.DueDate = _prmDueDate;
        }

        public FINGiroIn(string _prmGiroNo)
        {
            this.GiroNo = _prmGiroNo;
        }
    }
}
