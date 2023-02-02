using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_RecruitmentRequest_Master_Skill
    {
        private string _skillName = "";

        public HRM_RecruitmentRequest_Master_Skill(Guid _prmRecruitmentRequest, Guid _prmSkillCode, string _prmSkillName, byte _prmMinExperience)
        {
            this.RecruitmentRequestCode = _prmRecruitmentRequest;
            this.SkillCode = _prmSkillCode;
            this.SkillName = _prmSkillName;
            this.MinExperience = _prmMinExperience;
        }

        public string SkillName
        {
            get
            {
                return this._skillName;
            }
            set
            {
                this._skillName = value;
            }
        }
    }
}
