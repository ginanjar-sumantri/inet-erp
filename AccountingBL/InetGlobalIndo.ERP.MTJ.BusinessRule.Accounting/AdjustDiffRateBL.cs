using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public sealed class GLAdjustDiffRateBL : Base
    {
        public GLAdjustDiffRateBL()
        {
        }

        #region GLAdjustDiffRateHd
        public double RowsCountGLAdjustDiffRateHd(string _prmCategory, string _prmKeyword)
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
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
                _pattern1 = "%%";

            }
            else if (_prmCategory == "Remark")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            var _query =
                        (
                            from _glAdjDiffRateHd in this.db.GLAdjustDiffRateHds
                            where (SqlMethods.Like(_glAdjDiffRateHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like((_glAdjDiffRateHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_glAdjDiffRateHd.Remark ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                            select _glAdjDiffRateHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<GLAdjustDiffRateHd> GetListGLAdjustDiffRateHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<GLAdjustDiffRateHd> _result = new List<GLAdjustDiffRateHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";

            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
                _pattern1 = "%%";

            }
            else if (_prmCategory == "Remark")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query =
                            (
                                from _glAdjDiffRateHd in this.db.GLAdjustDiffRateHds
                                where (SqlMethods.Like(_glAdjDiffRateHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_glAdjDiffRateHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like((_glAdjDiffRateHd.Remark ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                orderby _glAdjDiffRateHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _glAdjDiffRateHd.TransNmbr,
                                    FileNmbr = _glAdjDiffRateHd.FileNmbr,
                                    TransDate = _glAdjDiffRateHd.TransDate,
                                    Status = _glAdjDiffRateHd.Status,
                                    Remark = _glAdjDiffRateHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GLAdjustDiffRateHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLAdjustDiffRateHd GetSingleGLAdjustDiffRateHd(string _prmCode)
        {
            GLAdjustDiffRateHd _result = null;

            try
            {
                _result = this.db.GLAdjustDiffRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public char GetStatusGLAdjustDiffRateHd(string _prmCode)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _glAdjDiffRateHd in this.db.GLAdjustDiffRateHds
                                where _glAdjDiffRateHd.TransNmbr == _prmCode
                                select new
                                {
                                    Status = _glAdjDiffRateHd.Status
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

        public bool DeleteMultiGLAdjustDiffRateHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLAdjustDiffRateHd _glAdjDiffRateHd = this.db.GLAdjustDiffRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glAdjDiffRateHd != null)
                    {
                        if ((_glAdjDiffRateHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLAdjustDiffRateDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLAdjustDiffRateDts.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.GLAdjustDiffRateDt2s
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.GLAdjustDiffRateDt2s.DeleteAllOnSubmit(_query2);

                            this.db.GLAdjustDiffRateHds.DeleteOnSubmit(_glAdjDiffRateHd);

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

        public string AddGLAdjustDiffRateHd(GLAdjustDiffRateHd _prmGLAdjustDiffRateHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_prmGLAdjustDiffRateHd.TransDate.Year, _prmGLAdjustDiffRateHd.TransDate.Month, AppModule.GetValue(TransactionType.GLAdjustDiffRateHd), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmGLAdjustDiffRateHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.GLAdjustDiffRateHds.InsertOnSubmit(_prmGLAdjustDiffRateHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmGLAdjustDiffRateHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGLAdjustDiffRateHd(GLAdjustDiffRateHd _prmGLAdjustDiffRateHd)
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

        //public string GetApproval(string _prmCode, string _prmuser)
        //{
        //    string _result = "";

        //    try
        //    {
        //        this.db.S_GLGLAdjustDiffRateHdGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

        //        if (_result == "")
        //        {
        //            _result = "Get Approval Success";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = "Get Approval Failed ";
        //        ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
        //    }

        //    return _result;
        //}

        //public string Approve(string _prmCode, string _prmuser)
        //{
        //    string _result = "";

        //    try
        //    {
        //        using (TransactionScope _scope = new TransactionScope())
        //        {
        //            this.db.S_GLGLAdjustDiffRateHdApprove(_prmCode, 0, 0, _prmuser, ref _result);

        //            if (_result == "")
        //            {
        //                GLAdjustDiffRateHd _glAdjDiffRateHd = this.GetSingleGLAdjustDiffRateHd(_prmCode);
        //                foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_glAdjDiffRateHd.TransDate.Year, _glAdjDiffRateHd.TransDate.Month, AppModule.GetValue(TransactionType.GLAdjustDiffRateHd), this._companyTag, ""))
        //                {
        //                    _glAdjDiffRateHd.FileNmbr = _item.Number;
        //                }

        //                this.db.SubmitChanges();

        //                _scope.Complete();

        //                _result = "Approve Success";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = "Approve Failed";
        //        ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
        //    }

        //    return _result;
        //}

        public string Posting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                GLAdjustDiffRateHd _glAdjDiffRateHd = this.db.GLAdjustDiffRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_glAdjDiffRateHd.TransDate.Year, _glAdjDiffRateHd.TransDate.Month, AppModule.GetValue(TransactionType.AdjustDiffRate), this._companyTag, ""))
                {
                    _glAdjDiffRateHd.FileNmbr = _item.Number;
                }
                this.db.SubmitChanges();

                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                String _locked = _transCloseBL.IsExistAndLocked(_glAdjDiffRateHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLAdjustDiffRatePost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.AdjustDiffRate);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleGLAdjustDiffRateHd(_prmCode).FileNmbr;
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

                GLAdjustDiffRateHd _glAdjDiffRateHd = this.db.GLAdjustDiffRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glAdjDiffRateHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLAdjustDiffRateUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.AdjustDiffRate);
                        //_transActivity.TransNmbr = _prmCode.ToString();
                        //_transActivity.FileNmbr = this.GetSingleGLAdjustDiffRateHd(_prmCode).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleGLAdjustDiffRateHd(_prmCode).TransDate;
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
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Process(string _prmCode)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLAdjustDiffRateHd _glAdjDiffRateHd = this.db.GLAdjustDiffRateHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glAdjDiffRateHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLAdjustDiffRateGetProcess(_prmCode);

                    if (_result == "")
                    {
                        _result = "Process Success";
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "Process Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }
        #endregion

        #region GLAdjustDiffRateDt

        public int RowsCountGLAdjustDiffRateDt(string _prmTransNmbr)
        {
            int _result = 0;

            _result = this.db.GLAdjustDiffRateDts.Where(_row => _row.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()).Count();

            return _result;
        }

        public List<GLAdjustDiffRateDt> GetListGLAdjustDiffRateDt(string _prmCode)
        {
            List<GLAdjustDiffRateDt> _result = new List<GLAdjustDiffRateDt>();

            try
            {
                var _query =
                            (
                                from _adjDiffRateDt in this.db.GLAdjustDiffRateDts
                                where _adjDiffRateDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _adjDiffRateDt.CurrCode ascending
                                select new
                                {
                                    TransNmbr = _adjDiffRateDt.TransNmbr,
                                    CurrCode = _adjDiffRateDt.CurrCode,
                                    ForexRate = _adjDiffRateDt.ForexRate
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new GLAdjustDiffRateDt(_row.TransNmbr, _row.CurrCode, _row.ForexRate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLAdjustDiffRateDt GetSingleGLAdjustDiffRateDt(string _prmCode, string _prmCurrCode)
        {
            GLAdjustDiffRateDt _result = null;

            try
            {
                _result = this.db.GLAdjustDiffRateDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.CurrCode == _prmCurrCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiGLAdjustDiffRateDt(string[] _prmCurrCode, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCurrCode.Length; i++)
                {
                    GLAdjustDiffRateDt _glGLAdjustDiffRateDt = this.db.GLAdjustDiffRateDts.Single(_temp => _temp.CurrCode == _prmCurrCode[i] && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    GLAdjustDiffRateHd _glAdjDiffRateHd = this.GetSingleGLAdjustDiffRateHd(_prmCode);
                    this.db.GLAdjustDiffRateDts.DeleteOnSubmit(_glGLAdjustDiffRateDt);
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

        public bool AddGLAdjustDiffRateDt(GLAdjustDiffRateDt _prmGLAdjustDiffRateDt)
        {
            bool _result = false;

            try
            {
                this.db.GLAdjustDiffRateDts.InsertOnSubmit(_prmGLAdjustDiffRateDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGLAdjustDiffRateDt(GLAdjustDiffRateDt _prmGLAdjustDiffRateDt)
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

        public Boolean IsCurrencyExist(String _prmTransNmbr, String _prmCurrCode)
        {
            Boolean _result = false;

            var _query = (
                            from _adjustDt in this.db.GLAdjustDiffRateDts
                            where _adjustDt.TransNmbr == _prmTransNmbr
                                && _adjustDt.CurrCode == _prmCurrCode
                            select _adjustDt
                         ).Count();

            if (_query > 0)
            {
                _result = true;
            }

            return _result;
        }

        #endregion

        #region GLAdjustDiffRateDt2
        public int RowsCountGLAdjustDiffRateDt2(string _prmTransNmbr)
        {
            int _result = 0;

            _result = this.db.GLAdjustDiffRateDt2s.Where(_row => _row.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()).Count();

            return _result;
        }

        public List<GLAdjustDiffRateDt2> GetListGLAdjustDiffRateDt2(string _prmCode)
        {
            List<GLAdjustDiffRateDt2> _result = new List<GLAdjustDiffRateDt2>();

            try
            {
                var _query =
                            (
                                from _adjDiffRateDt2 in this.db.GLAdjustDiffRateDt2s
                                where _adjDiffRateDt2.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    TransNmbr = _adjDiffRateDt2.TransNmbr,
                                    Account = _adjDiffRateDt2.Account,
                                    CurrCode = _adjDiffRateDt2.CurrCode,
                                    FgSubLed = _adjDiffRateDt2.FgSubLed,
                                    SubLed = _adjDiffRateDt2.SubLed,
                                    TotalForex = _adjDiffRateDt2.TotalForex,
                                    TotalNewHome = _adjDiffRateDt2.TotalNewHome,
                                    TotalOldHome = _adjDiffRateDt2.TotalOldHome,
                                    TotalAdjustHome = _adjDiffRateDt2.TotalAdjustHome
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new GLAdjustDiffRateDt2(_row.TransNmbr, _row.Account, _row.CurrCode, _row.FgSubLed, _row.SubLed, _row.TotalForex, _row.TotalNewHome, _row.TotalOldHome, _row.TotalAdjustHome));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLAdjustDiffRateDt2 GetSingleGLAdjustDiffRateDt2(string _prmCode, string _prmAccount, string _prmCurrCode)
        {
            GLAdjustDiffRateDt2 _result = null;

            try
            {
                _result = this.db.GLAdjustDiffRateDt2s.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.Account == _prmAccount && _temp.CurrCode == _prmCurrCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiGLAdjustDiffRateDt2(string _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _adjDiffRate2 in this.db.GLAdjustDiffRateDt2s
                                where _adjDiffRate2.TransNmbr == _prmCode
                                select _adjDiffRate2
                             );

                this.db.GLAdjustDiffRateDt2s.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddGLAdjustDiffRateDt2(GLAdjustDiffRateDt2 _prmGLAdjustDiffRateDt2)
        {
            bool _result = false;

            try
            {
                this.db.GLAdjustDiffRateDt2s.InsertOnSubmit(_prmGLAdjustDiffRateDt2);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGLAdjustDiffRateDt2(GLAdjustDiffRateDt2 _prmGLAdjustDiffRateDt2)
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

        ~GLAdjustDiffRateBL()
        {
        }
    }
}
