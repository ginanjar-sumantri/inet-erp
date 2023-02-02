using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINDPCustList
    {
        private string _custName = "";

        public FINDPCustList(string _prmTransNmbr,string _prmFileNmbr ,DateTime _prmTransDate, string _prmCustCode, string _prmCustName, string _prmCurrCode, char _prmStatus)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.CurrCode = _prmCurrCode;
            this.Status = _prmStatus;
        }

        public FINDPCustList(string _prmTransNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
        }

        public FINDPCustList(string _prmTransNmbr,string _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
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
