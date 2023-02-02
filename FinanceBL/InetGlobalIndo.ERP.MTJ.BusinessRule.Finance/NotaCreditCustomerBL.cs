using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class NotaCreditCustomerBL : Base
    {
        public NotaCreditCustomerBL()
        {

        }

        #region FINCNCustHd
        public double RowsCountFINCNCustHd(string _prmCategory, string _prmKeyword)
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

            var _query =
                        (
                           from _finCNCustHd in this.db.FINCNCustHds
                           join _msCust in this.db.MsCustomers
                               on _finCNCustHd.CustCode equals _msCust.CustCode
                           where (SqlMethods.Like(_finCNCustHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                              && (SqlMethods.Like((_finCNCustHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                              && _finCNCustHd.Status != NotaCreditCustDataMapper.GetStatus(TransStatus.Deleted)
                           select _finCNCustHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINCNCustHd> GetListFINCNCustHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINCNCustHd> _result = new List<FINCNCustHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

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

            try
            {
                var _query = (
                                from _finCNCustHd in this.db.FINCNCustHds
                                join _msCust in this.db.MsCustomers
                                    on _finCNCustHd.CustCode equals _msCust.CustCode
                                where (SqlMethods.Like(_finCNCustHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finCNCustHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finCNCustHd.Status != NotaCreditCustDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finCNCustHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finCNCustHd.TransNmbr,
                                    FileNmbr = _finCNCustHd.FileNmbr,
                                    TransDate = _finCNCustHd.TransDate,
                                    CurrCode = _finCNCustHd.CurrCode,
                                    Status = _finCNCustHd.Status,
                                    CustCode = _finCNCustHd.CustCode,
                                    CustName = _msCust.CustName,
                                    InvoiceNo = _finCNCustHd.InvoiceNo,
                                    Term = _finCNCustHd.Term,
                                    TermName = (
                                                    from _msTerm in this.db.MsTerms
                                                    where _finCNCustHd.Term == _msTerm.TermCode
                                                    select _msTerm.TermName
                                                ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINCNCustHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.CustCode, _row.CustName, _row.InvoiceNo, _row.Term, _row.TermName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCNCustHd GetSingleFINCNCustHd(string _prmCode)
        {
            FINCNCustHd _result = null;

            try
            {
                _result = this.db.FINCNCustHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCNCustHd GetSingleFINCNCustHdView(string _prmCode)
        {
            FINCNCustHd _result = new FINCNCustHd();

            try
            {
                var _query = (
                               from _finCNCustHd in this.db.FINCNCustHds
                               join _msCust in this.db.MsCustomers
                                    on _finCNCustHd.CustCode equals _msCust.CustCode
                               join _msTerm in this.db.MsTerms
                                    on _finCNCustHd.Term equals _msTerm.TermCode
                               orderby _finCNCustHd.DatePrep descending
                               where _finCNCustHd.TransNmbr == _prmCode
                               select new
                               {
                                   TransNmbr = _finCNCustHd.TransNmbr,
                                   FileNmbr = _finCNCustHd.FileNmbr,
                                   TransDate = _finCNCustHd.TransDate,
                                   Status = _finCNCustHd.Status,
                                   CustCode = _finCNCustHd.CustCode,
                                   CustName = _msCust.CustName,
                                   CurrCode = _finCNCustHd.CurrCode,
                                   ForexRate = _finCNCustHd.ForexRate,
                                   TotalForex = _finCNCustHd.TotalForex,
                                   Term = _finCNCustHd.Term,
                                   TermName = _msTerm.TermName,
                                   InvoiceNo = _finCNCustHd.InvoiceNo,
                                   DiscForex = _finCNCustHd.DiscForex,
                                   BaseForex = _finCNCustHd.BaseForex,
                                   Attn = _finCNCustHd.Attn,
                                   Remark = _finCNCustHd.Remark,
                                   CNCustNo = _finCNCustHd.CNCustNo
                               }
                           ).Single();

                _result.TransNmbr = _query.TransNmbr;
                _result.FileNmbr = _query.FileNmbr;
                _result.TransDate = _query.TransDate;
                _result.Status = _query.Status;
                _result.CustCode = _query.CustCode;
                _result.CustName = _query.CustName;
                _result.CurrCode = _query.CurrCode;
                _result.ForexRate = _query.ForexRate;
                _result.TotalForex = _query.TotalForex;
                _result.Term = _query.Term;
                _result.TermName = _query.TermName;
                _result.InvoiceNo = _query.InvoiceNo;
                _result.DiscForex = _query.DiscForex;
                _result.BaseForex = _query.BaseForex;
                _result.Attn = _query.Attn;
                _result.Remark = _query.Remark;
                _result.CNCustNo = _query.CNCustNo;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINCNCustHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCNCustHd _finCNCustHd = this.db.FINCNCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finCNCustHd != null)
                    {
                        if ((_finCNCustHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINCNCustDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINCNCustDts.DeleteAllOnSubmit(_query);

                            this.db.FINCNCustHds.DeleteOnSubmit(_finCNCustHd);

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

        public string AddFINCNCustHd(FINCNCustHd _prmFINCNCustHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINCNCustHd.TransDate.Year, _prmFINCNCustHd.TransDate.Month, AppModule.GetValue(TransactionType.NotaCreditCustomer), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINCNCustHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINCNCustHds.InsertOnSubmit(_prmFINCNCustHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINCNCustHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINCNCustHd(FINCNCustHd _prmFINCNCustHd)
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

        public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                int _success = this.db.S_FNCICNGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.NotaCreditCustomer);
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
                    int _success = this.db.S_FNCICNApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINCNCustHd _finCNCustHd = this.GetSingleFINCNCustHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finCNCustHd.TransDate.Year, _finCNCustHd.TransDate.Month, AppModule.GetValue(TransactionType.NotaCreditCustomer), this._companyTag, ""))
                        {
                            _finCNCustHd.FileNmbr = item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.NotaCreditCustomer);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleFINCNCustHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
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

        public string Posting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINCNCustHd _finCNCustHd = this.db.FINCNCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finCNCustHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNCICNPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.DeliveryOrder);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleFINCNCustHd(_prmTransNmbr).FileNmbr;
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
                FINCNCustHd _finCNCustHd = this.db.FINCNCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finCNCustHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNCICNUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

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

        public bool GetSingleFINCNCustHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCNCustHd _finCNCustHd = this.db.FINCNCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finCNCustHd != null)
                    {
                        if (_finCNCustHd.Status != NotaCreditCustDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINCNCustHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCNCustHd _finCNCustHd = this.db.FINCNCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finCNCustHd.Status == NotaCreditCustDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finCNCustHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finCNCustHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finCNCustHd != null)
                    {
                        if ((_finCNCustHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINCNCustDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINCNCustDts.DeleteAllOnSubmit(_query);

                            this.db.FINCNCustHds.DeleteOnSubmit(_finCNCustHd);

                            _result = true;
                        }
                        else if (_finCNCustHd.FileNmbr != "" && _finCNCustHd.Status == NotaCreditCustDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finCNCustHd.Status = NotaCreditCustDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINCNCustDt
        public int RowsCountFINCNCustDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINCNCustDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINCNCustDt> GetListFINCNCustDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<FINCNCustDt> _result = new List<FINCNCustDt>();

            try
            {
                var _query = (
                                from _finCNCustDt in this.db.FINCNCustDts
                                join _msAccount in this.db.MsAccounts
                                    on _finCNCustDt.Account equals _msAccount.Account
                                join _msUnit in this.db.MsUnits
                                    on _finCNCustDt.Unit equals _msUnit.UnitCode
                                where _finCNCustDt.TransNmbr == _prmCode
                                orderby _finCNCustDt.ItemNo ascending
                                select new
                                {
                                    ItemNo = _finCNCustDt.ItemNo,
                                    Account = _finCNCustDt.Account,
                                    AccountName = _msAccount.AccountName,
                                    AmountForex = _finCNCustDt.AmountForex,
                                    Subled = _finCNCustDt.SubLed,
                                    SubledName = (
                                                    from _viewMsSubled in this.db.V_MsSubleds
                                                    where _viewMsSubled.SubLed_No == _finCNCustDt.SubLed
                                                    select _viewMsSubled.SubLed_Name
                                                 ).FirstOrDefault(),
                                    PriceForex = _finCNCustDt.PriceForex,
                                    Qty = _finCNCustDt.Qty,
                                    Unit = _finCNCustDt.Unit,
                                    UnitName = _msUnit.UnitName,
                                    Remark = _finCNCustDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINCNCustDt(_row.ItemNo, _row.Account, _row.AccountName, _row.AmountForex, _row.Subled, _row.SubledName, _row.PriceForex, _row.Qty, _row.Unit, _row.UnitName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCNCustDt GetSingleFINCNCustDt(string _prmCode, int _prmItemNo)
        {
            FINCNCustDt _result = null;

            try
            {
                _result = this.db.FINCNCustDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCNCustDt GetSingleFINCNCustDtView(string _prmCode, int _prmItemNo)
        {
            FINCNCustDt _result = new FINCNCustDt();

            try
            {
                var _query = (
                               from _finCNCustDt in this.db.FINCNCustDts
                               join _msAccount in this.db.MsAccounts
                                    on _finCNCustDt.Account equals _msAccount.Account
                               join _msUnit in this.db.MsUnits
                                    on _finCNCustDt.Unit equals _msUnit.UnitCode
                               orderby _finCNCustDt.ItemNo ascending
                               where _finCNCustDt.TransNmbr == _prmCode && _finCNCustDt.ItemNo == _prmItemNo
                               select new
                               {
                                   ItemNo = _finCNCustDt.ItemNo,
                                   Account = _finCNCustDt.Account,
                                   AccountName = _msAccount.AccountName,
                                   AmountForex = _finCNCustDt.AmountForex,
                                   Subled = _finCNCustDt.SubLed,
                                   SubledName = (
                                                    from _viewMsSubled in this.db.V_MsSubleds
                                                    where _viewMsSubled.SubLed_No == _finCNCustDt.SubLed
                                                    select _viewMsSubled.SubLed_Name
                                                 ).FirstOrDefault(),
                                   PriceForex = _finCNCustDt.PriceForex,
                                   Qty = _finCNCustDt.Qty,
                                   Unit = _finCNCustDt.Unit,
                                   UnitName = _msUnit.UnitName,
                                   Remark = _finCNCustDt.Remark
                               }
                           ).Single();

                _result.ItemNo = _query.ItemNo;
                _result.Account = _query.Account;
                _result.AccountName = _query.AccountName;
                _result.AmountForex = _query.AmountForex;
                _result.SubLed = _query.Subled;
                _result.SubledName = _query.SubledName;
                _result.PriceForex = _query.PriceForex;
                _result.Qty = _query.Qty;
                _result.Unit = _query.Unit;
                _result.UnitName = _query.UnitName;
                _result.Remark = _query.Remark;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINCNCustDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            FINCNCustHd _finCNCustHd = new FINCNCustHd();

            decimal _total = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCNCustDt _finCNCustDt = this.db.FINCNCustDts.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmCode[i]) && _temp.TransNmbr == _prmTransNo);

                    this.db.FINCNCustDts.DeleteOnSubmit(_finCNCustDt);
                }

                var _query = (
                                from _finCNCustDt2 in this.db.FINCNCustDts
                                where !(
                                            from _code in _prmCode
                                            select _code
                                        ).Contains(_finCNCustDt2.ItemNo.ToString())
                                        && _finCNCustDt2.TransNmbr == _prmTransNo
                                group _finCNCustDt2 by _finCNCustDt2.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finCNCustHd = this.db.FINCNCustHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _finCNCustHd.BaseForex = _total;
                _finCNCustHd.TotalForex = _finCNCustHd.BaseForex - _finCNCustHd.DiscForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINCNCustDt(FINCNCustDt _prmFINCNCustDt)
        {
            bool _result = false;

            FINCNCustHd _finCNCustHd = new FINCNCustHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finCNCustDt in this.db.FINCNCustDts
                               where !(
                                           from _finCNCustDt2 in this.db.FINCNCustDts
                                           where _finCNCustDt2.ItemNo == _prmFINCNCustDt.ItemNo && _finCNCustDt2.TransNmbr == _prmFINCNCustDt.TransNmbr
                                           select _finCNCustDt2.ItemNo
                                       ).Contains(_finCNCustDt.ItemNo)
                                       && _finCNCustDt.TransNmbr == _prmFINCNCustDt.TransNmbr
                               group _finCNCustDt by _finCNCustDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }


                _finCNCustHd = this.db.FINCNCustHds.Single(_fa => _fa.TransNmbr == _prmFINCNCustDt.TransNmbr);

                _finCNCustHd.BaseForex = _total + _prmFINCNCustDt.AmountForex;
                _finCNCustHd.TotalForex = _finCNCustHd.BaseForex - _finCNCustHd.DiscForex;

                this.db.FINCNCustDts.InsertOnSubmit(_prmFINCNCustDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINCNCustDt(FINCNCustDt _prmFINCNCustDt)
        {
            bool _result = false;

            FINCNCustHd _finCNCustHd = new FINCNCustHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finCNCustDt in this.db.FINCNCustDts
                               where !(
                                           from _finCNCustDt2 in this.db.FINCNCustDts
                                           where _finCNCustDt2.ItemNo == _prmFINCNCustDt.ItemNo && _finCNCustDt2.TransNmbr == _prmFINCNCustDt.TransNmbr
                                           select _finCNCustDt2.ItemNo
                                       ).Contains(_finCNCustDt.ItemNo)
                                       && _finCNCustDt.TransNmbr == _prmFINCNCustDt.TransNmbr
                               group _finCNCustDt by _finCNCustDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finCNCustHd = this.db.FINCNCustHds.Single(_fa => _fa.TransNmbr == _prmFINCNCustDt.TransNmbr);

                _finCNCustHd.BaseForex = _total + _prmFINCNCustDt.AmountForex;
                _finCNCustHd.TotalForex = _finCNCustHd.BaseForex - _finCNCustHd.DiscForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxNoItemFINCNCustDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINCNCustDts.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region FINARPosting
        public List<FINARPosting> GetListInvoiceNoFINARPostingForDDL(string _prmCustCode)
        {
            List<FINARPosting> _result = new List<FINARPosting>();

            try
            {
                var _query = (
                                from _v_FNARPosting in this.db.V_FNARPostings
                                where _v_FNARPosting.Cust_Code == _prmCustCode
                                    && _v_FNARPosting.Amount_Saldo > 0
                                    && (_v_FNARPosting.FileNmbr ?? "").Trim() == _v_FNARPosting.FileNmbr.Trim()
                                orderby _v_FNARPosting.Invoice_No descending
                                select new
                                {
                                    InvoiceNo = _v_FNARPosting.Invoice_No,
                                    FileNmbr = _v_FNARPosting.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINARPosting(_row.InvoiceNo, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~NotaCreditCustomerBL()
        {
        }
    }
}
