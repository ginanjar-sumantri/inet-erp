using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Sales
{
    public sealed class DeliveryOrderBL : Base
    {
        public DeliveryOrderBL()
        {
        }

        #region MKTDOHd
        public double RowsCountMKTDOHd(string _prmCategory, string _prmKeyword)
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
            else if (_prmCategory == "CustName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            var _query =
                        (
                            from _mktDOHd in this.db.MKTDOHds
                            join _msCustomer in this.db.MsCustomers
                                on _mktDOHd.CustCode equals _msCustomer.CustCode
                            where (SqlMethods.Like(_mktDOHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_mktDOHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _mktDOHd.Status != DeliveryOrderDataMapper.GetStatus(TransStatus.Deleted)
                            select _mktDOHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MKTDOHd> GetListMKTDOHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MKTDOHd> _result = new List<MKTDOHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "CustName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _mktDOHd in this.db.MKTDOHds
                                join _msCustomer in this.db.MsCustomers
                                    on _mktDOHd.CustCode equals _msCustomer.CustCode
                                where (SqlMethods.Like(_mktDOHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_mktDOHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _mktDOHd.Status != DeliveryOrderDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _mktDOHd.TransDate descending
                                select new
                                {
                                    TransNmbr = _mktDOHd.TransNmbr,
                                    FileNmbr = _mktDOHd.FileNmbr,
                                    TransDate = _mktDOHd.TransDate,
                                    Status = _mktDOHd.Status,
                                    CustCode = _mktDOHd.CustCode,
                                    SONo = _mktDOHd.SONo,
                                    POCustNo = _mktDOHd.POCustNo
                                }
                            );

                if (_prmOrderBy == "Trans No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.TransNmbr)) : (_query.OrderByDescending(a => a.TransNmbr));

                if (_prmOrderBy == "File No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.FileNmbr)) : (_query.OrderByDescending(a => a.FileNmbr));

                if (_prmOrderBy == "Trans Date")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.TransDate)) : (_query.OrderByDescending(a => a.TransDate));

                if (_prmOrderBy == "Status")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.Status)) : (_query.OrderByDescending(a => a.Status));

                if (_prmOrderBy == "Customer")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.CustCode)) : (_query.OrderByDescending(a => a.CustCode));

                if (_prmOrderBy == "SO No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.SONo)) : (_query.OrderByDescending(a => a.SONo));

                if (_prmOrderBy == "Customer PO No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.POCustNo)) : (_query.OrderByDescending(a => a.POCustNo));

                _query = _query.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MKTDOHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.CustCode, _row.SONo, _row.POCustNo));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MKTDOHd GetSingleMKTDOHd(string _prmCode)
        {
            MKTDOHd _result = null;

            try
            {
                _result = this.db.MKTDOHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiMKTDOHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MKTDOHd _mktDOHd = this.db.MKTDOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_mktDOHd != null)
                    {
                        if ((_mktDOHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.MKTDODts
                                          where _detail.TransNmbr == _prmCode[i]
                                          select _detail);

                            this.db.MKTDODts.DeleteAllOnSubmit(_query);

                            this.db.MKTDOHds.DeleteOnSubmit(_mktDOHd);

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

        public string AddMKTDOHd(MKTDOHd _prmMKTDOHd, List<MKTDODt> _prmMKTDODt)
        {
            string _result = "";

            try
            {
                List<MKTDODt> _listDetail = new List<MKTDODt>();

                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmMKTDOHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.MKTDOHds.InsertOnSubmit(_prmMKTDOHd);

                if (_prmMKTDODt.Count > 0)
                {
                    foreach (MKTDODt _item in _prmMKTDODt)
                    {
                        MKTDODt _mktDODt = new MKTDODt();

                        _mktDODt.TransNmbr = _prmMKTDOHd.TransNmbr;
                        _mktDODt.ProductCode = _item.ProductCode;
                        _mktDODt.Qty = _item.Qty;
                        _mktDODt.Unit = _item.Unit;
                        _mktDODt.Remark = _item.Remark;
                        _mktDODt.DoneClosing = _item.DoneClosing;
                        _mktDODt.QtyClose = _item.QtyClose;
                        _mktDODt.QtySJ = _item.QtySJ;

                        _listDetail.Add(new MKTDODt(_mktDODt.TransNmbr, _mktDODt.ProductCode, _mktDODt.Qty, _mktDODt.Unit, _mktDODt.Remark, _mktDODt.DoneClosing, _mktDODt.QtyClose, _mktDODt.QtySJ));
                    }

                    this.db.MKTDODts.InsertAllOnSubmit(_listDetail);
                }

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmMKTDOHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditMKTDOHd(MKTDOHd _prmMKTDOHd)
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

        public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                int _success = this.db.S_MKDOGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.DeliveryOrder);
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
                    int _success = this.db.S_MKDOApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        MKTDOHd _mktDOHd = this.GetSingleMKTDOHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_mktDOHd.TransDate.Year, _mktDOHd.TransDate.Month, AppModule.GetValue(TransactionType.DeliveryOrder), this._companyTag, ""))
                        {
                            _mktDOHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.DeliveryOrder);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleMKTDOHd(_prmTransNmbr).FileNmbr;
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

                MKTDOHd _mktDOHd = this.db.MKTDOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_mktDOHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_MKDOPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.DeliveryOrder);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleMKTDOHd(_prmTransNmbr).FileNmbr;
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

                MKTDOHd _mktDOHd = this.db.MKTDOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_mktDOHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_MKDOUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";
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

        public string Closing(string _prmTransNmbr, string _prmProduct, string _prmRemark, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_MKDOClosing(_prmTransNmbr, _prmProduct, _prmRemark, _prmuser, ref _errorMsg);

                if (_errorMsg != "")
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Closing Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string GetFileNmbrMKTDOHd(string _prmTransNmbr)
        {
            string _result = "";

            try
            {
                _result = (this.db.MKTDOHds.Single(_temp => _temp.TransNmbr == _prmTransNmbr).FileNmbr ?? "").Trim();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleMKTDOHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MKTDOHd _mktDOHd = this.db.MKTDOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_mktDOHd != null)
                    {
                        if (_mktDOHd.Status != DeliveryOrderDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveMKTDOHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MKTDOHd _mktDOHd = this.db.MKTDOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_mktDOHd.Status == DeliveryOrderDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _mktDOHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _mktDOHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }                    

                    if (_mktDOHd != null)
                    {
                        if ((_mktDOHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.MKTDODts
                                          where _detail.TransNmbr == _prmCode[i]
                                          select _detail);

                            this.db.MKTDODts.DeleteAllOnSubmit(_query);

                            this.db.MKTDOHds.DeleteOnSubmit(_mktDOHd);

                            _result = true;
                        }
                        else if (_mktDOHd.FileNmbr != "" && _mktDOHd.Status == DeliveryOrderDataMapper.GetStatus(TransStatus.Approved))
                        {                            
                            _mktDOHd.Status = DeliveryOrderDataMapper.GetStatus(TransStatus.Deleted);
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

        #region MKTDODt
        public int RowsCountMKTDODt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _mktDODt in this.db.MKTDODts
                                 where _mktDODt.TransNmbr == _prmCode
                                 select _mktDODt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MKTDODt> GetListMKTDODt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<MKTDODt> _result = new List<MKTDODt>();

            try
            {
                var _query = (
                                from _mktDODt in this.db.MKTDODts
                                where _mktDODt.TransNmbr == _prmCode
                                orderby _mktDODt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _mktDODt.TransNmbr,
                                    ProductCode = _mktDODt.ProductCode,
                                    Qty = _mktDODt.Qty,
                                    Unit = _mktDODt.Unit,
                                    Remark = _mktDODt.Remark,
                                    DoneClosing = _mktDODt.DoneClosing,
                                    QtyClose = _mktDODt.QtyClose
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MKTDODt(_row.TransNmbr, _row.ProductCode, _row.Qty, _row.Unit, _row.Remark, _row.DoneClosing, _row.QtyClose));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MKTDODt> GetListMKTDODt(string _prmSONo)
        {
            List<MKTDODt> _result = new List<MKTDODt>();

            try
            {
                var _query = (
                                from _mktDODt in this.db.MKTDODts
                                join _mktDOHd in this.db.MKTDOHds
                                    on _mktDODt.TransNmbr equals _mktDOHd.TransNmbr
                                where _mktDOHd.SONo == _prmSONo
                                orderby _mktDODt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _mktDODt.TransNmbr,
                                    ProductCode = _mktDODt.ProductCode,
                                    Qty = _mktDODt.Qty,
                                    Unit = _mktDODt.Unit,
                                    Remark = _mktDODt.Remark,
                                    DoneClosing = _mktDODt.DoneClosing,
                                    QtyClose = _mktDODt.QtyClose
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MKTDODt(_row.TransNmbr, _row.ProductCode, _row.Qty, _row.Unit, _row.Remark, _row.DoneClosing, _row.QtyClose));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MKTDODt GetSingleMKTDODt(string _prmCode, string _prmProductCode)
        {
            MKTDODt _result = null;

            try
            {
                _result = this.db.MKTDODts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiMKTDODt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MKTDODt _MKTDODt = this.db.MKTDODts.Single(_temp => _temp.ProductCode == _prmCode[i] && _temp.TransNmbr == _prmTransNo);

                    this.db.MKTDODts.DeleteOnSubmit(_MKTDODt);
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

        public bool AddMKTDODt(MKTDODt _prmMKTDODt)
        {
            bool _result = false;

            try
            {
                this.db.MKTDODts.InsertOnSubmit(_prmMKTDODt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditMKTDODt(MKTDODt _prmMKTDODt)
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

        #region V_MKDOforSJ


        public List<MKTSOHd> GetListSONoForDDL(string _prmCustCode)
        {
            List<MKTSOHd> _result = new List<MKTSOHd>();

            try
            {
                var _query = (
                                from _mktSOHd in this.db.MKTSOHds
                                where _mktSOHd.CustCode == _prmCustCode
                                && ((_mktSOHd.FileNmbr ?? "") == _mktSOHd.FileNmbr)
                                orderby _mktSOHd.FileNmbr ascending
                                select new
                                {
                                    SO_No = _mktSOHd.TransNmbr,
                                    FileNmbr = _mktSOHd.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MKTSOHd(_row.SO_No, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MKTSOHd> GetListSONoForDDLInDO(string _prmCustCode)
        {
            List<MKTSOHd> _result = new List<MKTSOHd>();

            try
            {
                var _query = (
                                from _mktSOHd in this.db.MKTSOHds
                                join _mktSoDt in this.db.MKTSODts
                                    on _mktSOHd.TransNmbr equals _mktSoDt.TransNmbr
                                where _mktSOHd.CustCode == _prmCustCode
                                && ((_mktSOHd.FileNmbr ?? "") == _mktSOHd.FileNmbr)
                                && (_mktSoDt.Qty - _mktSoDt.QtyDO != 0)
                                orderby _mktSOHd.FileNmbr ascending
                                select new
                                {
                                    SO_No = _mktSOHd.TransNmbr,
                                    FileNmbr = _mktSOHd.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MKTSOHd(_row.SO_No, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public List<MsProduct> GetListProductForDDLForSJ(string _prmSONo)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _mkDOForSJ in this.db.V_MKDOForSJs
                                where _mkDOForSJ.SO_No == _prmSONo
                                select new
                                {
                                    ProductNo = _mkDOForSJ.Product_Code,
                                    ProductName = _mkDOForSJ.Product_Name
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductNo = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductNo, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MKTDOHd> GetListDoForDDLForSJ(string _prmSONo)
        {
            List<MKTDOHd> _result = new List<MKTDOHd>();

            try
            {
                var _query = (
                                from _mkDOForSJ in this.db.V_MKDOForSJs
                                where _mkDOForSJ.SO_No == _prmSONo
                                orderby _mkDOForSJ.FileNmbr ascending
                                select new
                                {
                                    DO_No = _mkDOForSJ.DO_No,
                                    FileNmbr = _mkDOForSJ.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MKTDOHd(_row.DO_No, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        #endregion

        ~DeliveryOrderBL()
        {
        }
    }
}
