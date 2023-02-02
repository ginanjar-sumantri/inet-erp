using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class FINDPCustomerBL : Base
    {
        public FINDPCustomerBL()
        {

        }

        #region FINDPCustHd
        public double RowsCountFINDPCustHd(string _prmCategory, string _prmKeyword)
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
                            from _finDPCustHd in this.db.FINDPCustHds
                            join _msCust in this.db.MsCustomers
                                   on _finDPCustHd.CustCode equals _msCust.CustCode
                            where (SqlMethods.Like(_finDPCustHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_finDPCustHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _finDPCustHd.Status != DPCustomerDataMapper.GetStatus(TransStatus.Deleted)
                            select _finDPCustHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINDPCustHd> GetListFINDPCustHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINDPCustHd> _result = new List<FINDPCustHd>();

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
                                from _finDPCustHd in this.db.FINDPCustHds
                                join _msCust in this.db.MsCustomers
                                    on _finDPCustHd.CustCode equals _msCust.CustCode
                                where (SqlMethods.Like(_finDPCustHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finDPCustHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finDPCustHd.Status != DPCustomerDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finDPCustHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finDPCustHd.TransNmbr,
                                    FileNmbr = _finDPCustHd.FileNmbr,
                                    TransDate = _finDPCustHd.TransDate,
                                    CurrCode = _finDPCustHd.CurrCode,
                                    Status = _finDPCustHd.Status,
                                    CustCode = _finDPCustHd.CustCode,
                                    CustName = _msCust.CustName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPCustHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.CustCode, _row.CustName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDPCustHd GetSingleFINDPCustHd(string _prmCode)
        {
            FINDPCustHd _result = null;

            try
            {
                _result = this.db.FINDPCustHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public FINDPCustHd GetSingleFINDPCustHdView(string _prmCode)
        //{
        //    FINDPCustHd _result = new FINDPCustHd();

        //    try
        //    {
        //        var _query = (
        //                       from _finDPCustHd in this.db.FINDPCustHds
        //                       join _msCust in this.db.MsCustomers
        //                            on _finDPCustHd.CustCode equals _msCust.CustCode
        //                       orderby _finDPCustHd.DatePrep descending
        //                       where _finDPCustHd.TransNmbr == _prmCode
        //                       select new
        //                       {
        //                           TransNmbr = _finDPCustHd.TransNmbr,
        //                           TransDate = _finDPCustHd.TransDate,
        //                           Status = _finDPCustHd.Status,
        //                           CustCode = _finDPCustHd.CustCode,
        //                           CustName = _msCust.CustName,
        //                           CurrCode = _finDPCustHd.CurrCode,
        //                           ForexRate = _finDPCustHd.ForexRate,
        //                           TotalForex = _finDPCustHd.TotalForex,
        //                           BaseForex = _finDPCustHd.BaseForex,
        //                           Attn = _finDPCustHd.Attn,
        //                           Remark = _finDPCustHd.Remark
        //                       }
        //                   ).Single();

        //        _result.TransNmbr = _query.TransNmbr;
        //        _result.TransDate = _query.TransDate;
        //        _result.Status = _query.Status;
        //        _result.CustCode = _query.CustCode;
        //        _result.CustName = _query.CustName;
        //        _result.CurrCode = _query.CurrCode;
        //        _result.ForexRate = _query.ForexRate;
        //        _result.TotalForex = _query.TotalForex;
        //        _result.BaseForex = _query.BaseForex;
        //        _result.Attn = _query.Attn;
        //        _result.Remark = _query.Remark;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool DeleteMultiFINDPCustHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPCustHd _finDPCustHd = this.db.FINDPCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPCustHd != null)
                    {
                        if ((_finDPCustHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINDPCustDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINDPCustDts.DeleteAllOnSubmit(_query);

                            this.db.FINDPCustHds.DeleteOnSubmit(_finDPCustHd);

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

        public string AddFINDPCustHd(FINDPCustHd _prmFINDPCustHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINDPCustHd.TransDate.Year, _prmFINDPCustHd.TransDate.Month, AppModule.GetValue(TransactionType.DPCustomer), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINDPCustHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINDPCustHds.InsertOnSubmit(_prmFINDPCustHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINDPCustHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDPCustHd(FINDPCustHd _prmFINDPCustHd)
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
                int _success = this.db.S_FNDPCustGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.DPCustomer);
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
                    int _success = this.db.S_FNDPCustApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINDPCustHd _finDPCustHd = this.GetSingleFINDPCustHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finDPCustHd.TransDate.Year, _finDPCustHd.TransDate.Month, AppModule.GetValue(TransactionType.DPCustomer), this._companyTag, ""))
                        {
                            _finDPCustHd.FileNmbr = item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.DPCustomer);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINDPCustHd(_prmTransNmbr).FileNmbr;
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
                FINDPCustHd _finDPCustHd = this.db.FINDPCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDPCustHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNDPCustPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.DPCustomer);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINDPCustHd(_prmTransNmbr).FileNmbr;
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
                FINDPCustHd _finDPCustHd = this.db.FINDPCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDPCustHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNDPCustUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.DPCustomer);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINDPCustHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINDPCustHd(_prmTransNmbr).TransDate;
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

        public string GetCurrCodeHeader(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finDPCustHd in this.db.FINDPCustHds
                                where _finDPCustHd.TransNmbr == _prmCode
                                select new
                                {
                                    CurrCode = _finDPCustHd.CurrCode
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

        public bool GetSingleFINDPCustHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPCustHd _finDPCustHd = this.db.FINDPCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPCustHd != null)
                    {
                        if (_finDPCustHd.Status != DPCustomerDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINDPCustHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPCustHd _finDPCustHd = this.db.FINDPCustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPCustHd.Status == DPCustomerDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finDPCustHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finDPCustHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finDPCustHd != null)
                    {
                        if ((_finDPCustHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINDPCustDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINDPCustDts.DeleteAllOnSubmit(_query);

                            this.db.FINDPCustHds.DeleteOnSubmit(_finDPCustHd);

                            _result = true;
                        }
                        else if (_finDPCustHd.FileNmbr != "" && _finDPCustHd.Status == DPCustomerDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finDPCustHd.Status = DPCustomerDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINDPCustDt
        public int RowsCountFINDPCustDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINDPCustDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINDPCustDt> GetListFINDPCustDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<FINDPCustDt> _result = new List<FINDPCustDt>();

            try
            {
                var _query = (
                                from _finDPCustDt in this.db.FINDPCustDts
                                where _finDPCustDt.TransNmbr == _prmCode
                                orderby _finDPCustDt.ItemNo ascending
                                select new
                                {
                                    ItemNo = _finDPCustDt.ItemNo,
                                    ReceiptType = _finDPCustDt.ReceiptType,
                                    DueDate = _finDPCustDt.DueDate,
                                    BankGiro = _finDPCustDt.BankGiro,
                                    AmountForex = _finDPCustDt.AmountForex
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPCustDt(_row.ItemNo, _row.ReceiptType, _row.DueDate, _row.BankGiro, _row.AmountForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDPCustDt GetSingleFINDPCustDt(string _prmCode, int _prmItemNo)
        {
            FINDPCustDt _result = null;

            try
            {
                _result = this.db.FINDPCustDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINDPCustDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            FINDPCustHd _finDPCustHd = new FINDPCustHd();

            decimal _total = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPCustDt _finDPCustDt = this.db.FINDPCustDts.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmCode[i]) && _temp.TransNmbr == _prmTransNo);

                    this.db.FINDPCustDts.DeleteOnSubmit(_finDPCustDt);
                }

                var _query = (
                                from _finDPCustDt2 in this.db.FINDPCustDts
                                where !(
                                            from _code in _prmCode
                                            select _code
                                        ).Contains(_finDPCustDt2.ItemNo.ToString())
                                        && _finDPCustDt2.TransNmbr == _prmTransNo
                                group _finDPCustDt2 by _finDPCustDt2.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finDPCustHd = this.db.FINDPCustHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _finDPCustHd.TotalForex = _total;
                _finDPCustHd.BaseForex = (_finDPCustHd.TotalForex * 100) / (100 + _finDPCustHd.PPN);
                _finDPCustHd.PPNForex = (_finDPCustHd.BaseForex * _finDPCustHd.PPN) / 100;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINDPCustDt(FINDPCustDt _prmFINDPCustDt)
        {
            bool _result = false;

            FINDPCustHd _finDPCustHd = new FINDPCustHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finDPCustDt in this.db.FINDPCustDts
                               where !(
                                           from _finDPCustDt2 in this.db.FINDPCustDts
                                           where _finDPCustDt2.ItemNo == _prmFINDPCustDt.ItemNo && _finDPCustDt2.TransNmbr == _prmFINDPCustDt.TransNmbr
                                           select _finDPCustDt2.ItemNo
                                       ).Contains(_finDPCustDt.ItemNo)
                                       && _finDPCustDt.TransNmbr == _prmFINDPCustDt.TransNmbr
                               group _finDPCustDt by _finDPCustDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finDPCustHd = this.db.FINDPCustHds.Single(_fa => _fa.TransNmbr == _prmFINDPCustDt.TransNmbr);

                _finDPCustHd.TotalForex = _total + _prmFINDPCustDt.AmountForex;
                _finDPCustHd.BaseForex = (_finDPCustHd.TotalForex * 100) / (100 + _finDPCustHd.PPN);
                _finDPCustHd.PPNForex = (_finDPCustHd.BaseForex * _finDPCustHd.PPN) / 100;
                this.db.FINDPCustDts.InsertOnSubmit(_prmFINDPCustDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDPCustDt(FINDPCustDt _prmFINDPCustDt)
        {
            bool _result = false;

            FINDPCustHd _finDPCustHd = new FINDPCustHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finDPCustDt in this.db.FINDPCustDts
                               where !(
                                           from _finDPCustDt2 in this.db.FINDPCustDts
                                           where _finDPCustDt2.ItemNo == _prmFINDPCustDt.ItemNo && _finDPCustDt2.TransNmbr == _prmFINDPCustDt.TransNmbr
                                           select _finDPCustDt2.ItemNo
                                       ).Contains(_finDPCustDt.ItemNo)
                                       && _finDPCustDt.TransNmbr == _prmFINDPCustDt.TransNmbr
                               group _finDPCustDt by _finDPCustDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finDPCustHd = this.db.FINDPCustHds.Single(_fa => _fa.TransNmbr == _prmFINDPCustDt.TransNmbr);

                _finDPCustHd.TotalForex = _total + _prmFINDPCustDt.AmountForex;
                _finDPCustHd.BaseForex = (_finDPCustHd.TotalForex * 100) / (100 + _finDPCustHd.PPN);
                _finDPCustHd.PPNForex = (_finDPCustHd.BaseForex * _finDPCustHd.PPN) / 100;
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxNoItemFINDPCustDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINDPCustDts.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~FINDPCustomerBL()
        {
        }
    }
}
