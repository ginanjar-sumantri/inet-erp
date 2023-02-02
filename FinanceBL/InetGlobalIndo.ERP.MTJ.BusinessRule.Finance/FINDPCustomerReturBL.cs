using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class FINDPCustomerReturBL : Base
    {
        public FINDPCustomerReturBL()
        {
        }

        #region FINDPCustReturHd
        public double RowsCountFINDPCustReturHd(string _prmCategory, string _prmKeyword)
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
                            from _finDPCustReturHd in this.db.FINDPCustReturHds
                            join _msCust in this.db.MsCustomers
                                    on _finDPCustReturHd.CustCode equals _msCust.CustCode
                            where (SqlMethods.Like(_finDPCustReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_finDPCustReturHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _finDPCustReturHd.Status != DPCustomerReturDataMapper.GetStatus(TransStatus.Deleted)    
                            select _finDPCustReturHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINDPCustReturHd> GetListFINDPCustReturHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINDPCustReturHd> _result = new List<FINDPCustReturHd>();

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
                                from _finDPCustReturHd in this.db.FINDPCustReturHds
                                join _msCust in this.db.MsCustomers
                                    on _finDPCustReturHd.CustCode equals _msCust.CustCode
                                where (SqlMethods.Like(_finDPCustReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finDPCustReturHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finDPCustReturHd.Status != DPCustomerReturDataMapper.GetStatus(TransStatus.Deleted)    
                                orderby _finDPCustReturHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finDPCustReturHd.TransNmbr,
                                    FileNmbr = _finDPCustReturHd.FileNmbr,
                                    TransDate = _finDPCustReturHd.TransDate,
                                    Status = _finDPCustReturHd.Status,
                                    CustCode = _finDPCustReturHd.CustCode,
                                    CustName = _msCust.CustName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPCustReturHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.CustCode, _row.CustName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDPCustReturHd GetSingleFINDPCustReturHd(string _prmCode)
        {
            FINDPCustReturHd _result = null;

            try
            {
                _result = this.db.FINDPCustReturHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCustomerByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finDPCustReturHd in this.db.FINDPCustReturHds
                                where _finDPCustReturHd.TransNmbr == _prmCode
                                select new
                                {
                                    CustCode = _finDPCustReturHd.CustCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CustCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINDPCustReturDt> GetListDPNoForDDL(string _prmCode)
        {
            List<FINDPCustReturDt> _result = new List<FINDPCustReturDt>();

            try
            {
                var _query = (
                                from _finDpCustHd in this.db.FINDPCustHds
                                join _finDPCustReturHd in this.db.FINDPCustReturHds
                                    on _finDpCustHd.CustCode equals _finDPCustReturHd.CustCode
                                where _finDpCustHd.CustCode == _prmCode
                                && _finDpCustHd.Status == DPCustomerReturDataMapper.GetStatus(TransStatus.Posted)
                                && (_finDpCustHd.BaseForex > ((_finDpCustHd.Balance == null) ? 0 : _finDpCustHd.Balance)
                                    || _finDpCustHd.PPNForex > ((_finDpCustHd.BalancePPn == null) ? 0 : _finDpCustHd.BalancePPn))
                                select new
                                {
                                    TransNmbr = _finDpCustHd.TransNmbr
                                }
                              ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPCustReturDt(_row.TransNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINDPCustReturHd> GetListDPNoFileNoForDDL(string _prmCode)
        {
            List<FINDPCustReturHd> _result = new List<FINDPCustReturHd>();

            try
            {
                var _query = (
                                from _finDpCustHd in this.db.FINDPCustHds
                                join _finDPCustReturHd in this.db.FINDPCustReturHds
                                    on _finDpCustHd.CustCode equals _finDPCustReturHd.CustCode
                                where _finDpCustHd.CustCode == _prmCode
                                && _finDpCustHd.Status == DPCustomerReturDataMapper.GetStatus(TransStatus.Posted)
                                && (_finDpCustHd.BaseForex > ((_finDpCustHd.Balance == null) ? 0 : _finDpCustHd.Balance)
                                    || _finDpCustHd.PPNForex > ((_finDpCustHd.BalancePPn == null) ? 0 : _finDpCustHd.BalancePPn))
                                && (_finDpCustHd.FileNmbr ?? "").Trim().ToLower() == _finDpCustHd.FileNmbr.Trim().ToLower()
                                select new
                                {
                                    TransNmbr = _finDpCustHd.TransNmbr,
                                    FileNmbr = _finDpCustHd.FileNmbr
                                }
                              ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPCustReturHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINDPCustReturHd> GetListDPNoForDDLCustRetur(string _prmCode)
        {
            List<FINDPCustReturHd> _result = new List<FINDPCustReturHd>();

            try
            {
                var _query = (
                                from _finDpCustHd in this.db.FINDPCustHds
                                join _finDPCustReturHd in this.db.FINDPCustReturHds
                                    on _finDpCustHd.CustCode equals _finDPCustReturHd.CustCode
                                where _finDpCustHd.CustCode == _prmCode
                                && _finDpCustHd.Status == DPCustomerReturDataMapper.GetStatus(TransStatus.Posted)
                                && (_finDpCustHd.BaseForex > ((_finDpCustHd.Balance == null) ? 0 : _finDpCustHd.Balance)
                                    || _finDpCustHd.PPNForex > ((_finDpCustHd.BalancePPn == null) ? 0 : _finDpCustHd.BalancePPn))
                                select new
                                {
                                    TransNmbr = _finDpCustHd.TransNmbr,
                                    FileNmbr = _finDpCustHd.FileNmbr
                                }
                              ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPCustReturHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINDPCustReturHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPCustReturHd _finDPCustReturHd = this.db.FINDPCustReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPCustReturHd != null)
                    {
                        if ((_finDPCustReturHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINDPCustReturDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINDPCustReturDts.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail2 in this.db.FINDPCustReturPays
                                           where _detail2.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail2);

                            this.db.FINDPCustReturPays.DeleteAllOnSubmit(_query2);

                            this.db.FINDPCustReturHds.DeleteOnSubmit(_finDPCustReturHd);

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

        public string AddFINDPCustReturHd(FINDPCustReturHd _prmFINDPCustReturHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINDPCustReturHd.TransDate.Year, _prmFINDPCustReturHd.TransDate.Month, AppModule.GetValue(TransactionType.FINDPCustRetur), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINDPCustReturHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINDPCustReturHds.InsertOnSubmit(_prmFINDPCustReturHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINDPCustReturHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDPCustReturHd(FINDPCustReturHd _prmFINDPCustReturHd)
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
                this.db.S_FNDPCustReturGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FINDPCustRetur);
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

        public string Approve(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_FNDPCustReturApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        FINDPCustReturHd _finDPCustReturHd = this.GetSingleFINDPCustReturHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_finDPCustReturHd.TransDate.Year, _finDPCustReturHd.TransDate.Month, AppModule.GetValue(TransactionType.FINDPCustRetur), this._companyTag, ""))
                        {
                            _finDPCustReturHd.FileNmbr = _item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FINDPCustRetur);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINDPCustReturHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();
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
                FINDPCustReturHd _finDPCustReturHd = this.db.FINDPCustReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDPCustReturHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNDPCustReturPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FINDPCustRetur);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINDPCustReturHd(_prmTransNmbr).FileNmbr;
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
                FINDPCustReturHd _finDPCustReturHd = this.db.FINDPCustReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDPCustReturHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNDPCustReturUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.FINDPCustRetur);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINDPCustReturHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINDPCustReturHd(_prmTransNmbr).TransDate;
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

        public bool GetSingleFINDPCustReturHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPCustReturHd _finDPCustReturHd = this.db.FINDPCustReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPCustReturHd != null)
                    {
                        if (_finDPCustReturHd.Status != DPCustomerReturDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINDPCustReturHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPCustReturHd _finDPCustReturHd = this.db.FINDPCustReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPCustReturHd.Status == DPCustomerReturDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finDPCustReturHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finDPCustReturHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finDPCustReturHd != null)
                    {
                        if ((_finDPCustReturHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINDPCustReturDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINDPCustReturDts.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail2 in this.db.FINDPCustReturPays
                                           where _detail2.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail2);

                            this.db.FINDPCustReturPays.DeleteAllOnSubmit(_query2);

                            this.db.FINDPCustReturHds.DeleteOnSubmit(_finDPCustReturHd);

                            _result = true;
                        }
                        else if (_finDPCustReturHd.FileNmbr != "" && _finDPCustReturHd.Status == DPCustomerReturDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finDPCustReturHd.Status = DPCustomerReturDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINDPCustReturDt
        public int RowsCountFINDPCustReturDt
        {
            get
            {
                return this.db.FINDPCustReturDts.Count();
            }
        }

        public List<FINDPCustReturDt> GetListFINDPCustReturDt(string _prmCode)
        {
            List<FINDPCustReturDt> _result = new List<FINDPCustReturDt>();

            try
            {
                var _query = (
                                from _finDPCustReturDt in this.db.FINDPCustReturDts
                                where _finDPCustReturDt.TransNmbr == _prmCode
                                orderby _finDPCustReturDt.DPNo ascending
                                select new
                                {
                                    DPNo = _finDPCustReturDt.DPNo,
                                    CurrCode = _finDPCustReturDt.CurrCode,
                                    TotalForex = _finDPCustReturDt.TotalForex,
                                    Remark = _finDPCustReturDt.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPCustReturDt(_row.DPNo, _row.CurrCode, _row.TotalForex, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDPCustReturDt GetSingleFINDPCustReturDt(string _prmCode, string _prmDPNo)
        {
            FINDPCustReturDt _result = null;

            try
            {
                _result = this.db.FINDPCustReturDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.DPNo == _prmDPNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINDPCustReturDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPCustReturDt _finDPCustReturDt = this.db.FINDPCustReturDts.Single(_temp => _temp.DPNo == _prmCode[i] && _temp.TransNmbr == _prmTransNo);

                    this.db.FINDPCustReturDts.DeleteOnSubmit(_finDPCustReturDt);
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

        public bool AddFINDPCustReturDt(FINDPCustReturDt _prmFINDPCustReturDt)
        {
            bool _result = false;

            try
            {
                this.db.FINDPCustReturDts.InsertOnSubmit(_prmFINDPCustReturDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDPCustReturDt(FINDPCustReturDt _prmFINDPCustReturDt)
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

        #region FINDPCustReturPay
        public int RowsCountFINDPCustReturPay
        {
            get
            {
                return this.db.FINDPCustReturPays.Count();
            }
        }

        public List<FINDPCustReturPay> GetListFINDPCustReturPay(string _prmCode)
        {
            List<FINDPCustReturPay> _result = new List<FINDPCustReturPay>();

            try
            {
                var _query = (
                                from _finDPCustReturPay in this.db.FINDPCustReturPays
                                where _finDPCustReturPay.TransNmbr == _prmCode
                                orderby _finDPCustReturPay.ItemNo ascending
                                select new
                                {
                                    ItemNo = _finDPCustReturPay.ItemNo,
                                    PayType = _finDPCustReturPay.PayType,
                                    DocumentNo = _finDPCustReturPay.DocumentNo,
                                    AmountHome = _finDPCustReturPay.AmountHome,
                                    BankPayment = _finDPCustReturPay.BankPayment,
                                    BankExpense = _finDPCustReturPay.BankExpense
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPCustReturPay(_row.ItemNo, _row.PayType, _row.DocumentNo, _row.AmountHome, _row.BankPayment, _row.BankExpense));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDPCustReturPay GetSingleFINDPCustReturPay(string _prmCode, int _prmItemNo)
        {
            FINDPCustReturPay _result = null;

            try
            {
                _result = this.db.FINDPCustReturPays.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINDPCustReturPay(FINDPCustReturPay _prmFINDPCustReturPay)
        {
            bool _result = false;

            try
            {
                this.db.FINDPCustReturPays.InsertOnSubmit(_prmFINDPCustReturPay);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDPCustReturPay(FINDPCustReturPay _prmFINDPCustReturPay)
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

        public bool DeleteMultiFINDPCustReturPay(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPCustReturPay _finDPCustReturPay = this.db.FINDPCustReturPays.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmCode[i]) && _temp.TransNmbr == _prmTransNo);

                    this.db.FINDPCustReturPays.DeleteOnSubmit(_finDPCustReturPay);
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

        public int GetMaxNoItemFINDPCustReturPay(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINDPCustReturPays.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~FINDPCustomerReturBL()
        {
        }
    }
}
