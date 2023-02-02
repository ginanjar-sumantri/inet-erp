using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.IO;
using System.Drawing;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class EmployeeBL : Base
    {
        public EmployeeBL()
        {

        }

        #region Employee Type
        public double RowsCount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _msEmpType in this.db.MsEmpTypes
                                where (SqlMethods.Like(_msEmpType.EmpTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msEmpType.EmpTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _msEmpType.EmpTypeCode
                              ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCount()
        {
            double _result = 0;

            try
            {
                var _query = (
                                from _msEmpType in this.db.MsEmpTypes
                                select _msEmpType.EmpTypeCode
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public MsEmpType GetSingle(string _prmEmpTypeCode)
        {
            MsEmpType _result = null;

            try
            {
                _result = this.db.MsEmpTypes.Single(_emp => _emp.EmpTypeCode == _prmEmpTypeCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetEmpTypeNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msEmpType in this.db.MsEmpTypes
                                where _msEmpType.EmpTypeCode == _prmCode
                                select new
                                {
                                    EmpTypeName = _msEmpType.EmpTypeName
                                }
                              ).FirstOrDefault();

                _result = _query.EmpTypeName;

                //foreach (var _obj in _query)
                //{
                //    _result = _obj.EmpTypeName;
                //}
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmpType> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsEmpType> _result = new List<MsEmpType>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            try
            {
                var _query = (
                                from emp in this.db.MsEmpTypes
                                where (SqlMethods.Like(emp.EmpTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(emp.EmpTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby emp.EditDate descending
                                select new
                                {
                                    EmpTypeCode = emp.EmpTypeCode,
                                    EmpTypeName = emp.EmpTypeName,
                                    HaveLeave = emp.HaveLeave,
                                    FgPermanent = emp.FgPermanent
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmpType(_row.EmpTypeCode, _row.EmpTypeName, _row.HaveLeave, _row.FgPermanent));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmpType> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<MsEmpType> _result = new List<MsEmpType>();

            try
            {
                var _query = (
                                from emp in this.db.MsEmpTypes
                                orderby emp.EmpTypeCode ascending
                                select new
                                {
                                    EmpTypeCode = emp.EmpTypeCode,
                                    EmpTypeName = emp.EmpTypeName,
                                    HaveLeave = emp.HaveLeave,
                                    FgPermanent = emp.FgPermanent
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmpType(_row.EmpTypeCode, _row.EmpTypeName, _row.HaveLeave, _row.FgPermanent));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmpType> GetList()
        {
            List<MsEmpType> _result = new List<MsEmpType>();

            try
            {
                var _query = (
                                from emp in this.db.MsEmpTypes
                                orderby emp.EditDate descending
                                select new
                                {
                                    EmpTypeCode = emp.EmpTypeCode,
                                    EmpTypeName = emp.EmpTypeName,
                                    HaveLeave = emp.HaveLeave,
                                    FgPermanent = emp.FgPermanent
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmpType(_row.EmpTypeCode, _row.EmpTypeName, _row.HaveLeave, _row.FgPermanent));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsEmpType _prmMsEmpType)
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

        public bool Add(MsEmpType _prmMsEmpType)
        {
            bool _result = false;

            try
            {
                this.db.MsEmpTypes.InsertOnSubmit(_prmMsEmpType);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmEmpTypeCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpTypeCode.Length; i++)
                {
                    var _query = (
                                    from _empTypeDt in this.db.HRM_EmpType_AbsenceTypes
                                    where _empTypeDt.EmpTypeCode == _prmEmpTypeCode[i]
                                    select _empTypeDt
                                 );

                    this.db.HRM_EmpType_AbsenceTypes.DeleteAllOnSubmit(_query);

                    MsEmpType _msEmpType = this.db.MsEmpTypes.Single(_emp => _emp.EmpTypeCode.Trim().ToLower() == _prmEmpTypeCode[i].Trim().ToLower());

                    this.db.MsEmpTypes.DeleteOnSubmit(_msEmpType);
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

        public List<MsEmpType> GetListEmpTypeForDDL()
        {
            List<MsEmpType> _result = new List<MsEmpType>();

            try
            {
                var _query = (
                                from _msEmpType in this.db.MsEmpTypes
                                orderby _msEmpType.EmpTypeName ascending
                                select new
                                {
                                    EmpTypeCode = _msEmpType.EmpTypeCode,
                                    EmpTypeName = _msEmpType.EmpTypeName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { EmpTypeCode = this._string, EmpTypeName = this._string });

                    _result.Add(new MsEmpType(_row.EmpTypeCode, _row.EmpTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region EmpType_AbsenceType
        public double RowsCountEmpTypeAbsenceType(string _prmEmpTypeCode)
        {
            double _result = 0;

            var _query =
                (
                    from _msEmpTypeAbsenceType in this.db.HRM_EmpType_AbsenceTypes
                    where _msEmpTypeAbsenceType.EmpTypeCode == _prmEmpTypeCode
                    select _msEmpTypeAbsenceType.EmpTypeAbsTypeCode
                ).Count();

            _result = _query;

            return _result;

        }

        public HRM_EmpType_AbsenceType GetSingleEmpTypeAbsenceType(Guid _prmEmpTypeAbsTypeCode)
        {
            HRM_EmpType_AbsenceType _result = null;

            try
            {
                _result = this.db.HRM_EmpType_AbsenceTypes.Single(_emp => _emp.EmpTypeAbsTypeCode == _prmEmpTypeAbsTypeCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_EmpType_AbsenceType> GetListEmpTypeAbsenceType(int _prmReqPage, int _prmPageSize, string _prmEmpType)
        {
            List<HRM_EmpType_AbsenceType> _result = new List<HRM_EmpType_AbsenceType>();

            try
            {
                var _query = (
                                from _empTypeAbsType in this.db.HRM_EmpType_AbsenceTypes
                                where _empTypeAbsType.EmpTypeCode == _prmEmpType
                                orderby _empTypeAbsType.EditDate descending
                                select new
                                {
                                    EmpTypeAbsTypeCode = _empTypeAbsType.EmpTypeAbsTypeCode,
                                    EmpTypeCode = _empTypeAbsType.EmpTypeCode,
                                    AbsenceTypeCode = _empTypeAbsType.AbsenceTypeCode,
                                    AbsenceTypeName = (
                                                            from _absenceType in this.db.HRMMsAbsenceTypes
                                                            where _absenceType.AbsenceTypeCode == _empTypeAbsType.AbsenceTypeCode
                                                            select _absenceType.AbsenceTypeName
                                                        ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_EmpType_AbsenceType(_row.EmpTypeAbsTypeCode, _row.EmpTypeCode, _row.AbsenceTypeCode, _row.AbsenceTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEmpTypeAbsenceType(HRM_EmpType_AbsenceType _prmHRM_EmpType_AbsenceType)
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

        public bool AddEmpTypeAbsenceType(HRM_EmpType_AbsenceType _prmHRM_EmpType_AbsenceType)
        {
            bool _result = false;

            try
            {
                this.db.HRM_EmpType_AbsenceTypes.InsertOnSubmit(_prmHRM_EmpType_AbsenceType);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiEmpTypeAbsenceType(string[] _prmEmpTypeAbsTypeCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpTypeAbsTypeCode.Length; i++)
                {
                    HRM_EmpType_AbsenceType _msEmpType = this.db.HRM_EmpType_AbsenceTypes.Single(_emp => _emp.EmpTypeAbsTypeCode == new Guid(_prmEmpTypeAbsTypeCode[i]));

                    this.db.HRM_EmpType_AbsenceTypes.DeleteOnSubmit(_msEmpType);
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

        #endregion

        #region EmpGroup
        public double RowsCountEmpGroup(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query =
                            (
                                from _msEmpGroup in this.db.HRMMsEmpGroups
                                where (SqlMethods.Like(_msEmpGroup.EmpGroupCode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msEmpGroup.EmpGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _msEmpGroup.EmpGroupCode
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountEmpGroup()
        {
            double _result = 0;

            try
            {
                var _query =
                            (
                                from _msEmpGroup in this.db.HRMMsEmpGroups
                                select _msEmpGroup.EmpGroupCode
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMMsEmpGroup GetSingleEmpGroup(String _prmEmpGroupCode)
        {
            HRMMsEmpGroup _result = null;

            try
            {
                _result = this.db.HRMMsEmpGroups.Single(_temp => _temp.EmpGroupCode == _prmEmpGroupCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetEmpGroupNameByCode(String _prmEmpGroupCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _hrmMsEmpGroup in this.db.HRMMsEmpGroups
                                where _hrmMsEmpGroup.EmpGroupCode == _prmEmpGroupCode
                                select new
                                {
                                    EmpGroupName = _hrmMsEmpGroup.EmpGroupName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.EmpGroupName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsEmpGroup> GetListEmpGroup(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsEmpGroup> _result = new List<HRMMsEmpGroup>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _hrmMsEmpGroup in this.db.HRMMsEmpGroups
                                where (SqlMethods.Like(_hrmMsEmpGroup.EmpGroupCode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_hrmMsEmpGroup.EmpGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _hrmMsEmpGroup.EmpGroupCode ascending
                                select new
                                {
                                    EmpGroupCode = _hrmMsEmpGroup.EmpGroupCode,
                                    EmpGroupName = _hrmMsEmpGroup.EmpGroupName,
                                    ScheduleType = _hrmMsEmpGroup.ScheduleType,
                                    ShiftCode = _hrmMsEmpGroup.ShiftCode,
                                    ShiftName = (
                                                    from _msShift in this.db.HRMMsShifts
                                                    where _hrmMsEmpGroup.ShiftCode == _msShift.ShiftCode
                                                    select _msShift.ShiftName
                                               ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsEmpGroup(_row.EmpGroupCode, _row.EmpGroupName, _row.ScheduleType, _row.ShiftCode, _row.ShiftName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsEmpGroup> GetListEmpGroupForReport(int _prmReqPage, int _prmPageSize)
        {
            List<HRMMsEmpGroup> _result = new List<HRMMsEmpGroup>();

            try
            {
                var _query = (
                                from _hrmMsEmpGroup in this.db.HRMMsEmpGroups
                                orderby _hrmMsEmpGroup.EmpGroupCode ascending
                                select new
                                {
                                    EmpGroupCode = _hrmMsEmpGroup.EmpGroupCode,
                                    EmpGroupName = _hrmMsEmpGroup.EmpGroupName,
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsEmpGroup(_row.EmpGroupCode, _row.EmpGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsEmpGroup> GetListEmpGroupForDDL()
        {
            List<HRMMsEmpGroup> _result = new List<HRMMsEmpGroup>();

            try
            {
                var _query = (
                                from _hrmMsEmpGroup in this.db.HRMMsEmpGroups
                                orderby _hrmMsEmpGroup.EmpGroupName ascending
                                select new
                                {
                                    EmpGroupCode = _hrmMsEmpGroup.EmpGroupCode,
                                    EmpGroupName = _hrmMsEmpGroup.EmpGroupName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsEmpGroup(_row.EmpGroupCode, _row.EmpGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String[] GetFromEmpGroupAssign(String _prmEmpNumb, DateTime _prmEffectiveDate)
        {
            String[] _result = new String[2];

            try
            {
                var _query = (
                                from _hrmTrEmpGroup in this.db.HRMTrEmpGroupAssignPostings
                                where _hrmTrEmpGroup.EmpNumb == _prmEmpNumb
                                    && _hrmTrEmpGroup.StartDate <= _prmEffectiveDate
                                    && _hrmTrEmpGroup.EndDate == null
                                orderby _hrmTrEmpGroup.StartDate descending
                                select new
                                {
                                    EmpGroupCode = _hrmTrEmpGroup.EmpGroupCode,
                                    EmpGroupName = (
                                                        from _msEmpGroup in this.db.HRMMsEmpGroups
                                                        where _msEmpGroup.EmpGroupCode == _hrmTrEmpGroup.EmpGroupCode
                                                        select _msEmpGroup.EmpGroupName
                                                    ).FirstOrDefault()
                                }
                            ).Take(1).Single();

                _result[0] = _query.EmpGroupCode;
                _result[1] = _query.EmpGroupName;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpDriver()
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                && _msEmployee.FgDriver == EmployeeDataMapper.IsActive(true)
                                orderby _msEmployee.UserDate descending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName,
                                    HandPhone1 = _msEmployee.HandPhone1
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName, _row.HandPhone1));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsEmpGroup> GetListEmpGroupScheduleShiftForDDL()
        {
            List<HRMMsEmpGroup> _result = new List<HRMMsEmpGroup>();

            try
            {
                var _query = (
                                from _hrmMsEmpGroup in this.db.HRMMsEmpGroups
                                orderby _hrmMsEmpGroup.EmpGroupName ascending
                                select new
                                {
                                    EmpGroupCode = _hrmMsEmpGroup.EmpGroupCode,
                                    EmpGroupName = _hrmMsEmpGroup.EmpGroupName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsEmpGroup(_row.EmpGroupCode, _row.EmpGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEmpGroup(HRMMsEmpGroup _prmHRMMsEmpGroup)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsEmpGroupName(_prmHRMMsEmpGroup.EmpGroupName, _prmHRMMsEmpGroup.EmpGroupCode) == false)
                {
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddEmpGroup(HRMMsEmpGroup _prmHRMMsEmpGroup)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsEmpGroupName(_prmHRMMsEmpGroup.EmpGroupName, _prmHRMMsEmpGroup.EmpGroupCode) == false)
                {
                    this.db.HRMMsEmpGroups.InsertOnSubmit(_prmHRMMsEmpGroup);
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        private bool IsExistsEmpGroupName(String _prmEmpGroupName, String _prmEmpGroupCode)
        {
            bool _result = false;

            try
            {
                var _query = from _hrmMsEmpGroup in this.db.HRMMsEmpGroups
                             where _hrmMsEmpGroup.EmpGroupName == _prmEmpGroupName && _hrmMsEmpGroup.EmpGroupCode != _prmEmpGroupCode
                             select new
                             {
                                 _hrmMsEmpGroup.EmpGroupCode
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

        public bool DeleteMultiEmpGroup(string[] _prmEmpGroupCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpGroupCode.Length; i++)
                {
                    HRMMsEmpGroup _HRMMsEmpGroup = this.db.HRMMsEmpGroups.Single(_temp => _temp.EmpGroupCode == _prmEmpGroupCode[i]);

                    this.db.HRMMsEmpGroups.DeleteOnSubmit(_HRMMsEmpGroup);
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
        #endregion

        #region Employee
        public double RowsCountEmp(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";
            string _pattern5 = "%%";
            string _pattern6 = "%%";

            if (_prmCategory == "EmpNumb")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "JobTtl")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "JobLvl")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern2 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "WorkPlace")
            {
                _pattern5 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern2 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "EmpType")
            {
                _pattern6 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern2 = "%%";
            }

            var _query = (
                            from _msEmployee in this.db.MsEmployees
                            join _msJobTtl in this.db.MsJobTitles on _msEmployee.JobTitle equals _msJobTtl.JobTtlCode
                            join _msJobLvl in this.db.MsJobLevels on _msEmployee.JobLevel equals _msJobLvl.JobLvlCode
                            join _msWorkPlace in this.db.MsWorkPlaces on _msEmployee.WorkPlace equals _msWorkPlace.WorkPlaceCode
                            join _msEmpType in this.db.MsEmpTypes on _msEmployee.EmpType equals _msEmpType.EmpTypeCode
                            where (SqlMethods.Like(_msEmployee.EmpNumb.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like(_msJobTtl.JobTtlName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && (SqlMethods.Like(_msJobLvl.JobLvlName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                               && (SqlMethods.Like(_msWorkPlace.WorkPlaceName.Trim().ToLower(), _pattern5.Trim().ToLower()))
                               && (SqlMethods.Like(_msEmpType.EmpTypeName.Trim().ToLower(), _pattern6.Trim().ToLower()))
                            select _msEmployee.EmpNumb
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountEmpForBankAccount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";
            string _pattern5 = "%%";
            string _pattern6 = "%%";

            if (_prmCategory == "EmpNumb")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "JobTtl")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "JobLvl")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern2 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "WorkPlace")
            {
                _pattern5 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern2 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "EmpType")
            {
                _pattern6 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern2 = "%%";
            }

            var _query = (
                            from _msEmployee in this.db.MsEmployees
                            join _msJobTtl in this.db.MsJobTitles on _msEmployee.JobTitle equals _msJobTtl.JobTtlCode
                            join _msJobLvl in this.db.MsJobLevels on _msEmployee.JobLevel equals _msJobLvl.JobLvlCode
                            join _msWorkPlace in this.db.MsWorkPlaces on _msEmployee.WorkPlace equals _msWorkPlace.WorkPlaceCode
                            join _msEmpType in this.db.MsEmpTypes on _msEmployee.EmpType equals _msEmpType.EmpTypeCode
                            where (SqlMethods.Like(_msEmployee.EmpNumb.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like(_msJobTtl.JobTtlName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && (SqlMethods.Like(_msJobLvl.JobLvlName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                               && (SqlMethods.Like(_msWorkPlace.WorkPlaceName.Trim().ToLower(), _pattern5.Trim().ToLower()))
                               && (SqlMethods.Like(_msEmpType.EmpTypeName.Trim().ToLower(), _pattern6.Trim().ToLower()))
                               && _msEmployee.FgActive == 'Y'
                            select _msEmployee.EmpNumb
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountEmpForSalary(string _prmEmpType, string _prmJobTitle, string _prmJobLevel, string _prmWorkPlace, String _prmTransNmbr)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";
            string _pattern5 = "%%";

            if (_prmEmpType != "")
            {
                _pattern1 = "%" + _prmEmpType + "%";
            }
            if (_prmJobTitle != "")
            {
                _pattern2 = "%" + _prmJobTitle + "%";
            }
            if (_prmJobLevel != "")
            {
                _pattern3 = "%" + _prmJobLevel + "%";
            }
            if (_prmWorkPlace != "")
            {
                _pattern4 = "%" + _prmWorkPlace + "%";
            }
            if (_prmTransNmbr != "")
            {
                _pattern5 = "%" + _prmTransNmbr + "%";
            }

            var _query =
                        (
                            from _msEmployee in this.db.MsEmployees
                            join _trSalary in this.db.PAYTrSalaryProcesses
                                on _msEmployee.EmpNumb equals db.SQLCription(_trSalary.EmpNumb, -1)
                            where (SqlMethods.Like(_msEmployee.EmpType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.JobTitle.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.JobLevel.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                    && (SqlMethods.Like(_trSalary.TransNmbr.Trim().ToLower(), _pattern5.Trim().ToLower()))
                            select _msEmployee.EmpNumb
                        ).Distinct().Count();

            _result = _query;

            return _result;
        }

        public double RowsCountEmpForReport(string _prmEmpType, string _prmJobTitle, string _prmJobLevel, string _prmWorkPlace)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmEmpType != "")
            {
                _pattern1 = "%" + _prmEmpType + "%";
            }
            if (_prmJobTitle != "")
            {
                _pattern2 = "%" + _prmJobTitle + "%";
            }
            if (_prmJobLevel != "")
            {
                _pattern3 = "%" + _prmJobLevel + "%";
            }
            if (_prmWorkPlace != "")
            {
                _pattern4 = "%" + _prmWorkPlace + "%";
            }

            var _query =
                        (
                            from _msEmployee in this.db.MsEmployees
                            where (SqlMethods.Like(_msEmployee.EmpType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.JobTitle.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.JobLevel.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                    && _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                            select _msEmployee.EmpNumb
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountEmpForAttendance(string _prmEmpType, string _prmJobTitle, string _prmJobLevel, string _prmWorkPlace)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmEmpType != "")
            {
                _pattern1 = "%" + _prmEmpType + "%";
            }
            if (_prmJobTitle != "")
            {
                _pattern2 = "%" + _prmJobTitle + "%";
            }
            if (_prmJobLevel != "")
            {
                _pattern3 = "%" + _prmJobLevel + "%";
            }
            if (_prmWorkPlace != "")
            {
                _pattern4 = "%" + _prmWorkPlace + "%";
            }

            var _query =
                        (
                            from _msEmployee in this.db.MsEmployees
                            where (SqlMethods.Like(_msEmployee.EmpType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.JobTitle.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.JobLevel.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                            //&& _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                            select _msEmployee.EmpNumb
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountEmpForReportAndSearch(string _prmEmpType, string _prmJobTitle, string _prmJobLevel, string _prmWorkPlace, String _prmCategory, String _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            string _pattern5 = "%%";
            string _pattern6 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern5 = "%" + _prmKeyword + "%";
                _pattern6 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern6 = "%" + _prmKeyword + "%";
                _pattern5 = "%%";
            }

            if (_prmEmpType != "")
            {
                _pattern1 = "%" + _prmEmpType + "%";
            }
            if (_prmJobTitle != "")
            {
                _pattern2 = "%" + _prmJobTitle + "%";
            }
            if (_prmJobLevel != "")
            {
                _pattern3 = "%" + _prmJobLevel + "%";
            }
            if (_prmWorkPlace != "")
            {
                _pattern4 = "%" + _prmWorkPlace + "%";
            }

            var _query =
                        (
                            from _msEmployee in this.db.MsEmployees
                            where (SqlMethods.Like(_msEmployee.EmpType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.JobTitle.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.JobLevel.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.EmpNumb.Trim().ToLower(), _pattern5.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern6.Trim().ToLower()))
                                    && _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                            select _msEmployee.EmpNumb
                        ).Count();

            _result = _query;

            return _result;
        }

        public MsEmployee GetSingleEmp(string _prmEmpNumb)
        {
            MsEmployee _result = null;

            try
            {
                _result = this.db.MsEmployees.Single(_emp => _emp.EmpNumb == _prmEmpNumb);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetEmpNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msEmp in this.db.MsEmployees
                                where _msEmp.EmpNumb == _prmCode
                                select new
                                {
                                    EmpName = _msEmp.EmpName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.EmpName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetEmpNumbByFingerPrintID(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msEmp in this.db.MsEmployees
                                where _msEmp.FingerPrintID == _prmCode
                                select new
                                {
                                    EmpNumb = _msEmp.EmpNumb
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.EmpNumb;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsEmpNumbExists(String _prmEmpNumb)
        {
            bool _result = false;

            try
            {
                var _query = from _msEmployee in this.db.MsEmployees
                             where _msEmployee.EmpNumb == _prmEmpNumb
                             select new
                             {
                                 _msEmployee.EmpNumb
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

        public List<MsEmployee> GetListEmp(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";
            string _pattern5 = "%%";
            string _pattern6 = "%%";

            if (_prmCategory == "EmpNumb")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "JobTtl")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "JobLvl")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern2 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "WorkPlace")
            {
                _pattern5 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern2 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "EmpType")
            {
                _pattern6 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                 from _msEmployee in this.db.MsEmployees
                                 join _msJobTtl in this.db.MsJobTitles on _msEmployee.JobTitle equals _msJobTtl.JobTtlCode
                                 join _msJobLvl in this.db.MsJobLevels on _msEmployee.JobLevel equals _msJobLvl.JobLvlCode
                                 join _msWorkPlace in this.db.MsWorkPlaces on _msEmployee.WorkPlace equals _msWorkPlace.WorkPlaceCode
                                 join _msEmpType in this.db.MsEmpTypes on _msEmployee.EmpType equals _msEmpType.EmpTypeCode
                                 where (SqlMethods.Like(_msEmployee.EmpNumb.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msJobTtl.JobTtlName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msJobLvl.JobLvlName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                    && (SqlMethods.Like(_msWorkPlace.WorkPlaceName.Trim().ToLower(), _pattern5.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmpType.EmpTypeName.Trim().ToLower(), _pattern6.Trim().ToLower()))
                                 orderby _msEmployee.UserDate descending
                                 select new
                                 {
                                     EmpNumb = _msEmployee.EmpNumb,
                                     EmpName = _msEmployee.EmpName,
                                     JobLevel = _msJobLvl.JobLvlName,
                                     JobTitle = _msJobTtl.JobTtlName,
                                     EmpTypeName = _msEmpType.EmpTypeName,
                                     FgActive = _msEmployee.FgActive
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName, _row.JobLevel, _row.JobTitle, _row.EmpTypeName, _row.FgActive));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmp()
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                join _msJobLevel in this.db.MsJobLevels
                                    on _msEmployee.JobLevel equals _msJobLevel.JobLvlCode
                                join _msJobTitle in this.db.MsJobTitles
                                   on _msEmployee.JobTitle equals _msJobTitle.JobTtlCode
                                join _msEmpType in this.db.MsEmpTypes
                                    on _msEmployee.EmpType equals _msEmpType.EmpTypeCode
                                orderby _msEmployee.UserDate descending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName,
                                    JobLevel = _msJobLevel.JobLvlName,
                                    JobTitle = _msJobTitle.JobTtlName,
                                    EmpTypeName = _msEmpType.EmpTypeName,
                                    FgActive = _msEmployee.FgActive
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName, _row.JobLevel, _row.JobTitle, _row.EmpTypeName, _row.FgActive));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEmp(MsEmployee _prmMsEmployee)
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

        public bool AddEmp(MsEmployee _prmMsEmployee, Master_EmpLeaveDay _prmMaster_EmpLeaveDay)
        {
            bool _result = false;

            try
            {
                this.db.MsEmployees.InsertOnSubmit(_prmMsEmployee);

                if (this.GetSingleEmpLeaveDay(_prmMsEmployee.EmpNumb) == null)
                {
                    this.db.Master_EmpLeaveDays.InsertOnSubmit(_prmMaster_EmpLeaveDay);
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

        public bool DeleteMultiEmp(string[] _prmEmpNumb)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpNumb.Length; i++)
                {
                    var _query = (from _detail in this.db.Master_EmpBanks
                                  where _detail.EmpNmbr.Trim().ToLower() == _prmEmpNumb[i].Trim().ToLower()
                                  select _detail);

                    this.db.Master_EmpBanks.DeleteAllOnSubmit(_query);

                    var _query2 = (from _detail in this.db.Master_EmpEdus
                                   where _detail.EmpNmbr.Trim().ToLower() == _prmEmpNumb[i].Trim().ToLower()
                                   select _detail);

                    this.db.Master_EmpEdus.DeleteAllOnSubmit(_query2);

                    var _query3 = (from _detail in this.db.Master_EmpExps
                                   where _detail.EmpNumb.Trim().ToLower() == _prmEmpNumb[i].Trim().ToLower()
                                   select _detail);

                    this.db.Master_EmpExps.DeleteAllOnSubmit(_query3);

                    var _query4 = (from _detail in this.db.Master_EmpFamilies
                                   where _detail.EmpNumb.Trim().ToLower() == _prmEmpNumb[i].Trim().ToLower()
                                   select _detail);

                    this.db.Master_EmpFamilies.DeleteAllOnSubmit(_query4);

                    var _query5 = (from _detail in this.db.Master_EmpSkills
                                   where _detail.EmpNmbr.Trim().ToLower() == _prmEmpNumb[i].Trim().ToLower()
                                   select _detail);

                    this.db.Master_EmpSkills.DeleteAllOnSubmit(_query5);

                    var _query6 = (from _detail in this.db.Master_OrgUnit_MsEmployees
                                   where _detail.EmpNumb.Trim().ToLower() == _prmEmpNumb[i].Trim().ToLower()
                                   select _detail);

                    this.db.Master_OrgUnit_MsEmployees.DeleteAllOnSubmit(_query6);

                    Master_EmpLeaveDay _msEmpLeaveDay = this.GetSingleEmpLeaveDay(_prmEmpNumb[i]);
                    if (_msEmpLeaveDay != null)
                    {
                        this.db.Master_EmpLeaveDays.DeleteOnSubmit(_msEmpLeaveDay);
                    }

                    var _query7 = (from _detail in this.db.HRM_Employee_WorkingHours
                                   where _detail.EmpNumb.Trim().ToLower() == _prmEmpNumb[i].Trim().ToLower()
                                   select _detail);

                    this.db.HRM_Employee_WorkingHours.DeleteAllOnSubmit(_query7);

                    MsEmployee _msEmployee = this.db.MsEmployees.Single(_emp => _emp.EmpNumb.Trim().ToLower() == _prmEmpNumb[i].Trim().ToLower());

                    this.db.MsEmployees.DeleteOnSubmit(_msEmployee);
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

        public List<MsEmployee> GetListEmpSalesForDDL()
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.FgSales == EmployeeDataMapper.IsSales(true)
                                    && _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                orderby _msEmployee.UserDate descending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { EmpNumb = this._string, EmpName = this._string });

                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpLoanForDDL(String _prmTransNmbr)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                join _hrmTrLoanDt in this.db.HRMTrLoanInDts
                                    on _msEmployee.EmpNumb equals _hrmTrLoanDt.EmpNumb
                                where _hrmTrLoanDt.TransNmbr == _prmTransNmbr
                                    && _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                orderby _msEmployee.EmpName descending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpNumb + " - " + _msEmployee.EmpName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { EmpNumb = this._string, EmpName = this._string });

                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForSendSlipByEmail()
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                      && _msEmployee.FgSendEmail == true
                                orderby _msEmployee.UserDate descending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName,
                                    Email = _msEmployee.Email
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName, _row.Email));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetStringEmpForSendSlipByEmail()
        {
            String _result = "";

            try
            {
                var _query = (
                                  from _msEmployee in this.db.MsEmployees
                                  where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                        && _msEmployee.FgSendEmail == true
                                  orderby _msEmployee.UserDate descending
                                  select _msEmployee.EmpNumb
                            );

                foreach (String _item in _query)
                {
                    if (_result == "")
                    {
                        _result = _item;
                    }
                    else
                    {
                        _result += "," + _item;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForDDL()
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                orderby _msEmployee.UserDate descending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { EmpNumb = this._string, EmpName = this._string });

                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForLeaveProcess(int _prmYear, int _prmPeriod)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                && Convert.ToDecimal(_msEmployee.StartDate.Year.ToString() + _msEmployee.StartDate.Month.ToString()) <= Convert.ToDecimal(_prmYear.ToString() + _prmPeriod.ToString())
                                orderby _msEmployee.UserDate descending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { EmpNumb = this._string, EmpName = this._string });

                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForReport(int _prmReqPage, int _prmPageSize, string _prmEmpType, string _prmJobTitle, string _prmJobLevel, string _prmWorkPlace)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmEmpType != "")
            {
                _pattern1 = "%" + _prmEmpType + "%";
            }
            if (_prmJobTitle != "")
            {
                _pattern2 = "%" + _prmJobTitle + "%";
            }
            if (_prmJobLevel != "")
            {
                _pattern3 = "%" + _prmJobLevel + "%";
            }
            if (_prmWorkPlace != "")
            {
                _pattern4 = "%" + _prmWorkPlace + "%";
            }

            try
            {
                var _query = (
                                 from _msEmployee in this.db.MsEmployees
                                 where (SqlMethods.Like(_msEmployee.EmpType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobTitle.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobLevel.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                        && _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                 orderby _msEmployee.EmpNumb ascending
                                 select new
                                 {
                                     EmpNumb = _msEmployee.EmpNumb,
                                     EmpName = _msEmployee.EmpName
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForReportAndSearch(int _prmReqPage, int _prmPageSize, string _prmEmpType, string _prmJobTitle, string _prmJobLevel, string _prmWorkPlace, String _prmCategory, String _prmKeyword)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            string _pattern5 = "%%";
            string _pattern6 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern5 = "%" + _prmKeyword + "%";
                _pattern6 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern6 = "%" + _prmKeyword + "%";
                _pattern5 = "%%";
            }

            if (_prmEmpType != "")
            {
                _pattern1 = "%" + _prmEmpType + "%";
            }
            if (_prmJobTitle != "")
            {
                _pattern2 = "%" + _prmJobTitle + "%";
            }
            if (_prmJobLevel != "")
            {
                _pattern3 = "%" + _prmJobLevel + "%";
            }
            if (_prmWorkPlace != "")
            {
                _pattern4 = "%" + _prmWorkPlace + "%";
            }

            try
            {
                var _query = (
                                 from _msEmployee in this.db.MsEmployees
                                 where (SqlMethods.Like(_msEmployee.EmpType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobTitle.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobLevel.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.EmpNumb.Trim().ToLower(), _pattern5.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern6.Trim().ToLower()))
                                        && _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                 orderby _msEmployee.EmpNumb ascending
                                 select new
                                 {
                                     EmpNumb = _msEmployee.EmpNumb,
                                     EmpName = _msEmployee.EmpName
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForAttendance(int _prmReqPage, int _prmPageSize, string _prmEmpType, string _prmJobTitle, string _prmJobLevel, string _prmWorkPlace, DateTime _prmStartDate, DateTime _prmEndDate)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmEmpType != "")
            {
                _pattern1 = "%" + _prmEmpType + "%";
            }
            if (_prmJobTitle != "")
            {
                _pattern2 = "%" + _prmJobTitle + "%";
            }
            if (_prmJobLevel != "")
            {
                _pattern3 = "%" + _prmJobLevel + "%";
            }
            if (_prmWorkPlace != "")
            {
                _pattern4 = "%" + _prmWorkPlace + "%";
            }

            try
            {
                var _query = (
                                 from _msEmployee in this.db.MsEmployees
                                 where (SqlMethods.Like(_msEmployee.EmpType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobTitle.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobLevel.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                 //&& (_msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                 //|| (_msEmployee.EndDate >= _prmStartDate && _msEmployee.EndDate <= _prmEndDate))
                                 orderby _msEmployee.EmpNumb ascending
                                 select new
                                 {
                                     EmpNumb = _msEmployee.EmpNumb,
                                     EmpName = _msEmployee.EmpName
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForSalary(int _prmReqPage, int _prmPageSize, string _prmEmpType, string _prmJobTitle, string _prmJobLevel, string _prmWorkPlace, string _prmTransNmbr, String _prmCategory, String _prmKeyword)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";
            string _pattern5 = "%%";

            string _pattern6 = "%%";
            string _pattern7 = "%%";

            if (_prmEmpType != "")
            {
                _pattern1 = "%" + _prmEmpType + "%";
            }
            if (_prmJobTitle != "")
            {
                _pattern2 = "%" + _prmJobTitle + "%";
            }
            if (_prmJobLevel != "")
            {
                _pattern3 = "%" + _prmJobLevel + "%";
            }
            if (_prmWorkPlace != "")
            {
                _pattern4 = "%" + _prmWorkPlace + "%";
            }
            if (_prmTransNmbr != "")
            {
                _pattern5 = "%" + _prmTransNmbr + "%";
            }

            if (_prmCategory == "EmpNumb")
            {
                _pattern6 = "%" + _prmKeyword + "%";
                _pattern7 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern7 = "%" + _prmKeyword + "%";
                _pattern6 = "%%";
            }

            try
            {
                var _query = (
                                 from _msEmployee in this.db.MsEmployees
                                 join _trSalary in this.db.PAYTrSalaryProcesses
                                    on _msEmployee.EmpNumb equals db.SQLCription(_trSalary.EmpNumb, -1)
                                 where (SqlMethods.Like(_msEmployee.EmpType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobTitle.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobLevel.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                        && (SqlMethods.Like(_trSalary.TransNmbr.Trim().ToLower(), _pattern5.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.EmpNumb.Trim().ToLower(), _pattern6.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern7.Trim().ToLower()))
                                 orderby _msEmployee.EmpNumb ascending
                                 select new
                                 {
                                     EmpNumb = db.SQLCription(_trSalary.EmpNumb, -1),
                                     EmpName = db.SQLCription(_trSalary.EmpNumb, -1) + " - " + _msEmployee.EmpName
                                 }
                            ).Distinct().Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForSalary(string _prmEmpType, string _prmJobTitle, string _prmJobLevel, string _prmWorkPlace, String _prmTransNmbr, String _prmCategory, String _prmKeyword)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";
            string _pattern5 = "%%";

            string _pattern6 = "%%";
            string _pattern7 = "%%";

            if (_prmEmpType != "")
            {
                _pattern1 = "%" + _prmEmpType + "%";
            }
            if (_prmJobTitle != "")
            {
                _pattern2 = "%" + _prmJobTitle + "%";
            }
            if (_prmJobLevel != "")
            {
                _pattern3 = "%" + _prmJobLevel + "%";
            }
            if (_prmWorkPlace != "")
            {
                _pattern4 = "%" + _prmWorkPlace + "%";
            }
            if (_prmTransNmbr != "")
            {
                _pattern5 = "%" + _prmTransNmbr + "%";
            }

            if (_prmCategory == "EmpNumb")
            {
                _pattern6 = "%" + _prmKeyword + "%";
                _pattern7 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern7 = "%" + _prmKeyword + "%";
                _pattern6 = "%%";
            }

            try
            {
                var _query = (
                                 from _msEmployee in this.db.MsEmployees
                                 join _trSalary in this.db.PAYTrSalaryProcesses
                                    on _msEmployee.EmpNumb equals db.SQLCription(_trSalary.EmpNumb, -1)
                                 where (SqlMethods.Like(_msEmployee.EmpType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobTitle.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobLevel.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                        && (SqlMethods.Like(_trSalary.TransNmbr.Trim().ToLower(), _pattern5.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.EmpNumb.Trim().ToLower(), _pattern6.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern7.Trim().ToLower()))
                                 orderby _msEmployee.EmpNumb ascending
                                 select new
                                 {
                                     EmpNumb = db.SQLCription(_trSalary.EmpNumb, -1),
                                     EmpName = db.SQLCription(_trSalary.EmpNumb, -1) + " - " + _msEmployee.EmpName
                                 }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForReport(string _prmEmpType, string _prmJobTitle, string _prmJobLevel, string _prmWorkPlace)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmEmpType != "")
            {
                _pattern1 = "%" + _prmEmpType + "%";
            }
            if (_prmJobTitle != "")
            {
                _pattern2 = "%" + _prmJobTitle + "%";
            }
            if (_prmJobLevel != "")
            {
                _pattern3 = "%" + _prmJobLevel + "%";
            }
            if (_prmWorkPlace != "")
            {
                _pattern4 = "%" + _prmWorkPlace + "%";
            }

            try
            {
                var _query = (
                                 from _msEmployee in this.db.MsEmployees
                                 where (SqlMethods.Like(_msEmployee.EmpType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobTitle.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobLevel.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                        && _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                 orderby _msEmployee.EmpNumb ascending
                                 select new
                                 {
                                     EmpNumb = _msEmployee.EmpNumb,
                                     EmpName = _msEmployee.EmpName
                                 }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForReportAndSearch(string _prmEmpType, string _prmJobTitle, string _prmJobLevel, string _prmWorkPlace, String _prmCategory, String _prmKeyword)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            string _pattern5 = "%%";
            string _pattern6 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern5 = "%" + _prmKeyword + "%";
                _pattern6 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern6 = "%" + _prmKeyword + "%";
                _pattern5 = "%%";
            }

            if (_prmEmpType != "")
            {
                _pattern1 = "%" + _prmEmpType + "%";
            }
            if (_prmJobTitle != "")
            {
                _pattern2 = "%" + _prmJobTitle + "%";
            }
            if (_prmJobLevel != "")
            {
                _pattern3 = "%" + _prmJobLevel + "%";
            }
            if (_prmWorkPlace != "")
            {
                _pattern4 = "%" + _prmWorkPlace + "%";
            }

            try
            {
                var _query = (
                                 from _msEmployee in this.db.MsEmployees
                                 where (SqlMethods.Like(_msEmployee.EmpType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobTitle.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobLevel.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.EmpNumb.Trim().ToLower(), _pattern5.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern6.Trim().ToLower()))
                                        && _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                 orderby _msEmployee.EmpNumb ascending
                                 select new
                                 {
                                     EmpNumb = _msEmployee.EmpNumb,
                                     EmpName = _msEmployee.EmpName
                                 }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForAttendance(string _prmEmpType, string _prmJobTitle, string _prmJobLevel, string _prmWorkPlace, DateTime _prmStartDate, DateTime _prmEndDate)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmEmpType != "")
            {
                _pattern1 = "%" + _prmEmpType + "%";
            }
            if (_prmJobTitle != "")
            {
                _pattern2 = "%" + _prmJobTitle + "%";
            }
            if (_prmJobLevel != "")
            {
                _pattern3 = "%" + _prmJobLevel + "%";
            }
            if (_prmWorkPlace != "")
            {
                _pattern4 = "%" + _prmWorkPlace + "%";
            }

            try
            {
                var _query = (
                                 from _msEmployee in this.db.MsEmployees
                                 where (SqlMethods.Like(_msEmployee.EmpType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobTitle.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.JobLevel.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.WorkPlace.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                 //&& (_msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                 //|| (_msEmployee.EndDate >= _prmStartDate && _msEmployee.EndDate <= _prmEndDate))
                                 orderby _msEmployee.EmpNumb ascending
                                 select new
                                 {
                                     EmpNumb = _msEmployee.EmpNumb,
                                     EmpName = _msEmployee.EmpName
                                 }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForDDLInTerminationReqEdit()
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = ((
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                    && !(
                                            from _terminationReq in this.db.HRM_TerminationRequests
                                            where _terminationReq.EmpNumb == _msEmployee.EmpNumb
                                                && !(_terminationReq.Status == TerminationRequestDataMapper.GetStatus(TerminationRequestStatus.Rejected) || _terminationReq.Status == TerminationRequestDataMapper.GetStatus(TerminationRequestStatus.Cancelled))
                                            select _terminationReq.EmpNumb
                                        ).Contains(_msEmployee.EmpNumb)
                                orderby _msEmployee.EmpName ascending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName
                                }
                            ).Union
                            (
                                from _terminationReq in this.db.HRM_TerminationRequests
                                where (_terminationReq.Status == TerminationRequestDataMapper.GetStatus(TerminationRequestStatus.Draft) || _terminationReq.Status == TerminationRequestDataMapper.GetStatus(TerminationRequestStatus.WaitingForApproval))
                                select new
                                {
                                    EmpNumb = _terminationReq.EmpNumb,
                                    EmpName = (
                                                from _msEmp in this.db.MsEmployees
                                                where _msEmp.EmpNumb == _terminationReq.EmpNumb
                                                select _msEmp.EmpName
                                              ).FirstOrDefault()
                                }
                            )).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { EmpNumb = this._string, EmpName = this._string });

                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForDDLInTerminationReqAdd()
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                    && !(
                                            from _terminationReq in this.db.HRM_TerminationRequests
                                            where _terminationReq.EmpNumb == _msEmployee.EmpNumb
                                                && !(_terminationReq.Status == TerminationRequestDataMapper.GetStatus(TerminationRequestStatus.Rejected) || _terminationReq.Status == TerminationRequestDataMapper.GetStatus(TerminationRequestStatus.Cancelled))
                                            select _terminationReq.EmpNumb
                                        ).Contains(_msEmployee.EmpNumb)
                                orderby _msEmployee.EmpName ascending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { EmpNumb = this._string, EmpName = this._string });

                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForDDLInExitInterviewAdd(Guid _prmTerminationRequestCode)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                    && !(
                                            from _terminationRequest in this.db.HRM_TerminationRequests
                                            where _terminationRequest.EmpNumb == _msEmployee.EmpNumb
                                                && _terminationRequest.TerminationRequestCode == _prmTerminationRequestCode
                                            select _terminationRequest.EmpNumb
                                        ).Contains(_msEmployee.EmpNumb)
                                orderby _msEmployee.EmpName ascending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { EmpNumb = this._string, EmpName = this._string });

                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForDDLInHandOver(Guid _prmTerminationRequestCode)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                    && !(
                                            from _terminationRequest in this.db.HRM_TerminationRequests
                                            where _terminationRequest.EmpNumb == _msEmployee.EmpNumb
                                                && _terminationRequest.TerminationRequestCode == _prmTerminationRequestCode
                                            select _terminationRequest.EmpNumb
                                        ).Contains(_msEmployee.EmpNumb)
                                    && !(
                                            from _terminationHandOver in this.db.HRM_TerminationHandOvers
                                            where _terminationHandOver.EmpNumb == _msEmployee.EmpNumb
                                                && _terminationHandOver.TerminationRequestCode == _prmTerminationRequestCode
                                            select _terminationHandOver.EmpNumb
                                        ).Contains(_msEmployee.EmpNumb)
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { EmpNumb = this._string, EmpName = this._string });

                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForDDLNotInAbsenceRequest(string _prmAbsReqCode)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                    && !(
                                            from _absReq in this.db.HRM_AbsenceRequests
                                            where _absReq.AbsenceRequestCode == new Guid(_prmAbsReqCode)
                                            select _absReq.EmpNumb
                                       ).Contains(_msEmployee.EmpNumb)
                                orderby _msEmployee.UserDate descending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { EmpNumb = this._string, EmpName = this._string });

                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForDDLNotInInvitation(string _prmCode)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            try
            {
                var _query = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                    && !(
                                            from _absReq in this.db.HRM_TerminationReqCommentInvitations
                                            where _absReq.TerminationRequestCode == new Guid(_prmCode)
                                            select _absReq.EmpNumb
                                       ).Contains(_msEmployee.EmpNumb)
                                orderby _msEmployee.UserDate descending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForDDLUserNameAdd()
        {
            List<MsEmployee> _result = new List<MsEmployee>();
            MTJMembershipDataContext db2 = new MTJMembershipDataContext(ApplicationConfig.MembershipConnString);

            try
            {
                var _queryMapper = (
                                    from _userEmp in db2.User_Employees
                                    select new
                                    {
                                        EmpNumb = _userEmp.EmployeeId
                                    }
                                );

                List<String> _mapper = new List<String>();
                foreach (var _item in _queryMapper)
                {
                    _mapper.Add(_item.EmpNumb);
                }

                var _queryEmp = (
                                from _msEmployee in this.db.MsEmployees
                                where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                    && !(_mapper).Contains(_msEmployee.EmpNumb)
                                orderby _msEmployee.EmpName descending
                                select new
                                {
                                    EmpNumb = _msEmployee.EmpNumb,
                                    EmpName = _msEmployee.EmpName
                                }
                            );

                foreach (var _rowEmp in _queryEmp)
                {
                    _result.Add(new MsEmployee(_rowEmp.EmpNumb, _rowEmp.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsEmployee> GetListEmpForDDLUserNameEdit(string _prmEmpNumb)
        {
            List<MsEmployee> _result = new List<MsEmployee>();
            MTJMembershipDataContext db2 = new MTJMembershipDataContext(ApplicationConfig.MembershipConnString);

            try
            {
                var _queryMapper = (
                                        from _userEmp in db2.User_Employees
                                        select new
                                        {
                                            EmpNumb = _userEmp.EmployeeId
                                        }
                                   );

                List<String> _mapper = new List<String>();
                foreach (var _item in _queryMapper)
                {
                    _mapper.Add(_item.EmpNumb);
                }
                _mapper.Remove(_prmEmpNumb);

                var _queryEmp = (
                                    from _msEmployee in this.db.MsEmployees
                                    where _msEmployee.FgActive == EmployeeDataMapper.IsActive(true)
                                        && !(_mapper).Contains(_msEmployee.EmpNumb)
                                    orderby _msEmployee.EmpName descending
                                    select new
                                    {
                                        EmpNumb = _msEmployee.EmpNumb,
                                        EmpName = _msEmployee.EmpName
                                    }
                                );

                foreach (var _rowEmp in _queryEmp)
                {
                    _result.Add(new MsEmployee(_rowEmp.EmpNumb, _rowEmp.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddEmployeeList(List<MsEmployee> _prmMsEmployeeList)
        {
            bool _result = false;

            try
            {
                foreach (MsEmployee _row in _prmMsEmployeeList)
                {
                    MsEmployee _msEmployee = new MsEmployee();

                    _msEmployee.EmpNumb = _row.EmpNumb;
                    _msEmployee.EmpName = _row.EmpName;
                    _msEmployee.Gender = _row.Gender;
                    _msEmployee.BirthPlace = _row.BirthPlace;
                    _msEmployee.BirthDate = _row.BirthDate;
                    _msEmployee.Tribe = _row.Tribe;
                    _msEmployee.AbsenceCardNo = _row.AbsenceCardNo;
                    _msEmployee.TypeCard = _row.TypeCard;
                    _msEmployee.IDCard = _row.IDCard;
                    _msEmployee.Religion = _row.Religion;
                    _msEmployee.BloodType = _row.BloodType;
                    _msEmployee.Weight = _row.Weight;
                    _msEmployee.Height = _row.Height;
                    _msEmployee.HandPhone1 = _row.HandPhone1;
                    _msEmployee.HandPhone2 = _row.HandPhone2;
                    _msEmployee.Email = _row.Email;
                    _msEmployee.FgPPh21 = _row.FgPPh21;
                    _msEmployee.NPWP = _row.NPWP;
                    _msEmployee.MaritalSt = _row.MaritalSt;
                    _msEmployee.MaritalTax = _row.MaritalTax;
                    _msEmployee.StartDate = _row.StartDate;
                    _msEmployee.TakenLeave = _row.TakenLeave;
                    _msEmployee.EndDate = _row.EndDate;
                    _msEmployee.PensiunNo = _row.PensiunNo;
                    _msEmployee.JamSosTekNo = _row.JamSosTekNo;
                    _msEmployee.JamSosTekDate = _row.JamSosTekDate;
                    _msEmployee.ResAddr1 = _row.ResAddr1;
                    _msEmployee.ResAddr2 = _row.ResAddr2;
                    _msEmployee.ResZipCode = _row.ResZipCode;
                    _msEmployee.ResCity = _row.ResCity;
                    _msEmployee.ResPhone = _row.ResPhone;
                    _msEmployee.OriAddr1 = _row.OriAddr1;
                    _msEmployee.OriAddr2 = _row.OriAddr2;
                    _msEmployee.OriZipCode = _row.OriZipCode;
                    _msEmployee.OriCity = _row.OriCity;
                    _msEmployee.OriPhone = _row.OriPhone;
                    _msEmployee.SalaryType = _row.SalaryType;
                    _msEmployee.FgActive = _row.FgActive;
                    _msEmployee.SKNo = _row.SKNo;
                    _msEmployee.JobLevel = _row.JobLevel;
                    _msEmployee.JobTitle = _row.JobTitle;
                    _msEmployee.EmpType = _row.EmpType;
                    _msEmployee.WorkPlace = _row.WorkPlace;
                    _msEmployee.OldEmpNumb = _row.OldEmpNumb;
                    _msEmployee.UserID = _row.UserID;
                    _msEmployee.UserDate = _row.UserDate;
                    _msEmployee.FgSales = _row.FgSales;
                    _msEmployee.FgDriver = _row.FgDriver;
                    _msEmployee.FgOperator = _row.FgOperator;
                    _msEmployee.FgTeknisi = _row.FgTeknisi;
                    _msEmployee.Nationality = _row.Nationality;
                    _msEmployee.MaritalDate = _row.MaritalDate;
                    _msEmployee.MaritalDocNo = _row.MaritalDocNo;
                    _msEmployee.Photo = _row.Photo;
                    _msEmployee.FingerPrintID = _row.FingerPrintID;
                    _msEmployee.MethodSalary = _row.MethodSalary;
                    _msEmployee.ContractEndDate = _row.ContractEndDate;
                    _msEmployee.BankCode = _row.BankCode;
                    _msEmployee.BankRekNo = _row.BankRekNo;
                    _msEmployee.BankRekName = _row.BankRekName;
                    _msEmployee.NPWPDate = _row.NPWPDate;

                    this.db.MsEmployees.InsertOnSubmit(_msEmployee);
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

        public List<MsEmployee> GetListEmpForBankAccount(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsEmployee> _result = new List<MsEmployee>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";
            string _pattern5 = "%%";
            string _pattern6 = "%%";

            if (_prmCategory == "EmpNumb")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "JobTtl")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "JobLvl")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern2 = "%%";
                _pattern5 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "WorkPlace")
            {
                _pattern5 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern2 = "%%";
                _pattern6 = "%%";
            }
            else if (_prmCategory == "EmpType")
            {
                _pattern6 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
                _pattern5 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                 from _msEmployee in this.db.MsEmployees
                                 join _msJobTtl in this.db.MsJobTitles on _msEmployee.JobTitle equals _msJobTtl.JobTtlCode
                                 join _msJobLvl in this.db.MsJobLevels on _msEmployee.JobLevel equals _msJobLvl.JobLvlCode
                                 join _msWorkPlace in this.db.MsWorkPlaces on _msEmployee.WorkPlace equals _msWorkPlace.WorkPlaceCode
                                 join _msEmpType in this.db.MsEmpTypes on _msEmployee.EmpType equals _msEmpType.EmpTypeCode
                                 where (SqlMethods.Like(_msEmployee.EmpNumb.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msJobTtl.JobTtlName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msJobLvl.JobLvlName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                    && (SqlMethods.Like(_msWorkPlace.WorkPlaceName.Trim().ToLower(), _pattern5.Trim().ToLower()))
                                    && (SqlMethods.Like(_msEmpType.EmpTypeName.Trim().ToLower(), _pattern6.Trim().ToLower()))
                                    && _msEmployee.FgActive == 'Y'
                                 orderby _msEmployee.UserDate descending
                                 select new
                                 {
                                     EmpNumb = _msEmployee.EmpNumb,
                                     EmpName = _msEmployee.EmpName,
                                     BankName = (from _msBank in this.db.MsBanks
                                                 where _msEmployee.BankCode == _msBank.BankCode
                                                 select _msBank.BankName
                                                ).FirstOrDefault(),
                                     BankRekNo = _msEmployee.BankRekNo,
                                     BankRekName = _msEmployee.BankRekName,
                                     StartDate = _msEmployee.StartDate,
                                     JobLevel = _msJobLvl.JobLvlName,
                                     JobTitle = _msJobTtl.JobTtlName,
                                     EmpTypeName = _msEmpType.EmpTypeName,
                                     WorkPlaceName = _msWorkPlace.WorkPlaceName
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsEmployee(_row.EmpNumb, _row.EmpName, _row.JobLevel, _row.JobTitle, _row.EmpTypeName, _row.BankName, _row.BankRekNo, _row.BankRekName, _row.StartDate, _row.WorkPlaceName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region Job Title

        public double RowsCountJobTitle(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            try
            {
                var _query =
                            (
                                from _msJobTitle in this.db.MsJobTitles
                                where (SqlMethods.Like(_msJobTitle.JobTtlCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msJobTitle.JobTtlName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _msJobTitle.JobTtlCode

                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountJobTitle()
        {
            double _result = 0;

            try
            {
                var _query =
                            (
                                from _msJobTitle in this.db.MsJobTitles
                                select _msJobTitle.JobTtlCode
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MsJobTitle GetSingleJobTitle(string _prmJobTitleCode)
        {
            MsJobTitle _result = null;

            try
            {
                _result = this.db.MsJobTitles.Single(_jobTitle => _jobTitle.JobTtlCode == _prmJobTitleCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetJobTitleNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msJobTitle in this.db.MsJobTitles
                                where _msJobTitle.JobTtlCode == _prmCode
                                select new
                                {
                                    JobTtlName = _msJobTitle.JobTtlName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.JobTtlName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetJobTitleCodeByName(string _prmName)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msJobTitle in this.db.MsJobTitles
                                where _msJobTitle.JobTtlName == _prmName
                                select new
                                {
                                    JobTtlCode = _msJobTitle.JobTtlCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.JobTtlCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsJobTitle> GetListJobTitle(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsJobTitle> _result = new List<MsJobTitle>();
            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            try
            {
                var _query = (
                                from job in this.db.MsJobTitles
                                where (SqlMethods.Like(job.JobTtlCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(job.JobTtlName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby job.UserDate descending
                                select new
                                {
                                    JobTitleCode = job.JobTtlCode,
                                    JobTitleName = job.JobTtlName,
                                    IsGetOT = job.IsGetOT
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsJobTitle(_row.JobTitleCode, _row.JobTitleName, _row.IsGetOT));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsJobTitle> GetListJobTitle(int _prmReqPage, int _prmPageSize)
        {
            List<MsJobTitle> _result = new List<MsJobTitle>();

            try
            {
                var _query = (
                                from job in this.db.MsJobTitles
                                orderby job.JobTtlCode ascending
                                select new
                                {
                                    JobTitleCode = job.JobTtlCode,
                                    JobTitleName = job.JobTtlName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsJobTitle(_row.JobTitleCode, _row.JobTitleName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsJobTitle> GetListJobTitleForDDL()
        {
            List<MsJobTitle> _result = new List<MsJobTitle>();

            try
            {
                var _query = (
                                from _msJobTitle in this.db.MsJobTitles
                                orderby _msJobTitle.JobTtlName ascending
                                select new
                                {
                                    JobTtlCode = _msJobTitle.JobTtlCode,
                                    JobTtlName = _msJobTitle.JobTtlName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsJobTitle(_row.JobTtlCode, _row.JobTtlName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditJobTitle(MsJobTitle _prmMsJobTitle)
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

        public bool AddJobTitle(MsJobTitle _prmMsJobTitle)
        {
            bool _result = false;

            try
            {
                this.db.MsJobTitles.InsertOnSubmit(_prmMsJobTitle);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiJobTitle(string[] _prmJobTitleCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmJobTitleCode.Length; i++)
                {
                    MsJobTitle _msJobTitle = this.db.MsJobTitles.Single(_job => _job.JobTtlCode.Trim().ToLower() == _prmJobTitleCode[i].Trim().ToLower());

                    this.db.MsJobTitles.DeleteOnSubmit(_msJobTitle);
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
        #endregion

        #region Job Level
        public double RowsCountJobLevel(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query =
                    (
                        from _msJobLevel in this.db.MsJobLevels
                        where (SqlMethods.Like(_msJobLevel.JobLvlCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msJobLevel.JobLvlName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                        select _msJobLevel.JobLvlCode

                    ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountJobLevel()
        {
            double _result = 0;

            try
            {
                var _query =
                            (
                                from _msJobLevel in this.db.MsJobLevels
                                select _msJobLevel.JobLvlCode
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MsJobLevel GetSingleJobLevel(string _prmJobLevelCode)
        {
            MsJobLevel _result = null;

            try
            {
                _result = this.db.MsJobLevels.Single(_emp => _emp.JobLvlCode == _prmJobLevelCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetJobLevelNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msJobLevel in this.db.MsJobLevels
                                where _msJobLevel.JobLvlCode == _prmCode
                                select new
                                {
                                    JobLvlName = _msJobLevel.JobLvlName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.JobLvlName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsJobLevel> GetListJobLevel(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsJobLevel> _result = new List<MsJobLevel>();
            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            try
            {
                var _query = (
                                from _msJobLevel in this.db.MsJobLevels
                                where (SqlMethods.Like(_msJobLevel.JobLvlCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msJobLevel.JobLvlName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msJobLevel.UserDate descending
                                select new
                                {
                                    JobLvlCode = _msJobLevel.JobLvlCode,
                                    JobLvlName = _msJobLevel.JobLvlName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { JobLvlCode = this._string, JobLvlName = this._string });

                    _result.Add(new MsJobLevel(_row.JobLvlCode, _row.JobLvlName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsJobLevel> GetListJobLevel(int _prmReqPage, int _prmPageSize)
        {
            List<MsJobLevel> _result = new List<MsJobLevel>();

            try
            {
                var _query = (
                                from _msJobLevel in this.db.MsJobLevels
                                orderby _msJobLevel.JobLvlCode ascending
                                select new
                                {
                                    JobLvlCode = _msJobLevel.JobLvlCode,
                                    JobLvlName = _msJobLevel.JobLvlName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { JobLvlCode = this._string, JobLvlName = this._string });

                    _result.Add(new MsJobLevel(_row.JobLvlCode, _row.JobLvlName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsJobLevel> GetListJobLevelForDDL()
        {
            List<MsJobLevel> _result = new List<MsJobLevel>();

            try
            {
                var _query = (
                                from _msJobLevel in this.db.MsJobLevels
                                orderby _msJobLevel.JobLvlName ascending
                                select new
                                {
                                    JobLvlCode = _msJobLevel.JobLvlCode,
                                    JobLvlName = _msJobLevel.JobLvlName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsJobLevel(_row.JobLvlCode, _row.JobLvlName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiJobLevel(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsJobLevel _msJobLevel = this.db.MsJobLevels.Single(_temp => _temp.JobLvlCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsJobLevels.DeleteOnSubmit(_msJobLevel);
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

        public bool AddJobLevel(MsJobLevel _prmMsJobLevel)
        {
            bool _result = false;

            try
            {
                this.db.MsJobLevels.InsertOnSubmit(_prmMsJobLevel);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditJobLevel(MsJobLevel _prmMsJobLevel)
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
        #endregion

        #region Education
        public double RowsCountEducation(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";


            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                (
                    from _master_Education in this.db.Master_Educations
                    where (SqlMethods.Like(_master_Education.EducationName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    select _master_Education.EducationCode
                ).Count();

            _result = _query;
            return _result;

        }

        public Master_Education GetSingleEducation(Guid _prmEducationCode)
        {
            Master_Education _result = null;

            try
            {
                _result = this.db.Master_Educations.Single(_temp => _temp.EducationCode == _prmEducationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetEducationNameByCode(Guid _prmEducationCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _master_Education in this.db.Master_Educations
                                where _master_Education.EducationCode == _prmEducationCode
                                select new
                                {
                                    EducationName = _master_Education.EducationName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.EducationName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Education> GetListEducation(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_Education> _result = new List<Master_Education>();

            string _pattern1 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _master_Education in this.db.Master_Educations
                                where (SqlMethods.Like(_master_Education.EducationName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _master_Education.EducationName ascending
                                select new
                                {
                                    EducationCode = _master_Education.EducationCode,
                                    EducationName = _master_Education.EducationName,
                                    EducationDescription = _master_Education.EducationDescription
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Education(_row.EducationCode, _row.EducationName, _row.EducationDescription));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Education> GetListEducationForDDL()
        {
            List<Master_Education> _result = new List<Master_Education>();

            try
            {
                var _query = (
                                from _master_Education in this.db.Master_Educations
                                orderby _master_Education.EducationName ascending
                                select new
                                {
                                    EducationCode = _master_Education.EducationCode,
                                    EducationName = _master_Education.EducationName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Education(_row.EducationCode, _row.EducationName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEducation(Master_Education _prmMaster_Education)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsEducationName(_prmMaster_Education.EducationName, _prmMaster_Education.EducationCode) == false)
                {
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddEducation(Master_Education _prmMaster_Education)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsEducationName(_prmMaster_Education.EducationName, _prmMaster_Education.EducationCode) == false)
                {
                    this.db.Master_Educations.InsertOnSubmit(_prmMaster_Education);
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        private bool IsExistsEducationName(String _prmEducationName, Guid _prmEducationCode)
        {
            bool _result = false;

            try
            {
                var _query = from _master_Education in this.db.Master_Educations
                             where _master_Education.EducationName == _prmEducationName && (_master_Education.EducationCode != _prmEducationCode)
                             select new
                             {
                                 _master_Education.EducationCode
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

        public bool DeleteMultiEducation(string[] _prmEducationCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEducationCode.Length; i++)
                {
                    Master_Education _master_Education = this.db.Master_Educations.Single(_temp => _temp.EducationCode == new Guid(_prmEducationCode[i]));

                    this.db.Master_Educations.DeleteOnSubmit(_master_Education);
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
        #endregion

        #region EmpSkill
        public double RowsCountEmpSkill(string _prmEmpNumb)
        {
            double _result = 0;


            var _query =
                (
                    from _master_EmpSkill in this.db.Master_EmpSkills
                    where _master_EmpSkill.EmpNmbr == _prmEmpNumb
                    select _master_EmpSkill.EmpSkillCode
                ).Count();

            _result = _query;
            return _result;

        }

        public Master_EmpSkill GetSingleEmpSkill(Guid _prmEmpSkillCode, string _prmEmpNumb)
        {
            Master_EmpSkill _result = null;

            try
            {
                _result = this.db.Master_EmpSkills.Single(_temp => _temp.EmpSkillCode == _prmEmpSkillCode && _temp.EmpNmbr == _prmEmpNumb);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_EmpSkill> GetListEmpSkill(int _prmReqPage, int _prmPageSize, string _prmEmpNumb)
        {
            List<Master_EmpSkill> _result = new List<Master_EmpSkill>();

            try
            {
                var _query = (
                                from _master_EmpSkill in this.db.Master_EmpSkills
                                where _master_EmpSkill.EmpNmbr == _prmEmpNumb
                                orderby _master_EmpSkill.EditDate descending
                                select new
                                {
                                    EmpSkillCode = _master_EmpSkill.EmpSkillCode,
                                    EmpNmbr = _master_EmpSkill.EmpNmbr,
                                    SkillCode = _master_EmpSkill.SkillCode,
                                    SkillName = (
                                                    from _master_Skill in this.db.Master_Skills
                                                    where _master_EmpSkill.SkillCode == _master_Skill.SkillCode
                                                    select _master_Skill.SkillName
                                                ).FirstOrDefault(),
                                    Lvl = _master_EmpSkill.Lvl,
                                    LevelDate = _master_EmpSkill.LevelDate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_EmpSkill(_row.EmpSkillCode, _row.EmpNmbr, _row.SkillCode, _row.SkillName, _row.Lvl, _row.LevelDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEmpSkill(Master_EmpSkill _prmMaster_EmpSkill)
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

        public bool AddEmpSkill(Master_EmpSkill _prmMaster_EmpSkill)
        {
            bool _result = false;

            try
            {
                this.db.Master_EmpSkills.InsertOnSubmit(_prmMaster_EmpSkill);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiEmpSkill(string _prmEmpNmbr, string[] _prmEmpSkillCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpSkillCode.Length; i++)
                {
                    Master_EmpSkill _Master_EmpSkill = this.db.Master_EmpSkills.Single(_temp => _temp.EmpSkillCode == new Guid(_prmEmpSkillCode[i]) && _temp.EmpNmbr == _prmEmpNmbr);

                    this.db.Master_EmpSkills.DeleteOnSubmit(_Master_EmpSkill);
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
        #endregion

        #region EmpEdu
        public double RowsCountEmpEdu(string _prmEmpNumb)
        {
            double _result = 0;


            var _query =
                (
                    from _master_EmpEdu in this.db.Master_EmpEdus
                    where _master_EmpEdu.EmpNmbr == _prmEmpNumb
                    select _master_EmpEdu.EmpEduCode
                ).Count();

            _result = _query;
            return _result;

        }

        public Master_EmpEdu GetSingleEmpEdu(Guid _prmEmpEduCode, string _prmEmpNumb)
        {
            Master_EmpEdu _result = null;

            try
            {
                _result = this.db.Master_EmpEdus.Single(_temp => _temp.EmpEduCode == _prmEmpEduCode && _temp.EmpNmbr == _prmEmpNumb);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_EmpEdu> GetListEmpEdu(int _prmReqPage, int _prmPageSize, string _prmEmpNumb)
        {
            List<Master_EmpEdu> _result = new List<Master_EmpEdu>();

            try
            {
                var _query = (
                                from _master_EmpEdu in this.db.Master_EmpEdus
                                where _master_EmpEdu.EmpNmbr == _prmEmpNumb
                                orderby _master_EmpEdu.EditDate descending
                                select new
                                {
                                    EmpEduCode = _master_EmpEdu.EmpEduCode,
                                    EmpNmbr = _master_EmpEdu.EmpNmbr,
                                    EducationCode = _master_EmpEdu.EducationCode,
                                    EducationName = (
                                                        from _master_Edu in this.db.Master_Educations
                                                        where _master_EmpEdu.EducationCode == _master_Edu.EducationCode
                                                        select _master_Edu.EducationName
                                                    ).FirstOrDefault(),
                                    Degree = _master_EmpEdu.Degree,
                                    Institution = _master_EmpEdu.Institution,
                                    CityCode = _master_EmpEdu.CityCode,
                                    CityName = (
                                                    from _msCity in this.db.MsCities
                                                    where _master_EmpEdu.CityCode == _msCity.CityCode
                                                    select _msCity.CityName
                                               ).FirstOrDefault(),
                                    StartYear = _master_EmpEdu.StartYear,
                                    Graduated = _master_EmpEdu.Graduated
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_EmpEdu(_row.EmpEduCode, _row.EmpNmbr, _row.EducationCode, _row.EducationName, _row.Degree, _row.Institution, _row.CityCode, _row.CityName, _row.StartYear, _row.Graduated));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEmpEdu(Master_EmpEdu _prmMaster_EmpEdu)
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

        public bool AddEmpEdu(Master_EmpEdu _prmMaster_EmpEdu)
        {
            bool _result = false;

            try
            {
                this.db.Master_EmpEdus.InsertOnSubmit(_prmMaster_EmpEdu);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiEmpEdu(string _prmEmpNmbr, string[] _prmEmpEduCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpEduCode.Length; i++)
                {
                    Master_EmpEdu _master_EmpEdu = this.db.Master_EmpEdus.Single(_temp => _temp.EmpEduCode == new Guid(_prmEmpEduCode[i]) && _temp.EmpNmbr == _prmEmpNmbr);

                    this.db.Master_EmpEdus.DeleteOnSubmit(_master_EmpEdu);
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
        #endregion

        #region EmpFamily
        public double RowsCountEmpFamily(string _prmEmpNumb)
        {
            double _result = 0;


            var _query =
                (
                    from _master_EmpFamily in this.db.Master_EmpFamilies
                    where _master_EmpFamily.EmpNumb == _prmEmpNumb
                    select _master_EmpFamily.EmpFamilyCode
                ).Count();

            _result = _query;
            return _result;

        }

        public Master_EmpFamily GetSingleEmpFamily(Guid _prmEmpFamilyCode, string _prmEmpNumb)
        {
            Master_EmpFamily _result = null;

            try
            {
                _result = this.db.Master_EmpFamilies.Single(_temp => _temp.EmpFamilyCode == _prmEmpFamilyCode && _temp.EmpNumb == _prmEmpNumb);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_EmpFamily> GetListEmpFamily(int _prmReqPage, int _prmPageSize, string _prmEmpNumb)
        {
            List<Master_EmpFamily> _result = new List<Master_EmpFamily>();

            try
            {
                var _query = (
                                from _master_EmpFamily in this.db.Master_EmpFamilies
                                where _master_EmpFamily.EmpNumb == _prmEmpNumb
                                orderby _master_EmpFamily.EditDate descending
                                select new
                                {
                                    EmpFamilyCode = _master_EmpFamily.EmpFamilyCode,
                                    EmpNumb = _master_EmpFamily.EmpNumb,
                                    FamilyName = _master_EmpFamily.FamilyName,
                                    FamilyStatus = _master_EmpFamily.FamilyStatus,
                                    Gender = _master_EmpFamily.Gender,
                                    Address1 = _master_EmpFamily.Address1,
                                    Address2 = _master_EmpFamily.Address2,
                                    Phone = _master_EmpFamily.Phone,
                                    HP = _master_EmpFamily.HP
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_EmpFamily(_row.EmpFamilyCode, _row.EmpNumb, _row.FamilyName, _row.FamilyStatus, _row.Gender, _row.Address1, _row.Address2, _row.Phone, _row.HP));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEmpFamily(Master_EmpFamily _prmMaster_EmpFamily)
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

        public bool AddEmpFamily(Master_EmpFamily _prmMaster_EmpFamily)
        {
            bool _result = false;

            try
            {
                this.db.Master_EmpFamilies.InsertOnSubmit(_prmMaster_EmpFamily);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiEmpFamily(string _prmEmpNmbr, string[] _prmEmpFamilyCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpFamilyCode.Length; i++)
                {
                    Master_EmpFamily _master_EmpFamily = this.db.Master_EmpFamilies.Single(_temp => _temp.EmpFamilyCode == new Guid(_prmEmpFamilyCode[i]) && _temp.EmpNumb == _prmEmpNmbr);

                    this.db.Master_EmpFamilies.DeleteOnSubmit(_master_EmpFamily);
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
        #endregion

        #region EmpBank
        public double RowsCountEmpBank(string _prmEmpNumb)
        {
            double _result = 0;


            var _query =
                (
                    from _master_EmpBank in this.db.Master_EmpBanks
                    where _master_EmpBank.EmpNmbr == _prmEmpNumb
                    select _master_EmpBank.EmpBankCode
                ).Count();

            _result = _query;
            return _result;

        }

        public Master_EmpBank GetSingleEmpBank(Guid _prmEmpBankCode, string _prmEmpNumb)
        {
            Master_EmpBank _result = null;

            try
            {
                _result = this.db.Master_EmpBanks.Single(_temp => _temp.EmpBankCode == _prmEmpBankCode && _temp.EmpNmbr == _prmEmpNumb);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_EmpBank> GetListEmpBank(int _prmReqPage, int _prmPageSize, string _prmEmpNumb)
        {
            List<Master_EmpBank> _result = new List<Master_EmpBank>();

            try
            {
                var _query = (
                                from _master_EmpBank in this.db.Master_EmpBanks
                                where _master_EmpBank.EmpNmbr == _prmEmpNumb
                                orderby _master_EmpBank.EditDate descending
                                select new
                                {
                                    EmpBankCode = _master_EmpBank.EmpBankCode,
                                    EmpNmbr = _master_EmpBank.EmpNmbr,
                                    BankCode = _master_EmpBank.BankCode,
                                    BankName = (
                                                    from _msBank in this.db.MsBanks
                                                    where _master_EmpBank.BankCode == _msBank.BankCode
                                                    select _msBank.BankName
                                                ).FirstOrDefault(),
                                    BankAddr = _master_EmpBank.BankAddr,
                                    BankAccount = _master_EmpBank.BankAccount,
                                    BankAccountName = _master_EmpBank.BankAccountName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_EmpBank(_row.EmpBankCode, _row.EmpNmbr, _row.BankCode, _row.BankName, _row.BankAddr, _row.BankAccount, _row.BankAccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEmpBank(Master_EmpBank _prmMaster_EmpBank)
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

        public bool AddEmpBank(Master_EmpBank _prmMaster_EmpBank)
        {
            bool _result = false;

            try
            {
                this.db.Master_EmpBanks.InsertOnSubmit(_prmMaster_EmpBank);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiEmpBank(string _prmEmpNmbr, string[] _prmEmpBankCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpBankCode.Length; i++)
                {
                    Master_EmpBank _master_EmpBank = this.db.Master_EmpBanks.Single(_temp => _temp.EmpBankCode == new Guid(_prmEmpBankCode[i]) && _temp.EmpNmbr == _prmEmpNmbr);

                    this.db.Master_EmpBanks.DeleteOnSubmit(_master_EmpBank);
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
        #endregion

        #region EmpExp
        public double RowsCountEmpExp(string _prmEmpNumb)
        {
            double _result = 0;


            var _query =
                (
                    from _master_EmpExp in this.db.Master_EmpExps
                    where _master_EmpExp.EmpNumb == _prmEmpNumb
                    select _master_EmpExp.EmpExpCode
                ).Count();

            _result = _query;
            return _result;

        }

        public Master_EmpExp GetSingleEmpExp(Guid _prmEmpExpCode, string _prmEmpNumb)
        {
            Master_EmpExp _result = null;

            try
            {
                _result = this.db.Master_EmpExps.Single(_temp => _temp.EmpExpCode == _prmEmpExpCode && _temp.EmpNumb == _prmEmpNumb);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_EmpExp> GetListEmpExp(int _prmReqPage, int _prmPageSize, string _prmEmpNumb)
        {
            List<Master_EmpExp> _result = new List<Master_EmpExp>();

            try
            {
                var _query = (
                                from _master_EmpExp in this.db.Master_EmpExps
                                where _master_EmpExp.EmpNumb == _prmEmpNumb
                                orderby _master_EmpExp.EditDate descending
                                select new
                                {
                                    EmpExpCode = _master_EmpExp.EmpExpCode,
                                    EmpNumb = _master_EmpExp.EmpNumb,
                                    CompanyName = _master_EmpExp.CompanyName,
                                    CompanyCity = _master_EmpExp.CompanyCity,
                                    CompanyCityName = (
                                                            from _msCity in this.db.MsCities
                                                            where _master_EmpExp.CompanyCity == _msCity.CityCode
                                                            select _msCity.CityName
                                                       ).FirstOrDefault(),
                                    JobTitle = _master_EmpExp.JobTitle,
                                    JobTitleName = (
                                                        from _msJobTitle in this.db.MsJobTitles
                                                        where _master_EmpExp.JobTitle == _msJobTitle.JobTtlCode
                                                        select _msJobTitle.JobTtlName
                                                   ).FirstOrDefault(),
                                    StartDate = _master_EmpExp.StartDate,
                                    EndDate = _master_EmpExp.EndDate,
                                    Address1 = _master_EmpExp.Address1,
                                    Address2 = _master_EmpExp.Address2,
                                    Phone = _master_EmpExp.Phone,
                                    CurrCode = _master_EmpExp.CurrCode,
                                    LastSalary = _master_EmpExp.LastSalary
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_EmpExp(_row.EmpExpCode, _row.EmpNumb, _row.CompanyName, _row.CompanyCity, _row.CompanyCityName, _row.JobTitle, _row.JobTitleName, _row.StartDate, _row.EndDate, _row.Address1, _row.Address2, _row.Phone, _row.CurrCode, _row.LastSalary));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEmpExp(Master_EmpExp _prmMaster_EmpExp)
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

        public bool AddEmpExp(Master_EmpExp _prmMaster_EmpExp)
        {
            bool _result = false;

            try
            {
                this.db.Master_EmpExps.InsertOnSubmit(_prmMaster_EmpExp);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiEmpExp(string _prmEmpNmbr, string[] _prmEmpExpCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpExpCode.Length; i++)
                {
                    Master_EmpExp _master_EmpExp = this.db.Master_EmpExps.Single(_temp => _temp.EmpExpCode == new Guid(_prmEmpExpCode[i]) && _temp.EmpNumb == _prmEmpNmbr);

                    this.db.Master_EmpExps.DeleteOnSubmit(_master_EmpExp);
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
        #endregion

        #region EmpLeaveDay

        public Master_EmpLeaveDay GetSingleEmpLeaveDay(string _prmEmpNumb)
        {
            Master_EmpLeaveDay _result = null;

            try
            {
                _result = this.db.Master_EmpLeaveDays.Single(_temp => _temp.EmpNumb == _prmEmpNumb);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_EmpLeaveDay> GetListEmpLeaveDay(int _prmReqPage, int _prmPageSize, string _prmEmpNumb)
        {
            List<Master_EmpLeaveDay> _result = new List<Master_EmpLeaveDay>();

            try
            {
                var _query = (
                                from _master_EmpLeaveDay in this.db.Master_EmpLeaveDays
                                where _master_EmpLeaveDay.EmpNumb == _prmEmpNumb
                                orderby _master_EmpLeaveDay.EditDate descending
                                select new
                                {
                                    EmpNumb = _master_EmpLeaveDay.EmpNumb,
                                    LeaveDayRemain = _master_EmpLeaveDay.LeaveDayRemain,
                                    ExpiredDateLeaveRemain = _master_EmpLeaveDay.ExpiredDateLeaveRemain,
                                    LeaveCurrent = _master_EmpLeaveDay.LeaveCurrent,
                                    ExpiredDateLeaveCurrent = _master_EmpLeaveDay.ExpiredDateLeaveCurrent
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_EmpLeaveDay(_row.EmpNumb, _row.LeaveDayRemain, _row.ExpiredDateLeaveRemain, _row.LeaveCurrent, _row.ExpiredDateLeaveCurrent));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEmpLeaveDay(Master_EmpLeaveDay _prmMaster_EmpLeaveDay)
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

        public bool AddEmpLeaveDay(Master_EmpLeaveDay _prmMaster_EmpLeaveDay)
        {
            bool _result = false;

            try
            {
                this.db.Master_EmpLeaveDays.InsertOnSubmit(_prmMaster_EmpLeaveDay);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiEmpLeaveDay(string[] _prmEmpNumb)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpNumb.Length; i++)
                {
                    Master_EmpLeaveDay _master_EmpLeaveDay = this.db.Master_EmpLeaveDays.Single(_temp => _temp.EmpNumb == _prmEmpNumb[i]);

                    this.db.Master_EmpLeaveDays.DeleteOnSubmit(_master_EmpLeaveDay);
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
        #endregion

        #region Employee_WorkingHour
        public double RowsCountEmployeeWorkingHour(string _prmEmpNumb)
        {
            double _result = 0;

            var _query =
                (
                    from _msEmployeeWorkingHour in this.db.HRM_Employee_WorkingHours
                    where _msEmployeeWorkingHour.EmpNumb == _prmEmpNumb
                    select _msEmployeeWorkingHour.EmployeeWorkingHourCode
                ).Count();

            _result = _query;

            return _result;

        }

        public HRM_Employee_WorkingHour GetSingleEmployeeWorkingHour(Guid _prmEmployeeWorkingHourCode)
        {
            HRM_Employee_WorkingHour _result = null;

            try
            {
                _result = this.db.HRM_Employee_WorkingHours.Single(_emp => _emp.EmployeeWorkingHourCode == _prmEmployeeWorkingHourCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_Employee_WorkingHour> GetListEmployeeWorkingHour(int _prmReqPage, int _prmPageSize, string _prmEmpNumb)
        {
            List<HRM_Employee_WorkingHour> _result = new List<HRM_Employee_WorkingHour>();

            try
            {
                var _query = (
                                from _employeeWorkingHour in this.db.HRM_Employee_WorkingHours
                                where _employeeWorkingHour.EmpNumb == _prmEmpNumb
                                orderby _employeeWorkingHour.EditDate descending
                                select new
                                {
                                    EmployeeWorkingHourCode = _employeeWorkingHour.EmployeeWorkingHourCode,
                                    EmpNumb = _employeeWorkingHour.EmpNumb,
                                    WorkingHourCode = _employeeWorkingHour.WorkingHourCode,
                                    WorkingHourName = (
                                                            from _workingHour in this.db.HRM_Master_WorkingHours
                                                            where _workingHour.WorkingHourCode == _employeeWorkingHour.WorkingHourCode
                                                            select _workingHour.WorkingHourName
                                                        ).FirstOrDefault(),
                                    StartDate = _employeeWorkingHour.StartDate,
                                    EndDate = _employeeWorkingHour.EndDate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_Employee_WorkingHour(_row.EmployeeWorkingHourCode, _row.EmpNumb, _row.WorkingHourCode, _row.WorkingHourName, _row.StartDate, _row.EndDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEmployeeWorkingHour(HRM_Employee_WorkingHour _prmHRM_Employee_WorkingHour)
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

        public bool AddEmployeeWorkingHour(HRM_Employee_WorkingHour _prmHRM_Employee_WorkingHour)
        {
            bool _result = false;

            try
            {
                this.db.HRM_Employee_WorkingHours.InsertOnSubmit(_prmHRM_Employee_WorkingHour);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiEmployeeWorkingHour(string[] _prmEmployeeWorkingHourCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmployeeWorkingHourCode.Length; i++)
                {
                    HRM_Employee_WorkingHour _msEmpType = this.db.HRM_Employee_WorkingHours.Single(_emp => _emp.EmployeeWorkingHourCode == new Guid(_prmEmployeeWorkingHourCode[i]));

                    this.db.HRM_Employee_WorkingHours.DeleteOnSubmit(_msEmpType);
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

        #endregion

        public String ChangeEmployeePicture(String _prmEmpID, FileUpload _prmFileUpload)
        {
            String _result = "";

            String _path = _prmFileUpload.PostedFile.FileName;
            FileInfo _file = new FileInfo(_path);
            String _imagepath = ApplicationConfig.EmployeePhotoPath + _prmEmpID + _file.Extension;

            if (_path == "")
            {
                _result = "Invalid filename supplied";
            }
            if (_prmFileUpload.PostedFile.ContentLength == 0)
            {
                _result = "Invalid file content";
            }
            if (_result == "")
            {
                if (PictureHandler.IsPictureFile(_path, ApplicationConfig.ImageExtension) == true)
                {
                    System.Drawing.Image _uploadedImage = System.Drawing.Image.FromStream(_prmFileUpload.PostedFile.InputStream);

                    Decimal _width = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Width);
                    Decimal _height = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Height);

                    if (_width > Convert.ToDecimal(ApplicationConfig.ImageWidth) || _height > Convert.ToDecimal(ApplicationConfig.ImageHeight))
                    {
                        _result = "This image is too big - please resize it!";
                    }
                    else
                    {
                        if (_prmFileUpload.PostedFile.ContentLength <= Convert.ToDecimal(ApplicationConfig.ImageMaxSize))
                        {
                            MsEmployee _emp = this.GetSingleEmp(_prmEmpID);

                            if (_emp.Photo != ApplicationConfig.ImageDefault)
                            {
                                if (File.Exists(ApplicationConfig.EmployeePhotoPath + _emp.Photo) == true)
                                {
                                    File.Delete(ApplicationConfig.EmployeePhotoPath + _emp.Photo);
                                }
                            }

                            //_file.CopyTo(_imagepath, true);
                            _prmFileUpload.PostedFile.SaveAs(_imagepath);

                            _emp.Photo = _prmEmpID + _file.Extension;
                            this.db.SubmitChanges();

                            _file.Refresh();

                            _result = "File uploaded successfully";
                        }
                        else
                        {
                            _result = "Unable to upload, file exceeds maximum limit";
                        }
                    }
                }
                else
                {
                    _result = "File type not supported";
                }
            }

            return _result;
        }

        ~EmployeeBL()
        {

        }
    }
}
