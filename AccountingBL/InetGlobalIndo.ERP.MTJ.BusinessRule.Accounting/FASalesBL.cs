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
    public sealed class FASalesBL : Base
    {
        public FASalesBL()
        {
        }

        #region FASales
        public List<S_GLFASalesReffResult> FASalesForList(int _prmReqPage, int _prmPageSize, string _prmTransNmbr, string _prmCategory, string _prmKeyword)
        {
            List<S_GLFASalesReffResult> _result = new List<S_GLFASalesReffResult>();

            string _pattern1 = "";
            string _pattern2 = "";

            if (_prmCategory == "Code")
            {
                _pattern1 = _prmKeyword.Trim();
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = _prmKeyword.Trim();
            }

            try
            {
                var _query = (
                                from _glFASalesReff in this.db.S_GLFASalesReff(_pattern1, _pattern2)
                                where !(
                                    from _glFASalesDt in this.db.GLFASalesDts
                                    where _glFASalesDt.FACode == _glFASalesReff.FACode && _glFASalesDt.TransNmbr == _prmTransNmbr
                                    select _glFASalesDt.FACode
                                ).Contains(_glFASalesReff.FACode)
                                select new
                                {
                                    FACode = _glFASalesReff.FACode,
                                    FAName = _glFASalesReff.FAName,
                                    AmountCurr = _glFASalesReff.AmountCurr,
                                    AmountForex = _glFASalesReff.AmountForex,
                                    AmountHome = _glFASalesReff.AmountHome,
                                    BuyDate = _glFASalesReff.BuyDate,
                                    FAOwner = _glFASalesReff.FAOwner,
                                    FAStatus = _glFASalesReff.FAStatus,
                                    FASubGroup = _glFASalesReff.FASubGroup
                                }
                                //select _glFASalesReff
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                //IList<S_GLFASalesReffResult> _listGLFASales = _query.ToList();

                //foreach (var _item in _listGLFASales)
                //{
                //    _item.FACode
                //}

                foreach (var _item in _query)
                {
                    S_GLFASalesReffResult _reff = new S_GLFASalesReffResult();

                    _reff.FACode = _item.FACode;
                    _reff.FAName = _item.FAName;
                    _reff.AmountCurr = _item.AmountCurr;
                    _reff.AmountForex = _item.AmountForex;
                    _reff.AmountHome = _item.AmountHome;
                    _reff.BuyDate = _item.BuyDate;
                    _reff.FAOwner = _item.FAOwner;
                    _reff.FAStatus = _item.FAStatus;
                    _reff.FASubGroup = _item.FASubGroup;

                    _result.Add(_reff);
                }
            }

            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFixedAsset> FASalesForList(string _prmTransNmbr)
        {
            List<MsFixedAsset> _result = new List<MsFixedAsset>();

            try
            {
                var _query = (
                                from _glFASalesReff in this.db.S_GLFASalesReff(this._string, this._string)
                                where !(
                                    from _glFASalesDt in this.db.GLFASalesDts
                                    where _glFASalesDt.FACode == _glFASalesReff.FACode && _glFASalesDt.TransNmbr == _prmTransNmbr
                                    select _glFASalesDt.FACode
                                ).Contains(_glFASalesReff.FACode)
                                select new
                                {
                                    FACode = _glFASalesReff.FACode
                                }
                            );

                foreach (var _item in _query)
                {
                    MsFixedAsset _sales = new MsFixedAsset();

                    _sales.FACode = _item.FACode;

                    _result.Add(_sales);
                }
            }

            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<S_GLFASalesReffResult> FASales(string _prmTransNmbr)
        //{
        //    List<S_GLFASalesReffResult> _result = new List<S_GLFASalesReffResult>();

        //    try
        //    {
        //        var _query = (
        //                        from _glFASalesReff in this.db.S_GLFASalesReff(this._string, this._string)
        //                        where !(
        //                            from _glFASalesDt in this.db.GLFASalesDts
        //                            where _glFASalesDt.FACode == _glFASalesReff.FACode && _glFASalesDt.TransNmbr == _prmTransNmbr
        //                            select _glFASalesDt.FACode
        //                        ).Contains(_glFASalesReff.FACode)

        //                        select new
        //                        {
        //                            _glFASalesReff.FACode,
        //                            _glFASalesReff.FAName,
        //                            _glFASalesReff.AmountCurr,
        //                            _glFASalesReff.AmountForex,
        //                            _glFASalesReff.AmountHome,
        //                            _glFASalesReff.BuyDate,
        //                            _glFASalesReff.FAOwner,
        //                            _glFASalesReff.FAStatus,
        //                            _glFASalesReff.FASubGroup
        //                        }
        //                    );

        //        foreach (var _item in _query)
        //        {
        //            S_GLFASalesReffResult _reff = new S_GLFASalesReffResult();

        //            _reff.FACode = _item.FACode;
        //            _reff.FAName = _item.FAName;
        //            _reff.AmountCurr = _item.AmountCurr;
        //            _reff.AmountForex = _item.AmountForex;
        //            _reff.AmountHome = _item.AmountHome;
        //            _reff.BuyDate = _item.BuyDate;
        //            _reff.FAOwner = _item.FAOwner;
        //            _reff.FAStatus = _item.FAStatus;
        //            _reff.FASubGroup = _item.FASubGroup;

        //            _result.Add(_reff);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public int FASales(string _prmTransNmbr, string _prmCategory, string _prmKeyword)
        {
            int _result = 0;

            string _pattern1 = "";
            string _pattern2 = "";

            if (_prmCategory == "Code")
            {
                _pattern1 = _prmKeyword.Trim();
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = _prmKeyword.Trim();
            }

            try
            {
                var _query = (
                                from _glFASalesReff in this.db.S_GLFASalesReff(_pattern1, _pattern2)
                                where !(
                                    from _glFASalesDt in this.db.GLFASalesDts
                                    where _glFASalesDt.FACode == _glFASalesReff.FACode && _glFASalesDt.TransNmbr == _prmTransNmbr
                                    select _glFASalesDt.FACode
                                ).Contains(_glFASalesReff.FACode)

                                select new
                                {
                                    _glFASalesReff.FACode
                                }
                            ).Count();

                _result = _query;

                //foreach (var _item in _query)
                //{
                //    S_GLFASalesReffResult _reff = new S_GLFASalesReffResult();

                //    _reff.FACode = _item.FACode;
                //    _reff.FAName = _item.FAName;
                //    _reff.AmountCurr = _item.AmountCurr;
                //    _reff.AmountForex = _item.AmountForex;
                //    _reff.AmountHome = _item.AmountHome;
                //    _reff.BuyDate = _item.BuyDate;
                //    _reff.FAOwner = _item.FAOwner;
                //    _reff.FAStatus = _item.FAStatus;
                //    _reff.FASubGroup = _item.FASubGroup;

                //    _result.Add(_reff);
                //}
            }

            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLFASalesDt> AddList(string[] _prmCode, string _prmTransNmbr)
        {
            List<GLFASalesDt> _result = new List<GLFASalesDt>();

            try
            {
                foreach (S_GLFASalesReffResult _item in this.db.S_GLFASalesReff(this._string, this._string))
                {
                    GLFASalesDt _glFASalesDt = new GLFASalesDt();

                    _glFASalesDt.TransNmbr = _prmTransNmbr;
                    _glFASalesDt.FACode = Convert.ToString(_item.FACode);
                    _glFASalesDt.FAName = Convert.ToString(_item.FAName);
                    _glFASalesDt.AmountCurrent = Convert.ToDecimal(_item.AmountCurr);
                    _glFASalesDt.AmountForex = Convert.ToDecimal(_item.AmountForex);
                    _glFASalesDt.AmountHome = Convert.ToDecimal(_item.AmountHome);

                    _result.Add(_glFASalesDt);
                }

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFASalesDt _glFASalesDt = new GLFASalesDt();
                    GLFASalesHd _glFASalesHd = new GLFASalesHd();

                    decimal _total = 0;

                    _glFASalesDt.FACode = _prmCode[i];

                    var _query = (
                                from _glFASalesDt2 in this.db.GLFASalesDts
                                where _glFASalesDt2.TransNmbr == _prmTransNmbr && _glFASalesDt.FACode == _prmCode[i]
                                group _glFASalesDt2 by _glFASalesDt2.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                    foreach (var _obj in _query)
                    {
                        _total = _obj.AmountForex;
                    }

                    _glFASalesHd = this.db.GLFASalesHds.Single(_fa => _fa.TransNmbr == _prmTransNmbr);

                    _glFASalesHd.BaseForex = _total;
                    _glFASalesHd.PPNForex = (_glFASalesHd.BaseForex - _glFASalesHd.DiscForex) * _glFASalesHd.PPN / 100;
                    _glFASalesHd.TotalForex = _glFASalesHd.BaseForex - _glFASalesHd.DiscForex + _glFASalesHd.PPNForex;

                    foreach (var _item in _result)
                    {
                        if (_glFASalesDt.FACode == _item.FACode)
                        {
                            this.db.GLFASalesDts.InsertOnSubmit(_item);
                        }
                    }

                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int RowsCountFASales(string _prmTransNmbr, string _prmCategory, string _prmKeyword)
        {
            int _result = 0;

            _result = this.FASales(_prmTransNmbr, _prmCategory, _prmKeyword);

            return _result;
        }

        public int RowsCountFASalesDt(string _prmTransNmbr)
        {
            int _result = 0;

            _result = this.db.GLFASalesDts.Where(_row => _row.TransNmbr == _prmTransNmbr).Count();

            return _result;
        }

        public bool EditFASalesDt(GLFASalesDt _prmGLFASalesDt)
        {
            bool _result = false;

            GLFASalesHd _glFASalesHd = new GLFASalesHd();

            decimal _total = 0;

            try
            {
                var _query = (
                                from _glFASalesDt in this.db.GLFASalesDts
                                where !(
                                            from _glFASalesDt2 in this.db.GLFASalesDts
                                            where _glFASalesDt2.FACode == _prmGLFASalesDt.FACode && _glFASalesDt2.TransNmbr == _prmGLFASalesDt.TransNmbr
                                            select _glFASalesDt2.FACode
                                        ).Contains(_glFASalesDt.FACode)
                                        && _glFASalesDt.TransNmbr == _prmGLFASalesDt.TransNmbr
                                group _glFASalesDt by _glFASalesDt.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }


                _glFASalesHd = this.db.GLFASalesHds.Single(_fa => _fa.TransNmbr == _prmGLFASalesDt.TransNmbr);

                _glFASalesHd.BaseForex = _total + _prmGLFASalesDt.AmountForex;
                _glFASalesHd.PPNForex = (_glFASalesHd.BaseForex - _glFASalesHd.DiscForex) * _glFASalesHd.PPN / 100;
                _glFASalesHd.TotalForex = _glFASalesHd.BaseForex - _glFASalesHd.DiscForex + _glFASalesHd.PPNForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleGLFASalesHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFASalesHd _glFADevaluationHd = this.db.GLFASalesHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

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

        public GLFASalesDt GetSingleFASalesDt(string _prmTransNmbr, string _prmFACode)
        {
            GLFASalesDt _result = null;

            try
            {
                _result = this.db.GLFASalesDts.Single(_fa => _fa.TransNmbr == _prmTransNmbr && _fa.FACode == _prmFACode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLFASalesDt> GetListFASalesDt(int _prmReqPage, int _prmPageSize, string _prmTransNmbr, string _prmCategory, string _prmKeyword)
        {
            List<GLFASalesDt> _result = new List<GLFASalesDt>();

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
                var _query =
                            (
                                from _faSalesDt in this.db.GLFASalesDts
                                join _fixedAsset in this.db.MsFixedAssets
                                on _faSalesDt.FACode equals _fixedAsset.FACode
                                where _faSalesDt.TransNmbr == _prmTransNmbr
                                    && (SqlMethods.Like(_faSalesDt.FACode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_fixedAsset.FAName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    FACode = _faSalesDt.FACode,
                                    FAName = _fixedAsset.FAName,
                                    AmountForex = _faSalesDt.AmountForex,
                                    AmountHome = _faSalesDt.AmountHome,
                                    AmountCurrent = _faSalesDt.AmountCurrent
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FACode = this._string, FAName = this._string, AmountForex = this._decimal, AmountHome = this._decimal, AmountCurrent = this._decimal });

                    _result.Add(new GLFASalesDt(_row.FACode, _row.FAName, _row.AmountForex, _row.AmountHome, _row.AmountCurrent));
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
                this.db.S_GLFASalesGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FASales);
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
                    this.db.S_GLFASalesApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        GLFASalesHd _glFASalesHd = this.GetSingleFASalesHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_glFASalesHd.TransDate.Year, _glFASalesHd.TransDate.Month, AppModule.GetValue(TransactionType.FASales), this._companyTag, ""))
                        {
                            _glFASalesHd.FileNmbr = _item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FASales);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleFASalesHd(_prmTransNmbr).FileNmbr;
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

                GLFASalesHd _glFASalesHd = this.db.GLFASalesHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFASalesHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFASalesPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FASales);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleFASalesHd(_prmTransNmbr).FileNmbr;
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

                GLFASalesHd _glFASalesHd = this.db.GLFASalesHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFASalesHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFASalesUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.FASales);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleFASalesHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleFASalesHd(_prmTransNmbr).TransDate;
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

        public double RowsCountFASalesHd(string _prmCategory, string _prmKeyword)
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
                _pattern2 = "%%";
                _pattern1 = "%%";

            }
            else if (_prmCategory == "CustName")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }

            var _query =
                        (
                            from _glFASalesHd in this.db.GLFASalesHds
                            join _msCustomer in this.db.MsCustomers
                                on _glFASalesHd.CustCode equals _msCustomer.CustCode
                            where (SqlMethods.Like(_glFASalesHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_glFASalesHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && (_glFASalesHd.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted))  
                            select _glFASalesHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool EditFASalesHd(GLFASalesHd _prmGLFASalesHd)
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

        public string AddFASalesHd(GLFASalesHd _prmGLFASalesHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_prmGLFASalesHd.TransDate.Year, _prmGLFASalesHd.TransDate.Month, AppModule.GetValue(TransactionType.FASales), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmGLFASalesHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.GLFASalesHds.InsertOnSubmit(_prmGLFASalesHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmGLFASalesHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFASalesHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFASalesHd _glFASalesHd = this.db.GLFASalesHds.Single(_fa => _fa.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFASalesHd != null)
                    {
                        if ((_glFASalesHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLFASalesDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLFASalesDts.DeleteAllOnSubmit(_query);

                            this.db.GLFASalesHds.DeleteOnSubmit(_glFASalesHd);

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

        public bool DeleteMultiApproveFASalesHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFASalesHd _glFASalesHd = this.db.GLFASalesHds.Single(_fa => _fa.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFASalesHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _glFASalesHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _glFASalesHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_glFASalesHd != null)
                    {
                        if ((_glFASalesHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLFASalesDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLFASalesDts.DeleteAllOnSubmit(_query);

                            this.db.GLFASalesHds.DeleteOnSubmit(_glFASalesHd);

                            _result = true;
                        }
                        else if (_glFASalesHd.FileNmbr != "" && _glFASalesHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _glFASalesHd.Status = TransactionDataMapper.GetStatus(TransStatus.Deleted);
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

        public bool DeleteMultiFASalesDt(string[] _prmCode, string _prmTransNmbr)
        {
            bool _result = false;

            GLFASalesHd _glFASalesHd = new GLFASalesHd();

            decimal _total = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFASalesDt _glFASalesDt = this.db.GLFASalesDts.Single(_fa => _fa.FACode.Trim().ToLower() == _prmCode[i].Trim().ToLower() && _fa.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());

                    this.db.GLFASalesDts.DeleteOnSubmit(_glFASalesDt);
                }

                var _query = (
                                from _glFASalesDt2 in this.db.GLFASalesDts
                                where !(
                                            from _code in _prmCode
                                            select _code
                                        ).Contains(_glFASalesDt2.FACode)
                                        && _glFASalesDt2.TransNmbr == _prmTransNmbr
                                group _glFASalesDt2 by _glFASalesDt2.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _glFASalesHd = this.db.GLFASalesHds.Single(_fa => _fa.TransNmbr == _prmTransNmbr);

                _glFASalesHd.BaseForex = _total;
                _glFASalesHd.PPNForex = (_glFASalesHd.BaseForex - _glFASalesHd.DiscForex) * _glFASalesHd.PPN / 100;
                _glFASalesHd.TotalForex = _glFASalesHd.BaseForex - _glFASalesHd.DiscForex + _glFASalesHd.PPNForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteAllFASalesDt(string _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _glFASalesDt in this.db.GLFASalesDts
                                where _glFASalesDt.TransNmbr == _prmTransNmbr
                                select _glFASalesDt
                              );

                this.db.GLFASalesDts.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFASalesHd GetSingleFASalesHd(string _prmTransNmbr)
        {
            GLFASalesHd _result = null;

            try
            {
                _result = this.db.GLFASalesHds.Single(_fa => _fa.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFASalesHd GetSingleFASalesHdView(string _prmTransNmbr)
        {
            GLFASalesHd _result = new GLFASalesHd();

            try
            {
                var _query =
                            (
                                from _faSalesHd in this.db.GLFASalesHds
                                join _msCustomer in this.db.MsCustomers
                                on _faSalesHd.CustCode equals _msCustomer.CustCode
                                join _msTerm in this.db.MsTerms
                                on _faSalesHd.Term equals _msTerm.TermCode
                                where _faSalesHd.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    TransNmbr = _faSalesHd.TransNmbr,
                                    FileNmbr = _faSalesHd.FileNmbr,
                                    TransDate = _faSalesHd.TransDate,
                                    Status = _faSalesHd.Status,
                                    Attn = _faSalesHd.Attn,
                                    BaseForex = _faSalesHd.BaseForex,
                                    DiscForex = _faSalesHd.DiscForex,
                                    CustCode = _faSalesHd.CustCode,
                                    CustName = _msCustomer.CustName,
                                    CurrCode = _faSalesHd.CurrCode,
                                    Forexrate = _faSalesHd.Forexrate,
                                    PPN = _faSalesHd.PPN,
                                    PPNDate = _faSalesHd.PPNDate,
                                    PPNForex = _faSalesHd.PPNForex,
                                    PPNNo = _faSalesHd.PPNNo,
                                    PPNRate = _faSalesHd.PPNRate,
                                    Remark = _faSalesHd.Remark,
                                    TermName = _msTerm.TermName,
                                    TermCode = _faSalesHd.Term,
                                    TotalForex = _faSalesHd.TotalForex
                                }
                            ).Single();

                _result.TransNmbr = _query.TransNmbr;
                _result.FileNmbr = _query.FileNmbr;
                _result.TransDate = _query.TransDate;
                _result.Status = _query.Status;
                _result.Attn = _query.Attn;
                _result.BaseForex = _query.BaseForex;
                _result.DiscForex = _query.DiscForex;
                _result.CustCode = _query.CustCode;
                _result.CustName = _query.CustName;
                _result.CurrCode = _query.CurrCode;
                _result.Forexrate = _query.Forexrate;
                _result.PPN = _query.PPN;
                _result.PPNDate = _query.PPNDate;
                _result.PPNForex = _query.PPNForex;
                _result.PPNNo = _query.PPNNo;
                _result.PPNRate = _query.PPNRate;
                _result.Remark = _query.Remark;
                _result.Term = _query.TermCode;
                _result.TermName = _query.TermName;
                _result.TotalForex = _query.TotalForex;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLFASalesHd> GetListFASalesHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<GLFASalesHd> _result = new List<GLFASalesHd>();

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
                _pattern2 = "%%";
                _pattern1 = "%%";

            }
            else if (_prmCategory == "CustName")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }

            try
            {
                var _query =
                            (
                                from _glFASalesHd in this.db.GLFASalesHds
                                join _msCustomer in this.db.MsCustomers
                                on _glFASalesHd.CustCode equals _msCustomer.CustCode
                                join _msTerm in this.db.MsTerms
                                on _glFASalesHd.Term equals _msTerm.TermCode
                                where (SqlMethods.Like(_glFASalesHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_glFASalesHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (_glFASalesHd.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted))  
                                orderby _glFASalesHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _glFASalesHd.TransNmbr,
                                    FileNmbr = _glFASalesHd.FileNmbr,
                                    TransDate = _glFASalesHd.TransDate,
                                    Status = _glFASalesHd.Status,
                                    CustName = _msCustomer.CustName,
                                    CurrCode = _glFASalesHd.CurrCode,
                                    TermName = _msTerm.TermName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {

                    _result.Add(new GLFASalesHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.CustName, _row.CurrCode, _row.TermName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetForexRate(string _prmTransNmbr)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _glFASalesHd in this.db.GLFASalesHds
                                where _glFASalesHd.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    ForexRate = _glFASalesHd.Forexrate
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ForexRate;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~FASalesBL()
        {
        }
    }
}
