using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYMsTunjangan
    {
        public PAYMsTunjangan(string _prmTunjanganType, int _prmStartYear, int _prmEndYear, String _prmBaseOn,
            Decimal _prmFormula, String _prmRemark)
        {
            this.TunjanganType = _prmTunjanganType;
            this.StartYear = _prmStartYear;
            this.EndYear = _prmEndYear;
            this.BaseOn = _prmBaseOn;
            this.Formula = _prmFormula;
            this.Remark = _prmRemark;
        }
    }
}
