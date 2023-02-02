using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsClaim
    {
        private string _claimGroupName = "";

        public HRMMsClaim(string _prmClaimCode, string _prmClaimName, string _prmClaimGroup, string _prmClaimGroupName, int? _prmMaxTaken,
            Boolean? _prmfgCheckPlafon, Decimal? _prmEmployeePercentage, Decimal? _prmFamilyPercentage, Decimal? _prmReimbustEmployeePercentage,
            Decimal? _prmReimbustFamilyPercentage, String _prmCheckPlafonType)
        {
            this.ClaimCode = _prmClaimCode;
            this.ClaimName = _prmClaimName;
            this.ClaimGroup = _prmClaimGroup;
            this.ClaimGroupName = _prmClaimGroupName;
            this.MaxTaken = _prmMaxTaken;
            this.fgCheckPlafon = _prmfgCheckPlafon;
            this.EmployeePercentage = _prmEmployeePercentage;
            this.FamilyPercentage = _prmFamilyPercentage;
            this.ReimbustEmployeePercentage = _prmReimbustEmployeePercentage;
            this.ReimbustFamilyPercentage = _prmReimbustFamilyPercentage;
            this.CheckPlafonType = _prmCheckPlafonType;
        }

        public HRMMsClaim(string _prmClaimCode, string _prmClaimName)
        {
            this.ClaimCode = _prmClaimCode;
            this.ClaimName = _prmClaimName;
        }

        public string ClaimGroupName
        {
            get
            {
                return this._claimGroupName;
            }
            set
            {
                this._claimGroupName = value;
            }
        }
    }
}
