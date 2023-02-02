using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Sales
{
    public sealed class SalesOrderBL : Base
    {
        public SalesOrderBL()
        {
        }

        #region MKTSOHd

        public double RowsCountMKTSOHd(string _prmCategory, string _prmKeyword)
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
                            from _mktSOHd in this.db.MKTSOHds
                            join _msCustomer in this.db.MsCustomers
                                on _mktSOHd.CustCode equals _msCustomer.CustCode
                            where _mktSOHd.Revisi == this.db.MKTSOHds.Where(_temp => _mktSOHd.TransNmbr == _temp.TransNmbr).Max(_temp2 => _temp2.Revisi)
                                 && (SqlMethods.Like(_mktSOHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                 && (SqlMethods.Like((_mktSOHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                 && _mktSOHd.Status != SalesOrderDataMapper.GetStatus(TransStatus.Deleted)
                            select _mktSOHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MKTSOHd> GetListMKTSOHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MKTSOHd> _result = new List<MKTSOHd>();

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
                                from _mktSOHd in this.db.MKTSOHds
                                join _msCustomer in this.db.MsCustomers
                                    on _mktSOHd.CustCode equals _msCustomer.CustCode
                                where _mktSOHd.Revisi == this.db.MKTSOHds.Where(_temp => _mktSOHd.TransNmbr == _temp.TransNmbr).Max(_temp2 => _temp2.Revisi)
                                     && (SqlMethods.Like(_mktSOHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                     && (SqlMethods.Like((_mktSOHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                     && _mktSOHd.Status != SalesOrderDataMapper.GetStatus(TransStatus.Deleted)
                                select new
                                {
                                    TransNmbr = _mktSOHd.TransNmbr,
                                    FileNmbr = _mktSOHd.FileNmbr,
                                    TransDate = _mktSOHd.TransDate,
                                    Revisi = _mktSOHd.Revisi,
                                    CurrCode = _mktSOHd.CurrCode,
                                    Status = _mktSOHd.Status,
                                    CustCode = _mktSOHd.CustCode,
                                    CustName = _mktSOHd.CustName,
                                    Term = _mktSOHd.Term,
                                    TermName = _mktSOHd.TermName,
                                    BillTo = _mktSOHd.BillTo,
                                    BillToName = _mktSOHd.BillToName
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

                if (_prmOrderBy == "Currency")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.CurrCode)) : (_query.OrderByDescending(a => a.CurrCode));

                if (_prmOrderBy == "Customer")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.CustCode)) : (_query.OrderByDescending(a => a.CustCode));

                if (_prmOrderBy == "Term")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.Term)) : (_query.OrderByDescending(a => a.Term));

                if (_prmOrderBy == "Bill To")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.BillTo)) : (_query.OrderByDescending(a => a.BillTo));

                _query = _query.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MKTSOHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Revisi, _row.Status, _row.CurrCode, _row.CustCode, _row.CustName, _row.Term, _row.TermName, _row.BillTo, _row.BillToName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MKTSOHd> GetListMKTSOForDDL()
        {
            List<MKTSOHd> _result = new List<MKTSOHd>();

            try
            {
                var _query = (
                                from _salesOrderHd in this.db.MKTSOHds
                                orderby _salesOrderHd.TransNmbr ascending
                                select new
                                {
                                    TransNmbr = _salesOrderHd.TransNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MKTSOHd(_row.TransNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MKTSOHd> GetListMKTSOForDDLByCustCode(string _prmCustCode)
        {
            List<MKTSOHd> _result = new List<MKTSOHd>();

            try
            {
                var _query = (
                                from _salesOrderHd in this.db.MKTSOHds
                                where _salesOrderHd.CustCode.Trim().ToLower() == _prmCustCode.Trim().ToLower() && _salesOrderHd.Status == SalesOrderDataMapper.GetStatus(TransStatus.Posted)
                                orderby _salesOrderHd.TransNmbr ascending
                                select new
                                {
                                    TransNmbr = _salesOrderHd.TransNmbr,
                                    FileNmbr = _salesOrderHd.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MKTSOHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MKTSOHd> GetListRevisiForDDL(string _prmTransNmbr)
        {
            List<MKTSOHd> _result = new List<MKTSOHd>();

            try
            {
                var _query = (
                                from _salesOrderHd in this.db.MKTSOHds
                                where _salesOrderHd.TransNmbr == _prmTransNmbr
                                orderby _salesOrderHd.Revisi ascending
                                select new
                                {
                                    Revisi = _salesOrderHd.Revisi
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MKTSOHd(_row.Revisi));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MKTSOHd GetSingleMKTSOHd(string _prmCode, int _prmRevisi)
        {
            MKTSOHd _result = null;

            try
            {
                _result = this.db.MKTSOHds.Single(_temp => _temp.TransNmbr.ToLower() == _prmCode.ToLower() && _temp.Revisi == _prmRevisi);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetLastRevisiMKTSOHd(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.MKTSOHds.Where(_temp => _temp.TransNmbr.ToLower() == _prmCode.ToLower() && _temp.Status == SalesOrderDataMapper.GetStatus(TransStatus.Posted)).Max(_temp => _temp.Revisi);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetNewRevisiByCode(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                from _mktSOHd in this.db.MKTSOHds
                                where _mktSOHd.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    Revisi = _mktSOHd.Revisi
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.Revisi;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiMKTSOHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    MKTSOHd _salesOrderHd = this.db.MKTSOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.Revisi.ToString().Trim().ToLower() == _tempSplit[1].Trim().ToLower());

                    if (_salesOrderHd != null)
                    {
                        if ((_salesOrderHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.MKTSODts
                                          where _detail.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _detail.Revisi.ToString().Trim().ToLower() == _tempSplit[1].Trim().ToLower()
                                          select _detail);

                            this.db.MKTSODts.DeleteAllOnSubmit(_query);

                            this.db.MKTSOHds.DeleteOnSubmit(_salesOrderHd);

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

        public string AddMKTSOHd(MKTSOHd _prmMKTSOHd)
        {
            string _result = "";

            try
            {

                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmMKTSOHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.MKTSOHds.InsertOnSubmit(_prmMKTSOHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmMKTSOHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditMKTSOHd(MKTSOHd _prmMKTSOHd)
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

        public string Revisi(string _prmTransNmbr, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_MKSOCreateRevisi(_prmTransNmbr, _prmuser, ref _errorMsg);

                if (_errorMsg != "")
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Revisi Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Closing(string _prmTransNmbr, int _prmRevisi, string _prmProduct, string _prmRemark, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_MKSOClosing(_prmTransNmbr, _prmRevisi, _prmProduct, _prmRemark, _prmuser, ref _errorMsg);

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

        public string GetAppr(string _prmTransNmbr, int _prmRevisi, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_MKSOGetAppr(_prmTransNmbr, _prmRevisi, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.SalesOrder);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
                else
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Approve(string _prmTransNmbr, int _prmRevisi, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.S_MKSOApprove(_prmTransNmbr, _prmRevisi, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        MKTSOHd _mktSOHd = this.GetSingleMKTSOHd(_prmTransNmbr, _prmRevisi);

                        if ((_mktSOHd.FileNmbr ?? "").Trim() == "")
                        {
                            foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_mktSOHd.TransDate.Year, _mktSOHd.TransDate.Month, AppModule.GetValue(TransactionType.SalesOrder), this._companyTag, ""))
                            {
                                _mktSOHd.FileNmbr = item.Number;
                            }
                        }                        

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.SalesOrder);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleMKTSOHd(_prmTransNmbr,_prmRevisi).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

                        _result = "Approve Success";
                        _scope.Complete();
                    }
                    else
                    {
                        _result = _errorMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Posting(string _prmTransNmbr, int _prmRevisi, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                MKTSOHd _mktSOHd = this.db.MKTSOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.Revisi == _prmRevisi);
                String _locked = _transCloseBL.IsExistAndLocked(_mktSOHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_MKSOPost(_prmTransNmbr, _prmRevisi, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.SalesOrder);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleMKTSOHd(_prmTransNmbr, _prmRevisi).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();
                    }
                    else
                    {
                        _result = _errorMsg;
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
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Unposting(string _prmTransNmbr, int _prmRevisi, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                MKTSOHd _mktSOHd = this.db.MKTSOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.Revisi == _prmRevisi);
                String _locked = _transCloseBL.IsExistAndLocked(_mktSOHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_MKSOUnPost(_prmTransNmbr, _prmRevisi, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";
                    }
                    else
                    {
                        _result = _errorMsg;
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
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string GetFileNmbrMKTSOHd(string _prmTransNmbr)
        {
            string _result = "";

            try
            {
                _result = (this.db.MKTSOHds.Single(_temp => _temp.TransNmbr == _prmTransNmbr).FileNmbr ?? "").Trim();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleMKTSOHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    MKTSOHd _mktDOHd = this.db.MKTSOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.Revisi.ToString().Trim().ToLower() == _tempSplit[1].Trim().ToLower());

                    if (_mktDOHd != null)
                    {
                        if (_mktDOHd.Status != SalesOrderDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveMKTSOHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    MKTSOHd _mktSOHd = this.db.MKTSOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.Revisi.ToString().Trim().ToLower() == _tempSplit[1].Trim().ToLower());

                    if (_mktSOHd.Status == SalesOrderDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _mktSOHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _mktSOHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_mktSOHd != null)
                    {
                        if ((_mktSOHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.MKTSODts
                                          where _detail.TransNmbr == _prmCode[i]
                                          select _detail);

                            this.db.MKTSODts.DeleteAllOnSubmit(_query);

                            this.db.MKTSOHds.DeleteOnSubmit(_mktSOHd);

                            _result = true;
                        }
                        else if (_mktSOHd.FileNmbr != "" && _mktSOHd.Status == SalesOrderDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _mktSOHd.Status = SalesOrderDataMapper.GetStatus(TransStatus.Deleted);
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

        #region MKTSODt
        public int RowsCountMKTSODt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _MKTSODt in this.db.MKTSODts
                                 where _MKTSODt.TransNmbr == _prmCode
                                 select _MKTSODt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MKTSODt> GetListMKTSODt(int _prmReqPage, int _prmPageSize, string _prmCode, int _prmRevisi)
        {
            List<MKTSODt> _result = new List<MKTSODt>();

            try
            {
                var _query = (
                                from _salesOrderDt in this.db.MKTSODts
                                join _product in this.db.MsProducts
                                    on _salesOrderDt.ProductCode equals _product.ProductCode
                                where _salesOrderDt.TransNmbr == _prmCode && _salesOrderDt.Revisi == _prmRevisi
                                orderby _salesOrderDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _salesOrderDt.TransNmbr,
                                    Revisi = _salesOrderDt.Revisi,
                                    ProductCode = _salesOrderDt.ProductCode,
                                    ProductName = _product.ProductName,
                                    QtyOrder = _salesOrderDt.QtyOrder,
                                    UnitOrder = _salesOrderDt.UnitOrder,
                                    Qty = _salesOrderDt.Qty,
                                    Unit = _salesOrderDt.Unit,
                                    DoneClosing = _salesOrderDt.DoneClosing,
                                    QtyClose = _salesOrderDt.QtyClose,
                                    Price = _salesOrderDt.Price,
                                    Amount = _salesOrderDt.Amount,
                                    Remark = _salesOrderDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MKTSODt(_row.TransNmbr, _row.Revisi, _row.ProductCode, _row.ProductName, _row.QtyOrder, _row.UnitOrder, _row.Qty, _row.Unit, _row.DoneClosing, _row.QtyClose, _row.Price, _row.Amount, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MKTSODt> GetListMKTSODt(string _prmCode, int _prmRevisi)
        {
            List<MKTSODt> _result = new List<MKTSODt>();

            try
            {
                var _query = (
                                from _salesOrderDt in this.db.MKTSODts
                                join _product in this.db.MsProducts
                                    on _salesOrderDt.ProductCode equals _product.ProductCode
                                where _salesOrderDt.TransNmbr == _prmCode && _salesOrderDt.Revisi == _prmRevisi
                                orderby _salesOrderDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _salesOrderDt.TransNmbr,
                                    Revisi = _salesOrderDt.Revisi,
                                    ProductCode = _salesOrderDt.ProductCode,
                                    ProductName = _product.ProductName,
                                    QtyOrder = _salesOrderDt.QtyOrder,
                                    UnitOrder = _salesOrderDt.UnitOrder,
                                    Qty = _salesOrderDt.Qty,
                                    Unit = _salesOrderDt.Unit,
                                    DoneClosing = _salesOrderDt.DoneClosing,
                                    QtyClose = _salesOrderDt.QtyClose,
                                    Price = _salesOrderDt.Price,
                                    Amount = _salesOrderDt.Amount,
                                    Remark = _salesOrderDt.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MKTSODt(_row.TransNmbr, _row.Revisi, _row.ProductCode, _row.ProductName, _row.QtyOrder, _row.UnitOrder, _row.Qty, _row.Unit, _row.DoneClosing, _row.QtyClose, _row.Price, _row.Amount, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<String> GetListProductMKTSODt(string _prmCode, int _prmRevisi)
        {
            List<String> _result = new List<String>();

            try
            {
                var _query = (
                                from _salesOrderDt in this.db.MKTSODts
                                join _product in this.db.MsProducts
                                    on _salesOrderDt.ProductCode equals _product.ProductCode
                                where _salesOrderDt.TransNmbr == _prmCode && _salesOrderDt.Revisi == _prmRevisi
                                orderby _salesOrderDt.ProductCode ascending
                                select new
                                {
                                    ProductCode = _salesOrderDt.ProductCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(_row.ProductCode);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MKTSODt GetSingleMKTSODt(string _prmCode, int _prmRevisi, string _prmProductCode)
        {
            MKTSODt _result = null;

            try
            {
                _result = this.db.MKTSODts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.Revisi == _prmRevisi && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Decimal GetPriceMKTSODtLastRevision(string _prmCode, string _prmProductCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _mktSODt in this.db.MKTSODts
                                where _mktSODt.TransNmbr == _prmCode
                                    && _mktSODt.ProductCode == _prmProductCode
                                orderby _mktSODt.Revisi descending
                                select _mktSODt.Price
                             ).FirstOrDefault();

                _result = (_query == null) ? 0 : Convert.ToDecimal(_query);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiMKTSODt(string[] _prmCode, string _prmTransNo, int _prmRevisi)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            decimal? _tempDPForex = 0;
            decimal _tempDiscForex = 0;
            decimal _tempDisc = 0;
            decimal _tempPPNForex = 0;
            decimal _tempTotalForex = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MKTSODt _mktSODt = this.db.MKTSODts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _prmCode[i].Trim().ToLower() && _temp.Revisi == _prmRevisi && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    MKTSOHd _mktSOHd = this.GetSingleMKTSOHd(_prmTransNo, _prmRevisi);

                    decimal _amount = Convert.ToDecimal((_mktSODt.Amount == null) ? 0 : _mktSODt.Amount);

                    _tempBaseForex = _mktSOHd.BaseForex - _amount;

                    _tempDisc = _mktSOHd.Disc;
                    if (_tempDisc == 0)
                    {
                        _tempDiscForex = _mktSOHd.DiscForex;
                    }
                    else
                    {
                        _tempDiscForex = _tempBaseForex * _tempDisc / 100;
                    }

                    _tempDPForex = (_tempBaseForex - _tempDiscForex) * (((_mktSOHd.DP == null) ? 0 : _mktSOHd.DP) / 100);
                    _tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_mktSOHd.PPN / 100);
                    _tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex;

                    _mktSOHd.BaseForex = _tempBaseForex;
                    _mktSOHd.DPForex = _tempDPForex;
                    _mktSOHd.DiscForex = _tempDiscForex;
                    _mktSOHd.Disc = _tempDisc;
                    _mktSOHd.PPNForex = _tempPPNForex;
                    _mktSOHd.TotalForex = _tempTotalForex;

                    this.db.MKTSODts.DeleteOnSubmit(_mktSODt);
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

        public bool AddMKTSODtList(List<MKTSODt> _prmMKTSODtList)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            //decimal? _tempDPForex = 0;
            //decimal _tempDiscForex = 0;
            //decimal _tempDisc = 0;
            //decimal _tempPPNForex = 0;
            //decimal _tempTotalForex = 0;
            decimal _amount = 0;

            try
            {
                MKTSOHd _mktSOHd = null;

                foreach (MKTSODt _row in _prmMKTSODtList)
                {
                    MKTSODt _mktSODt = new MKTSODt();

                    _mktSODt.TransNmbr = _row.TransNmbr;
                    _mktSODt.Revisi = _row.Revisi;
                    _mktSODt.ProductCode = _row.ProductCode;
                    _mktSODt.QtyOrder = _row.QtyOrder;
                    _mktSODt.UnitOrder = _row.UnitOrder;
                    _mktSODt.Qty = _row.Qty;
                    _mktSODt.Unit = _row.Unit;
                    _mktSODt.Price = _row.Price;
                    _mktSODt.Amount = _row.Amount;
                    _mktSODt.Remark = _row.Remark;
                    _mktSODt.DoneClosing = _row.DoneClosing;
                    _mktSODt.QtyDO = _row.QtyDO;

                    this.db.MKTSODts.InsertOnSubmit(_mktSODt);

                    _mktSOHd = this.GetSingleMKTSOHd(_mktSODt.TransNmbr, _mktSODt.Revisi);

                    _amount = Convert.ToDecimal((_mktSODt.Amount == null) ? 0 : _mktSODt.Amount);

                    _tempBaseForex = _tempBaseForex + _amount;

                    //_tempDisc = _mktSOHd.Disc;
                    //if (_tempDisc == 0)
                    //{
                    //    _tempDiscForex = _mktSOHd.DiscForex;
                    //}
                    //else
                    //{
                    //    _tempDiscForex = _tempBaseForex * _tempDisc / 100;
                    //}
                    //_tempDPForex = (_tempBaseForex - _tempDiscForex) * (((_mktSOHd.DP == null) ? 0 : _mktSOHd.DP) / 100);
                    //_tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_mktSOHd.PPN / 100);
                    //_tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex;
                }

                _mktSOHd.BaseForex = _mktSOHd.BaseForex + _tempBaseForex;
                if (_mktSOHd.Disc == 0)
                {
                    _mktSOHd.DiscForex = _mktSOHd.DiscForex;
                }
                else
                {
                    _mktSOHd.DiscForex = _mktSOHd.BaseForex * _mktSOHd.Disc / 100;
                }
                _mktSOHd.DPForex = (_mktSOHd.BaseForex - _mktSOHd.DiscForex) * (((_mktSOHd.DP == null) ? 0 : _mktSOHd.DP) / 100);
                _mktSOHd.PPNForex = (_mktSOHd.BaseForex - _mktSOHd.DiscForex) * (_mktSOHd.PPN / 100);
                _mktSOHd.TotalForex = _mktSOHd.BaseForex - _mktSOHd.DiscForex + _mktSOHd.PPNForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddMKTSODt(MKTSODt _prmMKTSODt)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            decimal? _tempDPForex = 0;
            decimal _tempDiscForex = 0;
            decimal _tempDisc = 0;
            decimal _tempPPNForex = 0;
            decimal _tempTotalForex = 0;

            try
            {
                MKTSOHd _mktSOHd = this.GetSingleMKTSOHd(_prmMKTSODt.TransNmbr, _prmMKTSODt.Revisi);

                decimal _amount = Convert.ToDecimal((_prmMKTSODt.Amount == null) ? 0 : _prmMKTSODt.Amount);

                _tempBaseForex = _mktSOHd.BaseForex + _amount;
                _tempDisc = _mktSOHd.Disc;
                if (_tempDisc == 0)
                {
                    _tempDiscForex = _mktSOHd.DiscForex;
                }
                else
                {
                    _tempDiscForex = _tempBaseForex * _tempDisc / 100;
                }
                _tempDPForex = (_tempBaseForex - _tempDiscForex) * (((_mktSOHd.DP == null) ? 0 : _mktSOHd.DP) / 100);
                _tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_mktSOHd.PPN / 100);
                _tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex;

                _mktSOHd.BaseForex = _tempBaseForex;
                _mktSOHd.DPForex = _tempDPForex;
                _mktSOHd.DiscForex = _tempDiscForex;
                _mktSOHd.Disc = _tempDisc;
                _mktSOHd.PPNForex = _tempPPNForex;
                _mktSOHd.TotalForex = _tempTotalForex;

                this.db.MKTSODts.InsertOnSubmit(_prmMKTSODt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;

            //bool _result = false;

            //try
            //{


            //    this.db.MKTSODts.InsertOnSubmit(_prmMKTSODt);
            //    this.db.SubmitChanges();

            //    _result = true;
            //}
            //catch (Exception ex)
            //{
            //    ErrorHandler.Record(ex, EventLogEntryType.Error);
            //}

            //return _result;
        }

        public bool EditMKTSODt(MKTSODt _prmMKTSODt, decimal _prmAmountOriginal)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            decimal? _tempDPForex = 0;
            decimal _tempDiscForex = 0;
            decimal _tempDisc = 0;
            decimal _tempPPNForex = 0;
            decimal _tempTotalForex = 0;

            try
            {
                MKTSOHd _mktSOHd = this.GetSingleMKTSOHd(_prmMKTSODt.TransNmbr, _prmMKTSODt.Revisi);

                decimal _amount = Convert.ToDecimal((_prmMKTSODt.Amount == null) ? 0 : _prmMKTSODt.Amount);

                _tempBaseForex = _mktSOHd.BaseForex - _prmAmountOriginal; // delete amount original
                _tempBaseForex = _tempBaseForex + _amount; // add amount edited
                _tempDisc = _mktSOHd.Disc;
                if (_tempDisc == 0)
                {
                    _tempDiscForex = _mktSOHd.DiscForex;
                }
                else
                {
                    _tempDiscForex = _tempBaseForex * _tempDisc / 100;
                }
                _tempDPForex = (_tempBaseForex - _tempDiscForex) * (((_mktSOHd.DP == null) ? 0 : _mktSOHd.DP) / 100);
                _tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_mktSOHd.PPN / 100);
                _tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex;

                _mktSOHd.BaseForex = _tempBaseForex;
                _mktSOHd.DPForex = _tempDPForex;
                _mktSOHd.DiscForex = _tempDiscForex;
                _mktSOHd.Disc = _tempDisc;
                _mktSOHd.PPNForex = _tempPPNForex;
                _mktSOHd.TotalForex = _tempTotalForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string Close(string _prmTransNmbr, string _prmProduct, string _prmRemark, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                //int _success = this.db.S_STTransferReqClosing(_prmTransNmbr, _prmProduct, _prmRemark, _prmuser, ref _errorMsg);

                //if (_errorMsg != "")
                //{
                //    _result = _errorMsg;
                //}
            }
            catch (Exception ex)
            {
                _result = "Close Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        #endregion

        #region V_MKSOForDO
        public List<MKTSOHd> GetSONoFromVMKSOForDO(string _prmCustCode)
        {
            List<MKTSOHd> _result = new List<MKTSOHd>();

            try
            {
                var _query = (
                                from _vMKSOForDO in this.db.V_MKSOForDOs
                                where _vMKSOForDO.Customer_Code == _prmCustCode
                                select new
                                {
                                    SO_No = _vMKSOForDO.SO_No,
                                    FileNmbr = _vMKSOForDO.FileNmbr
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

        public string GetPOCustNoFromVMKSOForDO(string _prmSONo)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vMKSOForDOs in this.db.V_MKSOForDOs
                                where _vMKSOForDOs.SO_No == _prmSONo
                                select new
                                {
                                    POCustNo = _vMKSOForDOs.Customer_PO_No
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = _row.POCustNo;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetDeliveryToFromVMKSOForDO(string _prmSONo)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vMKSOForDO in this.db.V_MKSOForDOs
                                where _vMKSOForDO.SO_No == _prmSONo
                                select new
                                {
                                    DeliveryTo = _vMKSOForDO.Delivery_To
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = _row.DeliveryTo;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetDeliveryDateFromVMKSOForDO(string _prmSONo)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vMKSOForDO in this.db.V_MKSOForDOs
                                where _vMKSOForDO.SO_No == _prmSONo
                                //select _vMKSOForDO
                                select new
                                {
                                    DeliveryDate = _vMKSOForDO.Delivery_Date
                                }
                             );

                foreach (var _row in _query)
                {//DeliveryDate = (_vMKSOForDO.Delivery_Date == null) ? "" : DateFormMapper.GetValue(_vMKSOForDO.Delivery_Date)
                    _result = (_row.DeliveryDate == null) ? "" : DateFormMapper.GetValue(_row.DeliveryDate);
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductForDDLDODt(string _prmSONo)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _vMKSOForDO in this.db.V_MKSOForDOs
                                where _vMKSOForDO.SO_No.Trim().ToLower() == _prmSONo.Trim().ToLower()
                                orderby _vMKSOForDO.Product_Name
                                select new
                                {
                                    Product_Code = _vMKSOForDO.Product_Code,
                                    Product_Name = _vMKSOForDO.Product_Name
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.Product_Code, _row.Product_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetQtyFromVMKSOForDO(string _prmSONo, string _prmProductCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _vMKSOForDO in this.db.V_MKSOForDOs
                                where _vMKSOForDO.SO_No == _prmSONo && _vMKSOForDO.Product_Code == _prmProductCode
                                select new
                                {
                                    Qty = (_vMKSOForDO.Qty == null) ? 0 : Convert.ToDecimal(_vMKSOForDO.Qty)
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = _row.Qty;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~SalesOrderBL()
        {
        }
    }
}
