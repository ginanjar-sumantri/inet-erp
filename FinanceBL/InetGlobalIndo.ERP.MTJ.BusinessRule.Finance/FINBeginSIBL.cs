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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class FINBeginSIBL : Base
    {
        public FINBeginSIBL()
        {
        }

        #region FINBeginSI
        public double RowsCountFINBeginSI(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "InvoiceNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _finBeginSI in this.db.FINBeginSIs
                            join _msSupp in this.db.MsSuppliers
                                on _finBeginSI.SuppCode equals _msSupp.SuppCode
                            where (SqlMethods.Like(_finBeginSI.InvoiceNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _finBeginSI.InvoiceNo
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINBeginSI> GetListFINBeginSI(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINBeginSI> _result = new List<FINBeginSI>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "InvoiceNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _finBeginSI in this.db.FINBeginSIs
                                join _msSupp in this.db.MsSuppliers
                                    on _finBeginSI.SuppCode equals _msSupp.SuppCode
                                where (SqlMethods.Like(_finBeginSI.InvoiceNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _finBeginSI.DatePrep descending
                                select new
                                {
                                    InvoiceNo = _finBeginSI.InvoiceNo,
                                    TransDate = _finBeginSI.TransDate,
                                    Status = _finBeginSI.Status,
                                    SuppCode = _finBeginSI.SuppCode,
                                    SuppName = _msSupp.SuppName,
                                    CurrCode = _finBeginSI.CurrCode,
                                    DueDate = _finBeginSI.DueDate,
                                    Term = _finBeginSI.Term
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINBeginSI(_row.InvoiceNo, _row.TransDate, _row.Status, _row.SuppCode, _row.SuppName, _row.CurrCode, _row.DueDate, _row.Term));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINBeginSI> GetListForDDL(string _prmCode)
        {
            List<FINBeginSI> _result = new List<FINBeginSI>();

            try
            {
                var _query = (
                                from _finBeginSI in this.db.FINBeginSIs
                                where _finBeginSI.SuppCode == _prmCode
                                orderby _finBeginSI.InvoiceNo ascending
                                select new
                                {
                                    InvoiceNo = _finBeginSI.InvoiceNo
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINBeginSI(_row.InvoiceNo));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINBeginSI GetSingleFINBeginSI(string _prmCode)
        {
            FINBeginSI _result = null;

            try
            {
                _result = this.db.FINBeginSIs.Single(_temp => _temp.InvoiceNo == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINBeginSI(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINBeginSI _finBeginSI = this.db.FINBeginSIs.Single(_temp => _temp.InvoiceNo.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finBeginSI != null)
                    {
                        if (_finBeginSI.Status != SuppBeginningDataMapper.GetStatus(TransStatus.Posted))
                        {
                            this.db.FINBeginSIs.DeleteOnSubmit(_finBeginSI);

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

        public bool AddFINBeginSI(FINBeginSI _prmFINBeginSI)
        {
            bool _result = false;

            try
            {
                this.db.FINBeginSIs.InsertOnSubmit(_prmFINBeginSI);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINBeginSI(FINBeginSI _prmFINBeginSI)
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
                int _success = this.db.S_FNSIBeginGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = "SS";
                    _transActivity.TransNmbr = _prmTransNmbr;
                    _transActivity.FileNmbr = GetSingleFINBeginSI(_prmTransNmbr).InvoiceNo;
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
                int _success = this.db.S_FNSIBeginApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Approve Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = "SS";
                    _transActivity.TransNmbr = _prmTransNmbr;
                    _transActivity.FileNmbr = GetSingleFINBeginSI(_prmTransNmbr).InvoiceNo;
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
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
                FINBeginSI _finBeginSI = this.db.FINBeginSIs.Single(_temp => _temp.InvoiceNo.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finBeginSI.TransDate);

                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSIBeginPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = "SS";
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINBeginSI(_prmTransNmbr).InvoiceNo;
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
                FINBeginSI _finBeginSI = this.db.FINBeginSIs.Single(_temp => _temp.InvoiceNo.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finBeginSI.TransDate);

                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSIBeginUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = "SS";
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINBeginSI(_prmTransNmbr).InvoiceNo;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINBeginSI(_prmTransNmbr).TransDate;
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
                _result = "Unposting Failed ";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }
        #endregion

        ~FINBeginSIBL()
        {
        }
    }
}
