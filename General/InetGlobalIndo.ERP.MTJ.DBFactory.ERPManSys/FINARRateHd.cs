using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINARRateHd
    {
        public FINARRateHd(string _prmTransNmbr,string _prmFileNmbr ,DateTime _prmTransDate, string _prmCurrCode, char _prmStatus, decimal _prmNewRate, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.CurrCode = _prmCurrCode;
            this.Status = _prmStatus;
            this.NewRate = _prmNewRate;
            this.Remark = _prmRemark;
        }
    }
}
