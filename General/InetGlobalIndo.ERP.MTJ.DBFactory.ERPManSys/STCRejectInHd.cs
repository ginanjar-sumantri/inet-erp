using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCRejectInHd
    {
        private string _suppName = "";
        private string _wrhsName = "";

        public STCRejectInHd(string _prmTransNmbr,string _prmFileNmbr ,DateTime _prmTransDate, char _prmStatus, string _prmTransReff, string _prmSuppCode, string _prmSuppName, string _prmWrhsCode, string _prmWrhsName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.TransReff = _prmTransReff;
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
        }
        public STCRejectInHd(string _prmTransNmbr, string _prmFileNmbr)
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
