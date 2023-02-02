using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYMsPremi
    {
        public PAYMsPremi(string _prmGroupBy, string _prmGroupCode, Decimal? _prmPremiRate, Decimal? _prmPotAbsence, int? _prmStartLC1, Decimal? _prmLCRate1, int? _prmStartLC2, Decimal? _prmLCRate2, int? _prmStartLC3, Decimal? _prmLCRate3)
        {
            this.GroupBy = _prmGroupBy;
            this.GroupCode = _prmGroupCode;
            this.PremiRate = _prmPremiRate;
            this.PotAbsence = _prmPotAbsence;
            this.StartLC1 = _prmStartLC1;
            this.LCRate1 = _prmLCRate1;
            this.StartLC2 = _prmStartLC2;
            this.LCRate2 = _prmLCRate2;
            this.StartLC3 = _prmStartLC3;
            this.LCRate3 = _prmLCRate3;
        }
    }
}
