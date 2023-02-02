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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class StockChangeToFABL : Base
    {
        private StockIssueFixedAssetBL _stcIssueFABL = new StockIssueFixedAssetBL();

        public StockChangeToFABL()
        {

        }

        #region GLFAAddStockHd
        public double RowsCountFAAddStockHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNo")
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
                            from _glFAAddStockHd in this.db.GLFAAddStockHds
                            where (SqlMethods.Like(_glFAAddStockHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like((_glFAAddStockHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && _glFAAddStockHd.Status != StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Deleted)
                            select _glFAAddStockHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<GLFAAddStockHd> GetListFAAddStockHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<GLFAAddStockHd> _result = new List<GLFAAddStockHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNo")
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
                var _query1 =
                            (
                                from _glFAAddStockHd in this.db.GLFAAddStockHds
                                where (SqlMethods.Like(_glFAAddStockHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_glFAAddStockHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _glFAAddStockHd.Status != StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _glFAAddStockHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _glFAAddStockHd.TransNmbr,
                                    FileNmbr = _glFAAddStockHd.FileNmbr,
                                    TransDate = _glFAAddStockHd.TransDate,
                                    Status = _glFAAddStockHd.Status,
                                    WISNo = _glFAAddStockHd.WISNo,
                                    FileNoWIS = (
                                                    from _stcIssueToFA in this.db.STCIssueToFAHds
                                                    where _stcIssueToFA.TransNmbr == _glFAAddStockHd.WISNo
                                                    select _stcIssueToFA.FileNmbr
                                                ).FirstOrDefault(),
                                    ReceiveBy = _glFAAddStockHd.ReceiveBy
                                }
                            );

                if (_prmOrderBy == "Trans No.")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TransNmbr)) : (_query1.OrderByDescending(a => a.TransNmbr));
                if (_prmOrderBy == "File No.")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FileNmbr)) : (_query1.OrderByDescending(a => a.FileNmbr));
                if (_prmOrderBy == "Trans Date")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TransDate)) : (_query1.OrderByDescending(a => a.TransDate));
                if (_prmOrderBy == "Status")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Status)) : (_query1.OrderByDescending(a => a.Status));
                if (_prmOrderBy == "WIS No.")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.WISNo)) : (_query1.OrderByDescending(a => a.WISNo));
                if (_prmOrderBy == "Received By")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.ReceiveBy)) : (_query1.OrderByDescending(a => a.ReceiveBy));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GLFAAddStockHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.WISNo, _row.FileNoWIS, _row.ReceiveBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFAAddStockHd GetSingleFAAddStockHd(string _prmCode)
        {
            GLFAAddStockHd _result = null;

            try
            {
                _result = this.db.GLFAAddStockHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFAAddStockHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAAddStockHd _glFAAddStockHd = this.db.GLFAAddStockHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAAddStockHd != null)
                    {
                        if ((_glFAAddStockHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLFAAddStockDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            foreach (var _row in _query)
                            {
                                var _query2 = (from _detail2 in this.db.GLFAAddStockDt2s
                                               where _detail2.GLFAAddStockDtCode == _row.GLFAAddStockDtCode
                                               select _detail2);

                                this.db.GLFAAddStockDt2s.DeleteAllOnSubmit(_query2);

                                var _query3 = (from _detail3 in this.db.GLFAAddStockDtExtensions
                                               where _detail3.GLFAAddStockDtCode == _row.GLFAAddStockDtCode
                                               select _detail3);

                                this.db.GLFAAddStockDtExtensions.DeleteAllOnSubmit(_query3);
                            }

                            this.db.GLFAAddStockDts.DeleteAllOnSubmit(_query);

                            this.db.GLFAAddStockHds.DeleteOnSubmit(_glFAAddStockHd);

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

        public bool GetSingleGLFAAddStockHdForAccounting(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAAddStockHd _glFAAddStockHd = this.db.GLFAAddStockHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAAddStockHd != null)
                    {
                        if (_glFAAddStockHd.Status != StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFAAddStockHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAAddStockHd _glFAAddStockHd = this.db.GLFAAddStockHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAAddStockHd.Status == StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _glFAAddStockHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _glFAAddStockHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_glFAAddStockHd != null)
                    {
                        if ((_glFAAddStockHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLFAAddStockDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            foreach (var _row in _query)
                            {
                                var _query2 = (from _detail2 in this.db.GLFAAddStockDt2s
                                               where _detail2.GLFAAddStockDtCode == _row.GLFAAddStockDtCode
                                               select _detail2);

                                this.db.GLFAAddStockDt2s.DeleteAllOnSubmit(_query2);

                                var _query3 = (from _detail3 in this.db.GLFAAddStockDtExtensions
                                               where _detail3.GLFAAddStockDtCode == _row.GLFAAddStockDtCode
                                               select _detail3);

                                this.db.GLFAAddStockDtExtensions.DeleteAllOnSubmit(_query3);
                            }

                            this.db.GLFAAddStockDts.DeleteAllOnSubmit(_query);

                            this.db.GLFAAddStockHds.DeleteOnSubmit(_glFAAddStockHd);

                            _result = true;
                        }
                        else if (_glFAAddStockHd.FileNmbr != "" && _glFAAddStockHd.Status == StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _glFAAddStockHd.Status = StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddFAAddStockHd(GLFAAddStockHd _prmGLFAAddStockHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmGLFAAddStockHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                this.db.GLFAAddStockHds.InsertOnSubmit(_prmGLFAAddStockHd);

                List<STCIssueToFADt> _stcIssuetoFADt = _stcIssueFABL.GetListFAAddStockDtFromSTCIssueToFADt(_prmGLFAAddStockHd.WISNo);

                FixedAssetsBL _faBL = new FixedAssetsBL();

                foreach (var _row in _stcIssuetoFADt)
                {
                    for (int i = 0; i < Convert.ToInt32(_row.Qty); i++)
                    {
                        GLFAAddStockDt _glFAAddStockDt = new GLFAAddStockDt();

                        _glFAAddStockDt.GLFAAddStockDtCode = Guid.NewGuid();
                        _glFAAddStockDt.TransNmbr = _prmGLFAAddStockHd.TransNmbr;
                        _glFAAddStockDt.ProductCode = _row.ProductCode;
                        _glFAAddStockDt.LocationCode = _row.LocationCode;

                        this.db.GLFAAddStockDts.InsertOnSubmit(_glFAAddStockDt);

                        GLFAAddStockDtExtension _glFAAddStockDtExt = new GLFAAddStockDtExtension();

                        _glFAAddStockDtExt.GLFAAddStockDtCode = _glFAAddStockDt.GLFAAddStockDtCode;

                        this.db.GLFAAddStockDtExtensions.InsertOnSubmit(_glFAAddStockDtExt);
                    }
                }

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmGLFAAddStockHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFAAddStockHd(GLFAAddStockHd _prmGLFAAddStockHd)
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
                this.db.S_GLFAAddStockGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FAAddStock);
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
                    this.db.S_GLFAAddStockApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        GLFAAddStockHd _glFAAddStockHd = this.GetSingleFAAddStockHd(_prmCode);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_glFAAddStockHd.TransDate.Year, _glFAAddStockHd.TransDate.Month, AppModule.GetValue(TransactionType.FAAddStock), this._companyTag, ""))
                        {
                            _glFAAddStockHd.FileNmbr = _item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FAAddStock);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleFAAddStockHd(_prmCode).FileNmbr;
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

        public string Posting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLFAAddStockHd _glFAAddStockHd = this.db.GLFAAddStockHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFAAddStockHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFAAddStockPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FAAddStock);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleFAAddStockHd(_prmCode).FileNmbr;
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

                GLFAAddStockHd _glFAAddStockHd = this.db.GLFAAddStockHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFAAddStockHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFAAddStockUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.FAAddStock);
                        //_transActivity.TransNmbr = _prmCode.ToString();
                        //_transActivity.FileNmbr = this.GetSingleFAAddStockHd(_prmCode).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleFAAddStockHd(_prmCode).TransDate;
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
        #endregion

        #region GLFAAddStockDt

        public int RowsCountFAAddStockDt(string _prmTransNmbr)
        {
            int _result = 0;

            _result = this.db.GLFAAddStockDts.Where(_row => _row.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()).Count();

            return _result;
        }

        public List<GLFAAddStockDt> GetListFAAddStockDt(int _prmReqPage, int _prmPageSize, string _prmCode, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<GLFAAddStockDt> _result = new List<GLFAAddStockDt>();

            try
            {
                var _query1 =
                            (
                                from _faAddStockDt in this.db.GLFAAddStockDts
                                where _faAddStockDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _faAddStockDt.ProductCode ascending
                                select new
                                {
                                    GLFAAddStockDtCode = _faAddStockDt.GLFAAddStockDtCode,
                                    TransNmbr = _faAddStockDt.TransNmbr,
                                    ProductCode = _faAddStockDt.ProductCode,
                                    ProductName = (
                                                        from _msProduct in this.db.MsProducts
                                                        where _msProduct.ProductCode == _faAddStockDt.ProductCode
                                                        select _msProduct.ProductName
                                                    ).FirstOrDefault(),
                                    LocationCode = _faAddStockDt.LocationCode,
                                    LocationName = (
                                                        from _msWrhsLoc in this.db.MsWrhsLocations
                                                        where _msWrhsLoc.WLocationCode == _faAddStockDt.LocationCode
                                                        select _msWrhsLoc.WLocationName
                                                    ).FirstOrDefault(),
                                    FACode = (
                                                    from _faAddStockDt2a in this.db.GLFAAddStockDt2s
                                                    where _faAddStockDt.GLFAAddStockDtCode == _faAddStockDt2a.GLFAAddStockDtCode
                                                    select _faAddStockDt2a.FACode
                                                ).FirstOrDefault(),
                                    FAName = (
                                                    from _faAddStockDt2b in this.db.GLFAAddStockDt2s
                                                    where _faAddStockDt.GLFAAddStockDtCode == _faAddStockDt2b.GLFAAddStockDtCode
                                                    select _faAddStockDt2b.FAName
                                                ).FirstOrDefault(),
                                    FAStatus = (
                                                    from _faAddStockDt2c in this.db.GLFAAddStockDt2s
                                                    where _faAddStockDt.GLFAAddStockDtCode == _faAddStockDt2c.GLFAAddStockDtCode
                                                    select _faAddStockDt2c.FAStatus
                                                ).FirstOrDefault(),
                                    FAStatusName = (
                                                        from _msFAStatus in this.db.MsFAStatus
                                                        join _faAddStockDt2 in this.db.GLFAAddStockDt2s
                                                            on _msFAStatus.FAStatusCode equals _faAddStockDt2.FAStatus
                                                        where _faAddStockDt.GLFAAddStockDtCode == _faAddStockDt2.GLFAAddStockDtCode
                                                        select _msFAStatus.FAStatusName
                                                    ).FirstOrDefault(),
                                    FAOwner = (
                                                    (
                                                        (
                                                            from _faAddStockDt2d in this.db.GLFAAddStockDt2s
                                                            where _faAddStockDt.GLFAAddStockDtCode == _faAddStockDt2d.GLFAAddStockDtCode
                                                            select _faAddStockDt2d.FAOwner
                                                         ).FirstOrDefault() == null
                                                    ) ? 'N' :
                                                    (from _faAddStockDt2d in this.db.GLFAAddStockDt2s
                                                     where _faAddStockDt.GLFAAddStockDtCode == _faAddStockDt2d.GLFAAddStockDtCode
                                                     select _faAddStockDt2d.FAOwner).FirstOrDefault()
                                                ),
                                    FASubGroup = (
                                                    from _faAddStockDt2e in this.db.GLFAAddStockDt2s
                                                    where _faAddStockDt.GLFAAddStockDtCode == _faAddStockDt2e.GLFAAddStockDtCode
                                                    select _faAddStockDt2e.FASubGroup
                                                ).FirstOrDefault(),
                                    FASubGroupName = (
                                                        from _msFAGroupSub in this.db.MsFAGroupSubs
                                                        join _faAddStockDt2f in this.db.GLFAAddStockDt2s
                                                            on _msFAGroupSub.FASubGrpCode equals _faAddStockDt2f.FASubGroup
                                                        where _faAddStockDt.GLFAAddStockDtCode == _faAddStockDt2f.GLFAAddStockDtCode
                                                        select _msFAGroupSub.FASubGrpName
                                                    ).FirstOrDefault()
                                }
                            );

                if (_prmOrderBy == "Product")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.ProductCode)) : (_query1.OrderByDescending(a => a.ProductCode));
                if (_prmOrderBy == "Location")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.LocationName)) : (_query1.OrderByDescending(a => a.LocationName));
                if (_prmOrderBy == "FA Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAName)) : (_query1.OrderByDescending(a => a.FAName));
                if (_prmOrderBy == "FA Condition")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAStatus)) : (_query1.OrderByDescending(a => a.FAStatus));
                if (_prmOrderBy == "FA Owner")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAOwner)) : (_query1.OrderByDescending(a => a.FAOwner));
                if (_prmOrderBy == "FA SubGroup")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FASubGroup)) : (_query1.OrderByDescending(a => a.FASubGroup));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GLFAAddStockDt(_row.GLFAAddStockDtCode, _row.TransNmbr, _row.ProductCode, _row.ProductName, _row.LocationCode, _row.LocationName, _row.FACode, _row.FAName, _row.FAStatus, _row.FAStatusName, _row.FAOwner, _row.FASubGroup, _row.FASubGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFAAddStockDt GetSingleFAAddStockDt(Guid _prmCode)
        {
            GLFAAddStockDt _result = null;

            try
            {
                _result = this.db.GLFAAddStockDts.Single(_temp => _temp.GLFAAddStockDtCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFAAddStockDt(GLFAAddStockDt _prmGLFAAddStockDt)
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

        #region GLFAAddStockDt2
        public bool AddFAAddStockDt2(GLFAAddStockDt2 _prmGLFAAddStockDt2)
        {
            bool _result = false;

            try
            {
                this.db.GLFAAddStockDt2s.InsertOnSubmit(_prmGLFAAddStockDt2);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFAAddStockDt2 GetSingleFAAddStockDt2(Guid _prmCode)
        {
            GLFAAddStockDt2 _result = null;

            try
            {
                _result = this.db.GLFAAddStockDt2s.Single(_temp => _temp.GLFAAddStockDtCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFAAddStockDt2(GLFAAddStockDt2 _prmGLFAAddStockDt2)
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

        public List<GLFAAddStockDt2> GetListFAAddStockDt2(Guid _prmCode)
        {
            List<GLFAAddStockDt2> _result = new List<GLFAAddStockDt2>();

            try
            {
                var _query =
                            (
                                from _faAddStockDt2 in this.db.GLFAAddStockDt2s
                                where _faAddStockDt2.GLFAAddStockDtCode == _prmCode
                                select new
                                {
                                    GLFAAddStockDtCode = _faAddStockDt2.GLFAAddStockDtCode,
                                    FACode = _faAddStockDt2.FACode,
                                    FAName = _faAddStockDt2.FAName,
                                    FAStatus = _faAddStockDt2.FAStatus,
                                    FAStatusName = (
                                                        from _msFAStatus in this.db.MsFAStatus
                                                        where _msFAStatus.FAStatusCode == _faAddStockDt2.FAStatus
                                                        select _msFAStatus.FAStatusName
                                                    ).FirstOrDefault(),
                                    FAOwner = _faAddStockDt2.FAOwner,
                                    FASubGroup = _faAddStockDt2.FASubGroup,
                                    FASubGroupName = (
                                                        from _msFAGroupSub in this.db.MsFAGroupSubs
                                                        where _msFAGroupSub.FASubGrpCode == _faAddStockDt2.FASubGroup
                                                        select _msFAGroupSub.FASubGrpName
                                                    ).FirstOrDefault()
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new GLFAAddStockDt2(_row.GLFAAddStockDtCode, _row.FACode, _row.FAName, _row.FAStatus, _row.FAStatusName, _row.FAOwner, _row.FASubGroup, _row.FASubGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~StockChangeToFABL()
        {

        }
    }
}
