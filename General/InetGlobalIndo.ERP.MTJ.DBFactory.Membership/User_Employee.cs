using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class User_Employee
    {
        public User_Employee(Guid _prmUserId, string _prmEmployeeId)
        {
            this.UserId = _prmUserId;
            this.EmployeeId = _prmEmployeeId;
        }
    }
}
