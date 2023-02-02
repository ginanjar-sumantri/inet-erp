using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrAppraisalDt
    {
        private string _appraisalName = "";

        public HRMTrAppraisalDt(String _prmTransNmbr, String _prmAppraisalCode, String _prmAppraisalName, Decimal _prmBobot, Decimal _prmResult, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.AppraisalCode = _prmAppraisalCode;
            this.AppraisalName = _prmAppraisalName;
            this.Bobot = _prmBobot;
            this.Result = _prmResult;
            this.Remark = _prmRemark;
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
