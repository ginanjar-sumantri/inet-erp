using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrSalaryEmp
    {
        private string _fileNmbr = "";
        private string _empName = "";

        public PAYTrSalaryEmp(String _prmTransNmbr, String _prmFileNmbr, String _prmEmpNumb, String _prmEmpName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
        }

        public PAYTrSalaryEmp(String _prmTransNmbr, String _prmEmpNumb, String _prmEmpName, Decimal? _prmAmountSalary, Decimal? _prmAmountGP,
            Decimal? _prmAmountPesangon, Decimal? _prmAmountPot, Decimal? _prmAmountPPh, Decimal? _prmAmountT2, Decimal? _prmAmountT3)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.AmountSalary = _prmAmountSalary;
            this.AmountGP = _prmAmountGP;
            this.AmountPesangon = _prmAmountPesangon;
            this.AmountPot = _prmAmountPot;
            this.AmountPPh = _prmAmountPPh;
            this.AmountT2 = _prmAmountT2;
            this.AmountT3 = _prmAmountT3;
        }

        public PAYTrSalaryEmp(String _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
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
