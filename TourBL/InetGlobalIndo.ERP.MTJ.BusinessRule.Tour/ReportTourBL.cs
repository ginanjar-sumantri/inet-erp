using System;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Tour
{
    public sealed class ReportTourBL
    {
        public ReportTourBL()
        {
        }

        public ReportDataSource HotelPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "spPOS_PrintPreviewHotel";
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

        public ReportDataSource HotelPrintPreview2()
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
                _cmd.CommandText = "spPOS_TourBank";

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet2";
            }
            catch (Exception ex)
            {
            }

            return _result;
        }


        public ReportDataSource HotelJournalPrintPreview(String _prmTransNmbr)
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

        public ReportDataSource TicketingPrintPreview(String _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();
                SqlCommand _cmd2 = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spPOS_PrintPreviewTicket";
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

        //public ReportDataSource TicketingPrintPreview2()
        //{
        //    ReportDataSource _result = new ReportDataSource();
        //    DataTable _dataTable = new DataTable();
            

        //    try
        //    {
        //        SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

        //        SqlCommand _cmd = new SqlCommand();

        //        _cmd.CommandType = CommandType.StoredProcedure;
        //        _cmd.Parameters.Clear();
        //        _cmd.Connection = _conn;
        //        _cmd.CommandText = "spPOS_TourBank";

        //        SqlDataAdapter _da2 = new SqlDataAdapter();

        //        _da2.SelectCommand = _cmd;
        //        _da2.Fill(_dataTable);

        //        _result.Value = _dataTable;
        //        _result.Name = "DataSet2";


        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return _result;
        //}

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

        public ReportDataSource RekapTicketingByDate(DateTime _prmBeginDate, DateTime _prmEndDate)
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
                _cmd.CommandText = "spPOS_RptRekapTicketByDate";
                _cmd.Parameters.AddWithValue("@BeginDate", _prmBeginDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);

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

        public ReportDataSource RekapVoucherHotelByDate(DateTime _prmBeginDate, DateTime _prmEndDate)
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
                _cmd.CommandText = "spPOS_RptRekapHotelByDate";
                _cmd.Parameters.AddWithValue("@BeginDate", _prmBeginDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);

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

        ~ReportTourBL()
        {

        }
    }
}
