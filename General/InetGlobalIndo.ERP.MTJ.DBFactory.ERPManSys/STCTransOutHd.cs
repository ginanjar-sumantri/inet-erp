using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCTransOutHd
    {
        private string _wrhsSrcName = "";
        private string _wrhsDestName = "";
        private string _fileNoRequest = "";

        public STCTransOutHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmTransReff, string _prmFileNoRequest, string _prmWrhsSrc, string _prmWrhsSrcName, string _prmWrhsDest, string _prmWrhsDestName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.RequestNo = _prmTransReff;
            this.FileNoRequest = _prmFileNoRequest;
            this.WrhsSrc = _prmWrhsSrc;
            this.WrhsSrcName = _prmWrhsSrcName;
            this.WrhsDest = _prmWrhsDest;
            this.WrhsDestName = _prmWrhsDestName;
        }

        public STCTransOutHd(string _prmTransNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
        }

        public STCTransOutHd(string _prmTransNmbr, string _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
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

        public string FileNoRequest
        {
            get
            {
                return this._fileNoRequest;
            }
            set
            {
                this._fileNoRequest = value;
            }
        }
    }
}
