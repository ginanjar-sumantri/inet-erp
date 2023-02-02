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
    public sealed class FINARRateBL : Base
    {
        public FINARRateBL()
        {

        }

        #region FINARRateHd
        public double RowsCountFINARRateHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _finARRateHd in this.db.FINARRateHds
                            where (SqlMethods.Like(_finARRateHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            && (SqlMethods.Like((_finARRateHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                            && _finARRateHd.Status !=  ARRateDataMapper.GetStatus(TransStatus.Deleted)
                            select _finARRateHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINARRateHd> GetListFINARRateHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINARRateHd> _result = new List<FINARRateHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _finARRateHd in this.db.FINARRateHds
                                where (SqlMethods.Like(_finARRateHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_finARRateHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _finARRateHd.Status != ARRateDataMapper.GetStatus(TransStatus.Deleted)
                                // || ((_finARRateHd.FileNmbr ?? "") != ""))
                                orderby _finARRateHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finARRateHd.TransNmbr,
                                    FileNmbr = _finARRateHd.FileNmbr,
                                    TransDate = _finARRateHd.TransDate,
                                    CurrCode = _finARRateHd.CurrCode,
                                    Status = _finARRateHd.Status,
                                    NewRate = _finARRateHd.NewRate,
                                    Remark = _finARRateHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINARRateHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.NewRate, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINARRateHd GetSingleFINARRateHd(string _prmCode)
        {
            FINARRateHd _result = null;

            try
            {
                _result = this.db.FINARRateHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINARRateHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINARRateHd _finARRateHd = this.db.FINARRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finARRateHd != null)
                    {
                        if ((_finARRateHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINARRateDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINARRateDts.DeleteAllOnSubmit(_query);

                            this.db.FINARRateHds.DeleteOnSubmit(_finARRateHd);

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

        public string AddFINARRateHd(FINARRateHd _prmFINARRateHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINARRateHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.FINARRateHds.InsertOnSubmit(_prmFINARRateHd);
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINARRateHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINARRateHd(FINARRateHd _prmFINARRateHd)
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
                int _success = this.db.S_FNCIRateGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ARRate);
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
                    int _success = this.db.S_FNCIRateApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINARRateHd _finARRateHd = this.GetSingleFINARRateHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finARRateHd.TransDate.Year, _finARRateHd.TransDate.Month, AppModule.GetValue(TransactionType.ARRate), this._companyTag, ""))
                        {
                            _finARRateHd.FileNmbr = item.Number;
                        }


                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ARRate);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINARRateHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

                        _result = "Approve Success";

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
                FINARRateHd _finARRateHd = this.db.FINARRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finARRateHd.TransDate);

                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    int _success = this.db.S_FNCIRatePost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ARRate);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINARRateHd(_prmTransNmbr).FileNmbr;
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
                FINARRateHd _finARRateHd = this.db.FINARRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finARRateHd.TransDate);

                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNCIRateUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid()();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.ARRate);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINARRateHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINARRateHd(_prmTransNmbr).TransDate;
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
                                from _finARRateHd in this.db.FINARRateHds
                                where _finARRateHd.TransNmbr == _prmCode
                                select new
                                {
                                    CurrCode = _finARRateHd.CurrCode
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

        public decimal GetNewRateHeader(string _prmCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _finARRateHd in this.db.FINARRateHds
                                where _finARRateHd.TransNmbr == _prmCode
                                select new
                                {
                                    NewRate = _finARRateHd.NewRate
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.NewRate;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleFINARRateHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINARRateHd _finARRateHd = this.db.FINARRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finARRateHd != null)
                    {
                        if (_finARRateHd.Status != ARRateDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINARRateHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINARRateHd _finARRateHd = this.db.FINARRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finARRateHd.Status == ARRateDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finARRateHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finARRateHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finARRateHd != null)
                    {
                        if ((_finARRateHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINARRateDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINARRateDts.DeleteAllOnSubmit(_query);

                            this.db.FINARRateHds.DeleteOnSubmit(_finARRateHd);

                            _result = true;
                        }
                        else if (_finARRateHd.FileNmbr != "" && _finARRateHd.Status == ARRateDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finARRateHd.Status = ARRateDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINARRateDt
        public int RowsCountFINARRateDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINARRateDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINARRateDt> GetListFINARRateDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<FINARRateDt> _result = new List<FINARRateDt>();

            try
            {
                var _query = (
                                from _finARRateDt in this.db.FINARRateDts
                                where _finARRateDt.TransNmbr == _prmCode
                                orderby _finARRateDt.InvoiceNo ascending
                                select new
                                {
                                    TransNmbr = _finARRateDt.TransNmbr,
                                    InvoiceNo = _finARRateDt.InvoiceNo,
                                    CustCode = _finARRateDt.CustCode,
                                    CustName = (
                                                    from _msCust in this.db.MsCustomers
                                                    where _msCust.CustCode == _finARRateDt.CustCode
                                                    select _msCust.CustName
                                                ).FirstOrDefault(),
                                    ForexRate = _finARRateDt.ForexRate,
                                    AmountForex = _finARRateDt.AmountForex,
                                    AmountHome = _finARRateDt.AmountHome,
                                    NewAmountHome = _finARRateDt.NewAmountHome,
                                    IsApplyToPPN = _finARRateDt.IsApplyToPPN
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINARRateDt(_row.TransNmbr, _row.InvoiceNo, _row.CustCode, _row.CustName, _row.ForexRate, _row.AmountForex, _row.AmountHome, _row.NewAmountHome, _row.IsApplyToPPN));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINARRateDt GetSingleFINARRateDt(string _prmCode, string _prmInvoiceNo)
        {
            FINARRateDt _result = null;

            try
            {
                _result = this.db.FINARRateDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.InvoiceNo == _prmInvoiceNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINARRateDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINARRateDt _finARRateDt = this.db.FINARRateDts.Single(_temp => _temp.InvoiceNo == _prmCode[i] && _temp.TransNmbr == _prmTransNo);

                    this.db.FINARRateDts.DeleteOnSubmit(_finARRateDt);
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

        public bool AddFINARRateDt(FINARRateDt _prmFINARRateDt)
        {
            bool _result = false;

            try
            {
                this.db.FINARRateDts.InsertOnSubmit(_prmFINARRateDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINARRateDt(FINARRateDt _prmFINARRateDt)
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

        ~FINARRateBL()
        {
        }
    }
}
