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
    public sealed class ClaimIssueBL : Base
    {
        public ClaimIssueBL()
        {

        }

        #region HRMTrClaimIssueHd


        public double RowsCountClaimIssue(string _prmCategory, string _prmKeyword)
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
                               from _claimIssue in this.db.HRMTrClaimIssueHds
                               where (SqlMethods.Like(_claimIssue.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_claimIssue.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _claimIssue.Status != ClaimIssueDataMapper.GetStatus(TransStatus.Deleted)
                               select _claimIssue.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrClaimIssueHd> GetListClaimIssue(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrClaimIssueHd> _result = new List<HRMTrClaimIssueHd>();

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
                                from _claimIssue in this.db.HRMTrClaimIssueHds
                                where (SqlMethods.Like(_claimIssue.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_claimIssue.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _claimIssue.Status != ClaimIssueDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _claimIssue.EditDate descending
                                select new
                                {
                                    TransNmbr = _claimIssue.TransNmbr,
                                    FileNmbr = _claimIssue.FileNmbr,
                                    Status = _claimIssue.Status,
                                    TransDate = _claimIssue.TransDate,
                                    EmpNumb = _claimIssue.EmpNumb,
                                    EmpName = (
                                                from _employee in this.db.MsEmployees
                                                where _employee.EmpNumb == _claimIssue.EmpNumb
                                                select _employee.EmpName
                                               ).FirstOrDefault(),
                                    CurrCode = _claimIssue.CurrCode,
                                    ForexRate = _claimIssue.ForexRate,
                                    AccidentDate = _claimIssue.AccidentDate,
                                    ClaimFor = _claimIssue.ClaimFor,
                                    ClaimFamilyInfo = _claimIssue.ClaimFamilyInfo,
                                    Remark = _claimIssue.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrClaimIssueHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.EmpNumb, _row.EmpName, _row.CurrCode, _row.ForexRate, _row.AccidentDate, _row.ClaimFor, _row.ClaimFamilyInfo, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrClaimIssueHd GetSingleClaimIssue(string _prmCode)
        {
            HRMTrClaimIssueHd _result = null;

            try
            {
                _result = this.db.HRMTrClaimIssueHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleHRMTrClaimIssueHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrClaimIssueHd _hRMTrClaimIssueHd = this.db.HRMTrClaimIssueHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrClaimIssueHd != null)
                    {
                        if (_hRMTrClaimIssueHd.Status != ClaimIssueDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiClaimIssue(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrClaimIssueHd _claimIssue = this.db.HRMTrClaimIssueHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_claimIssue.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrClaimIssueDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrClaimIssueDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrClaimIssueHds.DeleteOnSubmit(_claimIssue);

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

        public bool DeleteMultiApproveHRMTrClaimIssueHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrClaimIssueHd _hRMTrClaimIssueHd = this.db.HRMTrClaimIssueHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrClaimIssueHd.Status == ClaimIssueDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrClaimIssueHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrClaimIssueHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrClaimIssueHd != null)
                    {
                        if ((_hRMTrClaimIssueHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrClaimIssueDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrClaimIssueDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrClaimIssueHds.DeleteOnSubmit(_hRMTrClaimIssueHd);

                            _result = true;
                        }
                        else if (_hRMTrClaimIssueHd.FileNmbr != "" && _hRMTrClaimIssueHd.Status == ClaimIssueDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrClaimIssueHd.Status = ClaimIssueDataMapper.GetStatus(TransStatus.Deleted);
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

        public bool EditClaimIssue(HRMTrClaimIssueHd _prmClaimIssue)
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

        public string AddClaimIssue(HRMTrClaimIssueHd _prmClaimIssue)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmClaimIssue.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrClaimIssueHds.InsertOnSubmit(_prmClaimIssue);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmClaimIssue.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrClaimIssueHd> GetListDDLClaimIssue()
        {
            List<HRMTrClaimIssueHd> _result = new List<HRMTrClaimIssueHd>();

            try
            {
                var _query = (
                                from _claimIssueHd in this.db.HRMTrClaimIssueHds
                                where _claimIssueHd.Status == ClaimIssueDataMapper.GetStatus(TransStatus.Posted)
                                orderby _claimIssueHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _claimIssueHd.TransNmbr,
                                    FileNmbr = _claimIssueHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrClaimIssueHd(_row.TransNmbr, _row.FileNmbr));
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
                this.db.spHRM_ClaimIssueGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ClaimIssue);
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
                    this.db.spHRM_ClaimIssueApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrClaimIssueHd _claimIssue = this.GetSingleClaimIssue(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_claimIssue.TransDate.Year, _claimIssue.TransDate.Month, AppModule.GetValue(TransactionType.ClaimIssue), this._companyTag, ""))
                        {
                            _claimIssue.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ClaimIssue);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _claimIssue.FileNmbr;
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
                _result = "Approve Failed, " + ex.Message;
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Posting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spHRM_ClaimIssuePosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ClaimIssue);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleClaimIssue(_prmCode).FileNmbr;
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
                _result = "Posting Failed, " + ex.Message;
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string UnPosting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spHRM_ClaimIssueUnPost(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "UnPosting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed, " + ex.Message;
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        #endregion

        #region HRMTrClaimIssueDt

        public double RowsCountClaimIssueDt(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _claimIssueDt in this.db.HRMTrClaimIssueDts
                where _claimIssueDt.TransNmbr == _prmCode
                select _claimIssueDt.ClaimCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrClaimIssueDt> GetListClaimIssueDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrClaimIssueDt> _result = new List<HRMTrClaimIssueDt>();

            try
            {
                var _query = (
                                from _claimIssueDt in this.db.HRMTrClaimIssueDts
                                where _claimIssueDt.TransNmbr == _prmCode
                                orderby _claimIssueDt.ClaimCode ascending
                                select new
                                {
                                    TransNmbr = _claimIssueDt.TransNmbr,
                                    ClaimCode = _claimIssueDt.ClaimCode,
                                    ClaimName = (
                                                    from _claim in this.db.HRMMsClaims
                                                    where _claim.ClaimCode == _claimIssueDt.ClaimCode
                                                    select _claim.ClaimName
                                                ).FirstOrDefault(),
                                    ReceiptNo = _claimIssueDt.ReceiptNo,
                                    ReceiptDate = _claimIssueDt.ReceiptDate,
                                    AmountClaim = _claimIssueDt.AmountClaim,
                                    ReimburstPercentage = _claimIssueDt.ReimburstPercentage,
                                    AmountReimburst = _claimIssueDt.AmountReimburst,
                                    Remark = _claimIssueDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrClaimIssueDt(_row.TransNmbr, _row.ClaimCode, _row.ClaimName, _row.ReceiptNo, _row.ReceiptDate, _row.AmountClaim, _row.ReimburstPercentage, _row.AmountReimburst, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetPlafonInfo(DateTime _prmAccidentDate, String _prmClaimCode, String _prmEmpNumb, String _prmClaimFor)
        {
            String _result = "";

            var _query = (
                            from _sp in this.db.spHRM_ClaimIssueGetInfoPlafon(_prmAccidentDate, _prmClaimCode, _prmEmpNumb, _prmClaimFor)
                            select new
                            {
                                Balance = _sp.Balance,
                                EndPlafon = _sp.EndPlafon,
                                FgCheck = _sp.FgCheck,
                                Plafon = _sp.Plafon,
                                StartPlafon = _sp.StartPlafon,
                                Used = _sp.Used
                            }
                         );

            foreach (var _row in _query)
            {
                _result = _row.Balance.ToString() + "=" + _row.Plafon.ToString() + "=" + _row.Used.ToString() + "=" + DateFormMapper.GetValue(_row.StartPlafon) + "=" + DateFormMapper.GetValue(_row.EndPlafon) + "=" + _row.FgCheck.ToString();
            }

            return _result;
        }

        public HRMTrClaimIssueDt GetSingleClaimIssueDt(string _prmClaimCode, string _prmTransNmbr)
        {
            HRMTrClaimIssueDt _result = null;

            try
            {
                _result = this.db.HRMTrClaimIssueDts.Single(_temp => _temp.ClaimCode == _prmClaimCode && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiClaimIssueDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrClaimIssueDt _claimIssueDt = this.db.HRMTrClaimIssueDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.ClaimCode == _tempSplit[1]);

                    this.db.HRMTrClaimIssueDts.DeleteOnSubmit(_claimIssueDt);
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

        public bool AddClaimIssueDt(HRMTrClaimIssueDt _prmClaimIssueDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrClaimIssueDts.InsertOnSubmit(_prmClaimIssueDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditClaimIssueDt(HRMTrClaimIssueDt _prmClaimIssueDt)
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

        ~ClaimIssueBL()
        {

        }
    }
}
