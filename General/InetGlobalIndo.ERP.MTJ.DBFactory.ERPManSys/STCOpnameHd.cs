using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCOpnameHd
    {
        private string _wrhsName = "";

        public STCOpnameHd(string _prmTransNmbr,string _prmFileNmbr ,DateTime _prmTransDate, char _prmStatus, string _prmWrhsCode, string _prmWrhsName, char? _prmWrhsFgSubLed, string _prmWrhsSubLed, string _prmOperator, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
            this.WrhsFgSubLed = _prmWrhsFgSubLed;
            this.WrhsSubLed = _prmWrhsSubLed;
            this.Operator = _prmOperator;
            this.Remark = _prmRemark;
        }

        public STCOpnameHd(string _prmTransNmbr, string _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
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
