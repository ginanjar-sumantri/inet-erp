using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
//using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Settings
{
    public sealed class User_EmployeeBL : Base
    {
        public User_EmployeeBL()
        {

        }

        #region User_Employee

        public int RowsCount
        {
            get
            {
                return this.db.User_Employees.Count();
            }
        }

        public List<User_Employee> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<User_Employee> _result = new List<User_Employee>();

            try
            {
                var _query = (
                                from _userEmployee in this.db.User_Employees
                                select new
                                {
                                    UserId = _userEmployee.UserId,
                                    EmployeeId = _userEmployee.EmployeeId
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { UserId = this._guid, EmployeeId = this._string });

                    _result.Add(new User_Employee(_row.UserId, _row.EmployeeId));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<User_Employee> GetList()
        {
            List<User_Employee> _result = new List<User_Employee>();

            try
            {
                var _query = (
                                from _userEmployee in this.db.User_Employees
                                select new
                                {
                                    UserId = _userEmployee.UserId,
                                    EmployeeId = _userEmployee.EmployeeId
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { UserId = this._guid, EmployeeId = this._string });

                    _result.Add(new User_Employee(_row.UserId, _row.EmployeeId));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public User_Employee GetSingle(string _prmCode)
        {
            User_Employee _result = null;

            try
            {
                _result = this.db.User_Employees.Single(_temp => _temp.UserId == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetEmployeeIDByUserName(string _prmUserName)
        {
            string _result = "";

            UserBL _userBL = new UserBL();

            _result = this.GetEmployeeIdByCode(_userBL.GetUserIDByName(_prmUserName).ToString());

            return _result;
        }

        public string GetEmployeeIdByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _userEmployee in this.db.User_Employees
                                where _userEmployee.UserId == new Guid(_prmCode)
                                select new
                                {
                                    EmployeeId = _userEmployee.EmployeeId
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.EmployeeId;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Guid GetUserIdEmpId(string _prmEmpId)
        {
            Guid _result = new Guid() ;

            try
            {
                var _query = (
                                from _userEmployee in this.db.User_Employees
                                where _userEmployee.EmployeeId == _prmEmpId
                                select new
                                {
                                    UserId = _userEmployee.UserId
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.UserId;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    User_Employee _userEmployee = this.db.User_Employees.Single(_temp => Convert.ToString(_temp.UserId).Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.User_Employees.DeleteOnSubmit(_userEmployee);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string _prmCode)
        {
            bool _result = false;

            try
            {
                User_Employee _userEmployee = this.db.User_Employees.Single(_temp => Convert.ToString(_temp.UserId).Trim().ToLower() == _prmCode.Trim().ToLower());

                this.db.User_Employees.DeleteOnSubmit(_userEmployee);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(User_Employee _prmUser_Employee)
        {
            bool _result = false;

            try
            {
                this.db.User_Employees.InsertOnSubmit(_prmUser_Employee);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(User_Employee _prmUser_Employee)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsUserEmpExists(string _prmUserEmpId)
        {
            bool _result = false;

            try
            {
                var _query = from _userEmp in this.db.User_Employees
                             where (_userEmp.UserId == new Guid(_prmUserEmpId))
                             select new
                             {
                                 _userEmp.UserId
                             };

                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~User_EmployeeBL()
        {
        }
    }
}
