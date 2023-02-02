using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using System.Data.Linq.SqlClient;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class CustomerInvoiceBL : Base
    {
        public CustomerInvoiceBL()
        {

        }

        #region Billing.CustomerInvoiceHd

        public int RowsCountCustomerInvoiceHd(string _prmCategory, string _prmKeyword)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "TransNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "CustomerName")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "Period")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "Year")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%" + _prmKeyword + "%";
            }

            _result =
                       (
                           from _customerInvoiceHd in this.db.Billing_CustomerInvoiceHds
                           join _msCustomer in this.db.MsCustomers
                                on _customerInvoiceHd.CustCode equals _msCustomer.CustCode
                           where
                                (SqlMethods.Like((_customerInvoiceHd.TransNmbr ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && (SqlMethods.Like(_customerInvoiceHd.Period.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                && (SqlMethods.Like(_customerInvoiceHd.Year.ToString().Trim().ToLower(), _pattern4.Trim().ToLower()))
                           select new
                           {
                               CustomerInvoiceHdCode = _customerInvoiceHd.CustomerInvoiceHdCode
                           }
                       ).Count();

            return _result;
        }

        public List<Billing_CustomerInvoiceHd> GetListCustomerInvoiceHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Billing_CustomerInvoiceHd> _result = new List<Billing_CustomerInvoiceHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "TransNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "CustomerName")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "Period")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "Year")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _customerInvoiceHd in this.db.Billing_CustomerInvoiceHds
                                join _msCustomer in this.db.MsCustomers
                                     on _customerInvoiceHd.CustCode equals _msCustomer.CustCode
                                where
                                     (SqlMethods.Like((_customerInvoiceHd.TransNmbr ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                     && (SqlMethods.Like(_customerInvoiceHd.Period.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                     && (SqlMethods.Like(_customerInvoiceHd.Year.ToString().Trim().ToLower(), _pattern4.Trim().ToLower()))
                                orderby _customerInvoiceHd.EditDate descending
                                select new
                                {
                                    CustomerInvoiceHdCode = _customerInvoiceHd.CustomerInvoiceHdCode,
                                    TransNmbr = _customerInvoiceHd.TransNmbr,
                                    CustName = _msCustomer.CustName,
                                    Status = _customerInvoiceHd.Status,
                                    CurrCode = _customerInvoiceHd.CurrCode,
                                    BaseForex = _customerInvoiceHd.BaseForex,
                                    PPNForex = _customerInvoiceHd.PPNForex,
                                    DiscForex = _customerInvoiceHd.DiscForex,
                                    OtherFee = _customerInvoiceHd.OtherFee,
                                    StampFee = _customerInvoiceHd.StampFee,
                                    CommissionExpense = _customerInvoiceHd.CommissionExpense,
                                    TotalForex = _customerInvoiceHd.TotalForex
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Billing_CustomerInvoiceHd(_row.CustomerInvoiceHdCode, _row.TransNmbr, _row.CustName, _row.Status, _row.CurrCode, _row.BaseForex, _row.PPNForex, _row.DiscForex, _row.OtherFee, _row.StampFee, _row.CommissionExpense, _row.TotalForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Billing_CustomerInvoiceHd GetSingleCustomerInvoiceHd(Guid _prmCode)
        {
            Billing_CustomerInvoiceHd _result = null;

            try
            {
                _result = this.db.Billing_CustomerInvoiceHds.Single(_temp => _temp.CustomerInvoiceHdCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean AddCustomerInvoiceHd(Billing_CustomerInvoiceHd _prmCustomerInvoiceHd)
        {
            Boolean _result = false;

            try
            {
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmCustomerInvoiceHd.Year, _prmCustomerInvoiceHd.Period, AppModule.GetValue(TransactionType.CustomerInvoice), this._companyTag, ""))

                this.db.Billing_CustomerInvoiceHds.InsertOnSubmit(_prmCustomerInvoiceHd);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditCustomerInvoiceHd(Billing_CustomerInvoiceHd _prmCustomerInvoiceHd)
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

        public bool DeleteMultiCustomerInvoiceHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Billing_CustomerInvoiceHd _prmCustomerInvoiceHd = this.GetSingleCustomerInvoiceHd(new Guid(_prmCode[i]));

                    int _query2 = (
                                    from _finReceiptTradeCrs in this.db.FINReceiptTradeCrs
                                    where _finReceiptTradeCrs.InvoiceNo == _prmCustomerInvoiceHd.TransNmbr
                                    select _finReceiptTradeCrs
                                 ).Count();

                    if (_prmCustomerInvoiceHd != null)
                    {
                        if (_query2 == 0)
                        {
                            if ((_prmCustomerInvoiceHd.TransNmbr ?? "").Trim() == "")
                            {
                                var _query = (from _detail in this.db.Billing_CustomerInvoiceDts
                                              where _detail.CustomerInvoiceHdCode == new Guid(_prmCode[i])
                                              select _detail);

                                this.db.Billing_CustomerInvoiceDts.DeleteAllOnSubmit(_query);

                                this.db.Billing_CustomerInvoiceHds.DeleteOnSubmit(_prmCustomerInvoiceHd);

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

                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetTransactionNoFromCustomerInvoiceHd(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _customerInvoiceHd in this.db.Billing_CustomerInvoiceHds
                                where _customerInvoiceHd.CustomerInvoiceHdCode == _prmCode
                                select new
                                {
                                    TransNmbr = _customerInvoiceHd.TransNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.TransNmbr;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCustCodeFromCustomerInvoiceHd(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _customerInvoiceHd in this.db.Billing_CustomerInvoiceHds
                                where _customerInvoiceHd.CustomerInvoiceHdCode == _prmCode
                                select new
                                {
                                    CustCode = _customerInvoiceHd.CustCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.CustCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCurrCodeFromCustomerInvoiceHd(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _customerInvoiceHd in this.db.Billing_CustomerInvoiceHds
                                where _customerInvoiceHd.CustomerInvoiceHdCode == _prmCode
                                select new
                                {
                                    CurrCode = _customerInvoiceHd.CurrCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.CurrCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public char GetStatusCustomerInvoiceHd(Guid _prmCode)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _customerInvoiceHd in this.db.Billing_CustomerInvoiceHds
                                where _customerInvoiceHd.CustomerInvoiceHdCode == _prmCode
                                select new
                                {
                                    Status = _customerInvoiceHd.Status
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.Status;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(Guid _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_BillingCustomerInvoiceGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(Guid _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_BillingCustomerInvoiceApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        Billing_CustomerInvoiceHd _customerInvoiceHd = this.GetSingleCustomerInvoiceHd(_prmCode);

                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_customerInvoiceHd.TransDate.Year, _customerInvoiceHd.TransDate.Month, AppModule.GetValue(TransactionType.CustomerInvoice), this._companyTag, ""))
                        {
                            _customerInvoiceHd.TransNmbr = item.Number;
                        }

                        this.db.SubmitChanges();

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

        public string Posting(Guid _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                Billing_CustomerInvoiceHd _custBillInvoiceHd = this.db.Billing_CustomerInvoiceHds.Single(_temp => _temp.CustomerInvoiceHdCode == _prmCode);
                String _locked = _transCloseBL.IsExistAndLocked(_custBillInvoiceHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_BillingCustomerInvoicePost(_prmCode, 0, 0, _prmuser, ref _result);
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

        public string UnPosting(Guid _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                Billing_CustomerInvoiceHd _custBillInvoiceHd = this.db.Billing_CustomerInvoiceHds.Single(_temp => _temp.CustomerInvoiceHdCode == _prmCode);
                String _locked = _transCloseBL.IsExistAndLocked(_custBillInvoiceHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_BillingCustomerInvoiceUnPost(_prmCode, 0, 0, _prmuser, ref _result);
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public byte GetTypeCustomerInvoiceHd(Guid _prmCode)
        {
            byte _result = 0;

            try
            {
                var _query = (
                                from _customerInvoiceHd in this.db.Billing_CustomerInvoiceHds
                                where _customerInvoiceHd.CustomerInvoiceHdCode == _prmCode
                                select new
                                {
                                    Type = _customerInvoiceHd.Type
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.Type;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region Billing.CustomerInvoiceDt

        public int RowsCountCustomerInvoiceDt(Guid _prmCode)
        {
            int _result = 0;

            _result = this.db.Billing_CustomerInvoiceDts.Where(_row => _row.CustomerInvoiceHdCode == _prmCode).Count();

            return _result;
        }

        public List<Billing_CustomerInvoiceDt> GetListCustomerInvoiceDt(int _prmReqPage, int _prmPageSize, Guid _prmCode)
        {
            List<Billing_CustomerInvoiceDt> _result = new List<Billing_CustomerInvoiceDt>();

            try
            {
                var _query = (
                                from _customerInvoiceDt in this.db.Billing_CustomerInvoiceDts
                                where _customerInvoiceDt.CustomerInvoiceHdCode == _prmCode
                                select new
                                {
                                    CustomerInvoiceDtCode = _customerInvoiceDt.CustomerInvoiceDtCode,
                                    CustomerInvoiceHdCode = _customerInvoiceDt.CustomerInvoiceHdCode,
                                    CustInvoiceDescription = _customerInvoiceDt.CustInvoiceDescription,
                                    AmountForex = _customerInvoiceDt.AmountForex,
                                    Remark = _customerInvoiceDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Billing_CustomerInvoiceDt(_row.CustomerInvoiceDtCode, _row.CustomerInvoiceHdCode, _row.CustInvoiceDescription, _row.AmountForex, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Billing_CustomerInvoiceDt GetSingleCustomerInvoiceDt(Guid _prmDtCode)
        {
            Billing_CustomerInvoiceDt _result = null;

            try
            {
                _result = this.db.Billing_CustomerInvoiceDts.Single(_temp => _temp.CustomerInvoiceDtCode == _prmDtCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Billing_CustomerInvoiceDtExtension GetSingleCustomerInvoiceDtExtension(Guid _prmDtCode)
        {
            Billing_CustomerInvoiceDtExtension _result = null;

            try
            {
                _result = this.db.Billing_CustomerInvoiceDtExtensions.Single(_temp => _temp.CustomerInvoiceDtCode == _prmDtCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiCustomerInvoiceDt(string[] _prmDtCode, Guid _prmInvoiceHd)
        {
            bool _result = false;

            decimal _tempBaseForex = 0;
            decimal _tempDiscForex = 0;
            decimal _tempPPNForex = 0;
            decimal _tempOtherFee = 0;
            decimal _tempStampFee = 0;
            decimal _tempCommissionExpense = 0;
            decimal _tempTotalForex = 0;

            try
            {
                for (int i = 0; i < _prmDtCode.Length; i++)
                {
                    Billing_CustomerInvoiceDt _customerInvoiceDt = this.db.Billing_CustomerInvoiceDts.Single(_temp => _temp.CustomerInvoiceDtCode == new Guid(_prmDtCode[i]));

                    Billing_CustomerInvoiceHd _customerInvoiceHd = this.GetSingleCustomerInvoiceHd(_prmInvoiceHd);

                    decimal _amount = _customerInvoiceDt.AmountForex;
                    int _decimalPlace = Convert.ToInt32(new CurrencyBL().GetDecimalPlace(_customerInvoiceHd.CurrCode));

                    _tempBaseForex = Math.Round((Convert.ToDecimal(_customerInvoiceHd.BaseForex) - _amount), _decimalPlace);
                    if (_customerInvoiceHd.DiscPercent != 0)
                    {
                        _tempDiscForex = Math.Round(Convert.ToDecimal(_tempBaseForex * _customerInvoiceHd.DiscPercent / 100), _decimalPlace);
                    }
                    else
                    {
                        _tempDiscForex = Math.Round(Convert.ToDecimal(_customerInvoiceHd.DiscForex), _decimalPlace);
                    }
                    _tempPPNForex = Math.Round(((_tempBaseForex - _tempDiscForex) * (Convert.ToDecimal(_customerInvoiceHd.PPN) / 100)), _decimalPlace);
                    _tempOtherFee = Convert.ToDecimal(_customerInvoiceHd.OtherFee);
                    _tempStampFee = Convert.ToDecimal(_customerInvoiceHd.StampFee);
                    _tempCommissionExpense = Convert.ToDecimal(_customerInvoiceHd.CommissionExpense);
                    _tempTotalForex = Math.Round((_tempBaseForex - _tempDiscForex + _tempPPNForex + _tempOtherFee + _tempStampFee - _tempCommissionExpense), _decimalPlace);

                    _customerInvoiceHd.BaseForex = _tempBaseForex;

                    if (_tempBaseForex > 0)
                    {
                        _customerInvoiceHd.DiscForex = _tempDiscForex;
                        _customerInvoiceHd.PPNForex = _tempPPNForex;
                        _customerInvoiceHd.StampFee = _tempStampFee;
                        _customerInvoiceHd.OtherFee = _tempOtherFee;
                        _customerInvoiceHd.CommissionExpense = _tempCommissionExpense;
                        _customerInvoiceHd.TotalForex = _tempTotalForex;
                    }
                    else
                    {
                        _customerInvoiceHd.DiscForex = 0;
                        _customerInvoiceHd.PPNForex = 0;
                        _customerInvoiceHd.StampFee = 0;
                        _customerInvoiceHd.OtherFee = 0;
                        _customerInvoiceHd.CommissionExpense = 0;
                        _customerInvoiceHd.TotalForex = 0;
                    }
                    this.db.Billing_CustomerInvoiceDts.DeleteOnSubmit(_customerInvoiceDt);
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

        public bool AddCustomerInvoiceDt(Billing_CustomerInvoiceDt _prmCustomerInvoiceDt, Billing_CustomerInvoiceDtExtension _prmCustomerInvoiceDtExtension)
        {
            bool _result = false;

            decimal _total = 0;

            try
            {
                var _query = (
                           from _customerInvoiceDt in this.db.Billing_CustomerInvoiceDts
                           where !(
                                       from _customerInvoiceDt2 in this.db.Billing_CustomerInvoiceDts
                                       where _customerInvoiceDt2.CustomerInvoiceDtCode == _prmCustomerInvoiceDt.CustomerInvoiceDtCode
                                       select _customerInvoiceDt2.CustomerInvoiceDtCode
                                   ).Contains(_customerInvoiceDt.CustomerInvoiceDtCode)
                                   && _customerInvoiceDt.CustomerInvoiceHdCode == _prmCustomerInvoiceDt.CustomerInvoiceHdCode
                           group _customerInvoiceDt by new { _customerInvoiceDt.CustomerInvoiceHdCode } into _grp
                           select new
                           {
                               AmountForex = _grp.Sum(a => a.AmountForex)
                           }
                         );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                Billing_CustomerInvoiceHd _customerInvoiceHd = this.GetSingleCustomerInvoiceHd(_prmCustomerInvoiceDt.CustomerInvoiceHdCode);
                int _decimalPlace = Convert.ToInt32(new CurrencyBL().GetDecimalPlace(_customerInvoiceHd.CurrCode));
                _customerInvoiceHd.BaseForex = Math.Round(_total + _prmCustomerInvoiceDt.AmountForex, _decimalPlace);
                if (_customerInvoiceHd.DiscPercent != 0)
                {
                    _customerInvoiceHd.DiscForex = Math.Round(Convert.ToDecimal(_customerInvoiceHd.BaseForex * _customerInvoiceHd.DiscPercent / 100), _decimalPlace);
                }
                _customerInvoiceHd.PPNForex = Math.Round(Convert.ToDecimal((_customerInvoiceHd.BaseForex - _customerInvoiceHd.DiscForex) * _customerInvoiceHd.PPN / 100), _decimalPlace);
                _customerInvoiceHd.TotalForex = Math.Round(Convert.ToDecimal(_customerInvoiceHd.BaseForex - _customerInvoiceHd.DiscForex + _customerInvoiceHd.PPNForex + _customerInvoiceHd.OtherFee + _customerInvoiceHd.StampFee - _customerInvoiceHd.CommissionExpense), _decimalPlace);

                this.db.Billing_CustomerInvoiceDts.InsertOnSubmit(_prmCustomerInvoiceDt);

                if (_prmCustomerInvoiceDtExtension != null)
                {
                    Billing_CustomerInvoiceDtExtension _customerInvoiceDtExtension = this.GetSingleCustomerInvoiceDtExtension(_prmCustomerInvoiceDtExtension.CustomerInvoiceDtCode);
                    if (_customerInvoiceDtExtension != null)
                    {
                        this.db.Billing_CustomerInvoiceDtExtensions.DeleteOnSubmit(_customerInvoiceDtExtension);
                    }

                    this.db.Billing_CustomerInvoiceDtExtensions.InsertOnSubmit(_prmCustomerInvoiceDtExtension);
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

        public bool EditCustomerInvoiceDt(Billing_CustomerInvoiceDt _prmCustomerInvoiceDt, Billing_CustomerInvoiceDtExtension _prmCustomerInvoiceDtExtension)
        {
            bool _result = false;

            decimal _total = 0;

            try
            {
                var _query = (
                          from _customerInvoiceDt in this.db.Billing_CustomerInvoiceDts
                          where !(
                                      from _customerInvoiceDt2 in this.db.Billing_CustomerInvoiceDts
                                      where _customerInvoiceDt2.CustomerInvoiceDtCode == _prmCustomerInvoiceDt.CustomerInvoiceDtCode
                                      select _customerInvoiceDt2.CustomerInvoiceDtCode
                                  ).Contains(_customerInvoiceDt.CustomerInvoiceDtCode)
                                  && _customerInvoiceDt.CustomerInvoiceHdCode == _prmCustomerInvoiceDt.CustomerInvoiceHdCode
                          group _customerInvoiceDt by new { _customerInvoiceDt.CustomerInvoiceHdCode } into _grp
                          select new
                          {
                              AmountForex = _grp.Sum(a => a.AmountForex)
                          }
                        );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                Billing_CustomerInvoiceHd _customerInvoiceHd = this.GetSingleCustomerInvoiceHd(_prmCustomerInvoiceDt.CustomerInvoiceHdCode);
                int _decimalPlace = Convert.ToInt32(new CurrencyBL().GetDecimalPlace(_customerInvoiceHd.CurrCode));
                _customerInvoiceHd.BaseForex = Math.Round(_total + _prmCustomerInvoiceDt.AmountForex, _decimalPlace);
                if (_customerInvoiceHd.DiscPercent != 0)
                {
                    _customerInvoiceHd.DiscForex = Math.Round(Convert.ToDecimal(_customerInvoiceHd.BaseForex * _customerInvoiceHd.DiscPercent / 100), _decimalPlace);
                }
                _customerInvoiceHd.PPNForex = Math.Round(Convert.ToDecimal((_customerInvoiceHd.BaseForex - _customerInvoiceHd.DiscForex) * _customerInvoiceHd.PPN / 100), _decimalPlace);
                _customerInvoiceHd.TotalForex = Math.Round(Convert.ToDecimal(_customerInvoiceHd.BaseForex - _customerInvoiceHd.DiscForex + _customerInvoiceHd.PPNForex + _customerInvoiceHd.OtherFee + _customerInvoiceHd.StampFee - _customerInvoiceHd.CommissionExpense), _decimalPlace);

                if (_prmCustomerInvoiceDtExtension != null)
                {
                    Billing_CustomerInvoiceDtExtension _customerInvoiceDtExtension = this.GetSingleCustomerInvoiceDtExtension(_prmCustomerInvoiceDtExtension.CustomerInvoiceDtCode);
                    if (_customerInvoiceDtExtension != null)
                    {
                        this.db.Billing_CustomerInvoiceDtExtensions.DeleteOnSubmit(_customerInvoiceDtExtension);
                    }

                    this.db.Billing_CustomerInvoiceDtExtensions.InsertOnSubmit(_prmCustomerInvoiceDtExtension);
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

        #endregion

        ~CustomerInvoiceBL()
        {

        }
    }
}
