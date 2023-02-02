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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class AbsenceTypeBL : Base
    {
        public AbsenceTypeBL()
        {
        }

        #region AbsenceType
        public double RowsCountAbsenceType(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "Description")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }

            var _query =
                (
                from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                where (SqlMethods.Like(_master_AbsenceType.AbsenceTypeName.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                    && (SqlMethods.Like(_master_AbsenceType.Description.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                select _master_AbsenceType.AbsenceTypeCode
                ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountAbsenceTypeForReport()
        {
            double _result = 0;

            var _query =
                        (
                            from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                            select _master_AbsenceType.AbsenceTypeCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public HRMMsAbsenceType GetSingleAbsenceType(String _prmCode)
        {
            HRMMsAbsenceType _result = null;

            try
            {
                _result = this.db.HRMMsAbsenceTypes.Single(_temp => _temp.AbsenceTypeCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetAbsenceTypeNameByCode(String _prmCode)
        {
            String _result = "";

            try
            {
                _result = this.db.HRMMsAbsenceTypes.Single(_temp => _temp.AbsenceTypeCode == _prmCode).AbsenceTypeName;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAbsenceType> GetListAbsenceType(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsAbsenceType> _result = new List<HRMMsAbsenceType>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "Description")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                                where (SqlMethods.Like(_master_AbsenceType.AbsenceTypeName.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_master_AbsenceType.Description.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _master_AbsenceType.EditDate descending
                                select new
                                {
                                    AbsenceTypeCode = _master_AbsenceType.AbsenceTypeCode,
                                    AbsenceTypeName = _master_AbsenceType.AbsenceTypeName,
                                    AbsenceTypeAlias = _master_AbsenceType.AbsenceTypeAlias,
                                    Description = _master_AbsenceType.Description,
                                    IsActingRequired = _master_AbsenceType.IsActingRequired,
                                    IsRelatedToFemaleGender = _master_AbsenceType.IsRelatedToFemaleGender,
                                    IsCutLeave = _master_AbsenceType.IsCutLeave
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsenceType(_row.AbsenceTypeCode, _row.AbsenceTypeName, _row.AbsenceTypeAlias, _row.Description, _row.IsActingRequired, _row.IsRelatedToFemaleGender, _row.IsCutLeave));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAbsenceType> GetListAbsenceType(int _prmReqPage, int _prmPageSize)
        {
            List<HRMMsAbsenceType> _result = new List<HRMMsAbsenceType>();

            try
            {
                var _query = (
                                from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                                orderby _master_AbsenceType.EditDate descending
                                select new
                                {
                                    AbsenceTypeCode = _master_AbsenceType.AbsenceTypeCode,
                                    AbsenceTypeName = _master_AbsenceType.AbsenceTypeName,
                                    AbsenceTypeAlias = _master_AbsenceType.AbsenceTypeAlias,
                                    Description = _master_AbsenceType.Description,
                                    IsActingRequired = _master_AbsenceType.IsActingRequired,
                                    IsRelatedToFemaleGender = _master_AbsenceType.IsRelatedToFemaleGender,
                                    IsCutLeave = _master_AbsenceType.IsCutLeave
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsenceType(_row.AbsenceTypeCode, _row.AbsenceTypeName, _row.AbsenceTypeAlias, _row.Description, _row.IsActingRequired, _row.IsRelatedToFemaleGender, _row.IsCutLeave));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAbsenceType> GetListAbsenceTypeForDDL()
        {
            List<HRMMsAbsenceType> _result = new List<HRMMsAbsenceType>();

            try
            {
                var _query = (
                                from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                                orderby _master_AbsenceType.AbsenceTypeName ascending
                                select new
                                {
                                    AbsenceTypeCode = _master_AbsenceType.AbsenceTypeCode,
                                    AbsenceTypeName = _master_AbsenceType.AbsenceTypeName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsenceType(_row.AbsenceTypeCode, _row.AbsenceTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAbsenceType> GetListAbsenceTypeForReport(int _prmReqPage, int _prmPageSize)
        {
            List<HRMMsAbsenceType> _result = new List<HRMMsAbsenceType>();

            try
            {
                var _query = (
                                from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                                orderby _master_AbsenceType.AbsenceTypeName ascending
                                select new
                                {
                                    AbsenceTypeCode = _master_AbsenceType.AbsenceTypeCode,
                                    AbsenceTypeName = _master_AbsenceType.AbsenceTypeName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsenceType(_row.AbsenceTypeCode, _row.AbsenceTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAbsenceType> GetListAbsenceTypeForDDLForAttendance()
        {
            List<HRMMsAbsenceType> _result = new List<HRMMsAbsenceType>();

            try
            {
                var _query = (
                                from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                                //where _master_AbsenceType.ForAttendance == true
                                orderby _master_AbsenceType.AbsenceTypeName ascending
                                select new
                                {
                                    AbsenceTypeCode = _master_AbsenceType.AbsenceTypeCode,
                                    AbsenceTypeName = _master_AbsenceType.AbsenceTypeName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsenceType(_row.AbsenceTypeCode, _row.AbsenceTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAbsenceType> GetListAbsenceTypeForDDLForAttendanceDaily()
        {
            List<HRMMsAbsenceType> _result = new List<HRMMsAbsenceType>();

            try
            {
                var _query = (
                                from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                                where _master_AbsenceType.AbsenceTypeCode == "HADIR"
                                orderby _master_AbsenceType.AbsenceTypeName ascending
                                select new
                                {
                                    AbsenceTypeCode = _master_AbsenceType.AbsenceTypeCode,
                                    AbsenceTypeName = _master_AbsenceType.AbsenceTypeName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsenceType(_row.AbsenceTypeCode, _row.AbsenceTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAbsenceType> GetListAbsenceTypeForDDLForAbsenceRequest()
        {
            List<HRMMsAbsenceType> _result = new List<HRMMsAbsenceType>();

            try
            {
                var _query = (
                                from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                                where _master_AbsenceType.IsActingRequired == true
                                orderby _master_AbsenceType.AbsenceTypeName ascending
                                select new
                                {
                                    AbsenceTypeCode = _master_AbsenceType.AbsenceTypeCode,
                                    AbsenceTypeName = _master_AbsenceType.AbsenceTypeName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsenceType(_row.AbsenceTypeCode, _row.AbsenceTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAbsenceType> GetListAbsenceTypeForDDLNotForAttendance(string _prmEmpNumb)
        {
            List<HRMMsAbsenceType> _result = new List<HRMMsAbsenceType>();

            try
            {
                var _query = (
                                from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                                join _msEmpTypeAbsenceType in this.db.HRM_EmpType_AbsenceTypes
                                    on _master_AbsenceType.AbsenceTypeCode equals _msEmpTypeAbsenceType.AbsenceTypeCode
                                join _msEmp in this.db.MsEmployees
                                    on _msEmpTypeAbsenceType.EmpTypeCode equals _msEmp.EmpType
                                where //_master_AbsenceType.ForAttendance == false && 
                                    _msEmp.EmpNumb == _prmEmpNumb
                                orderby _master_AbsenceType.AbsenceTypeName ascending
                                select new
                                {
                                    AbsenceTypeCode = _master_AbsenceType.AbsenceTypeCode,
                                    AbsenceTypeName = _master_AbsenceType.AbsenceTypeName
                                }
                            ).Union
                            (
                                from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                                join _msGenderAbsenceType in this.db.HRM_Master_Gender_Master_AbsenceTypes
                                    on _master_AbsenceType.AbsenceTypeCode equals _msGenderAbsenceType.AbsenceTypeCode
                                join _msEmp in this.db.MsEmployees
                                    on _msGenderAbsenceType.GenderCode equals _msEmp.Gender
                                where //_master_AbsenceType.ForAttendance == false && 
                                    _msEmp.EmpNumb == _prmEmpNumb
                                orderby _master_AbsenceType.AbsenceTypeName ascending
                                select new
                                {
                                    AbsenceTypeCode = _master_AbsenceType.AbsenceTypeCode,
                                    AbsenceTypeName = _master_AbsenceType.AbsenceTypeName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsenceType(_row.AbsenceTypeCode, _row.AbsenceTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAbsenceType> GetListAbsenceTypeForDDLForEmpType(string _prmAbsTypeCode)
        {
            List<HRMMsAbsenceType> _result = new List<HRMMsAbsenceType>();

            try
            {
                var _query = (
                                from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                                where !(
                                        from _mapper in this.db.HRM_EmpType_AbsenceTypes
                                        where _mapper.EmpTypeCode == _prmAbsTypeCode
                                            && _mapper.AbsenceTypeCode == _master_AbsenceType.AbsenceTypeCode
                                        select _mapper.AbsenceTypeCode
                                       ).Contains(_master_AbsenceType.AbsenceTypeCode)
                                    && _master_AbsenceType.IsRelatedToFemaleGender == false
                                //&& _master_AbsenceType.ForAttendance == false
                                orderby _master_AbsenceType.AbsenceTypeName ascending
                                select new
                                {
                                    AbsenceTypeCode = _master_AbsenceType.AbsenceTypeCode,
                                    AbsenceTypeName = _master_AbsenceType.AbsenceTypeName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsenceType(_row.AbsenceTypeCode, _row.AbsenceTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAbsenceType> GetListAbsenceTypeForDDLForGender(string _prmGenderCode)
        {
            List<HRMMsAbsenceType> _result = new List<HRMMsAbsenceType>();

            try
            {
                var _query = (
                                from _master_AbsenceType in this.db.HRMMsAbsenceTypes
                                where !(
                                        from _mapper in this.db.HRM_Master_Gender_Master_AbsenceTypes
                                        where _mapper.GenderCode == _prmGenderCode
                                            && _mapper.AbsenceTypeCode == _master_AbsenceType.AbsenceTypeCode
                                        select _mapper.AbsenceTypeCode
                                       ).Contains(_master_AbsenceType.AbsenceTypeCode)
                                    && _master_AbsenceType.IsRelatedToFemaleGender == true
                                //&& _master_AbsenceType.ForAttendance == false
                                orderby _master_AbsenceType.AbsenceTypeName ascending
                                select new
                                {
                                    AbsenceTypeCode = _master_AbsenceType.AbsenceTypeCode,
                                    AbsenceTypeName = _master_AbsenceType.AbsenceTypeName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsenceType(_row.AbsenceTypeCode, _row.AbsenceTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddAbsenceType(HRMMsAbsenceType _prmMaster_AbsenceType)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsAbsenceTypes.InsertOnSubmit(_prmMaster_AbsenceType);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditAbsenceType(HRMMsAbsenceType _prmMaster_AbsenceType)
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

        public bool DeleteMultiAbsenceType(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMMsAbsenceType _master_AbsenceType = this.db.HRMMsAbsenceTypes.Single(_temp => _temp.AbsenceTypeCode.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());
                    this.db.HRMMsAbsenceTypes.DeleteOnSubmit(_master_AbsenceType);
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

        public bool GetIsActingRequired(String _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                            from _absType in this.db.HRMMsAbsenceTypes
                            where _absType.AbsenceTypeCode == _prmCode
                                && _absType.IsActingRequired == true
                            select _absType.AbsenceTypeCode
                         ).Count();

                if (_query > 0)
                {
                    _result = true;
                }
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public bool GetIsAllowedEditDefaultDay(String _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                            from _absType in this.db.HRMMsAbsenceTypes
                            where _absType.AbsenceTypeCode == _prmCode
                            //&& _absType.IsAllowedEditDefaultDay == true
                            select _absType.AbsenceTypeCode
                         ).Count();

                if (_query > 0)
                {
                    _result = true;
                }
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public bool GetIsCutLeave(String _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                            from _absType in this.db.HRMMsAbsenceTypes
                            where _absType.AbsenceTypeCode == _prmCode
                            //&& _absType.IsCutLeave == true
                            select _absType.AbsenceTypeCode
                         ).Count();

                if (_query > 0)
                {
                    _result = true;
                }
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public bool GetIsRelatedToGender(String _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                            from _absType in this.db.HRMMsAbsenceTypes
                            where _absType.AbsenceTypeCode == _prmCode
                                && _absType.IsRelatedToFemaleGender == true
                            select _absType.AbsenceTypeCode
                         ).Count();

                if (_query > 0)
                {
                    _result = true;
                }
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public bool GetForAttendance(String _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                            from _absType in this.db.HRMMsAbsenceTypes
                            where _absType.AbsenceTypeCode == _prmCode
                            //&& _absType.ForAttendance == true
                            select _absType.AbsenceTypeCode
                         ).Count();

                if (_query > 0)
                {
                    _result = true;
                }
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        //public int GetDefaultDay(String _prmCode)
        //{
        //    int _result = 0;

        //    try
        //    {
        //        var _query = (
        //                    from _absType in this.db.HRMMsAbsenceTypes
        //                    where _absType.AbsenceTypeCode == _prmCode
        //                    select _absType.DefaultDay
        //                 ).FirstOrDefault();

        //        _result = _query;
        //    }
        //    catch (Exception Ex)
        //    {
        //    }

        //    return _result;
        //}

        //public int GetMaxDay(String _prmCode)
        //{
        //    int _result = 0;

        //    try
        //    {
        //        var _query = (
        //                    from _absType in this.db.HRMMsAbsenceTypes
        //                    where _absType.AbsenceTypeCode == _prmCode
        //                    select _absType.MaxDay
        //                 ).FirstOrDefault();

        //        _result = _query;
        //    }
        //    catch (Exception Ex)
        //    {
        //    }

        //    return _result;
        //}
        #endregion

        #region AbsenceTypeLeaveAllowed
        public double RowsCountAbsenceTypeLeaveAllowed(Guid _prmAbsenceTypeCode)
        {
            double _result = 0;

            var _query =
                        (
                            from _absenceTypeLeaveAllowed in this.db.HRM_AbsenceTypeLeaveAlloweds
                            where _absenceTypeLeaveAllowed.AbsenceTypeCode == _prmAbsenceTypeCode
                            select _absenceTypeLeaveAllowed.AbsenceTypeLeaveAllowedCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public HRM_AbsenceTypeLeaveAllowed GetSingleAbsenceTypeLeaveAllowed(Guid _prmCode)
        {
            HRM_AbsenceTypeLeaveAllowed _result = null;

            try
            {
                _result = this.db.HRM_AbsenceTypeLeaveAlloweds.Single(_temp => _temp.AbsenceTypeLeaveAllowedCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_AbsenceTypeLeaveAllowed> GetListAbsenceTypeLeaveAllowed(int _prmReqPage, int _prmPageSize, Guid _prmAbsenceTypeCode)
        {
            List<HRM_AbsenceTypeLeaveAllowed> _result = new List<HRM_AbsenceTypeLeaveAllowed>();

            try
            {
                var _query = (
                                from _absenceTypeLeaveAllowed in this.db.HRM_AbsenceTypeLeaveAlloweds
                                where _absenceTypeLeaveAllowed.AbsenceTypeCode == _prmAbsenceTypeCode
                                orderby _absenceTypeLeaveAllowed.EditDate descending
                                select new
                                {
                                    AbsenceTypeLeaveAllowedCode = _absenceTypeLeaveAllowed.AbsenceTypeLeaveAllowedCode,
                                    AbsenceTypeCode = _absenceTypeLeaveAllowed.AbsenceTypeCode,
                                    MaxDaysAllowed = _absenceTypeLeaveAllowed.MaxDaysAllowed,
                                    BeginMonth = _absenceTypeLeaveAllowed.BeginMonth,
                                    EndingMonth = _absenceTypeLeaveAllowed.EndingMonth
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_AbsenceTypeLeaveAllowed(_row.AbsenceTypeLeaveAllowedCode, _row.AbsenceTypeCode, _row.MaxDaysAllowed, _row.BeginMonth, _row.EndingMonth));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddAbsenceTypeLeaveAllowed(HRM_AbsenceTypeLeaveAllowed _prmAbsenceTypeLeaveAllowed)
        {
            string _result = "";

            try
            {
                _result = this.IsExist(_prmAbsenceTypeLeaveAllowed.BeginMonth, _prmAbsenceTypeLeaveAllowed.EndingMonth, _prmAbsenceTypeLeaveAllowed.AbsenceTypeCode, _prmAbsenceTypeLeaveAllowed.AbsenceTypeLeaveAllowedCode);

                if (_result == "")
                {
                    this.db.HRM_AbsenceTypeLeaveAlloweds.InsertOnSubmit(_prmAbsenceTypeLeaveAllowed);
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                _result = "You Failed Add Data";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string EditAbsenceTypeLeaveAllowed(HRM_AbsenceTypeLeaveAllowed _prmAbsenceTypeLeaveAllowed)
        {
            string _result = "";

            try
            {
                _result = this.IsExist(_prmAbsenceTypeLeaveAllowed.BeginMonth, _prmAbsenceTypeLeaveAllowed.EndingMonth, _prmAbsenceTypeLeaveAllowed.AbsenceTypeCode, _prmAbsenceTypeLeaveAllowed.AbsenceTypeLeaveAllowedCode);

                if (_result == "")
                {
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                _result = "You Failed Edit Data";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiAbsenceTypeLeaveAllowed(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_AbsenceTypeLeaveAllowed _absenceTypeLeaveAllowed = this.db.HRM_AbsenceTypeLeaveAlloweds.Single(_temp => _temp.AbsenceTypeLeaveAllowedCode.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.HRM_AbsenceTypeLeaveAlloweds.DeleteOnSubmit(_absenceTypeLeaveAllowed);
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

        public string IsExist(byte _prmStartMonth, byte _prmEndMonth, Guid _prmAbsenceTypeCode, Guid _prmCode)
        {
            string _result = "";

            try
            {
                //range data baru
                List<int> _rangePrm = new List<int>();

                int _beginPrm = _prmStartMonth;

                if (_prmStartMonth <= _prmEndMonth)
                {
                    for (byte i = _prmStartMonth; i <= _prmEndMonth; i++)
                    {
                        _rangePrm.Add(_beginPrm);

                        _beginPrm = _beginPrm + 1;
                    }
                }
                else
                {
                    for (byte i = _prmStartMonth; i <= 12; i++)
                    {
                        _rangePrm.Add(_beginPrm);

                        _beginPrm = _beginPrm + 1;
                    }
                    for (byte j = 1; j <= _prmEndMonth; j++)
                    {
                        _rangePrm.Add(j);
                    }
                }

                var _query = (
                                from _hrmAbsenceTypeLeaveAllowed in this.db.HRM_AbsenceTypeLeaveAlloweds
                                where _hrmAbsenceTypeLeaveAllowed.AbsenceTypeLeaveAllowedCode != _prmCode
                                    && _hrmAbsenceTypeLeaveAllowed.AbsenceTypeCode == _prmAbsenceTypeCode
                                select new
                                          {
                                              BeginMonth = _hrmAbsenceTypeLeaveAllowed.BeginMonth,
                                              EndingMonth = _hrmAbsenceTypeLeaveAllowed.EndingMonth
                                          }
                              );

                foreach (var _item in _query)
                {
                    //range data lama
                    List<int> _range = new List<int>();

                    int _begin = _item.BeginMonth;

                    if (_item.BeginMonth <= _item.EndingMonth)
                    {
                        for (byte i = _item.BeginMonth; i <= _item.EndingMonth; i++)
                        {
                            _range.Add(_begin);

                            _begin = _begin + 1;
                        }
                    }
                    else
                    {
                        for (byte i = _item.BeginMonth; i <= 12; i++)
                        {
                            _range.Add(_begin);

                            _begin = _begin + 1;
                        }
                        for (byte j = 1; j <= _item.EndingMonth; j++)
                        {
                            _range.Add(j);
                        }
                    }

                    bool _break = false;
                    foreach (var _row in _range)
                    {
                        foreach (var _rowPrm in _rangePrm)
                        {
                            if (_rowPrm == _row)
                            {
                                _result = "Invalid Month Range";
                                _break = true;
                                break;
                            }
                            if (_rowPrm == _row)
                            {
                                _result = "Invalid Month Range";
                                _break = true;
                                break;
                            }
                        }
                        if (_break == true) break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~AbsenceTypeBL()
        {

        }
    }
}
