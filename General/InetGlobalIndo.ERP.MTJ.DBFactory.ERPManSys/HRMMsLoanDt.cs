using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsLoanDt
    {
        private string _jobLevelName = "";

        public HRMMsLoanDt(string _prmLoanCode, string _prmJobLevel, string _prmJobLevelName, decimal? _prmAmount)
        {
            this.LoanCode = _prmLoanCode;
            this.JobLevel = _prmJobLevel;
            this.JobLevelName = _prmJobLevelName;
            this.Amount = _prmAmount;
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
    }
}
