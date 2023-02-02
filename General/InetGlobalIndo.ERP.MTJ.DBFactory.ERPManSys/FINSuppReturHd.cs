using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINSuppReturHd 
    {
        private string _termName = "";
        private string _suppName = "";

        public FINSuppReturHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, string _prmCurrCode, char _prmStatus, string _prmSuppCode, string _prmSuppName, string _prmTerm, string _prmTermName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.CurrCode = _prmCurrCode;
            this.Status = _prmStatus;
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
            this.Term = _prmTerm;
            this.TermName = _prmTermName;
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
