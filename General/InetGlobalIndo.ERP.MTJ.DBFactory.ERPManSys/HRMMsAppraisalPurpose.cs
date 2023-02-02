using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsAppraisalPurpose
    {
        private string _appraisalName = "";
        private string _purposeName = "";

        public HRMMsAppraisalPurpose(string _prmPurposeCode, string _prmPurposeName, string _prmAppraisalCode, string _prmAppraisalName)
        {
            this.PurposeCode = _prmPurposeCode;
            this.PurposeName = _prmPurposeName;
            this.AppraisalCode = _prmAppraisalCode;
            this.AppraisalName = _prmAppraisalName;
        }

        public string PurposeName
        {
            get
            {
                return this._purposeName;
            }
            set
            {
                this._purposeName = value;
            }
        }

        public string AppraisalName
        {
            get
            {
                return this._appraisalName;
            }
            set
            {
                this._appraisalName = value;
            }
        }
    }
}
