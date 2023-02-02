using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
   public partial class FINDPCustHd
    {
        private string _custName = "";

        public FINDPCustHd(string _prmTransNmbr, DateTime _prmTransDate, string _prmCurrCode, char _prmStatus, string _prmCustCode, string _prmCustName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.TransDate = _prmTransDate;
            this.CurrCode = _prmCurrCode;
            this.Status = _prmStatus;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
        }

        public FINDPCustHd(string _prmTransNmbr,string _prmFileNmbr ,DateTime _prmTransDate, string _prmCurrCode, char _prmStatus, string _prmCustCode, string _prmCustName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.CurrCode = _prmCurrCode;
            this.Status = _prmStatus;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
        }


        public string CustName
        {
            get
            {
                return this._custName;
            }
            set
            {
                this._custName = value;
            }
        }
    }
}
