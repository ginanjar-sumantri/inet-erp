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
    public sealed class LoanInBL : Base
    {
        public LoanInBL()
        {

        }

        #region HRMTrLoanInHd


        public double RowsCountLoanIn(string _prmCategory, string _prmKeyword)
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
                               from _loanIn in this.db.HRMTrLoanInHds
                               where (SqlMethods.Like(_loanIn.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_loanIn.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _loanIn.Status != LoanInDataMapper.GetStatus(TransStatus.Deleted)
                               select _loanIn.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrLoanInHd> GetListLoanIn(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrLoanInHd> _result = new List<HRMTrLoanInHd>();

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
                                from _loanIn in this.db.HRMTrLoanInHds
                                where (SqlMethods.Like(_loanIn.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_loanIn.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _loanIn.Status != LoanInDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _loanIn.EditDate descending
                                select new
                                {
                                    TransNmbr = _loanIn.TransNmbr,
                                    FileNmbr = _loanIn.FileNmbr,
                                    Status = _loanIn.Status,
                                    TransDate = _loanIn.TransDate,
                                    LoanCode = _loanIn.LoanCode,
                                    LoanName = (
                                                    from _msLoan in this.db.HRMMsLoans
                                                    where _msLoan.LoanCode == _loanIn.LoanCode
                                                    select _msLoan.LoanName
                                                ).FirstOrDefault(),
                                    PayrollCode = _loanIn.PayrollCode,
                                    Remark = _loanIn.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLoanInHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.LoanCode, _row.LoanName, _row.PayrollCode, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLoanInHd GetSingleLoanIn(string _prmCode)
        {
            HRMTrLoanInHd _result = null;

            try
            {
                _result = this.db.HRMTrLoanInHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleLoanInApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLoanInHd _hRMTrLoanInHd = this.db.HRMTrLoanInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrLoanInHd != null)
                    {
                        if (_hRMTrLoanInHd.Status != LoanInDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiLoanIn(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLoanInHd _loanIn = this.db.HRMTrLoanInHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_loanIn.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrLoanInDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrLoanInDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrLoanInHds.DeleteOnSubmit(_loanIn);

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

        public bool DeleteMultiApproveLoanIn(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLoanInHd _hRMTrLoanInHd = this.db.HRMTrLoanInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrLoanInHd.Status == LoanInDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrLoanInHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrLoanInHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrLoanInHd != null)
                    {
                        if ((_hRMTrLoanInHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrLoanInDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrLoanInDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrLoanInHds.DeleteOnSubmit(_hRMTrLoanInHd);

                            _result = true;
                        }
                        else if (_hRMTrLoanInHd.FileNmbr != "" && _hRMTrLoanInHd.Status == LoanInDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrLoanInHd.Status = LoanInDataMapper.GetStatus(TransStatus.Deleted);
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

        public bool EditLoanIn(HRMTrLoanInHd _prmLoanIn)
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

        public string AddLoanIn(HRMTrLoanInHd _prmLoanIn)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmLoanIn.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrLoanInHds.InsertOnSubmit(_prmLoanIn);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmLoanIn.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrLoanInHd> GetListDDLLoanIn()
        {
            List<HRMTrLoanInHd> _result = new List<HRMTrLoanInHd>();

            try
            {
                var _query = (
                                from _loanInHd in this.db.HRMTrLoanInHds
                                where _loanInHd.Status == LoanInDataMapper.GetStatus(TransStatus.Posted)
                                orderby _loanInHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _loanInHd.TransNmbr,
                                    FileNmbr = _loanInHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLoanInHd(_row.TransNmbr, _row.FileNmbr));
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
                this.db.spHRM_LoanInGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLoanIn);
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
                    this.db.spHRM_LoanInApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrLoanInHd _loanIn = this.GetSingleLoanIn(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_loanIn.TransDate.Year, _loanIn.TransDate.Month, AppModule.GetValue(TransactionType.HRMLoanIn), this._companyTag, ""))
                        {
                            _loanIn.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLoanIn);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _loanIn.FileNmbr;
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
                this.db.spHRM_LoanInPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLoanIn);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleLoanIn(_prmCode).FileNmbr;
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
                this.db.spHRM_LoanInUnPost(_prmCode, _prmuser, ref _result);

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

        #region HRMTrLoanInDt

        public double RowsCountLoanInDt(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _loanInDt in this.db.HRMTrLoanInDts
                where _loanInDt.TransNmbr == _prmCode
                select _loanInDt.EmpNumb
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrLoanInDt> GetListLoanInDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrLoanInDt> _result = new List<HRMTrLoanInDt>();

            try
            {
                var _query = (
                                from _loanInDt in this.db.HRMTrLoanInDts
                                where _loanInDt.TransNmbr == _prmCode
                                orderby _loanInDt.EmpNumb ascending
                                select new
                                {
                                    TransNmbr = _loanInDt.TransNmbr,
                                    EmpNumb = _loanInDt.EmpNumb,
                                    EmpName = (
                                                from _msEmployee in this.db.MsEmployees
                                                where _msEmployee.EmpNumb == _loanInDt.EmpNumb
                                                select _msEmployee.EmpName
                                              ).FirstOrDefault(),
                                    StartClaim = _loanInDt.StartClaim,
                                    CurrCode = _loanInDt.CurrCode,
                                    AmountLoan = _loanInDt.AmountLoan,
                                    Payment = _loanInDt.Payment,
                                    AmountClaim = _loanInDt.AmountClaim,
                                    Remark = _loanInDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLoanInDt(_row.TransNmbr, _row.EmpNumb, _row.EmpName, _row.StartClaim, _row.CurrCode, _row.AmountLoan, _row.Payment, _row.AmountClaim, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLoanInDt GetSingleLoanInDt(string _prmEmpNumb, string _prmTransNmbr)
        {
            HRMTrLoanInDt _result = null;

            try
            {
                _result = this.db.HRMTrLoanInDts.Single(_temp => _temp.EmpNumb == _prmEmpNumb && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiLoanInDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrLoanInDt _loanInDt = this.db.HRMTrLoanInDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.EmpNumb == _tempSplit[1]);

                    this.db.HRMTrLoanInDts.DeleteOnSubmit(_loanInDt);
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

        public bool AddLoanInDt(HRMTrLoanInDt _prmLoanInDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrLoanInDts.InsertOnSubmit(_prmLoanInDt);

                //for (int i = 0; i < _prmLoanInDt.Payment; i++)
                //{
                //    HRMTrLoanInDt2 _hrmTrLoanInDt2 = new HRMTrLoanInDt2();

                //    _hrmTrLoanInDt2.TransNmbr = _prmLoanInDt.TransNmbr;
                //    _hrmTrLoanInDt2.PaymentNo = i + 1;
                //    _hrmTrLoanInDt2.EmpNumb = _prmLoanInDt.EmpNumb;
                //    _hrmTrLoanInDt2.ClaimAmount = Convert.ToDecimal(_prmLoanInDt.AmountClaim);
                //    _hrmTrLoanInDt2.ClaimDate = _prmLoanInDt.StartClaim.AddMonths(i);
                //    _hrmTrLoanInDt2.FgPay = false;

                //    this.db.HRMTrLoanInDt2s.InsertOnSubmit(_hrmTrLoanInDt2);
                //}

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditLoanInDt(HRMTrLoanInDt _prmLoanInDt)
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

        #region HRMTrLoanInDt2

        public double RowsCountLoanInDt2(string _prmTransNmbr, String _prmEmpNumb)
        {
            double _result = 0;

            var _query =
             (
                from _loanInDt2 in this.db.HRMTrLoanInDt2s
                where _loanInDt2.TransNmbr == _prmTransNmbr && _loanInDt2.EmpNumb == _prmEmpNumb
                select _loanInDt2.PaymentNo
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrLoanInDt2> GetListLoanInDt2(string _prmTransNmbr)
        {
            List<HRMTrLoanInDt2> _result = new List<HRMTrLoanInDt2>();

            try
            {
                var _query = (
                                from _loanInDt2 in this.db.HRMTrLoanInDt2s
                                where _loanInDt2.TransNmbr == _prmTransNmbr
                                orderby _loanInDt2.EmpNumb ascending
                                select new
                                {
                                    TransNmbr = _loanInDt2.TransNmbr,
                                    EmpNumb = _loanInDt2.EmpNumb,
                                    EmpName = (
                                                from _msEmployee in this.db.MsEmployees
                                                where _msEmployee.EmpNumb == _loanInDt2.EmpNumb
                                                select _msEmployee.EmpName
                                              ).FirstOrDefault(),
                                    PaymentNo = _loanInDt2.PaymentNo,
                                    ClaimDate = _loanInDt2.ClaimDate,
                                    ClaimAmount = _loanInDt2.ClaimAmount,
                                    FgPay = _loanInDt2.FgPay
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLoanInDt2(_row.TransNmbr, _row.EmpNumb, _row.EmpName, _row.PaymentNo, _row.ClaimDate, _row.ClaimAmount, _row.FgPay));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrLoanInDt2> GetListLoanInDt2(string _prmTransNmbr, String _prmEmpNumb)
        {
            List<HRMTrLoanInDt2> _result = new List<HRMTrLoanInDt2>();

            try
            {
                var _query = (
                                from _loanInDt2 in this.db.HRMTrLoanInDt2s
                                where _loanInDt2.TransNmbr == _prmTransNmbr && _loanInDt2.EmpNumb == _prmEmpNumb
                                orderby _loanInDt2.PaymentNo ascending
                                select new
                                {
                                    TransNmbr = _loanInDt2.TransNmbr,
                                    EmpNumb = _loanInDt2.EmpNumb,
                                    EmpName = (
                                                from _msEmployee in this.db.MsEmployees
                                                where _msEmployee.EmpNumb == _loanInDt2.EmpNumb
                                                select _msEmployee.EmpName
                                              ).FirstOrDefault(),
                                    PaymentNo = _loanInDt2.PaymentNo,
                                    ClaimDate = _loanInDt2.ClaimDate,
                                    ClaimAmount = _loanInDt2.ClaimAmount,
                                    FgPay = _loanInDt2.FgPay
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLoanInDt2(_row.TransNmbr, _row.EmpNumb, _row.EmpName, _row.PaymentNo, _row.ClaimDate, _row.ClaimAmount, _row.FgPay));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLoanInDt2 GetSingleLoanInDt2(int _prmPaymentNo, string _prmEmpNumb, string _prmTransNmbr)
        {
            HRMTrLoanInDt2 _result = null;

            try
            {
                _result = this.db.HRMTrLoanInDt2s.Single(_temp => _temp.PaymentNo == _prmPaymentNo && _temp.EmpNumb == _prmEmpNumb && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiLoanInDt2(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrLoanInDt2 _loanInDt2 = this.db.HRMTrLoanInDt2s.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.EmpNumb == _tempSplit[1] && _temp.PaymentNo == Convert.ToInt32(_tempSplit[2]));

                    this.db.HRMTrLoanInDt2s.DeleteOnSubmit(_loanInDt2);
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

        public bool AddLoanInDt2(HRMTrLoanInDt2 _prmLoanInDt2)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrLoanInDt2s.InsertOnSubmit(_prmLoanInDt2);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditLoanInDt2(HRMTrLoanInDt2 _prmLoanInDt2)
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

        ~LoanInBL()
        {

        }
    }
}
