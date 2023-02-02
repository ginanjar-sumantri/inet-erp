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
    public sealed class AbsenceBL : Base
    {
        public AbsenceBL()
        {

        }

        #region Absence Group
        public double RowsCountAbsenceGroup(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                (
                    from _master_AbsenceGroup in this.db.HRMMsAbsGroups
                    where (SqlMethods.Like(_master_AbsenceGroup.AbsenceGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like(_master_AbsenceGroup.AbsenceGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _master_AbsenceGroup.AbsenceGroup
                ).Count();

            _result = _query;
            return _result;

        }

        public HRMMsAbsGroup GetSingleAbsenceGroup(String _prmAbsenceGroupCode)
        {
            HRMMsAbsGroup _result = null;

            try
            {
                _result = this.db.HRMMsAbsGroups.Single(_temp => _temp.AbsenceGroup == _prmAbsenceGroupCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAbsenceGroupNameByCode(String _prmAbsenceGroupCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _master_AbsenceGroup in this.db.HRMMsAbsGroups
                                where _master_AbsenceGroup.AbsenceGroup == _prmAbsenceGroupCode
                                select new
                                {
                                    AbsenceGroupName = _master_AbsenceGroup.AbsenceGroupName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.AbsenceGroupName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAbsGroup> GetListAbsenceGroup(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsAbsGroup> _result = new List<HRMMsAbsGroup>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _master_AbsenceGroup in this.db.HRMMsAbsGroups
                                where (SqlMethods.Like(_master_AbsenceGroup.AbsenceGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_master_AbsenceGroup.AbsenceGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _master_AbsenceGroup.AbsenceGroup ascending
                                select new
                                {
                                    AbsenceGroup = _master_AbsenceGroup.AbsenceGroup,
                                    AbsenceGroupName = _master_AbsenceGroup.AbsenceGroupName,
                                    AbsenceGroupRemark = _master_AbsenceGroup.AbsenceGroupRemark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsGroup(_row.AbsenceGroup, _row.AbsenceGroupName, _row.AbsenceGroupRemark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAbsGroup> GetListAbsenceGroupForDDL()
        {
            List<HRMMsAbsGroup> _result = new List<HRMMsAbsGroup>();

            try
            {
                var _query = (
                                from _master_AbsenceGroup in this.db.HRMMsAbsGroups
                                orderby _master_AbsenceGroup.AbsenceGroupName ascending
                                select new
                                {
                                    AbsenceGroup = _master_AbsenceGroup.AbsenceGroup,
                                    AbsenceGroupName = _master_AbsenceGroup.AbsenceGroupName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsGroup(_row.AbsenceGroup, _row.AbsenceGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditAbsenceGroup(HRMMsAbsGroup _prmHRMMsAbsGroup)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsAbsenceGroupName(_prmHRMMsAbsGroup.AbsenceGroupName, _prmHRMMsAbsGroup.AbsenceGroup) == false)
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

        private bool IsExistsAbsenceGroupName(String _prmAbsenceGroupName, String _prmAbsenceGroupCode)
        {
            bool _result = false;

            try
            {
                var _query = from _master_AbsenceGroup in this.db.HRMMsAbsGroups
                             where _master_AbsenceGroup.AbsenceGroupName == _prmAbsenceGroupName && _master_AbsenceGroup.AbsenceGroup != _prmAbsenceGroupCode
                             select new
                             {
                                 _master_AbsenceGroup.AbsenceGroup
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

        public bool AddAbsenceGroup(HRMMsAbsGroup _prmHRMMsAbsGroup)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsAbsenceGroupName(_prmHRMMsAbsGroup.AbsenceGroupName, _prmHRMMsAbsGroup.AbsenceGroup) == false)
                {
                    this.db.HRMMsAbsGroups.InsertOnSubmit(_prmHRMMsAbsGroup);
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

        public bool DeleteMultiAbsenceGroup(string[] _prmAbsenceGroupCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmAbsenceGroupCode.Length; i++)
                {
                    HRMMsAbsGroup _master_AbsenceGroup = this.db.HRMMsAbsGroups.Single(_temp => _temp.AbsenceGroup == _prmAbsenceGroupCode[i]);

                    this.db.HRMMsAbsGroups.DeleteOnSubmit(_master_AbsenceGroup);
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

        #region Absence Group Detail
        public double RowsCountAbsGroupDt(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _msAbsGroupDt in this.db.HRMMsAbsGroupDts
                where _msAbsGroupDt.AbsenceGroup == _prmCode
                select _msAbsGroupDt
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMMsAbsGroupDt> GetListAbsGroupDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMMsAbsGroupDt> _result = new List<HRMMsAbsGroupDt>();

            try
            {
                var _query = (
                                from _msAbsGroupDt in this.db.HRMMsAbsGroupDts
                                where _msAbsGroupDt.AbsenceGroup == _prmCode
                                select new
                                {
                                    AbsenceGroup = _msAbsGroupDt.AbsenceGroup,
                                    AbsenceTypeCode = _msAbsGroupDt.AbsenceTypeCode,
                                    AbsenceTypeName = (
                                                        from _absenceType in this.db.HRMMsAbsenceTypes
                                                        where _absenceType.AbsenceTypeCode == _msAbsGroupDt.AbsenceTypeCode
                                                        select _absenceType.AbsenceTypeName
                                                      ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAbsGroupDt(_row.AbsenceGroup, _row.AbsenceTypeCode, _row.AbsenceTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMMsAbsGroupDt GetSingleAbsGroupDt(string _prmAbsGroupCode, string _prmAbsenceTypeCode)
        {
            HRMMsAbsGroupDt _result = null;

            try
            {
                _result = this.db.HRMMsAbsGroupDts.Single(_temp => _temp.AbsenceGroup == _prmAbsGroupCode && _temp.AbsenceTypeCode == _prmAbsenceTypeCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiAbsGroupDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _code = _prmCode[i].Split('=');

                    HRMMsAbsGroupDt _msAbsGroupDt = this.db.HRMMsAbsGroupDts.Single(_temp => _temp.AbsenceGroup == _code[0] && _temp.AbsenceTypeCode == _code[1]);
                    this.db.HRMMsAbsGroupDts.DeleteOnSubmit(_msAbsGroupDt);
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

        public bool AddAbsGroupDt(String _prmAbsGroupCode, String _prmDetail)
        {
            bool _result = false;

            try
            {
                String[] _tempSplit = _prmDetail.Split(',');

                for (int i = 0; i < _tempSplit.Length; i++)
                {
                    HRMMsAbsGroupDt _absGroupDt = new HRMMsAbsGroupDt();

                    _absGroupDt.AbsenceGroup = _prmAbsGroupCode;
                    _absGroupDt.AbsenceTypeCode = _tempSplit[i];

                    this.db.HRMMsAbsGroupDts.InsertOnSubmit(_absGroupDt);
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

        public bool EditAbsGroupDt(HRMMsAbsGroupDt _prmAbsGroupDt)
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

        #region Absence
        public double RowsCountAbsence(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";


            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                (
                    from _master_Absence in this.db.Master_Absences
                    where (SqlMethods.Like(_master_Absence.AbsenceName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    select _master_Absence.AbsenceCode
                ).Count();

            _result = _query;
            return _result;

        }

        public Master_Absence GetSingleAbsence(Guid _prmAbsenceCode)
        {
            Master_Absence _result = null;

            try
            {
                _result = this.db.Master_Absences.Single(_temp => _temp.AbsenceCode == _prmAbsenceCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAbsenceNameByCode(Guid _prmAbsenceCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _master_Absence in this.db.Master_Absences
                                where _master_Absence.AbsenceCode == _prmAbsenceCode
                                select new
                                {
                                    AbsenceName = _master_Absence.AbsenceName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.AbsenceName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Absence> GetListAbsence(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_Absence> _result = new List<Master_Absence>();

            string _pattern1 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _master_Absence in this.db.Master_Absences
                                //join _master_AbsenceGroup in this.db.HRMMsAbsGroups
                                //    on _master_Absence.AbsenceGroupCode equals _master_AbsenceGroup.AbsenceGroup
                                where (SqlMethods.Like(_master_Absence.AbsenceName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _master_Absence.AbsenceName ascending
                                select new
                                {
                                    AbsenceCode = _master_Absence.AbsenceCode,
                                    AbsenceGroupCode = _master_Absence.AbsenceGroupCode,
                                    AbsenceGroupName = _master_Absence.AbsenceGroupName,//_master_AbsenceGroup.AbsenceGroupName,
                                    AbsenceName = _master_Absence.AbsenceName,
                                    AbsenceDescription = _master_Absence.AbsenceDescription
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Absence(_row.AbsenceCode, _row.AbsenceGroupCode, _row.AbsenceGroupName, _row.AbsenceName, _row.AbsenceDescription));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Absence> GetListAbsenceForDDL()
        {
            List<Master_Absence> _result = new List<Master_Absence>();

            try
            {
                var _query = (
                                from _master_Absence in this.db.Master_Absences
                                orderby _master_Absence.AbsenceName ascending
                                select new
                                {
                                    AbsenceCode = _master_Absence.AbsenceCode,
                                    AbsenceName = _master_Absence.AbsenceName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Absence(_row.AbsenceCode, _row.AbsenceName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditAbsence(Master_Absence _prmMaster_Absence)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsAbsenceName(_prmMaster_Absence.AbsenceName, _prmMaster_Absence.AbsenceCode) == false)
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

        public bool AddAbsence(Master_Absence _prmMaster_Absence)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsAbsenceName(_prmMaster_Absence.AbsenceName, _prmMaster_Absence.AbsenceCode) == false)
                {
                    this.db.Master_Absences.InsertOnSubmit(_prmMaster_Absence);
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

        private bool IsExistsAbsenceName(String _prmAbsenceName, Guid _prmAbsenceCode)
        {
            bool _result = false;

            try
            {
                var _query = from _master_Absence in this.db.Master_Absences
                             where _master_Absence.AbsenceName == _prmAbsenceName && _master_Absence.AbsenceCode != _prmAbsenceCode
                             select new
                             {
                                 _master_Absence.AbsenceCode
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

        public bool DeleteMultiAbsence(string[] _prmAbsenceCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmAbsenceCode.Length; i++)
                {
                    Master_Absence _master_Absence = this.db.Master_Absences.Single(_temp => _temp.AbsenceCode == new Guid(_prmAbsenceCode[i]));

                    this.db.Master_Absences.DeleteOnSubmit(_master_Absence);
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

        ~AbsenceBL()
        {

        }
    }
}
