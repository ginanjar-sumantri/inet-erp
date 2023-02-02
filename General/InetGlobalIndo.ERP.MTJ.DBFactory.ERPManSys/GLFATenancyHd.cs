using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFATenancyHd
    {
        string _custName = "";
        string _termName = "";

        public GLFATenancyHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmCustName, string _prmTerm)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.CustName = _prmCustName;
            this.Term = _prmTerm;
        }

        public GLFATenancyHd(string _prmTransNmbr, DateTime _prmTransDate, char _prmStatus, string _prmCustName, string _prmCurrency, string _prmTermName, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.CustName = _prmCustName;
            this.CurrCode = _prmCurrency;
            this.TermName = _prmTermName;
            this.Remark = _prmRemark;
        }

        public GLFATenancyHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmCustName, string _prmCurrency, string _prmTermName, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.CustName = _prmCustName;
            this.CurrCode = _prmCurrency;
            this.TermName = _prmTermName;
            this.Remark = _prmRemark;
        }

        public string CustName
        {
            get
            {
                return this._custName;
            }
            set
            {
                this._custName = value;
            }
        }

        public string TermName
        {
            get
            {
                return this._termName;
            }
            set
            {
                this._termName = value;
            }
        }
    }
}
