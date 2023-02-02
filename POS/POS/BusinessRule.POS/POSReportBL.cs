using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Web;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.DBFactory;
using System.Data;
using System.Data.SqlClient;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;


namespace BusinessRule.POS
{
    public sealed class POSReportBL : Base
    {
        public POSReportBL()
        {
        }

        #region Report

        public double RowsCountTransactionReport(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";


            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }


            var _query =
                        (
                            from _trSettlemetHD in this.db.POSTrSettlementHds
                            where //_msSupp.FgActive == SupplierDataMapper.SuppStatus(SupplierStatus.Active)
                               (SqlMethods.Like(_trSettlemetHD.CashierID.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            //&& (SqlMethods.Like(_msSupp.SuppType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            //&& (SqlMethods.Like(_msSupp.SuppCode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                            //&& (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                            group _trSettlemetHD by new { CashierID = _trSettlemetHD.CashierID } into _trSettlement
                            select _trSettlement.Key.CashierID
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<POSTrSettlementHd> GetListCashierForReport(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSTrSettlementHd> _result = new List<POSTrSettlementHd>();

            string _pattern1 = "%%";


            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _trSettlemetHD in this.db.POSTrSettlementHds
                                where //_msSupp.FgActive == SupplierDataMapper.SuppStatus(SupplierStatus.Active)
                                    (SqlMethods.Like(_trSettlemetHD.CashierID.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                //&& (SqlMethods.Like(_msSupp.SuppType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                //&& (SqlMethods.Like(_msSupp.SuppCode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                //&& (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                group _trSettlemetHD by new { CashierID = _trSettlemetHD.CashierID } into _trSettlement
                                orderby _trSettlement.Key.CashierID ascending
                                select new
                                {
                                    CashierID = _trSettlement.Key.CashierID
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    POSTrSettlementHd _posTrSettlementHd = new POSTrSettlementHd();
                    _posTrSettlementHd.CashierID = _row.CashierID;
                    _result.Add(_posTrSettlementHd);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountDDLProductForReport(string _prmProductSubGroup, string _prmProductType, string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmProductSubGroup != "null")
            {
                _pattern1 = "%" + _prmProductSubGroup + "%";
            }

            if (_prmProductType != "null")
            {
                _pattern2 = "%" + _prmProductType + "%";
            }

            if (_prmCategory == "Code")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }

            var _query =
                        (
                            from _trSettlementDtProducts in this.db.POSTrSettlementDtProducts
                            join _msProduct in this.db.MsProducts
                            on _trSettlementDtProducts.ProductCode equals _msProduct.ProductCode
                            where
                               _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes)
                               && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                            group _trSettlementDtProducts by new { ProductCode = _trSettlementDtProducts.ProductCode } into _trDtProducts
                            select _trDtProducts.Key.ProductCode 
                        ).Count();

            _result = _query;

            return _result;
        }


        public List<MsProduct> GetListDDLProductForReport(int _prmReqPage, int _prmPageSize, string _prmProductSubGroup, string _prmProductType, string _prmCategory, string _prmKeyword)
        {
            List<MsProduct> _result = new List<MsProduct>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmProductSubGroup != "null")
            {
                _pattern1 = "%" + _prmProductSubGroup + "%";
            }

            if (_prmProductType != "null")
            {
                _pattern2 = "%" + _prmProductType + "%";
            }

            if (_prmCategory == "Code")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }

            try
            {
                var _query = (
                                from _trSettlementDtProducts in this.db.POSTrSettlementDtProducts
                                join _msProduct in this.db.MsProducts
                                on _trSettlementDtProducts.ProductCode equals _msProduct.ProductCode
                                where
                                    _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes)
                                    && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                group _trSettlementDtProducts by new { ProductCode = _trSettlementDtProducts.ProductCode, ProductName = _msProduct.ProductName } into _trDtProducts
                                orderby _trDtProducts.Key.ProductCode ascending
                                select new
                                {
                                    ProductCode = _trDtProducts.Key.ProductCode,
                                    ProductName = _trDtProducts.Key.ProductName + " - " + _trDtProducts.Key.ProductCode
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public ReportDataSource POSReportListPerTransaction(DateTime _prmStart, DateTime _prmEnd, int _prmFgReport, string _prmCashierID)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spPOS_RptSettlementPerTransaction";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStart);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEnd);
                _cmd.Parameters.AddWithValue("@FgReport", _prmFgReport);
                _cmd.Parameters.AddWithValue("@PrmCashierId", _prmCashierID);
                //_cmd.Parameters.AddWithValue("@From", _prmFrom);
                //_cmd.Parameters.AddWithValue("@To", _prmTo);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }
        public ReportDataSource POSReportListPerProduct(DateTime _prmStart, DateTime _prmEnd, int _prmFgReport, string _prmProductCode, string _prmFrom, string _prmTo, string _prmCashier, string _prmProductSubGroup)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spPOS_RptSettlementPerProduct";
                _cmd.Parameters.AddWithValue("@StartDate", _prmStart);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEnd);
                _cmd.Parameters.AddWithValue("@PrmProductSubGroup", _prmProductSubGroup);
                _cmd.Parameters.AddWithValue("@PrmCashierId", _prmCashier);
                _cmd.Parameters.AddWithValue("@PrmProduct", _prmProductCode);
                _cmd.Parameters.AddWithValue("@From", _prmFrom);
                _cmd.Parameters.AddWithValue("@To", _prmTo);
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

        public ReportDataSource POSReportListCloseShiftPerTransaction(DateTime _prmStart, DateTime _prmEnd, int _prmFgReport)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spPOS_RptCloseShift2";
                _cmd.Parameters.AddWithValue("@BeginDate", _prmStart);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEnd);
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
        
        #endregion

        ~POSReportBL()
        {
        }
    }
}
