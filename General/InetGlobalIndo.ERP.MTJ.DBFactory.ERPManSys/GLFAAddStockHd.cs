using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFAAddStockHd
    {
        private string _fileNoWIS = "";

        public GLFAAddStockHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmWISNo, string _prmFileNoWIS, string _prmReceiveBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.WISNo = _prmWISNo;
            this.FileNoWIS = _prmFileNoWIS;
            this.ReceiveBy = _prmReceiveBy;
        }

        public string FileNoWIS
        {
            get
            {
                return this._fileNoWIS;
            }

            set
            {
                this._fileNoWIS = value;
            }
        }
    }
}
