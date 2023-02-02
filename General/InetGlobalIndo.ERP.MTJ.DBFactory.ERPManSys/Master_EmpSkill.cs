using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_EmpSkill
    {
        string _skillName = "";

        public Master_EmpSkill(Guid _prmEmpSkillCode, string _prmEmpNmbr, Guid _prmSkillCode, string _prmSkillName, byte _prmLvl, DateTime _prmLvlDate)
        {
            this.EmpSkillCode = _prmEmpSkillCode;
            this.EmpNmbr = _prmEmpNmbr;
            this.SkillCode = _prmSkillCode;
            this.SkillName = _prmSkillName;
            this.Lvl = _prmLvl;
            this.LevelDate = _prmLvlDate;
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
