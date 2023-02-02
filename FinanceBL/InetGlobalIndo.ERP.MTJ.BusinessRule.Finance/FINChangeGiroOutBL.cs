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
    public sealed class FINChangeGiroOutBL : Base
    {
        public FINChangeGiroOutBL()
        {

        }

        #region FINChangeGiroOutHd
        public double RowsCountFINChangeGiroOutHd(string _prmCategory, string _prmKeyword)
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
                            from _finChangeGiroOutHd in this.db.FINChangeGiroOutHds
                            where (SqlMethods.Like(_finChangeGiroOutHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_finChangeGiroOutHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _finChangeGiroOutHd.Status != GiroPaymentChangeDataMapper.GetStatus(TransStatus.Deleted)
                            select _finChangeGiroOutHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINChangeGiroOutHd> GetListFINChangeGiroOutHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINChangeGiroOutHd> _result = new List<FINChangeGiroOutHd>();

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
                                from _finChangeGiroOutHd in this.db.FINChangeGiroOutHds
                                where (SqlMethods.Like(_finChangeGiroOutHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_finChangeGiroOutHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _finChangeGiroOutHd.Status != GiroPaymentChangeDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finChangeGiroOutHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finChangeGiroOutHd.TransNmbr,
                                    FileNmbr = _finChangeGiroOutHd.FileNmbr,
                                    TransDate = _finChangeGiroOutHd.TransDate,
                                    SuppCode = _finChangeGiroOutHd.SuppCode,
                                    CustCode = _finChangeGiroOutHd.CustCode,
                                    Status = _finChangeGiroOutHd.Status
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINChangeGiroOutHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.SuppCode, _row.CustCode, _row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINChangeGiroOutHd GetSingleFINChangeGiroOutHd(string _prmCode)
        {
            FINChangeGiroOutHd _result = null;

            try
            {
                _result = this.db.FINChangeGiroOutHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINChangeGiroOutHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINChangeGiroOutHd _finChangeGiroOutHd = this.db.FINChangeGiroOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finChangeGiroOutHd != null)
                    {
                        if ((_finChangeGiroOutHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINChangeGiroOutDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINChangeGiroOutDts.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINChangeGiroOutPays
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINChangeGiroOutPays.DeleteAllOnSubmit(_query2);

                            this.db.FINChangeGiroOutHds.DeleteOnSubmit(_finChangeGiroOutHd);

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

        public string AddFINChangeGiroOutHd(FINChangeGiroOutHd _prmFINChangeGiroOutHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINChangeGiroOutHd.TransDate.Year, _prmFINChangeGiroOutHd.TransDate.Month, AppModule.GetValue(TransactionType.ChangeGiroPayment), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINChangeGiroOutHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINChangeGiroOutHds.InsertOnSubmit(_prmFINChangeGiroOutHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINChangeGiroOutHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINChangeGiroOutHd(FINChangeGiroOutHd _prmFINChangeGiroOutHd)
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

        public string GetApproval(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_FNChangeGiroOutGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ChangeGiroPayment);
                    _transActivity.TransNmbr = _prmCode.ToString();
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

        public string Approve(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_FNChangeGiroOutApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINChangeGiroOutHd _finChangeGiroOutHd = this.GetSingleFINChangeGiroOutHd(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finChangeGiroOutHd.TransDate.Year, _finChangeGiroOutHd.TransDate.Month, AppModule.GetValue(TransactionType.ChangeGiroPayment), this._companyTag, ""))
                        {
                            _finChangeGiroOutHd.FileNmbr = item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ChangeGiroPayment);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleFINChangeGiroOutHd(_prmCode).FileNmbr;
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

        public string Posting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINChangeGiroOutHd _finChangeGiroOutHd = this.db.FINChangeGiroOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finChangeGiroOutHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNChangeGiroOutPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ChangeGiroPayment);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleFINChangeGiroOutHd(_prmCode).FileNmbr;
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

        public string UnPosting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINChangeGiroOutHd _finChangeGiroOutHd = this.db.FINChangeGiroOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finChangeGiroOutHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNChangeGiroOutUnPost(_prmCode, 0, 0, _prmuser, ref _result);

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

        public bool GetSingleFINChangeGiroOutHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINChangeGiroOutHd _finChangeGiroOutHd = this.db.FINChangeGiroOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finChangeGiroOutHd != null)
                    {
                        if (_finChangeGiroOutHd.Status != GiroPaymentChangeDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINChangeGiroOutHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINChangeGiroOutHd _finChangeGiroOutHd = this.db.FINChangeGiroOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finChangeGiroOutHd.Status == GiroPaymentChangeDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finChangeGiroOutHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finChangeGiroOutHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finChangeGiroOutHd != null)
                    {
                        if ((_finChangeGiroOutHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINChangeGiroOutDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINChangeGiroOutDts.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINChangeGiroOutPays
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINChangeGiroOutPays.DeleteAllOnSubmit(_query2);

                            this.db.FINChangeGiroOutHds.DeleteOnSubmit(_finChangeGiroOutHd);

                            _result = true;
                        }
                        else if (_finChangeGiroOutHd.FileNmbr != "" && _finChangeGiroOutHd.Status == GiroPaymentChangeDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finChangeGiroOutHd.Status = GiroPaymentChangeDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINChangeGiroOutDt
        public int RowsCountFINChangeGiroOutDt
        {
            get
            {
                return this.db.FINChangeGiroOutDts.Count();
            }
        }

        public List<FINChangeGiroOutDt> GetListFINChangeGiroOutDt(string _prmCode)
        {
            List<FINChangeGiroOutDt> _result = new List<FINChangeGiroOutDt>();

            try
            {
                var _query = (
                                from _finChangeGiroOutDt in this.db.FINChangeGiroOutDts
                                where _finChangeGiroOutDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _finChangeGiroOutDt.PaymentDate descending
                                select new
                                {
                                    TransNmbr = _finChangeGiroOutDt.TransNmbr,
                                    OldGiro = _finChangeGiroOutDt.OldGiro,
                                    PaymentDate = _finChangeGiroOutDt.PaymentDate,
                                    DueDate = _finChangeGiroOutDt.DueDate,
                                    BankPayment = _finChangeGiroOutDt.BankPayment,
                                    CurrCode = _finChangeGiroOutDt.CurrCode,
                                    ForexRate = _finChangeGiroOutDt.ForexRate,
                                    AmountForex = _finChangeGiroOutDt.AmountForex
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINChangeGiroOutDt(_row.TransNmbr, _row.OldGiro, _row.PaymentDate, _row.DueDate, _row.BankPayment, _row.CurrCode, _row.ForexRate, _row.AmountForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINChangeGiroOutDt GetSingleFINChangeGiroOutDt(string _prmCode, string _prmOldGiro)
        {
            FINChangeGiroOutDt _result = null;

            try
            {
                _result = this.db.FINChangeGiroOutDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.OldGiro.Trim().ToLower() == _prmOldGiro.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINChangeGiroOutDt(FINChangeGiroOutDt _prmFINChangeGiroOutDt)
        {
            bool _result = false;

            try
            {
                this.db.FINChangeGiroOutDts.InsertOnSubmit(_prmFINChangeGiroOutDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINChangeGiroOutDt(FINChangeGiroOutDt _prmFINChangeGiroOutDt)
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

        public bool DeleteMultiFINChangeGiroOutDt(string[] _prmItemCode, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItemCode.Length; i++)
                {
                    FINChangeGiroOutDt _FINChangeGiroOutDt = this.db.FINChangeGiroOutDts.Single(_temp => _temp.OldGiro.Trim().ToLower() == _prmItemCode[i].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.FINChangeGiroOutDts.DeleteOnSubmit(_FINChangeGiroOutDt);
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
        #endregion

        #region FINChangeGiroOutPay

        public int RowsCountFINChangeGiroOutPay
        {
            get
            {
                return this.db.FINChangeGiroOutPays.Count();
            }
        }

        public List<FINChangeGiroOutPay> GetListFINChangeGiroOutPay(string _prmCode)
        {
            List<FINChangeGiroOutPay> _result = new List<FINChangeGiroOutPay>();

            try
            {
                var _query = (
                                from _finChangeGiroOutPay in this.db.FINChangeGiroOutPays
                                where _finChangeGiroOutPay.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _finChangeGiroOutPay.ItemNo descending
                                select new
                                {
                                    TransNmbr = _finChangeGiroOutPay.TransNmbr,
                                    ItemNo = _finChangeGiroOutPay.ItemNo,
                                    PayType = _finChangeGiroOutPay.PayType,
                                    DocumentNo = _finChangeGiroOutPay.DocumentNo,
                                    CurrCode = _finChangeGiroOutPay.CurrCode,
                                    ForexRate = _finChangeGiroOutPay.ForexRate,
                                    AmountForex = _finChangeGiroOutPay.AmountForex,
                                    Remark = _finChangeGiroOutPay.Remark,
                                    BankPayment = _finChangeGiroOutPay.BankPayment,
                                    DueDate = _finChangeGiroOutPay.DueDate,
                                    BankExpense = _finChangeGiroOutPay.BankExpense
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINChangeGiroOutPay(_row.TransNmbr, _row.ItemNo, _row.PayType, _row.DocumentNo, _row.CurrCode, _row.ForexRate, _row.AmountForex, _row.Remark, _row.BankPayment, _row.DueDate, _row.BankExpense));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxNoItemChangeGiroOutPay(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINChangeGiroOutPays.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINChangeGiroOutPay(FINChangeGiroOutPay _prmFINChangeGiroOutPay)
        {
            bool _result = false;

            try
            {
                this.db.FINChangeGiroOutPays.InsertOnSubmit(_prmFINChangeGiroOutPay);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINChangeGiroOutPay(FINChangeGiroOutPay _prmFINChangeGiroOutPay)
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

        public FINChangeGiroOutPay GetSingleFINChangeGiroOutPay(string _prmCode, string _prmItemNo)
        {
            FINChangeGiroOutPay _result = null;

            try
            {
                _result = this.db.FINChangeGiroOutPays.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.ItemNo == Convert.ToInt32(_prmItemNo));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINChangeGiroOutPay(string[] _prmItemCode, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItemCode.Length; i++)
                {
                    FINChangeGiroOutPay _FINChangeGiroOutPay = this.db.FINChangeGiroOutPays.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmItemCode[i]) && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.FINChangeGiroOutPays.DeleteOnSubmit(_FINChangeGiroOutPay);
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

        #endregion

        ~FINChangeGiroOutBL()
        {

        }
    }
}
