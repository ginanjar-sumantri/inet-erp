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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing
{
    public sealed class ReportPurchaseBL : Base
    {
        public ReportPurchaseBL()
        {

        }

        public ReportDataSource PurchaseOrderPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "S_PRPOPrintPreview";
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

        public ReportDataSource FixedAssetPurchaseOrderPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "spPRC_FAPOPrintPreview";
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

        public ReportDataSource PurchaseRequestPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "spRpt_PRPrintPreview";
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

        public ReportDataSource PurchaseReturPrintPreview(String _prmTransNmbr)
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
                _cmd.CommandText = "S_PRReturPrintPreview";
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

        public ReportDataSource SupplierList(String _prmSuppGroup, String _prmSuppType)
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
                _cmd.CommandText = "S_PRRptSupplierList";
                _cmd.Parameters.AddWithValue("@StrGroup", _prmSuppGroup);
                _cmd.Parameters.AddWithValue("@StrType", _prmSuppType);

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

        public ReportDataSource PurchaseRequestPerTrans(DateTime _prmStartDate, DateTime _prmEndDate, String _prmOrgUnit, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptRequestPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmOrgUnit);
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

        public ReportDataSource PurchaseRequestPerProduct(DateTime _prmStartDate, DateTime _prmEndDate, String _prmProductSubGroup, String _prmProductCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptRequestPerProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);
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

        public ReportDataSource PurchaseRequestPerProductSubGroup(DateTime _prmStartDate, DateTime _prmEndDate, String _prmProductSubGroup, String _prmProductCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptRequestPerProductGrp";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);
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

        public ReportDataSource OutstandingPurchaseRequestPerTrans(DateTime _prmDate, String _prmProductSubGroup, String _prmProductCode)
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
                _cmd.CommandText = "S_PRRptMROutPerNmbr";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
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

        public ReportDataSource OutstandingPurchaseRequestPerProduct(DateTime _prmDate, String _prmProductSubGroup, String _prmProductCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptMROutPerProduct";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);
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

        public ReportDataSource OutstandingPurchaseRequestPerProductSubGroup(DateTime _prmDate, String _prmProductSubGroup, String _prmProductCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptMROutPerProductGrp";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);
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

        public ReportDataSource ClosingPurchaseRequest(DateTime _prmStartDate, DateTime _prmEndDate, String _prmProductCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptMRClosing";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductCode);
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

        public ReportDataSource ProgressRequestReceiveSummary(DateTime _prmStartDate, DateTime _prmEndDate, String _prmProductSubGroup, String _prmProductCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptProgressMRReceiveRkp";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
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

        public ReportDataSource ProgressRequestReceiveDetail(DateTime _prmStartDate, DateTime _prmEndDate, String _prmProductSubGroup, String _prmProductCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptProgressMRReceiveDtl";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroup);
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

        public ReportDataSource ProgressRequestInvoice(DateTime _prmStartDate, DateTime _prmEndDate, String _prmSuppCode)
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
                _cmd.CommandText = "S_PRRptProgressReqInv";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);

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

        public ReportDataSource ProgressRequestPayment(DateTime _prmStartDate, DateTime _prmEndDate, String _prmSuppCode)
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
                _cmd.CommandText = "S_PRRptProgressReqPay";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);

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

        public ReportDataSource PurchaseOrderPerTrans(DateTime _prmStartDate, DateTime _prmEndDate, String _prmSuppCode, int _prmFgReport, int _prmFgCurr, int _prmFgFilter, String _prmFrom, String _prmTo)
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
                _cmd.CommandText = "S_PRRptPOPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgFilter", _prmFgFilter);
                _cmd.Parameters.AddWithValue("@FromSupplier", _prmFrom);
                _cmd.Parameters.AddWithValue("@ToSupplier", _prmTo);

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

        public ReportDataSource PurchaseOrderPerSupp(DateTime _prmStartDate, DateTime _prmEndDate, String _prmSuppCode, int _prmFgReport, int _prmFgCurr, int _prmFgFilter, String _prmFrom, String _prmTo)
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
                _cmd.CommandText = "S_PRRptPOPerSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgFilter", _prmFgFilter);
                _cmd.Parameters.AddWithValue("@FromSupplier", _prmFrom);
                _cmd.Parameters.AddWithValue("@ToSupplier", _prmTo);

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

        public ReportDataSource PurchaseOrderPerProduct(DateTime _prmStartDate, DateTime _prmEndDate, String _prmSuppCode, int _prmFgReport, int _prmFgCurr, int _prmFgFilter, String _prmFromSupplier, String _prmToSupplier, String _prmFromProduct, String _prmToProduct)
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
                _cmd.CommandText = "S_PRRptPOPerProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgFilter", _prmFgFilter);
                _cmd.Parameters.AddWithValue("@FromSupplier", _prmFromSupplier);
                _cmd.Parameters.AddWithValue("@ToSupplier", _prmToSupplier);
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

        public ReportDataSource PurchaseOrderSummaryPerProduct(String _prmStartDate, String _prmEndDate, String _prmProductSubGroupCode, String _prmSuppCode, int _prmFgCurr, int _prmFgReport, int _prmFilter, String _prmFromProduct, String _prmToProduct, String _prmFromSuppCode, String _prmToSuppCode)
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
                _cmd.CommandText = "S_PRRptPOMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroupCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str3", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@Str4", this._string);
                _cmd.Parameters.AddWithValue("@Str5", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgFilter", _prmFilter);
                _cmd.Parameters.AddWithValue("@FromProduct", _prmFromProduct);
                _cmd.Parameters.AddWithValue("@ToProduct", _prmToProduct);
                _cmd.Parameters.AddWithValue("@FromSuppCode", _prmFromSuppCode);
                _cmd.Parameters.AddWithValue("@ToSuppCode", _prmToSuppCode);

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

        public ReportDataSource PurchaseOrderSummaryPerSupp(String _prmStartDate, String _prmEndDate, String _prmSuppCode, int _prmFgCurr)
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
                _cmd.CommandText = "S_PRRptPOMonthSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", _prmFgCurr);

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

        public ReportDataSource PurchaseOrderSummaryPerSupp(String _prmStartDate, String _prmEndDate, String _prmSuppCode, int _prmFgCurr, string _prmFgFilter, string _prmFromSupplier, string _prmToSupplier)
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
                _cmd.CommandText = "S_PRRptPOMonthSuppMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@Str4", "");
                _cmd.Parameters.AddWithValue("@Str5", "");
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

        public ReportDataSource PurchaseOrderSummaryPerSuppYear(String _prmStartDate, String _prmEndDate, String _prmSuppCode, int _prmFgCurr, int _prmFgReport, string _prmFgFilter, string _prmFromSupplier, string _prmToSupplier)
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
                _cmd.CommandText = "S_PRRptPOMonthSuppYear";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", _prmFgCurr);
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

        public ReportDataSource OutstandingPurchaseOrderPerTrans(DateTime _prmDate, String _prmSuppCode, String _prmProductCode)
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
                _cmd.CommandText = "S_PRRptPOOutPerNmbr";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", "");

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

        public ReportDataSource OutstandingPurchaseOrderPerSupp(DateTime _prmDate, String _prmSuppCode, String _prmProductCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptPOOutPerSupp";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
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

        public ReportDataSource OutstandingPurchaseOrderPerProduct(DateTime _prmDate, String _prmSuppCode, String _prmProductCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptPOOutPerProduct";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
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

        public ReportDataSource ClosingPurchaseOrder(DateTime _prmStartDate, DateTime _prmEndDate, String _prmSuppCode, String _prmProductCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptPOClosing";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
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

        public ReportDataSource PurchaseCostPerTrans(DateTime _prmStartDate, DateTime _prmEndDate, String _prmSuppCode, String _prmCurrCode, int _prmFgReport, int _prmFgCurr)
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
                _cmd.CommandText = "S_PRRptPOCostPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource PurchaseCostPerSupp(DateTime _prmStartDate, DateTime _prmEndDate, String _prmSuppCode, String _prmCurrCode, int _prmFgReport, int _prmFgCurr)
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
                _cmd.CommandText = "S_PRRptPOCostPerSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
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

        public ReportDataSource PurchaseCostSummaryPerSupp(String _prmStartDate, String _prmEndDate, String _prmSuppCode, int _prmFgCurr)
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
                _cmd.CommandText = "S_PRRptPOCostMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgCurr);

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

        public ReportDataSource PurchaseReturPerTrans(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptPReturPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@Str4", this._string);
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

        public ReportDataSource PurchaseReturPerSupp(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptPReturPerSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@Str4", this._string);
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

        public ReportDataSource PurchaseReturPerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptPReturPerProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@Str4", this._string);
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

        public ReportDataSource PurchaseReturPerWrhs(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmWrhsCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptPReturPerWrhs";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@Str4", this._string);
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

        public ReportDataSource PurchaseReturSummaryPerProduct(String _prmStartDate, String _prmEndDate, String _prmProductType, String _prmProductSubGroupCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptPReturMonthProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductType);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroupCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgAmount", 0);

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

        public ReportDataSource PurchaseReturSummaryPerSupp(String _prmStartDate, String _prmEndDate, String _prmProductSubGroupCode, String _prmSuppCode, int _prmFgReport)
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
                _cmd.CommandText = "S_PRRptPReturMonthSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductSubGroupCode);
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

        public ReportDataSource DirectPurchaseReportListPerTransaction(DateTime _prmStart, DateTime _prmEnd, int _prmFgReport, string _prmSuppCode, string _prmFrom, string _prmTo)
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
                _cmd.CommandText = "spSTC_RptDirectPurchaseReportListPerTransaction";
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

        public ReportDataSource DirectPurchaseReportListPerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode, string _prmFrom, string _prmTo)
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
                _cmd.CommandText = "spSTC_RptDirectPurchaseReportListPerProduct";
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

        public ReportDataSource DirectPurchaseReportListPerSupplier(DateTime _prmStart, DateTime _prmEnd, int _prmFgReport, string _prmSuppCode, string _prmFrom, string _prmTo)
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
                _cmd.CommandText = "spSTC_RptDirectPurchaseReportListPerSupplier";
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

        ~ReportPurchaseBL()
        {

        }
    }
}
