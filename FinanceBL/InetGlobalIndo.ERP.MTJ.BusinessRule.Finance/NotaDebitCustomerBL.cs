using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class NotaDebitCustomerBL : Base
    {
        public NotaDebitCustomerBL()
        {

        }

        #region FINDNCustHd
        public double RowsCountFINDNCustHd(string _prmCategory, string _prmKeyword)
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
                           from _finDNCustHd in this.db.FINDNCustHds
                           join _msCust in this.db.MsCustomers
                               on _finDNCustHd.CustCode equals _msCust.CustCode
                           where (SqlMethods.Like(_finDNCustHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                              && (SqlMethods.Like((_finDNCustHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                              && _finDNCustHd.Status != NotaDebitCustDataMapper.GetStatus(TransStatus.Deleted)
                           select _finDNCustHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINDNCustHd> GetListFINDNCustHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINDNCustHd> _result = new List<FINDNCustHd>();

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
                                from _finDNCustHd in this.db.FINDNCustHds
                                join _msCust in this.db.MsCustomers
                                    on _finDNCustHd.CustCode equals _msCust.CustCode
                                where (SqlMethods.Like(_finDNCustHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finDNCustHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finDNCustHd.Status != NotaDebitCustDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finDNCustHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finDNCustHd.TransNmbr,
                                    FileNmbr = _finDNCustHd.FileNmbr,
                                    TransDate = _finDNCustHd.TransDate,
                                    CurrCode = _finDNCustHd.CurrCode,
                                    Status = _finDNCustHd.Status,
                                    CustCode = _finDNCustHd.CustCode,
                                    CustName = _msCust.CustName,
                                    Term = _finDNCustHd.Term,
                                    TermName = (
                                                   from _msTerm in this.db.MsTerms
                                                   where _finDNCustHd.Term == _msTerm.TermCode
                                                   select _msTerm.TermName
                                               ).FirstOrDefault(),
                                    BillTo = _finDNCustHd.BillTo,
                                    BillToName = (
                                                   from _msCustomer in this.db.MsCustomers
                                                   where _finDNCustHd.BillTo == _msCustomer.CustCode
                                                   select _msCustomer.CustName
                                               ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINDNCustHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.CustCode, _row.CustName, _row.Term, _row.TermName, _row.BillTo, _row.BillToName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDNCustHd GetSingleFINDNCustHd(string _prmCode)
        {
            FINDNCustHd _result = null;

            try
            {
                _result = this.db.FINDNCustHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDNCustHd GetSingleFINDNCustHdView(string _prmCode)
        {
            FINDNCustHd _result = new FINDNCustHd();

            try
            {
                var _query = (
                               from _finDNCustHd in this.db.FINDNCustHds
                               orderby _finDNCustHd.DatePrep descending
                               where _finDNCustHd.TransNmbr == _prmCode
                               select new
                               {
                                   TransNmbr = _finDNCustHd.TransNmbr,
                                   FileNmbr = _finDNCustHd.FileNmbr,
                                   TransDate = _finDNCustHd.TransDate,
                                   Status = _finDNCustHd.Status,
                                   CustCode = _finDNCustHd.CustCode,
                                   CustName = (
                                                    from _msCust in this.db.MsCustomers
                                                    where _finDNCustHd.CustCode == _msCust.CustCode
                                                    select _msCust.CustName
                                                ).FirstOrDefault(),
                                   CurrCode = _finDNCustHd.CurrCode,
                                   ForexRate = _finDNCustHd.ForexRate,
                                   TotalForex = _finDNCustHd.TotalForex,
                                   Term = _finDNCustHd.Term,
                                   TermName = (
                                                 from _msTerm in this.db.MsTerms
                                                 where _finDNCustHd.Term == _msTerm.TermCode
                                                 select _msTerm.TermName
                                              ).FirstOrDefault(),
                                   DiscForex = _finDNCustHd.DiscForex,
                                   BaseForex = _finDNCustHd.BaseForex,
                                   Attn = _finDNCustHd.Attn,
                                   Remark = _finDNCustHd.Remark,
                                   PPN = _finDNCustHd.PPN,
                                   PPNDate = _finDNCustHd.PPNDate,
                                   PPNForex = _finDNCustHd.PPNForex,
                                   PPNNo = _finDNCustHd.PPNNo,
                                   PPNRate = _finDNCustHd.PPNRate,
                                   BillTo = _finDNCustHd.BillTo,
                                   BillToName = new CustomerBL().GetNameByCode(_finDNCustHd.BillTo),
                                   CustPONo = _finDNCustHd.CustPONo
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
                _result.DiscForex = _query.DiscForex;
                _result.BaseForex = _query.BaseForex;
                _result.Attn = _query.Attn;
                _result.Remark = _query.Remark;
                _result.PPN = _query.PPN;
                _result.PPNDate = _query.PPNDate;
                _result.PPNForex = _query.PPNForex;
                _result.PPNNo = _query.PPNNo;
                _result.PPNRate = _query.PPNRate;
                _result.BillTo = _query.BillTo;
                _result.BillToName = _query.BillToName;
                _result.CustPONo = _query.CustPONo;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINDNCustHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDNCustHd _finDNCustHd = this.db.FINDNCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDNCustHd != null)
                    {
                        if ((_finDNCustHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINDNCustDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINDNCustDts.DeleteAllOnSubmit(_query);

                            this.db.FINDNCustHds.DeleteOnSubmit(_finDNCustHd);

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

        public string AddFINDNCustHd(FINDNCustHd _prmFINDNCustHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINDNCustHd.TransDate.Year, _prmFINDNCustHd.TransDate.Month, AppModule.GetValue(TransactionType.NotaDebitCustomer), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINDNCustHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINDNCustHds.InsertOnSubmit(_prmFINDNCustHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINDNCustHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDNCustHd(FINDNCustHd _prmFINDNCustHd)
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
                int _success = this.db.S_FNCIDNGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.NotaDebitCustomer);
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
                    int _success = this.db.S_FNCIDNApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINDNCustHd _finDNCustHd = this.GetSingleFINDNCustHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finDNCustHd.TransDate.Year, _finDNCustHd.TransDate.Month, AppModule.GetValue(TransactionType.NotaDebitCustomer), this._companyTag, ""))
                        {
                            _finDNCustHd.FileNmbr = item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.NotaDebitCustomer);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINDNCustHd(_prmTransNmbr).FileNmbr;
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
                FINDNCustHd _finDnCustHd = this.db.FINDNCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDnCustHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNCIDNPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.NotaDebitCustomer);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINDNCustHd(_prmTransNmbr).FileNmbr;
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
                FINDNCustHd _finDnCustHd = this.db.FINDNCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDnCustHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {

                    int _success = this.db.S_FNCIDNUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.NotaDebitCustomer);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINDNCustHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINDNCustHd(_prmTransNmbr).TransDate;
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

        public bool GetSingleFINDNCustHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDNCustHd _finDNCustHd = this.db.FINDNCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDNCustHd != null)
                    {
                        if (_finDNCustHd.Status != NotaDebitCustDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINDNCustHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDNCustHd _finDNCustHd = this.db.FINDNCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDNCustHd.Status == NotaDebitCustDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finDNCustHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finDNCustHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finDNCustHd != null)
                    {
                        if ((_finDNCustHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINDNCustDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINDNCustDts.DeleteAllOnSubmit(_query);

                            this.db.FINDNCustHds.DeleteOnSubmit(_finDNCustHd);

                            _result = true;
                        }
                        else if (_finDNCustHd.FileNmbr != "" && _finDNCustHd.Status == NotaDebitCustDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finDNCustHd.Status = NotaDebitCustDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINDNCustDt
        public int RowsCountFINDNCustDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINDNCustDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINDNCustDt> GetListFINDNCustDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<FINDNCustDt> _result = new List<FINDNCustDt>();

            try
            {
                var _query = (
                                from _finDNCustDt in this.db.FINDNCustDts
                                join _msAccount in this.db.MsAccounts
                                    on _finDNCustDt.Account equals _msAccount.Account
                                join _msUnit in this.db.MsUnits
                                    on _finDNCustDt.Unit equals _msUnit.UnitCode
                                where _finDNCustDt.TransNmbr == _prmCode
                                orderby _finDNCustDt.ItemNo ascending
                                select new
                                {
                                    ItemNo = _finDNCustDt.ItemNo,
                                    Account = _finDNCustDt.Account,
                                    AccountName = _msAccount.AccountName,
                                    AmountForex = _finDNCustDt.AmountForex,
                                    Subled = _finDNCustDt.SubLed,
                                    SubledName = (
                                                    from _viewMsSubled in this.db.V_MsSubleds
                                                    where _viewMsSubled.SubLed_No == _finDNCustDt.SubLed
                                                    select _viewMsSubled.SubLed_Name
                                                 ).FirstOrDefault(),
                                    PriceForex = _finDNCustDt.PriceForex,
                                    Qty = _finDNCustDt.Qty,
                                    Unit = _finDNCustDt.Unit,
                                    UnitName = _msUnit.UnitName,
                                    Remark = _finDNCustDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINDNCustDt(_row.ItemNo, _row.Account, _row.AccountName, _row.AmountForex, _row.Subled, _row.SubledName, _row.PriceForex, _row.Qty, _row.Unit, _row.UnitName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDNCustDt GetSingleFINDNCustDt(string _prmCode, int _prmItemNo)
        {
            FINDNCustDt _result = null;

            try
            {
                _result = this.db.FINDNCustDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDNCustDt GetSingleFINDNCustDtView(string _prmCode, int _prmItemNo)
        {
            FINDNCustDt _result = new FINDNCustDt();

            try
            {
                var _query = (
                               from _finDNCustDt in this.db.FINDNCustDts
                               join _msAccount in this.db.MsAccounts
                                    on _finDNCustDt.Account equals _msAccount.Account
                               join _msUnit in this.db.MsUnits
                                    on _finDNCustDt.Unit equals _msUnit.UnitCode
                               orderby _finDNCustDt.ItemNo ascending
                               where _finDNCustDt.TransNmbr == _prmCode && _finDNCustDt.ItemNo == _prmItemNo
                               select new
                               {
                                   ItemNo = _finDNCustDt.ItemNo,
                                   Account = _finDNCustDt.Account,
                                   AccountName = _msAccount.AccountName,
                                   AmountForex = _finDNCustDt.AmountForex,
                                   Subled = _finDNCustDt.SubLed,
                                   SubledName = (
                                                    from _viewMsSubled in this.db.V_MsSubleds
                                                    where _viewMsSubled.SubLed_No == _finDNCustDt.SubLed
                                                    select _viewMsSubled.SubLed_Name
                                                 ).FirstOrDefault(),
                                   PriceForex = _finDNCustDt.PriceForex,
                                   Qty = _finDNCustDt.Qty,
                                   Unit = _finDNCustDt.Unit,
                                   UnitName = _msUnit.UnitName,
                                   Remark = _finDNCustDt.Remark
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

        public bool DeleteMultiFINDNCustDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            FINDNCustHd _finDNCustHd = new FINDNCustHd();

            decimal _total = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDNCustDt _finDNCustDt = this.db.FINDNCustDts.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmCode[i]) && _temp.TransNmbr == _prmTransNo);

                    this.db.FINDNCustDts.DeleteOnSubmit(_finDNCustDt);
                }

                var _query = (
                                from _finDNCustDt2 in this.db.FINDNCustDts
                                where !(
                                            from _code in _prmCode
                                            select _code
                                        ).Contains(_finDNCustDt2.ItemNo.ToString())
                                        && _finDNCustDt2.TransNmbr == _prmTransNo
                                group _finDNCustDt2 by _finDNCustDt2.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finDNCustHd = this.db.FINDNCustHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _finDNCustHd.BaseForex = _total;
                _finDNCustHd.PPNForex = (_finDNCustHd.BaseForex - _finDNCustHd.DiscForex) * _finDNCustHd.PPN / 100;
                _finDNCustHd.TotalForex = _finDNCustHd.BaseForex - _finDNCustHd.DiscForex + _finDNCustHd.PPNForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINDNCustDt(FINDNCustDt _prmFINDNCustDt)
        {
            bool _result = false;

            FINDNCustHd _finDNCustHd = new FINDNCustHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finDNCustDt in this.db.FINDNCustDts
                               where !(
                                           from _finDNCustDt2 in this.db.FINDNCustDts
                                           where _finDNCustDt2.ItemNo == _prmFINDNCustDt.ItemNo && _finDNCustDt2.TransNmbr == _prmFINDNCustDt.TransNmbr
                                           select _finDNCustDt2.ItemNo
                                       ).Contains(_finDNCustDt.ItemNo)
                                       && _finDNCustDt.TransNmbr == _prmFINDNCustDt.TransNmbr
                               group _finDNCustDt by _finDNCustDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }


                _finDNCustHd = this.db.FINDNCustHds.Single(_fa => _fa.TransNmbr == _prmFINDNCustDt.TransNmbr);

                _finDNCustHd.BaseForex = _total + _prmFINDNCustDt.AmountForex;
                _finDNCustHd.PPNForex = (_finDNCustHd.BaseForex - _finDNCustHd.DiscForex) * _finDNCustHd.PPN / 100;
                _finDNCustHd.TotalForex = _finDNCustHd.BaseForex - _finDNCustHd.DiscForex + _finDNCustHd.PPNForex;

                this.db.FINDNCustDts.InsertOnSubmit(_prmFINDNCustDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDNCustDt(FINDNCustDt _prmFINDNCustDt)
        {
            bool _result = false;

            FINDNCustHd _finDNCustHd = new FINDNCustHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finDNCustDt in this.db.FINDNCustDts
                               where !(
                                           from _finDNCustDt2 in this.db.FINDNCustDts
                                           where _finDNCustDt2.ItemNo == _prmFINDNCustDt.ItemNo && _finDNCustDt2.TransNmbr == _prmFINDNCustDt.TransNmbr
                                           select _finDNCustDt2.ItemNo
                                       ).Contains(_finDNCustDt.ItemNo)
                                       && _finDNCustDt.TransNmbr == _prmFINDNCustDt.TransNmbr
                               group _finDNCustDt by _finDNCustDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finDNCustHd = this.db.FINDNCustHds.Single(_fa => _fa.TransNmbr == _prmFINDNCustDt.TransNmbr);

                _finDNCustHd.BaseForex = _total + _prmFINDNCustDt.AmountForex;
                _finDNCustHd.PPNForex = (_finDNCustHd.BaseForex - _finDNCustHd.DiscForex) * _finDNCustHd.PPN / 100;
                _finDNCustHd.TotalForex = _finDNCustHd.BaseForex - _finDNCustHd.DiscForex + _finDNCustHd.PPNForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxNoItemFINDNCustDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINDNCustDts.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~NotaDebitCustomerBL()
        {
        }
    }
}
