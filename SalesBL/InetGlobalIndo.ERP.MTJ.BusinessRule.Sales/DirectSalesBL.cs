using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Sales
{
    public sealed class DirectSalesBL : Base
    {
        public DirectSalesBL()
        {
        }

        #region SalTrDirectSalesHd

        public double RowsCountSalTrDirectSalesHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "CustName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Remark")
            {
                _pattern3 = "%%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern4 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                            from _salTrDirectSalesHd in this.db.SALTrDirectSalesHds
                            join _msCustomer in this.db.MsCustomers
                                on _salTrDirectSalesHd.CustCode equals _msCustomer.CustCode
                            where (SqlMethods.Like(_salTrDirectSalesHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                 && (SqlMethods.Like((_salTrDirectSalesHd.FileNo ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                 && (SqlMethods.Like(_salTrDirectSalesHd.Remark.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                 && _salTrDirectSalesHd.Status != DirectSalesDataMapper.GetStatusByte(TransStatus.Deleted)
                            select _salTrDirectSalesHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<SALTrDirectSalesHd> GetListSalTrDirectSalesHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<SALTrDirectSalesHd> _result = new List<SALTrDirectSalesHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "CustName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Remark")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _salTrDirectSalesHd in this.db.SALTrDirectSalesHds
                                join _msCustomer in this.db.MsCustomers
                                    on _salTrDirectSalesHd.CustCode equals _msCustomer.CustCode
                                where (SqlMethods.Like(_salTrDirectSalesHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                     && (SqlMethods.Like((_salTrDirectSalesHd.FileNo ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                     && (SqlMethods.Like(_salTrDirectSalesHd.Remark.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                     && _salTrDirectSalesHd.Status != DirectSalesDataMapper.GetStatusByte(TransStatus.Deleted)
                                orderby _salTrDirectSalesHd.Date descending
                                select new
                                {
                                    TransNmbr = _salTrDirectSalesHd.TransNmbr,
                                    FileNmbr = _salTrDirectSalesHd.FileNo,
                                    TransDate = _salTrDirectSalesHd.Date,
                                    CurrCode = _salTrDirectSalesHd.CurrCode,
                                    Status = _salTrDirectSalesHd.Status,
                                    CustCode = _salTrDirectSalesHd.CustCode,
                                    Remark = _salTrDirectSalesHd.Remark
                                }
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new SALTrDirectSalesHd(_row.TransNmbr, _row.TransDate, _row.Status, _row.CustCode, _row.FileNmbr, _row.CurrCode, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSalTrDirectSalesHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    SALTrDirectSalesHd _directSalesHd = this.db.SALTrDirectSalesHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower());

                    if (_directSalesHd != null)
                    {
                        if ((_directSalesHd.FileNo ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.SALTrDirectSalesDts
                                          where _detail.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower()
                                          select _detail);

                            this.db.SALTrDirectSalesDts.DeleteAllOnSubmit(_query);

                            this.db.SALTrDirectSalesHds.DeleteOnSubmit(_directSalesHd);

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

        public String postingTransaksi(SALTrDirectSalesHd _prmSALDirectSales, String _prmDetilTrans)
        {
            DirectPurchaseBL _purchaseBL = new DirectPurchaseBL();
            ProductBL _productBL = new ProductBL();

            String _result = "";
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmSALDirectSales.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.SALTrDirectSalesHds.InsertOnSubmit(_prmSALDirectSales);

                    var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                    this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);


                    String[] _detailTransaksi = _prmDetilTrans.Split('^');
                    foreach (String _dataDetil in _detailTransaksi)
                    {
                        String[] _rowData = _dataDetil.Split('|');
                        SALTrDirectSalesDt _addTrDirectSales = new SALTrDirectSalesDt();

                        //String _unit = _productBL.GetSingleProduct(_rowData[1].ToString()).Unit;
                        MsProductConvert _msProductConvert = _purchaseBL.GetSingleProductConvert(_rowData[1], _rowData[4]);

                        _addTrDirectSales.TransNmbr = _prmSALDirectSales.TransNmbr;
                        _addTrDirectSales.ProductCode = _rowData[1];
                        _addTrDirectSales.QtyOrder = Convert.ToInt32(_rowData[3]);
                        _addTrDirectSales.UnitOrder = _rowData[4];
                        _addTrDirectSales.Qty = ((_msProductConvert == null) ? Convert.ToInt32(_rowData[3]) : (_msProductConvert.Rate * Convert.ToInt32(_rowData[3])));
                        _addTrDirectSales.Unit = ((_msProductConvert == null) ? _rowData[4] : _msProductConvert.UnitConvert);
                        _addTrDirectSales.Price = Convert.ToDecimal(_rowData[5]);
                        _addTrDirectSales.Amount = Convert.ToDecimal(_rowData[6]);

                        var _query1 = (
                                    from _msWareHouse in this.db.MsWarehouses
                                    where _msWareHouse.WrhsName == _rowData[7]
                                    select new
                                    {
                                        WrhsCode = _msWareHouse.WrhsCode,
                                        WrhsFgSubLed = _msWareHouse.FgSubLed
                                    }
                            ).FirstOrDefault();

                        var _query2 = (
                                       from _msWareHouseLocation in this.db.MsWrhsLocations
                                       where _msWareHouseLocation.WLocationName == _rowData[9]
                                       select new
                                       {
                                           WLocationCode = _msWareHouseLocation.WLocationCode
                                       }
                            ).FirstOrDefault();

                        ////////////////////////////////////////////
                        _addTrDirectSales.WrhsCode = _query1.WrhsCode;
                        _addTrDirectSales.WrhsFgSubLed = Convert.ToChar(_query1.WrhsFgSubLed);
                        _addTrDirectSales.WrhsSubLed = _rowData[8];
                        _addTrDirectSales.WLocationCode = _query2.WLocationCode;

                        this.db.SALTrDirectSalesDts.InsertOnSubmit(_addTrDirectSales);
                    }

                    this.db.SubmitChanges();

                    _scope.Complete();

                    _result = _prmSALDirectSales.TransNmbr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public SALTrDirectSalesHd GetSingleDirectSalesHd(string _prmCode)
        {
            SALTrDirectSalesHd _result = null;

            try
            {
                _result = this.db.SALTrDirectSalesHds.Single(_temp => _temp.TransNmbr.ToLower() == _prmCode.ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public SALTrDirectSalesDt GetSingleDirectSalesDt(string _prmTransNmbr, string _prmCode, string _prmWarehouse, string _prmLocation)
        {
            SALTrDirectSalesDt _result = null;

            try
            {
                _result = this.db.SALTrDirectSalesDts.Single(_temp => _temp.TransNmbr.ToLower() == _prmTransNmbr.ToLower() && _temp.ProductCode.ToLower() == _prmCode.ToLower() && _temp.WrhsCode.ToLower() == _prmWarehouse.ToLower() && _temp.WLocationCode.ToLower() == _prmLocation.ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiDirectSalesDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;
            decimal _tempBaseAmount = 0;
            //decimal? _tempDPForex = 0;
            //decimal _tempDiscAmount = 0;
            decimal _tempDisc = 0;
            decimal _tempTax = 0;
            //decimal _tempPPNAmount = 0;
            decimal _tempTotalAmount = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');

                    SALTrDirectSalesDt _directSalesDt = this.db.SALTrDirectSalesDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.WrhsCode.Trim().ToLower() == _tempSplit[2].Trim().ToLower() && _temp.WLocationCode.Trim().ToLower() == _tempSplit[3].Trim().ToLower());

                    SALTrDirectSalesHd _directSalesHd = this.GetSingleDirectSalesHd(_prmTransNo);

                    decimal _amount = Convert.ToDecimal((_directSalesDt.Amount == 0) ? 0 : _directSalesDt.Amount);

                    _tempBaseAmount = _directSalesHd.BaseForex - _amount;

                    if (_directSalesHd.DiscPercent == 0 && _directSalesHd.BaseForex == 0)
                    {
                        _tempDisc = 0;
                        _tempTax = 0;
                    }
                    else
                    {
                        _tempDisc = Convert.ToDecimal((_directSalesHd.DiscPercent * (_directSalesHd.BaseForex - _directSalesDt.Amount)) / 100);
                        _tempTax = Convert.ToDecimal((_directSalesHd.PPNPercent * ((_directSalesHd.BaseForex - _directSalesDt.Amount) - _tempDisc)) / 100);
                    }


                    //_tempDiscAmount = _directSalesHd.DiscAmount;
                    //_tempDisc = (_directSalesHd.DiscAmount / _directSalesHd.BaseForex) * 100;
                    //_tempTex = (_directSalesHd.PPNAmount / (_directSalesHd.BaseForex + _directSalesHd.DiscAmount)) * 100;

                    //if (_tempDisc == 0)
                    //{
                    //    _tempDiscAmount = _directSalesHd.DiscAmount;
                    //}
                    //else
                    //{
                    //    _tempDiscAmount = (_tempBaseAmount * _tempDisc) / 100;
                    //}

                    //if (_tempTex == 0)
                    //{
                    //    _tempPPNAmount = _directSalesHd.PPNAmount;
                    //}
                    //else
                    //{
                    //    _tempPPNAmount = ((_tempBaseAmount - _tempDiscAmount) * _tempTex) / 100;
                    //}

                    //_tempDPForex = (_tempBaseForex - _tempDiscForex) * (((_mktSOHd.DP == null) ? 0 : _mktSOHd.DP) / 100);
                    //_tempPPNAmount = (_tempBaseAmount - _tempDiscAmount) * (_directSalesHd.PPNAmount / 100);
                    //_tempTotalAmount = _tempBaseAmount - _tempDiscAmount + _tempPPNAmount;
                    _tempTotalAmount = (_tempBaseAmount - _tempDisc) + _tempTax;

                    _directSalesHd.BaseForex = _tempBaseAmount;
                    //_directSalesHd.DPForex = _tempDPForex;
                    _directSalesHd.DiscAmount = _tempDisc;
                    //_directSalesHd.Disc = _tempDisc;
                    _directSalesHd.PPNAmount = _tempTax;
                    _directSalesHd.TotalAmount = _tempTotalAmount;

                    this.db.SALTrDirectSalesDts.DeleteOnSubmit(_directSalesDt);

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

        public List<SALTrDirectSalesDt> GetListDirectSalesDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<SALTrDirectSalesDt> _result = new List<SALTrDirectSalesDt>();

            try
            {
                var _query = (
                                from _directSalesDt in this.db.SALTrDirectSalesDts
                                join _msProduct in this.db.MsProducts
                                    on _directSalesDt.ProductCode equals _msProduct.ProductCode
                                join _wareHouse in this.db.MsWarehouses
                                    on _directSalesDt.WrhsCode equals _wareHouse.WrhsCode
                                join _wareHouseLocation in this.db.MsWrhsLocations
                                    on _directSalesDt.WLocationCode equals _wareHouseLocation.WLocationCode
                                where _directSalesDt.TransNmbr == _prmCode
                                orderby _directSalesDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _directSalesDt.TransNmbr,
                                    ProductCode = _directSalesDt.ProductCode,
                                    productName = _msProduct.ProductName,
                                    Qty = Convert.ToDecimal(_directSalesDt.QtyOrder),
                                    Price = _directSalesDt.Price,
                                    Amount = _directSalesDt.Amount,
                                    WrhsName = _wareHouse.WrhsName,
                                    WrhsFgSubLed = _directSalesDt.WrhsFgSubLed,
                                    WrhsSubLedName = _directSalesDt.WrhsSubLed,
                                    WLocationName = _wareHouseLocation.WLocationName,
                                    WrhsCode = _directSalesDt.WrhsCode,
                                    WrhsSubledCode = _directSalesDt.WrhsSubLed,
                                    WrhsLocationCode = _directSalesDt.WLocationCode
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new SALTrDirectSalesDt(_row.TransNmbr, _row.ProductCode, _row.productName, _row.Qty, _row.Price, _row.Amount, _row.WrhsCode, _row.WrhsFgSubLed, _row.WrhsSubledCode, _row.WrhsLocationCode, _row.WrhsName, _row.WrhsSubLedName, _row.WLocationName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<SALTrDirectSalesDt> GetListDirectSalesDt(string _prmCode)
        {
            List<SALTrDirectSalesDt> _result = new List<SALTrDirectSalesDt>();

            try
            {
                var _query = (
                                from _directSalesDt in this.db.SALTrDirectSalesDts
                                join _msProduct in this.db.MsProducts
                                    on _directSalesDt.ProductCode equals _msProduct.ProductCode
                                join _wareHouse in this.db.MsWarehouses
                                    on _directSalesDt.WrhsCode equals _wareHouse.WrhsCode
                                join _wareHouseLocation in this.db.MsWrhsLocations
                                    on _directSalesDt.WLocationCode equals _wareHouseLocation.WLocationCode
                                where _directSalesDt.TransNmbr == _prmCode
                                orderby _directSalesDt.ProductCode ascending
                                select _directSalesDt
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


        public int RowsCountDirectSalesDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _DirectSalesDt in this.db.SALTrDirectSalesDts
                                 where _DirectSalesDt.TransNmbr == _prmCode
                                 select _DirectSalesDt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddDirectSales(Decimal _prmAmount, SALTrDirectSalesDt _prmDirectSalesDt)
        {
            bool _result = false;
            decimal _tempBaseAmount = 0;
            //decimal _tempDiscAmount = 0;
            decimal _tempDisc = 0;
            decimal _tempTax = 0;
            //decimal _tempPPNAmount = 0;
            decimal _tempTotalAmount = 0;

            try
            {

                SALTrDirectSalesHd _directSalesHd = this.GetSingleDirectSalesHd(_prmDirectSalesDt.TransNmbr);

                _tempBaseAmount = _directSalesHd.BaseForex + Convert.ToDecimal(_prmAmount);

                //_tempDiscAmount = _directSalesHd.DiscAmount;
                if (_directSalesHd.DiscPercent == 0 && _directSalesHd.BaseForex == 0)
                {
                    _tempDisc = 0;
                    _tempTax = 0;
                }
                else
                {
                    _tempDisc = Convert.ToDecimal((_directSalesHd.DiscPercent * (_directSalesHd.BaseForex + _prmDirectSalesDt.Amount)) / 100);
                    _tempTax = Convert.ToDecimal((_directSalesHd.PPNPercent * ((_directSalesHd.BaseForex + _prmDirectSalesDt.Amount) - _tempDisc)) / 100);
                }

                //if (_tempDisc == 0)
                //{
                //    _tempDiscAmount = _directSalesHd.DiscAmount;
                //}
                //else
                //{
                //    _tempDiscAmount = Math.Round((_tempBaseAmount * _tempDisc) / 100, 2);
                //}

                //if (_tempTax == 0)
                //{
                //    _tempPPNAmount = _directSalesHd.PPNAmount;
                //}
                //else
                //{
                //    _tempPPNAmount = Convert.ToDecimal(Math.Round(((_tempBaseAmount - _tempDiscAmount) * _tempTax) / 100, 2));
                //}

                _tempTotalAmount = (_tempBaseAmount - _tempDisc) + _tempTax;

                _directSalesHd.BaseForex = _tempBaseAmount;
                _directSalesHd.DiscAmount = _tempDisc;
                _directSalesHd.PPNAmount = _tempTax;
                _directSalesHd.TotalAmount = _tempTotalAmount;
                _directSalesHd.EditBy = HttpContext.Current.User.Identity.Name;
                _directSalesHd.EditDate = DateTime.Now;

                this.db.SALTrDirectSalesDts.InsertOnSubmit(_prmDirectSalesDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditDirectSalesHd(SALTrDirectSalesHd _prmDirectSalesHd)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditDirectSalesDtAndEditDirectSalesHd(SALTrDirectSalesDt _prmDirectSalesDt, decimal _prmAmount)
        {
            bool _result = false;
            decimal _tempBaseAmount = 0;
            decimal _tempDiscAmount = 0;
            decimal _tempDisc = 0;
            decimal _tempTex = 0;
            decimal _tempPPNAmount = 0;
            decimal _tempTotalAmount = 0;

            try
            {
                SALTrDirectSalesHd _directSalesHd = this.GetSingleDirectSalesHd(_prmDirectSalesDt.TransNmbr);

                _tempBaseAmount = (_directSalesHd.BaseForex - _prmDirectSalesDt.Amount) + _prmAmount;

                //_tempBaseAmount = _directSalesHd.BaseForex + Convert.ToDecimal(_prmAmount);
                _prmDirectSalesDt.Amount = _prmAmount;
                //_tempDiscAmount = _directSalesHd.DiscAmount;
                //_tempDisc = (_directSalesHd.DiscAmount / _directSalesHd.BaseForex) * 100;
                //_tempTex = (_directSalesHd.PPNAmount / (_directSalesHd.BaseForex - _directSalesHd.DiscAmount)) * 100;

                if (_directSalesHd.DiscPercent == 0)
                {
                    _tempDiscAmount = _directSalesHd.DiscAmount;
                }
                else
                {
                    _tempDiscAmount = Math.Round((_tempBaseAmount * Convert.ToDecimal(_directSalesHd.DiscPercent)) / 100, 2);
                }

                if (_directSalesHd.PPNPercent == 0)
                {
                    _tempPPNAmount = _directSalesHd.PPNAmount;
                }
                else
                {
                    if (_tempBaseAmount != 0)
                        _tempPPNAmount = Convert.ToDecimal(Math.Round(((_tempBaseAmount - _tempDiscAmount) * Convert.ToDecimal(_directSalesHd.PPNPercent)) / 100, 2));
                    else
                        _tempPPNAmount = 0;
                }

                if (_tempBaseAmount != 0)
                    _tempTotalAmount = _tempBaseAmount - _tempDiscAmount + _tempPPNAmount;

                _directSalesHd.BaseForex = _tempBaseAmount;
                _directSalesHd.DiscAmount = _tempDiscAmount;
                _directSalesHd.PPNAmount = _tempPPNAmount;
                _directSalesHd.TotalAmount = _tempTotalAmount;
                _directSalesHd.EditBy = HttpContext.Current.User.Identity.Name;
                _directSalesHd.EditDate = DateTime.Now;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                int _success = this.db.SpSAL_DirectSalesGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.DirectSales);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.SpSAL_DirectSalesApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        SALTrDirectSalesHd _directSalesHd = this.GetSingleDirectSalesHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_directSalesHd.Date.Year, _directSalesHd.Date.Month, AppModule.GetValue(TransactionType.DirectSales), this._companyTag, ""))
                        {
                            _directSalesHd.FileNo = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.DirectSales);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleDirectSalesHd(_prmTransNmbr).FileNo;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

                        _result = "Approve Success";
                        _scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Posting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                SALTrDirectSalesHd _trDirectSalesHd = this.db.SALTrDirectSalesHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_trDirectSalesHd.Date);
                if (_locked == "")
                {
                    int _success = this.db.SpSAL_DirectSalesPosting(_prmTransNmbr, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.DirectSales);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleDirectSalesHd(_prmTransNmbr).FileNo;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Unposting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                SALTrDirectSalesHd _trDirectSalesHd = this.db.SALTrDirectSalesHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_trDirectSalesHd.Date);
                if (_locked == "")
                {
                    int _success = this.db.SpSAL_DirectSalesUnPost(_prmTransNmbr, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "Unposting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public MsProductType GetSingleProductType(string _prmCode)
        {
            MsProductType _result = null;

            try
            {
                _result = this.db.MsProductTypes.Single(_temp => _temp.ProductTypeCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<SALTrDirectSalesDt_SerialNmbr> GetListSerialNmbr(int _prmReqPage, int _prmPageSize, String _prmTransNmbr, string _prmProductCode, String _prmLocation, String _prmWrhsCode)
        {
            List<SALTrDirectSalesDt_SerialNmbr> _result = new List<SALTrDirectSalesDt_SerialNmbr>();

            try
            {
                var _query = (
                                from _salTrDirectSalesDt_SerialNmbrs in this.db.SALTrDirectSalesDt_SerialNmbrs
                                where _salTrDirectSalesDt_SerialNmbrs.TransNmbr == _prmTransNmbr
                                    && _salTrDirectSalesDt_SerialNmbrs.ProductCode == _prmProductCode
                                     && _salTrDirectSalesDt_SerialNmbrs.WLocationCode == _prmLocation
                                    && _salTrDirectSalesDt_SerialNmbrs.WrhsCode == _prmWrhsCode
                                orderby _salTrDirectSalesDt_SerialNmbrs.SerialNmbr ascending
                                select new
                                {
                                    SerialNumber = _salTrDirectSalesDt_SerialNmbrs.SerialNmbr
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new SALTrDirectSalesDt_SerialNmbr(_row.SerialNumber));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Double RowsCountSALTrDirectSalesSerialNumberByProduct(String _prmTransNmbr, String _prmProductCode, String _prmLocation, String _prmWrhsCode)
        {
            Double _result = 0;

            try
            {
                var _query = (
                                from _salTrDirectSalesDt_SerialNumber in this.db.SALTrDirectSalesDt_SerialNmbrs
                                where _salTrDirectSalesDt_SerialNumber.TransNmbr == _prmTransNmbr
                                    && _salTrDirectSalesDt_SerialNumber.ProductCode == _prmProductCode
                                     && _salTrDirectSalesDt_SerialNumber.WLocationCode == _prmLocation
                                    && _salTrDirectSalesDt_SerialNumber.WrhsCode == _prmWrhsCode
                                orderby _salTrDirectSalesDt_SerialNumber.SerialNmbr ascending
                                select new
                                {
                                    SerialNumber = _salTrDirectSalesDt_SerialNumber.SerialNmbr
                                }
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSALTrDirectSalesSerialNumber(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    SALTrDirectSalesDt_SerialNmbr _salTrDirectSalesSerialNumber = this.db.SALTrDirectSalesDt_SerialNmbrs.Single(_temp => _temp.SerialNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_salTrDirectSalesSerialNumber != null)
                    {
                        this.db.SALTrDirectSalesDt_SerialNmbrs.DeleteOnSubmit(_salTrDirectSalesSerialNumber);
                    }
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

        public bool AddSerialNumber(SALTrDirectSalesDt_SerialNmbr _prmSALTrDirectSalesDt_SerialNmbr)
        {
            bool _result = false;

            try
            {
                this.db.SALTrDirectSalesDt_SerialNmbrs.InsertOnSubmit(_prmSALTrDirectSalesDt_SerialNmbr);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSerialNumber(SALTrDirectSalesDt_SerialNmbr _prmSALTrDirectSalesDt_SerialNmbr)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public SALTrDirectSalesDt_SerialNmbr GetSingleSerialNumber(string _prmCode)
        {
            SALTrDirectSalesDt_SerialNmbr _result = null;

            try
            {
                _result = this.db.SALTrDirectSalesDt_SerialNmbrs.Single(_temp => _temp.SerialNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<String> GetListSerialNumber(String _prmTransNmbr, string _prmProductCode, String _prmLocation, String _prmWrhsCode)
        {
            List<String> _result = new List<String>();

            try
            {
                var _query = (
                                from _salTrDirectSalesDt_SerialNmbrs in this.db.SALTrDirectSalesDt_SerialNmbrs
                                where _salTrDirectSalesDt_SerialNmbrs.TransNmbr == _prmTransNmbr
                                    && _salTrDirectSalesDt_SerialNmbrs.ProductCode == _prmProductCode
                                    && _salTrDirectSalesDt_SerialNmbrs.WLocationCode == _prmLocation
                                    && _salTrDirectSalesDt_SerialNmbrs.WrhsCode == _prmWrhsCode
                                orderby _salTrDirectSalesDt_SerialNmbrs.SerialNmbr ascending
                                select new
                                {
                                    SerialNumber = _salTrDirectSalesDt_SerialNmbrs.SerialNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(_row.SerialNumber);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(List<SALTrDirectSalesDt_SerialNmbr> _prmSALTrDirectSalesDt_SerialNmbr)
        {
            bool _result = false;

            try
            {
                foreach (SALTrDirectSalesDt_SerialNmbr _row in _prmSALTrDirectSalesDt_SerialNmbr)
                {
                    SALTrDirectSalesDt_SerialNmbr _salTrDirectSalesDt_SerialNmbr = new SALTrDirectSalesDt_SerialNmbr();

                    _salTrDirectSalesDt_SerialNmbr.SerialNmbr = _row.SerialNmbr;
                    _salTrDirectSalesDt_SerialNmbr.ProductCode = _row.ProductCode;
                    _salTrDirectSalesDt_SerialNmbr.TransNmbr = _row.TransNmbr;
                    _salTrDirectSalesDt_SerialNmbr.WLocationCode = _row.WLocationCode;
                    _salTrDirectSalesDt_SerialNmbr.WrhsCode = _row.WrhsCode;

                    this.db.SALTrDirectSalesDt_SerialNmbrs.InsertOnSubmit(_salTrDirectSalesDt_SerialNmbr);
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

        public List<MsProduct_SerialNumber> GetPINForNCPSales(String _prmTransNmbr)
        {
            List<MsProduct_SerialNumber> _result = new List<MsProduct_SerialNumber>();

            try
            {
                var _query = (
                                from _msProduct_SerialNumber in this.db.MsProduct_SerialNumbers
                                join _sal_trDirectSalesDt_SerialNmbr in this.db.SALTrDirectSalesDt_SerialNmbrs
                                    on _msProduct_SerialNumber.SerialNumber equals _sal_trDirectSalesDt_SerialNmbr.SerialNmbr
                                where _sal_trDirectSalesDt_SerialNmbr.TransNmbr == _prmTransNmbr
                                && _sal_trDirectSalesDt_SerialNmbr.ProductCode == _msProduct_SerialNumber.ProductCode
                                orderby _msProduct_SerialNumber.SerialNumber ascending
                                select new
                                {
                                    SerialNumber = _msProduct_SerialNumber.SerialNumber,
                                    PIN = _msProduct_SerialNumber.PIN,
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct_SerialNumber(_row.SerialNumber, _row.PIN));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int CekInSALTrDirectSales_SerialNumber(String _prmTransNmbr)
        {
            int _result = 0;
            try
            {
                var _query = (from _salTrDirectSalesDt in this.db.SALTrDirectSalesDts
                              join _salTrDirectSalesSerialNmbr in this.db.SALTrDirectSalesDt_SerialNmbrs
                              on _salTrDirectSalesDt.TransNmbr equals _salTrDirectSalesSerialNmbr.TransNmbr
                              where _salTrDirectSalesDt.ProductCode == _salTrDirectSalesSerialNmbr.ProductCode
                              && _salTrDirectSalesDt.WLocationCode == _salTrDirectSalesSerialNmbr.WLocationCode
                              && _salTrDirectSalesDt.WrhsCode == _salTrDirectSalesSerialNmbr.WrhsCode
                              && _salTrDirectSalesDt.TransNmbr == _prmTransNmbr
                              select _salTrDirectSalesDt
                              ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _result;
        }

        public bool GetSingleDirectSalesHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    SALTrDirectSalesHd _directSalesHd = this.db.SALTrDirectSalesHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower());

                    if (_directSalesHd != null)
                    {
                        if (_directSalesHd.Status != DirectSalesDataMapper.GetStatusByte(TransStatus.Posted))
                        {
                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiApproveDirectSalesHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    SALTrDirectSalesHd _directSalesHd = this.db.SALTrDirectSalesHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower());

                    if (_directSalesHd.Status == DirectSalesDataMapper.GetStatusByte(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _directSalesHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _directSalesHd.FileNo;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_directSalesHd != null)
                    {
                        if ((_directSalesHd.FileNo ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.SALTrDirectSalesDts
                                          where _detail.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower()
                                          select _detail);

                            this.db.SALTrDirectSalesDts.DeleteAllOnSubmit(_query);

                            this.db.SALTrDirectSalesHds.DeleteOnSubmit(_directSalesHd);

                            _result = true;
                        }
                        else if (_directSalesHd.FileNo != "" && _directSalesHd.Status == DirectSalesDataMapper.GetStatusByte(TransStatus.Approved))
                        {
                            _directSalesHd.Status = DirectSalesDataMapper.GetStatusByte(TransStatus.Deleted);
                            _result = true;
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


        #endregion

        #region SalTrDirectSalesDt

        public List<Master_ProductSalesPrice> GetUnitForDDL(String _prmProductCode, String _prmCurr)
        {
            List<Master_ProductSalesPrice> _result = new List<Master_ProductSalesPrice>();

            try
            {
                var _query = (
                                from _master_ProductSalesPrice in this.db.Master_ProductSalesPrices
                                where _master_ProductSalesPrice.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower()
                                && _master_ProductSalesPrice.CurrCode.Trim().ToLower() == _prmCurr.Trim().ToLower()
                                select new
                                {
                                    SalesPrice = _master_ProductSalesPrice.SalesPrice,
                                    UnitCode = _master_ProductSalesPrice.UnitCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_ProductSalesPrice(_row.UnitCode, Convert.ToDecimal(_row.SalesPrice)));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Decimal GetPriceForDDL(String _prmProductCode, String _prmUnitCode, String _prmCurr)
        {
            Decimal _result = 0;

            try
            {
                var _query = (
                                from _master_ProductSalesPrice in this.db.Master_ProductSalesPrices
                                where _master_ProductSalesPrice.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower()
                                && _master_ProductSalesPrice.UnitCode.Trim().ToLower() == _prmUnitCode.Trim().ToLower()
                                && _master_ProductSalesPrice.CurrCode.Trim().ToLower() == _prmCurr.Trim().ToLower()
                                select _master_ProductSalesPrice.SalesPrice
                            );

                _result = _query.FirstOrDefault();
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
                                from _salTrDirectSalesDts in this.db.SALTrDirectSalesDts
                                where _salTrDirectSalesDts.TransNmbr.ToLower() == _prmSJNo.ToLower() && _salTrDirectSalesDts.ProductCode == _prmProductCode
                                select new
                                {
                                    Qty = _salTrDirectSalesDts.Qty
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

        #endregion

        public string GetValueForReport(string _prmReportID, string _prmReportParameter)
        {
            string _result = "";

            try
            {
                var _query = (from _companyConfig in this.db.CompanyConfigReportParameters
                              where _companyConfig.ReportID == _prmReportID
                              && _companyConfig.ReportParameter == _prmReportParameter
                              select new
                              {
                                  Value = _companyConfig.Value
                              }
                              );
                foreach (var _obj in _query)
                {
                    _result = _obj.Value;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        ~DirectSalesBL()
        {
        }
    }
}
