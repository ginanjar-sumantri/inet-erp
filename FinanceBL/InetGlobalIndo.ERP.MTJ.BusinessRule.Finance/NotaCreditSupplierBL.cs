using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class NotaCreditSupplierBL : Base
    {
        public NotaCreditSupplierBL()
        {

        }

        #region FINCNSuppHd
        public double RowsCountFINCNSuppHd(string _prmCategory, string _prmKeyword)
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
                            from _finCNSuppHd in this.db.FINCNSuppHds
                            join _msSupp in this.db.MsSuppliers
                                on _finCNSuppHd.SuppCode equals _msSupp.SuppCode
                            where (SqlMethods.Like(_finCNSuppHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_finCNSuppHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _finCNSuppHd.Status != NotaCreditSuppDataMapper.GetStatus(TransStatus.Deleted)    
                            select _finCNSuppHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINCNSuppHd> GetListFINCNSuppHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINCNSuppHd> _result = new List<FINCNSuppHd>();

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
                                from _finCNSuppHd in this.db.FINCNSuppHds
                                join _msSupp in this.db.MsSuppliers
                                    on _finCNSuppHd.SuppCode equals _msSupp.SuppCode
                                where (SqlMethods.Like(_finCNSuppHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finCNSuppHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finCNSuppHd.Status != NotaCreditSuppDataMapper.GetStatus(TransStatus.Deleted)    
                                orderby _finCNSuppHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finCNSuppHd.TransNmbr,
                                    FileNmbr = _finCNSuppHd.FileNmbr,
                                    TransDate = _finCNSuppHd.TransDate,
                                    CurrCode = _finCNSuppHd.CurrCode,
                                    Status = _finCNSuppHd.Status,
                                    SuppCode = _finCNSuppHd.SuppCode,
                                    SuppName = _msSupp.SuppName,
                                    Term = _finCNSuppHd.Term,
                                    TermName = (
                                                    from _msTerm in this.db.MsTerms
                                                    where _finCNSuppHd.Term == _msTerm.TermCode
                                                    select _msTerm.TermName
                                                ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINCNSuppHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.SuppCode, _row.SuppName, _row.Term, _row.TermName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCNSuppHd GetSingleFINCNSuppHd(string _prmCode)
        {
            FINCNSuppHd _result = null;

            try
            {
                _result = this.db.FINCNSuppHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCNSuppHd GetSingleFINCNSuppHdView(string _prmCode)
        {
            FINCNSuppHd _result = new FINCNSuppHd();

            try
            {
                var _query = (
                               from _finCNSuppHd in this.db.FINCNSuppHds
                               join _msSupp in this.db.MsSuppliers
                                    on _finCNSuppHd.SuppCode equals _msSupp.SuppCode
                               join _msTerm in this.db.MsTerms
                               on _finCNSuppHd.Term equals _msTerm.TermCode
                               orderby _finCNSuppHd.DatePrep descending
                               where _finCNSuppHd.TransNmbr == _prmCode
                               select new
                               {
                                   TransNmbr = _finCNSuppHd.TransNmbr,
                                   FileNmbr = _finCNSuppHd.FileNmbr,
                                   TransDate = _finCNSuppHd.TransDate,
                                   Status = _finCNSuppHd.Status,
                                   SuppCode = _finCNSuppHd.SuppCode,
                                   SuppName = _msSupp.SuppName,
                                   CurrCode = _finCNSuppHd.CurrCode,
                                   ForexRate = _finCNSuppHd.ForexRate,
                                   TotalForex = _finCNSuppHd.TotalForex,
                                   Term = _finCNSuppHd.Term,
                                   TermName = _msTerm.TermName,
                                   DiscForex = _finCNSuppHd.DiscForex,
                                   BaseForex = _finCNSuppHd.BaseForex,
                                   Attn = _finCNSuppHd.Attn,
                                   Remark = _finCNSuppHd.Remark,
                                   PPN = _finCNSuppHd.PPN,
                                   PPNDate = _finCNSuppHd.PPNDate,
                                   PPNForex = _finCNSuppHd.PPNForex,
                                   PPNNo = _finCNSuppHd.PPNNo,
                                   PPNRate = _finCNSuppHd.PPNRate,
                                   SuppInvNo = _finCNSuppHd.SuppInvNo,
                                   SuppPONo = _finCNSuppHd.SuppPONo
                               }
                           ).Single();

                _result.TransNmbr = _query.TransNmbr;
                _result.FileNmbr = _query.FileNmbr;
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
                _result.PPN = _query.PPN;
                _result.PPNDate = _query.PPNDate;
                _result.PPNForex = _query.PPNForex;
                _result.PPNNo = _query.PPNNo;
                _result.PPNRate = _query.PPNRate;
                _result.SuppInvNo = _query.SuppInvNo;
                _result.SuppPONo = _query.SuppPONo;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINCNSuppHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCNSuppHd _finCNSuppHd = this.db.FINCNSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finCNSuppHd != null)
                    {
                        if ((_finCNSuppHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINCNSuppDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINCNSuppDts.DeleteAllOnSubmit(_query);

                            this.db.FINCNSuppHds.DeleteOnSubmit(_finCNSuppHd);

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

        public string AddFINCNSuppHd(FINCNSuppHd _prmFINCNSuppHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINCNSuppHd.TransDate.Year, _prmFINCNSuppHd.TransDate.Month, AppModule.GetValue(TransactionType.NotaCreditSupplier), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINCNSuppHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINCNSuppHds.InsertOnSubmit(_prmFINCNSuppHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINCNSuppHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINCNSuppHd(FINCNSuppHd _prmFINCNSuppHd)
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
                int _success = this.db.S_FNSICNGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.NotaCreditSupplier);
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
                    int _success = this.db.S_FNSICNApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINCNSuppHd _finCNSuppHd = this.GetSingleFINCNSuppHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finCNSuppHd.TransDate.Year, _finCNSuppHd.TransDate.Month, AppModule.GetValue(TransactionType.NotaCreditSupplier), this._companyTag, ""))
                        {
                            _finCNSuppHd.FileNmbr = item.Number;
                        }
                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.NotaCreditSupplier);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINCNSuppHd(_prmTransNmbr).FileNmbr;
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
                FINCNSuppHd _finCNSuppHd = this.db.FINCNSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finCNSuppHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSICNPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.NotaCreditSupplier);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINCNSuppHd(_prmTransNmbr).FileNmbr;
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
                FINCNSuppHd _finCNSuppHd = this.db.FINCNSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finCNSuppHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSICNUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.NotaCreditSupplier);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINCNSuppHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINCNSuppHd(_prmTransNmbr).TransDate;
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

        public bool GetSingleFINCNSuppHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCNSuppHd _finCNSuppHd = this.db.FINCNSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finCNSuppHd != null)
                    {
                        if (_finCNSuppHd.Status != NotaCreditSuppDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINCNSuppHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCNSuppHd _finCNSuppHd = this.db.FINCNSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finCNSuppHd.Status == NotaCreditSuppDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finCNSuppHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finCNSuppHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finCNSuppHd != null)
                    {
                        if ((_finCNSuppHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINCNSuppDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINCNSuppDts.DeleteAllOnSubmit(_query);

                            this.db.FINCNSuppHds.DeleteOnSubmit(_finCNSuppHd);

                            _result = true;
                        }
                        else if (_finCNSuppHd.FileNmbr != "" && _finCNSuppHd.Status == NotaCreditSuppDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finCNSuppHd.Status = NotaCreditSuppDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINCNSuppDt
        public int RowsCountFINCNSuppDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINCNSuppDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINCNSuppDt> GetListFINCNSuppDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<FINCNSuppDt> _result = new List<FINCNSuppDt>();

            try
            {
                var _query = (
                                from _finCNSuppDt in this.db.FINCNSuppDts
                                join _msAccount in this.db.MsAccounts
                                    on _finCNSuppDt.Account equals _msAccount.Account
                                join _msUnit in this.db.MsUnits
                                    on _finCNSuppDt.Unit equals _msUnit.UnitCode

                                where _finCNSuppDt.TransNmbr == _prmCode
                                orderby _finCNSuppDt.ItemNo ascending
                                select new
                                {
                                    ItemNo = _finCNSuppDt.ItemNo,
                                    Account = _finCNSuppDt.Account,
                                    AccountName = _msAccount.AccountName,
                                    AmountForex = _finCNSuppDt.AmountForex,
                                    Subled = _finCNSuppDt.Subled,
                                    SubledName = (
                                                    from _viewMsSubled in this.db.V_MsSubleds
                                                    where _viewMsSubled.SubLed_No == _finCNSuppDt.Subled
                                                    select _viewMsSubled.SubLed_Name
                                                 ).FirstOrDefault(),
                                    PriceForex = _finCNSuppDt.PriceForex,
                                    Qty = _finCNSuppDt.Qty,
                                    Unit = _finCNSuppDt.Unit,
                                    UnitName = _msUnit.UnitName,
                                    Remark = _finCNSuppDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINCNSuppDt(_row.ItemNo, _row.Account, _row.AccountName, _row.AmountForex, _row.Subled, _row.SubledName, _row.PriceForex, _row.Qty, _row.Unit, _row.UnitName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCNSuppDt GetSingleFINCNSuppDt(string _prmCode, int _prmItemNo)
        {
            FINCNSuppDt _result = null;

            try
            {
                _result = this.db.FINCNSuppDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCNSuppDt GetSingleFINCNSuppDtView(string _prmCode, int _prmItemNo)
        {
            FINCNSuppDt _result = new FINCNSuppDt();

            try
            {
                var _query = (
                               from _finCNSuppDt in this.db.FINCNSuppDts
                               join _msAccount in this.db.MsAccounts
                                    on _finCNSuppDt.Account equals _msAccount.Account
                               join _msUnit in this.db.MsUnits
                                    on _finCNSuppDt.Unit equals _msUnit.UnitCode
                               orderby _finCNSuppDt.ItemNo ascending
                               where _finCNSuppDt.TransNmbr == _prmCode && _finCNSuppDt.ItemNo == _prmItemNo
                               select new
                               {
                                   ItemNo = _finCNSuppDt.ItemNo,
                                   Account = _finCNSuppDt.Account,
                                   AccountName = _msAccount.AccountName,
                                   AmountForex = _finCNSuppDt.AmountForex,
                                   Subled = _finCNSuppDt.Subled,
                                   SubledName = (
                                                    from _viewMsSubled in this.db.V_MsSubleds
                                                    where _viewMsSubled.SubLed_No == _finCNSuppDt.Subled
                                                    select _viewMsSubled.SubLed_Name
                                                 ).FirstOrDefault(),
                                   PriceForex = _finCNSuppDt.PriceForex,
                                   Qty = _finCNSuppDt.Qty,
                                   Unit = _finCNSuppDt.Unit,
                                   UnitName = _msUnit.UnitName,
                                   Remark = _finCNSuppDt.Remark
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

        public bool DeleteMultiFINCNSuppDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            FINCNSuppHd _finCNSuppHd = new FINCNSuppHd();

            decimal _total = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCNSuppDt _finCNSuppDt = this.db.FINCNSuppDts.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmCode[i]) && _temp.TransNmbr == _prmTransNo);

                    this.db.FINCNSuppDts.DeleteOnSubmit(_finCNSuppDt);
                }

                var _query = (
                                from _finCNSuppDt2 in this.db.FINCNSuppDts
                                where !(
                                             from _code in _prmCode
                                             select _code
                                        ).Contains(_finCNSuppDt2.ItemNo.ToString())
                                        && _finCNSuppDt2.TransNmbr == _prmTransNo
                                group _finCNSuppDt2 by _finCNSuppDt2.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finCNSuppHd = this.db.FINCNSuppHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _finCNSuppHd.BaseForex = _total;
                _finCNSuppHd.PPNForex = (_finCNSuppHd.BaseForex - _finCNSuppHd.DiscForex) * _finCNSuppHd.PPN / 100;
                _finCNSuppHd.TotalForex = _finCNSuppHd.BaseForex - _finCNSuppHd.DiscForex + _finCNSuppHd.PPNForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINCNSuppDt(FINCNSuppDt _prmFINCNSuppDt)
        {
            bool _result = false;

            FINCNSuppHd _finCNSuppHd = new FINCNSuppHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finCNSuppDt in this.db.FINCNSuppDts
                               where !(
                                           from _finCNSuppDt2 in this.db.FINCNSuppDts
                                           where _finCNSuppDt2.ItemNo == _prmFINCNSuppDt.ItemNo && _finCNSuppDt2.TransNmbr == _prmFINCNSuppDt.TransNmbr
                                           select _finCNSuppDt2.ItemNo
                                       ).Contains(_finCNSuppDt.ItemNo)
                                       && _finCNSuppDt.TransNmbr == _prmFINCNSuppDt.TransNmbr
                               group _finCNSuppDt by _finCNSuppDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }


                _finCNSuppHd = this.db.FINCNSuppHds.Single(_fa => _fa.TransNmbr == _prmFINCNSuppDt.TransNmbr);

                _finCNSuppHd.BaseForex = _total + _prmFINCNSuppDt.AmountForex;
                _finCNSuppHd.PPNForex = (_finCNSuppHd.BaseForex - _finCNSuppHd.DiscForex) * _finCNSuppHd.PPN / 100;
                _finCNSuppHd.TotalForex = _finCNSuppHd.BaseForex - _finCNSuppHd.DiscForex + _finCNSuppHd.PPNForex;

                this.db.FINCNSuppDts.InsertOnSubmit(_prmFINCNSuppDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINCNSuppDt(FINCNSuppDt _prmFINCNSuppDt)
        {
            bool _result = false;

            FINCNSuppHd _finCNSuppHd = new FINCNSuppHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finCNSuppDt in this.db.FINCNSuppDts
                               where !(
                                           from _finCNSuppDt2 in this.db.FINCNSuppDts
                                           where _finCNSuppDt2.ItemNo == _prmFINCNSuppDt.ItemNo && _finCNSuppDt2.TransNmbr == _prmFINCNSuppDt.TransNmbr
                                           select _finCNSuppDt2.ItemNo
                                       ).Contains(_finCNSuppDt.ItemNo)
                                       && _finCNSuppDt.TransNmbr == _prmFINCNSuppDt.TransNmbr
                               group _finCNSuppDt by _finCNSuppDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finCNSuppHd = this.db.FINCNSuppHds.Single(_fa => _fa.TransNmbr == _prmFINCNSuppDt.TransNmbr);

                _finCNSuppHd.BaseForex = _total + _prmFINCNSuppDt.AmountForex;
                _finCNSuppHd.PPNForex = (_finCNSuppHd.BaseForex - _finCNSuppHd.DiscForex) * _finCNSuppHd.PPN / 100;
                _finCNSuppHd.TotalForex = _finCNSuppHd.BaseForex - _finCNSuppHd.DiscForex + _finCNSuppHd.PPNForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxNoItemFINCNSuppDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINCNSuppDts.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~NotaCreditSupplierBL()
        {
        }
    }
}
