using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class NotaDebitSupplierBL : Base
    {
        public NotaDebitSupplierBL()
        {

        }

        #region FINDNSuppHd
        public double RowsCountFINDNSuppHd(string _prmCategory, string _prmKeyword)
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
            else if (_prmCategory == "SuppName")
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
                            from _finDNSuppHd in this.db.FINDNSuppHds
                            join _msSupp in this.db.MsSuppliers
                                on _finDNSuppHd.SuppCode equals _msSupp.SuppCode
                            where (SqlMethods.Like(_finDNSuppHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_finDNSuppHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _finDNSuppHd.Status != NotaDebitSuppDataMapper.GetStatus(TransStatus.Deleted)
                            select _finDNSuppHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINDNSuppHd> GetListFINDNSuppHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINDNSuppHd> _result = new List<FINDNSuppHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

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
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _finDNSuppHd in this.db.FINDNSuppHds
                                join _msSupp in this.db.MsSuppliers
                                    on _finDNSuppHd.SuppCode equals _msSupp.SuppCode
                                where (SqlMethods.Like(_finDNSuppHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finDNSuppHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finDNSuppHd.Status != NotaDebitSuppDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finDNSuppHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finDNSuppHd.TransNmbr,
                                    FileNmbr = _finDNSuppHd.FileNmbr,
                                    TransDate = _finDNSuppHd.TransDate,
                                    CurrCode = _finDNSuppHd.CurrCode,
                                    Status = _finDNSuppHd.Status,
                                    SuppCode = _finDNSuppHd.SuppCode,
                                    SuppName = _msSupp.SuppName,
                                    Term = _finDNSuppHd.Term,
                                    TermName = (
                                                   from _msTerm in this.db.MsTerms
                                                   where _finDNSuppHd.Term == _msTerm.TermCode
                                                   select _msTerm.TermName
                                               ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINDNSuppHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.SuppCode, _row.SuppName, _row.Term, _row.TermName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDNSuppHd GetSingleFINDNSuppHd(string _prmCode)
        {
            FINDNSuppHd _result = null;

            try
            {
                _result = this.db.FINDNSuppHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDNSuppHd GetSingleFINDNSuppHdView(string _prmCode)
        {
            FINDNSuppHd _result = new FINDNSuppHd();

            try
            {
                var _query = (
                               from _finDNSuppHd in this.db.FINDNSuppHds
                               join _msSupp in this.db.MsSuppliers
                                    on _finDNSuppHd.SuppCode equals _msSupp.SuppCode
                               join _msTerm in this.db.MsTerms
                               on _finDNSuppHd.Term equals _msTerm.TermCode
                               orderby _finDNSuppHd.DatePrep descending
                               where _finDNSuppHd.TransNmbr == _prmCode
                               select new
                               {
                                   TransNmbr = _finDNSuppHd.TransNmbr,
                                   TransDate = _finDNSuppHd.TransDate,
                                   Status = _finDNSuppHd.Status,
                                   SuppCode = _finDNSuppHd.SuppCode,
                                   SuppName = _msSupp.SuppName,
                                   CurrCode = _finDNSuppHd.CurrCode,
                                   ForexRate = _finDNSuppHd.ForexRate,
                                   TotalForex = _finDNSuppHd.TotalForex,
                                   Term = _finDNSuppHd.Term,
                                   TermName = _msTerm.TermName,
                                   DiscForex = _finDNSuppHd.DiscForex,
                                   BaseForex = _finDNSuppHd.BaseForex,
                                   Attn = _finDNSuppHd.Attn,
                                   Remark = _finDNSuppHd.Remark
                               }
                           ).Single();

                _result.TransNmbr = _query.TransNmbr;
                _result.TransDate = _query.TransDate;
                _result.Status = _query.Status;
                _result.SuppCode = _query.SuppCode;
                _result.SuppName = _query.SuppName;
                _result.CurrCode = _query.CurrCode;
                _result.ForexRate = _query.ForexRate;
                _result.TotalForex = _query.TotalForex;
                _result.Term = _query.Term;
                _result.TermName = _query.TermName;
                _result.DiscForex = _query.DiscForex;
                _result.BaseForex = _query.BaseForex;
                _result.Attn = _query.Attn;
                _result.Remark = _query.Remark;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINDNSuppHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDNSuppHd _finDNSuppHd = this.db.FINDNSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDNSuppHd != null)
                    {
                        if ((_finDNSuppHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINDNSuppDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINDNSuppDts.DeleteAllOnSubmit(_query);

                            this.db.FINDNSuppHds.DeleteOnSubmit(_finDNSuppHd);

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

        public string AddFINDNSuppHd(FINDNSuppHd _prmFINDNSuppHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINDNSuppHd.TransDate.Year, _prmFINDNSuppHd.TransDate.Month, AppModule.GetValue(TransactionType.NotaDebitSupplier), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINDNSuppHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINDNSuppHds.InsertOnSubmit(_prmFINDNSuppHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINDNSuppHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDNSuppHd(FINDNSuppHd _prmFINDNSuppHd)
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
                int _success = this.db.S_FNSIDNGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.NotaDebitSupplier);
                    _transActivity.TransNmbr = _prmTransNmbr;
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
                    int _success = this.db.S_FNSIDNApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINDNSuppHd _finDNSuppHd = this.GetSingleFINDNSuppHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finDNSuppHd.TransDate.Year, _finDNSuppHd.TransDate.Month, AppModule.GetValue(TransactionType.NotaDebitSupplier), this._companyTag, ""))
                        {
                            _finDNSuppHd.FileNmbr = item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.NotaDebitSupplier);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINDNSuppHd(_prmTransNmbr).FileNmbr;
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
                FINDNSuppHd _finDNSuppHd = this.db.FINDNSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDNSuppHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSIDNPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.NotaDebitSupplier);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINDNSuppHd(_prmTransNmbr).FileNmbr;
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
                FINDNSuppHd _finDNSuppHd = this.db.FINDNSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDNSuppHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSIDNUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.NotaDebitSupplier);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINDNSuppHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINDNSuppHd(_prmTransNmbr).TransDate;
                        //_transActivity.Reason = "";

                        //this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        //this.db.SubmitChanges();
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

        public bool GetSingleFINDNSuppHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDNSuppHd _finDNSuppHd = this.db.FINDNSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDNSuppHd != null)
                    {
                        if (_finDNSuppHd.Status != NotaDebitSuppDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINDNSuppHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDNSuppHd _finDNSuppHd = this.db.FINDNSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDNSuppHd.Status == NotaDebitSuppDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finDNSuppHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finDNSuppHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finDNSuppHd != null)
                    {
                        if ((_finDNSuppHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINDNSuppDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINDNSuppDts.DeleteAllOnSubmit(_query);

                            this.db.FINDNSuppHds.DeleteOnSubmit(_finDNSuppHd);

                            _result = true;
                        }
                        else if (_finDNSuppHd.FileNmbr != "" && _finDNSuppHd.Status == NotaDebitSuppDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finDNSuppHd.Status = NotaDebitSuppDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINDNSuppDt
        public int RowsCountFINDNSuppDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINDNSuppDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINDNSuppDt> GetListFINDNSuppDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<FINDNSuppDt> _result = new List<FINDNSuppDt>();

            try
            {
                var _query = (
                                from _finDNSuppDt in this.db.FINDNSuppDts
                                join _msAccount in this.db.MsAccounts
                                    on _finDNSuppDt.Account equals _msAccount.Account
                                join _msUnit in this.db.MsUnits
                                    on _finDNSuppDt.Unit equals _msUnit.UnitCode

                                where _finDNSuppDt.TransNmbr == _prmCode
                                orderby _finDNSuppDt.ItemNo ascending
                                select new
                                {
                                    ItemNo = _finDNSuppDt.ItemNo,
                                    Account = _finDNSuppDt.Account,
                                    AccountName = _msAccount.AccountName,
                                    AmountForex = _finDNSuppDt.AmountForex,
                                    Subled = _finDNSuppDt.Subled,
                                    SubledName = (
                                                    from _viewMsSubled in this.db.V_MsSubleds
                                                    where _viewMsSubled.SubLed_No == _finDNSuppDt.Subled
                                                    select _viewMsSubled.SubLed_Name
                                                 ).FirstOrDefault(),
                                    PriceForex = _finDNSuppDt.PriceForex,
                                    Qty = _finDNSuppDt.Qty,
                                    Unit = _finDNSuppDt.Unit,
                                    UnitName = _msUnit.UnitName,
                                    Remark = _finDNSuppDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINDNSuppDt(_row.ItemNo, _row.Account, _row.AccountName, _row.AmountForex, _row.Subled, _row.SubledName, _row.PriceForex, _row.Qty, _row.Unit, _row.UnitName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDNSuppDt GetSingleFINDNSuppDt(string _prmCode, int _prmItemNo)
        {
            FINDNSuppDt _result = null;

            try
            {
                _result = this.db.FINDNSuppDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDNSuppDt GetSingleFINDNSuppDtView(string _prmCode, int _prmItemNo)
        {
            FINDNSuppDt _result = new FINDNSuppDt();

            try
            {
                var _query = (
                               from _finDNSuppDt in this.db.FINDNSuppDts
                               join _msAccount in this.db.MsAccounts
                                    on _finDNSuppDt.Account equals _msAccount.Account
                               join _msUnit in this.db.MsUnits
                                    on _finDNSuppDt.Unit equals _msUnit.UnitCode
                               orderby _finDNSuppDt.ItemNo ascending
                               where _finDNSuppDt.TransNmbr == _prmCode && _finDNSuppDt.ItemNo == _prmItemNo
                               select new
                               {
                                   ItemNo = _finDNSuppDt.ItemNo,
                                   Account = _finDNSuppDt.Account,
                                   AccountName = _msAccount.AccountName,
                                   AmountForex = _finDNSuppDt.AmountForex,
                                   Subled = _finDNSuppDt.Subled,
                                   SubledName = (
                                                    from _viewMsSubled in this.db.V_MsSubleds
                                                    where _viewMsSubled.SubLed_No == _finDNSuppDt.Subled
                                                    select _viewMsSubled.SubLed_Name
                                                 ).FirstOrDefault(),
                                   PriceForex = _finDNSuppDt.PriceForex,
                                   Qty = _finDNSuppDt.Qty,
                                   Unit = _finDNSuppDt.Unit,
                                   UnitName = _msUnit.UnitName,
                                   Remark = _finDNSuppDt.Remark
                               }
                           ).Single();

                _result.ItemNo = _query.ItemNo;
                _result.Account = _query.Account;
                _result.AccountName = _query.AccountName;
                _result.AmountForex = _query.AmountForex;
                _result.Subled = _query.Subled;
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

        public bool DeleteMultiFINDNSuppDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            FINDNSuppHd _finDNSuppHd = new FINDNSuppHd();

            decimal _total = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDNSuppDt _finDNSuppDt = this.db.FINDNSuppDts.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmCode[i]) && _temp.TransNmbr == _prmTransNo);

                    this.db.FINDNSuppDts.DeleteOnSubmit(_finDNSuppDt);
                }

                var _query = (
                            from _finDNSuppDt2 in this.db.FINDNSuppDts
                            where !(
                                         from _code in _prmCode
                                         select _code
                                    ).Contains(_finDNSuppDt2.ItemNo.ToString())
                                    && _finDNSuppDt2.TransNmbr == _prmTransNo
                            group _finDNSuppDt2 by _finDNSuppDt2.TransNmbr into _grp
                            select new
                            {
                                AmountForex = _grp.Sum(a => a.AmountForex)
                            }
                          );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finDNSuppHd = this.db.FINDNSuppHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _finDNSuppHd.BaseForex = _total;
                _finDNSuppHd.TotalForex = _finDNSuppHd.BaseForex - _finDNSuppHd.DiscForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINDNSuppDt(FINDNSuppDt _prmFINDNSuppDt)
        {
            bool _result = false;

            FINDNSuppHd _finDNSuppHd = new FINDNSuppHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finDNSuppDt in this.db.FINDNSuppDts
                               where !(
                                           from _finDNSuppDt2 in this.db.FINDNSuppDts
                                           where _finDNSuppDt2.ItemNo == _prmFINDNSuppDt.ItemNo && _finDNSuppDt2.TransNmbr == _prmFINDNSuppDt.TransNmbr
                                           select _finDNSuppDt2.ItemNo
                                       ).Contains(_finDNSuppDt.ItemNo)
                                       && _finDNSuppDt.TransNmbr == _prmFINDNSuppDt.TransNmbr
                               group _finDNSuppDt by _finDNSuppDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }


                _finDNSuppHd = this.db.FINDNSuppHds.Single(_fa => _fa.TransNmbr == _prmFINDNSuppDt.TransNmbr);

                _finDNSuppHd.BaseForex = _total + _prmFINDNSuppDt.AmountForex;
                _finDNSuppHd.TotalForex = _finDNSuppHd.BaseForex - _finDNSuppHd.DiscForex;

                this.db.FINDNSuppDts.InsertOnSubmit(_prmFINDNSuppDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDNSuppDt(FINDNSuppDt _prmFINDNSuppDt)
        {
            bool _result = false;

            FINDNSuppHd _finDNSuppHd = new FINDNSuppHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finDNSuppDt in this.db.FINDNSuppDts
                               where !(
                                           from _finDNSuppDt2 in this.db.FINDNSuppDts
                                           where _finDNSuppDt2.ItemNo == _prmFINDNSuppDt.ItemNo && _finDNSuppDt2.TransNmbr == _prmFINDNSuppDt.TransNmbr
                                           select _finDNSuppDt2.ItemNo
                                       ).Contains(_finDNSuppDt.ItemNo)
                                       && _finDNSuppDt.TransNmbr == _prmFINDNSuppDt.TransNmbr
                               group _finDNSuppDt by _finDNSuppDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finDNSuppHd = this.db.FINDNSuppHds.Single(_fa => _fa.TransNmbr == _prmFINDNSuppDt.TransNmbr);

                _finDNSuppHd.BaseForex = _total + _prmFINDNSuppDt.AmountForex;
                _finDNSuppHd.TotalForex = _finDNSuppHd.BaseForex - _finDNSuppHd.DiscForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxNoItemFINDNSuppDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINDNSuppDts.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~NotaDebitSupplierBL()
        {
        }
    }
}
