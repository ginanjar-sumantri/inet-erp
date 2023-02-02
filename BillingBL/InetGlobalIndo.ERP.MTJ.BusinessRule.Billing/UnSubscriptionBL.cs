using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class UnSubscriptionBL : Base
    {
        public UnSubscriptionBL()
        {
        }

        #region UnSubscription

        public int RowsCountUnSub(string _prmCategory, string _prmKeyword)
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
            else if (_prmCategory == "TransDate")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "Status")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%" + _prmKeyword + "%";
            }


            _result = (
                           from _unSub in this.db.BILTrUnSubscriptionHds
                           where
                                (SqlMethods.Like((_unSub.TransNmbr ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_unSub.FileNmbr.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                && (SqlMethods.Like(_unSub.TransDate.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && (SqlMethods.Like(_unSub.Status.ToString().Trim().ToLower(), _pattern4.Trim().ToLower()))
                           select new
                           {
                               InvoiceHd = _unSub.TransNmbr
                           }
                       ).Count();

            return _result;
        }

        public List<BILTrUnSubscriptionHd> GetListUnSub(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<BILTrUnSubscriptionHd> _result = new List<BILTrUnSubscriptionHd>();

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
            else if (_prmCategory == "TransDate")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "Status")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _unSub in this.db.BILTrUnSubscriptionHds
                                join _msCustomer in this.db.MsCustomers
                                on _unSub.CustCode equals _msCustomer.CustCode
                                where
                                    (SqlMethods.Like((_unSub.TransNmbr ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_unSub.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_unSub.TransDate.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_unSub.Status.ToString().Trim().ToLower(), _pattern4.Trim().ToLower()))
                                orderby _unSub.TransDate descending
                                select new
                                {
                                    TransNmbr = _unSub.TransNmbr,
                                    FileNmbr = _unSub.FileNmbr,
                                    TransDate = _unSub.TransDate,
                                    CustCode = _unSub.CustCode,
                                    Status = _unSub.Status,
                                    CompanyName = _msCustomer.CustName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILTrUnSubscriptionHd(_row.TransNmbr, _row.FileNmbr, _row.CustCode, Convert.ToDateTime(_row.TransDate.ToString()), Convert.ToByte(_row.Status), _row.CompanyName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiUnSub(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    BILTrUnSubscriptionHd _bilTrUnSub = this.db.BILTrUnSubscriptionHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.BILTrUnSubscriptionHds.DeleteOnSubmit(_bilTrUnSub);
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

        public bool EditUnSub(BILTrUnSubscriptionHd _prmBILTrUnSub)
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

        public BILTrUnSubscriptionHd GetSingleUnSub(String _prmTransNmbr)
        {
            BILTrUnSubscriptionHd _result = null;

            try
            {
                _result = this.db.BILTrUnSubscriptionHds.Single(_temp => _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String AddtoBILTrUnSub(BILTrUnSubscriptionHd _prmBILTrUnSub)
        {
            String _result = "";
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmBILTrUnSub.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.BILTrUnSubscriptionHds.InsertOnSubmit(_prmBILTrUnSub);

                    var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                    this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                    this.db.SubmitChanges();


                    _scope.Complete();

                    _result = _prmBILTrUnSub.TransNmbr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                int _success = this.db.spBIL_UnSubGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

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

        public string Approve(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spBIL_UnSubApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        BILTrUnSubscriptionHd _bilTrContract = this.GetSingleUnSub(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmYear, _prmPeriod, AppModule.GetValue(TransactionType.Contract), this._companyTag, ""))
                        {
                            _bilTrContract.FileNmbr = item.Number;
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


        public int RowsCountUnSubscriptionDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _unSubscriptionDt in this.db.BILTrUnSubscriptionDts
                                 where _unSubscriptionDt.TransNmbr == _prmCode
                                 select _unSubscriptionDt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<BILTrUnSubscriptionDt> GetListUnSubscriptionDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<BILTrUnSubscriptionDt> _result = new List<BILTrUnSubscriptionDt>();

            try
            {
                var _query = (
                                from _unSubscriptionDt in this.db.BILTrUnSubscriptionDts
                                join _custBillAccount in this.db.Master_CustBillAccounts
                                    on _unSubscriptionDt.CustBillCode equals _custBillAccount.CustBillCode
                                join _msProduct in this.db.MsProducts
                                    on _custBillAccount.ProductCode equals _msProduct.ProductCode
                                join _msCurr in this.db.MsCurrencies
                                    on _custBillAccount.CurrCode equals _msCurr.CurrCode
                                where _unSubscriptionDt.TransNmbr == _prmCode
                                && _custBillAccount.fgActive == true
                                orderby _unSubscriptionDt.TransNmbr ascending
                                select new
                                {
                                    TransNmbr = _unSubscriptionDt.TransNmbr,
                                    CustBillAccount = _custBillAccount.CustBillAccount,
                                    ProductName = _msProduct.ProductName,
                                    Curr = _msCurr.CurrCode,
                                    AmountForex = Convert.ToDecimal(_custBillAccount.AmountForex),
                                    Typepayment = _custBillAccount.TypePayment,
                                    ActivateDate = Convert.ToDateTime(_custBillAccount.ActivateDate.ToString()),
                                    ExpiredDate = Convert.ToDateTime(_custBillAccount.ExpiredDate.ToString()),
                                    CustBillCode = _custBillAccount.CustBillCode,
                                    CustBillDescription = _custBillAccount.CustBillDescription
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILTrUnSubscriptionDt(_row.TransNmbr, _row.CustBillAccount, _row.ProductName, _row.Curr, _row.AmountForex, _row.Typepayment, _row.ActivateDate, _row.ExpiredDate, _row.CustBillCode, _row.CustBillDescription));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiUnSubscriptionDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    BILTrUnSubscriptionDt _UnSubscriptionDt = this.db.BILTrUnSubscriptionDts.Single(_temp => _temp.CustBillCode == new Guid(_prmCode[i]) && _temp.TransNmbr == _prmTransNo);

                    this.db.BILTrUnSubscriptionDts.DeleteOnSubmit(_UnSubscriptionDt);
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

        public BILTrUnSubscriptionDt GetSingleUnSubscriptionDt(string _prmTransNmbr, string _prmCode)
        {
            BILTrUnSubscriptionDt _result = null;

            try
            {
                _result = this.db.BILTrUnSubscriptionDts.Single(_temp => _temp.TransNmbr.ToLower() == _prmTransNmbr.ToLower() && _temp.CustBillCode.ToString().ToLower() == _prmCode.ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddUnSubscriptionDt(BILTrUnSubscriptionDt _prmUnSubscriptionDt)
        {
            bool _result = false;

            try
            {
                this.db.BILTrUnSubscriptionDts.InsertOnSubmit(_prmUnSubscriptionDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditUnSubscriptionDt(BILTrUnSubscriptionDt _prmBILTrUnSubscriptionDt)
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


        #endregion

        ~UnSubscriptionBL()
        {
        }
    }
}
