using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_Skill
    {
        string _skillTypeName = "";

        public Master_Skill(Guid _prmSkillCode, Guid _prmSkillTypeCode, string _prmSkillTypeName, string _prmSkillName, string _prmSkillDescription)
        {
            this.SkillCode = _prmSkillCode;
            this.SkillTypeCode = _prmSkillTypeCode;
            this.SkillTypeName = _prmSkillTypeName;
            this.SkillName = _prmSkillName;
            this.SkillDescription = _prmSkillDescription;
        }

        public Master_Skill(Guid _prmSkillCode, string _prmSkillName)
        {
            this.SkillCode = _prmSkillCode;
            this.SkillName = _prmSkillName;
        }

        public string SkillTypeName
        {
            get
            {
                return this._skillTypeName;
            }
            set
            {
                this._skillTypeName = value;
            }
        }
    }
}
