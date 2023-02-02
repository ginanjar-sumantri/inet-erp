using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCTransInHd
    {
        private string _wrhsSrcName = "";
        private string _wrhsDestName = "";
        private string _fileNoTransReff = "";
        public STCTransInHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmTransReff, string _prmFileNoTransReff, string _prmWrhsSrc, string _prmWrhsSrcName, string _prmWrhsDest, string _prmWrhsDestName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.TransReff = _prmTransReff;
            this.FileNoTransReff = _prmFileNoTransReff;
            this.WrhsSrc = _prmWrhsSrc;
            this.WrhsSrcName = _prmWrhsSrcName;
            this.WrhsDest = _prmWrhsDest;
            this.WrhsDestName = _prmWrhsDestName;
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

        public string FileNoTransReff
        {
            get
            {
                return this._fileNoTransReff;
            }
            set
            {
                this._fileNoTransReff = value;
            }
        }
    }
}
