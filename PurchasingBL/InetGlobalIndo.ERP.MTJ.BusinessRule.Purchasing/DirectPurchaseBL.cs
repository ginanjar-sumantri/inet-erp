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
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing
{
    public sealed class DirectPurchaseBL : Base
    {
        public DirectPurchaseBL()
        {
        }

        #region PRCTrDirectPurchaseHd

        public double RowsCountPRCTrDirectPurchaseHd(string _prmCategory, string _prmKeyword)
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
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
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

            var _query =
                        (
                            from _prcTrDirectPurchaseHd in this.db.PRCTrDirectPurchaseHds
                            join _msSupplier in this.db.MsSuppliers
                                on _prcTrDirectPurchaseHd.SuppCode equals _msSupplier.SuppCode
                            where (SqlMethods.Like(_prcTrDirectPurchaseHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                 && (SqlMethods.Like((_prcTrDirectPurchaseHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                 && (SqlMethods.Like(_prcTrDirectPurchaseHd.Remark.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                 && _prcTrDirectPurchaseHd.Status != DirectPurchaseDataMapper.GetStatusByte(TransStatus.Deleted)
                            select _prcTrDirectPurchaseHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<PRCTrDirectPurchaseHd> GetListPRCTrDirectPurchaseHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<PRCTrDirectPurchaseHd> _result = new List<PRCTrDirectPurchaseHd>();

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
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
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
                                from _prcTrDirectPurchaseHd in this.db.PRCTrDirectPurchaseHds
                                join _msSupplier in this.db.MsSuppliers
                               on _prcTrDirectPurchaseHd.SuppCode equals _msSupplier.SuppCode
                                where (SqlMethods.Like(_prcTrDirectPurchaseHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                     && (SqlMethods.Like((_prcTrDirectPurchaseHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                     && (SqlMethods.Like(_prcTrDirectPurchaseHd.Remark.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                     && _prcTrDirectPurchaseHd.Status != DirectPurchaseDataMapper.GetStatusByte(TransStatus.Deleted)
                                orderby _prcTrDirectPurchaseHd.EditDate descending
                                select new
                                {
                                    TransNmbr = _prcTrDirectPurchaseHd.TransNmbr,
                                    FileNmbr = _prcTrDirectPurchaseHd.FileNmbr,
                                    TransDate = _prcTrDirectPurchaseHd.TransDate,
                                    Status = _prcTrDirectPurchaseHd.Status,
                                    SuppCode = _prcTrDirectPurchaseHd.SuppCode,
                                    SuppName = _msSupplier.SuppName,
                                    CurrCode = _prcTrDirectPurchaseHd.CurrCode,
                                    Remark = _prcTrDirectPurchaseHd.Remark
                                }
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new PRCTrDirectPurchaseHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.SuppCode, _row.SuppName, _row.CurrCode, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPRCTrDirectPurchaseHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCTrDirectPurchaseHd _directPurchaseHd = this.db.PRCTrDirectPurchaseHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_directPurchaseHd != null)
                    {
                        if ((_directPurchaseHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.PRCTrDirectPurchaseDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.PRCTrDirectPurchaseDts.DeleteAllOnSubmit(_query);

                            this.db.PRCTrDirectPurchaseHds.DeleteOnSubmit(_directPurchaseHd);

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

        public bool DeleteMultiApprovePRCTrDirectPurchaseHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCTrDirectPurchaseHd _directPurchaseHd = this.db.PRCTrDirectPurchaseHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_directPurchaseHd.Status == DirectPurchaseDataMapper.GetStatusByte(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _directPurchaseHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _directPurchaseHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_directPurchaseHd != null)
                    {
                        if ((_directPurchaseHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.PRCTrDirectPurchaseDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.PRCTrDirectPurchaseDts.DeleteAllOnSubmit(_query);

                            this.db.PRCTrDirectPurchaseHds.DeleteOnSubmit(_directPurchaseHd);

                            _result = true;
                        }
                        else if (_directPurchaseHd.FileNmbr != "" && _directPurchaseHd.Status == DirectPurchaseDataMapper.GetStatusByte(TransStatus.Approved))
                        {
                            _directPurchaseHd.Status = DirectPurchaseDataMapper.GetStatusByte(TransStatus.Deleted);
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

        public String Add(PRCTrDirectPurchaseHd _prmPRCDirectPurchase, String _prmDetilTrans)
        {
            String _result = "";
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPRCDirectPurchase.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.PRCTrDirectPurchaseHds.InsertOnSubmit(_prmPRCDirectPurchase);

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
                        PRCTrDirectPurchaseDt _prcTrDirectPurchase = new PRCTrDirectPurchaseDt();
                        MsProductConvert _msProductConvert = this.GetSingleProductConvert(_rowData[1], _rowData[7]);

                        _prcTrDirectPurchase.TransNmbr = _prmPRCDirectPurchase.TransNmbr;
                        _prcTrDirectPurchase.ProductCode = _rowData[1];
                        _prcTrDirectPurchase.QtyOrder = Convert.ToInt32(_rowData[6]);
                        _prcTrDirectPurchase.UnitOrder = _rowData[7];
                        _prcTrDirectPurchase.Qty = ((_msProductConvert == null) ? Convert.ToInt32(_rowData[6]) : _msProductConvert.Rate * Convert.ToInt32(_rowData[6]));
                        _prcTrDirectPurchase.Unit = ((_msProductConvert == null) ? _rowData[7] : _msProductConvert.UnitConvert);
                        _prcTrDirectPurchase.Price = Convert.ToDecimal(_rowData[8]);
                        _prcTrDirectPurchase.Amount = Convert.ToDecimal(_rowData[9]);
                        _prcTrDirectPurchase.Remark = _rowData[10];

                        var _query1 = (
                                    from _msWareHouse in this.db.MsWarehouses
                                    where _msWareHouse.WrhsName == _rowData[3]
                                    select new
                                    {
                                        WrhsCode = _msWareHouse.WrhsCode,
                                        WrhsFgSubLed = _msWareHouse.FgSubLed
                                    }
                            ).FirstOrDefault();

                        var _query2 = (
                                       from _msWareHouseLocation in this.db.MsWrhsLocations
                                       where _msWareHouseLocation.WLocationName == _rowData[5]
                                       select new
                                       {
                                           WLocationCode = _msWareHouseLocation.WLocationCode
                                       }
                            ).FirstOrDefault();

                        //////////////////////////////////////////////
                        _prcTrDirectPurchase.WrhsCode = _query1.WrhsCode;
                        _prcTrDirectPurchase.WrhsFgSubLed = Convert.ToChar(_query1.WrhsFgSubLed);
                        _prcTrDirectPurchase.WrhsSubLed = _rowData[4];
                        _prcTrDirectPurchase.LocationCode = _query2.WLocationCode;

                        this.db.PRCTrDirectPurchaseDts.InsertOnSubmit(_prcTrDirectPurchase);
                    }

                    this.db.SubmitChanges();

                    _scope.Complete();

                    _result = _prmPRCDirectPurchase.TransNmbr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public PRCTrDirectPurchaseHd GetSingleDirectPurchaseHd(string _prmCode)
        {
            PRCTrDirectPurchaseHd _result = null;

            try
            {
                _result = this.db.PRCTrDirectPurchaseHds.Single(_temp => _temp.TransNmbr.ToLower() == _prmCode.ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSinglePRCTrDirectPurchaseHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {

                    PRCTrDirectPurchaseHd _prcTrDirectPurchaseHd = this.db.PRCTrDirectPurchaseHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_prcTrDirectPurchaseHd != null)
                    {
                        if (_prcTrDirectPurchaseHd.Status != DirectPurchaseDataMapper.GetStatusByte(TransStatus.Posted))
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

        public PRCTrDirectPurchaseDt GetSingleDirectPurchaseDt(string _prmTransNmbr, string _prmCode, string _prmWarehouse, string _prmLocation)
        {
            PRCTrDirectPurchaseDt _result = null;

            try
            {
                _result = this.db.PRCTrDirectPurchaseDts.Single(_temp => _temp.TransNmbr.ToLower() == _prmTransNmbr.ToLower() && _temp.ProductCode.ToLower() == _prmCode.ToLower() && _temp.WrhsCode.ToLower() == _prmWarehouse.ToLower() && _temp.LocationCode.ToLower() == _prmLocation.ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiDirectPurchaseDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;
            decimal _tempBaseAmount = 0;
            decimal _tempDiscAmount = 0;
            decimal _tempDisc = 0;
            decimal _tempPPN = 0;
            decimal _tempPPNAmount = 0;
            decimal _tempPPH = 0;
            decimal _tempPPHAmount = 0;
            decimal _tempTotalAmount = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('^');

                    PRCTrDirectPurchaseDt _directPurchaseDt = this.db.PRCTrDirectPurchaseDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.WrhsCode.Trim().ToLower() == _tempSplit[2].Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _tempSplit[3].Trim().ToLower());

                    PRCTrDirectPurchaseHd _directPurchaseHd = this.GetSingleDirectPurchaseHd(_prmTransNo);

                    decimal _amount = _directPurchaseDt.Amount;

                    _tempBaseAmount = _directPurchaseHd.BaseForex - _amount;
                    _tempDisc = _directPurchaseHd.Disc;
                    _tempDiscAmount = (_tempDisc * _tempBaseAmount) / 100;
                    _tempPPN = _directPurchaseHd.PPN;
                    _tempPPNAmount = (_tempBaseAmount - _tempDiscAmount) * _tempPPN / 100;
                    _tempPPH = _directPurchaseHd.PPh;
                    _tempPPHAmount = (_tempBaseAmount - _tempDiscAmount) * _tempPPH / 100;
                    _tempTotalAmount = _tempBaseAmount - _tempDiscAmount + _tempPPNAmount + _tempPPHAmount;

                    _directPurchaseHd.BaseForex = _tempBaseAmount;
                    _directPurchaseHd.Disc = _tempDisc;
                    _directPurchaseHd.DiscForex = _tempDiscAmount;
                    _directPurchaseHd.PPN = _tempPPN;
                    _directPurchaseHd.PPNForex = _tempPPNAmount;
                    _directPurchaseHd.PPh = _tempPPH;
                    _directPurchaseHd.PPhForex = _tempPPHAmount;
                    _directPurchaseHd.TotalForex = _tempTotalAmount;
                    _directPurchaseHd.EditBy = HttpContext.Current.User.Identity.Name;
                    _directPurchaseHd.EditDate = DateTime.Now;

                    this.db.PRCTrDirectPurchaseDts.DeleteOnSubmit(_directPurchaseDt);
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

        public List<PRCTrDirectPurchaseDt> GetListDirectPurchaseDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<PRCTrDirectPurchaseDt> _result = new List<PRCTrDirectPurchaseDt>();

            try
            {
                var _query = (
                                from _directPurchaseDt in this.db.PRCTrDirectPurchaseDts
                                join _msProduct in this.db.MsProducts
                                    on _directPurchaseDt.ProductCode equals _msProduct.ProductCode
                                join _wareHouse in this.db.MsWarehouses
                                    on _directPurchaseDt.WrhsCode equals _wareHouse.WrhsCode
                                join _wareHouseLocation in this.db.MsWrhsLocations
                                    on _directPurchaseDt.LocationCode equals _wareHouseLocation.WLocationCode
                                where _directPurchaseDt.TransNmbr == _prmCode
                                orderby _directPurchaseDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _directPurchaseDt.TransNmbr,
                                    ProductCode = _directPurchaseDt.ProductCode,
                                    productName = _msProduct.ProductName,
                                    WrhsCode = _directPurchaseDt.WrhsCode,
                                    WrhsName = _wareHouse.WrhsName,
                                    LocationCode = _directPurchaseDt.LocationCode,
                                    LocationName = _wareHouseLocation.WLocationName,
                                    Qty = Convert.ToDecimal(_directPurchaseDt.QtyOrder),
                                    Price = _directPurchaseDt.Price,
                                    Unit = _directPurchaseDt.UnitOrder,
                                    UnitName = (
                                                    from _msUnit in this.db.MsUnits
                                                    where _directPurchaseDt.UnitOrder == _msUnit.UnitCode
                                                    select _msUnit.UnitName
                                                ).FirstOrDefault(),
                                    Amount = _directPurchaseDt.Amount,
                                    Remark = _directPurchaseDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new PRCTrDirectPurchaseDt(_row.TransNmbr, _row.ProductCode, _row.productName, _row.LocationCode, _row.LocationName, _row.WrhsCode, _row.WrhsName, _row.Qty, _row.Unit, _row.UnitName, _row.Price, _row.Amount, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int RowsCountDirectPurchaseDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _DirectPurchaseDt in this.db.PRCTrDirectPurchaseDts
                                 where _DirectPurchaseDt.TransNmbr == _prmCode
                                 select _DirectPurchaseDt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddDirectPurchaseDt(Decimal _prmAmount, PRCTrDirectPurchaseDt _prmDirectPurchaseDt)
        {
            bool _result = false;
            decimal _tempBaseAmount = 0;
            decimal _tempDiscAmount = 0;
            decimal _tempDisc = 0;
            decimal _tempPPN = 0;
            decimal _tempPPNAmount = 0;
            decimal _tempPPH = 0;
            decimal _tempPPHAmount = 0;
            decimal _tempTotalAmount = 0;

            try
            {
                PRCTrDirectPurchaseHd _directPurchaseHd = this.GetSingleDirectPurchaseHd(_prmDirectPurchaseDt.TransNmbr);

                _tempBaseAmount = _directPurchaseHd.BaseForex + _prmAmount;
                _tempDisc = _directPurchaseHd.Disc;
                _tempDiscAmount = (_tempDisc * _tempBaseAmount) / 100;
                _tempPPN = _directPurchaseHd.PPN;
                _tempPPNAmount = (_tempBaseAmount - _tempDiscAmount) * _tempPPN / 100;
                _tempPPH = _directPurchaseHd.PPh;
                _tempPPHAmount = (_tempBaseAmount - _tempDiscAmount) * _tempPPH / 100;
                _tempTotalAmount = _tempBaseAmount - _tempDiscAmount + _tempPPNAmount + _tempPPHAmount;

                _directPurchaseHd.BaseForex = _tempBaseAmount;
                _directPurchaseHd.Disc = _tempDisc;
                _directPurchaseHd.DiscForex = _tempDiscAmount;
                _directPurchaseHd.PPN = _tempPPN;
                _directPurchaseHd.PPNForex = _tempPPNAmount;
                _directPurchaseHd.PPh = _tempPPH;
                _directPurchaseHd.PPhForex = _tempPPHAmount;
                _directPurchaseHd.TotalForex = _tempTotalAmount;
                _directPurchaseHd.EditBy = HttpContext.Current.User.Identity.Name;
                _directPurchaseHd.EditDate = DateTime.Now;

                this.db.PRCTrDirectPurchaseDts.InsertOnSubmit(_prmDirectPurchaseDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditDirectPurchaseHd(PRCTrDirectPurchaseHd _prmDirectPurchaseHd)
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

        public bool EditDirectPurchaseDtAndEditDirectPurchaseHd(PRCTrDirectPurchaseDt _prmDirectPurchaseDt, decimal _prmAmount)
        {
            bool _result = false;
            decimal _tempBaseAmount = 0;
            decimal _tempDiscAmount = 0;
            decimal _tempDisc = 0;
            decimal _tempPPN = 0;
            decimal _tempPPNAmount = 0;
            decimal _tempPPH = 0;
            decimal _tempPPHAmount = 0;
            decimal _tempTotalAmount = 0;

            try
            {
                PRCTrDirectPurchaseHd _directPurchaseHd = this.GetSingleDirectPurchaseHd(_prmDirectPurchaseDt.TransNmbr);

                _tempBaseAmount = (_directPurchaseHd.BaseForex - _prmDirectPurchaseDt.Amount) + _prmAmount;
                _tempDisc = _directPurchaseHd.Disc;
                _tempDiscAmount = (_tempDisc * _tempBaseAmount) / 100;
                _tempPPN = _directPurchaseHd.PPN;
                _tempPPNAmount = (_tempBaseAmount - _tempDiscAmount) * _tempPPN / 100;
                _tempPPH = _directPurchaseHd.PPh;
                _tempPPHAmount = (_tempBaseAmount - _tempDiscAmount) * _tempPPH / 100;
                _tempTotalAmount = _tempBaseAmount - _tempDiscAmount + _tempPPNAmount + _tempPPHAmount;

                _directPurchaseHd.BaseForex = _tempBaseAmount;
                _directPurchaseHd.Disc = _tempDisc;
                _directPurchaseHd.DiscForex = _tempDiscAmount;
                _directPurchaseHd.PPN = _tempPPN;
                _directPurchaseHd.PPNForex = _tempPPNAmount;
                _directPurchaseHd.PPh = _tempPPH;
                _directPurchaseHd.PPhForex = _tempPPHAmount;
                _directPurchaseHd.TotalForex = _tempTotalAmount;
                _directPurchaseHd.EditBy = HttpContext.Current.User.Identity.Name;
                _directPurchaseHd.EditDate = DateTime.Now;

                _prmDirectPurchaseDt.Amount = _prmAmount;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(string _prmTransNmbr, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spPRC_DirectPurchaseGetAppr(_prmTransNmbr, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(string _prmTransNmbr, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.spPRC_DirectPurchaseApprove(_prmTransNmbr,_prmuser, ref _result);

                    if (_result == "")
                    {
                        PRCTrDirectPurchaseHd _directPurchaseHd = this.GetSingleDirectPurchaseHd(_prmTransNmbr);

                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_directPurchaseHd.TransDate.Year, _directPurchaseHd.TransDate.Month, AppModule.GetValue(TransactionType.DirectPurchase), this._companyTag, ""))
                        {
                            _directPurchaseHd.FileNmbr = _item.Number;
                        }
                        this.db.SubmitChanges();

                        _scope.Complete();

                        _result = "Approve Success";
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

        public string Posting(string _prmTransNmbr, String _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                PRCTrDirectPurchaseHd _directPurchaseHd = this.GetSingleDirectPurchaseHd(_prmTransNmbr);
                String _locked = _transCloseBL.IsExistAndLocked(_directPurchaseHd.TransDate);
                if (_locked == "")
                {
                    this.db.spPRC_DirectPurchasePost(_prmTransNmbr, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";
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

        public string Unposting(string _prmTransNmbr, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                PRCTrDirectPurchaseHd _directPurchaseHd = this.GetSingleDirectPurchaseHd(_prmTransNmbr);
                String _locked = _transCloseBL.IsExistAndLocked(_directPurchaseHd.TransDate);
                if (_locked == "")
                {
                    this.db.spPRC_DirectPurchaseUnPost(_prmTransNmbr, _prmuser, ref _result);

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

        #endregion

        public MsProductConvert GetSingleProductConvert(String _prmProductCode, String _prmUnitCode)
        {
            MsProductConvert _result = new MsProductConvert();
            
            try
            {
                //_result = this.db.MsProductConverts.Single(temp => temp.ProductCode == _prmProductCode && temp.UnitCode == _prmUnitCode);
                _result = (from _msProductConverts in this.db.MsProductConverts 
                           where _msProductConverts.ProductCode.ToLower() == _prmProductCode.ToLower()
                           && _msProductConverts.UnitCode.ToLower() == _prmUnitCode.ToLower()
                           select _msProductConverts
                            ).FirstOrDefault();
            }
            catch (Exception ex)
            {   
                throw ex;
            }

            return _result;
        }

        ~DirectPurchaseBL()
        {
        }
    }
}
