using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITV.DataAccess.ITVDatabase;
using ITV.Common;

namespace ITV.BusinessRule
{
    public class RoleBL : Base
    {
        public RoleBL() { }

        public UsRMSRoles GetSingleRole(String _prmRoleID, ref String _errMessage)
        {
            UsRMSRoles _result = new UsRMSRoles();

            try
            {
                _result = this.db.UsRMSRoles.Single(_temp => _temp.UsrmSRolesID.ToString().ToLower() == _prmRoleID.ToLower());
            }
            catch (Exception Ex)
            {
                _errMessage = Ex.Message;
            }

            return _result;
        }

        public List<UsRMSRoles> GetListRole(ref String _errMessage)
        {
            List<UsRMSRoles> _result = new List<UsRMSRoles>();

            try
            {
                var _query = (
                                from _role in this.db.UsRMSRoles
                                select _role
                             ).ToList();

                _result = _query;
            }
            catch (Exception Ex)
            {
                _errMessage = Ex.Message;
            }

            return _result;
        }

        public List<UsRMSRoles> GetListRoleForDDL(ref String _errMessage)
        {
            List<UsRMSRoles> _result = new List<UsRMSRoles>();

            try
            {
                var _query = (
                                from _role in this.db.UsRMSRoles
                                select new
                                {
                                    UsrmSRolesID = _role.UsrmSRolesID,
                                    RoleName = _role.RoleName
                                }
                             );

                foreach (var _row in _query)
                {
                    _result.Add(new UsRMSRoles(_row.UsrmSRolesID, _row.RoleName));
                }
            }
            catch (Exception Ex)
            {
                _errMessage = Ex.Message;
            }

            return _result;
        }

        public Boolean AddRole(UsRMSRoles _prmUSRMSRoles, ref String _errMessage)
        {
            Boolean _result = false;

            try
            {
                this.db.UsRMSRoles.InsertOnSubmit(_prmUSRMSRoles);
                this.db.SubmitChanges();
            }
            catch (Exception Ex)
            {
                _errMessage = Ex.Message;
            }

            return _result;
        }

        public Boolean EditRole(UsRMSRoles _prmUSRMSRoles, ref String _errMessage)
        {
            Boolean _result = false;

            try
            {
                this.db.SubmitChanges();
            }
            catch (Exception Ex)
            {
                _errMessage = Ex.Message;
            }

            return _result;
        }

        public Boolean DeleteRole(String[] _prmRoleID, ref String _errMessage)
        {
            Boolean _result = false;

            try
            {
                foreach (var _item in _prmRoleID)
                {
                    var _query = (
                                    from _roles in this.db.UsRMSRoles
                                    where _roles.UsrmSRolesID.ToString().ToLower() == _item.ToString().ToLower()
                                    select _roles
                                 );

                    this.db.UsRMSRoles.DeleteAllOnSubmit(_query);
                    this.db.SubmitChanges();
                }
            }
            catch (Exception Ex)
            {
                _errMessage = Ex.Message;
            }

            return _result;
        }

        ~RoleBL() { }
    }
}
