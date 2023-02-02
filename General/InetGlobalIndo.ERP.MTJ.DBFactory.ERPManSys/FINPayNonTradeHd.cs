using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINPayNonTradeHd
    {
        private string _suppName = "";

        public FINPayNonTradeHd(string _prmTransNmbr,string _prmFileNmbr ,DateTime _prmTransDate, char _prmStatus, string _prmSuppCode, string _prmSuppName, string _prmCurrCode, decimal _prmForexRate, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.Remark = _prmRemark;
        }

        public string SuppName
        {
            get
            {
                return this._suppName;
            }
            set
            {
                this._suppName = value;
            }
        }
    }
}
