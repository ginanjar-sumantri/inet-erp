using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrPPhYearDt
    {
        private string _empName = "";

        public PAYTrPPhYearDt(int _prmYear, String _prmEmpNumb, string _prmEmpName, DateTime _prmStartDate, DateTime _prmEndDate, String _prmCurrCode,
            Decimal _prmYearBruto, Decimal _prmYearPremi, Decimal _prmYearIuranJbt, Decimal _prmYearNetto, Decimal _prmYearPTKP, Decimal _prmYearPKP,
            Decimal _prmYearPPh)
        {
            this.Year = _prmYear;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
            this.CurrCode = _prmCurrCode;
            this.YearBruto = _prmYearBruto;
            this.YearPremi = _prmYearPremi;
            this.YearIuranJbt = _prmYearIuranJbt;
            this.YearNetto = _prmYearNetto;
            this.YearPTKP = _prmYearPTKP;
            this.YearPKP = _prmYearPKP;
            this.YearPPh = _prmYearPPh;
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
