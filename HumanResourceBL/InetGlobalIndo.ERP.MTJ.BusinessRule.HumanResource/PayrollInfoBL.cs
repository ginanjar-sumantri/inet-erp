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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class PayrollInfoBL : Base
    {
        public PayrollInfoBL()
        {

        }

        #region PayrollInfo
        public List<spHRM_ValidateSalaryAbsResult> GetListSalaryAbs(string _prmTransNmbr)
        {
            List<spHRM_ValidateSalaryAbsResult> _result = new List<spHRM_ValidateSalaryAbsResult>();

            try
            {
                foreach (spHRM_ValidateSalaryAbsResult _item in this.db.spHRM_ValidateSalaryAbs(_prmTransNmbr))
                {
                    spHRM_ValidateSalaryAbsResult _spHRM_ValidateSalaryAbsResult = new spHRM_ValidateSalaryAbsResult();

                    _spHRM_ValidateSalaryAbsResult.EmpNumb = _item.EmpNumb;
                    _spHRM_ValidateSalaryAbsResult.EmpName = _item.EmpName;
                    _spHRM_ValidateSalaryAbsResult.TransDate = _item.TransDate;
                    _spHRM_ValidateSalaryAbsResult.Shift = _item.Shift;
                    _spHRM_ValidateSalaryAbsResult.FgAbsence = _item.FgAbsence;
                    _spHRM_ValidateSalaryAbsResult.FgSchedule = _item.FgSchedule;

                    _result.Add(_spHRM_ValidateSalaryAbsResult);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<spHRM_ValidateSalaryEmpResult> GetListSalaryEmp(string _prmTransNmbr)
        {
            List<spHRM_ValidateSalaryEmpResult> _result = new List<spHRM_ValidateSalaryEmpResult>();

            try
            {
                foreach (spHRM_ValidateSalaryEmpResult _item in this.db.spHRM_ValidateSalaryEmp(_prmTransNmbr))
                {
                    spHRM_ValidateSalaryEmpResult _spHRM_ValidateSalaryEmpResult = new spHRM_ValidateSalaryEmpResult();

                    _spHRM_ValidateSalaryEmpResult.EmpNumb = _item.EmpNumb;
                    _spHRM_ValidateSalaryEmpResult.EmpName = _item.EmpName;
                    _spHRM_ValidateSalaryEmpResult.StartDate = _item.StartDate;
                    _spHRM_ValidateSalaryEmpResult.JobTitle = _item.JobTitle;
                    _spHRM_ValidateSalaryEmpResult.JobLevel = _item.JobLevel;
                    _spHRM_ValidateSalaryEmpResult.EmpGroup = _item.EmpGroup;

                    _result.Add(_spHRM_ValidateSalaryEmpResult);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<spHRM_ValidateSalarySlipResult> GetListSalarySlip(string _prmTransNmbr)
        {
            List<spHRM_ValidateSalarySlipResult> _result = new List<spHRM_ValidateSalarySlipResult>();

            try
            {
                foreach (spHRM_ValidateSalarySlipResult _item in this.db.spHRM_ValidateSalarySlip(_prmTransNmbr))
                {
                    spHRM_ValidateSalarySlipResult _spHRM_ValidateSalarySlipResult = new spHRM_ValidateSalarySlipResult();

                    _spHRM_ValidateSalarySlipResult.EmpNumb = _item.EmpNumb;
                    _spHRM_ValidateSalarySlipResult.EmpName = _item.EmpName;
                    _spHRM_ValidateSalarySlipResult.StartDate = _item.StartDate;
                    _spHRM_ValidateSalarySlipResult.JobTitle = _item.JobTitle;
                    _spHRM_ValidateSalarySlipResult.SKNo = _item.SKNo;

                    _result.Add(_spHRM_ValidateSalarySlipResult);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<spHRM_ValidateSalaryOvtResult> GetListSalaryOvt(string _prmTransNmbr)
        {
            List<spHRM_ValidateSalaryOvtResult> _result = new List<spHRM_ValidateSalaryOvtResult>();

            try
            {
                foreach (spHRM_ValidateSalaryOvtResult _item in this.db.spHRM_ValidateSalaryOvt(_prmTransNmbr))
                {
                    spHRM_ValidateSalaryOvtResult _spHRM_ValidateSalaryOvtResult = new spHRM_ValidateSalaryOvtResult();

                    _spHRM_ValidateSalaryOvtResult.TransNmbr = _item.TransNmbr;
                    _spHRM_ValidateSalaryOvtResult.TransDate = _item.TransDate;
                    _spHRM_ValidateSalaryOvtResult.Status = _item.Status;
                    _spHRM_ValidateSalaryOvtResult.DayType = _item.DayType;
                    _spHRM_ValidateSalaryOvtResult.EmpNumb = _item.EmpNumb;
                    _spHRM_ValidateSalaryOvtResult.EmpName = _item.EmpName;
                    _spHRM_ValidateSalaryOvtResult.StartHours = _item.StartHours;
                    _spHRM_ValidateSalaryOvtResult.EndHours = _item.EndHours;

                    _result.Add(_spHRM_ValidateSalaryOvtResult);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<spHRM_ValidateSalaryScheduleResult> GetListSalarySchedule(string _prmTransNmbr)
        {
            List<spHRM_ValidateSalaryScheduleResult> _result = new List<spHRM_ValidateSalaryScheduleResult>();

            try
            {
                foreach (spHRM_ValidateSalaryScheduleResult _item in this.db.spHRM_ValidateSalarySchedule(_prmTransNmbr))
                {
                    spHRM_ValidateSalaryScheduleResult _spHRM_ValidateSalaryScheduleResult = new spHRM_ValidateSalaryScheduleResult();

                    _spHRM_ValidateSalaryScheduleResult.EmpNumb = _item.EmpNumb;
                    _spHRM_ValidateSalaryScheduleResult.EmpName = _item.EmpName;
                    _spHRM_ValidateSalaryScheduleResult.StartDate = _item.StartDate;
                    _spHRM_ValidateSalaryScheduleResult.EmpGroup = _item.EmpGroup;

                    _result.Add(_spHRM_ValidateSalaryScheduleResult);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~PayrollInfoBL()
        {

        }
    }
}
