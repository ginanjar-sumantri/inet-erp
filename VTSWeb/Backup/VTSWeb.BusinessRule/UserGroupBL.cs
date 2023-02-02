using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using VTSWeb.Database;
using System.Diagnostics;
using System.Data.Linq.SqlClient;

namespace VTSWeb.BusinessRule
{
    public sealed class UserGroupBL : Base
    {
        public UserGroupBL()
        {
        }
        ~UserGroupBL()
        {
        }

        #region UserGroup
        public int RowsCount(String _prmAssGroup, String _prmKeyword)
        {
            int _result = 0;

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmAssGroup == "UserGroupCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }

            if (_prmAssGroup == "UserGroupName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result = (from _MsUserGroup in this.db.MsUserGroups
                       where (SqlMethods.Like(_MsUserGroup.UserGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_MsUserGroup.UserGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                       select _MsUserGroup.UserGroupCode).Count();
            return _result;
        }

        public List<MsUserGroup> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<MsUserGroup> _result = new List<MsUserGroup>();

            try
            {
                var _query = (
                                from _MsUserGroup in this.db.MsUserGroups
                                orderby _MsUserGroup.UserGroupName ascending
                                select new
                                {
                                    UserGroupCode = _MsUserGroup.UserGroupCode,
                                    UserGroupName = _MsUserGroup.UserGroupName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsUserGroup(_row.UserGroupCode, _row.UserGroupName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsUserGroup> GetList(int _prmReqPage, int _prmPageSize, String _prmAssGroup, String _prmKeyword)
        {
            List<MsUserGroup> _result = new List<MsUserGroup>();

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmAssGroup == "UserGroupCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            if (_prmAssGroup == "UserGroupName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";

            }


            try
            {
                var _query = (
                                from _MsUserGroup in this.db.MsUserGroups
                                where (SqlMethods.Like(_MsUserGroup.UserGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_MsUserGroup.UserGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                //&& (SqlMethods.Like(_msModule.BusinessPhone.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))

                                orderby _MsUserGroup.UserGroupCode ascending
                                select new
                                {
                                    UserGroupCode = _MsUserGroup.UserGroupCode,
                                    UserGroupName = _MsUserGroup.UserGroupName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsUserGroup(_row.UserGroupCode, _row.UserGroupName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsUserGroup> GetList()
        {
            List<MsUserGroup> _result = new List<MsUserGroup>();

            try
            {
                var _query = (
                                from _MsUserGroup in this.db.MsUserGroups
                                orderby _MsUserGroup.UserGroupName ascending
                                select new
                                {
                                    UserGroupCode = _MsUserGroup.UserGroupCode,
                                    UserGroupName = _MsUserGroup.UserGroupName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsUserGroup(_row.UserGroupCode, _row.UserGroupName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public MsUserGroup GetSingle(String _prmUserGroupCode)
        {
            MsUserGroup _result = null;

            try
            {
                _result = this.db.MsUserGroups.Single(_temp => _temp.UserGroupCode == _prmUserGroupCode);
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String GetUserGroupNameByCode(String _prmUserGroupCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _MsUserGroup in this.db.MsUserGroups
                                where _MsUserGroup.UserGroupCode == _prmUserGroupCode
                                select new
                                {
                                    UserGroupName = _MsUserGroup.UserGroupName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.UserGroupName;
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool DeleteMulti(String[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsUserGroup _MsUserGroup = this.db.MsUserGroups.Single(_temp => _temp.UserGroupCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsUserGroups.DeleteOnSubmit(_MsUserGroup);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool Add(MsUserGroup _prmMsUserGroup)
        {
            bool _result = false;

            try
            {
                this.db.MsUserGroups.InsertOnSubmit(_prmMsUserGroup);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool Edit(MsUserGroup _prmMsUserGroup)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        #endregion

        #region UserGroupDt
        public List<MsUserGroupDt> GetListUserGroupDt(string _prmUserGroupCode)
        {
            List<MsUserGroupDt> _result = new List<MsUserGroupDt>();

            try
            {
                var _query = (
                                from _MsUserGroupDt in this.db.MsUserGroupDts
                                join _msEmployee in this.db.MsEmployees
                                    on _MsUserGroupDt.EmpNumb equals _msEmployee.EmpNumb
                                where _MsUserGroupDt.UserGroupCode == _prmUserGroupCode
                                orderby _msEmployee.EmpName
                                select new
                                {
                                    UserGroupCode = _MsUserGroupDt.UserGroupCode,
                                    EmpNumb = _MsUserGroupDt.EmpNumb,
                                    EmpName = _msEmployee.EmpName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsUserGroupDt(_row.UserGroupCode, _row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public MsUserGroupDt GetSingleUserGroupDt(string _prmUserGroupCode, string _prmEmpNumb)
        {
            MsUserGroupDt _result = null;

            try
            {
                _result = this.db.MsUserGroupDts.Single(_temp => _temp.UserGroupCode == _prmUserGroupCode && _temp.EmpNumb == _prmEmpNumb);
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool DeleteMultiUserGroupDt(string[] _prmEmpNumb, string _prmUserGroupCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpNumb.Length; i++)
                {
                    MsUserGroupDt _MsUserGroupDt = this.db.MsUserGroupDts.Single(_temp => _temp.EmpNumb.Trim().ToLower() == _prmEmpNumb[i].Trim().ToLower() && _temp.UserGroupCode.Trim().ToLower() == _prmUserGroupCode.Trim().ToLower());

                    this.db.MsUserGroupDts.DeleteOnSubmit(_MsUserGroupDt);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool AddUserGroupDt(MsUserGroupDt _prmMsUserGroupDt)
        {
            bool _result = false;

            try
            {
                this.db.MsUserGroupDts.InsertOnSubmit(_prmMsUserGroupDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool EditUserGroupDt(MsUserGroupDt _prmMsUserGroupDt)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsUserGroup> GetGroupListForDDL()
        {
            List<MsUserGroup> _result = new List<MsUserGroup>();

            try
            {
                var _query = (
                                from _msGroup in this.db.MsUserGroups
                                //where _msCustomer.CompanyID == _prmCompanyID
                                select new
                                {
                                    UserGroupCode = _msGroup.UserGroupCode,
                                    UserGroupName = _msGroup.UserGroupName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsUserGroup(_row.UserGroupCode, _row.UserGroupName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        #endregion
    }
}
