using System;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Sales
{
    public sealed class ReportSalesBL
    {
        public ReportSalesBL()
        {

        }

        public ReportDataSource SalesOrderPrintPreview(String _prmTransNmbr, int _prmRevisi)
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
                _cmd.Parameters.AddWithValue("@Revisi", _prmRevisi);

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

        public ReportDataSource DeliveryOrderPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "S_MKDOPrintPreview";
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

        public ReportDataSource DirectSalesPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "SpSAL_DirectSalesPrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmTransNmbr);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "dataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource RequestSalesReturPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "SpSAL_RequestSalesReturPrintPreview";
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

        public ReportDataSource CustomerList(String _prmCustGroup, String _prmCustType, String _prmCity, int _prmFgExport)
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
                _cmd.CommandText = "S_MKRptCustomerList";
                _cmd.Parameters.AddWithValue("@StrGroup", _prmCustGroup);
                _cmd.Parameters.AddWithValue("@StrCity", _prmCity);
                _cmd.Parameters.AddWithValue("@Str1", "0");
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);
                _cmd.Parameters.AddWithValue("@StrType", _prmCustType);

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

        public ReportDataSource ProFormaInvPerTrans(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, int _prmFgReport, int _prmFgExport, int _prmFgCurr, int _prmSelection, String _prmFrom, String _prmTo)
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
                _cmd.CommandText = "S_MKRptSOPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgSelection", _prmSelection);
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

        public ReportDataSource RequestSalesReturPerTrans(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, int _prmFgReport, int _prmFgCurr)
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
                _cmd.CommandText = "S_MKRptReqRtrPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);

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

        public ReportDataSource RequestSalesReturPerProduct(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, int _prmFgReport, int _prmFgCurr)
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
                _cmd.CommandText = "S_MKRptReqRtrPerProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);

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

        public ReportDataSource RequestSalesReturPerCust(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, int _prmFgReport, int _prmFgCurr)
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
                _cmd.CommandText = "S_MKRptReqRtrPerCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);

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

        public ReportDataSource RequestSalesReturOutPerTrans(DateTime _prmDate, String _prmCustCode, String _prmProduct)
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
                _cmd.CommandText = "S_MKRptReqRtrOutPerNmbr";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProduct);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", 0);

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

        public ReportDataSource ProgressSumPFI_BL(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, String _prmProduct, int _prmType, int _prmExport)
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
                _cmd.CommandText = "S_MKRptProgressSODORkp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProduct);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmType);
                _cmd.Parameters.AddWithValue("@FgExport", _prmExport);

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

        public ReportDataSource ProgressDetailPFI_BL(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, String _prmProduct, int _prmType, int _prmExport)
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
                _cmd.CommandText = "S_MKRptProgressSODODtl";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProduct);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmType);
                _cmd.Parameters.AddWithValue("@FgExport", _prmExport);

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

        public ReportDataSource RequestSalesReturOutPerCust(DateTime _prmDate, String _prmCustCode, String _prmProduct, int _prmReportType)
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
                _cmd.CommandText = "S_MKRptReqRtrOutPerCust";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProduct);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmReportType);

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

        public ReportDataSource RequestSalesReturOutPerProd(DateTime _prmDate, String _prmCustCode, String _prmProduct, int _prmReportType)
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
                _cmd.CommandText = "S_MKRptReqRtrOutPerProduct";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProduct);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmReportType);

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

        public ReportDataSource ProFormaInvPerCust(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, int _prmFgReport, int _prmFgExport, int _prmFgCurr, int _prmSelection, String _prmFrom, String _prmTo)
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
                _cmd.CommandText = "S_MKRptSOPerCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgSelection", _prmSelection);
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

        public ReportDataSource ProFormaInvPerProduct(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, int _prmFgReport, int _prmFgExport, int _prmFgCurr, int _prmSelection, String _prmFrom, String _prmTo)
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
                _cmd.CommandText = "S_MKRptSOPerProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgSelection", _prmSelection);
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

        public ReportDataSource ProFormaInvSummaryPerCust(String _prmStart, String _prmEnd, String _prmCustCode, int _prmFgExport, int _prmFgCurr, int _prmSelection, String _prmFrom, String _prmTo)
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
                _cmd.CommandText = "S_MKRptSOMonthCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgSelection", _prmSelection);
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

        public ReportDataSource ProFormaInvSummaryPerCustByYear(String _prmStart, String _prmEnd, String _prmCustCode, int _prmFgReport, int _prmFgExport, int _prmFgCurr, int _prmSelection, String _prmFrom, String _prmTo)
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
                _cmd.CommandText = "S_MKRptSOYear";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgFilter", _prmSelection);
                _cmd.Parameters.AddWithValue("@FromCust", _prmFrom);
                _cmd.Parameters.AddWithValue("@ToCust", _prmTo);

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

        public ReportDataSource ProFormaInvSummaryPerProduct(String _prmStart, String _prmEnd, String _prmProductSubGroup, String _prmCustCode, int _prmFgReport, int _prmFgExport, int _prmFgCurr, int _prmSelection, String _prmFrom, String _prmTo, String _prmProdSubFrom, String _prmProdSubTo)
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
                _cmd.CommandText = "S_MKRptSOMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgSelection", _prmSelection);
                _cmd.Parameters.AddWithValue("@From", _prmFrom);
                _cmd.Parameters.AddWithValue("@To", _prmTo);
                _cmd.Parameters.AddWithValue("@ProdFrom", _prmProdSubFrom);
                _cmd.Parameters.AddWithValue("@ProdTo", _prmProdSubTo);

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

        public ReportDataSource OutstandingProFormaInvPerTrans(DateTime _prmDate, String _prmCustCode, String _prmProductCode, int _prmFgExport, int _prmFgCurr)
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
                _cmd.CommandText = "S_MKRptSOOutPerNmbr";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", 0);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);

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

        public ReportDataSource OutstandingProFormaInvPerCust(DateTime _prmDate, String _prmCustCode, String _prmProductCode, int _prmFgReport, int _prmFgExport, int _prmFgCurr)
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
                _cmd.CommandText = "S_MKRptSOOutPerCust";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);

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

        public ReportDataSource OutstandingProFormaInvPerProduct(DateTime _prmDate, String _prmCustCode, String _prmProductCode, int _prmFgReport, int _prmFgExport, int _prmFgCurr)
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
                _cmd.CommandText = "S_MKRptSOOutPerProduct";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);

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

        public ReportDataSource DOPerTrans(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, int _prmFgReport, int _prmFgExport)
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
                _cmd.CommandText = "S_MKRptDOPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);

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

        public ReportDataSource DOPerCust(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, int _prmFgReport, int _prmFgExport)
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
                _cmd.CommandText = "S_MKRptDOPerCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);

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

        public ReportDataSource DOPerDelivery(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, int _prmFgReport, int _prmFgExport)
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
                _cmd.CommandText = "S_MKRptDOPerDelivery";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);

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

        public ReportDataSource DOPerProduct(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, int _prmFgReport, int _prmFgExport)
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
                _cmd.CommandText = "S_MKRptDOPerProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", "");
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);

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

        public ReportDataSource DOSummaryPerCust(String _prmStart, String _prmEnd, String _prmCustCode, String _prmProductSubGroup, int _prmFgExport)
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
                _cmd.CommandText = "S_MKRptDOMonthCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);

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

        public ReportDataSource DOSummaryPerProduct(String _prmStart, String _prmEnd, String _prmProductSubGroup, String _prmProductCode, int _prmFgReport, int _prmFgExport)
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
                _cmd.CommandText = "S_MKRptDOMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);

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

        public ReportDataSource OutstandingDOPerTrans(DateTime _prmDate, String _prmCustCode, String _prmProductCode, int _prmFgExport)
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
                _cmd.CommandText = "S_MKRptDOOutPerNmbr";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", 0);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);

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

        public ReportDataSource OutstandingDOPerCust(DateTime _prmDate, String _prmCustCode, String _prmProductCode, int _prmFgReport, int _prmFgExport)
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
                _cmd.CommandText = "S_MKRptDOOutPerCust";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);

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

        public ReportDataSource OutstandingDOPerProduct(DateTime _prmDate, String _prmCustCode, String _prmProductCode, int _prmFgReport, int _prmFgExport)
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
                _cmd.CommandText = "S_MKRptDOOutPerProduct";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);

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

        public ReportDataSource ClosingDO(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, String _prmProductCode, int _prmFgReport)
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
                _cmd.CommandText = "S_MKRptDOClosing";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
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

        public ReportDataSource ClosingProFormaInv(DateTime _prmStart, DateTime _prmEnd, String _prmCustCode, String _prmProductCode, int _prmFgReport, int _prmFgExport, int _prmFgCurr)
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
                _cmd.CommandText = "S_MKRptSOClosing";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", "");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgExport", _prmFgExport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);

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

        public ReportDataSource RetailPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "spSAL_RetailPrintPreview";
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

        public ReportDataSource NCPSalesPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "spSAL_NCPSalesPrintPreview";
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

        public ReportDataSource POSReport(DateTime _prmStartDate, DateTime _prmEndDate, String _prmFileNmbr, 
            String _prmCustCode, String _prmOrderBy, int _prmFgHeader)
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
                _cmd.CommandText = "spSAL_RptPOS";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);                
                _cmd.Parameters.AddWithValue("@FileNo", _prmFileNmbr);                
                _cmd.Parameters.AddWithValue("@CustCode", _prmCustCode);
                _cmd.Parameters.AddWithValue("@OrderBy", _prmOrderBy);
                _cmd.Parameters.AddWithValue("@FgHeader", _prmFgHeader);

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

        ~ReportSalesBL()
        {

        }
    }
}
