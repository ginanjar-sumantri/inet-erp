using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrSalaryPosting
    {
        private string _fileNmbr = "";
        private string _empName = "";

        public PAYTrSalaryPosting(String _prmTransNmbr, String _prmFileNmbr, String _prmEmpNumb, String _prmEmpName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
        }

        public PAYTrSalaryPosting(String _prmTransNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
        }

        public PAYTrSalaryPosting(String _prmTransNmbr, Decimal? _prmAmountPay, Decimal? _prmAmountSalary, String _prmEmpNumb)
        {
            this.TransNmbr = _prmTransNmbr;
            this.AmountPay = _prmAmountPay;
            this.AmountSalary = _prmAmountSalary;
            this.EmpNumb = _prmEmpNumb;
        }

        public string FileNmbr
        {
            get
            {
                return this._fileNmbr;
            }
            set
            {
                this._fileNmbr = value;
            }
        }

        public string EmpName
        {
            get
            {
                return this._empName;
            }
            set
            {
                this._empName = value;
            }
        }
    }
}
