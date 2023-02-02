using System;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class ReportHumanResourceBL : Base
    {
        public ReportHumanResourceBL()
        {
        }

        public ReportDataSource EmployeeList(string _prmOrgUnit, string _prmEmpType, string _prmWorkPlace, string _prmJobLevel, string _prmJobTitle, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptEmployeeList";
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@EmpType", _prmEmpType);
                _cmd.Parameters.AddWithValue("@WorkPlace", _prmWorkPlace);
                _cmd.Parameters.AddWithValue("@JobLevel", _prmJobLevel);
                _cmd.Parameters.AddWithValue("@JobTitle", _prmJobTitle);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource RecruitmentRequestListPerTrans(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptRecruitmentRequestListPerTrans";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource RecruitmentRequestListPerDate(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptRecruitmentRequestListPerDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource RecruitmentRequestListPerOrgUnit(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptRecruitmentRequestListPerOrgUnit";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource RecruitmentRequestListPerSkill(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, string _prmSkillCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptRecruitmentRequestListPerSkill";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@SkillCode", _prmSkillCode);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ScreeningScheduleListPerTrans(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, string _prmProcessType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptScreeningSchedulePerTrans";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@ProcessType", _prmProcessType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ScreeningScheduleListPerDate(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, string _prmProcessType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptScreeningSchedulePerDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@ProcessType", _prmProcessType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ScreeningScheduleListPerProcessType(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, string _prmProcessType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptScreeningSchedulePerProcessType";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@ProcessType", _prmProcessType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ScreeningScheduleListPerApplicant(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, string _prmProcessType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptScreeningSchedulePerApplicant";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@ProcessType", _prmProcessType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ScreeningProcessListPerTrans(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, string _prmProcessType, string _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptScreeningProcessPerTrans";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@ProcessType", _prmProcessType);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ScreeningProcessListPerDate(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, string _prmProcessType, string _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptScreeningProcessPerDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@ProcessType", _prmProcessType);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ScreeningProcessListPerProcessType(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, string _prmProcessType, string _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptScreeningProcessPerProcessType";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@ProcessType", _prmProcessType);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ScreeningProcessListPerApplicant(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, string _prmProcessType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptScreeningProcessPerApplicant";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@ProcessType", _prmProcessType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public ReportDataSource ScreeningProcessListPerComment(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, string _prmProcessType, string _prmCommentStatus)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptScreeningProcessPerCommentStatus";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@ProcessType", _prmProcessType);
                _cmd.Parameters.AddWithValue("@CommentStatus", _prmCommentStatus);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public ReportDataSource ApplicantResume(DateTime _prmStartDate, DateTime _prmEndDate, string _prmStatus)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptApplicantResume";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ApplicantFinishingPerTrans(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptApplicantFinishingPerTrans";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ApplicantFinishingPerDate(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptApplicantFinishingPerDate";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ApplicantFinishingPerOrgUnit(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptApplicantFinishingPerOrgUnit";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ApplicantFinishingPerRecruitmentReq(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptApplicantFinishingPerRecruitmentReq";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ApplicantFinishingPerApplicant(DateTime _prmStartDate, DateTime _prmEndDate, string _prmOrgUnit, string _prmStatus)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptApplicantFinishingPerApplicant";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@OrgUnit", _prmOrgUnit);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource TerminationRequestPerTrans(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmStatus, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptTerminationRequestPerTrans";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource TerminationRequestPerDate(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmStatus, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptTerminationRequestPerDate";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource TerminationRequestPerEmployee(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmStatus, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptTerminationRequestPerEmployee";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource TerminationRequestPerReason(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmStatus, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptTerminationRequestPerReason";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource AbsenceRequestPerTrans(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmStatus, string _prmAbsenceType, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptAbsenceRequestPerTrans";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@AbsenceType", _prmAbsenceType);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource AbsenceRequestPerDate(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmStatus, string _prmAbsenceType, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptAbsenceRequestPerDate";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@AbsenceType", _prmAbsenceType);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource AbsenceRequestPerEmployee(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmStatus, string _prmAbsenceType, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptAbsenceRequestPerEmployee";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@AbsenceType", _prmAbsenceType);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource AbsenceRequestPerAbsenceType(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmStatus, string _prmAbsenceType, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptAbsenceRequestPerAbsenceType";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@AbsenceType", _prmAbsenceType);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource AbsenceRequestPerEmpActing(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmStatus, string _prmAbsenceType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptAbsenceRequestPerEmpActing";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@AbsenceType", _prmAbsenceType);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource Attendance(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmAbsenceType, string _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptAttendance";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@AbsenceType", _prmAbsenceType);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public List<ReportDataSource> ScheduleShiftEmpGroup(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpGroup)
        {
            List<ReportDataSource> _result = new List<ReportDataSource>();
            DataTable _dataTable = new DataTable();
            DataTable _dataTable2 = new DataTable();

            try
            {
                ReportDataSource _dataset1 = new ReportDataSource();
                ReportDataSource _dataset2 = new ReportDataSource();

                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));
                SqlConnection _conn2 = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();
                SqlCommand _cmd2 = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "SpHRM_RptScheduleShiftEmpGroupSummary";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpGroup", _prmEmpGroup);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _cmd2.CommandType = CommandType.Text;
                _cmd2.Parameters.Clear();
                _cmd2.Connection = _conn2;
                _cmd2.CommandText = "SELECT shiftcode, shiftname FROM HRMMsShift";
                SqlDataAdapter _da2 = new SqlDataAdapter();

                _da2.SelectCommand = _cmd2;
                _da2.Fill(_dataTable2);

                _dataset1.Value = _dataTable;
                _dataset1.Name = "DataSet1";
                _dataset2.Value = _dataTable2;
                _dataset2.Name = "DataSet2";

                _result.Add(_dataset1);
                _result.Add(_dataset2);
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public List<ReportDataSource> ScheduleShiftEmp(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb)
        {
            List<ReportDataSource> _result = new List<ReportDataSource>();
            DataTable _dataTable = new DataTable();
            DataTable _dataTable2 = new DataTable();

            try
            {
                ReportDataSource _dataset1 = new ReportDataSource();
                ReportDataSource _dataset2 = new ReportDataSource();

                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));
                SqlConnection _conn2 = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();
                SqlCommand _cmd2 = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "SpHRM_RptScheduleShiftSummary";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Employee", _prmEmpNumb);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _cmd2.CommandType = CommandType.Text;
                _cmd2.Parameters.Clear();
                _cmd2.Connection = _conn2;
                _cmd2.CommandText = "SELECT shiftcode, shiftname FROM HRMMsShift";
                SqlDataAdapter _da2 = new SqlDataAdapter();

                _da2.SelectCommand = _cmd2;
                _da2.Fill(_dataTable2);

                _dataset1.Value = _dataTable;
                _dataset1.Name = "DataSet1";
                _dataset2.Value = _dataTable2;
                _dataset2.Name = "DataSet2";

                _result.Add(_dataset1);
                _result.Add(_dataset2);
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource UpdateLeavePerTrans(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmStatus)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptUpdateLeavePerTrans";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource UpdateLeavePerDate(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmStatus)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptUpdateLeavePerDate";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource UpdateLeavePerEmployee(DateTime _prmStartDate, DateTime _prmEndDate, string _prmEmpNumb, string _prmStatus)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptUpdateLeavePerEmployee";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@Status", _prmStatus);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource EmployeeSummaryReport(int _prmYear)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_PERptEmpSummary";
                _cmd.Parameters.AddWithValue("@Year", _prmYear);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource EmployeeListingReport(string _prmStr2, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_PERptEmpListing";
                //_cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                //_cmd.Parameters.AddWithValue("@Str3 ", _prmStr3);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource OvertimeEmp(DateTime _prmStartDate, DateTime _prmEndDate, String _prmEmpNumb, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptOvertimePerEmp";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource OvertimeEmpType(DateTime _prmStartDate, DateTime _prmEndDate, String _prmEmpNumb, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptOvertimePerEmpType";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource OvertimeJobLevel(DateTime _prmStartDate, DateTime _prmEndDate, String _prmEmpNumb, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptOvertimePerJobLvl";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource OvertimeJobTitle(DateTime _prmStartDate, DateTime _prmEndDate, String _prmEmpNumb, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptOvertimePerJobTtl";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource OvertimeWorkPlace(DateTime _prmStartDate, DateTime _prmEndDate, String _prmEmpNumb, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptOvertimePerWorkPlace";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmEmpNumb);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource EmployeeTotal(string _prmJobTitle, String _prmDisplayType, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptEmpTotal";
                _cmd.Parameters.AddWithValue("@Str1", _prmJobTitle);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", _prmDisplayType);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource EmployeeExpire(string _prmJobTitle, String _prmEmpStatus, int _prmFgReport, DateTime _prmDateTime)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptEmpExpireContract";
                _cmd.Parameters.AddWithValue("@Date", _prmDateTime);
                _cmd.Parameters.AddWithValue("@Str1", _prmJobTitle);
                _cmd.Parameters.AddWithValue("@Str2", _prmEmpStatus);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource EmployeeUnder(string _prmJobTitle, String _prmEmpStatus, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptEmpUnderContract";
                _cmd.Parameters.AddWithValue("@Str1", ((_prmJobTitle == "") ? " " : _prmJobTitle));
                _cmd.Parameters.AddWithValue("@Str2", _prmEmpStatus);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource EmployeeLeaveIn(string _prmJobTitle, int _prmFgReport, int _prmStartYear, int _prmEndYear, int _prmStartPeriod, int _prmEndPeriod)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptLeaveInPerEmp";
                _cmd.Parameters.AddWithValue("@Str1", _prmJobTitle);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgSortBy", _prmFgReport);
                _cmd.Parameters.AddWithValue("@StartYear", _prmStartYear);
                _cmd.Parameters.AddWithValue("@EndYear", _prmEndYear);
                _cmd.Parameters.AddWithValue("@StartPeriod", _prmStartPeriod);
                _cmd.Parameters.AddWithValue("@EndPeriod", _prmEndPeriod);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource EmployeePrintPreview(string _prmEmpNumb)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_EmployeePrintPreview";
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource AbsenceRequestPrintPreview(string _prmAbsenceRequestCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_AbsenceRequestPrintPreview";
                _cmd.Parameters.AddWithValue("@Code", _prmAbsenceRequestCode);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ClaimIssuePrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_ClaimIssuePrintPreview";
                _cmd.Parameters.AddWithValue("@TransNmbr", _prmTransNmbr);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ClaimPlafonPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_ClaimPlafonPrintPreview";
                _cmd.Parameters.AddWithValue("@TransNmbr", _prmTransNmbr);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource LeaveCard(DateTime _prmDate, string _prmEmpNumb)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "SpHRM_RptLeaveCard";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@StrEmp", _prmEmpNumb);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource HRAnalysis(DateTime _prmStartDate, DateTime _prmEndDate, int _prmFgFilter, String _prmFromEmp, String _prmToEmp, string _prmEmpNumb)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spHRM_RptHRAnalysis";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@FgFilter", _prmFgFilter);
                _cmd.Parameters.AddWithValue("@FromEmp", _prmFromEmp);
                _cmd.Parameters.AddWithValue("@ToEmp", _prmToEmp);
                _cmd.Parameters.AddWithValue("@EmpNumb", _prmEmpNumb);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        ~ReportHumanResourceBL()
        {
        }
    }
}
