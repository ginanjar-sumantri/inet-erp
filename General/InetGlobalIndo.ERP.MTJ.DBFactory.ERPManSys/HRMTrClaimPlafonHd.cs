using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrClaimPlafonHd
    {
        private string _claimName = "";

        public HRMTrClaimPlafonHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, DateTime _prmTransDate, DateTime _prmStartDate, DateTime _prmEndDate,
            String _prmClaimCode, String _prmClaimName, Decimal? _prmADefautAmount)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
            this.ClaimCode = _prmClaimCode;
            this.ClaimName = _prmClaimName;
            this.DefautAmount = _prmADefautAmount;
        }

        public HRMTrClaimPlafonHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public string ClaimName
        {
            get
            {
                return this._claimName;
            }
            set
            {
                this._claimName = value;
            }
        }
    }
}
