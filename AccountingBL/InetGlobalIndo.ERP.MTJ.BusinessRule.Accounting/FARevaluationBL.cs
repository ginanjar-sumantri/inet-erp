using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class FARevaluationBL : Base
    {
        public FARevaluationBL()
        {
        }

        #region FADevaluation
        public double RowsCountGLFADevaluationHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _glFADevaluationHd in this.db.GLFADevaluationHds
                            where (SqlMethods.Like(_glFADevaluationHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like((_glFADevaluationHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && (_glFADevaluationHd.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted))  
                            select _glFADevaluationHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<GLFADevaluationHd> GetListGLFADevaluationHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<GLFADevaluationHd> _result = new List<GLFADevaluationHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query1 = (
                               from _glFADevaluationHd in this.db.GLFADevaluationHds
                               where (SqlMethods.Like(_glFADevaluationHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like((_glFADevaluationHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && (_glFADevaluationHd.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted))  
                               orderby _glFADevaluationHd.TransDate descending
                               select new
                               {
                                   TransNmbr = _glFADevaluationHd.TransNmbr,
                                   FileNmbr = _glFADevaluationHd.FileNmbr,
                                   TransDate = _glFADevaluationHd.TransDate,
                                   Status = _glFADevaluationHd.Status,
                                   Operator = _glFADevaluationHd.Operator,
                                   Remark = _glFADevaluationHd.Remark
                               }
                            );

                if (_prmOrderBy == "Trans No")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TransNmbr)) : (_query1.OrderByDescending(a => a.TransNmbr));
                if (_prmOrderBy == "File No")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FileNmbr)) : (_query1.OrderByDescending(a => a.FileNmbr));
                if (_prmOrderBy == "Trans Date")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TransDate)) : (_query1.OrderByDescending(a => a.TransDate));
                if (_prmOrderBy == "Status")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Status)) : (_query1.OrderByDescending(a => a.Status));
                if (_prmOrderBy == "Operator")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Operator)) : (_query1.OrderByDescending(a => a.Operator));
                if (_prmOrderBy == "Remark")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Remark)) : (_query1.OrderByDescending(a => a.Remark));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GLFADevaluationHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.Operator, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFADevaluationHd GetSingleGLFADevaluationHd(string _prmTransNmbr)
        {
            GLFADevaluationHd _result = null;

            try
            {
                _result = this.db.GLFADevaluationHds.Single(_temp => _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiGLDevaluationHd(string[] _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTransNmbr.Length; i++)
                {
                    GLFADevaluationHd _glFADevaluationHd = this.db.GLFADevaluationHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower());

                    if (_glFADevaluationHd != null)
                    {
                        if ((_glFADevaluationHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLFADevaluationDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLFADevaluationDts.DeleteAllOnSubmit(_query);

                            this.db.GLFADevaluationHds.DeleteOnSubmit(_glFADevaluationHd);

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

        public bool DeleteMultiApproveGLDevaluationHd(string[] _prmTransNmbr, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTransNmbr.Length; i++)
                {
                    GLFADevaluationHd _glFADevaluationHd = this.db.GLFADevaluationHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower());

                    if (_glFADevaluationHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _glFADevaluationHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _glFADevaluationHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_glFADevaluationHd != null)
                    {
                        if ((_glFADevaluationHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLFADevaluationDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLFADevaluationDts.DeleteAllOnSubmit(_query);

                            this.db.GLFADevaluationHds.DeleteOnSubmit(_glFADevaluationHd);

                            _result = true;
                        }
                        else if (_glFADevaluationHd.FileNmbr != "" && _glFADevaluationHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _glFADevaluationHd.Status = TransactionDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddGLFADevaluationHd(GLFADevaluationHd _prmGLFADevaluationHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmGLFADevaluationHd.TransDate.Year, _prmGLFADevaluationHd.TransDate.Month, AppModule.GetValue(TransactionType.FADevaluation), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmGLFADevaluationHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.GLFADevaluationHds.InsertOnSubmit(_prmGLFADevaluationHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmGLFADevaluationHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGLFADevaluationHd(GLFADevaluationHd _prmGLFADevaluationHd)
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

        public string GetApprDevalution(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_GLFADevaluationGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FADevaluation);
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

        public string ApproveFADevaluation(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_GLFADevaluationApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        GLFADevaluationHd _glFADevaluationHd = this.GetSingleGLFADevaluationHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_glFADevaluationHd.TransDate.Year, _glFADevaluationHd.TransDate.Month, AppModule.GetValue(TransactionType.FADevaluation), this._companyTag, ""))
                        {
                            _glFADevaluationHd.FileNmbr = _item.Number;
                        }

                        

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FADevaluation);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleGLFADevaluationHd(_prmTransNmbr).FileNmbr;
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
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string PostingFADevaluation(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLFADevaluationHd _glFADevaluationHd = this.db.GLFADevaluationHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFADevaluationHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFADevaluationPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FADevaluation);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleGLFADevaluationHd(_prmTransNmbr).FileNmbr;
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
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string UnpostingFADevaluation(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLFADevaluationHd _glFADevaluationHd = this.db.GLFADevaluationHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFADevaluationHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFADevaluationUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.FADevaluation);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleGLFADevaluationHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleGLFADevaluationHd(_prmTransNmbr).TransDate;
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
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int RowsCountGLFADevaluationDt(string _prmTransNmbr)
        {
            int _result = 0;

            _result = this.db.GLFADevaluationDts.Where(_row => _row.TransNmbr == _prmTransNmbr).Count();

            return _result;
        }

        public Double RowsCountListFAForDevaluation(string _prmTransNmbr, string _prmCategory, string _prmKeyword)
        {
            Double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
            }

            try
            {
                _result = (
                                from _vDevaluationGetDetail in this.db.V_MsFixedAssets
                                join _msFixedAsset in this.db.MsFixedAssets
                                on _vDevaluationGetDetail.FA_Code equals _msFixedAsset.FACode
                                where !(
                                            from _devaluationDetail in this.db.GLFADevaluationDts
                                            where _devaluationDetail.FACode == _vDevaluationGetDetail.FA_Code && _devaluationDetail.TransNmbr == _prmTransNmbr
                                            select _devaluationDetail.FACode
                                       ).Contains(_vDevaluationGetDetail.FA_Code)
                                       && _vDevaluationGetDetail.FgActive == 'Y'
                                && _vDevaluationGetDetail.FgSold == 'N'
                                && _vDevaluationGetDetail.FgProcess == 'Y'
                                && (SqlMethods.Like(_msFixedAsset.FACode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_msFixedAsset.FAName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    FACode = _vDevaluationGetDetail.FA_Code
                                }
                           ).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLFADevaluationDt> ListFAForDevaluation(int _prmReqPage, int _prmPageSize, string _prmTransNmbr, string _prmCategory, string _prmKeyword)
        {
            List<GLFADevaluationDt> _result = new List<GLFADevaluationDt>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (from _vDevaluationGetDetail in this.db.V_MsFixedAssets
                              //join _msFixedAsset in this.db.MsFixedAssets
                              //on _vDevaluationGetDetail.FA_Code equals _msFixedAsset.FACode                              
                              //  from _spDevaluationGetDetail in this.db.S_GLFADevaluationGetDt(_pattern1, _pattern2)
                              where 
                                  !(
                                       from _devaluationDetail in this.db.GLFADevaluationDts
                                       where _devaluationDetail.FACode == _vDevaluationGetDetail.FA_Code 
                                            && _devaluationDetail.TransNmbr == _prmTransNmbr
                                            select _devaluationDetail.FACode
                                       ).Contains(_vDevaluationGetDetail.FA_Code)
                                && _vDevaluationGetDetail.FgActive == 'Y'
                                && _vDevaluationGetDetail.FgSold == 'N'
                                && _vDevaluationGetDetail.FgProcess == 'Y'
                                && (SqlMethods.Like(_vDevaluationGetDetail.FA_Code.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_vDevaluationGetDetail.FA_Name.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    FACode = _vDevaluationGetDetail.FA_Code,
                                    FAName = _vDevaluationGetDetail.FA_Name,
                                    BalanceLife = _vDevaluationGetDetail.LifeCurrent,
                                    BalanceAmount = _vDevaluationGetDetail.AmountCurrent
                                }
                              ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    int _balanceLife = Convert.ToInt32((_row.BalanceLife == null) ? 0 : _row.BalanceLife);
                    decimal _balanceAmount = Convert.ToDecimal((_row.BalanceAmount == null) ? 0 : _row.BalanceAmount);

                    _result.Add(new GLFADevaluationDt(_row.FACode, _row.FAName, _balanceLife, _balanceAmount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFixedAsset> ListFAForDevaluation(string _prmTransNmbr)
        {
            List<MsFixedAsset> _result = new List<MsFixedAsset>();

            try
            {
                var _query = (from _vDevaluationGetDetail in this.db.V_MsFixedAssets
                              where !(
                                          from _devaluationDetail in this.db.GLFADevaluationDts
                                          where _devaluationDetail.FACode == _vDevaluationGetDetail.FA_Code && _devaluationDetail.TransNmbr == _prmTransNmbr
                                          select _devaluationDetail.FACode
                                     ).Contains(_vDevaluationGetDetail.FA_Code)
                                     && _vDevaluationGetDetail.FgActive == 'Y'
                                && _vDevaluationGetDetail.FgSold == 'N'
                                && _vDevaluationGetDetail.FgProcess == 'Y'
                              select new
                              {
                                  _vDevaluationGetDetail.FA_Code
                              }
                              );

                foreach (var _item in _query)
                {
                    MsFixedAsset _reavaluation = new MsFixedAsset();

                    _reavaluation.FACode = _item.FA_Code;

                    _result.Add(_reavaluation);
                }
            }

            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLFADevaluationDt> GetListGLFADevaluationDt(string _prmTransNmbr)
        {
            List<GLFADevaluationDt> _result = new List<GLFADevaluationDt>();

            try
            {
                var _query = (
                               from _glFADevaluationDt in this.db.GLFADevaluationDts
                               orderby _glFADevaluationDt.FACode ascending
                               where _glFADevaluationDt.TransNmbr == _prmTransNmbr
                               select new
                               {
                                   TransNmbr = _glFADevaluationDt.TransNmbr,
                                   FACode = _glFADevaluationDt.FACode,
                                   FAName = (
                                                from _msFixedAsset in this.db.MsFixedAssets
                                                where _msFixedAsset.FACode == _glFADevaluationDt.FACode
                                                select _msFixedAsset.FAName
                                             ).FirstOrDefault(),
                                   BalanceLife = _glFADevaluationDt.BalanceLife,
                                   BalanceAmount = _glFADevaluationDt.BalanceAmount,
                                   NewLife = _glFADevaluationDt.NewLife,
                                   NewAmount = _glFADevaluationDt.NewAmount,
                                   AdjustLife = _glFADevaluationDt.AdjustLife,
                                   AdjustAmount = _glFADevaluationDt.AdjustAmount
                               }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { TransNmbr = this._string, FACode = this._string, FAName = this._string, BalanceLife = this._int, BalanceAmount = this._decimal, NewLife = this._int, NewAmount = this._decimal, AdjustLife = this._int, AdjustAmount = this._decimal });

                    _result.Add(new GLFADevaluationDt(_row.TransNmbr, _row.FACode, _row.FAName, _row.BalanceLife, _row.BalanceAmount, _row.NewLife, _row.NewAmount, _row.AdjustLife, _row.AdjustAmount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLFADevaluationDt> GetListGLFADevaluationDt(int _prmReqPage, int _prmPageSize, string _prmTransNmbr, String _prmOrderBy, Boolean _prmAscDesc, String _prmCategory, String _prmKeyword)
        {
            List<GLFADevaluationDt> _result = new List<GLFADevaluationDt>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query1 = (
                               from _glFADevaluationDt in this.db.GLFADevaluationDts

                               join _msFA in this.db.MsFixedAssets
                               on _glFADevaluationDt.FACode equals _msFA.FACode
                               orderby _glFADevaluationDt.FACode ascending
                               where _glFADevaluationDt.TransNmbr == _prmTransNmbr
                                   && (SqlMethods.Like(_glFADevaluationDt.FACode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msFA.FAName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   //&& _glFADevaluationDt.FAName.Trim().ToLower().Contains(_pattern2.Trim().ToLower())
                               select new
                               {
                                   TransNmbr = _glFADevaluationDt.TransNmbr,
                                   FACode = _glFADevaluationDt.FACode,
                                   FAName = (
                                                from _msFixedAsset in this.db.MsFixedAssets
                                                where _msFixedAsset.FACode == _glFADevaluationDt.FACode
                                                select _msFixedAsset.FAName
                                             ).FirstOrDefault(),
                                   BalanceLife = _glFADevaluationDt.BalanceLife,
                                   BalanceAmount = _glFADevaluationDt.BalanceAmount,
                                   NewLife = _glFADevaluationDt.NewLife,
                                   NewAmount = _glFADevaluationDt.NewAmount,
                                   AdjustLife = _glFADevaluationDt.AdjustLife,
                                   AdjustAmount = _glFADevaluationDt.AdjustAmount
                               }
                            );

                if (_prmOrderBy == "Fixed Asset Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FACode)) : (_query1.OrderByDescending(a => a.FACode));
                if (_prmOrderBy == "Fixed Asset Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAName)) : (_query1.OrderByDescending(a => a.FAName));
                if (_prmOrderBy == "Life")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.BalanceLife)) : (_query1.OrderByDescending(a => a.BalanceLife));
                if (_prmOrderBy == "Amount")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.BalanceAmount)) : (_query1.OrderByDescending(a => a.BalanceAmount));
                if (_prmOrderBy == "Life1")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.NewLife)) : (_query1.OrderByDescending(a => a.NewLife));
                if (_prmOrderBy == "Amount1")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.NewAmount)) : (_query1.OrderByDescending(a => a.NewAmount));
                if (_prmOrderBy == "Life2")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AdjustLife)) : (_query1.OrderByDescending(a => a.AdjustLife));
                if (_prmOrderBy == "Amount2")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AdjustAmount)) : (_query1.OrderByDescending(a => a.AdjustAmount));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { TransNmbr = this._string, FACode = this._string, FAName = this._string, BalanceLife = this._int, BalanceAmount = this._decimal, NewLife = this._int, NewAmount = this._decimal, AdjustLife = this._int, AdjustAmount = this._decimal });

                    _result.Add(new GLFADevaluationDt(_row.TransNmbr, _row.FACode, _row.FAName, _row.BalanceLife, _row.BalanceAmount, _row.NewLife, _row.NewAmount, _row.AdjustLife, _row.AdjustAmount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleGLFADevaluationHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFADevaluationHd _glFADevaluationHd = this.db.GLFADevaluationHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFADevaluationHd != null)
                    {
                        if (_glFADevaluationHd.Status != TransactionDataMapper.GetStatus(TransStatus.Posted))
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

        public GLFADevaluationDt GetSingleGLFADevaluationDt(string _prmTransNmbr, string _prmFACode)
        {
            GLFADevaluationDt _result = null;

            try
            {
                _result = this.db.GLFADevaluationDts.Single(_temp => _temp.FACode == _prmFACode && _temp.TransNmbr == _prmTransNmbr);

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiGLFADevaluationDt(string[] _prmFACode, string _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmFACode.Length; i++)
                {
                    GLFADevaluationDt _glFADevaluationDt = this.db.GLFADevaluationDts.Single(_temp => _temp.FACode == _prmFACode[i] && _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());

                    this.db.GLFADevaluationDts.DeleteOnSubmit(_glFADevaluationDt);
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

        public bool DeleteAllFADevaluationDt(string _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _glFADevaluationDt in this.db.GLFADevaluationDts
                                where _glFADevaluationDt.TransNmbr == _prmTransNmbr
                                select _glFADevaluationDt
                              );

                this.db.GLFADevaluationDts.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddGLFADevaluationDt(GLFADevaluationDt _prmGLFADevaluationDt)
        {
            bool _result = false;

            try
            {

                this.db.GLFADevaluationDts.InsertOnSubmit(_prmGLFADevaluationDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGLFADevaluationDt(GLFADevaluationDt _prmGLFADevaluationDt)
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

        public List<GLFADevaluationDt> AddListFADevaluation(string[] _prmCode, string _prmTransNmbr)
        {
            List<GLFADevaluationDt> _result = new List<GLFADevaluationDt>();

            try
            {
                var _query = (from _vDevaluationGetDetail in this.db.V_MsFixedAssets                              
                              //  from _spDevaluationGetDetail in this.db.S_GLFADevaluationGetDt(_pattern1, _pattern2)
                              where !(
                                       from _devaluationDetail in this.db.GLFADevaluationDts
                                       where _devaluationDetail.FACode == _vDevaluationGetDetail.FA_Code
                                            && _devaluationDetail.TransNmbr == _prmTransNmbr
                                       select _devaluationDetail.FACode
                                       ).Contains(_vDevaluationGetDetail.FA_Code)
                                && _vDevaluationGetDetail.FgActive == 'Y'
                                && _vDevaluationGetDetail.FgSold == 'N'
                                && _vDevaluationGetDetail.FgProcess == 'Y'

                              select _vDevaluationGetDetail
                                );
                              
                foreach (V_MsFixedAsset _item in _query)
                {
                    GLFADevaluationDt _glFADevaluationDt = new GLFADevaluationDt();
                    _glFADevaluationDt.TransNmbr = _prmTransNmbr;
                    _glFADevaluationDt.FACode = _item.FA_Code;
                    _glFADevaluationDt.BalanceLife = Convert.ToInt32(_item.LifeCurrent);
                    _glFADevaluationDt.BalanceAmount = Convert.ToDecimal(_item.AmountCurrent);
                    _glFADevaluationDt.NewLife = Convert.ToInt32(_item.LifeCurrent);
                    _glFADevaluationDt.NewAmount = Convert.ToDecimal(_item.AmountCurrent);
                    _glFADevaluationDt.AdjustLife = 0;
                    _glFADevaluationDt.AdjustAmount = 0;

                    _result.Add(_glFADevaluationDt);
                }


                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFADevaluationDt _glFADevaluationDt = new GLFADevaluationDt();
                    _glFADevaluationDt.FACode = _prmCode[i];

                    foreach (var item in _result)
                    {
                        if (_glFADevaluationDt.FACode == item.FACode)
                        {
                            this.db.GLFADevaluationDts.InsertOnSubmit(item);
                        }
                    }
                }

                this.db.SubmitChanges();

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~FARevaluationBL()
        {
        }
    }
}
