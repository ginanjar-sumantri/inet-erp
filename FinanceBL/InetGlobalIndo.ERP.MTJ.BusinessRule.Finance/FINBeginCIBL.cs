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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class FINBeginCIBL : Base
    {
        public FINBeginCIBL()
        {
        }

        #region FINBeginCI
        public double RowsCountFINBeginCI(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "InvoiceNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "CustName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _finBeginCI in this.db.FINBeginCIs
                            join _msCustomer in this.db.MsCustomers
                                on _finBeginCI.CustCode equals _msCustomer.CustCode
                            where (SqlMethods.Like(_finBeginCI.InvoiceNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _finBeginCI.InvoiceNo
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINBeginCI> GetListFINBeginCI(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINBeginCI> _result = new List<FINBeginCI>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "InvoiceNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "CustName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _finBeginCI in this.db.FINBeginCIs
                                join _msCustomer in this.db.MsCustomers
                                    on _finBeginCI.CustCode equals _msCustomer.CustCode
                                where (SqlMethods.Like(_finBeginCI.InvoiceNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _finBeginCI.DatePrep descending
                                select new
                                {
                                    InvoiceNo = _finBeginCI.InvoiceNo,
                                    TransDate = _finBeginCI.TransDate,
                                    CurrCode = _finBeginCI.CurrCode,
                                    Status = _finBeginCI.Status,
                                    CustCode = _finBeginCI.CustCode,
                                    CustName = _msCustomer.CustName,
                                    DueDate = _finBeginCI.DueDate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINBeginCI(_row.InvoiceNo, _row.TransDate, _row.CurrCode, _row.Status, _row.CustCode, _row.CustName, _row.DueDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINBeginCI> GetListForDDL(string _prmCode)
        {
            List<FINBeginCI> _result = new List<FINBeginCI>();

            try
            {
                var _query = (
                                from _finBeginCI in this.db.FINBeginCIs
                                where _finBeginCI.CustCode == _prmCode
                                orderby _finBeginCI.InvoiceNo ascending
                                select new
                                {
                                    InvoiceNo = _finBeginCI.InvoiceNo
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINBeginCI(_row.InvoiceNo));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINBeginCI GetSingleFINBeginCI(string _prmCode)
        {
            FINBeginCI _result = null;

            try
            {
                _result = this.db.FINBeginCIs.Single(_temp => _temp.InvoiceNo == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINBeginCI(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINBeginCI _finBeginCI = this.db.FINBeginCIs.Single(_temp => _temp.InvoiceNo.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finBeginCI != null)
                    {
                        if (_finBeginCI.Status != CustBeginningDataMapper.GetStatus(TransStatus.Posted))
                        {
                            this.db.FINBeginCIs.DeleteOnSubmit(_finBeginCI);

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

        public bool AddFINBeginCI(FINBeginCI _prmFINBeginCI)
        {
            bool _result = false;

            try
            {
                this.db.FINBeginCIs.InsertOnSubmit(_prmFINBeginCI);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINBeginCI(FINBeginCI _prmFINBeginCI)
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
                int _success = this.db.S_FNCIBeginGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = "SC";
                    _transActivity.TransNmbr = _prmTransNmbr;
                    _transActivity.FileNmbr = GetSingleFINBeginCI(_prmTransNmbr).InvoiceNo;
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
                int _success = this.db.S_FNCIBeginApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Approve Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = "SC";
                    _transActivity.TransNmbr = _prmTransNmbr;
                    _transActivity.FileNmbr = GetSingleFINBeginCI(_prmTransNmbr).InvoiceNo;
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
                FINBeginCI _finBeginCI = this.db.FINBeginCIs.Single(_temp => _temp.InvoiceNo.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finBeginCI.TransDate);

                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNCIBeginPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = "SC";
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINBeginCI(_prmTransNmbr).InvoiceNo;
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
                FINBeginCI _finBeginCI = this.db.FINBeginCIs.Single(_temp => _temp.InvoiceNo.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finBeginCI.TransDate);

                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNCIBeginUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = "SC";
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINBeginCI(_prmTransNmbr).InvoiceNo;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINBeginCI(_prmTransNmbr).TransDate;
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
        #endregion

        ~FINBeginCIBL()
        {
        }
    }
}
