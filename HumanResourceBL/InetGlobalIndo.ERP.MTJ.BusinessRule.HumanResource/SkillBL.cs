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
    public sealed class SkillBL : Base
    {
        public SkillBL()
        {

        }

        #region Skill Type
        public double RowsCountSkillType(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";


            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                (
                    from _master_SkillType in this.db.Master_SkillTypes
                    where (SqlMethods.Like(_master_SkillType.SkillTypeName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    select _master_SkillType.SkillTypeCode
                ).Count();

            _result = _query;
            return _result;

        }

        public Master_SkillType GetSingleSkillType(Guid _prmSkillTypeCode)
        {
            Master_SkillType _result = null;

            try
            {
                _result = this.db.Master_SkillTypes.Single(_temp => _temp.SkillTypeCode == _prmSkillTypeCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetSkillTypeNameByCode(Guid _prmSkillTypeCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _master_SkillType in this.db.Master_SkillTypes
                                where _master_SkillType.SkillTypeCode == _prmSkillTypeCode
                                select new
                                {
                                    SkillTypeName = _master_SkillType.SkillTypeName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.SkillTypeName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_SkillType> GetListSkillType(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_SkillType> _result = new List<Master_SkillType>();

            string _pattern1 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _master_SkillType in this.db.Master_SkillTypes
                                where (SqlMethods.Like(_master_SkillType.SkillTypeName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _master_SkillType.SkillTypeName ascending
                                select new
                                {
                                    SkillTypeCode = _master_SkillType.SkillTypeCode,
                                    SkillTypeName = _master_SkillType.SkillTypeName,
                                    SkillTypeDescription = _master_SkillType.SkillTypeDescription
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_SkillType(_row.SkillTypeCode, _row.SkillTypeName, _row.SkillTypeDescription));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_SkillType> GetListSkillTypeForDDL()
        {
            List<Master_SkillType> _result = new List<Master_SkillType>();

            try
            {
                var _query = (
                                from _master_SkillType in this.db.Master_SkillTypes
                                orderby _master_SkillType.SkillTypeName ascending
                                select new
                                {
                                    SkillTypeCode = _master_SkillType.SkillTypeCode,
                                    SkillTypeName = _master_SkillType.SkillTypeName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_SkillType(_row.SkillTypeCode, _row.SkillTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSkillType(Master_SkillType _prmMaster_SkillType)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsSkillTypeName(_prmMaster_SkillType.SkillTypeName, _prmMaster_SkillType.SkillTypeCode) == false)
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

        public bool AddSkillType(Master_SkillType _prmMaster_SkillType)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsSkillTypeName(_prmMaster_SkillType.SkillTypeName, _prmMaster_SkillType.SkillTypeCode) == false)
                {
                    this.db.Master_SkillTypes.InsertOnSubmit(_prmMaster_SkillType);
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

        private bool IsExistsSkillTypeName(String _prmSkillTypeName, Guid _prmSkillTypeCode)
        {
            bool _result = false;

            try
            {
                var _query = from _master_SkillType in this.db.Master_SkillTypes
                             where _master_SkillType.SkillTypeName == _prmSkillTypeName && _master_SkillType.SkillTypeCode != _prmSkillTypeCode
                             select new
                             {
                                 _master_SkillType.SkillTypeCode
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

        public bool DeleteMultiSkillType(string[] _prmSkillTypeCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmSkillTypeCode.Length; i++)
                {
                    Master_SkillType _master_SkillType = this.db.Master_SkillTypes.Single(_temp => _temp.SkillTypeCode == new Guid(_prmSkillTypeCode[i]));

                    this.db.Master_SkillTypes.DeleteOnSubmit(_master_SkillType);
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

        #region Skill
        public double RowsCountSkill(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";


            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                (
                    from _master_Skill in this.db.Master_Skills
                    where (SqlMethods.Like(_master_Skill.SkillName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    select _master_Skill.SkillCode
                ).Count();

            _result = _query;
            return _result;

        }

        public double RowsCountSkillReport(string _prmSkillType)
        {
            double _result = 0;

            string _pattern1 = "%%";


            if (_prmSkillType != "")
            {
                _pattern1 = "%" + _prmSkillType + "%";
            }

            var _query =
                (
                    from _master_Skill in this.db.Master_Skills
                    where (SqlMethods.Like(_master_Skill.SkillTypeCode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                    select _master_Skill.SkillCode
                ).Count();

            _result = _query;
            return _result;

        }

        public Master_Skill GetSingleSkill(Guid _prmSkillCode)
        {
            Master_Skill _result = null;

            try
            {
                _result = this.db.Master_Skills.Single(_temp => _temp.SkillCode == _prmSkillCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetSkillNameByCode(Guid _prmSkillCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _master_Skill in this.db.Master_Skills
                                where _master_Skill.SkillCode == _prmSkillCode
                                select new
                                {
                                    SkillName = _master_Skill.SkillName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.SkillName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Skill> GetListSkill(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_Skill> _result = new List<Master_Skill>();

            string _pattern1 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _master_Skill in this.db.Master_Skills
                                join _master_SkillType in this.db.Master_SkillTypes
                                    on _master_Skill.SkillTypeCode equals _master_SkillType.SkillTypeCode
                                where (SqlMethods.Like(_master_Skill.SkillName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _master_Skill.SkillName ascending
                                select new
                                {
                                    SkillCode = _master_Skill.SkillCode,
                                    SkillTypeCode = _master_Skill.SkillTypeCode,
                                    SkillTypeName = _master_SkillType.SkillTypeName,
                                    SkillName = _master_Skill.SkillName,
                                    SkillDescription = _master_Skill.SkillDescription
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Skill(_row.SkillCode, _row.SkillTypeCode, _row.SkillTypeName, _row.SkillName, _row.SkillDescription));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Skill> GetListSkillForDDL()
        {
            List<Master_Skill> _result = new List<Master_Skill>();

            try
            {
                var _query = (
                                from _master_Skill in this.db.Master_Skills
                                orderby _master_Skill.SkillName ascending
                                select new
                                {
                                    SkillCode = _master_Skill.SkillCode,
                                    SkillName = _master_Skill.SkillName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Skill(_row.SkillCode, _row.SkillName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Skill> GetListSkillForDDL(string _prmSkillType)
        {
            List<Master_Skill> _result = new List<Master_Skill>();

            string _pattern1 = "%%";

            if (_prmSkillType != "")
            {
                _pattern1 = "%" + _prmSkillType + "%";
            }

            try
            {
                var _query = (
                                from _master_Skill in this.db.Master_Skills
                                where (SqlMethods.Like(_master_Skill.SkillTypeCode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _master_Skill.SkillName ascending
                                select new
                                {
                                    SkillCode = _master_Skill.SkillCode,
                                    SkillName = _master_Skill.SkillName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Skill(_row.SkillCode, _row.SkillName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Skill> GetListSkillForDDL(string _prmSkillType, int _prmReqPage, int _prmPageSize)
        {
            List<Master_Skill> _result = new List<Master_Skill>();

            string _pattern1 = "%%";

            if (_prmSkillType != "")
            {
                _pattern1 = "%" + _prmSkillType + "%";
            }

            try
            {
                var _query = (
                                from _master_Skill in this.db.Master_Skills
                                where (SqlMethods.Like(_master_Skill.SkillTypeCode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _master_Skill.SkillName ascending
                                select new
                                {
                                    SkillCode = _master_Skill.SkillCode,
                                    SkillName = _master_Skill.SkillName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Skill(_row.SkillCode, _row.SkillName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSkill(Master_Skill _prmMaster_Skill)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsSkillName(_prmMaster_Skill.SkillName, _prmMaster_Skill.SkillCode) == false)
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

        public bool AddSkill(Master_Skill _prmMaster_Skill)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsSkillName(_prmMaster_Skill.SkillName, _prmMaster_Skill.SkillCode) == false)
                {
                    this.db.Master_Skills.InsertOnSubmit(_prmMaster_Skill);
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

        private bool IsExistsSkillName(String _prmSkillName, Guid _prmSkillCode)
        {
            bool _result = false;

            try
            {
                var _query = from _master_Skill in this.db.Master_Skills
                             where _master_Skill.SkillName == _prmSkillName && _master_Skill.SkillCode != _prmSkillCode
                             select new
                             {
                                 _master_Skill.SkillCode
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

        public bool DeleteMultiSkill(string[] _prmSkillCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmSkillCode.Length; i++)
                {
                    Master_Skill _master_Skill = this.db.Master_Skills.Single(_temp => _temp.SkillCode == new Guid(_prmSkillCode[i]));

                    this.db.Master_Skills.DeleteOnSubmit(_master_Skill);
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

        ~SkillBL()
        {

        }
    }
}
