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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class ReportFinanceBL : Base
    {
        public ReportFinanceBL()
        {

        }

        public ReportDataSource PettyCashPrintPreview(string _prmTransNmbr, string _prmRemark, string _prmUser)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNCashPrintPreview";
                _cmd.Parameters.AddWithValue("@TransNmbr", _prmTransNmbr);
                _cmd.Parameters.AddWithValue("@Remark", _prmRemark);
                _cmd.Parameters.AddWithValue("@User", _prmUser);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PettyCashReceivePrintPreview(string _prmTransNmbr, string _prmRemark, string _prmUser)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNCashReceivePrintPreview";
                _cmd.Parameters.AddWithValue("@TransNmbr", _prmTransNmbr);
                _cmd.Parameters.AddWithValue("@Remark", _prmRemark);
                _cmd.Parameters.AddWithValue("@User", _prmUser);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource FNDPSuppReturPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNDPSuppReturPrintPreview";
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

        public ReportDataSource FNDPSuppPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNDPSuppPrintPreview";
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

        public ReportDataSource FNDPCustReturPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNDPCustReturPrintPreview";
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

        public ReportDataSource FNDPCustPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNDPCustPrintPreview";
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

        public ReportDataSource DebitNoteSupplierPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNSIDNPrintPreview";
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

        public ReportDataSource DifferenceRateAPPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNSIRatePrintPreview";
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

        public ReportDataSource DifferenceRateAPRrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNCIRatePrintPreview";
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

        public ReportDataSource DebitNoteCustomerPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNCIDNPrintPreview";
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

        public ReportDataSource PaymentAPPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNPayTradePrintPreview";
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

        public ReportDataSource PaymentNonAPPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNPayNTPrintPreview";
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

        public ReportDataSource ReceiptNonARPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNReceiptNTPrintPreview";
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

        public ReportDataSource CreditNoteSupplierPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNSuppInvConsignmentPrintPreview";
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

        public ReportDataSource CreditNoteCustomerPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNCICNPrintPreview";
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

        public ReportDataSource FNChangeGiroInPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNChangeGiroINPrintPreview";
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

        public ReportDataSource FNChangeGiroOutPrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNChangeGiroOutPrintPreview";
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

        public ReportDataSource ReceiptTradeReceivePrintPreview(string _prmTransNmbr, string _prmUser)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNReceiptTradePrintPreview";
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

        public ReportDataSource SupplierInvoicePrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNSIRRPrintPreview";
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

        public ReportDataSource GiroCustListReceiptDate(DateTime _prmStartDate, DateTime _prmEndDate, string _prmStr0, string _prmStr1, int _prmFgReport) //, int _prmPeriod
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInListReceiptDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr0);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr1);
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

        public ReportDataSource GiroCustListCustomer(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInListCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroCustListBankGiro(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInListBankGiro";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroCustODCustomer(DateTime _prmDate, string _prmCustCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInODCust";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource CashAndBankSummary(DateTime _prmStartDate, DateTime _prmEndDate, string _prmAccount, int _prmOrder)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_RptCashAndBankSummary";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@PrmAccount", _prmAccount);
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

        public ReportDataSource GiroCustODBankGiro(DateTime _prmDate, string _prmCustCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInODBankGiro";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroCustOutstanding(DateTime _prmDate, string _prmCustCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInOut";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroSuppOutstanding(DateTime _prmDate, string _prmSuppCode, string _prmBankPayment, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutOut";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankPayment);
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

        public ReportDataSource APListing(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmSuppCode, int _prmFgCurr, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptAPListing";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
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

        public ReportDataSource ARListing(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmCustCode, int _prmFgCurr, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptARListing";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
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

        public ReportDataSource IncomeGraph(Int32 _prmYear, String _prmCustGroup, string _prmCustType, string _prmCurrency)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptARAnalysisReceiptPerYear";
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@CustGroup", _prmCustGroup);
                _cmd.Parameters.AddWithValue("@CustType", _prmCustType);
                _cmd.Parameters.AddWithValue("@Currency", _prmCurrency);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ReceiptPerCustomer(DateTime _prmStart, DateTime _prmEnd, string _prmReceiptType, string _prmCustCode, int _prmFgType, int _prmFgReport, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptReceiptPerCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmReceiptType);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@CurrType", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource DPCustomerIssue(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, int _prmFgReport, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptDPCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource DPCustomerOutstanding(DateTime _prmPeriod, string _prmCustCode, int _prmFgReport, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptDPCustOut";
                _cmd.Parameters.AddWithValue("@Date", _prmPeriod);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ReceiptPerReceiptType(DateTime _prmStart, DateTime _prmEnd, string _prmReceiptType, string _prmCustCode, int _prmFgType, int _prmFgReport, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptReceiptPerReceiptType";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmReceiptType);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@CurrType", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource DPCustomerInvPay(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, int _prmFgReport, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPayCustDPPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource DPCustomerReturperTrans(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, int _prmFgReport, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptReturDPCustPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource DPCustomerReturperCust(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, int _prmFgReport, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptReturDPCustPerCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ReceiptPerDate(DateTime _prmStart, DateTime _prmEnd, string _prmReceiptType, string _prmCustCode, int _prmFgType, int _prmFgReport, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptReceiptPerDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmReceiptType);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@CurrType", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ReceiptPerTransaction(DateTime _prmStart, DateTime _prmEnd, string _prmReceiptType, string _prmCustCode, int _prmFgReport, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptReceiptPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmReceiptType);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@CurrType", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource CashFlowForecast(DateTime _prmDate)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptCashForeCast";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource CashFlowCashSummary(DateTime _prmStart, DateTime _prmEnd, string _prmAccount, string _prmFrom, string _prmTo)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptCashSummary2";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmAccount);
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

        public ReportDataSource CashFlowCashDetail(DateTime _prmStart, DateTime _prmEnd, string _prmAccount, string _prmFrom, string _prmTo)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptCashDetail";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmAccount);
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

        public ReportDataSource ReceiptPerInvoice(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptReceiptInvoice";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgDetail", _prmFgReport);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource APAging(DateTime _prmDate, string _prmCurrCode, string _prmSuppCode, int _prmPeriod1, int _prmPeriod2, int _prmPeriod3, int _prmFgCurr, int _prmFgReport, int _prmFgBack)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptAPAging";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@ID1", _prmPeriod1);
                _cmd.Parameters.AddWithValue("@ID2", _prmPeriod2);
                _cmd.Parameters.AddWithValue("@ID3", _prmPeriod3);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgBack", _prmFgBack);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ARAging(DateTime _prmDate, string _prmCurrCode, string _prmCustCode, int _prmPeriod1, int _prmPeriod2, int _prmPeriod3, int _prmFgCurr, int _prmFgReport, int _prmFgBack)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptARAging";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@ID1", _prmPeriod1);
                _cmd.Parameters.AddWithValue("@ID2", _prmPeriod2);
                _cmd.Parameters.AddWithValue("@ID3", _prmPeriod3);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgBack", _prmFgBack);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource GiroCustAging(DateTime _prmDate, string _prmBankCode, string _prmCustCode, int _prmPeriod1, int _prmPeriod2, int _prmPeriod3, int _prmGroupBy, int _prmFgBack)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInAging";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankCode);
                _cmd.Parameters.AddWithValue("@ID1", _prmPeriod1);
                _cmd.Parameters.AddWithValue("@ID2", _prmPeriod2);
                _cmd.Parameters.AddWithValue("@ID3", _prmPeriod3);
                _cmd.Parameters.AddWithValue("@GroupBy", _prmGroupBy);
                _cmd.Parameters.AddWithValue("@FgBack", _prmFgBack);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource GiroCustListDueDate(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInListDueDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroCustODDueDate(DateTime _prmDate, string _prmCustCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInODDueDate";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroCustTolakBankGiro(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInTolakBankGiro";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroCustTolakBankSetor(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInTolakBankSetor";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroCustTolakCust(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInTolakCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroCustTolakDate(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInTolakDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroSupplierCairDate(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutCairDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroCustomerPerCairDate(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInCairDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroCustomerPerBankSetor(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInCairBankSetor";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroCustomerPerBankGiro(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInCairBankGiro";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroCustomerPerCustomer(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroInCairCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroBankPaymentCairDate(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutCairBankGiro";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroSupplierPerSupplier(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankGiro, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutCairSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankGiro);
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

        public ReportDataSource GiroSuppAging(DateTime _prmDate, string _prmSuppCode, string _prmBankCode, int _prmPeriod1, int _prmPeriod2, int _prmPeriod3, int _prmGroupBy, int _prmFgBack)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutAging";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankCode);
                _cmd.Parameters.AddWithValue("@1", _prmPeriod1);
                _cmd.Parameters.AddWithValue("@2", _prmPeriod2);
                _cmd.Parameters.AddWithValue("@3", _prmPeriod3);
                _cmd.Parameters.AddWithValue("@GroupBy", _prmGroupBy);
                _cmd.Parameters.AddWithValue("@FgBack", _prmFgBack);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource GiroSuppTolakDate(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankPayment, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutTolakDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankPayment);
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

        public ReportDataSource GiroSuppTolakSupp(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankPayment, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutTolakSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankPayment);
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

        public ReportDataSource GiroSuppTolakBankPayment(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankPayment, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutTolakBankGiro";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankPayment);
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

        public ReportDataSource GiroSuppODDueDate(DateTime _prmDate, string _prmSuppCode, string _prmBankPayment, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutODDueDate";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankPayment);
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

        public ReportDataSource GiroSuppODSupplier(DateTime _prmDate, string _prmSuppCode, string _prmBankPayment, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutODSupp";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankPayment);
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

        public ReportDataSource GiroSuppODBankGiro(DateTime _prmDate, string _prmSuppCode, string _prmBankPayment, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutODBankGiro";
                _cmd.Parameters.AddWithValue("@Date", _prmDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankPayment);
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

        public ReportDataSource GiroSuppListBankPayment(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankPayment, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutListBankGiro";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankPayment);
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

        public ReportDataSource GiroSuppListDueDate(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankPayment, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutListDueDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankPayment);
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

        public ReportDataSource GiroSuppListPaymentDate(DateTime _prmStartDate, DateTime _prmEndDate, string _prmStr0, string _prmStr1, int _prmFgReport) //, int _prmPeriod
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutListPaymentDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr0);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr1);
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

        public ReportDataSource GiroSuppListSupplier(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmBankPayment, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptGiroOutListSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmBankPayment);
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

        public ReportDataSource SupplierNotePrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNSIRRPrintPreview";
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

        public ReportDataSource CustomerNotePrintPreview(string _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spFIN_CustomerNotePrintPreview";
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

        public ReportDataSource ARAnalysis(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmCustCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptReceiptAnalysis";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrCode);
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

        public ReportDataSource PPNListingSales(DateTime _prmStartDate, DateTime _prmEndDate, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPPNSales";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", this._string);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
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

        public ReportDataSource PPNListingPurchase(DateTime _prmStartDate, DateTime _prmEndDate, int _prmFgType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPPNPurchase";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", this._string);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
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

        public ReportDataSource ARPaidFull(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmCustCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptARPaidFull";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrCode);
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

        public ReportDataSource APPaidFull(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmSuppCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptAPPaidFull";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@GroupBy", 0);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource SupplierHistory(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmGroupBy)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSuppHistory";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmGroupBy);
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

        public ReportDataSource ARStatementOfAccount(Int32 _prmYear, Int32 _prmPeriod, string _prmCustCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptStateAccount";
                _cmd.Parameters.AddWithValue("@Year", _prmYear);
                _cmd.Parameters.AddWithValue("@Period", _prmPeriod);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
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

        public ReportDataSource PettyCashPerTransaction(DateTime _prmStartDate, DateTime _prmEndDate, string _prmPettyType, string _prmCurrency, string _prmGroupBy, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPettyPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmPettyType);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrency);
                _cmd.Parameters.AddWithValue("@Str3", _prmGroupBy);
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

        public ReportDataSource PettyCashPerAccount(DateTime _prmStartDate, DateTime _prmEndDate, string _prmPettyType, string _prmCurrency, string _prmGroupBy, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPettyPerAcc";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmPettyType);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrency);
                _cmd.Parameters.AddWithValue("@Str3", _prmGroupBy);
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

        public ReportDataSource PettyCashPerSummary(string _prmStart, string _prmEnd, string _prmPettyType, string _prmCurrency, string _prmDivideBy, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPettyMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmPettyType);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrency);
                _cmd.Parameters.AddWithValue("@Str3", _prmDivideBy);
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

        public ReportDataSource PurchaseInvoicePerTrans(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmSuppCode, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPurchasePerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrCode);
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

        public ReportDataSource CustomerHistory(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmGroupBy)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptCustHistory";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmGroupBy);
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

        public ReportDataSource SalesInvoicePerTrans(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmCustCode, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustCode);
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

        public ReportDataSource SalesInvoicePerTrans(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmCustCode, int _prmFgReport, string _prmFgFilter, string _prmFromCustomer, string _prmToCustomer, string _prmFromProduct, string _prmToProduct)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
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

        public ReportDataSource SalesInvoicePerCustomer(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmCustCode, int _prmFgReport, string _prmGroupBy)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesPerCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str3", _prmGroupBy);
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

        public ReportDataSource SalesInvoicePerCustomer(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmCustCode, int _prmFgReport, string _prmGroupBy, string _prmFgFilter, string _prmFromCustomer, string _prmToCustomer)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesPerCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str3", _prmGroupBy);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
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

        public ReportDataSource SalesInvoicePerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmProductCode, int _prmFgReport, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesPerProd";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
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

        public ReportDataSource SalesInvoicePerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmProductCode, int _prmFgReport, int _prmFgCurr, string _prmFgFilter, string _prmFromProduct, string _prmToProduct)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesPerProd";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmProductCode);
                _cmd.Parameters.AddWithValue("@Str3", this._string);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
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

        public ReportDataSource SalesInvoiceTopSalesPerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmProductSubGroup, string _prmRecord, int _prmOrder)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptTopSales";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@iTop", _prmRecord);
                _cmd.Parameters.AddWithValue("@Order", _prmOrder);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource SalesInvoiceTopSalesPerCustomer(DateTime _prmStart, DateTime _prmEnd, string _prmProductSubGroup, string _prmRecord)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptTopSalesCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@iTop", _prmRecord);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource SalesInvoiceBottomSalesPerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmProductSubGroup, string _prmRecord, int _prmOrder)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptBottomSales";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@iTop", _prmRecord);
                _cmd.Parameters.AddWithValue("@Order", _prmOrder);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource SalesInvoiceBottomSalesPerCustomer(DateTime _prmStart, DateTime _prmEnd, string _prmProductSubGroup, string _prmRecord)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptBottomSalesCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@iTop", _prmRecord);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource SalesInvoiceSummaryPerCustomer(string _prmStart, string _prmEnd, string _prmCustode, string _prmDivideBy, int _prmFgCurr, int _prmFgFilter, String _prmFrom, String _prmTo)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesInvMonthCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", this._string);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustode);
                _cmd.Parameters.AddWithValue("@Str3", _prmDivideBy);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgFilter", _prmFgFilter);
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

        public ReportDataSource SalesInvoiceSummaryPerCustomerByMonth(string _prmStart, string _prmEnd, string _prmCustode, string _prmDivideBy, int _prmFgCurr, int _prmFgFilter, String _prmFrom, String _prmTo, String _prmFromProd, String _prmToProd)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesInvMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", this._string);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustode);
                _cmd.Parameters.AddWithValue("@Str3", _prmDivideBy);
                _cmd.Parameters.AddWithValue("@FgReport", 4);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgFilter", _prmFgFilter);
                _cmd.Parameters.AddWithValue("@FromCustomer", _prmFrom);
                _cmd.Parameters.AddWithValue("@ToCustomer", _prmTo);
                _cmd.Parameters.AddWithValue("@FromProduct", _prmFromProd);
                _cmd.Parameters.AddWithValue("@ToProduct", _prmToProd);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource SalesInvoiceSummaryPerCustomerByYear(string _prmStart, string _prmEnd, string _prmCustode, string _prmDivideBy, int _prmFgCurr, int _prmFgFilter, String _prmFrom, String _prmTo)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesInvYear";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", this._string);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustode);
                _cmd.Parameters.AddWithValue("@Str3", _prmDivideBy);
                _cmd.Parameters.AddWithValue("@FgReport", 0);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmFgCurr);
                _cmd.Parameters.AddWithValue("@FgFilter", _prmFgFilter);
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

        public ReportDataSource SalesInvoiceSummaryPerProduct(string _prmStart, string _prmEnd, string _prmProdSubGrp, string _prmCustCode, string _prmDivideBy, int _prmFgCurr, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesInvMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProdSubGrp);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str3", _prmDivideBy);
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

        public ReportDataSource SalesInvoiceSalesPriceHistory(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptCustPriceHistory";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductCode);
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

        public ReportDataSource SalesReturPerTrans(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmWrhsCode, int _prmFgReport, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesRetur";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
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

        public ReportDataSource SalesReturPerCust(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmWrhsCode, int _prmFgReport, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesReturCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
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

        public ReportDataSource SalesReturPerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmCustCode, string _prmWrhsCode, int _prmFgReport, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesReturProd";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
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

        public ReportDataSource SalesReturSummaryPerCustomer(string _prmStart, string _prmEnd, string _prmCustCode, string _prmDivideBy, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesReturMonthCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", _prmDivideBy);
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

        public ReportDataSource SalesReturSummaryPerProduct(string _prmStart, string _prmEnd, string _prmProdSubGrp, string _prmCustCode, string _prmDivideBy, int _prmFgCurr, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesReturMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProdSubGrp);
                _cmd.Parameters.AddWithValue("@Str2", _prmCustCode);
                _cmd.Parameters.AddWithValue("@Str3", _prmDivideBy);
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

        public ReportDataSource PurchaseInvoicePerSupp(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmSuppCode, int _prmFgReport, string _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPurchasePerSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmCurrCode);
                _cmd.Parameters.AddWithValue("@Str3", _prmFgCurr);
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

        public ReportDataSource PurchaseInvoicePerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmCurrCode, string _prmProductCode, int _prmFgReport, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPurchasePerProd";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductCode);
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

        public ReportDataSource PurchaseInvoiceTopPurchasePerSupplier(DateTime _prmStart, DateTime _prmEnd, string _prmProductSubGroup, string _prmRecord)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptTopPurcSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@iTop", _prmRecord);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PurchaseInvoiceTopPurchasePerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmProductSubGroup, string _prmRecord, int _prmOrder)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptTopPurcProd";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@iTop", _prmRecord);
                _cmd.Parameters.AddWithValue("@Order", _prmOrder);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PurchaseInvoiceBottomPurchasePerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmProductSubGroup, string _prmRecord, int _prmOrder)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptBottomPurcProd";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@iTop", _prmRecord);
                _cmd.Parameters.AddWithValue("@Order", _prmOrder);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PurchaseInvoiceBottomPurchasePerSupplier(DateTime _prmStart, DateTime _prmEnd, string _prmProductSubGroup, string _prmRecord)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptBottomPurcSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@iTop", _prmRecord);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductSubGroup);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PurchaseInvoicePurchasePriceHistory(DateTime _prmStart, DateTime _prmEnd, string _prmProductCode)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSuppPriceHistory";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProductCode);
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

        public ReportDataSource PurchaseReturPerTrans(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmWrhsCode, int _prmFgReport, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPurchaseRetur";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
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

        public ReportDataSource PurchaseReturPerProduct(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmWrhsCode, int _prmFgReport, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPurchaseReturProd";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
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

        public ReportDataSource PurchaseReturPerSupp(DateTime _prmStart, DateTime _prmEnd, string _prmSuppCode, string _prmWrhsCode, int _prmFgReport, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPurchaseReturSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", _prmWrhsCode);
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

        public ReportDataSource PaymentInvoice(DateTime _prmStart, DateTime _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgDetail)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPaymentInvoice";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
                _cmd.Parameters.AddWithValue("@FgDetail", _prmFgDetail);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PaymentPerSupplier(DateTime _prmStart, DateTime _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgReport, int _prmFgType, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPaymentPerSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);
                _cmd.Parameters.AddWithValue("@CurrType", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PaymentPerDate(DateTime _prmStart, DateTime _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgReport, int _prmFgType, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPaymentPerDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);
                _cmd.Parameters.AddWithValue("@CurrType", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PaymentPerPaymentType(DateTime _prmStart, DateTime _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgReport, int _prmFgType, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPaymentPerPayType";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);
                _cmd.Parameters.AddWithValue("@CurrType", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PaymentSummaryPerSupplier(string _prmStart, string _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgReport, int _prmFgType, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPaymentSumSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PaymentSummaryPerPaymentType(string _prmStart, string _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgReport, int _prmFgType, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPaymentSum";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource ReceiptSummaryPerCustomer(string _prmStart, string _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgReport, int _prmFgType, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptReceiptSumCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }


        public ReportDataSource ReceiptSummaryPerReceiptType(string _prmStart, string _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgReport, int _prmFgType, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptReceiptSum";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@FgType", _prmFgType);
                _cmd.Parameters.AddWithValue("@FgCurr", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }


        public ReportDataSource SalesProfitSumPerProductGroup(string _prmStart, string _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesProfitMonthProductGrp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _string);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource SalesProfitSumPerCustomer(string _prmStart, string _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesProfitMonthCust";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _string);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }


        public ReportDataSource PaymentPerTransaction(DateTime _prmStart, DateTime _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgReport, int _prmCurrType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPaymentPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@CurrType", _prmCurrType);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PurchaseInvoiceSummaryPerSupplier(string _prmStart, string _prmEnd, string _prmSuppCode, string _prmDivideBy, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPurchaseInvMonthSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", _prmDivideBy);
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

        public ReportDataSource PurchaseInvoiceSummaryPerProduct(string _prmStart, string _prmEnd, string _prmProdSubGrp, string _prmSuppCode, string _prmDivideBy, int _prmFgCurr, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPurchaseInvMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProdSubGrp);
                _cmd.Parameters.AddWithValue("@Str2", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str3", _prmDivideBy);
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

        public ReportDataSource PurchaseReturSummaryPerSupplier(string _prmStart, string _prmEnd, string _prmSuppCode, string _prmDivideBy, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPurchaseReturMonthSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str2", this._string);
                _cmd.Parameters.AddWithValue("@Str3", _prmDivideBy);
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

        public ReportDataSource PurchaseReturSummaryPerProduct(string _prmStart, string _prmEnd, string _prmProdSubGrp, string _prmSuppCode, string _prmDivideBy, int _prmFgCurr, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPurchaseReturMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmProdSubGrp);
                _cmd.Parameters.AddWithValue("@Str2", _prmSuppCode);
                _cmd.Parameters.AddWithValue("@Str3", _prmDivideBy);
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

        public ReportDataSource DifferenceRateSupplierPerTransaction(DateTime _prmStartDate, DateTime _prmEndDate, string _prmStr1, string _prmStr2, string _prmStr3)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptAPRatePerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource DifferenceRateCustomerPerDate(DateTime _prmStartDate, DateTime _prmEndDate, string _prmStr1, string _prmStr2, string _prmStr3)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptARRatePerDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource DifferenceRateCustomerPerTransaction(DateTime _prmStartDate, DateTime _prmEndDate, string _prmStr1, string _prmStr2, string _prmStr3)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptARRatePerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource DifferenceRateSupplierPerDate(DateTime _prmStartDate, DateTime _prmEndDate, string _prmStr1, string _prmStr2, string _prmStr3)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptAPRatePerDate";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource DifferenceRateSupplierHistory(DateTime _prmStartDate, DateTime _prmEndDate, string _prmStr1, string _prmStr2, string _prmStr3)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptAPHisInvRate";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource DifferenceRateCustHistory(DateTime _prmStartDate, DateTime _prmEndDate, string _prmStr1, string _prmStr2, string _prmStr3)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptARHisInvRate";
                _cmd.Parameters.AddWithValue("@Start", _prmStartDate);
                _cmd.Parameters.AddWithValue("@End", _prmEndDate);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PaymentAndReceiptAnalysisSummary(DateTime _prmStart, DateTime _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmGroupBy)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPayReceiptSum";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
                _cmd.Parameters.AddWithValue("@GroupBy", _prmGroupBy);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PaymentAndReceiptAnalysisDetail(DateTime _prmStart, DateTime _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmGroupBy)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPayReceiptDtl";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
                _cmd.Parameters.AddWithValue("@GroupBy", _prmGroupBy);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }

            catch (Exception ex)
            {

            }

            return _result;
        }

        public ReportDataSource PaymentAndReceiptAnalysisMonthly(string _prmStart, string _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPayReceiptMonth";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
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

        public ReportDataSource DPSupplierPayInvoicePerTransaction(DateTime _prmStart, DateTime _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgReport, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptPaySuppDPPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
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

        public ReportDataSource DPSupplierReturPerTransaction(DateTime _prmStart, DateTime _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgReport, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptReturDPSuppPerNmbr";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
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

        public ReportDataSource DPSupplierReturPerSupplier(DateTime _prmStart, DateTime _prmEnd, string _prmStr1, string _prmStr2, string _prmStr3, int _prmFgReport, int _prmFgCurr)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptReturDPSuppPerSupp";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
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

        public ReportDataSource SalesProfitSummaryPerProduct(DateTime _prmStart, DateTime _prmEnd, String _prmStr1, String _prmStr2, String _prmStr3, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesProfitProduct";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
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

        public ReportDataSource SalesProfitDetailPerTransaction(DateTime _prmStart, DateTime _prmEnd, String _prmStr1, String _prmStr2, String _prmStr3, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesProfit";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
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

        public ReportDataSource SalesProfitDetailPerCustomer(DateTime _prmStart, DateTime _prmEnd, String _prmStr1, String _prmStr2, String _prmStr3, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesProfit";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
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

        public ReportDataSource SalesProfitDetailPerProduct(DateTime _prmStart, DateTime _prmEnd, String _prmStr1, String _prmStr2, String _prmStr3, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "S_FNRptSalesProfit";
                _cmd.Parameters.AddWithValue("@Start", _prmStart);
                _cmd.Parameters.AddWithValue("@End", _prmEnd);
                _cmd.Parameters.AddWithValue("@Str1", _prmStr1);
                _cmd.Parameters.AddWithValue("@Str2", _prmStr2);
                _cmd.Parameters.AddWithValue("@Str3", _prmStr3);
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

        ~ReportFinanceBL()
        {

        }
    }
}
