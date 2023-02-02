using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using VTSWeb.Database;

namespace VTSWeb.BusinessRule
{
    public sealed class EmployeeBL : Base
    {
        public EmployeeBL()
        {
        }
        ~EmployeeBL()
        {
        }

        #region Employee
        public int RowsCount(String _prmEmployee, String _prmKeyword)
        {
            int _result = 0;

            String _pattern1 = "%%";
            String _pattern2 = "%%";
            String _pattern3 = "%%";

            if (_prmEmployee == "EmpNumb")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            if (_prmEmployee == "EmpName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            if (_prmEmployee == "JobTitle")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            _result = (
                        from _msEmployee in this.db.MsEmployees
                        where (SqlMethods.Like(_msEmployee.EmpNumb.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            && (SqlMethods.Like(_msEmployee.JobTitle.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                        select _msEmployee.EmpNumb
                      ).Count();

            return _result;
        }

        public List<MsEmployee> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                orderby _msEmployee.JobLevel ascending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName,
                                    JobTitle = _msEmployee.JobTitle,
                                    JobLevel = _msEmployee.JobLevel,
                                    Active = _msEmployee.Active
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName, _row.JobTitle, _row.JobLevel, _row.Active));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsEmployee> GetList(int _prmReqPage, int _prmPageSize, String _prmEmployee, String _prmKeyword)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            String _pattern1 = "%%";
            String _pattern2 = "%%";
            String _pattern3 = "%%";

            if (_prmEmployee == "EmpNumb")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            if (_prmEmployee == "EmpName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            if (_prmEmployee == "JobTitle")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where (SqlMethods.Like(_msEmployee.EmpNumb.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.JobTitle.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                orderby _msEmployee.EmpNumb ascending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName,
                                    JobTitle = _msEmployee.JobTitle,
                                    JobLevel = _msEmployee.JobLevel,
                                    Active = _msEmployee.Active
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName, _row.JobTitle, _row.JobLevel, _row.Active));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsEmployee> GetList()
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.Active == true
                                orderby _msEmployee.EmpName
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName,
                                    JobTitle = _msEmployee.JobTitle,
                                    JobLevel = _msEmployee.JobLevel,
                                    Active = _msEmployee.Active
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName, _row.JobTitle, _row.JobLevel, _row.Active));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForAssignment(String _prmAssigntGroupCode)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmp in db.MsEmployees
                                where !(
                                        from _assignGroupDt in this.db.MsUserGroupDts
                                        where _assignGroupDt.EmpNumb == _msEmp.EmpNumb
                                        select _assignGroupDt.EmpNumb
                                       ).Contains(_msEmp.EmpNumb)
                                select new
                                {
                                    EmpNumb = _msEmp.EmpNumb,
                                    EmpName = _msEmp.EmpNumb + " - " + _msEmp.EmpName,
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForUser()
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where !(from _msUser_msEmployee in this.db.MsUser_MsEmployees
                                        where _msEmployee.EmpNumb == _msUser_msEmployee.EmpNumb
                                        select _msUser_msEmployee.EmpNumb
                                        ).Contains(_msEmployee.EmpNumb)
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpNumb + " - " + _msEmployee.EmpName
                                }
                            );
                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForUserEdit(String _prmUserName)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where !(from _msUser_msEmployee in this.db.MsUser_MsEmployees
                                        where _msEmployee.EmpNumb == _msUser_msEmployee.EmpNumb
                                        select _msUser_msEmployee.EmpNumb
                                        ).Contains(_msEmployee.EmpNumb)
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpNumb + " - " + _msEmployee.EmpName
                                }
                            );

                var _query2 = (
                                from _msEmployee in this.db.MsEmployees
                                join _msUserEmployee in this.db.MsUser_MsEmployees
                                    on _msEmployee.EmpNumb equals _msUserEmployee.EmpNumb
                                where _msUserEmployee.UserName == _prmUserName
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpNumb + " - " + _msEmployee.EmpName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }

                foreach (var _item in _query2)
                {
                    _result.Add(new MsEmployee(_item.EmpNumb, _item.EmpName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public MsEmployee GetSingle(String _prmCode)
        {
            MsEmployee _result = null;

            try
            {
                _result = this.db.MsEmployees.Single(_temp => _temp.EmpNumb == _prmCode);
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String GetEmployeeNameByCode(String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.EmpNumb == _prmCode
                                select new
                                {
                                    EmpName = _msEmployee.EmpName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.EmpName;
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
                    MsEmployee _msEmployee = this.db.MsEmployees.Single(_temp => _temp.EmpNumb.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsEmployees.DeleteOnSubmit(_msEmployee);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool Add(MsEmployee _prmMsEmployee)
        {
            bool _result = false;

            try
            {
                this.db.MsEmployees.InsertOnSubmit(_prmMsEmployee);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool Edit(MsEmployee _prmMsEmployee)
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


        public MsUser_MsEmployee GetSingleEmpForUser(String _prmCode)
        {
            MsUser_MsEmployee _result = null;

            try
            {
                _result = this.db.MsUser_MsEmployees.Single(_temp => _temp.UserName == _prmCode);
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsEmployee> GetEmployeeListForDDL()
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                //where _msCustomer.CompanyID == _prmCompanyID
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
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
