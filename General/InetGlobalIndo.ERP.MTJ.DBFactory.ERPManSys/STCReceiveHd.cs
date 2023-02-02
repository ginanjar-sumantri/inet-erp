using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCReceiveHd
    {
        private string _suppName = "";
        private string _fileNo = "";

        public STCReceiveHd(string _prmTransNmbr,string _prmFileNmbr ,DateTime _prmTransDate, char _prmStatus, string _prmSuppCode,string _prmSuppName, string _prmPONo, string _prmFileNo, string _prmWrhsCode, string _prmWrhsSubLed, char _prmWrhsFgSubLed)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
            this.PONo = _prmPONo;
            this.FileNo = _prmFileNo;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsSubLed = _prmWrhsSubLed;
            this.WrhsFgSubLed = _prmWrhsFgSubLed;
        }

        public STCReceiveHd(string _prmTransNmbr, string _prmFileNmbr)
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
