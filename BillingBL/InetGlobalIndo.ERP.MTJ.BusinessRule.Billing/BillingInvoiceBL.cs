using System;
using System.Web;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using System.Diagnostics;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class BillingInvoiceBL : Base
    {
        public BillingInvoiceBL()
        {

        }

        #region Billing.InvoiceHd

        public int RowsCountBillingInvoiceHd(string _prmCategory, string _prmKeyword)
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
                           from _billingInvoiceHd in this.db.Billing_InvoiceHds
                           join _msCustomer in this.db.MsCustomers
                                on _billingInvoiceHd.CustCode equals _msCustomer.CustCode
                           where
                                (SqlMethods.Like((_billingInvoiceHd.TransNmbr ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && (SqlMethods.Like(_billingInvoiceHd.Period.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                && (SqlMethods.Like(_billingInvoiceHd.Year.ToString().Trim().ToLower(), _pattern4.Trim().ToLower()))
                           select new
                           {
                               InvoiceHd = _billingInvoiceHd.InvoiceHd
                           }
                       ).Count();

            return _result;
        }

        public List<Billing_InvoiceHd> GetListBillingInvoiceHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Billing_InvoiceHd> _result = new List<Billing_InvoiceHd>();

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
                                from _billingInvoiceHd in this.db.Billing_InvoiceHds
                                join _msCustomer in this.db.MsCustomers
                                     on _billingInvoiceHd.CustCode equals _msCustomer.CustCode
                                where
                                    (SqlMethods.Like((_billingInvoiceHd.TransNmbr ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_billingInvoiceHd.Period.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_billingInvoiceHd.Year.ToString().Trim().ToLower(), _pattern4.Trim().ToLower()))
                                orderby _billingInvoiceHd.EditDate descending
                                select new
                                {
                                    InvoiceHd = _billingInvoiceHd.InvoiceHd,
                                    TransNmbr = _billingInvoiceHd.TransNmbr,
                                    CustName = _msCustomer.CustName,
                                    Status = _billingInvoiceHd.Status,
                                    CurrCode = _billingInvoiceHd.CurrCode,
                                    BaseForex = _billingInvoiceHd.BaseForex,
                                    PPNForex = _billingInvoiceHd.PPNForex,
                                    DiscForex = _billingInvoiceHd.DiscForex,
                                    StampFee = _billingInvoiceHd.StampFee,
                                    OtherFee = _billingInvoiceHd.OtherFee,
                                    TotalForex = _billingInvoiceHd.TotalForex
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Billing_InvoiceHd(_row.InvoiceHd, _row.TransNmbr, _row.CustName, _row.Status, _row.CurrCode, _row.BaseForex, _row.PPNForex, _row.DiscForex, _row.StampFee, _row.OtherFee, _row.TotalForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Billing_InvoiceHd GetSingleBillingInvoiceHd(Guid _prmCode)
        {
            Billing_InvoiceHd _result = null;

            try
            {
                _result = this.db.Billing_InvoiceHds.Single(_temp => _temp.InvoiceHd == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean AddBillingInvoiceHd(Billing_InvoiceHd _prmBillingInvoiceHd)
        {
            Boolean _result = false;

            try
            {
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmBillingInvoiceHd.Year, _prmBillingInvoiceHd.Period, AppModule.GetValue(TransactionType.BillingInvoice), this._companyTag, ""))

                this.db.Billing_InvoiceHds.InsertOnSubmit(_prmBillingInvoiceHd);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditBillingInvoiceHd(Billing_InvoiceHd _prmBillingInvoiceHd)
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

        public bool DeleteMultiBillingInvoiceHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Billing_InvoiceHd _prmBillingInvoiceHd = this.db.Billing_InvoiceHds.Single(_temp => _temp.InvoiceHd == new Guid(_prmCode[i]));

                    if (_prmBillingInvoiceHd != null)
                    {
                        var _finReceiptTradeCr = (
                                                    from _receiptTradeCr in this.db.FINReceiptTradeCrs
                                                    where _receiptTradeCr.InvoiceNo == _prmBillingInvoiceHd.TransNmbr
                                                    select _receiptTradeCr
                                                 ).Count();

                        //this.db.FINReceiptTradeCrs.Single(_temp => _temp.InvoiceNo == _prmBillingInvoiceHd.TransNmbr);
                        if (_finReceiptTradeCr == 0)
                        {
                            if ((_prmBillingInvoiceHd.TransNmbr ?? "").Trim() == "")
                            {
                                var _query = (from _detail in this.db.Billing_InvoiceDts
                                              where _detail.InvoiceHd == new Guid(_prmCode[i])
                                              select _detail);

                                this.db.Billing_InvoiceDts.DeleteAllOnSubmit(_query);

                                this.db.Billing_InvoiceHds.DeleteOnSubmit(_prmBillingInvoiceHd);

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

        public string GetTransactionNoFromBillingInvoiceHd(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _billingInvoiceHd in this.db.Billing_InvoiceHds
                                where _billingInvoiceHd.InvoiceHd == _prmCode
                                select new
                                {
                                    TransNmbr = _billingInvoiceHd.TransNmbr
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

        public string GetCustCodeFromBillingInvoiceHd(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _billingInvoiceHd in this.db.Billing_InvoiceHds
                                where _billingInvoiceHd.InvoiceHd == _prmCode
                                select new
                                {
                                    CustCode = _billingInvoiceHd.CustCode
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

        public string GetCurrCodeFromBillingInvoiceHd(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _billingInvoiceHd in this.db.Billing_InvoiceHds
                                where _billingInvoiceHd.InvoiceHd == _prmCode
                                select new
                                {
                                    CurrCode = _billingInvoiceHd.CurrCode
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

        public char GetStatusInvoiceHd(Guid _prmCode)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _billingInvoiceHd in this.db.Billing_InvoiceHds
                                where _billingInvoiceHd.InvoiceHd == _prmCode
                                select new
                                {
                                    Status = _billingInvoiceHd.Status
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

        public DateTime GetTransDateInvoiceHd(Guid _prmCode)
        {
            DateTime _result = new DateTime();

            try
            {
                var _query = (
                                from _billingInvoiceHd in this.db.Billing_InvoiceHds
                                where _billingInvoiceHd.InvoiceHd == _prmCode
                                select new
                                {
                                    TransDate = _billingInvoiceHd.TransDate
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.TransDate;
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
                this.db.S_BillingInvoiceGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

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

        public string Approve(Guid _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_BillingInvoiceApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        Billing_InvoiceHd _billingInvoice = this.GetSingleBillingInvoiceHd(_prmCode);

                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_billingInvoice.TransDate.Year, _billingInvoice.TransDate.Month, AppModule.GetValue(TransactionType.BillingInvoice), this._companyTag, ""))
                        {
                            _billingInvoice.TransNmbr = item.Number;
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

        public string Posting(Guid _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                Billing_InvoiceHd _billingInvoiceHd = this.db.Billing_InvoiceHds.Single(_temp => _temp.InvoiceHd == _prmCode);
                String _locked = _transCloseBL.IsExistAndLocked(_billingInvoiceHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_BillingInvoicePost(_prmCode, 0, 0, _prmuser, ref _result);

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

        public string UnPosting(Guid _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                Billing_InvoiceHd _billingInvoiceHd = this.db.Billing_InvoiceHds.Single(_temp => _temp.InvoiceHd == _prmCode);
                String _locked = _transCloseBL.IsExistAndLocked(_billingInvoiceHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_BillingInvoiceUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";
                    }
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

        public string Generate(int _prmPeriod, int _prmYear, string _prmCustomerGroup, string _prmCustomerType, string _prmCustomerCode)
        {
            string _result = "";

            Guid _companyId = new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name);
            string _companyTag = new UserBL().GetCompanyTag(_companyId);

            string _param1 = TaxDataMapper.GetStatusTaxTransactionCode(TaxTransactionCode.One);
            char _param2 = TaxDataMapper.GetStatusTaxStatus(TaxStatus.Zero);
            string _companyTaxBranchNo = new UserBL().GetCompanyTaxBranchNo(_companyId);

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                String _locked = _transCloseBL.IsExistAndLocked(DateTime.Now);
                if (_locked == "")
                {
                    this.db.S_BillingGenerateInvoice(_prmPeriod, _prmYear, _prmCustomerGroup, _prmCustomerType, _prmCustomerCode, _param1, _param2, _companyTaxBranchNo, AppModule.GetValue(TransactionType.BillingInvoice), _companyTag, HttpContext.Current.User.Identity.Name, ref _result);
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "You Failed Generate Invoice Service, " + ex.Message;
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public Boolean UpdateFgSendEmail(String _prmCode, Boolean _prmUpdateValue)
        {
            Boolean _result = false;

            try
            {
                Billing_InvoiceHd _bilInv = this.db.Billing_InvoiceHds.Single(_temp => _temp.InvoiceHd.ToString() == _prmCode);
                _bilInv.FgSendEmail = _prmUpdateValue;

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

        #region Billing.InvoiceDt

        public int RowsCountBillingInvoiceDt(Guid _prmCode)
        {
            int _result = 0;

            _result = this.db.Billing_InvoiceDts.Where(_row => _row.InvoiceHd == _prmCode).Count();

            return _result;
        }

        public List<Billing_InvoiceDt> GetListBillingInvoiceDt(int _prmReqPage, int _prmPageSize, Guid _prmCode)
        {
            List<Billing_InvoiceDt> _result = new List<Billing_InvoiceDt>();

            try
            {
                var _query = (
                                from _billingInvoiceDt in this.db.Billing_InvoiceDts
                                join _masterCustBillAccount in this.db.Master_CustBillAccounts
                                    on _billingInvoiceDt.CustBillCode equals _masterCustBillAccount.CustBillCode
                                where _billingInvoiceDt.InvoiceHd == _prmCode
                                orderby _masterCustBillAccount.CustBillAccount ascending
                                select new
                                {
                                    InvoiceDt = _billingInvoiceDt.InvoiceDt,
                                    InvoiceHd = _billingInvoiceDt.InvoiceHd,
                                    CustBillAccount = _masterCustBillAccount.CustBillAccount,
                                    CustBillDescription = _billingInvoiceDt.CustBillDescription,
                                    ProductName = (
                                                    from _masterCustBillAccount2 in this.db.Master_CustBillAccounts
                                                    join _msProduct in this.db.MsProducts
                                                        on _masterCustBillAccount2.ProductCode equals _msProduct.ProductCode
                                                    where _masterCustBillAccount2.CustBillCode == _billingInvoiceDt.CustBillCode
                                                    select _msProduct.ProductName
                                                   ).FirstOrDefault(),
                                    AmountForex = _billingInvoiceDt.AmountForex,
                                    Remark = _billingInvoiceDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Billing_InvoiceDt(_row.InvoiceDt, _row.InvoiceHd, _row.CustBillAccount, _row.CustBillDescription, _row.ProductName, _row.AmountForex, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Billing_InvoiceDt GetSingleBillingInvoiceDt(Guid _prmDtCode)
        {
            Billing_InvoiceDt _result = null;

            try
            {
                _result = this.db.Billing_InvoiceDts.Single(_temp => _temp.InvoiceDt == _prmDtCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiBillingInvoiceDt(string[] _prmDtCode, Guid _prmInvoiceHd)
        {
            bool _result = false;

            decimal _tempBaseForex = 0;
            decimal _tempDiscForex = 0;
            decimal _tempPPNForex = 0;
            decimal _tempStampFee = 0;
            decimal _tempOtherFee = 0;
            decimal _tempTotalForex = 0;

            try
            {
                for (int i = 0; i < _prmDtCode.Length; i++)
                {
                    Billing_InvoiceDt _billingInvoiceDt = this.db.Billing_InvoiceDts.Single(_temp => _temp.InvoiceDt == new Guid(_prmDtCode[i]));

                    Billing_InvoiceHd _billingInvoiceHd = this.GetSingleBillingInvoiceHd(_prmInvoiceHd);

                    decimal _amount = _billingInvoiceDt.AmountForex;
                    int _decimalPlace = Convert.ToInt32(new CurrencyBL().GetDecimalPlace(_billingInvoiceHd.CurrCode));
                    _tempBaseForex = Math.Round(Convert.ToDecimal(_billingInvoiceHd.BaseForex) - _amount, _decimalPlace);
                    if (_billingInvoiceHd.DiscPercent != 0)
                    {
                        _tempDiscForex = Math.Round(Convert.ToDecimal(_tempBaseForex * _billingInvoiceHd.DiscPercent / 100), _decimalPlace);
                    }
                    else
                    {
                        _tempDiscForex = Math.Round(Convert.ToDecimal(_billingInvoiceHd.DiscForex), _decimalPlace);
                    }
                    _tempPPNForex = Math.Round((_tempBaseForex - _tempDiscForex) * (Convert.ToDecimal(_billingInvoiceHd.PPN) / 100), _decimalPlace);
                    _tempStampFee = Convert.ToDecimal(_billingInvoiceHd.StampFee);
                    _tempOtherFee = Convert.ToDecimal(_billingInvoiceHd.OtherFee);
                    _tempTotalForex = Math.Round((_tempBaseForex - _tempDiscForex + _tempPPNForex + _tempStampFee + _tempOtherFee), _decimalPlace);

                    _billingInvoiceHd.BaseForex = _tempBaseForex;

                    if (_tempBaseForex > 0)
                    {
                        _billingInvoiceHd.DiscForex = _tempDiscForex;
                        _billingInvoiceHd.PPNForex = _tempPPNForex;
                        _billingInvoiceHd.StampFee = _tempStampFee;
                        _billingInvoiceHd.OtherFee = _tempOtherFee;
                        _billingInvoiceHd.TotalForex = _tempTotalForex;
                    }
                    else
                    {
                        _billingInvoiceHd.DiscForex = 0;
                        _billingInvoiceHd.PPNForex = 0;
                        _billingInvoiceHd.StampFee = 0;
                        _billingInvoiceHd.OtherFee = 0;
                        _billingInvoiceHd.TotalForex = 0;
                    }
                    this.db.Billing_InvoiceDts.DeleteOnSubmit(_billingInvoiceDt);
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

        public bool AddBillingInvoiceDt(Billing_InvoiceDt _prmBillingInvoiceDt)
        {
            bool _result = false;

            decimal _total = 0;

            try
            {
                if (this.IsCustBillAccountExist(_prmBillingInvoiceDt.InvoiceDt, _prmBillingInvoiceDt.InvoiceHd, _prmBillingInvoiceDt.CustBillCode) == false)
                {
                    var _query = (
                               from _invoiceDt in this.db.Billing_InvoiceDts
                               where !(
                                           from _invoiceDt2 in this.db.Billing_InvoiceDts
                                           where _invoiceDt2.InvoiceDt == _prmBillingInvoiceDt.InvoiceDt
                                           select _invoiceDt2.InvoiceDt
                                       ).Contains(_invoiceDt.InvoiceDt)
                                       && _invoiceDt.InvoiceHd == _prmBillingInvoiceDt.InvoiceHd
                               group _invoiceDt by new { _invoiceDt.InvoiceHd } into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                    foreach (var _obj in _query)
                    {
                        _total = _obj.AmountForex;
                    }

                    Billing_InvoiceHd _invoiceHd = this.GetSingleBillingInvoiceHd(_prmBillingInvoiceDt.InvoiceHd);
                    int _decimalPlace = Convert.ToInt32(new CurrencyBL().GetDecimalPlace(_invoiceHd.CurrCode));
                    _invoiceHd.BaseForex = Math.Round((_total + _prmBillingInvoiceDt.AmountForex), _decimalPlace);
                    if (_invoiceHd.DiscPercent != 0)
                    {
                        _invoiceHd.DiscForex = Math.Round(Convert.ToDecimal(_invoiceHd.BaseForex * _invoiceHd.DiscPercent / 100), _decimalPlace);
                    }
                    _invoiceHd.PPNForex = Math.Round(Convert.ToDecimal((_invoiceHd.BaseForex - _invoiceHd.DiscForex) * _invoiceHd.PPN / 100), _decimalPlace);
                    _invoiceHd.TotalForex = Math.Round(Convert.ToDecimal(_invoiceHd.BaseForex - _invoiceHd.DiscForex + _invoiceHd.PPNForex + _invoiceHd.StampFee + _invoiceHd.OtherFee), _decimalPlace);

                    this.db.Billing_InvoiceDts.InsertOnSubmit(_prmBillingInvoiceDt);

                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditBillingInvoiceDt(Billing_InvoiceDt _prmBillingInvoiceDt)
        {
            bool _result = false;

            decimal _total = 0;

            try
            {
                if (this.IsCustBillAccountExist(_prmBillingInvoiceDt.InvoiceDt, _prmBillingInvoiceDt.InvoiceHd, _prmBillingInvoiceDt.CustBillCode) == false)
                {
                    var _query = (
                                   from _invoiceDt in this.db.Billing_InvoiceDts
                                   where !(
                                               from _invoiceDt2 in this.db.Billing_InvoiceDts
                                               where _invoiceDt2.InvoiceDt == _prmBillingInvoiceDt.InvoiceDt
                                               select _invoiceDt2.InvoiceDt
                                           ).Contains(_invoiceDt.InvoiceDt)
                                           && _invoiceDt.InvoiceHd == _prmBillingInvoiceDt.InvoiceHd
                                   group _invoiceDt by new { _invoiceDt.InvoiceHd } into _grp
                                   select new
                                   {
                                       AmountForex = _grp.Sum(a => a.AmountForex)
                                   }
                                 );

                    foreach (var _obj in _query)
                    {
                        _total = _obj.AmountForex;
                    }

                    Billing_InvoiceHd _invoiceHd = this.GetSingleBillingInvoiceHd(_prmBillingInvoiceDt.InvoiceHd);
                    int _decimalPlace = Convert.ToInt32(new CurrencyBL().GetDecimalPlace(_invoiceHd.CurrCode));
                    _invoiceHd.BaseForex = Math.Round((_total + _prmBillingInvoiceDt.AmountForex), _decimalPlace);
                    if (_invoiceHd.DiscPercent != 0)
                    {
                        _invoiceHd.DiscForex = Math.Round(Convert.ToDecimal(_invoiceHd.BaseForex * _invoiceHd.DiscPercent / 100), _decimalPlace);
                    }
                    _invoiceHd.PPNForex = Math.Round(Convert.ToDecimal((_invoiceHd.BaseForex - _invoiceHd.DiscForex) * _invoiceHd.PPN / 100), _decimalPlace);
                    _invoiceHd.TotalForex = Math.Round(Convert.ToDecimal(_invoiceHd.BaseForex - _invoiceHd.DiscForex + _invoiceHd.PPNForex + _invoiceHd.StampFee + _invoiceHd.OtherFee), _decimalPlace);

                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        private bool IsCustBillAccountExist(Guid _prmInvoiceDt, Guid _prmInvoiceHd, Guid _prmCustBillCode)
        {
            bool _result = false;

            try
            {
                var _query = from _invoiceDt in this.db.Billing_InvoiceDts
                             where (_invoiceDt.InvoiceHd == _prmInvoiceHd) && (_invoiceDt.CustBillCode == _prmCustBillCode) && (_invoiceDt.InvoiceDt != _prmInvoiceDt)
                             select new
                             {
                                 _invoiceDt.InvoiceDt
                             };

                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~BillingInvoiceBL()
        {

        }
    }
}
