using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_SkillType
    {
        public Master_SkillType(Guid _prmSkillTypeCode, string _prmSkillTypeName, string _prmSkillTypeDescription)
        {
            this.SkillTypeCode = _prmSkillTypeCode;
            this.SkillTypeName = _prmSkillTypeName;
            this.SkillTypeDescription = _prmSkillTypeDescription;
        }

        public Master_SkillType(Guid _prmSkillTypeCode, string _prmSkillTypeName)
        {
            this.SkillTypeCode = _prmSkillTypeCode;
            this.SkillTypeName = _prmSkillTypeName;
        }
    }
}
