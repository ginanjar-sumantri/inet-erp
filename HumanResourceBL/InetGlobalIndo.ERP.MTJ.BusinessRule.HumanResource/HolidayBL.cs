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
    public sealed class HolidayBL : Base
    {
        public HolidayBL()
        {

        }

        #region Holiday
        public double RowsCountHoliday(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "Year")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "Month")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            if (_prmCategory == "Day")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern4 = "%%";
            }
            if (_prmCategory == "Desc")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern1 = "%%";
            }

            var _query =
                (
                    from _master_Holiday in this.db.HRM_Master_Holidays
                    where (SqlMethods.Like(_master_Holiday.HolidayDate.Year.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like(_master_Holiday.HolidayDate.Month.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && (SqlMethods.Like(_master_Holiday.HolidayDate.Day.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                        && (SqlMethods.Like(_master_Holiday.Description.Trim().ToLower(), _pattern4.Trim().ToLower()))
                    select _master_Holiday.HolidayDate
                ).Count();

            _result = _query;

            return _result;

        }

        public HRM_Master_Holiday GetSingleHoliday(DateTime _prmHolidayDate)
        {
            HRM_Master_Holiday _result = null;

            try
            {
                _result = this.db.HRM_Master_Holidays.Single(_temp => _temp.HolidayDate == _prmHolidayDate);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_Master_Holiday> GetListHoliday(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_Master_Holiday> _result = new List<HRM_Master_Holiday>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "Year")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "Month")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            if (_prmCategory == "Day")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern4 = "%%";
            }
            if (_prmCategory == "Desc")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _master_Holiday in this.db.HRM_Master_Holidays
                                where (SqlMethods.Like(_master_Holiday.HolidayDate.Year.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_master_Holiday.HolidayDate.Month.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_master_Holiday.HolidayDate.Day.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_master_Holiday.Description.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                orderby _master_Holiday.EditDate descending
                                select new
                                {
                                    HolidayDate = _master_Holiday.HolidayDate,
                                    Description = _master_Holiday.Description,
                                    isCutLeave = _master_Holiday.isCutLeave
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_Master_Holiday(_row.HolidayDate, _row.Description, _row.isCutLeave));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_Master_Holiday> GetListHolidayForDDL()
        {
            List<HRM_Master_Holiday> _result = new List<HRM_Master_Holiday>();

            try
            {
                var _query = (
                                from _master_Holiday in this.db.HRM_Master_Holidays
                                orderby _master_Holiday.Description ascending
                                select new
                                {
                                    HolidayDate = _master_Holiday.HolidayDate,
                                    Description = _master_Holiday.Description
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_Master_Holiday(_row.HolidayDate, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHoliday(HRM_Master_Holiday _prmMaster_Holiday)
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

        public bool AddHoliday(HRM_Master_Holiday _prmMaster_Holiday)
        {
            bool _result = false;

            try
            {
                this.db.HRM_Master_Holidays.InsertOnSubmit(_prmMaster_Holiday);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHoliday(string[] _prmHolidayDate)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmHolidayDate.Length; i++)
                {
                    HRM_Master_Holiday _master_Holiday = this.db.HRM_Master_Holidays.Single(_temp => _temp.HolidayDate == DateFormMapper.GetValue(_prmHolidayDate[i]));

                    this.db.HRM_Master_Holidays.DeleteOnSubmit(_master_Holiday);
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

        public bool DeleteScheduleShiftByHolidayDate(DateTime _prmHolidayDate)
        {
            bool _result = false;

            try
            {
                int _month = (
                                 from _companyConfig in this.db.CompanyConfigurations
                                 where _companyConfig.ConfigCode == CompanyConfigureDataMapper.GetCompanyConfigure(CompanyConfigure.LeaveCanTakeLeaveAfterWorkTime)
                                 select Convert.ToInt32(_companyConfig.SetValue)
                             ).FirstOrDefault();

                var _query = (
                                from _hrmTrScheduleShiftPosting in this.db.HRMTrScheduleShiftPostings
                                join _msEmpGroup in this.db.HRMMsEmpGroups
                                    on _hrmTrScheduleShiftPosting.EmpGroupCode equals _msEmpGroup.EmpGroupCode
                                join _msEmployee in this.db.MsEmployees
                                    on _hrmTrScheduleShiftPosting.EmpNumb equals _msEmployee.EmpNumb
                                where _hrmTrScheduleShiftPosting.ScheduleDate == _prmHolidayDate
                                    && _msEmpGroup.ScheduleType == ScheduleTypeDataMapper.GetScheduleType(ScheduleType.Office)
                                    && _msEmployee.StartDate.AddMonths(_month) > _prmHolidayDate
                                select _hrmTrScheduleShiftPosting
                            );

                this.db.HRMTrScheduleShiftPostings.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        ~HolidayBL()
        {

        }
    }
}
