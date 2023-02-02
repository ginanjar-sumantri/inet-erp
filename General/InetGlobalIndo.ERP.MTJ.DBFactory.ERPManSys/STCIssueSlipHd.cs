using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCIssueSlipHd
    {
        private string _wrhsName = "";
        private string _fileNo = "";

        public STCIssueSlipHd(string _prmTransNmbr,string _prmFileNmbr ,DateTime _prmTransDate, char _prmStatus, string _prmWrhsCode, string _prmWrhsName, string _prmRequestNo, string _prmFileNo, string _prmRequestBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
            this.RequestNo = _prmRequestNo;
            this.FileNo = _prmFileNo;
            this.RequestBy = _prmRequestBy;
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
