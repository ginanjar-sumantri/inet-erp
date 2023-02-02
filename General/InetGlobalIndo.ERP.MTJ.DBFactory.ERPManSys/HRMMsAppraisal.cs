using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsAppraisal
    {
        private string _appraisalGrpName = "";

        public HRMMsAppraisal(string _prmAppraisalCode, string _prmAppraisalName, string _prmAppraisalGrp, string _prmAppraisalGrpName, Decimal _prmBobot)
        {
            this.AppraisalCode = _prmAppraisalCode;
            this.AppraisalName = _prmAppraisalName;
            this.AppraisalGroup = _prmAppraisalGrp;
            this.AppraisalGrpName = _prmAppraisalGrpName;
            this.Bobot = _prmBobot;
        }

        public HRMMsAppraisal(string _prmAppraisalCode, string _prmAppraisalName)
        {
            this.AppraisalCode = _prmAppraisalCode;
            this.AppraisalName = _prmAppraisalName;
        }

        public string AppraisalGrpName
        {
            get
            {
                return this._appraisalGrpName;
            }
            set
            {
                this._appraisalGrpName = value;
            }
        }
    }
}
