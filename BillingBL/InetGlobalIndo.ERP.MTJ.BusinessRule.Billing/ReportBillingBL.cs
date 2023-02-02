using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Data;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using MySql.Data.MySqlClient;
namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class ReportBillingBL
    {
        public ReportBillingBL()
        {
        }

        public ReportDataSource BillingInvoicePrintPreview(String _prmInvoiceHd)
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
                _cmd.CommandText = "S_BillingInvoicePrintPreview";
                _cmd.Parameters.AddWithValue("@InvoiceNo", _prmInvoiceHd.ToString());

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

        public ReportDataSource BillingTaxInvoicePrintPreview(String _prmInvoiceHd)
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
                _cmd.CommandText = "S_BillingTaxInvoicePrintPreview";
                _cmd.Parameters.AddWithValue("@InvoiceNo", _prmInvoiceHd.ToString());

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

        public ReportDataSource PreviewBillingInvoice(int _prmPeriod, int _prmYear, string _prmCustomerGroup, string _prmCustomerType, string _prmCustomerCode)
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
                _cmd.CommandText = "S_BillingGenerateInvoicePrintPreview";
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@CustomerGroup", _prmCustomerGroup);
                _cmd.Parameters.AddWithValue("@CustomerType", _prmCustomerType);
                _cmd.Parameters.AddWithValue("@CustomerCode", _prmCustomerCode);

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

        public ReportDataSource BillingInvoiceLembarPengantar(int _prmPeriod, int _prmYear, string _prmCustomerGroup, string _prmCustomerType, string _prmCustomerCode)
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
                _cmd.CommandText = "S_BillingLembarPengantar";
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@CustomerGroup", _prmCustomerGroup);
                _cmd.Parameters.AddWithValue("@CustomerType", _prmCustomerType);
                _cmd.Parameters.AddWithValue("@CustomerCode", _prmCustomerCode);

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

        public ReportDataSource CustomerInvoiceLembarPengantar(int _prmPeriod, int _prmYear, string _prmCustomerGroup, string _prmCustomerType, string _prmCustomerCode)
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
                _cmd.CommandText = "S_BillingCustomerInvoiceLembarPengantar";
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@CustomerGroup", _prmCustomerGroup);
                _cmd.Parameters.AddWithValue("@CustomerType", _prmCustomerType);
                _cmd.Parameters.AddWithValue("@CustomerCode", _prmCustomerCode);

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

        public ReportDataSource PreviewTaxBillingInvoice(int _prmPeriod, int _prmYear, string _prmCustomerGroup, string _prmCustomerType, string _prmCustomerCode, int _prmStatusCurrency)
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
                _cmd.CommandText = "S_BillingGenerateTaxInvoicePrintPreview";
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@CustomerGroup", _prmCustomerGroup);
                _cmd.Parameters.AddWithValue("@CustomerType", _prmCustomerType);
                _cmd.Parameters.AddWithValue("@CustomerCode", _prmCustomerCode);
                _cmd.Parameters.AddWithValue("@StatusCurrCode", _prmStatusCurrency);

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

        public ReportDataSource CustomerInvoicePrintPreview(String _prmInvoiceHd)
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
                _cmd.CommandText = "S_BillingCustomerInvoicePrintPreview";
                _cmd.Parameters.AddWithValue("@InvoiceNo", _prmInvoiceHd.ToString());

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

        public ReportDataSource SalesConfirmationPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "spBIL_SalesConfirmationPrintPreview";
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

        public ReportDataSource BeritaAcaraPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "spBIL_BeritaAcaraPrintPreview";
                _cmd.Parameters.AddWithValue("@prmTransNmbr", _prmTransNmbr);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public ReportDataSource BillingTaxCustomerInvoicePrintPreview(String _prmCustInvoiceHd)
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
                _cmd.CommandText = "S_BillingTaxCustomerInvoicePrintPreview";
                _cmd.Parameters.AddWithValue("@CustomerInvoiceNo", _prmCustInvoiceHd);

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

        public ReportDataSource BillBandwidth()
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
                _cmd.CommandText = "spBilling_BandwidthReport";

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

        public ReportDataSource CustomerBillingAccount(string _prmPeriod, string _prmYear, string _prmCustGroup, string _prmCustType)
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
                _cmd.CommandText = "spBilling_CustomerBillingAccountReport";

                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@CustGroup", _prmCustGroup);
                _cmd.Parameters.AddWithValue("@CustType", _prmCustType);

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

        public ReportDataSource CustomerBandwitdhUsage(string _prmCustGroup, string _prmCustType)
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
                _cmd.CommandText = "spBilling_CustomerBandwidthUsageReport";

                //_cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                //_cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@CustGroup", _prmCustGroup);
                _cmd.Parameters.AddWithValue("@CustType", _prmCustType);

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

        public ReportDataSource CustomerInvoiceCategory(string _prmPeriod, string _prmYear, string _prmEndPeriod, string _prmEndYear)
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
                _cmd.CommandText = "spBilling_CustomerInvoiceCategoryReport";

                _cmd.Parameters.AddWithValue("@StartYear", _prmYear);
                _cmd.Parameters.AddWithValue("@StartPeriod", _prmPeriod);
                _cmd.Parameters.AddWithValue("@EndYear", _prmEndYear);
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

        public ReportDataSource UnSubscriptionPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "S_MKSOPrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmTransNmbr);
                //_cmd.Parameters.AddWithValue("@Revisi", );

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

        public ReportDataSource ContractPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "S_MKSOPrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmTransNmbr);
                //_cmd.Parameters.AddWithValue("@Revisi", );

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

        public ReportDataSource RadiusReportPerPeriod(string _prmPeriod, string _prmYear, string _prmRadiusCode)
        {
            ReportDataSource _result = new ReportDataSource();
            
            try
            {
                MySqlConnection _conn = new MySqlConnection(new RadiusUpdateVoucherBL().GetDatabaseConnString(_prmRadiusCode));

                MySqlCommand _cmd = new MySqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Connection = _conn;
                _cmd.CommandTimeout = 3000;
                _cmd.CommandText = "monthlyusageactivity";
                _cmd.Parameters.Clear();
                
                _cmd.Parameters.Add("_prmYearPeriod", MySqlDbType.VarChar, 20);
                _cmd.Parameters["_prmYearPeriod"].Value = _prmYear + _prmPeriod;

                if (_conn.State == System.Data.ConnectionState.Open)
                {
                    _conn.Close();
                }

                if (_conn.State != System.Data.ConnectionState.Open)
                {
                    _conn.Open();                    
                }

                MySqlDataAdapter _da = new MySqlDataAdapter();
                DataTable _dataTable = new DataTable();
                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);
                //MySqlDataReader _dr = _cmd.ExecuteReader();

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public ReportDataSource BillingInvoiceSummary(DateTime _prmStartDate, DateTime _prmEndDate, String _prmCustType, String _prmCustGroup)
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
                _cmd.CommandText = "spBIL_RptAllBillingInvoice";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustType);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustGroup);
                _cmd.Parameters.AddWithValue("@Str3", "");

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public ReportDataSource SalesConfirmation(DateTime _prmStartDate, DateTime _prmEndDate, String _prmDDL)
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
                _cmd.CommandText = "SpBil_salesConfirmasi";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Fg", _prmDDL);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public ReportDataSource CustomerInvoiceSummary(DateTime _prmStartDate, DateTime _prmEndDate, String _prmCustType, String _prmCustGroup)
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
                _cmd.CommandText = "spBIL_RptAllCustomerInvoice";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustType);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustGroup);
                _cmd.Parameters.AddWithValue("@Str3", "");

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }


        public ReportDataSource BeritaAcaraReport(DateTime _prmStartDate, DateTime _prmEndDate)
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
                _cmd.CommandText = "SpBIL_BeritaAcaraReport";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        ~ReportBillingBL()
        {
        }
    }
}
