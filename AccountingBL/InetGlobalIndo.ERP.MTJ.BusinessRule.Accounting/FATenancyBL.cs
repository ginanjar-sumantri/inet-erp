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
    public sealed class FATenancyBL : Base
    {
        public FATenancyBL()
        {
        }

        #region FATenancy
        public double RowsCountGLFATenancyHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "CustName")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }

            var _query =
                        (
                            from _glFATenancyHd in this.db.GLFATenancyHds
                            join _msCustomer in this.db.MsCustomers
                                on _glFATenancyHd.CustCode equals _msCustomer.CustCode
                            where (SqlMethods.Like(_glFATenancyHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_glFATenancyHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                            select _glFATenancyHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<GLFATenancyHd> GetListGLFATenancyHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<GLFATenancyHd> _result = new List<GLFATenancyHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "CustName")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }

            try
            {
                var _query = (
                               from _glFATenancyHd in this.db.GLFATenancyHds
                               join _msCustomer in this.db.MsCustomers
                               on _glFATenancyHd.CustCode equals _msCustomer.CustCode
                               join _msTerm in this.db.MsTerms
                               on _glFATenancyHd.Term equals _msTerm.TermCode
                               where (SqlMethods.Like(_glFATenancyHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && (SqlMethods.Like((_glFATenancyHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               orderby _glFATenancyHd.TransDate descending
                               select new
                               {
                                   TransNmbr = _glFATenancyHd.TransNmbr,
                                   FileNmbr = _glFATenancyHd.FileNmbr,
                                   TransDate = _glFATenancyHd.TransDate,
                                   Status = _glFATenancyHd.Status,
                                   CustName = _msCustomer.CustName,
                                   Term = _msTerm.TermName
                               }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {

                    _result.Add(new GLFATenancyHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.CustName, _row.Term));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public GLFATenancyHd GetSingleGLFATenancyHd(string _prmTransNmbr)
        //{
        //    GLFATenancyHd _result = new GLFATenancyHd();

        //    try
        //    {
        //        var _query = (
        //                       from _glFATenancyHd in this.db.GLFATenancyHds
        //                       join _msCustomer in this.db.MsCustomers
        //                       on _glFATenancyHd.CustCode equals _msCustomer.CustCode
        //                       join _msTerm in this.db.MsTerms
        //                       on _glFATenancyHd.Term equals _msTerm.TermCode
        //                       where _glFATenancyHd.TransNmbr == _prmTransNmbr
        //                       orderby _glFATenancyHd.TransDate descending

        //                       select new
        //                       {
        //                           TransNmbr = _glFATenancyHd.TransNmbr,
        //                           TransDate = _glFATenancyHd.TransDate,
        //                           Status = _glFATenancyHd.Status,
        //                           CustCode = _msCustomer.CustCode,
        //                           CustName = _msCustomer.CustName,
        //                           Currency = _glFATenancyHd.CurrCode,
        //                           TermCode = _glFATenancyHd.Term,
        //                           TermName = _msTerm.TermName,
        //                           Remark = _glFATenancyHd.Remark,
        //                           ForexRate = _glFATenancyHd.ForexRate,
        //                           Attn = _glFATenancyHd.Attn,
        //                           PPN = _glFATenancyHd.PPN,
        //                           PPNNo = _glFATenancyHd.PPNNo,
        //                           PPNForex = _glFATenancyHd.PPNForex,
        //                           PPNRate = _glFATenancyHd.PPNRate,
        //                           BaseForex = _glFATenancyHd.BaseForex,
        //                           Discount = _glFATenancyHd.DiscForex,
        //                           PPNDate = _glFATenancyHd.PPNDate,
        //                           TotalForex = _glFATenancyHd.TotalForex
        //                       }
        //                    ).Single();

        //        _result.TransNmbr = _query.TransNmbr;
        //        _result.TransDate = _query.TransDate;
        //        _result.Status = _query.Status;
        //        _result.CustCode = _query.CustCode;
        //        _result.CustName = _query.CustName;
        //        _result.CurrCode = _query.Currency;
        //        _result.ForexRate = _query.ForexRate;
        //        _result.Term = _query.TermCode;
        //        _result.TermName = _query.TermName;
        //        _result.Remark = _query.Remark;
        //        _result.Attn = _query.Attn;
        //        _result.PPNNo = _query.PPNNo;
        //        _result.PPN = _query.PPN;
        //        _result.PPNForex = _query.PPNForex;
        //        _result.PPNRate = _query.PPNRate;
        //        _result.BaseForex = _query.BaseForex;
        //        _result.DiscForex = _query.Discount;
        //        _result.PPNDate = _query.PPNDate;
        //        _result.TotalForex = _query.TotalForex;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public GLFATenancyHd GetSingleGLFATenancyHd(string _prmTransNmbr)
        {
            GLFATenancyHd _result = new GLFATenancyHd();

            try
            {
                _result = this.db.GLFATenancyHds.Single(_temp => _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleFATenancyHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAPurchaseHd _glFAPurchaseHd = this.db.GLFAPurchaseHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAPurchaseHd != null)
                    {
                        if (_glFAPurchaseHd.Status != TransactionDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiGLTenancyHd(string[] _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTransNmbr.Length; i++)
                {
                    GLFATenancyHd _glFATenancyHd = this.db.GLFATenancyHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower());

                    if (_glFATenancyHd != null)
                    {
                        if ((_glFATenancyHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLFATenancyDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLFATenancyDts.DeleteAllOnSubmit(_query);

                            this.db.GLFATenancyHds.DeleteOnSubmit(_glFATenancyHd);

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

        public bool DeleteMultiApproveGLTenancyHd(string[] _prmTransNmbr, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTransNmbr.Length; i++)
                {
                    GLFATenancyHd _glFATenancyHd = this.db.GLFATenancyHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower());

                    if (_glFATenancyHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _glFATenancyHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _glFATenancyHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_glFATenancyHd != null)
                    {
                        if ((_glFATenancyHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLFATenancyDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLFATenancyDts.DeleteAllOnSubmit(_query);

                            this.db.GLFATenancyHds.DeleteOnSubmit(_glFATenancyHd);

                            _result = true;
                        }
                        else if (_glFATenancyHd.FileNmbr != "" && _glFATenancyHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _glFATenancyHd.Status = TransactionDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddGLFATenancyHd(GLFATenancyHd _prmGLFATenancyHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmGLFATenancyHd.TransDate.Year, _prmGLFATenancyHd.TransDate.Month, AppModule.GetValue(TransactionType.FATenancy), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmGLFATenancyHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.GLFATenancyHds.InsertOnSubmit(_prmGLFATenancyHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmGLFATenancyHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGLFATenancyHd(GLFATenancyHd _prmGLFATenancyHd)
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
            string _period = "0" + _prmPeriod.ToString();

            try
            {
                this.db.S_GLFATenancyGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FATenancy);
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

        public string ApproveFATenancy(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_GLFATenancyApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        GLFATenancyHd _glFATenancyHd = this.GetSingleGLFATenancyHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_glFATenancyHd.TransDate.Year, _glFATenancyHd.TransDate.Month, AppModule.GetValue(TransactionType.FATenancy), this._companyTag, ""))
                        {
                            _glFATenancyHd.FileNmbr = _item.Number;
                        }
                                               

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FATenancy);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = this.GetSingleGLFATenancyHd(_prmTransNmbr).FileNmbr;
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

        public string PostingFATenancy(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLFATenancyHd _glFATetancyHd = this.db.GLFATenancyHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFATetancyHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFATenancyPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FATenancy);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = this.GetSingleGLFATenancyHd(_prmTransNmbr).FileNmbr;
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

        public string UnpostingFATenancy(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLFATenancyHd _glFATetancyHd = this.db.GLFATenancyHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFATetancyHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFATenancyUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.FATenancy);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = this.GetSingleGLFATenancyHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleGLFATenancyHd(_prmTransNmbr).TransDate;
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

        public int RowsCountGLFATenancyDt(string _prmTransNmbr)
        {
            int _result = 0;

            _result = this.db.GLFATenancyDts.Where(_row => _row.TransNmbr == _prmTransNmbr).Count();

            return _result;
        }

        public List<GLFATenancyDt> GetListGLFATenancyDt(string _prmTransNmbr) //no paging
        {
            List<GLFATenancyDt> _result = new List<GLFATenancyDt>();

            try
            {
                var _query = (
                               from _glFATenancyDt in this.db.GLFATenancyDts
                               orderby _glFATenancyDt.FACode ascending
                               where _glFATenancyDt.TransNmbr == _prmTransNmbr
                               select new
                               {
                                   TransNmbr = _glFATenancyDt.TransNmbr,
                                   FACode = _glFATenancyDt.FACode,
                                   FAName = (
                                                from _msFixedAsset in this.db.MsFixedAssets
                                                where _msFixedAsset.FACode == _glFATenancyDt.FACode
                                                select _msFixedAsset.FAName
                                             ).FirstOrDefault(),
                                   Qty = _glFATenancyDt.Qty,
                                   Unit = _glFATenancyDt.Unit,
                                   Price = _glFATenancyDt.Price,
                                   AmountForex = _glFATenancyDt.AmountForex,
                                   StartTenancy = _glFATenancyDt.StartTenancy,
                                   EndTenancy = _glFATenancyDt.EndTenancy,
                                   Remark = _glFATenancyDt.Remark
                               }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { TransNmbr = this._string, FACode = this._string, FAName = this._string, Qty = this._decimal, Unit = this._string, Price = this._decimal, AmountForex = this._decimal, StartTenancy = this._datetime, EndTenancy = this._datetime, Remark = this._string });

                    _result.Add(new GLFATenancyDt(_row.TransNmbr, _row.FACode, _row.FAName, _row.Qty, _row.Unit, _row.Price, _row.AmountForex, _row.StartTenancy, _row.EndTenancy, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLFATenancyDt> GetListGLFATenancyDt(int _prmReqPage, int _prmPageSize, string _prmTransNmbr, String _prmCategory, String _prmKeyword) //paging
        {
            List<GLFATenancyDt> _result = new List<GLFATenancyDt>();

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
                var _query = (
                               from _glFATenancyDt in this.db.GLFATenancyDts

                               join _msFA in this.db.MsFixedAssets
                               on _glFATenancyDt.FACode equals _msFA.FACode
                               orderby _glFATenancyDt.FACode ascending
                               where _glFATenancyDt.TransNmbr == _prmTransNmbr
                               && (SqlMethods.Like(_glFATenancyDt.FACode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msFA.FAName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               select new
                               {
                                   TransNmbr = _glFATenancyDt.TransNmbr,
                                   FACode = _glFATenancyDt.FACode,
                                   FAName = (
                                                from _msFixedAsset in this.db.MsFixedAssets
                                                where _msFixedAsset.FACode == _glFATenancyDt.FACode
                                                select _msFixedAsset.FAName
                                             ).FirstOrDefault(),
                                   Qty = _glFATenancyDt.Qty,
                                   Unit = (
                                                from _msUnit in this.db.MsUnits
                                                where _msUnit.UnitCode == _glFATenancyDt.Unit
                                                select _msUnit.UnitName
                                             ).FirstOrDefault(),
                                   Price = _glFATenancyDt.Price,
                                   AmountForex = _glFATenancyDt.AmountForex,
                                   StartTenancy = _glFATenancyDt.StartTenancy,
                                   EndTenancy = _glFATenancyDt.EndTenancy,
                                   Remark = _glFATenancyDt.Remark
                               }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { TransNmbr = this._string, FACode = this._string, FAName = this._string, Qty = this._decimal, Unit = this._string, Price = this._decimal, AmountForex = this._decimal, StartTenancy = this._datetime, EndTenancy = this._datetime, Remark = this._string });

                    _result.Add(new GLFATenancyDt(_row.TransNmbr, _row.FACode, _row.FAName, _row.Qty, _row.Unit, _row.Price, _row.AmountForex, _row.StartTenancy, _row.EndTenancy, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFATenancyDt GetSingleGLFATenancyDt(string _prmTransNmbr, string _prmFACode)
        {
            GLFATenancyDt _result = null;

            try
            {
                _result = this.db.GLFATenancyDts.Single(_temp => _temp.FACode == _prmFACode && _temp.TransNmbr == _prmTransNmbr);

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiGLFATenancyDt(string[] _prmFACode, string _prmTransNmbr)
        {
            bool _result = false;

            GLFATenancyHd _glFATenancyHd = new GLFATenancyHd();

            decimal _total = 0;

            try
            {
                for (int i = 0; i < _prmFACode.Length; i++)
                {
                    GLFATenancyDt _glFATenancyDt = this.db.GLFATenancyDts.Single(_temp => _temp.FACode == _prmFACode[i] && _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());

                    this.db.GLFATenancyDts.DeleteOnSubmit(_glFATenancyDt);

                    var _query = (
                                from _glFATenancyDt2 in this.db.GLFATenancyDts
                                where !(
                                            from _glFATenancyDt3 in this.db.GLFATenancyDts
                                            where _glFATenancyDt3.FACode == _prmFACode[i] && _glFATenancyDt3.TransNmbr == _prmTransNmbr
                                            select _glFATenancyDt3.FACode
                                        ).Contains(_glFATenancyDt2.FACode)
                                        && _glFATenancyDt2.TransNmbr == _prmTransNmbr
                                group _glFATenancyDt2 by _glFATenancyDt2.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                    foreach (var _obj in _query)
                    {
                        _total = _obj.AmountForex;
                    }

                    _glFATenancyHd = this.db.GLFATenancyHds.Single(_fa => _fa.TransNmbr == _prmTransNmbr);

                    _glFATenancyHd.BaseForex = _total;
                    _glFATenancyHd.PPNForex = (_glFATenancyHd.BaseForex - _glFATenancyHd.DiscForex) * _glFATenancyHd.PPN / 100;
                    _glFATenancyHd.TotalForex = _glFATenancyHd.BaseForex - _glFATenancyHd.DiscForex + _glFATenancyHd.PPNForex;
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

        public bool AddGLFATenancyDt(GLFATenancyDt _prmGLFATenancyDt)
        {
            bool _result = false;

            GLFATenancyHd _glFATenancyHd = new GLFATenancyHd();

            decimal _total = 0;

            try
            {
                var _query = (
                                from _glFATenancyDt in this.db.GLFATenancyDts
                                where !(
                                            from _faTenancyDt in this.db.GLFATenancyDts
                                            where _faTenancyDt.FACode == _prmGLFATenancyDt.FACode && _faTenancyDt.TransNmbr == _prmGLFATenancyDt.TransNmbr
                                            select _faTenancyDt.FACode
                                        ).Contains(_glFATenancyDt.FACode)
                                        && _glFATenancyDt.TransNmbr == _prmGLFATenancyDt.TransNmbr
                                group _glFATenancyDt by _glFATenancyDt.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }


                _glFATenancyHd = this.db.GLFATenancyHds.Single(_fa => _fa.TransNmbr == _prmGLFATenancyDt.TransNmbr);

                _glFATenancyHd.BaseForex = _total + _prmGLFATenancyDt.AmountForex;
                _glFATenancyHd.PPNForex = (_glFATenancyHd.BaseForex - _glFATenancyHd.DiscForex) * _glFATenancyHd.PPN / 100;
                _glFATenancyHd.TotalForex = _glFATenancyHd.BaseForex - _glFATenancyHd.DiscForex + _glFATenancyHd.PPNForex;

                this.db.GLFATenancyDts.InsertOnSubmit(_prmGLFATenancyDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGLFATenancyDt(GLFATenancyDt _prmGLFATenancyDt)
        {
            bool _result = false;

            GLFATenancyHd _glFATenancyHd = new GLFATenancyHd();

            decimal _total = 0;

            try
            {
                var _query = (
                                from _glFATenancyDt in this.db.GLFATenancyDts
                                where !(
                                            from _faTenancyDt in this.db.GLFATenancyDts
                                            where _faTenancyDt.FACode == _prmGLFATenancyDt.FACode && _faTenancyDt.TransNmbr == _prmGLFATenancyDt.TransNmbr
                                            select _faTenancyDt.FACode
                                        ).Contains(_glFATenancyDt.FACode)
                                        && _glFATenancyDt.TransNmbr == _prmGLFATenancyDt.TransNmbr
                                group _glFATenancyDt by _glFATenancyDt.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }


                _glFATenancyHd = this.db.GLFATenancyHds.Single(_fa => _fa.TransNmbr == _prmGLFATenancyDt.TransNmbr);

                _glFATenancyHd.BaseForex = _total + _prmGLFATenancyDt.AmountForex;
                _glFATenancyHd.PPNForex = (_glFATenancyHd.BaseForex - _glFATenancyHd.DiscForex) * _glFATenancyHd.PPN / 100;
                _glFATenancyHd.TotalForex = _glFATenancyHd.BaseForex - _glFATenancyHd.DiscForex + _glFATenancyHd.PPNForex;

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

        ~FATenancyBL()
        {
        }
    }
}
