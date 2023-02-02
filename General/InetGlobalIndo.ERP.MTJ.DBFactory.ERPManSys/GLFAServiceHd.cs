using System;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFAServiceHd
    {
        string _faName = "";

        public GLFAServiceHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmFACode, string _prmFAName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
        }

        public string FAName
        {
            get
            {
                return this._faName;
            }
            set
            {
                this._faName = value;
            }
        }
    }
}
