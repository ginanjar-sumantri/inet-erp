using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITV.DataAccess;
using ITV.DataAccess.ITVDatabase;

namespace ITV.BusinessRule
{
    public class UserBL : Base
    {
        public Boolean ValidateUser(String _username, String _password)
        {
            Boolean _result = false;

            var _query = (
                            from _usr in this.db.UsRMSUsers
                            where _usr.UserName == _username
                            && _usr.Password == _password
                            select _usr
                         );
            if (_query.Count() > 0) { return true; }
            //UsRMSUsers _user = this.db.UsRMSUsers.Single(_temp => _temp.UserName == _username && _temp.Password == _password);
            //if (_user != null) return true;

            return _result;
        }
    }
}
