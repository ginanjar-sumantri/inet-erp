using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINDPSuppHd
    {
        private string _suppName = "";

        public FINDPSuppHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, string _prmCurrCode, char _prmStatus, string _prmSuppCode, string _prmSuppName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.CurrCode = _prmCurrCode;
            this.Status = _prmStatus;
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
        }

        public FINDPSuppHd(string _prmTransNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
        }

        public FINDPSuppHd(string _prmTransNmbr,string _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
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
