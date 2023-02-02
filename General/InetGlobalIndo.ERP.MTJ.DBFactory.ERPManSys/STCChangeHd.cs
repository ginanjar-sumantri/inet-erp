using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCChangeHd
    {
        private string _wrhsSrcName = "";
        private string _wrhsDestName = "";

        public STCChangeHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmWrhsSrc, string _prmWrhsSrcName, string _prmWrhsDest, string _prmWrhsDestName, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.WrhsSrc = _prmWrhsSrc;
            this.WrhsSrcName = _prmWrhsSrcName;
            this.WrhsDest = _prmWrhsDest;
            this.WrhsDestName = _prmWrhsDestName;
            this.Remark = _prmRemark;
        }

        public string WrhsSrcName
        {
            get
            {
                return this._wrhsSrcName;
            }
            set
            {
                this._wrhsSrcName = value;
            }
        }

        public string WrhsDestName
        {
            get
            {
                return this._wrhsDestName;
            }
            set
            {
                this._wrhsDestName = value;
            }
        }
    }
}
