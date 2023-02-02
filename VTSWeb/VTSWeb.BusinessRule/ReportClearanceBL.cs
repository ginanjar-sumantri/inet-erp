using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using VTSWeb.Common;
using VTSWeb.SystemConfig;
using VTSWeb.Database;

namespace VTSWeb.UI
{
    public sealed class ReportClearanceBL : VTSWeb.SystemConfig.Base
    {
        public ReportClearanceBL()
        {
        }

        #region ReportClearance

        public ReportDataSource ReportCustomerLogVisitByCustomer(String _prmCusCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(ApplicationConfig.ConnString);

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "Sp_ReportCustomerLogVisitByCustomer";
                _cmd.Parameters.AddWithValue("@CustCode", _prmCusCode);

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

        public ReportDataSource ReportCustomerLogVisitByDate(DateTime _prmStartDate, DateTime _prmEndDate, String _prmStartCustName, String _prmEndCustName)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(ApplicationConfig.ConnString);

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "Sp_CustomerLogVisitByDate";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@StartCustName", _prmStartCustName);
                _cmd.Parameters.AddWithValue("@EndCustName", _prmEndCustName);

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

        public ReportDataSource ReportSummaryCustomerLogVisitByCustomer(DateTime _prmStartDate, DateTime _prmEndDate, String _prmStartCustName, String _prmEndCustName)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(ApplicationConfig.ConnString);

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "Sp_SummaryCustomerLogVisitByCustomer";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@StartCustName", _prmStartCustName);
                _cmd.Parameters.AddWithValue("@EndCustName", _prmEndCustName);

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

        public ReportDataSource ReportCustomerListVisitorPermission(String _prmCusCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(ApplicationConfig.ConnString);

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "Sp_ReportCustomerListVisitorPermission";
                _cmd.Parameters.AddWithValue("@CustCode", _prmCusCode);

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


        public ReportDataSource ReportCustomerVisitorCard(String _prmCusCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(ApplicationConfig.ConnString);

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "Sp_ReportCustomerVisitorCard";
                _cmd.Parameters.AddWithValue("@CustCode", _prmCusCode);

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
        #endregion

        #region ReportGoodsInOut
        public ReportDataSource ReportCustomerGoodInTransactionByCustomer(String _prmCusCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(ApplicationConfig.ConnString);

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "Sp_CustomerGoodInTransactionByCustomer";
                _cmd.Parameters.AddWithValue("@CustCode", _prmCusCode);

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

        public ReportDataSource ReportCustomerEquipmentList(String _prmCusCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(ApplicationConfig.ConnString);

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "Sp_CustomerEquipmentListNew";
                _cmd.Parameters.AddWithValue("@CustCode", _prmCusCode);

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

        #endregion

        ~ReportClearanceBL()
        {

        }

    }
}