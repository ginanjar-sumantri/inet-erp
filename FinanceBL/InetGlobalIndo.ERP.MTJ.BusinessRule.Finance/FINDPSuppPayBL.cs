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
    public sealed class FINDPSuppPayBL : Base
    {
        public FINDPSuppPayBL()
        {

        }

        #region FINDPSuppHd
        public double RowsCountFINDPSuppHd(string _prmCategory, string _prmKeyword)
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
                            from _finDPSuppHd in this.db.FINDPSuppHds
                            join _msSupp in this.db.MsSuppliers
                                on _finDPSuppHd.SuppCode equals _msSupp.SuppCode
                            where (SqlMethods.Like(_finDPSuppHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_finDPSuppHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _finDPSuppHd.Status != DPSuppPayDataMapper.GetStatus(TransStatus.Deleted)
                            select _finDPSuppHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINDPSuppHd> GetListFINDPSuppHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINDPSuppHd> _result = new List<FINDPSuppHd>();

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
                                from _finDPSuppHd in this.db.FINDPSuppHds
                                join _msSupp in this.db.MsSuppliers
                                    on _finDPSuppHd.SuppCode equals _msSupp.SuppCode
                                where (SqlMethods.Like(_finDPSuppHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finDPSuppHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finDPSuppHd.Status != DPSuppPayDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finDPSuppHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finDPSuppHd.TransNmbr,
                                    FileNmbr = _finDPSuppHd.FileNmbr,
                                    TransDate = _finDPSuppHd.TransDate,
                                    CurrCode = _finDPSuppHd.CurrCode,
                                    Status = _finDPSuppHd.Status,
                                    SuppCode = _finDPSuppHd.SuppCode,
                                    SuppName = _msSupp.SuppName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPSuppHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.SuppCode, _row.SuppName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDPSuppHd GetSingleFINDPSuppHd(string _prmCode)
        {
            FINDPSuppHd _result = null;

            try
            {
                _result = this.db.FINDPSuppHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINDPSuppHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPSuppHd _finDPSuppHd = this.db.FINDPSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPSuppHd != null)
                    {
                        if ((_finDPSuppHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINDPSuppDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINDPSuppDts.DeleteAllOnSubmit(_query);

                            this.db.FINDPSuppHds.DeleteOnSubmit(_finDPSuppHd);

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

        public string AddFINDPSuppHd(FINDPSuppHd _prmFINDPSuppHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINDPSuppHd.TransDate.Year, _prmFINDPSuppHd.TransDate.Month, AppModule.GetValue(TransactionType.DPSuppPay), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINDPSuppHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINDPSuppHds.InsertOnSubmit(_prmFINDPSuppHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINDPSuppHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDPSuppHd(FINDPSuppHd _prmFINDPSuppHd)
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
                int _success = this.db.S_FNDPSuppPayGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.DPSuppPay);
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
                    int _success = this.db.S_FNDPSuppPayApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINDPSuppHd _finDPSuppHd = this.GetSingleFINDPSuppHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_finDPSuppHd.TransDate.Year, _finDPSuppHd.TransDate.Month, AppModule.GetValue(TransactionType.DPSuppPay), this._companyTag, ""))
                        {
                            _finDPSuppHd.FileNmbr = _item.Number;
                        }
                 
                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.DPSuppPay);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINDPSuppHd(_prmTransNmbr).FileNmbr;
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
                FINDPSuppHd _finDPSuppHd = this.db.FINDPSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDPSuppHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNDPSuppPayPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.DPSuppPay);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINDPSuppHd(_prmTransNmbr).FileNmbr;
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
                FINDPSuppHd _finDPSuppHd = this.db.FINDPSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDPSuppHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNDPSuppPayUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.DPSuppPay);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINDPSuppHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINDPSuppHd(_prmTransNmbr).TransDate;
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
                                from _finDPSuppHd in this.db.FINDPSuppHds
                                where _finDPSuppHd.TransNmbr == _prmCode
                                select new
                                {
                                    CurrCode = _finDPSuppHd.CurrCode
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

        public List<FINDPSuppHd> GetListDPForDDL(string _prmSuppCode)
        {
            List<FINDPSuppHd> _result = new List<FINDPSuppHd>();

            try
            {
                var _query = (
                                from _finDPSuppHd in this.db.FINDPSuppHds
                                where (_finDPSuppHd.SuppCode.Trim().ToLower() == _prmSuppCode.Trim().ToLower())
                                    && (_finDPSuppHd.Status == DPSuppPayDataMapper.GetStatus(TransStatus.Posted))
                                    && (_finDPSuppHd.BaseForex > _finDPSuppHd.Balance || _finDPSuppHd.PPNForex > _finDPSuppHd.BalancePPn)
                                    && ((_finDPSuppHd.FileNmbr ?? "").Trim().ToLower() == _finDPSuppHd.FileNmbr.Trim().ToLower())
                                orderby _finDPSuppHd.TransNmbr ascending
                                select new
                                {
                                    TransNmbr = _finDPSuppHd.TransNmbr,
                                    FileNmbr = _finDPSuppHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPSuppHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINDPSuppHd> GetListDPForDDL(string _prmSuppCode, String _prmCurrCode)
        {
            List<FINDPSuppHd> _result = new List<FINDPSuppHd>();

            try
            {
                var _query = (
                                from _finDPSuppHd in this.db.FINDPSuppHds
                                where (_finDPSuppHd.SuppCode.Trim().ToLower() == _prmSuppCode.Trim().ToLower())
                                    && (_finDPSuppHd.Status == DPSuppPayDataMapper.GetStatus(TransStatus.Posted))
                                    && (_finDPSuppHd.BaseForex > _finDPSuppHd.Balance || _finDPSuppHd.PPNForex > _finDPSuppHd.BalancePPn)
                                    && ((_finDPSuppHd.FileNmbr ?? "").Trim().ToLower() == _finDPSuppHd.FileNmbr.Trim().ToLower())
                                    && _finDPSuppHd.CurrCode == _prmCurrCode
                                orderby _finDPSuppHd.TransNmbr ascending
                                select new
                                {
                                    TransNmbr = _finDPSuppHd.TransNmbr,
                                    FileNmbr = _finDPSuppHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPSuppHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public char GetStatusFINDPSuppHd(string _prmCode)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _finDPSuppHd in this.db.FINDPSuppHds
                                where _finDPSuppHd.TransNmbr == _prmCode
                                select new
                                {
                                    Status = _finDPSuppHd.Status
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

        public bool GetSingleFINDPSuppHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPSuppHd _finDPSuppHd = this.db.FINDPSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPSuppHd != null)
                    {
                        if (_finDPSuppHd.Status != DPSuppPayDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINDPSuppHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPSuppHd _finDPSuppHd = this.db.FINDPSuppHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPSuppHd.Status == DPSuppPayDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finDPSuppHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finDPSuppHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finDPSuppHd != null)
                    {
                        if ((_finDPSuppHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINDPSuppDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINDPSuppDts.DeleteAllOnSubmit(_query);

                            this.db.FINDPSuppHds.DeleteOnSubmit(_finDPSuppHd);

                            _result = true;
                        }
                        else if (_finDPSuppHd.FileNmbr != "" && _finDPSuppHd.Status == DPSuppPayDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finDPSuppHd.Status = DPSuppPayDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINDPSuppDt
        public int RowsCountFINDPSuppDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINDPSuppDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINDPSuppDt> GetListFINDPSuppDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<FINDPSuppDt> _result = new List<FINDPSuppDt>();

            try
            {
                var _query = (
                                from _finDPSuppDt in this.db.FINDPSuppDts
                                where _finDPSuppDt.TransNmbr == _prmCode
                                orderby _finDPSuppDt.ItemNo ascending
                                select new
                                {
                                    ItemNo = _finDPSuppDt.ItemNo,
                                    PayType = _finDPSuppDt.PayType,
                                    DueDate = _finDPSuppDt.DueDate,
                                    BankPaymentName = (
                                                        from _msPayType in this.db.MsPayTypes
                                                        where _msPayType.PayCode == _finDPSuppDt.BankPayment
                                                        select _msPayType.PayName
                                                   ).FirstOrDefault(),
                                    AmountForex = _finDPSuppDt.AmountForex
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPSuppDt(_row.ItemNo, _row.PayType, _row.DueDate, _row.BankPaymentName, _row.AmountForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDPSuppDt GetSingleFINDPSuppDt(string _prmCode, int _prmItemNo)
        {
            FINDPSuppDt _result = null;

            try
            {
                _result = this.db.FINDPSuppDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINDPSuppDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            FINDPSuppHd _finDPSuppHd = new FINDPSuppHd();

            decimal _total = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPSuppDt _finDPSuppDt = this.db.FINDPSuppDts.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmCode[i]) && _temp.TransNmbr == _prmTransNo);

                    this.db.FINDPSuppDts.DeleteOnSubmit(_finDPSuppDt);
                }

                var _query = (
                                from _finDPSuppDt2 in this.db.FINDPSuppDts
                                where !(
                                            from _code in _prmCode
                                            select _code
                                        ).Contains(_finDPSuppDt2.ItemNo.ToString())
                                        && _finDPSuppDt2.TransNmbr == _prmTransNo
                                group _finDPSuppDt2 by _finDPSuppDt2.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finDPSuppHd = this.db.FINDPSuppHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _finDPSuppHd.TotalForex = _total;
                _finDPSuppHd.BaseForex = (_finDPSuppHd.TotalForex * 100) / (100 + _finDPSuppHd.PPN);
                _finDPSuppHd.PPNForex = (_finDPSuppHd.BaseForex * _finDPSuppHd.PPN) / 100;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINDPSuppDt(FINDPSuppDt _prmFINDPSuppDt)
        {
            bool _result = false;

            FINDPSuppHd _finDPSuppHd = new FINDPSuppHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finDPSuppDt in this.db.FINDPSuppDts
                               where !(
                                           from _finDPSuppDt2 in this.db.FINDPSuppDts
                                           where _finDPSuppDt2.ItemNo == _prmFINDPSuppDt.ItemNo && _finDPSuppDt2.TransNmbr == _prmFINDPSuppDt.TransNmbr
                                           select _finDPSuppDt2.ItemNo
                                       ).Contains(_finDPSuppDt.ItemNo)
                                       && _finDPSuppDt.TransNmbr == _prmFINDPSuppDt.TransNmbr
                               group _finDPSuppDt by _finDPSuppDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finDPSuppHd = this.db.FINDPSuppHds.Single(_fa => _fa.TransNmbr == _prmFINDPSuppDt.TransNmbr);

                _finDPSuppHd.TotalForex = _total + _prmFINDPSuppDt.AmountForex;
                _finDPSuppHd.BaseForex = ((_finDPSuppHd.TotalForex * 100) / (100 + _finDPSuppHd.PPN));
                _finDPSuppHd.PPNForex = (_finDPSuppHd.BaseForex * _finDPSuppHd.PPN) / 100;
                this.db.FINDPSuppDts.InsertOnSubmit(_prmFINDPSuppDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDPSuppDt(FINDPSuppDt _prmFINDPSuppDt)
        {
            bool _result = false;

            FINDPSuppHd _finDPSuppHd = new FINDPSuppHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finDPSuppDt in this.db.FINDPSuppDts
                               where !(
                                           from _finDPSuppDt2 in this.db.FINDPSuppDts
                                           where _finDPSuppDt2.ItemNo == _prmFINDPSuppDt.ItemNo && _finDPSuppDt2.TransNmbr == _prmFINDPSuppDt.TransNmbr
                                           select _finDPSuppDt2.ItemNo
                                       ).Contains(_finDPSuppDt.ItemNo)
                                       && _finDPSuppDt.TransNmbr == _prmFINDPSuppDt.TransNmbr
                               group _finDPSuppDt by _finDPSuppDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finDPSuppHd = this.db.FINDPSuppHds.Single(_fa => _fa.TransNmbr == _prmFINDPSuppDt.TransNmbr);

                _finDPSuppHd.TotalForex = _total + _prmFINDPSuppDt.AmountForex;
                _finDPSuppHd.BaseForex = (_finDPSuppHd.TotalForex * 100) / (100 + _finDPSuppHd.PPN);
                _finDPSuppHd.PPNForex = (_finDPSuppHd.BaseForex * _finDPSuppHd.PPN) / 100;
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxNoItemFINDPSuppDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINDPSuppDts.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~FINDPSuppPayBL()
        {
        }
    }
}
