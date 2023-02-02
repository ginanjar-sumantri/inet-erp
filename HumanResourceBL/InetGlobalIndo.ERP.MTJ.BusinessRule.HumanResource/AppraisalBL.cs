using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class AppraisalBL : Base
    {
        public AppraisalBL()
        {

        }

        #region Appraisal
        public double RowsCountAppraisal(string _prmCategory, string _prmKeyword)
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
                                from _msAppraisal in this.db.HRMMsAppraisals
                                where (SqlMethods.Like(_msAppraisal.AppraisalCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msAppraisal.AppraisalName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _msAppraisal.AppraisalCode
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public HRMMsAppraisal GetSingleAppraisal(String _prmAppraisalCode)
        {
            HRMMsAppraisal _result = null;

            try
            {
                _result = this.db.HRMMsAppraisals.Single(_temp => _temp.AppraisalCode == _prmAppraisalCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAppraisalNameByCode(String _prmAppraisalCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _hrmMsAppraisal in this.db.HRMMsAppraisals
                                where _hrmMsAppraisal.AppraisalCode == _prmAppraisalCode
                                select new
                                {
                                    AppraisalName = _hrmMsAppraisal.AppraisalName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.AppraisalName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAppraisal> GetListAppraisal(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsAppraisal> _result = new List<HRMMsAppraisal>();

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
                                from _hrmMsAppraisal in this.db.HRMMsAppraisals
                                where (SqlMethods.Like(_hrmMsAppraisal.AppraisalCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_hrmMsAppraisal.AppraisalName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _hrmMsAppraisal.AppraisalCode ascending
                                select new
                                {
                                    AppraisalCode = _hrmMsAppraisal.AppraisalCode,
                                    AppraisalName = _hrmMsAppraisal.AppraisalName,
                                    AppraisalGrp = _hrmMsAppraisal.AppraisalGroup,
                                    AppraisalGrpName = (
                                                    from _msAppraisalGroup in this.db.HRMMsAppraisalGroups
                                                    where _hrmMsAppraisal.AppraisalGroup == _msAppraisalGroup.AppraisalGrpCode
                                                    select _msAppraisalGroup.AppraisalGrpName
                                                ).FirstOrDefault(),
                                    Bobot = _hrmMsAppraisal.Bobot
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAppraisal(_row.AppraisalCode, _row.AppraisalName, _row.AppraisalGrp, _row.AppraisalGrpName, _row.Bobot));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAppraisal> GetListAppraisalForDDL()
        {
            List<HRMMsAppraisal> _result = new List<HRMMsAppraisal>();

            try
            {
                var _query = (
                                from _hrmMsAppraisal in this.db.HRMMsAppraisals
                                orderby _hrmMsAppraisal.AppraisalCode ascending
                                select new
                                {
                                    AppraisalCode = _hrmMsAppraisal.AppraisalCode,
                                    AppraisalName = _hrmMsAppraisal.AppraisalName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAppraisal(_row.AppraisalCode, _row.AppraisalName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditAppraisal(HRMMsAppraisal _prmHRMMsAppraisal)
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

        public bool AddAppraisal(HRMMsAppraisal _prmHRMMsAppraisal)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsAppraisalName(_prmHRMMsAppraisal.AppraisalName, _prmHRMMsAppraisal.AppraisalCode) == false)
                {
                    this.db.HRMMsAppraisals.InsertOnSubmit(_prmHRMMsAppraisal);
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

        private bool IsExistsAppraisalName(String _prmAppraisalName, String _prmAppraisalCode)
        {
            bool _result = false;

            try
            {
                var _query = from _hrmMsAppraisal in this.db.HRMMsAppraisals
                             where _hrmMsAppraisal.AppraisalName == _prmAppraisalName && _hrmMsAppraisal.AppraisalCode != _prmAppraisalCode
                             select new
                             {
                                 _hrmMsAppraisal.AppraisalCode
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

        public bool DeleteMultiAppraisal(string[] _prmAppraisalCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmAppraisalCode.Length; i++)
                {
                    HRMMsAppraisal _HRMMsAppraisal = this.db.HRMMsAppraisals.Single(_temp => _temp.AppraisalCode == _prmAppraisalCode[i]);

                    this.db.HRMMsAppraisals.DeleteOnSubmit(_HRMMsAppraisal);
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

        #region AppraisalGroup
        public double RowsCountAppraisalGroup(string _prmCategory, string _prmKeyword)
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
                                from _msAppraisalGroup in this.db.HRMMsAppraisalGroups
                                where (SqlMethods.Like(_msAppraisalGroup.AppraisalGrpCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msAppraisalGroup.AppraisalGrpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _msAppraisalGroup.AppraisalGrpCode
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public HRMMsAppraisalGroup GetSingleAppraisalGroup(String _prmAppraisalGrpCode)
        {
            HRMMsAppraisalGroup _result = null;

            try
            {
                _result = this.db.HRMMsAppraisalGroups.Single(_temp => _temp.AppraisalGrpCode == _prmAppraisalGrpCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAppraisalGroupNameByCode(String _prmAppraisalGrpCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _hrmMsAppraisalGroup in this.db.HRMMsAppraisalGroups
                                where _hrmMsAppraisalGroup.AppraisalGrpCode == _prmAppraisalGrpCode
                                select new
                                {
                                    AppraisalGrpName = _hrmMsAppraisalGroup.AppraisalGrpName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.AppraisalGrpName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAppraisalGroup> GetListAppraisalGroup(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsAppraisalGroup> _result = new List<HRMMsAppraisalGroup>();

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
                                from _hrmMsAppraisalGroup in this.db.HRMMsAppraisalGroups
                                where (SqlMethods.Like(_hrmMsAppraisalGroup.AppraisalGrpCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_hrmMsAppraisalGroup.AppraisalGrpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _hrmMsAppraisalGroup.AppraisalGrpCode ascending
                                select new
                                {
                                    AppraisalGrpCode = _hrmMsAppraisalGroup.AppraisalGrpCode,
                                    AppraisalGrpName = _hrmMsAppraisalGroup.AppraisalGrpName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAppraisalGroup(_row.AppraisalGrpCode, _row.AppraisalGrpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsAppraisalGroup> GetListAppraisalGroupForDDL()
        {
            List<HRMMsAppraisalGroup> _result = new List<HRMMsAppraisalGroup>();

            try
            {
                var _query = (
                                from _hrmMsAppraisalGroup in this.db.HRMMsAppraisalGroups
                                orderby _hrmMsAppraisalGroup.AppraisalGrpCode ascending
                                select new
                                {
                                    AppraisalGrpCode = _hrmMsAppraisalGroup.AppraisalGrpCode,
                                    AppraisalGrpName = _hrmMsAppraisalGroup.AppraisalGrpName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAppraisalGroup(_row.AppraisalGrpCode, _row.AppraisalGrpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditAppraisalGroup(HRMMsAppraisalGroup _prmHRMMsAppraisalGroup)
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

        public bool AddAppraisalGroup(HRMMsAppraisalGroup _prmHRMMsAppraisalGroup)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsAppraisalGroupName(_prmHRMMsAppraisalGroup.AppraisalGrpName, _prmHRMMsAppraisalGroup.AppraisalGrpCode) == false)
                {
                    this.db.HRMMsAppraisalGroups.InsertOnSubmit(_prmHRMMsAppraisalGroup);
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

        private bool IsExistsAppraisalGroupName(String _prmAppraisalGroupName, String _prmAppraisalGroupCode)
        {
            bool _result = false;

            try
            {
                var _query = from _hrmMsAppraisalGroup in this.db.HRMMsAppraisalGroups
                             where _hrmMsAppraisalGroup.AppraisalGrpName == _prmAppraisalGroupName && _hrmMsAppraisalGroup.AppraisalGrpName != _prmAppraisalGroupCode
                             select new
                             {
                                 _hrmMsAppraisalGroup.AppraisalGrpCode
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

        public bool DeleteMultiAppraisalGroup(string[] _prmAppraisalGroupCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmAppraisalGroupCode.Length; i++)
                {
                    HRMMsAppraisalGroup _HRMMsAppraisalGroup = this.db.HRMMsAppraisalGroups.Single(_temp => _temp.AppraisalGrpCode == _prmAppraisalGroupCode[i]);

                    this.db.HRMMsAppraisalGroups.DeleteOnSubmit(_HRMMsAppraisalGroup);
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

        #region AppraisalPurpose
        public double RowsCountAppraisalPurpose(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _msAppraisalPurpose in this.db.HRMMsAppraisalPurposes
                where _msAppraisalPurpose.AppraisalCode == _prmCode
                select _msAppraisalPurpose
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMMsAppraisalPurpose> GetListAppraisalPurpose(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMMsAppraisalPurpose> _result = new List<HRMMsAppraisalPurpose>();

            try
            {
                var _query = (
                                from _msAppraisalPurpose in this.db.HRMMsAppraisalPurposes
                                where _msAppraisalPurpose.AppraisalCode == _prmCode
                                select new
                                {
                                    PurposeCode = _msAppraisalPurpose.PurposeCode,
                                    PurposeName = (
                                                        from _msPurpose in this.db.HRMMsPurposes
                                                        where _msPurpose.PurposeCode == _msAppraisalPurpose.PurposeCode
                                                        select _msPurpose.PurposeName
                                                      ).FirstOrDefault(),
                                    AppraisalCode = _msAppraisalPurpose.AppraisalCode,
                                    AppraisalName = (
                                                        from _msAppraisal in this.db.HRMMsAppraisals
                                                        where _msAppraisal.AppraisalCode == _msAppraisalPurpose.AppraisalCode
                                                        select _msAppraisal.AppraisalName
                                                    ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAppraisalPurpose(_row.PurposeCode, _row.PurposeName, _row.AppraisalCode, _row.AppraisalName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMMsAppraisalPurpose GetSingleAppraisalPurpose(string _prmAppraisalCode, string _prmPurposeCode)
        {
            HRMMsAppraisalPurpose _result = null;

            try
            {
                _result = this.db.HRMMsAppraisalPurposes.Single(_temp => _temp.AppraisalCode == _prmAppraisalCode && _temp.PurposeCode == _prmPurposeCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiAppraisalPurpose(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _code = _prmCode[i].Split('=');

                    HRMMsAppraisalPurpose _msAppraisalPurpose = this.db.HRMMsAppraisalPurposes.Single(_temp => _temp.PurposeCode == _code[1] && _temp.AppraisalCode == _code[0]);
                    this.db.HRMMsAppraisalPurposes.DeleteOnSubmit(_msAppraisalPurpose);
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

        public bool AddAppraisalPurpose(HRMMsAppraisalPurpose _prmAppraisalPurpose)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsAppraisalPurposes.InsertOnSubmit(_prmAppraisalPurpose);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditAppraisalPurpose(HRMMsAppraisalPurpose _prmAppraisalPurpose)
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

        #region AppraisalJobLvl
        public double RowsCountAppraisalJobLvl(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _msAppraisalJobLvl in this.db.HRMMsAppraisalJobLvls
                where _msAppraisalJobLvl.AppraisalCode == _prmCode
                select _msAppraisalJobLvl
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMMsAppraisalJobLvl> GetListAppraisalJobLvl(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMMsAppraisalJobLvl> _result = new List<HRMMsAppraisalJobLvl>();

            try
            {
                var _query = (
                                from _msAppraisalJobLvl in this.db.HRMMsAppraisalJobLvls
                                where _msAppraisalJobLvl.AppraisalCode == _prmCode
                                select new
                                {
                                    JobLevel = _msAppraisalJobLvl.JobLevel,
                                    JobLevelName = (
                                                        from _msJobLvl in this.db.MsJobLevels
                                                        where _msJobLvl.JobLvlCode == _msAppraisalJobLvl.JobLevel
                                                        select _msJobLvl.JobLvlName
                                                      ).FirstOrDefault(),
                                    AppraisalCode = _msAppraisalJobLvl.AppraisalCode,
                                    AppraisalName = (
                                                        from _msAppraisal in this.db.HRMMsAppraisals
                                                        where _msAppraisal.AppraisalCode == _msAppraisalJobLvl.AppraisalCode
                                                        select _msAppraisal.AppraisalName
                                                    ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsAppraisalJobLvl(_row.JobLevel, _row.JobLevelName, _row.AppraisalCode, _row.AppraisalName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMMsAppraisalJobLvl GetSingleAppraisalJobLvl(string _prmAppraisalCode, string _prmJobLvlCode)
        {
            HRMMsAppraisalJobLvl _result = null;

            try
            {
                _result = this.db.HRMMsAppraisalJobLvls.Single(_temp => _temp.AppraisalCode == _prmAppraisalCode && _temp.JobLevel == _prmJobLvlCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiAppraisalJobLvl(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _code = _prmCode[i].Split('=');

                    HRMMsAppraisalJobLvl _msAppraisalJobLvl = this.db.HRMMsAppraisalJobLvls.Single(_temp => _temp.JobLevel == _code[1] && _temp.AppraisalCode == _code[0]);
                    this.db.HRMMsAppraisalJobLvls.DeleteOnSubmit(_msAppraisalJobLvl);
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

        public bool AddAppraisalJobLvl(HRMMsAppraisalJobLvl _prmAppraisalJobLvl)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsAppraisalJobLvls.InsertOnSubmit(_prmAppraisalJobLvl);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditAppraisalJobLvl(HRMMsAppraisalJobLvl _prmAppraisalJobLvl)
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

        ~AppraisalBL()
        {

        }
    }
}
