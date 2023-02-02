using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrSalaryHd
    {
        private string _methodName = "";

        public PAYTrSalaryHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, int _prmYear,
            int _prmMonth, String _prmMethod, String _prmMethodName, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.Year = _prmYear;
            this.Month = _prmMonth;
            this.Method = _prmMethod;
            this.MethodName = _prmMethodName;
            this.Remark = _prmRemark;
        }

        public PAYTrSalaryHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public string MethodName
        {
            get
            {
                return this._methodName;
            }
            set
            {
                this._methodName = value;
            }
        }
    }
}
