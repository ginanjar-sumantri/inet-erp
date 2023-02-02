using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_EmpFamily
    {
        public Master_EmpFamily(Guid _prmEmpFamilyCode, string _prmEmpNmbr, string _prmFamilyName, string _prmFamilyStatus, bool _prmGender, string _prmAddress1, string _prmAddress2, string _prmPhone, string _prmHP)
        {
            this.EmpFamilyCode = _prmEmpFamilyCode;
            this.EmpNumb = _prmEmpNmbr;
            this.FamilyName = _prmFamilyName;
            this.FamilyStatus = _prmFamilyStatus;
            this.Gender = _prmGender;
            this.Address1 = _prmAddress1;
            this.Address2 = _prmAddress2;
            this.Phone = _prmPhone;
            this.HP = _prmHP;
        }
    }
}
