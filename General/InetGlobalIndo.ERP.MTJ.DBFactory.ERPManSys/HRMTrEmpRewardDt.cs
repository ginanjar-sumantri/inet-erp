using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrEmpRewardDt
    {
        private string _empName = "";
        private string _jobTitleName = "";
        private string _rewardName = "";

        public HRMTrEmpRewardDt(String _prmTransNmbr, String _prmEmployeeId, String _prmEmpName, String _prmJobTitle,
            String _prmJobTitleName, String _prmReward, String _prmRewardName, String _prmRewardNote, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmployeeId;
            this.EmpName = _prmEmpName;
            this.JobTitle = _prmJobTitle;
            this.JobTitleName = _prmJobTitleName;
            this.Reward = _prmReward;
            this.RewardName = _prmRewardName;
            this.RewardNote = _prmRewardNote;
            this.Remark = _prmRemark;
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

        public string JobTitleName
        {
            get
            {
                return this._jobTitleName;
            }
            set
            {
                this._jobTitleName = value;
            }
        }

        public string RewardName
        {
            get
            {
                return this._rewardName;
            }
            set
            {
                this._rewardName = value;
            }
        }
    }
}
