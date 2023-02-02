using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_LimitAuthorization
    {
        private string _transTypeName = "";
        private string _roleName = "";

        public Master_LimitAuthorization(Guid _prmRoleID, string _prmRoleName, string _prmTransTypeCode, string _prmTransTypeName, decimal _prmLimit)
        {
            this.RoleID = _prmRoleID;
            this.RoleName = _prmRoleName;
            this.TransTypeCode = _prmTransTypeCode;
            this.TransTypeName = _prmTransTypeName;
            this.Limit = _prmLimit;
        }

        public string RoleName
        {
            get
            {
                return this._roleName;
            }
            set
            {
                this._roleName = value;
            }
        }

        public string TransTypeName
        {
            get
            {
                return this._transTypeName;
            }
            set
            {
                this._transTypeName = value;
            }
        }
    }
}
