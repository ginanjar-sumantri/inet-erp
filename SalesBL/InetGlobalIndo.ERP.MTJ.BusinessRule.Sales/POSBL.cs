using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Transactions;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using System.Collections;
using System.Diagnostics;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Sales
{
    public class POSBL : Base
    {
        public POSBL() { }

        public String getProductData(String _prmProductCode, String _prmCurrCode)
        {
            String _result = "||";
            try
            {
                var _query = (
                        from _productData in this.db.MsProducts
                        join _productSalesPrices in this.db.Master_ProductSalesPrices
                        on _productData.ProductCode equals _productSalesPrices.ProductCode
                        where _productData.ProductCode == _prmProductCode
                            && _productSalesPrices.CurrCode == _prmCurrCode
                            && _productSalesPrices.UnitCode == _productData.Unit
                        orderby _productSalesPrices.SalesPrice ascending
                        select new
                        {
                            ProductName = _productData.ProductName,
                            Unit = _productSalesPrices.UnitCode,
                            SellingPrice = _productSalesPrices.SalesPrice
                        }
                    );
                if (_query.Count() > 0)
                {
                    var _rs = _query.FirstOrDefault();
                    _result = _rs.ProductName + "|" + _rs.Unit + "|" + _rs.SellingPrice;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String getProductData(String _prmProductCode, String _prmCurrCode, String _prmUnitCode)
        {
            String _result = "||";
            try
            {
                var _query = (
                        from _productData in this.db.MsProducts
                        join _productSalesPrices in this.db.Master_ProductSalesPrices
                        on _productData.ProductCode equals _productSalesPrices.ProductCode
                        where _productData.ProductCode == _prmProductCode
                            && _productSalesPrices.CurrCode == _prmCurrCode
                            && _productSalesPrices.UnitCode == _prmUnitCode
                        select new
                        {
                            ProductName = _productData.ProductName,
                            Unit = _productSalesPrices.UnitCode,
                            SellingPrice = _productSalesPrices.SalesPrice
                        }
                    );
                if (_query.Count() > 0)
                {
                    var _rs = _query.FirstOrDefault();
                    _result = _rs.ProductName + "|" + _rs.Unit + "|" + _rs.SellingPrice;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public int getPriceData(String _prmProductCode, String _prmCurrCode, String _prmUnitCode)
        {
            int _result = 0;
            try
            {
                var _query = (
                        from _productSalesPrices in this.db.Master_ProductSalesPrices
                        where _productSalesPrices.ProductCode == _prmProductCode
                            && _productSalesPrices.CurrCode == _prmCurrCode
                            && _productSalesPrices.UnitCode == _prmUnitCode
                        select _productSalesPrices.SalesPrice
                    ).FirstOrDefault();
                _result = Convert.ToInt32(_query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String postingTransaksi(SAL_TrRetail _prmSAL_TrRetail, String _prmDetilTrans)
        {
            String _result = "";
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmSAL_TrRetail.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmSAL_TrRetail.TransDate.Year, _prmSAL_TrRetail.TransDate.Month, AppModule.GetValue(TransactionType.Retail), this._companyTag, ""))
                    {
                        _prmSAL_TrRetail.FileNmbr = item.Number;
                    }

                    this.db.SAL_TrRetails.InsertOnSubmit(_prmSAL_TrRetail);

                    var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                    this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);


                    //this.itemCount.Value + "|" + this.productCode.Text + "|";
                    //this.boughtItems.Value += this.productName.Value + "|" + this.qty.Text + "|" + this.uom.Value + "|";
                    //this.boughtItems.Value += this.price.Value + "|" + this.lineTotal.Value;
                    String[] _detailTransaksi = _prmDetilTrans.Split('^');
                    foreach (String _dataDetil in _detailTransaksi)
                    {
                        String[] _rowData = _dataDetil.Split('|');
                        SAL_TrRetailItem _addTrRetailItem = new SAL_TrRetailItem();
                        _addTrRetailItem.POSItemCode = Guid.NewGuid();
                        _addTrRetailItem.TransNmbr = _prmSAL_TrRetail.TransNmbr;
                        _addTrRetailItem.ProductCode = _rowData[1];
                        _addTrRetailItem.Qty = Convert.ToInt32(_rowData[3]);
                        _addTrRetailItem.Price = Convert.ToDecimal(_rowData[5]);
                        _addTrRetailItem.DiscountAmount = Convert.ToDecimal(_rowData[3]) * Convert.ToDecimal(_rowData[5]) * _prmSAL_TrRetail.DiscPercent / 100;
                        _addTrRetailItem.Total = (_addTrRetailItem.Qty * _addTrRetailItem.Price) - _addTrRetailItem.DiscountAmount;
                        this.db.SAL_TrRetailItems.InsertOnSubmit(_addTrRetailItem);
                    }

                    this.db.SubmitChanges();

                    String _errMessage = "";
                    this.db.spSAL_POSPosting(_prmSAL_TrRetail.TransNmbr, HttpContext.Current.User.Identity.Name, _errMessage);

                    _scope.Complete();

                    _result = _prmSAL_TrRetail.TransNmbr + "|" + _prmSAL_TrRetail.FileNmbr + "|" + _errMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public ReportDataSource GetTransactionData(String _prmTransNmbr)
        {
            ReportDataSource _result = new ReportDataSource();
            try
            {
                DataTable _dataTable = new DataTable();
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));
                SqlCommand _cmd = new SqlCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spSAL_POSNota";
                _cmd.Parameters.AddWithValue("@TransNmbr", _prmTransNmbr);
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

        public SortedList getPaymentType()
        {
            SortedList _result = new SortedList();
            try
            {
                var _qry = (
                        from _msPayType in this.db.MsPayTypes
                        where _msPayType.FgType == 'R'
                            || _msPayType.FgType == 'A'
                        select _msPayType
                    );
                foreach (var _rs in _qry)
                    _result.Add(_rs.PayCode, _rs.PayName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public double RowsCountPOSHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _POSHd in this.db.SAL_TrRetails
                            where (SqlMethods.Like(_POSHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_POSHd.FileNmbr.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _POSHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountPOSDt(string _prmTransNmbr)
        {
            double _result = 0;
            var _query =
                        (
                            from _POSDt in this.db.SAL_TrRetailItems
                            where _POSDt.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()
                            select _POSDt
                        ).Count();
            _result = _query;
            return _result;
        }

        public bool DeleteMultiPOS(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    SAL_TrRetail _POSHd = this.db.SAL_TrRetails.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_POSHd != null)
                    {
                        if ((_POSHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.SAL_TrRetailItems
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.SAL_TrRetailItems.DeleteAllOnSubmit(_query);

                            this.db.SAL_TrRetails.DeleteOnSubmit(_POSHd);

                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
                        }
                    }
                }

                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPOSDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    SAL_TrRetailItem _POSDt = this.db.SAL_TrRetailItems.Single(_temp => _temp.POSItemCode.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_POSDt != null)
                        this.db.SAL_TrRetailItems.DeleteOnSubmit(_POSDt);
                }
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<SAL_TrRetail> GetListPOSHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<SAL_TrRetail> _result = new List<SAL_TrRetail>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _POSHd in this.db.SAL_TrRetails
                                where (SqlMethods.Like(_POSHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_POSHd.FileNmbr.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _POSHd.TransDate descending
                                select _POSHd
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public SAL_TrRetail GetSinglePOSHd(string _prmCode)
        {
            SAL_TrRetail _result = null;

            try
            {
                _result = this.db.SAL_TrRetails.Single(_temp => _temp.TransNmbr.ToLower() == _prmCode.ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public Boolean postingTransaksi(String _prmTransDate, String _prmEmpName,
        //    String _prmEmpNumb, String _prmCustName, String _prmPaymentType, String _prmBankName,
        //    String _prmCurrCode, String _prmTotalAmount, String _prmDiscPercent, String _prmDiscAmount,
        //    String _prmPPNPercent, String _prmPPNAmount, String _prmRemark)
        //{
        //    Boolean _result = false;
        //    try
        //    {
        //        SAL_TrRetail _addHeader = new SAL_TrRetail();
        //        _addHeader.TransDate = Convert.ToDateTime(_prmTransDate);
        //        _addHeader.EmpName = _prmEmpName;
        //        _addHeader.EmpNumb = _prmEmpNumb;
        //        _addHeader.CustName = _prmCustName;
        //        _addHeader.PaymentType = _prmPaymentType;
        //        _addHeader.BankName = _prmBankName;
        //        _addHeader.CurrCode = _prmCurrCode;
        //        _addHeader.TotalAmount = Convert.ToDecimal(_prmTotalAmount);
        //        _addHeader.DiscPercent = Convert.ToByte(_prmDiscPercent);
        //        _addHeader.DiscAmount = Convert.ToDecimal(_prmDiscAmount);
        //        _addHeader.PPNPercent = Convert.ToByte(_prmPPNPercent);
        //        _addHeader.PPNAmount = Convert.ToDecimal(_prmPPNAmount);
        //        if (_prmRemark != "")
        //            _addHeader.Remark = _prmRemark;
        //        this.db.SAL_TrRetails.InsertOnSubmit(_addHeader);
        //        this.db.SubmitChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _result;
        //}

        public List<SAL_TrRetailItem> GetListPOSDt(int _prmReqPage, int _prmPageSize, string _prmTransNmbr)
        {
            List<SAL_TrRetailItem> _result = new List<SAL_TrRetailItem>();
            try
            {
                var _query = (
                                from _POSDt in this.db.SAL_TrRetailItems
                                where _POSDt.TransNmbr == _prmTransNmbr                                
                                select _POSDt
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<SAL_TrRetailItem> GetListPOSDt(string _prmTransNmbr)
        {
            List<SAL_TrRetailItem> _result = new List<SAL_TrRetailItem>();
            try
            {
                var _query = (
                                from _POSDt in this.db.SAL_TrRetailItems
                                where _POSDt.TransNmbr == _prmTransNmbr
                                select _POSDt
                             );

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetSingleQtyFromSTCSJDt(string _prmSJNo, string _prmProductCode)
        {
            decimal _result = 0;

            try
            {
                var _query =
                            (
                                from _salTrRetailItems in this.db.SAL_TrRetailItems
                                where _salTrRetailItems.TransNmbr.ToLower() == _prmSJNo.ToLower() && _salTrRetailItems.ProductCode == _prmProductCode
                                select new
                                {
                                    Qty = _salTrRetailItems.Qty
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.Qty;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        ~POSBL() { }

        public Int32 GetDetailCount(String _prmTransNmbr)
        {
            Int32 _result = 0;
            try
            {
                _result = (
                        from _salTrRetailItem in this.db.SAL_TrRetailItems
                        where _salTrRetailItem.TransNmbr == _prmTransNmbr 
                        select _salTrRetailItem
                    ).Count();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }
    }
}
