using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCIssueToFAHd
    {
        private string _wrhsName = "";
        private string _fileNo = "";

        public STCIssueToFAHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmWrhsCode, string _prmWrhsName, string _prmReqAssetNo, string _prmFileNo)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
            this.ReqAssetNo = _prmReqAssetNo;
            this.FileNo = _prmFileNo;
        }

        public STCIssueToFAHd(string _prmTransNmbr, string _prmFileNmbr)
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

        public string FileNo
        {
            get
            {
                return this._fileNo;
            }
            set
            {
                this._fileNo = value;
            }
        }
    }
}
