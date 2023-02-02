using System;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class ReportBL : Base
    {
        public ReportBL()
        {

        }

        public ReportDataSource BSheetMonthly(int _prmYear, int _prmPeriod, int _prmType, decimal _prmFgDivide)
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
                _cmd.CommandText = "S_GLRptBSheetMonthly";
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                _cmd.Parameters.AddWithValue("@Type", _prmType);
                _cmd.Parameters.AddWithValue("@FgDivide", _prmFgDivide);

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

        public ReportDataSource CashFlowActualCashRange(string _prmStartYear, string _prmStartPeriod, string _prmEndYear, string _prmEndPeriod)
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
                _cmd.CommandText = "spAcc_RptCashFlowActual";
                _cmd.Parameters.AddWithValue("@StartYear", _prmStartYear);
                _cmd.Parameters.AddWithValue("@StartMonth", _prmStartPeriod);
                _cmd.Parameters.AddWithValue("@EndYear", _prmEndYear);
                _cmd.Parameters.AddWithValue("@EndMonth", _prmEndPeriod);

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

        public ReportDataSource BSheet(int _prmYear, int _prmPeriod, int _prmMode, int _prmType)
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
                _cmd.CommandText = "S_GLRptBSheet";
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                _cmd.Parameters.AddWithValue("@Mode", _prmMode);
                _cmd.Parameters.AddWithValue("@Type", _prmType);

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

        public ReportDataSource BSAndPL(int _prmYear, int _prmPeriod)
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
                _cmd.CommandText = "S_GLRptBSWithPL";
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                _cmd.Parameters.AddWithValue("@Type", 0);

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

        public ReportDataSource ChartOfAccount(decimal _prmType)
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
                _cmd.CommandText = "S_GLRptMsAccount";
                _cmd.Parameters.AddWithValue("@FgType", _prmType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "ChartOfAccount";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource TrialBalanceSummary(int _prmYear, int _prmPeriod, int _prmFgReport)
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
                _cmd.CommandText = "S_GLRptBSTrial";
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
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

        public ReportDataSource GLSummaryRefundByDay(DateTime _prmStartYear, string _prmStartPeriod, DateTime _prmEndYear, string _prmEndPeriod, string _prmReportList, string _prmRefund)
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
                _cmd.CommandText = "Sp_RptNCPRefundList";
                _cmd.Parameters.AddWithValue("@Start", _prmStartYear);
                _cmd.Parameters.AddWithValue("@End", _prmEndYear);
                _cmd.Parameters.AddWithValue("@ProdStart", _prmStartPeriod);
                _cmd.Parameters.AddWithValue("@ProdEnd", _prmEndPeriod);
                _cmd.Parameters.AddWithValue("@FgReport", _prmReportList);
                _cmd.Parameters.AddWithValue("@Str1", _prmRefund);

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


        public ReportDataSource GLSummary(int _prmStartYear, int _prmStartPeriod, int _prmEndYear, int _prmEndPeriod, string _prmAccount, int _prmOrder, string _prmFrom, string _prmTo)
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
                _cmd.CommandText = "S_GLRptGLSummary";
                _cmd.Parameters.AddWithValue("@StartYear", _prmStartYear);
                _cmd.Parameters.AddWithValue("@EndYear", _prmEndYear);
                _cmd.Parameters.AddWithValue("@StartPeriod", _prmStartPeriod);
                _cmd.Parameters.AddWithValue("@EndPeriod", _prmEndPeriod);
                _cmd.Parameters.AddWithValue("@PrmAccount", _prmAccount);
                _cmd.Parameters.AddWithValue("@OrderBy", _prmOrder);
                _cmd.Parameters.AddWithValue("@from", _prmFrom);
                _cmd.Parameters.AddWithValue("@To", _prmTo);

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

        public ReportDataSource GLSummaryForex(int _prmStartYear, int _prmStartPeriod, int _prmEndYear, int _prmEndPeriod, string _prmAccount, int _prmOrder, string _prmFrom, string _prmTo)
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
                _cmd.CommandText = "S_GLRptGLSummaryForex";
                _cmd.Parameters.AddWithValue("@StartYear", _prmStartYear);
                _cmd.Parameters.AddWithValue("@EndYear", _prmEndYear);
                _cmd.Parameters.AddWithValue("@StartPeriod", _prmStartPeriod);
                _cmd.Parameters.AddWithValue("@EndPeriod", _prmEndPeriod);
                _cmd.Parameters.AddWithValue("@PrmAccount", _prmAccount);
                _cmd.Parameters.AddWithValue("@OrderBy", _prmOrder);
                _cmd.Parameters.AddWithValue("@from", _prmFrom);
                _cmd.Parameters.AddWithValue("@To", _prmTo);

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

        public ReportDataSource GLSummaryByDate(DateTime _prmStartDate, DateTime _prmEndDate, string _prmAccount, int _prmOrder, string _prmFrom, string _prmTo)
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
                _cmd.CommandText = "S_GLRptGLSummaryByDate";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@PrmAccount", _prmAccount);
                _cmd.Parameters.AddWithValue("@OrderBy", _prmOrder);
                _cmd.Parameters.AddWithValue("@From", _prmFrom);
                _cmd.Parameters.AddWithValue("@To", _prmTo);

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

        public ReportDataSource GLSummaryByDateForex(DateTime _prmStartDate, DateTime _prmEndDate, string _prmAccount, int _prmOrder, string _prmFrom, string _prmTo)
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
                _cmd.CommandText = "S_GLRptGLSummaryByDateForex";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@PrmAccount", _prmAccount);
                _cmd.Parameters.AddWithValue("@OrderBy", _prmOrder);
                _cmd.Parameters.AddWithValue("@From", _prmFrom);
                _cmd.Parameters.AddWithValue("@To", _prmTo);

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

        public ReportDataSource TransactionJournalDaily(DateTime _prmStartDate, DateTime _prmEndDate, String _prmtransClass, String _prmFileNmbr) //, int _prmPeriod
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
                _cmd.CommandText = "S_GLRptJournalDaily";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@PrmTransClass", _prmtransClass);
                _cmd.Parameters.AddWithValue("@Str1", "00");
                _cmd.Parameters.AddWithValue("@Str2", _prmFileNmbr);

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

        public ReportDataSource DirectSalesPerTransaction(DateTime _prmStartDate, DateTime _prmEndDate, String _prmUser) //, int _prmPeriod
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
                _cmd.CommandText = "SpSAL_RptDirectSalesPerTransaction";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@prmUser", _prmUser);

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

        public ReportDataSource DirectSalesPerCustomer(DateTime _prmStartDate, DateTime _prmEndDate, String _prmCust) //, int _prmPeriod
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
                _cmd.CommandText = "SpSAL_RptDirectSalesPerCustomer";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@prmCust", _prmCust);

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

        public ReportDataSource DirectSalesPerProduct(DateTime _prmStartDate, DateTime _prmEndDate, String _prmUser) //, int _prmPeriod
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
                _cmd.CommandText = "SpSAL_RptDirectSalesPerProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@PrmProduct", _prmUser);

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

        public ReportDataSource GLSubled(int _prmStartYear, int _prmStartPeriod, int _prmEndYear, int _prmEndPeriod, string _prmFGSubled, int _prmOrder)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                //int _IsIndonesian = 0;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_GLRptGLSubLed";
                _cmd.Parameters.AddWithValue("@StartYear", _prmStartYear);
                _cmd.Parameters.AddWithValue("@EndYear", _prmEndYear);
                _cmd.Parameters.AddWithValue("@StartPeriod", _prmStartPeriod);
                _cmd.Parameters.AddWithValue("@EndPeriod", _prmEndPeriod);
                _cmd.Parameters.AddWithValue("@str1", "");
                _cmd.Parameters.AddWithValue("@FgSubLed", _prmFGSubled);
                _cmd.Parameters.AddWithValue("@SubLed", "");
                _cmd.Parameters.AddWithValue("@OrderBy", _prmOrder);

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

        public ReportDataSource AccountMutation(DateTime _prmStartDate, DateTime _prmEndDate, string _prmAccount)
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
                _cmd.CommandText = "S_GLRptAccMutation";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Account", _prmAccount);

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

        public ReportDataSource TransactionJournal(string _prmTransClass, int _prmYear, int _prmPeriod, string _prmFgType, string _prmTransId)
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
                _cmd.CommandText = "S_GLRptJournal";
                _cmd.Parameters.AddWithValue("@TransClass", _prmTransClass);
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);
                _cmd.Parameters.AddWithValue("@TransId", _prmTransId);

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

        public ReportDataSource PLSummary(int _prmYear, int _prmPeriod, int _prmGroup, int _prmMode)
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
                _cmd.CommandText = "S_GLRptPLSummary";
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                _cmd.Parameters.AddWithValue("@Mode", _prmMode);
                _cmd.Parameters.AddWithValue("@GroupBy", _prmGroup);

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

        public ReportDataSource PLMonthly(int _prmYear, int _prmPeriod, int _prmGroup, int _prmMode)
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
                _cmd.CommandText = "S_GLRptPLMonthly";
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                _cmd.Parameters.AddWithValue("@Mode", _prmMode);
                _cmd.Parameters.AddWithValue("@GroupBy", _prmGroup);

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

        public ReportDataSource FixedAssetList(string _prmFgActive, string _prmStatus, string _prmLocationCode, string _prmLocationType, int _prmType, string _prmFgSold, string _prmGroup, string _prmSubGroup)
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
                _cmd.CommandText = "S_RptFixAssetList";
                _cmd.Parameters.AddWithValue("@FAStatus", _prmStatus);
                _cmd.Parameters.AddWithValue("@FALocationCode", _prmLocationCode);
                _cmd.Parameters.AddWithValue("@FALocationType", _prmLocationType);
                _cmd.Parameters.AddWithValue("@FgActive", _prmFgActive);
                _cmd.Parameters.AddWithValue("@FgSold", _prmFgSold);
                _cmd.Parameters.AddWithValue("@Type", _prmType);
                _cmd.Parameters.AddWithValue("@FAGroup", _prmGroup);
                _cmd.Parameters.AddWithValue("@FASubGroup", _prmSubGroup);

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

        public ReportDataSource BudgetEntryPrintPreview(int _prmYear, int _prmPeriod)
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
                _cmd.CommandText = "S_GLBudgetPrintPreview";
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);

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

        public ReportDataSource JournalEntryPrintPreview(string _prmNmbr)
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
                _cmd.CommandText = "S_GLJEPrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmNmbr);

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

        public ReportDataSource FADevaluationPrintPreview(string _prmNmbr)
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
                _cmd.CommandText = "S_GLFADevaluationPrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmNmbr);

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

        public ReportDataSource GLFAAddStockPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandText = "S_GLFAFromStockPrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmTransNmbr);

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

        public ReportDataSource TransTypeAccountPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandText = "SpAcc_TransType_MsAccount";
                _cmd.Parameters.AddWithValue("@TransType", _prmTransNmbr);

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


        public ReportDataSource FAMovePrintPreview(string _prmNmbr)
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
                _cmd.CommandText = "S_GLFAMovePrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmNmbr);

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

        public ReportDataSource FASalesPrintPreview(string _prmNmbr)
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
                _cmd.CommandText = "S_GLFASalesPrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmNmbr);

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

        public ReportDataSource FAPurchasePrintPreview(string _prmNmbr)
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
                _cmd.CommandText = "S_GLFAPurchasePrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmNmbr);

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

        public ReportDataSource FATenancyPrintPreview(string _prmNmbr)
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
                _cmd.CommandText = "S_GLFATenancyPrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmNmbr);

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

        public ReportDataSource FAProcessPrintPreview(int _prmYear, int _prmPeriod)
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
                _cmd.CommandText = "S_GLFAProcessPrintPreview";
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);

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

        public ReportDataSource FAServicePrintPreview(string _prmNmbr)
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
                _cmd.CommandText = "S_GLFAServicePrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmNmbr);

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

        public ReportDataSource CurrRate(DateTime _prmStart, DateTime _prmEnd, String _prmCurrCode, int _prmFgReport)
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
                _cmd.CommandText = "S_GLRptCurrRate";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCurrCode);
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

        public ReportDataSource JournalTicketingPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "S_GLJEPrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmTransNmbr);

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

        public ReportDataSource CashflowProjection(DateTime _prmDate, String _prmRangeType, Int32 _prmRange)
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
                _cmd.CommandText = "S_FNRptCashFlowProjection";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@RangeType", _prmRangeType);
                _cmd.Parameters.AddWithValue("@RangeX", _prmRange);

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

        ~ReportBL()
        {

        }
    }
}