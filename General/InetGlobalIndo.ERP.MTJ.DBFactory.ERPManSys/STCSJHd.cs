using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCSJHd
    {
        private string _custName = "";
        private string _wrhsName = "";

        public STCSJHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmSONo, string _prmCustCode, string _prmCustName, string _prmWrhsCode, string _prmWrhsName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.SONo = _prmSONo;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
        }

        public STCSJHd(string _prmSJNo, string _prmFileNmbr)
        {
            TransNmbr = _prmSJNo;
            FileNmbr = _prmFileNmbr;
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

        public string WrhsName
        {
            get
            {
                return this._wrhsName;
            }
            set
            {
                this._wrhsName = value;
            }
        }
    }
}
