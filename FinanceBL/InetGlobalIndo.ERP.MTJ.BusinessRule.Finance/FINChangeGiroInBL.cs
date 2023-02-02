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
    public sealed class FINChangeGiroInBL : Base
    {
        public FINChangeGiroInBL()
        {

        }

        #region FINChangeGiroInHd
        public double RowsCountFINChangeGiroInHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _finChangeGiroInHd in this.db.FINChangeGiroInHds
                            where (SqlMethods.Like(_finChangeGiroInHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            && (SqlMethods.Like((_finChangeGiroInHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                            && _finChangeGiroInHd.Status != GiroReceiptChangeDataMapper.GetStatus(TransStatus.Deleted)
                            select _finChangeGiroInHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINChangeGiroInHd> GetListFINChangeGiroInHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINChangeGiroInHd> _result = new List<FINChangeGiroInHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _finChangeGiroInHd in this.db.FINChangeGiroInHds
                                where (SqlMethods.Like(_finChangeGiroInHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_finChangeGiroInHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _finChangeGiroInHd.Status != GiroReceiptChangeDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finChangeGiroInHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finChangeGiroInHd.TransNmbr,
                                    FileNmbr = _finChangeGiroInHd.FileNmbr,
                                    TransDate = _finChangeGiroInHd.TransDate,
                                    SuppCode = _finChangeGiroInHd.SuppCode,
                                    CustCode = _finChangeGiroInHd.CustCode,
                                    Status = _finChangeGiroInHd.Status
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINChangeGiroInHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.SuppCode, _row.CustCode, _row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINChangeGiroInHd GetSingleFINChangeGiroInHd(string _prmCode)
        {
            FINChangeGiroInHd _result = null;

            try
            {
                _result = this.db.FINChangeGiroInHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINChangeGiroInHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINChangeGiroInHd _finChangeGiroInHd = this.db.FINChangeGiroInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finChangeGiroInHd != null)
                    {
                        if ((_finChangeGiroInHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINChangeGiroInDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINChangeGiroInDts.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINChangeGiroInPays
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINChangeGiroInPays.DeleteAllOnSubmit(_query2);

                            this.db.FINChangeGiroInHds.DeleteOnSubmit(_finChangeGiroInHd);

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

        public string AddFINChangeGiroInHd(FINChangeGiroInHd _prmFINChangeGiroInHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINChangeGiroInHd.TransDate.Year, _prmFINChangeGiroInHd.TransDate.Month, AppModule.GetValue(TransactionType.ChangeGiroReceipt), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINChangeGiroInHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINChangeGiroInHds.InsertOnSubmit(_prmFINChangeGiroInHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINChangeGiroInHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINChangeGiroInHd(FINChangeGiroInHd _prmFINChangeGiroInHd)
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
                this.db.S_FNChangeGiroInGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ChangeGiroReceipt);
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
                    this.db.S_FNChangeGiroInAppove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINChangeGiroInHd _finChangeGiroInHd = this.GetSingleFINChangeGiroInHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finChangeGiroInHd.TransDate.Year, _finChangeGiroInHd.TransDate.Month, AppModule.GetValue(TransactionType.ChangeGiroReceipt), this._companyTag, ""))
                        {
                            _finChangeGiroInHd.FileNmbr = item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ChangeGiroReceipt);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleFINChangeGiroInHd(_prmTransNmbr).FileNmbr;
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
                FINChangeGiroInHd _finChangeGiroInHd = this.db.FINChangeGiroInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finChangeGiroInHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNChangeGiroInPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ChangeGiroReceipt);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleFINChangeGiroInHd(_prmTransNmbr).FileNmbr;
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
                FINChangeGiroInHd _finChangeGiroInHd = this.db.FINChangeGiroInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finChangeGiroInHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNChangeGiroInUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

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
                _result = "Unposting Failed ";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public bool GetSingleFINChangeGiroInHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINChangeGiroInHd _finChangeGiroInHd = this.db.FINChangeGiroInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finChangeGiroInHd != null)
                    {
                        if (_finChangeGiroInHd.Status != GiroReceiptChangeDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINChangeGiroInHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINChangeGiroInHd _finChangeGiroInHd = this.db.FINChangeGiroInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());
                    if (_finChangeGiroInHd.Status == GiroReceiptChangeDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finChangeGiroInHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finChangeGiroInHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finChangeGiroInHd != null)
                    {
                        if ((_finChangeGiroInHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINChangeGiroInDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINChangeGiroInDts.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINChangeGiroInPays
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINChangeGiroInPays.DeleteAllOnSubmit(_query2);

                            this.db.FINChangeGiroInHds.DeleteOnSubmit(_finChangeGiroInHd);

                            _result = true;
                        }
                        else if (_finChangeGiroInHd.FileNmbr != "" && _finChangeGiroInHd.Status == GiroReceiptChangeDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finChangeGiroInHd.Status = GiroReceiptChangeDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINChangeGiroInDt
        public int RowsCountFINChangeGiroInDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINChangeGiroInDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINChangeGiroInDt> GetListFINChangeGiroInDt(int _prmReqPage, int _prmPageSize, string _prmTransNo)
        {
            List<FINChangeGiroInDt> _result = new List<FINChangeGiroInDt>();

            try
            {
                var _query = (
                                from _finChangeGiroInDt in this.db.FINChangeGiroInDts
                                where _finChangeGiroInDt.TransNmbr == _prmTransNo
                                orderby _finChangeGiroInDt.TransNmbr descending
                                select new
                                {
                                    TransNmbr = _finChangeGiroInDt.TransNmbr,
                                    OldGiro = _finChangeGiroInDt.OldGiro,
                                    ReceiptDate = _finChangeGiroInDt.ReceiptDate,
                                    DueDate = _finChangeGiroInDt.DueDate,
                                    BankGiro = _finChangeGiroInDt.BankGiro,
                                    AmountForex = _finChangeGiroInDt.AmountForex
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINChangeGiroInDt(_row.TransNmbr, _row.OldGiro, _row.ReceiptDate, _row.DueDate, _row.BankGiro, _row.AmountForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINChangeGiroInDt GetSingleFINChangeGiroInDt(string _prmTransNmbr, string _prmOldGiro)
        {
            FINChangeGiroInDt _result = null;

            try
            {
                _result = this.db.FINChangeGiroInDts.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.OldGiro == _prmOldGiro);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINChangeGiroInDt(string[] _prmOldGiro, string _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmOldGiro.Length; i++)
                {
                    FINChangeGiroInDt _FINChangeGiroInDt = this.db.FINChangeGiroInDts.Single(_temp => _temp.OldGiro.Trim().ToLower() == _prmOldGiro[i].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());

                    this.db.FINChangeGiroInDts.DeleteOnSubmit(_FINChangeGiroInDt);
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

        public bool AddFINChangeGiroInDt(FINChangeGiroInDt _prmFINChangeGiroInDt)
        {
            bool _result = false;

            try
            {
                this.db.FINChangeGiroInDts.InsertOnSubmit(_prmFINChangeGiroInDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINChangeGiroInDt(FINChangeGiroInDt _prmFINChangeGiroInDt)
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

        #region FINChangeGiroInPay
        public int RowsCountFINChangeGiroInPay(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINChangeGiroInPays.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINChangeGiroInPay> GetListFINChangeGiroInPay(int _prmReqPage, int _prmPageSize, string _prmTransNo)
        {
            List<FINChangeGiroInPay> _result = new List<FINChangeGiroInPay>();

            try
            {
                var _query = (
                                from _finChangeGiroInPay in this.db.FINChangeGiroInPays
                                where _finChangeGiroInPay.TransNmbr == _prmTransNo
                                orderby _finChangeGiroInPay.TransNmbr descending
                                select new
                                {
                                    TransNmbr = _finChangeGiroInPay.TransNmbr,
                                    ItemNo = _finChangeGiroInPay.ItemNo,
                                    ReceiptType = _finChangeGiroInPay.ReceiptType,
                                    DueDate = _finChangeGiroInPay.DueDate,
                                    BankGiro = _finChangeGiroInPay.BankGiro,
                                    AmountForex = _finChangeGiroInPay.AmountForex
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINChangeGiroInPay(_row.TransNmbr, _row.ItemNo, _row.ReceiptType, _row.DueDate, _row.BankGiro, _row.AmountForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINChangeGiroInPay GetSingleFINChangeGiroInPay(string _prmTransNmbr, int _prmItemNo)
        {
            FINChangeGiroInPay _result = null;

            try
            {
                _result = this.db.FINChangeGiroInPays.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINChangeGiroInPay(string[] _prmItemNo, string _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItemNo.Length; i++)
                {
                    FINChangeGiroInPay _FINChangeGiroInPay = this.db.FINChangeGiroInPays.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmItemNo[i]) && _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());

                    this.db.FINChangeGiroInPays.DeleteOnSubmit(_FINChangeGiroInPay);
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

        public bool AddFINChangeGiroInPay(FINChangeGiroInPay _prmFINChangeGiroInPay)
        {
            bool _result = false;

            try
            {
                this.db.FINChangeGiroInPays.InsertOnSubmit(_prmFINChangeGiroInPay);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINChangeGiroInPay(FINChangeGiroInPay _prmFINChangeGiroInPay)
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

        public int GetMaxNoItemFINChangeGiroInPay(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINChangeGiroInPays.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~FINChangeGiroInBL()
        {

        }
    }
}
