using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsAppraisalJobLvl
    {
        private string _appraisalName = "";
        private string _jobLevelName = "";

        public HRMMsAppraisalJobLvl(string _prmJobLevel, string _prmJobLevelName, string _prmAppraisalCode, string _prmAppraisalName)
        {
            this.JobLevel = _prmJobLevel;
            this.JobLevelName = _prmJobLevelName;
            this.AppraisalCode = _prmAppraisalCode;
            this.AppraisalName = _prmAppraisalName;
        }

        public string JobLevelName
        {
            get
            {
                return this._jobLevelName;
            }
            set
            {
                this._jobLevelName = value;
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
