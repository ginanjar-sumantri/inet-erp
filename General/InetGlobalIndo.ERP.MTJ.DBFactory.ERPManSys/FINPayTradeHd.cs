using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINPayTradeHd
    {
        private string _suppName = "";

        public FINPayTradeHd(String _prmTransNo, string _prmFileNmbr, DateTime _prmTransDate, Char _prmStatus, String _prmSuppCode, String _prmSuppName, String _prmRemark)
        {
            this.TransNmbr = _prmTransNo;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
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
