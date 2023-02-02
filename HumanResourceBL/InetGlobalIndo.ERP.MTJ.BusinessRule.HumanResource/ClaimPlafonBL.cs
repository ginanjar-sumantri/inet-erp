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
    public sealed class ClaimPlafonBL : Base
    {
        public ClaimPlafonBL()
        {

        }

        #region HRMTrClaimPlafonHd


        public double RowsCountClaimPlafon(string _prmCategory, string _prmKeyword)
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
                               from _claimPlafon in this.db.HRMTrClaimPlafonHds
                               where (SqlMethods.Like(_claimPlafon.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_claimPlafon.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _claimPlafon.Status != ClaimPlafonDataMapper.GetStatus(TransStatus.Deleted)
                               select _claimPlafon.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrClaimPlafonHd> GetListClaimPlafon(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrClaimPlafonHd> _result = new List<HRMTrClaimPlafonHd>();

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
                                from _claimPlafon in this.db.HRMTrClaimPlafonHds
                                where (SqlMethods.Like(_claimPlafon.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_claimPlafon.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _claimPlafon.Status != ClaimPlafonDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _claimPlafon.EditDate descending
                                select new
                                {
                                    TransNmbr = _claimPlafon.TransNmbr,
                                    FileNmbr = _claimPlafon.FileNmbr,
                                    Status = _claimPlafon.Status,
                                    TransDate = _claimPlafon.TransDate,
                                    StartDate = _claimPlafon.StartDate,
                                    EndDate = _claimPlafon.EndDate,
                                    ClaimCode = _claimPlafon.ClaimCode,
                                    ClaimName = (
                                                    from _claim in this.db.HRMMsClaims
                                                    where _claim.ClaimCode == _claimPlafon.ClaimCode
                                                    select _claim.ClaimName
                                               ).FirstOrDefault(),
                                    DefautAmount = _claimPlafon.DefautAmount
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrClaimPlafonHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.StartDate, _row.EndDate, _row.ClaimCode, _row.ClaimName, _row.DefautAmount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrClaimPlafonHd GetSingleClaimPlafon(string _prmCode)
        {
            HRMTrClaimPlafonHd _result = null;

            try
            {
                _result = this.db.HRMTrClaimPlafonHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleHRMTrClaimPlafonHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrClaimPlafonHd _hRMTrClaimPlafonHd = this.db.HRMTrClaimPlafonHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrClaimPlafonHd != null)
                    {
                        if (_hRMTrClaimPlafonHd.Status != ClaimPlafonDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiClaimPlafon(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrClaimPlafonHd _claimPlafon = this.db.HRMTrClaimPlafonHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_claimPlafon.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrClaimPlafonDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrClaimPlafonDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrClaimPlafonHds.DeleteOnSubmit(_claimPlafon);

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

        public bool DeleteMultiApproveHRMTrClaimPlafonHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrClaimPlafonHd _hRMTrClaimPlafonHd = this.db.HRMTrClaimPlafonHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrClaimPlafonHd.Status == ClaimPlafonDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrClaimPlafonHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrClaimPlafonHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrClaimPlafonHd != null)
                    {
                        if ((_hRMTrClaimPlafonHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrClaimPlafonDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrClaimPlafonDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrClaimPlafonHds.DeleteOnSubmit(_hRMTrClaimPlafonHd);

                            _result = true;
                        }
                        else if (_hRMTrClaimPlafonHd.FileNmbr != "" && _hRMTrClaimPlafonHd.Status == ClaimPlafonDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrClaimPlafonHd.Status = ClaimPlafonDataMapper.GetStatus(TransStatus.Deleted);
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

        public bool EditClaimPlafon(HRMTrClaimPlafonHd _prmClaimPlafon)
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

        public string AddClaimPlafon(HRMTrClaimPlafonHd _prmClaimPlafon)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmClaimPlafon.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrClaimPlafonHds.InsertOnSubmit(_prmClaimPlafon);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmClaimPlafon.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrClaimPlafonHd> GetListDDLClaimPlafon()
        {
            List<HRMTrClaimPlafonHd> _result = new List<HRMTrClaimPlafonHd>();

            try
            {
                var _query = (
                                from _claimPlafonHd in this.db.HRMTrClaimPlafonHds
                                where _claimPlafonHd.Status == ClaimPlafonDataMapper.GetStatus(TransStatus.Posted)
                                orderby _claimPlafonHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _claimPlafonHd.TransNmbr,
                                    FileNmbr = _claimPlafonHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrClaimPlafonHd(_row.TransNmbr, _row.FileNmbr));
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
                this.db.spHRM_ClaimPlafonGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ClaimPlafon);
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
                    this.db.spHRM_ClaimPlafonApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrClaimPlafonHd _claimPlafon = this.GetSingleClaimPlafon(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_claimPlafon.TransDate.Year, _claimPlafon.TransDate.Month, AppModule.GetValue(TransactionType.ClaimPlafon), this._companyTag, ""))
                        {
                            _claimPlafon.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ClaimPlafon);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _claimPlafon.FileNmbr;
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
                this.db.spHRM_ClaimPlafonPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ClaimPlafon);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleClaimPlafon(_prmCode).FileNmbr;
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
                this.db.spHRM_ClaimPlafonUnPost(_prmCode, _prmuser, ref _result);

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

        #region HRMTrClaimPlafonDt

        public double RowsCountClaimPlafonDt(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _claimPlafonDt in this.db.HRMTrClaimPlafonDts
                where _claimPlafonDt.TransNmbr == _prmCode
                select _claimPlafonDt.PlafonType
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrClaimPlafonDt> GetListClaimPlafonDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrClaimPlafonDt> _result = new List<HRMTrClaimPlafonDt>();

            try
            {
                var _query = (
                                from _claimPlafonDt in this.db.HRMTrClaimPlafonDts
                                where _claimPlafonDt.TransNmbr == _prmCode
                                orderby _claimPlafonDt.PlafonType ascending
                                select new
                                {
                                    TransNmbr = _claimPlafonDt.TransNmbr,
                                    PlafonType = _claimPlafonDt.PlafonType,
                                    Amount = _claimPlafonDt.Amount,
                                    Remark = _claimPlafonDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrClaimPlafonDt(_row.TransNmbr, _row.PlafonType, _row.Amount, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrClaimPlafonDt GetSingleClaimPlafonDt(string _prmPlafonTypeCode, string _prmTransNmbr)
        {
            HRMTrClaimPlafonDt _result = null;

            try
            {
                _result = this.db.HRMTrClaimPlafonDts.Single(_temp => _temp.PlafonType == _prmPlafonTypeCode && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiClaimPlafonDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrClaimPlafonDt _claimPlafonDt = this.db.HRMTrClaimPlafonDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.PlafonType == _tempSplit[1]);

                    this.db.HRMTrClaimPlafonDts.DeleteOnSubmit(_claimPlafonDt);
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

        public bool AddClaimPlafonDt(HRMTrClaimPlafonDt _prmClaimPlafonDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrClaimPlafonDts.InsertOnSubmit(_prmClaimPlafonDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditClaimPlafonDt(HRMTrClaimPlafonDt _prmClaimPlafonDt)
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

        ~ClaimPlafonBL()
        {

        }
    }
}
