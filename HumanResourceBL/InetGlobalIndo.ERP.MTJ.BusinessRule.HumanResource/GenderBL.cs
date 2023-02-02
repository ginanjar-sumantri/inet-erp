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
    public sealed class GenderBL : Base
    {
        public GenderBL()
        {
        }

        #region Gender
        public double RowsCountGender(string _prmCategory, string _prmKeyword)
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

            var _query =
                (
                    from _master_Gender in this.db.HRM_Master_Genders
                    where (SqlMethods.Like(_master_Gender.GenderCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like(_master_Gender.GenderName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _master_Gender.GenderCode
                ).Count();

            _result = _query;

            return _result;

        }

        public HRM_Master_Gender GetSingleGender(String _prmGenderCode)
        {
            HRM_Master_Gender _result = null;

            try
            {
                _result = this.db.HRM_Master_Genders.Single(_temp => _temp.GenderCode == _prmGenderCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_Master_Gender> GetListGender(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_Master_Gender> _result = new List<HRM_Master_Gender>();

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
                                from _master_Gender in this.db.HRM_Master_Genders
                                where (SqlMethods.Like(_master_Gender.GenderCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_master_Gender.GenderName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _master_Gender.EditDate descending
                                select new
                                {
                                    GenderCode = _master_Gender.GenderCode,
                                    GenderName = _master_Gender.GenderName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_Master_Gender(_row.GenderCode, _row.GenderName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_Master_Gender> GetListGenderForDDL()
        {
            List<HRM_Master_Gender> _result = new List<HRM_Master_Gender>();

            try
            {
                var _query = (
                                from _master_Gender in this.db.HRM_Master_Genders
                                orderby _master_Gender.GenderName ascending
                                select new
                                {
                                    GenderCode = _master_Gender.GenderCode,
                                    GenderName = _master_Gender.GenderName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_Master_Gender(_row.GenderCode, _row.GenderName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGender(HRM_Master_Gender _prmMaster_Gender)
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

        public bool AddGender(HRM_Master_Gender _prmMaster_Gender)
        {
            bool _result = false;

            try
            {
                this.db.HRM_Master_Genders.InsertOnSubmit(_prmMaster_Gender);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiGender(string[] _prmGenderCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmGenderCode.Length; i++)
                {
                    HRM_Master_Gender _master_Gender = this.db.HRM_Master_Genders.Single(_temp => _temp.GenderCode == _prmGenderCode[i]);

                    this.db.HRM_Master_Genders.DeleteOnSubmit(_master_Gender);
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

        public String GetGenderNameByGenderCode(String _prmGenderCode)
        {
            String _result = "";

            try
            {
                _result = this.db.HRM_Master_Genders.Single(_temp => _temp.GenderCode == _prmGenderCode).GenderName;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region HRM.Gender_AbsenceType
        public double RowsCountGender_AbsenceType(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _mapperGender_AbsType in this.db.HRM_Master_Gender_Master_AbsenceTypes
                where _mapperGender_AbsType.GenderCode == _prmCode
                select _mapperGender_AbsType.AbsenceTypeCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_Master_Gender_Master_AbsenceType> GetListGender_AbsenceType(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRM_Master_Gender_Master_AbsenceType> _result = new List<HRM_Master_Gender_Master_AbsenceType>();

            try
            {
                var _query = (
                                from _mapperGender_AbsType in this.db.HRM_Master_Gender_Master_AbsenceTypes
                                where _mapperGender_AbsType.GenderCode == _prmCode
                                select new
                                {
                                    GenderCode = _mapperGender_AbsType.GenderCode,
                                    GenderName = (
                                                from _gender in this.db.HRM_Master_Genders
                                                where _gender.GenderCode == _mapperGender_AbsType.GenderCode
                                                select _gender.GenderName
                                              ).FirstOrDefault(),
                                    AbsenceTypeCode = _mapperGender_AbsType.AbsenceTypeCode,
                                    AbsenceTypeName = (
                                                from _absType in this.db.HRMMsAbsenceTypes
                                                where _absType.AbsenceTypeCode == _mapperGender_AbsType.AbsenceTypeCode
                                                select _absType.AbsenceTypeName
                                              ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_Master_Gender_Master_AbsenceType(_row.GenderCode, _row.GenderName, _row.AbsenceTypeCode, _row.AbsenceTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_Master_Gender_Master_AbsenceType GetSingleGender_AbsenceType(string _prmGenderCode, string _prmAbsTypeCode, string _prmEmp)
        {
            HRM_Master_Gender_Master_AbsenceType _result = null;

            try
            {
                _result = this.db.HRM_Master_Gender_Master_AbsenceTypes.Single(_temp => _temp.GenderCode == _prmGenderCode && _temp.AbsenceTypeCode == _prmAbsTypeCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiGender_AbsenceType(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _code = _prmCode[i].Split('=');

                    HRM_Master_Gender_Master_AbsenceType _mapperGender_AbsType = this.db.HRM_Master_Gender_Master_AbsenceTypes.Single(_temp => _temp.GenderCode == _code[0] && _temp.AbsenceTypeCode == _code[1]);
                    this.db.HRM_Master_Gender_Master_AbsenceTypes.DeleteOnSubmit(_mapperGender_AbsType);
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

        public bool AddGender_AbsenceType(HRM_Master_Gender_Master_AbsenceType _prmGender_AbsenceType)
        {
            bool _result = false;

            try
            {
                this.db.HRM_Master_Gender_Master_AbsenceTypes.InsertOnSubmit(_prmGender_AbsenceType);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGender_AbsenceType(HRM_Master_Gender_Master_AbsenceType _prmGender_AbsenceType)
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

        ~GenderBL()
        {
        }
    }
}
