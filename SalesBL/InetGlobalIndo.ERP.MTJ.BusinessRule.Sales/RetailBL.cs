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
using InetGlobalIndo.ERP.MTJ.BusinessRule.NCC;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Sales
{
    public sealed class RetailBL : Base
    {
        public RetailBL()
        {
        }

        #region SAL_TrRetail

        public double RowsCountSAL_TrRetail(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "EmpName")
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

            var _query =
                        (
                            from _sal_TrRetailItem in this.db.SAL_TrRetails
                            join _msEmployee in this.db.MsEmployees
                                on _sal_TrRetailItem.EmpNumb equals _msEmployee.EmpNumb
                            where (SqlMethods.Like(_sal_TrRetailItem.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                 && (SqlMethods.Like((_sal_TrRetailItem.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                            select _sal_TrRetailItem.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<SAL_TrRetail> GetListSAL_TrRetail(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<SAL_TrRetail> _result = new List<SAL_TrRetail>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "EmpName")
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

            try
            {
                var _query = (
                                from _sal_TrRetail in this.db.SAL_TrRetails
                                join _msEmployee in this.db.MsEmployees
                                    on _sal_TrRetail.EmpNumb equals _msEmployee.EmpNumb
                                where (SqlMethods.Like(_sal_TrRetail.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                     && (SqlMethods.Like((_sal_TrRetail.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                orderby _sal_TrRetail.TransDate descending
                                select new
                                {
                                    TransNmbr = _sal_TrRetail.TransNmbr,
                                    FileNmbr = _sal_TrRetail.FileNmbr,
                                    TransDate = _sal_TrRetail.TransDate,
                                    EmpNumb = _sal_TrRetail.EmpNumb,
                                    EmpName = _msEmployee.EmpName,
                                    CustName = _sal_TrRetail.CustName,
                                    PaymentType = _sal_TrRetail.PaymentType,
                                    BankName = _sal_TrRetail.BankName,
                                    PaymentCode = _sal_TrRetail.PaymentCode,
                                    CurrCode = _sal_TrRetail.CurrCode,
                                    ForexRate = _sal_TrRetail.ForexRate,
                                    BaseForex = _sal_TrRetail.BaseForex,
                                    TotalAmount = _sal_TrRetail.TotalAmount,
                                    DiscPercent = _sal_TrRetail.DiscPercent,
                                    DiscAmount = _sal_TrRetail.DiscAmount,
                                    PPNPercent = _sal_TrRetail.PPNPercent,
                                    PPNAmount = _sal_TrRetail.PPNAmount,
                                    AdditionalFee = _sal_TrRetail.AdditionalFee,
                                    Status = _sal_TrRetail.Status,
                                    Remark = _sal_TrRetail.Remark
                                }
                             );

                foreach (var _row in _query)
                {
                    _result.Add(new SAL_TrRetail(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.EmpNumb, _row.EmpName, _row.CustName, _row.PaymentType, _row.BankName, _row.PaymentCode, _row.CurrCode, _row.ForexRate, _row.BaseForex, _row.TotalAmount, _row.DiscPercent, _row.DiscAmount, _row.PPNPercent, _row.PPNAmount, _row.AdditionalFee, _row.Status, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<SAL_TrRetail> GetListSAL_TrRetailForDDL()
        {
            List<SAL_TrRetail> _result = new List<SAL_TrRetail>();

            try
            {
                var _query = (
                                from _sal_TrRetail in this.db.SAL_TrRetails
                                orderby _sal_TrRetail.TransNmbr ascending
                                select new
                                {
                                    TransNmbr = _sal_TrRetail.TransNmbr,
                                    FileNmbr = _sal_TrRetail.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new SAL_TrRetail(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public SAL_TrRetail GetSingleSAL_TrRetail(string _prmCode)
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

        public bool DeleteMultiSAL_TrRetail(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    SAL_TrRetail _sal_TrRetail = this.db.SAL_TrRetails.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_sal_TrRetail != null)
                    {
                        if ((_sal_TrRetail.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.SAL_TrRetailItems
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.SAL_TrRetailItems.DeleteAllOnSubmit(_query);

                            this.db.SAL_TrRetails.DeleteOnSubmit(_sal_TrRetail);

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

        public string AddSAL_TrRetail(SAL_TrRetail _prmSAL_TrRetail)
        {
            string _result = "";

            try
            {

                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSAL_TrRetail.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.SAL_TrRetails.InsertOnSubmit(_prmSAL_TrRetail);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSAL_TrRetail.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSAL_TrRetail(SAL_TrRetail _prmSAL_TrRetail)
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

        public string GetAppr(string _prmTransNmbr, String _prmUserName)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_SALTrRetailGetAppr(_prmTransNmbr, _prmUserName, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";
                }
                else
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Approve(string _prmTransNmbr, String _prmUserName)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.S_SALTrRetailApprove(_prmTransNmbr, _prmUserName, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        SAL_TrRetail _sal_TrRetail = this.GetSingleSAL_TrRetail(_prmTransNmbr);

                        if ((_sal_TrRetail.FileNmbr ?? "").Trim() == "")
                        {
                            foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_sal_TrRetail.TransDate.Year, _sal_TrRetail.TransDate.Month, AppModule.GetValue(TransactionType.Retail), this._companyTag, ""))
                            {
                                _sal_TrRetail.FileNmbr = item.Number;
                            }

                            this.db.SubmitChanges();

                            _scope.Complete();
                        }
                    }
                    else
                    {
                        _result = _errorMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        //public string Posting(string _prmTransNmbr, int _prmRevisi, int _prmYear, int _prmPeriod, string _prmuser)
        //{
        //    string _result = "";
        //    string _errorMsg = "";

        //    try
        //    {
        //        TransactionCloseBL _transCloseBL = new TransactionCloseBL();

        //        SAL_TrRetail _sal_TrRetailItem = this.db.SAL_TrRetails.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.Revisi == _prmRevisi);
        //        String _locked = _transCloseBL.IsExistAndLocked(_sal_TrRetailItem.TransDate);
        //        if (_locked == "")
        //        {
        //            int _success = this.db.S_MKSOPost(_prmTransNmbr, _prmRevisi, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

        //            if (_errorMsg == "")
        //            {
        //                _result = "Posting Success";
        //            }
        //            else
        //            {
        //                _result = _errorMsg;
        //            }
        //        }
        //        else
        //        {
        //            _result = _locked;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = "Posting Failed";
        //        ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
        //    }

        //    return _result;
        //}

        //public string Unposting(string _prmTransNmbr, int _prmRevisi, int _prmYear, int _prmPeriod, string _prmuser)
        //{
        //    string _result = "";
        //    string _errorMsg = "";

        //    try
        //    {
        //        TransactionCloseBL _transCloseBL = new TransactionCloseBL();

        //        SAL_TrRetail _sal_TrRetailItem = this.db.SAL_TrRetails.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.Revisi == _prmRevisi);
        //        String _locked = _transCloseBL.IsExistAndLocked(_sal_TrRetailItem.TransDate);
        //        if (_locked == "")
        //        {
        //            int _success = this.db.S_MKSOUnPost(_prmTransNmbr, _prmRevisi, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

        //            if (_errorMsg == "")
        //            {
        //                _result = "Unposting Success";
        //            }
        //            else
        //            {
        //                _result = _errorMsg;
        //            }
        //        }
        //        else
        //        {
        //            _result = _locked;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = "Unposting Failed";
        //        ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
        //    }

        //    return _result;
        //}

        public string GetFileNmbrSAL_TrRetail(string _prmTransNmbr)
        {
            string _result = "";

            try
            {
                _result = (this.db.SAL_TrRetails.Single(_temp => _temp.TransNmbr == _prmTransNmbr).FileNmbr ?? "").Trim();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region SAL_TrRetailItem
        public int RowsCountSAL_TrRetailItem(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _SAL_TrRetailItem in this.db.SAL_TrRetailItems
                                 where _SAL_TrRetailItem.TransNmbr == _prmCode
                                 select _SAL_TrRetailItem.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<SAL_TrRetailItem> GetListSAL_TrRetailItem(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<SAL_TrRetailItem> _result = new List<SAL_TrRetailItem>();

            try
            {
                var _query = (
                                from _sal_TrRetailItem in this.db.SAL_TrRetailItems
                                where _sal_TrRetailItem.TransNmbr == _prmCode
                                orderby _sal_TrRetailItem.ProductCode ascending
                                select new
                                {
                                    POSItemCode = _sal_TrRetailItem.POSItemCode,
                                    TransNmbr = _sal_TrRetailItem.TransNmbr,
                                    PhoneTypeCode = _sal_TrRetailItem.PhoneTypeCode,
                                    PhoneTypeName = (
                                                       from _msPhoneType in this.db.Master_PhoneTypes
                                                       where _msPhoneType.PhoneTypeCode == _sal_TrRetailItem.PhoneTypeCode
                                                       select _msPhoneType.PhoneTypeDesc
                                                     ).FirstOrDefault(),
                                    ProductCode = _sal_TrRetailItem.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _sal_TrRetailItem.ProductCode
                                                    select _msProduct.ProductName
                                                  ).FirstOrDefault(),
                                    SerialNumber = _sal_TrRetailItem.SerialNumber,
                                    IMEI = _sal_TrRetailItem.IMEI,
                                    Qty = _sal_TrRetailItem.Qty,
                                    Price = _sal_TrRetailItem.Price,
                                    DiscCode = _sal_TrRetailItem.DiscountCode,
                                    DiscAmount = _sal_TrRetailItem.DiscountAmount,
                                    Total = _sal_TrRetailItem.Total,
                                    Remark = _sal_TrRetailItem.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new SAL_TrRetailItem(_row.POSItemCode, _row.TransNmbr, _row.PhoneTypeCode, _row.PhoneTypeName, _row.ProductCode, _row.ProductName, _row.SerialNumber, _row.IMEI, _row.Qty, _row.Price, _row.DiscCode, _row.DiscAmount, _row.Total, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<SAL_TrRetailItem> GetListSAL_TrRetailItem(string _prmCode)
        {
            List<SAL_TrRetailItem> _result = new List<SAL_TrRetailItem>();

            try
            {
                var _query = (
                                from _sal_TrRetailItem in this.db.SAL_TrRetailItems
                                where _sal_TrRetailItem.TransNmbr == _prmCode
                                orderby _sal_TrRetailItem.ProductCode ascending
                                select new
                                {
                                    POSItemCode = _sal_TrRetailItem.POSItemCode,
                                    TransNmbr = _sal_TrRetailItem.TransNmbr,
                                    PhoneTypeCode = _sal_TrRetailItem.PhoneTypeCode,
                                    PhoneTypeName = (
                                                       from _msPhoneType in this.db.Master_PhoneTypes
                                                       where _msPhoneType.PhoneTypeCode == _sal_TrRetailItem.PhoneTypeCode
                                                       select _msPhoneType.PhoneTypeDesc
                                                     ).FirstOrDefault(),
                                    ProductCode = _sal_TrRetailItem.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _sal_TrRetailItem.ProductCode
                                                    select _msProduct.ProductName
                                                  ).FirstOrDefault(),
                                    SerialNumber = _sal_TrRetailItem.SerialNumber,
                                    IMEI = _sal_TrRetailItem.IMEI,
                                    Qty = _sal_TrRetailItem.Qty,
                                    Price = _sal_TrRetailItem.Price,
                                    DiscCode = _sal_TrRetailItem.DiscountCode,
                                    DiscAmount = _sal_TrRetailItem.DiscountAmount,
                                    Total = _sal_TrRetailItem.Total,
                                    Remark = _sal_TrRetailItem.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new SAL_TrRetailItem(_row.POSItemCode, _row.TransNmbr, _row.PhoneTypeCode, _row.PhoneTypeName, _row.ProductCode, _row.ProductName, _row.SerialNumber, _row.IMEI, _row.Qty, _row.Price, _row.DiscCode, _row.DiscAmount, _row.Total, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public SAL_TrRetailItem GetSingleSAL_TrRetailItem(Guid _prmCode)
        {
            SAL_TrRetailItem _result = null;

            try
            {
                _result = this.db.SAL_TrRetailItems.Single(_temp => _temp.POSItemCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSAL_TrRetailItem(string[] _prmCode, String _prmTransNo)
        {
            bool _result = false;
            decimal _baseForex = 0;
            decimal _discAmount = 0;
            decimal _discPercent = 0;
            decimal _ppnAmount = 0;
            byte _ppnPercent = 0;
            decimal _total = 0;
            decimal _addFee = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    SAL_TrRetailItem _sal_TrRetailItem = this.db.SAL_TrRetailItems.Single(_temp => _temp.POSItemCode == new Guid(_prmCode[i]));

                    SAL_TrRetail _sal_TrRetail = this.GetSingleSAL_TrRetail(_prmTransNo);

                    decimal _amount = _sal_TrRetailItem.Total;

                    _baseForex = _sal_TrRetail.BaseForex - _amount;
                    _discPercent = _sal_TrRetail.DiscPercent;
                    _ppnPercent = _sal_TrRetail.PPNPercent;
                    _addFee = _sal_TrRetail.AdditionalFee;

                    if (_discPercent == 0)
                    {
                        _discAmount = _sal_TrRetail.DiscAmount;
                    }
                    else
                    {
                        _discAmount = _baseForex * _discPercent / 100;
                    }
                    if (_ppnPercent == 0)
                    {
                        _ppnAmount = _sal_TrRetail.PPNAmount;
                    }
                    else
                    {
                        _ppnAmount = (_baseForex - _discAmount) * _sal_TrRetail.PPNPercent / 100;
                    }

                    _total = _baseForex - _discAmount + _ppnAmount + _addFee;

                    _sal_TrRetail.BaseForex = _baseForex;
                    _sal_TrRetail.AdditionalFee = _addFee;
                    _sal_TrRetail.TotalAmount = _total;
                    _sal_TrRetail.DiscAmount = _discAmount;
                    _sal_TrRetail.DiscPercent = _discPercent;
                    _sal_TrRetail.PPNAmount = _ppnAmount;
                    _sal_TrRetail.PPNPercent = _ppnPercent;

                    this.db.SAL_TrRetailItems.DeleteOnSubmit(_sal_TrRetailItem);

                    MsProduct_SerialNumber _msProductSerialNmbr = this.db.MsProduct_SerialNumbers.Single(_temp => _temp.SerialNumber == _sal_TrRetailItem.SerialNumber);

                    _msProductSerialNmbr.IsSold = false;
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

        public bool AddSAL_TrRetailItem(SAL_TrRetailItem _prmSAL_TrRetailItem)
        {
            bool _result = false;
            decimal _baseForex = 0;
            decimal _discAmount = 0;
            decimal _discPercent = 0;
            decimal _ppnAmount = 0;
            byte _ppnPercent = 0;
            decimal _total = 0;
            decimal _addFee = 0;

            try
            {
                SAL_TrRetail _sal_TrRetail = this.db.SAL_TrRetails.Single(_temp => _temp.TransNmbr.ToLower() == _prmSAL_TrRetailItem.TransNmbr.ToLower());

                decimal _amount = _prmSAL_TrRetailItem.Total;

                _baseForex = _sal_TrRetail.BaseForex + _amount;
                _discPercent = _sal_TrRetail.DiscPercent;
                _ppnPercent = _sal_TrRetail.PPNPercent;
                _addFee = _sal_TrRetail.AdditionalFee;

                if (_discPercent == 0)
                {
                    _discAmount = _sal_TrRetail.DiscAmount;
                }
                else
                {
                    _discAmount = _baseForex * _discPercent / 100;
                }
                if (_ppnPercent == 0)
                {
                    _ppnAmount = _sal_TrRetail.PPNAmount;
                }
                else
                {
                    _ppnAmount = (_baseForex - _discAmount) * _sal_TrRetail.PPNPercent / 100;
                }

                _total = _baseForex - _discAmount + _ppnAmount + _addFee;

                _sal_TrRetail.AdditionalFee = _addFee;
                _sal_TrRetail.BaseForex = _baseForex;
                _sal_TrRetail.TotalAmount = _total;
                _sal_TrRetail.DiscAmount = _discAmount;
                _sal_TrRetail.DiscPercent = _discPercent;
                _sal_TrRetail.PPNAmount = _ppnAmount;
                _sal_TrRetail.PPNPercent = _ppnPercent;

                this.db.SAL_TrRetailItems.InsertOnSubmit(_prmSAL_TrRetailItem);

                MsProduct_SerialNumber _msProductSerialNmbr = this.db.MsProduct_SerialNumbers.Single(_temp => _temp.SerialNumber == _prmSAL_TrRetailItem.SerialNumber);

                _msProductSerialNmbr.IsSold = true;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSAL_TrRetailItem(SAL_TrRetailItem _prmSAL_TrRetailItem, decimal _prmAmountOriginal, string _prmSerialNmbr)
        {
            bool _result = false;
            decimal _baseForex = 0;
            decimal _discAmount = 0;
            decimal _discPercent = 0;
            decimal _ppnAmount = 0;
            byte _ppnPercent = 0;
            decimal _total = 0;
            decimal _addFee = 0;

            try
            {
                SAL_TrRetail _sal_TrRetail = this.GetSingleSAL_TrRetail(_prmSAL_TrRetailItem.TransNmbr);

                decimal _amount = _prmSAL_TrRetailItem.Total;

                _baseForex = _sal_TrRetail.BaseForex - _prmAmountOriginal; // delete amount original
                _baseForex = _baseForex + _amount; // add amount edited
                _discPercent = _sal_TrRetail.DiscPercent;
                _ppnPercent = _sal_TrRetail.PPNPercent;
                _addFee = _sal_TrRetail.AdditionalFee;

                if (_discPercent == 0)
                {
                    _discAmount = _sal_TrRetail.DiscAmount;
                }
                else
                {
                    _discAmount = _baseForex * _discPercent / 100;
                }
                if (_ppnPercent == 0)
                {
                    _ppnAmount = _sal_TrRetail.PPNAmount;
                }
                else
                {
                    _ppnAmount = (_baseForex - _discAmount) * _sal_TrRetail.PPNPercent / 100;
                }

                _total = _baseForex - _discAmount + _ppnAmount + _addFee;

                _sal_TrRetail.AdditionalFee = _addFee;
                _sal_TrRetail.BaseForex = _baseForex;
                _sal_TrRetail.TotalAmount = _total;
                _sal_TrRetail.DiscAmount = _discAmount;
                _sal_TrRetail.DiscPercent = _discPercent;
                _sal_TrRetail.PPNAmount = _ppnAmount;
                _sal_TrRetail.PPNPercent = _ppnPercent;

                MsProduct_SerialNumber _msProductSerialNmbr2 = this.db.MsProduct_SerialNumbers.Single(_temp => _temp.SerialNumber == _prmSerialNmbr);

                _msProductSerialNmbr2.IsSold = false;

                MsProduct_SerialNumber _msProductSerialNmbr = this.db.MsProduct_SerialNumbers.Single(_temp => _temp.SerialNumber == _prmSAL_TrRetailItem.SerialNumber);

                _msProductSerialNmbr.IsSold = true;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public string Close(string _prmTransNmbr, string _prmProduct, string _prmRemark, string _prmuser)
        //{
        //    string _result = "";
        //    string _errorMsg = "";

        //    try
        //    {
        //        //int _success = this.db.S_STTransferReqClosing(_prmTransNmbr, _prmProduct, _prmRemark, _prmuser, ref _errorMsg);

        //        //if (_errorMsg != "")
        //        //{
        //        //    _result = _errorMsg;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = "Close Failed";
        //        ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
        //    }

        //    return _result;
        //}

        #endregion

        ~RetailBL()
        {
        }
    }
}
