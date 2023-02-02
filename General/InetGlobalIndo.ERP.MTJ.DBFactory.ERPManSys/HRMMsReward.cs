using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsReward
    {
        private string _RewardGroupName = "";

        public HRMMsReward(string _prmRewardCode, string _prmRewardName)
        {
            this.RewardCode = _prmRewardCode;
            this.RewardName = _prmRewardName;
        }
    }
}
