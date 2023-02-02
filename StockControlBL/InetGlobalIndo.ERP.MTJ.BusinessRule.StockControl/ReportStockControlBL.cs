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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class ReportStockControlBL : Base
    {
        private int _timeoutSec = 180;

        public ReportStockControlBL()
        {
        }

        public ReportDataSource SellingPriceList(string _prmProductSubGroup, string _prmProduct, string _prmStartProductName, string _prmEndProductName, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "spSTC_PriceList";
                _cmd.Parameters.AddWithValue("@PrmProductSubGroup", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@PrmProduct", _prmProduct);
                _cmd.Parameters.AddWithValue("@From", _prmStartProductName);
                _cmd.Parameters.AddWithValue("@To", _prmEndProductName);
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

        //public ReportDataSource SellingPriceList(string _prmStartProductName, string _prmEndProductName, string _prmStartCurr, string _prmEndCurr)
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
        //        _cmd.CommandTimeout = this._timeoutSec;
        //        _cmd.CommandText = "SpSTC_RptSellPrice";
        //        _cmd.Parameters.AddWithValue("@StartProductName", _prmStartProductName);
        //        _cmd.Parameters.AddWithValue("@EndProductName", _prmEndProductName);
        //        _cmd.Parameters.AddWithValue("@StartCurr", _prmStartCurr);
        //        _cmd.Parameters.AddWithValue("@EndCurr", _prmEndCurr);

        //        SqlDataAdapter _da = new SqlDataAdapter();

        //        _da.SelectCommand = _cmd;
        //        _da.Fill(_dataTable);


        //        _result.Value = _dataTable;
        //        _result.Name = "DataSet1";
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return _result;
        //}

        public ReportDataSource ProductListByPhone(string _prmStartProductName, string _prmEndProductName)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "SpSTC_RptProductList";
                _cmd.Parameters.AddWithValue("@Start", _prmStartProductName);
                _cmd.Parameters.AddWithValue("@End", _prmEndProductName);

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

        public ReportDataSource PriceList(string _prmStartCode, string _prmEndCode, string _prmStartPriceGroup, string _prmEndPriceGroup, string _prmStartPrice, string _prmEndPrice)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "SpSTC_RptPriceList";
                _cmd.Parameters.AddWithValue("@StartCode", _prmStartCode);
                _cmd.Parameters.AddWithValue("@EndCode", _prmEndCode);
                _cmd.Parameters.AddWithValue("@StartPG", _prmStartPriceGroup);
                _cmd.Parameters.AddWithValue("@EndPG", _prmEndPriceGroup);
                _cmd.Parameters.AddWithValue("@StartPrice", _prmStartPrice);
                _cmd.Parameters.AddWithValue("@EndPrice", _prmEndPrice);

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

        public ReportDataSource STAdjustPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STAdjustPrintPreview";
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

        public ReportDataSource StockReceivingReturPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "SpSTC_StockReceivingRetur";
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

        public ReportDataSource STBeginPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STBeginPrintPreview";
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

        public ReportDataSource STChangeGoodPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STChangePrintPreview";
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

        public ReportDataSource STRequestPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRequestPrintPreview";
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

        public ReportDataSource STRequestFAPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRequestFAPrintPreview";
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

        public ReportDataSource STIssueSlipPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STIssueSlipPrintPreview";
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

        public ReportDataSource STIssueToFAPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STIssueToFAPrintPreview";
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

        public ReportDataSource STOpnamePrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STOpnamePrintPreview";
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

        public ReportDataSource STSJPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STSJPrintPreview";
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

        public ReportDataSource ProductListing(string _prmFgReport, string _prmProductGroup, string _prmProductType)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptProductList";
                _cmd.Parameters.AddWithValue("@Str1", _prmProductType);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductGroup);
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

        public ReportDataSource BeginningStock(string _prmWrhsCode, string _prmProductCode, string _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockAwal";
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
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

        public ReportDataSource ReceivingPOPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRRPOPrintPreview";
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

        public ReportDataSource ProductAssemblyPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "spSTC_ProductAssemblyPrintPreview";
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

        public ReportDataSource SuratJalanPerTransaction(DateTime _prmStart, DateTime _prmEnd, string _prmWrhsCode, string _prmProductCode, string _prmFgReport, string _prmFgType, string _prmFgFilter, string _prmFromProduct, string _prmToProduct)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgType", Convert.ToInt32(_prmFgType));
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromProduct", _prmFromProduct);
                _cmd.Parameters.AddWithValue("@ToProduct", _prmToProduct);

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

        public ReportDataSource SuratJalanPerWarehouse(DateTime _prmStart, DateTime _prmEnd, string _prmWrhsCode, string _prmProductCode, string _prmFgReport, string _prmFgType, int _prmSelection, String _prmFrom, String _prmTo)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJPerWarehouse";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgType", Convert.ToInt32(_prmFgType));
                //_cmd.Parameters.AddWithValue("@FgSelection", _prmSelection);
                _cmd.Parameters.AddWithValue("@FgFilter", _prmSelection);
                _cmd.Parameters.AddWithValue("@FromProduct", _prmFrom);
                _cmd.Parameters.AddWithValue("@ToProduct", _prmTo);

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

        public ReportDataSource SuratJalanUnInvoicePerWarehouse(DateTime _prmStart, string _prmWrhsCode, string _prmProductCode, string _prmFgReport, string _prmFgType)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJUnInvoicePerWrhs";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));

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

        public ReportDataSource SuratJalanUnInvoicePerWarehouse(DateTime _prmStart, string _prmWrhsCode, string _prmProductCode, string _prmFgReport, string _prmFgType, String _prmFgFilter, String _prmFromProduct, String _prmToProduct)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJUnInvoicePerWrhs";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgType", Convert.ToInt32(_prmFgType));
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromProduct", _prmFromProduct);
                _cmd.Parameters.AddWithValue("@ToProduct", _prmToProduct);

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

        public ReportDataSource SuratJalanUnInvoicePerProduct(DateTime _prmStart, string _prmCustCode, string _prmWrhsCode, string _prmFgReport, string _prmFgType)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJUnInvoicePerProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgType", Convert.ToInt32(_prmFgType));

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

        public ReportDataSource SuratJalanUnInvoicePerProduct(DateTime _prmStart, string _prmCustCode, string _prmWrhsCode, string _prmFgReport, string _prmFgType, String _prmFgFilter, String _prmFromCustomer, String _prmToCustomer)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJUnInvoicePerProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgType", Convert.ToInt32(_prmFgType));
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromCustomer", _prmFromCustomer);
                _cmd.Parameters.AddWithValue("@ToCustomer", _prmToCustomer);

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

        public ReportDataSource SuratJalanPerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmWrhsCode, string _prmProductCode, string _prmFgReport, string _prmFgType, int _prmFilter, String _prmFrom, String _prmTo)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJPerProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgType", Convert.ToInt32(_prmFgType));
                _cmd.Parameters.AddWithValue("@FgFilter", _prmFilter);
                _cmd.Parameters.AddWithValue("@FromCustomer", _prmFrom);
                _cmd.Parameters.AddWithValue("@ToCustomer", _prmTo);

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

        public ReportDataSource SuratJalanUnInvoicePerTrans(DateTime _prmStart, string _prmWrhsCode, string _prmProductCode, string _prmFgReport, string _prmFgType)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJUnInvoicePerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgType", Convert.ToInt32(_prmFgType));

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

        public ReportDataSource SuratJalanUnInvoicePerTrans(DateTime _prmStart, string _prmWrhsCode, string _prmProductCode, string _prmFgReport, string _prmFgType, string _prmFgFilter, string _prmFromProduct, string _prmToProduct)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJUnInvoicePerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgType", Convert.ToInt32(_prmFgType));
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromProduct", _prmFromProduct);
                _cmd.Parameters.AddWithValue("@ToProduct", _prmToProduct);

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

        public ReportDataSource SuratJalanPerCustomer(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmProductCode, string _prmFgReport, string _prmFgType)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJPerCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgType", Convert.ToInt32(_prmFgType));

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

        public ReportDataSource SuratJalanPerCustomer(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmProductCode, string _prmFgReport, string _prmFgType, string _prmFgFilter, string _prmFromCustomer, string _prmToCustomer, string _prmFromProduct, string _prmToProduct)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJPerCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgType", Convert.ToInt32(_prmFgType));
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromCustomer", _prmFromCustomer);
                _cmd.Parameters.AddWithValue("@ToCustomer", _prmToCustomer);
                _cmd.Parameters.AddWithValue("@FromProduct", _prmFromProduct);
                _cmd.Parameters.AddWithValue("@ToProduct", _prmToProduct);

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

        public ReportDataSource SuratJalanUnInvoicePerCustomer(DateTime _prmStart, string _prmCustCode, string _prmProductCode, string _prmFgReport, string _prmFgType)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJUnInvoicePerCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgType", Convert.ToInt32(_prmFgType));

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

        public ReportDataSource SuratJalanUnInvoicePerCustomer(DateTime _prmStart, string _prmCustCode, string _prmProductCode, string _prmFgReport, string _prmFgType, string _prmFgFilter, string _prmFromCustomer, string _prmToCustomer, string _prmFromProduct, string _prmToProduct)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJUnInvoicePerCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgType", Convert.ToInt32(_prmFgType));
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromCustomer", _prmFromCustomer);
                _cmd.Parameters.AddWithValue("@ToCustomer", _prmToCustomer);
                _cmd.Parameters.AddWithValue("@FromProduct", _prmFromProduct);
                _cmd.Parameters.AddWithValue("@ToProduct", _prmToProduct);

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

        public ReportDataSource SuratJalanSummaryPerCustomer(string _prmStart, string _prmEnd, string _prmCustCode, string _prmProductCode, string _prmFgReport, int _prmFgAmount, string _prmFgFilter, string _prmFromProduct, string _prmToProduct, string _prmFromCustomer, string _prmToCustomer)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJMonthCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgAmount", _prmFgAmount);
                _cmd.Parameters.AddWithValue("@FgPriceType", 0);
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromProduct", _prmFromProduct);
                _cmd.Parameters.AddWithValue("@ToProduct", _prmToProduct);
                _cmd.Parameters.AddWithValue("@FromCustomer", _prmFromCustomer);
                _cmd.Parameters.AddWithValue("@ToCustomer", _prmToCustomer);

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

        public ReportDataSource SuratJalanSummaryPerCustomerByYear(string _prmStart, string _prmEnd, string _prmCustCode, string _prmProductCode, string _prmFgReport, string _prmFgFilter, string _prmFromProduct, string _prmToProduct, string _prmFromCustomer, string _prmToCustomer)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJYear";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                //_cmd.Parameters.AddWithValue("@FgAmount", _prmFgAmount);
                _cmd.Parameters.AddWithValue("@FgExport", 0);
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromProdSubGrp", _prmFromProduct);
                _cmd.Parameters.AddWithValue("@ToProdSubGrp", _prmToProduct);
                _cmd.Parameters.AddWithValue("@From", _prmFromCustomer);
                _cmd.Parameters.AddWithValue("@To", _prmToCustomer);

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

        public ReportDataSource SuratJalanSummaryPerProduct(string _prmStart, string _prmEnd, string _prmProductGroup, string _productType, string _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductGroup);
                _cmd.Parameters.AddWithValue("@Str2", _productType);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgAmount", 0);
                _cmd.Parameters.AddWithValue("@FgPriceType", 0);

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

        public ReportDataSource SuratJalanSummaryPerProduct(string _prmStart, string _prmEnd, string _prmProductGroup, string _productType, string _prmFgReport, string _prmFgFilter, string _prmFromProduct, string _prmToProduct)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSJMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductGroup);
                _cmd.Parameters.AddWithValue("@Str2", _productType);
                _cmd.Parameters.AddWithValue("@Str3", _string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgAmount", 0);
                _cmd.Parameters.AddWithValue("@FgPriceType", 0);
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromProduct", _prmFromProduct);
                _cmd.Parameters.AddWithValue("@ToProduct", _prmToProduct);

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

        public ReportDataSource StockUsage(DateTime _prmStart, string _prmProductSubGroup, string _prmWrhsCode, int _prmFgReport, int _prmFgDetail)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockUsage";
                _cmd.Parameters.AddWithValue("@Date", _prmStart);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@StrQty", "Qty");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgDetail", _prmFgDetail);
                _cmd.Parameters.AddWithValue("@FgDivide", 1);

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

        public ReportDataSource RRGoodsSummaryPerSupp(string _prmStart, string _prmEnd, string _prmSuppCode, string _prmProductSubGroup, string _prmFgReport, string _prmFgAmount)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptReceiveMonthSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgAmount", _prmFgAmount);
                _cmd.Parameters.AddWithValue("@FgType", "RR");

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

        public ReportDataSource RRGoodsSummaryPerSupp(string _prmStart, string _prmEnd, string _prmSuppCode, string _prmProductSubGroup, string _prmFgReport, string _prmFgAmount, string _prmFgFilter, string _prmFromSupplier, string _prmToSupplier)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptReceiveMonthSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgAmount", Convert.ToInt32(_prmFgAmount));
                _cmd.Parameters.AddWithValue("@FgType", "RR");
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromSupplier", _prmFromSupplier);
                _cmd.Parameters.AddWithValue("@ToSupplier", _prmToSupplier);

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

        public ReportDataSource RRGoodsSummaryPerSupp(string _prmStart, string _prmEnd, string _prmSuppCode, string _prmProductSubGroup, string _prmFgReport, string _prmFgFilter, string _prmFromSupplier, string _prmToSupplier)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptReceiveYearSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", Convert.ToInt32(_prmFgReport));
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromSupplier", _prmFromSupplier);
                _cmd.Parameters.AddWithValue("@ToSupplier", _prmToSupplier);

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

        public ReportDataSource RRReportListPerTransaction(DateTime _prmStart, DateTime _prmEnd, int _prmFgReport, string _prmSuppCode, string _prmFrom, string _prmTo)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "spSTC_RptRRReportListPerTransaction";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStart);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEnd);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@PrmSupplier", _prmSuppCode);
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

        public ReportDataSource RRReportListPerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmFrom, string _prmTo)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "spSTC_RptRRReportListPerProduct";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStart);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEnd);
                _cmd.Parameters.AddWithValue("@PrmProduct", _prmProductCode);
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

        public ReportDataSource ReportDateStockListPerProduct(string _prmProductCode, int _prmFgFilter, string _prmFrom, string _prmTo)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "Sp_DateStock";
                _cmd.Parameters.AddWithValue("@Str", _prmProductCode);
                _cmd.Parameters.AddWithValue("@FgFilter", _prmFgFilter);
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

        public ReportDataSource StockReceive(DateTime _prmStart, string _prmProductSubGroup, string _prmWrhsCode, int _prmFgReport, int _prmFgDetail)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockReceive";
                _cmd.Parameters.AddWithValue("@Date", _prmStart);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@StrQty", "Qty");
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgDetail", _prmFgDetail);
                _cmd.Parameters.AddWithValue("@FgDivide", 1);

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

        public ReportDataSource StockAvailable(DateTime _prmStart, string _prmProductSubGroup, string _prmProductType, int _prmFgReport, int _prmFgMin, int _prmFgMax)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockBalance";
                _cmd.Parameters.AddWithValue("@End", _prmStart);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductType);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgMin", _prmFgMin);
                _cmd.Parameters.AddWithValue("@FgMax", _prmFgMax);
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

        public ReportDataSource STCSalesReturPerTrans(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSReturPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource STCSalesReturPerCust(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSReturPerCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource STCSalesReturPerWrhs(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptSReturPerWrhs";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource StockBalance(DateTime _prmStart, string _prmProductSubGroup, string _prmWrhsCode, int _prmFgReport, char _prmFgQty, decimal _prmFgDivide)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockAvailable";
                _cmd.Parameters.AddWithValue("@End", _prmStart);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgQty", _prmFgQty);
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

        public ReportDataSource StockActivitiesMonthly(String _prmStart, String _prmEnd, string _prmProductType, string _prmProductSubGroup, int _prmFgReport, int _prmFgDetail, char _prmFgQty, decimal _prmFgDivide)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockActMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductType);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgDetail", _prmFgDetail);
                _cmd.Parameters.AddWithValue("@FgQty", _prmFgQty);
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

        public ReportDataSource StockActivitiesMonthly(String _prmStart, String _prmEnd, string _prmProductType, string _prmProductSubGroup, int _prmFgReport, int _prmFgDetail, char _prmFgQty, decimal _prmFgDivide, string _prmFgFilter, string _prmFromProduct, string _prmToProduct)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockActMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductType);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgDetail", _prmFgDetail);
                _cmd.Parameters.AddWithValue("@FgQty", _prmFgQty);
                _cmd.Parameters.AddWithValue("@FgDivide", _prmFgDivide);
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromProduct", _prmFromProduct);
                _cmd.Parameters.AddWithValue("@ToProduct", _prmToProduct);

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

        public ReportDataSource StockActivitiesListing(DateTime _prmStart, DateTime _prmEnd, string _prmProductType, string _prmProductSubGroup, int _prmFgReport, int _prmFgDetail, char _prmFgQty, decimal _prmFgDivide)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockActList";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductType);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgDetail", _prmFgDetail);
                _cmd.Parameters.AddWithValue("@FgQty", _prmFgQty);
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

        public ReportDataSource StockActivitiesListing(DateTime _prmStart, DateTime _prmEnd, string _prmProductType, string _prmProductSubGroup, int _prmFgReport, int _prmFgDetail, char _prmFgQty, decimal _prmFgDivide, string _prmFgFilter, string _prmFromProduct, string _prmToProduct)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockActList";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductType);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgDetail", _prmFgDetail);
                _cmd.Parameters.AddWithValue("@FgQty", _prmFgQty);
                _cmd.Parameters.AddWithValue("@FgDivide", _prmFgDivide);
                _cmd.Parameters.AddWithValue("@FgFilter", Convert.ToInt32(_prmFgFilter));
                _cmd.Parameters.AddWithValue("@FromProduct", _prmFromProduct);
                _cmd.Parameters.AddWithValue("@ToProduct", _prmToProduct);

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

        public ReportDataSource StockCardValue(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmWrhsCode, String _prmFrom, String _prmTo)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockCardValue";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStart);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
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

        public ReportDataSource StockCardValuePerDate(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmWrhsCode, String _prmFrom, String _prmTo)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockCardPerDate";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStart);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
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

        public ReportDataSource StockCardPerDate(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmWrhsCode, String _prmFrom, String _prmTo)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockCardPerDate";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStart);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
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

        public ReportDataSource StockCard(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmWrhsCode, String _prmFrom, String _prmTo)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockCard";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStart);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
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

        public ReportDataSource StockCardAnalysis(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmWrhsCode, String _prmTransTypeName, String _prmFrom, String _prmTo)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockCardAnalisa";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStart);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@From", _prmFrom);
                _cmd.Parameters.AddWithValue("@To", _prmTo);
                _cmd.Parameters.AddWithValue("@Trans1", _prmTransTypeName);

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

        public ReportDataSource StockOpnamePerTrans(DateTime _prmStart, DateTime _prmEnd, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource StockOpnamePerWrhs(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockPerWrhs";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource StockOpnamePerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockPerProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource StockOpnameSummaryPerProduct(String _prmStart, String _prmEnd, string _prmProductSubGroup, string _prmProductType, int _prmFgReport, int _prmFgAmount)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptOpnameMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductType);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgAmount", _prmFgAmount);

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

        public ReportDataSource StockAdjustPerTrans(DateTime _prmStart, DateTime _prmEnd, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptAdjustPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource StockAdjustPerWrhs(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptAdjustPerWrhs";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource StockAdjustPerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptAdjustPerProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource StockAdjustSummaryPerProduct(String _prmStart, String _prmEnd, string _prmProductSubGroup, string _prmProductType, int _prmFgReport, int _prmFgAmount)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptAdjustMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductType);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgAmount", _prmFgAmount);

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

        public ReportDataSource StockChangeGoodsPerTrans(DateTime _prmStart, DateTime _prmEnd, string _prmWrhsSrc, string _prmWrhsDest, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptChangeGoodPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsSrc);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsDest);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource StockChangeGoodsPerProductSrc(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptChangeGoodPerProdSrc";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource StockChangeGoodsPerProductDest(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptChangeGoodPerProdDest";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource StockChangeGoodsSummaryPerProductSrc(String _prmStart, String _prmEnd, string _prmProductSubGroup, string _prmProductType, int _prmFgReport, int _prmFgAmount)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptChangeProdSrcMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductType);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgAmount", _prmFgAmount);

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

        public ReportDataSource StockChangeGoodsSummaryPerProductDest(String _prmStart, String _prmEnd, string _prmProductSubGroup, string _prmProductType, int _prmFgReport, int _prmFgAmount)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptChangeProdDestMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductType);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgAmount", _prmFgAmount);

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

        public ReportDataSource StockMutation(DateTime _prmStart, DateTime _prmEnd, string _prmWrhsCode, string _prmWLocationCode, string _prmProductCode)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockMutation";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStart);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEnd);
                _cmd.Parameters.AddWithValue("@WrhsCode", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@ProductCode", _prmProductCode);
                _cmd.Parameters.AddWithValue("@LocationCode", _prmWLocationCode);

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

        public ReportDataSource ReceivingReportSupplier(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmWrhsCode)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "spSTC_RptReceivingReportSupplier";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStart);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);

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

        public ReportDataSource StockBalanceByProductValue(String _prmMode, DateTime _prmTransDate, String _prmProductCode, string _prmFrom, string _prmTo)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "S_STRptStockBalanceByProductValue";

                _cmd.Parameters.AddWithValue("@Mode", _prmMode);
                _cmd.Parameters.AddWithValue("@TransDate", _prmTransDate);
                _cmd.Parameters.AddWithValue("@PrmProduct", _prmProductCode);
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
                throw ex;
            }
            return _result;
        }

        public ReportDataSource STCTrDirectSJPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "spSTC_DirectSJPrintPreview";
                _cmd.Parameters.AddWithValue("@Nmbr", _prmTransNmbr);

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

        public ReportDataSource StockTransferInternalPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "SpSTC_TransferInternalPrintPreview";
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

        public ReportDataSource STTransRequestPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "SpSTC_TransferRequestPrintPreview";
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
        public ReportDataSource STTransDeliveryPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "SpSTC_TransferDeliveryPrintPreview";
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
        public ReportDataSource STTransReceivingPrintPreview (string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "SpSTC_TransferReceivingPrintPreview";
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
        public ReportDataSource STReceivingCustomerPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "SpSTC_ReceivingOtherPrintPreview";
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

        public ReportDataSource STServiceInPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "SpSTC_ServiceInPrintPreview";
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

        public ReportDataSource STServiceOutPrintPreview(string _prmTransNmbr)
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
                _cmd.CommandTimeout = this._timeoutSec;
                _cmd.CommandText = "SpSTC_ServiceOutPrintPreview";
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

        ~ReportStockControlBL()
        {

        }

    }
}
