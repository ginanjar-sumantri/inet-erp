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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class SupplierInvoiceBL : Base
    {
        public SupplierInvoiceBL()
        {

        }

        #region Billing.SupplierInvoiceHd

        private PaymentTradeBL _payTradeBL = new PaymentTradeBL();

        public int RowsCountSupplierInvoiceHd(string _prmCategory, string _prmKeyword)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
            }

            _result = (
                        from _supplierInvoiceHd in this.db.Billing_SupplierInvoiceHds
                        join _msSupplier in this.db.MsSuppliers
                             on _supplierInvoiceHd.SuppCode equals _msSupplier.SuppCode
                        where (SqlMethods.Like((_supplierInvoiceHd.TransNmbr ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                             && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                        select new
                        {
                            SupplierInvoiceHdCode = _supplierInvoiceHd.SupplierInvoiceHdCode
                        }
                       ).Count();

            return _result;
        }

        public List<Billing_SupplierInvoiceHd> GetListSupplierInvoiceHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Billing_SupplierInvoiceHd> _result = new List<Billing_SupplierInvoiceHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _supplierInvoiceHd in this.db.Billing_SupplierInvoiceHds
                                join _msSupplier in this.db.MsSuppliers
                                     on _supplierInvoiceHd.SuppCode equals _msSupplier.SuppCode
                                where (SqlMethods.Like((_supplierInvoiceHd.TransNmbr ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _supplierInvoiceHd.EditDate descending
                                select new
                                {
                                    SupplierInvoiceHdCode = _supplierInvoiceHd.SupplierInvoiceHdCode,
                                    TransNmbr = _supplierInvoiceHd.TransNmbr,
                                    SuppName = _msSupplier.SuppName,
                                    Status = _supplierInvoiceHd.Status,
                                    CurrCode = _supplierInvoiceHd.CurrCode,
                                    BaseForex = _supplierInvoiceHd.BaseForex,
                                    PPNForex = _supplierInvoiceHd.PPNForex,
                                    DiscForex = _supplierInvoiceHd.DiscForex,
                                    OtherFee = _supplierInvoiceHd.OtherFee,
                                    StampFee = _supplierInvoiceHd.StampFee,
                                    TotalForex = _supplierInvoiceHd.TotalForex
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Billing_SupplierInvoiceHd(_row.SupplierInvoiceHdCode, _row.TransNmbr, _row.SuppName, _row.Status, _row.CurrCode, _row.BaseForex, _row.PPNForex, _row.DiscForex, _row.OtherFee, _row.StampFee, _row.TotalForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Billing_SupplierInvoiceHd GetSingleSupplierInvoiceHd(Guid _prmCode)
        {
            Billing_SupplierInvoiceHd _result = null;

            try
            {
                _result = this.db.Billing_SupplierInvoiceHds.Single(_temp => _temp.SupplierInvoiceHdCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddSupplierInvoiceHd(Billing_SupplierInvoiceHd _prmSupplierInvoiceHd)
        {
            bool _result = false;


            try
            {
                if (this.IsExistsSupplierInvoiceHd(_prmSupplierInvoiceHd.SupplierInvoiceHdCode, _prmSupplierInvoiceHd.TransNmbr) == false)
                {
                    this.db.Billing_SupplierInvoiceHds.InsertOnSubmit(_prmSupplierInvoiceHd);
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

        public bool EditSupplierInvoiceHd(Billing_SupplierInvoiceHd _prmSupplierInvoiceHd)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsSupplierInvoiceHd(_prmSupplierInvoiceHd.SupplierInvoiceHdCode, _prmSupplierInvoiceHd.TransNmbr) == false)
                {
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

        public bool DeleteMultiSupplierInvoiceHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Billing_SupplierInvoiceHd _prmSupplierInvoiceHd = this.GetSingleSupplierInvoiceHd(new Guid(_prmCode[i]));

                    int _query2 = (
                                    from _finPayTradeDb in this.db.FINPayTradeDbs
                                    where _finPayTradeDb.InvoiceNo == _prmSupplierInvoiceHd.TransNmbr
                                    select _finPayTradeDb
                                 ).Count();

                    if (_prmSupplierInvoiceHd != null)
                    {
                        if (_query2 == 0)
                        {
                            if (_prmSupplierInvoiceHd.Status != SupplierInvoiceDataMapper.GetStatus(TransStatus.Posted))
                            {
                                var _query = (from _detail in this.db.Billing_SupplierInvoiceDts
                                              where _detail.SupplierInvoiceHdCode == new Guid(_prmCode[i])
                                              select _detail);

                                this.db.Billing_SupplierInvoiceDts.DeleteAllOnSubmit(_query);

                                this.db.Billing_SupplierInvoiceHds.DeleteOnSubmit(_prmSupplierInvoiceHd);

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

        public string GetTransactionNoFromSupplierInvoiceHd(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _supplierInvoiceHd in this.db.Billing_SupplierInvoiceHds
                                where _supplierInvoiceHd.SupplierInvoiceHdCode == _prmCode
                                select new
                                {
                                    TransNmbr = _supplierInvoiceHd.TransNmbr
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


        public string GetCurrCodeFromSupplierInvoiceHd(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _supplierInvoiceHd in this.db.Billing_SupplierInvoiceHds
                                where _supplierInvoiceHd.SupplierInvoiceHdCode == _prmCode
                                select new
                                {
                                    CurrCode = _supplierInvoiceHd.CurrCode
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

        public char GetStatusSupplierInvoiceHd(Guid _prmCode)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _supplierInvoiceHd in this.db.Billing_SupplierInvoiceHds
                                where _supplierInvoiceHd.SupplierInvoiceHdCode == _prmCode
                                select new
                                {
                                    Status = _supplierInvoiceHd.Status
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

        private bool IsExistsSupplierInvoiceHd(Guid _prmCode, string _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                var _query = from _supplierInvoiceHd in this.db.Billing_SupplierInvoiceHds
                             where (_supplierInvoiceHd.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()) && (_supplierInvoiceHd.SupplierInvoiceHdCode != _prmCode)
                             select new
                             {
                                 _supplierInvoiceHd.SupplierInvoiceHdCode
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

        public string GetApproval(Guid _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_BillingSupplierInvoiceGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

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
                this.db.S_BillingSupplierInvoiceApprove(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Approve Success";
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

                Billing_SupplierInvoiceHd _suppBillInvoiceHd = this.db.Billing_SupplierInvoiceHds.Single(_temp => _temp.SupplierInvoiceHdCode == _prmCode);
                String _locked = _transCloseBL.IsExistAndLocked(_suppBillInvoiceHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_BillingSupplierInvoicePost(_prmCode, 0, 0, _prmuser, ref _result);

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

                Billing_SupplierInvoiceHd _suppBillInvoiceHd = this.db.Billing_SupplierInvoiceHds.Single(_temp => _temp.SupplierInvoiceHdCode == _prmCode);
                String _locked = _transCloseBL.IsExistAndLocked(_suppBillInvoiceHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_BillingSupplierInvoiceUnPost(_prmCode, 0, 0, _prmuser, ref _result);

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

        #endregion

        #region Billing.SupplierInvoiceDt

        public int RowsCountSupplierInvoiceDt(Guid _prmCode)
        {
            int _result = 0;

            _result = this.db.Billing_SupplierInvoiceDts.Where(_row => _row.SupplierInvoiceHdCode == _prmCode).Count();

            return _result;
        }

        public List<Billing_SupplierInvoiceDt> GetListSupplierInvoiceDt(int _prmReqPage, int _prmPageSize, Guid _prmCode)
        {
            List<Billing_SupplierInvoiceDt> _result = new List<Billing_SupplierInvoiceDt>();

            try
            {
                var _query = (
                                from _supplierInvoiceDt in this.db.Billing_SupplierInvoiceDts
                                where _supplierInvoiceDt.SupplierInvoiceHdCode == _prmCode
                                select new
                                {
                                    SupplierInvoiceDtCode = _supplierInvoiceDt.SupplierInvoiceDtCode,
                                    SupplierInvoiceHdCode = _supplierInvoiceDt.SupplierInvoiceHdCode,
                                    ItemDescription = _supplierInvoiceDt.ItemDescription,
                                    AmountForex = _supplierInvoiceDt.AmountForex,
                                    Remark = _supplierInvoiceDt.Remark,
                                    Account = _supplierInvoiceDt.Account
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Billing_SupplierInvoiceDt(_row.SupplierInvoiceDtCode, _row.SupplierInvoiceHdCode, _row.ItemDescription, _row.AmountForex, _row.Remark,_row.Account));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Billing_SupplierInvoiceDt GetSingleSupplierInvoiceDt(Guid _prmDtCode)
        {
            Billing_SupplierInvoiceDt _result = null;

            try
            {
                _result = this.db.Billing_SupplierInvoiceDts.Single(_temp => _temp.SupplierInvoiceDtCode == _prmDtCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSupplierInvoiceDt(string[] _prmDtCode, Guid _prmInvoiceHd)
        {
            bool _result = false;

            decimal _tempBaseForex = 0;
            decimal _tempDiscForex = 0;
            decimal _tempPPNForex = 0;
            decimal _tempOtherFee = 0;
            decimal _tempStampFee = 0;
            decimal _tempTotalForex = 0;

            try
            {
                for (int i = 0; i < _prmDtCode.Length; i++)
                {
                    Billing_SupplierInvoiceDt _supplierInvoiceDt = this.db.Billing_SupplierInvoiceDts.Single(_temp => _temp.SupplierInvoiceDtCode == new Guid(_prmDtCode[i]));

                    Billing_SupplierInvoiceHd _supplierInvoiceHd = this.GetSingleSupplierInvoiceHd(_prmInvoiceHd);

                    decimal _amount = _supplierInvoiceDt.AmountForex;
                    int _decimalPlace = Convert.ToInt32(new CurrencyBL().GetDecimalPlace(_supplierInvoiceHd.CurrCode));

                    _tempBaseForex = Math.Round((Convert.ToDecimal(_supplierInvoiceHd.BaseForex) - _amount), _decimalPlace);
                    if (_supplierInvoiceHd.DiscPercent != 0)
                    {
                        _tempDiscForex = Math.Round(Convert.ToDecimal(_tempBaseForex * _supplierInvoiceHd.DiscPercent / 100), _decimalPlace);
                    }
                    else
                    {
                        _tempDiscForex = Math.Round(Convert.ToDecimal(_supplierInvoiceHd.DiscForex), _decimalPlace);
                    }
                    _tempPPNForex = Math.Round((_tempBaseForex - _tempDiscForex) * (Convert.ToDecimal(_supplierInvoiceHd.PPN) / 100), _decimalPlace);
                    _tempOtherFee = Convert.ToDecimal(_supplierInvoiceHd.OtherFee);
                    _tempStampFee = Convert.ToDecimal(_supplierInvoiceHd.StampFee);
                    _tempTotalForex = Math.Round((_tempBaseForex - _tempDiscForex + _tempPPNForex + _tempOtherFee + _tempStampFee), _decimalPlace);

                    _supplierInvoiceHd.BaseForex = _tempBaseForex;

                    if (_tempBaseForex > 0)
                    {
                        _supplierInvoiceHd.DiscForex = _tempDiscForex;
                        _supplierInvoiceHd.PPNForex = _tempPPNForex;
                        _supplierInvoiceHd.StampFee = _tempStampFee;
                        _supplierInvoiceHd.OtherFee = _tempOtherFee;
                        _supplierInvoiceHd.TotalForex = _tempTotalForex;
                    }
                    else
                    {
                        _supplierInvoiceHd.DiscForex = 0;
                        _supplierInvoiceHd.PPNForex = 0;
                        _supplierInvoiceHd.StampFee = 0;
                        _supplierInvoiceHd.OtherFee = 0;
                        _supplierInvoiceHd.TotalForex = 0;
                    }
                    this.db.Billing_SupplierInvoiceDts.DeleteOnSubmit(_supplierInvoiceDt);
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

        public bool AddSupplierInvoiceDt(Billing_SupplierInvoiceDt _prmSupplierInvoiceDt)
        {
            bool _result = false;

            decimal _total = 0;

            try
            {
                var _query = (
                           from _supplierInvoiceDt in this.db.Billing_SupplierInvoiceDts
                           where !(
                                       from _supplierInvoiceDt2 in this.db.Billing_SupplierInvoiceDts
                                       where _supplierInvoiceDt2.SupplierInvoiceDtCode == _prmSupplierInvoiceDt.SupplierInvoiceDtCode
                                       select _supplierInvoiceDt2.SupplierInvoiceDtCode
                                   ).Contains(_supplierInvoiceDt.SupplierInvoiceDtCode)
                                   && _supplierInvoiceDt.SupplierInvoiceHdCode == _prmSupplierInvoiceDt.SupplierInvoiceHdCode
                           group _supplierInvoiceDt by new { _supplierInvoiceDt.SupplierInvoiceHdCode } into _grp
                           select new
                           {
                               AmountForex = _grp.Sum(a => a.AmountForex)
                           }
                         );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                Billing_SupplierInvoiceHd _supplierInvoiceHd = this.GetSingleSupplierInvoiceHd(_prmSupplierInvoiceDt.SupplierInvoiceHdCode);
                int _decimalPlace = Convert.ToInt32(new CurrencyBL().GetDecimalPlace(_supplierInvoiceHd.CurrCode));
                _supplierInvoiceHd.BaseForex = Math.Round(Convert.ToDecimal(_total + _prmSupplierInvoiceDt.AmountForex), _decimalPlace);
                if (_supplierInvoiceHd.DiscPercent != 0)
                {
                    _supplierInvoiceHd.DiscForex = Math.Round(Convert.ToDecimal(_supplierInvoiceHd.BaseForex * _supplierInvoiceHd.DiscPercent / 100), _decimalPlace);
                }
                _supplierInvoiceHd.PPNForex = Math.Round(Convert.ToDecimal((_supplierInvoiceHd.BaseForex - _supplierInvoiceHd.DiscForex) * _supplierInvoiceHd.PPN / 100), _decimalPlace);
                _supplierInvoiceHd.TotalForex = Math.Round(Convert.ToDecimal(_supplierInvoiceHd.BaseForex - _supplierInvoiceHd.DiscForex + _supplierInvoiceHd.PPNForex + _supplierInvoiceHd.OtherFee + _supplierInvoiceHd.StampFee), _decimalPlace);

                this.db.Billing_SupplierInvoiceDts.InsertOnSubmit(_prmSupplierInvoiceDt);

                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSupplierInvoiceDt(Billing_SupplierInvoiceDt _prmSupplierInvoiceDt)
        {
            bool _result = false;

            decimal _total = 0;

            try
            {
                var _query = (
                           from _supplierInvoiceDt in this.db.Billing_SupplierInvoiceDts
                           where !(
                                       from _supplierInvoiceDt2 in this.db.Billing_SupplierInvoiceDts
                                       where _supplierInvoiceDt2.SupplierInvoiceDtCode == _prmSupplierInvoiceDt.SupplierInvoiceDtCode
                                       select _supplierInvoiceDt2.SupplierInvoiceDtCode
                                   ).Contains(_supplierInvoiceDt.SupplierInvoiceDtCode)
                                   && _supplierInvoiceDt.SupplierInvoiceHdCode == _prmSupplierInvoiceDt.SupplierInvoiceHdCode
                           group _supplierInvoiceDt by new { _supplierInvoiceDt.SupplierInvoiceHdCode } into _grp
                           select new
                           {
                               AmountForex = _grp.Sum(a => a.AmountForex)
                           }
                         );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                Billing_SupplierInvoiceHd _supplierInvoiceHd = this.GetSingleSupplierInvoiceHd(_prmSupplierInvoiceDt.SupplierInvoiceHdCode);
                int _decimalPlace = Convert.ToInt32(new CurrencyBL().GetDecimalPlace(_supplierInvoiceHd.CurrCode));
                _supplierInvoiceHd.BaseForex = Math.Round(Convert.ToDecimal(_total + _prmSupplierInvoiceDt.AmountForex), _decimalPlace);
                if (_supplierInvoiceHd.DiscPercent != 0)
                {
                    _supplierInvoiceHd.DiscForex = Math.Round(Convert.ToDecimal(_supplierInvoiceHd.BaseForex * _supplierInvoiceHd.DiscPercent / 100), _decimalPlace);
                }
                _supplierInvoiceHd.PPNForex = Math.Round(Convert.ToDecimal((_supplierInvoiceHd.BaseForex - _supplierInvoiceHd.DiscForex) * _supplierInvoiceHd.PPN / 100), _decimalPlace);
                _supplierInvoiceHd.TotalForex = Math.Round(Convert.ToDecimal(_supplierInvoiceHd.BaseForex - _supplierInvoiceHd.DiscForex + _supplierInvoiceHd.PPNForex + _supplierInvoiceHd.OtherFee + _supplierInvoiceHd.StampFee), _decimalPlace);

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

        ~SupplierInvoiceBL()
        {

        }
    }
}
