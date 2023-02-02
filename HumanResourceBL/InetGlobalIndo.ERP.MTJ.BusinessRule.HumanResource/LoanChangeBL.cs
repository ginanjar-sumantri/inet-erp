using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.IO;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class LoanChangeBL : Base
    {
        public LoanChangeBL()
        {

        }

        #region HRMTrLoanChangeHd


        public double RowsCountLoanChange(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query =
                            (
                               from _loanChange in this.db.HRMTrLoanChangeHds
                               where (SqlMethods.Like(_loanChange.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_loanChange.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _loanChange.Status != LoanChangeDataMapper.GetStatus(TransStatus.Deleted)
                               select _loanChange.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrLoanChangeHd> GetListLoanChange(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrLoanChangeHd> _result = new List<HRMTrLoanChangeHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _loanChange in this.db.HRMTrLoanChangeHds
                                where (SqlMethods.Like(_loanChange.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_loanChange.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _loanChange.Status != LoanChangeDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _loanChange.EditDate descending
                                select new
                                {
                                    TransNmbr = _loanChange.TransNmbr,
                                    FileNmbr = _loanChange.FileNmbr,
                                    Status = _loanChange.Status,
                                    TransDate = _loanChange.TransDate,
                                    LoanInNo = _loanChange.LoanInNo,
                                    EmpNumb = _loanChange.EmpNumb,
                                    EmpName = (
                                                    from _msEmployee in this.db.MsEmployees
                                                    where _msEmployee.EmpNumb == _loanChange.EmpNumb
                                                    select _msEmployee.EmpName
                                                ).FirstOrDefault(),
                                    Remark = _loanChange.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLoanChangeHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.LoanInNo, _row.EmpNumb, _row.EmpName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLoanChangeHd GetSingleLoanChange(string _prmCode)
        {
            HRMTrLoanChangeHd _result = null;

            try
            {
                _result = this.db.HRMTrLoanChangeHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleLoanChangeApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLoanChangeHd _hRMTrLoanChangeHd = this.db.HRMTrLoanChangeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrLoanChangeHd != null)
                    {
                        if (_hRMTrLoanChangeHd.Status != LoanChangeDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiLoanChange(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLoanChangeHd _loanChange = this.db.HRMTrLoanChangeHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_loanChange.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrLoanChangeDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrLoanChangeDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrLoanChangeHds.DeleteOnSubmit(_loanChange);

                        _result = true;
                    }
                    else
                    {
                        _result = false;
                        break;
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

        public bool DeleteMultiApproveLoanChange(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLoanChangeHd _hRMTrLoanChangeHd = this.db.HRMTrLoanChangeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrLoanChangeHd.Status == LoanChangeDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrLoanChangeHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrLoanChangeHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrLoanChangeHd != null)
                    {
                        if ((_hRMTrLoanChangeHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrLoanChangeDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrLoanChangeDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrLoanChangeHds.DeleteOnSubmit(_hRMTrLoanChangeHd);

                            _result = true;
                        }
                        else if (_hRMTrLoanChangeHd.FileNmbr != "" && _hRMTrLoanChangeHd.Status == LoanChangeDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrLoanChangeHd.Status = LoanChangeDataMapper.GetStatus(TransStatus.Deleted);
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

        public bool EditLoanChange(HRMTrLoanChangeHd _prmLoanChange)
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

        public string AddLoanChange(HRMTrLoanChangeHd _prmLoanChange)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmLoanChange.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrLoanChangeHds.InsertOnSubmit(_prmLoanChange);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                foreach (HRMTrLoanInDt2 _row in new LoanInBL().GetListLoanInDt2(_prmLoanChange.LoanInNo, _prmLoanChange.EmpNumb))
                {
                    HRMTrLoanChangeDt _hrmTrLoanChangeDt = new HRMTrLoanChangeDt();

                    _hrmTrLoanChangeDt.TransNmbr = _prmLoanChange.TransNmbr;
                    _hrmTrLoanChangeDt.PaymentNo = _row.PaymentNo;
                    _hrmTrLoanChangeDt.ClaimAmount = _row.ClaimAmount;
                    _hrmTrLoanChangeDt.ClaimDate = _row.ClaimDate;
                    _hrmTrLoanChangeDt.FgPay = _row.FgPay;

                    this.db.HRMTrLoanChangeDts.InsertOnSubmit(_hrmTrLoanChangeDt);
                }
                this.db.SubmitChanges();

                _result = _prmLoanChange.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrLoanChangeHd> GetListDDLLoanChange()
        {
            List<HRMTrLoanChangeHd> _result = new List<HRMTrLoanChangeHd>();

            try
            {
                var _query = (
                                from _loanChangeHd in this.db.HRMTrLoanChangeHds
                                where _loanChangeHd.Status == LoanChangeDataMapper.GetStatus(TransStatus.Posted)
                                orderby _loanChangeHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _loanChangeHd.TransNmbr,
                                    FileNmbr = _loanChangeHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLoanChangeHd(_row.TransNmbr, _row.FileNmbr));
                }
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
                this.db.spHRM_LoanChangesGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLoanChange);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();

                    _result = "Get Approval Success";
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
                    this.db.spHRM_LoanChangesApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrLoanChangeHd _loanChange = this.GetSingleLoanChange(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_loanChange.TransDate.Year, _loanChange.TransDate.Month, AppModule.GetValue(TransactionType.HRMLoanChange), this._companyTag, ""))
                        {
                            _loanChange.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLoanChange);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _loanChange.FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);

                        this.db.SubmitChanges();

                        _scope.Complete();

                        _result = "Approve Success";
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
                this.db.spHRM_LoanChangesPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLoanChange);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleLoanChange(_prmCode).FileNmbr;
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();

                    _result = "Posting Success";
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
                this.db.spHRM_LoanChangesUnPost(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "UnPosting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        #endregion

        #region HRMTrLoanChangeDt

        public double RowsCountLoanChangeDt(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _loanChangeDt in this.db.HRMTrLoanChangeDts
                where _loanChangeDt.TransNmbr == _prmCode
                select _loanChangeDt.PaymentNo
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrLoanChangeDt> GetListLoanChangeDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrLoanChangeDt> _result = new List<HRMTrLoanChangeDt>();

            try
            {
                var _query = (
                                from _loanChangeDt in this.db.HRMTrLoanChangeDts
                                where _loanChangeDt.TransNmbr == _prmCode
                                orderby _loanChangeDt.PaymentNo ascending
                                select new
                                {
                                    TransNmbr = _loanChangeDt.TransNmbr,
                                    PaymentNo = _loanChangeDt.PaymentNo,
                                    ClaimDate = _loanChangeDt.ClaimDate,
                                    ClaimAmount = _loanChangeDt.ClaimAmount,
                                    FgPay = _loanChangeDt.FgPay
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLoanChangeDt(_row.TransNmbr, _row.PaymentNo, _row.ClaimDate, _row.ClaimAmount, _row.FgPay));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Decimal GetAmountLoanChangeDt(string _prmCode)
        {
            Decimal _result = 0;

            try
            {
                var _query = (
                                from _loanChangeDt in this.db.HRMTrLoanChangeDts
                                where _loanChangeDt.TransNmbr == _prmCode && _loanChangeDt.FgPay == false
                                orderby _loanChangeDt.PaymentNo ascending
                                select _loanChangeDt.ClaimAmount
                            );

                if (_query.Count() > 0)
                {
                    _result = _query.Sum();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLoanChangeDt GetSingleLoanChangeDt(Int32 _prmPaymentNo, string _prmTransNmbr)
        {
            HRMTrLoanChangeDt _result = null;

            try
            {
                _result = this.db.HRMTrLoanChangeDts.Single(_temp => _temp.PaymentNo == _prmPaymentNo && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiLoanChangeDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrLoanChangeDt _loanChangeDt = this.db.HRMTrLoanChangeDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.PaymentNo == Convert.ToInt32(_tempSplit[1]));

                    this.db.HRMTrLoanChangeDts.DeleteOnSubmit(_loanChangeDt);
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

        public bool AddLoanChangeDt(HRMTrLoanChangeDt _prmLoanChangeDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrLoanChangeDts.InsertOnSubmit(_prmLoanChangeDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditLoanChangeDt(HRMTrLoanChangeDt _prmLoanChangeDt)
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

        #region HRMTrLoanChangeDt2

        public double RowsCountLoanChangeDt2(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _loanChangeDt2 in this.db.HRMTrLoanChangeDt2s
                where _loanChangeDt2.TransNmbr == _prmCode
                select _loanChangeDt2.PaymentNo
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrLoanChangeDt2> GetListLoanChangeDt2(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrLoanChangeDt2> _result = new List<HRMTrLoanChangeDt2>();

            try
            {
                var _query = (
                                from _loanChangeDt2 in this.db.HRMTrLoanChangeDt2s
                                where _loanChangeDt2.TransNmbr == _prmCode
                                orderby _loanChangeDt2.PaymentNo ascending
                                select new
                                {
                                    TransNmbr = _loanChangeDt2.TransNmbr,
                                    PaymentNo = _loanChangeDt2.PaymentNo,
                                    ClaimDate = _loanChangeDt2.ClaimDate,
                                    ClaimAmount = _loanChangeDt2.ClaimAmount
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLoanChangeDt2(_row.TransNmbr, _row.PaymentNo, _row.ClaimDate, _row.ClaimAmount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLoanChangeDt2 GetSingleLoanChangeDt2(Int32 _prmPaymentNo, string _prmTransNmbr)
        {
            HRMTrLoanChangeDt2 _result = null;

            try
            {
                _result = this.db.HRMTrLoanChangeDt2s.Single(_temp => _temp.PaymentNo == _prmPaymentNo && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiLoanChangeDt2(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrLoanChangeDt2 _loanChangeDt2 = this.db.HRMTrLoanChangeDt2s.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.PaymentNo == Convert.ToInt32(_tempSplit[1]));

                    this.db.HRMTrLoanChangeDt2s.DeleteOnSubmit(_loanChangeDt2);
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

        public bool AddLoanChangeDt2(HRMTrLoanChangeDt2 _prmLoanChangeDt2)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrLoanChangeDt2s.InsertOnSubmit(_prmLoanChangeDt2);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GenerateSameWithLoanChange(String _prmTransNmbr)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _loanChangeDt in this.db.HRMTrLoanChangeDts
                                where _loanChangeDt.TransNmbr == _prmTransNmbr && _loanChangeDt.FgPay == false
                                select _loanChangeDt
                             );

                foreach (HRMTrLoanChangeDt _row in _query)
                {
                    HRMTrLoanChangeDt2 _loanChangeDt2 = new HRMTrLoanChangeDt2();

                    _loanChangeDt2.TransNmbr = _row.TransNmbr;
                    _loanChangeDt2.PaymentNo = _row.PaymentNo;
                    _loanChangeDt2.ClaimDate = _row.ClaimDate;
                    _loanChangeDt2.ClaimAmount = _row.ClaimAmount;

                    this.db.HRMTrLoanChangeDt2s.InsertOnSubmit(_loanChangeDt2);
                }

                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
                _result = "Generate Failed, " + ex.Message;
            }

            return _result;
        }

        public String GenerateAllLoanChangeDt(String _prmTransNmbr, DateTime _prmStartClaim, Int32 _prmType, Int32 _prmPayment, Decimal _prmAmountLoan, Decimal _prmAmountClaim)
        {
            String _result = "";
            Decimal _amount = 0;
            Decimal _sisa = 0;
            try
            {
                var _query = (
                                from _loanChangeDt in this.db.HRMTrLoanChangeDts
                                where _loanChangeDt.TransNmbr == _prmTransNmbr && _loanChangeDt.FgPay == true
                                select _loanChangeDt.PaymentNo
                             ).Max();

                if (_prmType == 0)
                {
                    _amount = _prmAmountLoan;
                    _sisa = _prmAmountClaim;

                    while (_amount > 0)
                    {
                        if (_amount > _sisa)
                        {
                            _amount = _amount - _sisa;
                        }
                        else
                        {
                            _sisa = _amount;
                            _amount = _amount - _sisa;
                        }

                        int i = 0;

                        HRMTrLoanChangeDt2 _loanChangeDt2 = new HRMTrLoanChangeDt2();

                        _loanChangeDt2.TransNmbr = _prmTransNmbr;
                        _loanChangeDt2.PaymentNo = _query + i + 1;
                        _loanChangeDt2.ClaimAmount = _sisa;
                        _loanChangeDt2.ClaimDate = _prmStartClaim.AddMonths(i);

                        i = i + 1;
                    }
                }
                else if (_prmType == 1)
                {
                    for (int i = 0; i < _prmPayment; i++)
                    {
                        HRMTrLoanChangeDt2 _loanChangeDt2 = new HRMTrLoanChangeDt2();

                        _loanChangeDt2.TransNmbr = _prmTransNmbr;
                        _loanChangeDt2.PaymentNo = _query + i + 1;
                        _loanChangeDt2.ClaimAmount = _prmAmountClaim / _prmPayment;
                        _loanChangeDt2.ClaimDate = _prmStartClaim.AddMonths(i);
                    }
                }

                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
                _result = "Generate Failed, " + ex.Message;
            }

            return _result;
        }

        public bool EditLoanChangeDt2(HRMTrLoanChangeDt2 _prmLoanChangeDt2)
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

        ~LoanChangeBL()
        {

        }
    }
}
