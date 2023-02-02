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
    public sealed class FINAPRateBL : Base
    {
        public FINAPRateBL()
        {
        }

        #region FINAPRateHd
        public double RowsCountFINAPRateHd(string _prmCategory, string _prmKeyword)
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
                            from _finAPRateHd in this.db.FINAPRateHds
                            where (SqlMethods.Like(_finAPRateHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            && (SqlMethods.Like((_finAPRateHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                            && _finAPRateHd.Status != APRateDataMapper.GetStatus(TransStatus.Deleted)    
                            select _finAPRateHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINAPRateHd> GetListFINAPRateHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINAPRateHd> _result = new List<FINAPRateHd>();

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
                                from _finAPRateHd in this.db.FINAPRateHds
                                where (SqlMethods.Like(_finAPRateHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_finAPRateHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _finAPRateHd.Status != APRateDataMapper.GetStatus(TransStatus.Deleted)    
                                orderby _finAPRateHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finAPRateHd.TransNmbr,
                                    FileNmbr = _finAPRateHd.FileNmbr,
                                    TransDate = _finAPRateHd.TransDate,
                                    CurrCode = _finAPRateHd.CurrCode,
                                    Status = _finAPRateHd.Status,
                                    NewRate = _finAPRateHd.NewRate,
                                    Remark = _finAPRateHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINAPRateHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.NewRate, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINAPRateHd GetSingleFINAPRateHd(string _prmCode)
        {
            FINAPRateHd _result = null;

            try
            {
                _result = this.db.FINAPRateHds.Single(_temp => _temp.TransNmbr == _prmCode);
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
                                from _finAPRateHd in this.db.FINAPRateHds
                                where _finAPRateHd.TransNmbr == _prmCode
                                select new
                                {
                                    NewRate = _finAPRateHd.NewRate
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

        public bool DeleteMultiFINAPRateHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINAPRateHd _finAPRateHd = this.db.FINAPRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finAPRateHd != null)
                    {
                        if ((_finAPRateHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINAPRateDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINAPRateDts.DeleteAllOnSubmit(_query);

                            this.db.FINAPRateHds.DeleteOnSubmit(_finAPRateHd);

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

        public string AddFINAPRateHd(FINAPRateHd _prmFINAPRateHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINAPRateHd.TransDate.Year, _prmFINAPRateHd.TransDate.Month, AppModule.GetValue(TransactionType.APRate), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINAPRateHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINAPRateHds.InsertOnSubmit(_prmFINAPRateHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINAPRateHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINAPRateHd(FINAPRateHd _prmFINAPRateHd)
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

        public string GetCurrCodeHeader(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finAPRateHd in this.db.FINAPRateHds
                                where _finAPRateHd.TransNmbr == _prmCode
                                select new
                                {
                                    CurrCode = _finAPRateHd.CurrCode
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

        public decimal GetNewForexRateHeader(string _prmCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _finAPRateHd in this.db.FINAPRateHds
                                where _finAPRateHd.TransNmbr == _prmCode
                                select new
                                {
                                    NewRate = _finAPRateHd.NewRate
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

        public List<FINAPRateHd> GetListAPRateForDDL(string _prmSuppCode)
        {
            List<FINAPRateHd> _result = new List<FINAPRateHd>();

            try
            {
                var _query = (
                                from _finAPRateHd in this.db.FINAPRateHds
                                //where (_finAPRateHd.SuppCode.Trim().ToLower() == _prmSuppCode.Trim().ToLower())
                                //    && (_finAPRateHd.Status == new DPSupplierReturDataMapper().GetStatus(DPSupplierReturStatus.Posted))
                                //    && (_finAPRateHd.BaseForex > _finAPRateHd.Balance || _finAPRateHd.PPNForex > _finAPRateHd.BalancePPn)
                                orderby _finAPRateHd.TransNmbr ascending
                                select new
                                {
                                    TransNmbr = _finAPRateHd.TransNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINAPRateHd(_row.TransNmbr));
                }
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
                int _success = this.db.S_FNSIRateGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.APRate);
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
                    int _success = this.db.S_FNSIRateApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINAPRateHd _finAPRateHd = this.GetSingleFINAPRateHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_finAPRateHd.TransDate.Year, _finAPRateHd.TransDate.Month, AppModule.GetValue(TransactionType.APRate), this._companyTag, ""))
                        {
                            _finAPRateHd.FileNmbr = _item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.APRate);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINAPRateHd(_prmTransNmbr).FileNmbr;
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
                FINAPRateHd _finAPRateHd = this.db.FINAPRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finAPRateHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSIRatePost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.APRate);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINAPRateHd(_prmTransNmbr).FileNmbr;
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
                FINAPRateHd _finAPRateHd = this.db.FINAPRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finAPRateHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSIRateUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid()();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.APRate);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINAPRateHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINAPRateHd(_prmTransNmbr).TransDate;
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

        public bool GetSingleFINAPRateHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINAPRateHd _finAPRateHd = this.db.FINAPRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finAPRateHd != null)
                    {
                        if (_finAPRateHd.Status != APRateDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINAPRateHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINAPRateHd _finAPRateHd = this.db.FINAPRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finAPRateHd.Status == APRateDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finAPRateHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finAPRateHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finAPRateHd != null)
                    {
                        if ((_finAPRateHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINAPRateDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINAPRateDts.DeleteAllOnSubmit(_query);

                            this.db.FINAPRateHds.DeleteOnSubmit(_finAPRateHd);

                            _result = true;
                        }
                        else if (_finAPRateHd.FileNmbr != "" && _finAPRateHd.Status == APRateDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finAPRateHd.Status = APRateDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINAPRateDt

        public int RowsCountFINAPRateDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINAPRateDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINAPRateDt> GetListFINAPRateDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<FINAPRateDt> _result = new List<FINAPRateDt>();

            try
            {
                var _query = (
                                from _finAPRateDt in this.db.FINAPRateDts
                                join _supp in this.db.MsSuppliers
                                    on _finAPRateDt.SuppCode equals _supp.SuppCode
                                where _finAPRateDt.TransNmbr == _prmCode
                                orderby _finAPRateDt.InvoiceNo ascending
                                select new
                                {
                                    InvoiceNo = _finAPRateDt.InvoiceNo,
                                    FileNoInvoice = (
                                                        from _finAPPosting in this.db.FINAPPostings
                                                        where _finAPPosting.InvoiceNo == _finAPRateDt.InvoiceNo
                                                        select _finAPPosting.FileNmbr
                                                     ).FirstOrDefault(),
                                    SuppCode = _finAPRateDt.SuppCode,
                                    SuppName = _supp.SuppName,
                                    ForexRate = _finAPRateDt.ForexRate,
                                    AmountForex = _finAPRateDt.AmountForex,
                                    AmountHome = _finAPRateDt.AmountHome,
                                    NewAmountHome = _finAPRateDt.NewAmountHome,
                                    IsApplyToPPN = _finAPRateDt.IsApplyToPPN
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINAPRateDt(_row.InvoiceNo, _row.FileNoInvoice, _row.SuppCode, _row.SuppName, _row.ForexRate, _row.AmountForex, _row.AmountHome, _row.NewAmountHome, _row.IsApplyToPPN));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINAPRateDt GetSingleFINAPRateDt(string _prmCode, string _prmInvoiceNo)
        {
            FINAPRateDt _result = null;

            try
            {
                _result = this.db.FINAPRateDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.InvoiceNo == _prmInvoiceNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINAPRateDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINAPRateDt _finAPRateDt = this.db.FINAPRateDts.Single(_temp => _temp.InvoiceNo.Trim().ToLower() == _prmCode[i].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.FINAPRateDts.DeleteOnSubmit(_finAPRateDt);
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

        public bool AddFINAPRateDt(FINAPRateDt _prmFINAPRateDt)
        {
            bool _result = false;

            try
            {
                this.db.FINAPRateDts.InsertOnSubmit(_prmFINAPRateDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINAPRateDt(FINAPRateDt _prmFINAPRateDt)
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

        ~FINAPRateBL()
        {
        }
    }
}
