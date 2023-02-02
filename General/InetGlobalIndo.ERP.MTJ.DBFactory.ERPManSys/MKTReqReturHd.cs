using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MKTReqReturHd
    {
        private string _custName = "";

        public MKTReqReturHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, string _prmCurrCode, char _prmStatus, string _prmCustCode, string _prmCustName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.CurrCode = _prmCurrCode;
            this.Status = _prmStatus;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
        }

        public MKTReqReturHd(string _prmTransNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
        }

        public MKTReqReturHd(String _prmTransNmbr, String _prmFileNmbr)
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
