using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class WorkingHourBL : Base
    {
        public WorkingHourBL()
        {
        }

        #region WorkingHour
        public double RowsCountWorkingHour(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "WorkingHourName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                            from _hrm_Master_WorkingHour in this.db.HRM_Master_WorkingHours
                            where (SqlMethods.Like(_hrm_Master_WorkingHour.WorkingHourName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            select _hrm_Master_WorkingHour.WorkingHourCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public HRM_Master_WorkingHour GetSingle(Guid _prmWorkingHourCode)
        {
            HRM_Master_WorkingHour _result = null;

            try
            {
                _result = this.db.HRM_Master_WorkingHours.Single(_temp => _temp.WorkingHourCode == _prmWorkingHourCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetWorkingHourNameByCode(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _hrm_Master_WorkingHour in this.db.HRM_Master_WorkingHours
                                where _hrm_Master_WorkingHour.WorkingHourCode == _prmCode
                                select new
                                {
                                    WorkingHourName = _hrm_Master_WorkingHour.WorkingHourName
                                }
                            );

                foreach (var _obj in _query)
                {
                    _result = _obj.WorkingHourName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_Master_WorkingHour> GetListWorkingHour(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_Master_WorkingHour> _result = new List<HRM_Master_WorkingHour>();

            string _pattern1 = "%%";

            if (_prmCategory == "WorkingHourName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _workingHour in this.db.HRM_Master_WorkingHours
                                orderby _workingHour.EditDate descending
                                where (SqlMethods.Like(_workingHour.WorkingHourName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                select new
                                {
                                    WorkingHourCode = _workingHour.WorkingHourCode,
                                    WorkingHourName = _workingHour.WorkingHourName,
                                    IsActive = _workingHour.IsActive,
                                    Description = _workingHour.Description
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_Master_WorkingHour(_row.WorkingHourCode, _row.WorkingHourName, _row.IsActive, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_Master_WorkingHour> GetListWorkingHourForDDL()
        {
            List<HRM_Master_WorkingHour> _result = new List<HRM_Master_WorkingHour>();

            try
            {
                var _query = (
                                from _workingHour in this.db.HRM_Master_WorkingHours
                                where _workingHour.IsActive == true
                                orderby _workingHour.EditDate descending
                                select new
                                {
                                    WorkingHourCode = _workingHour.WorkingHourCode,
                                    WorkingHourName = _workingHour.WorkingHourName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_Master_WorkingHour(_row.WorkingHourCode, _row.WorkingHourName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(HRM_Master_WorkingHour _prmHRM_Master_WorkingHour)
        {
            bool _result = false;

            try
            {
                if (this.IsWorkingHourNameExists(_prmHRM_Master_WorkingHour.WorkingHourName, _prmHRM_Master_WorkingHour.WorkingHourCode) == false)
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

        private bool IsWorkingHourNameExists(string _prmWorkingHourName, Guid _prmWorkingHourCode)
        {
            bool _result = false;

            try
            {
                var _query = from _workingHour in this.db.HRM_Master_WorkingHours
                             where (_workingHour.WorkingHourName == _prmWorkingHourName) && (_workingHour.WorkingHourCode != _prmWorkingHourCode)
                             select new
                             {
                                 _workingHour.WorkingHourName
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

        public String Add(HRM_Master_WorkingHour _prmHRM_Master_WorkingHour)
        {
            String _result = "";

            try
            {
                if (this.IsWorkingHourNameExists(_prmHRM_Master_WorkingHour.WorkingHourName, _prmHRM_Master_WorkingHour.WorkingHourCode) == false)
                {
                    this.db.HRM_Master_WorkingHours.InsertOnSubmit(_prmHRM_Master_WorkingHour);

                    this.db.SubmitChanges();

                    _result = _prmHRM_Master_WorkingHour.WorkingHourCode.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmWorkingHourCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmWorkingHourCode.Length; i++)
                {
                    HRM_Master_WorkingHour _hrm_Master_WorkingHour = this.db.HRM_Master_WorkingHours.Single(_workingHour => _workingHour.WorkingHourCode == new Guid(_prmWorkingHourCode[i]));

                    var _query = (
                                    from _workingHourList in this.db.HRM_WorkingHourLists
                                    where _workingHourList.WorkingHourCode == new Guid(_prmWorkingHourCode[i])
                                    select _workingHourList
                                 );

                    this.db.HRM_WorkingHourLists.DeleteAllOnSubmit(_query);

                    this.db.HRM_Master_WorkingHours.DeleteOnSubmit(_hrm_Master_WorkingHour);
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

        #region WorkingHourList
        public double RowsCountWorkingHourList(Guid _prmWorkingHourCode)
        {
            double _result = 0;

            var _query =
                (
                    from _msWorkingHourList in this.db.HRM_WorkingHourLists
                    where _msWorkingHourList.WorkingHourCode == _prmWorkingHourCode
                    select _msWorkingHourList.WorkingHourListCode
                ).Count();

            _result = _query;

            return _result;

        }

        public HRM_WorkingHourList GetSingleWorkingHourList(Guid _prmWorkingHourListCode)
        {
            HRM_WorkingHourList _result = null;

            try
            {
                _result = this.db.HRM_WorkingHourLists.Single(_emp => _emp.WorkingHourListCode == _prmWorkingHourListCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_WorkingHourList> GetListWorkingHourList(int _prmReqPage, int _prmPageSize, Guid _prmWorkingHourCode)
        {
            List<HRM_WorkingHourList> _result = new List<HRM_WorkingHourList>();

            try
            {
                var _query = (
                                from _workingHourList in this.db.HRM_WorkingHourLists
                                where _workingHourList.WorkingHourCode == _prmWorkingHourCode
                                orderby _workingHourList.EditDate descending
                                select new
                                {
                                    WorkingHourListCode = _workingHourList.WorkingHourListCode,
                                    WorkingHourCode = _workingHourList.WorkingHourCode,
                                    WorkingHourName = (
                                                            from _workingHour in this.db.HRM_Master_WorkingHours
                                                            where _workingHour.WorkingHourCode == _workingHourList.WorkingHourCode
                                                            select _workingHour.WorkingHourName
                                                        ).FirstOrDefault(),
                                    WorkDay = _workingHourList.WorkDay,
                                    BeginHour = _workingHourList.BeginHour,
                                    BeginMinute = _workingHourList.BeginMinute,
                                    FinishHour = _workingHourList.FinishHour,
                                    FinishMinute = _workingHourList.FinishMinute,
                                    IsNextDay = _workingHourList.IsNextDay,
                                    Remark = _workingHourList.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_WorkingHourList(_row.WorkingHourListCode, _row.WorkingHourCode, _row.WorkingHourName, _row.WorkDay, _row.BeginHour, _row.BeginMinute, _row.FinishHour, _row.FinishMinute, _row.IsNextDay, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditWorkingHourList(HRM_WorkingHourList _prmHRM_WorkingHourList)
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

        public bool AddWorkingHourList(HRM_WorkingHourList _prmHRM_WorkingHourList)
        {
            bool _result = false;

            try
            {
                this.db.HRM_WorkingHourLists.InsertOnSubmit(_prmHRM_WorkingHourList);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiWorkingHourList(string[] _prmWorkingHourListCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmWorkingHourListCode.Length; i++)
                {
                    HRM_WorkingHourList _msWorkingHour = this.db.HRM_WorkingHourLists.Single(_emp => _emp.WorkingHourListCode == new Guid(_prmWorkingHourListCode[i]));

                    this.db.HRM_WorkingHourLists.DeleteOnSubmit(_msWorkingHour);
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

        ~WorkingHourBL()
        {
        }
    }
}
