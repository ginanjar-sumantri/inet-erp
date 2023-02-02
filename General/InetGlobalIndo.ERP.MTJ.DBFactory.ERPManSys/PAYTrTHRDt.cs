using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrTHRDt
    {
        private string _empName = "";

        public PAYTrTHRDt(String _prmTransNmbr, String _prmEmpNumb, String _prmEmpName, String _prmCurrCode, Decimal _prmTotalGP,
            Decimal _prmTotalTT, Decimal _prmMasaKerja, Decimal _prmTotalTHR, Decimal? _prmTotalPaid, Decimal? _prmPPh21, Decimal? _prmTotal)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.CurrCode = _prmCurrCode;
            this.TotalGP = _prmTotalGP;
            this.TotalTT = _prmTotalTT;
            this.MasaKerja = _prmMasaKerja;
            this.TotalTHR = _prmTotalTHR;
            this.TotalPaid = _prmTotalPaid;
            this.PPh21 = _prmPPh21;
            this.Total = _prmTotal;
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
