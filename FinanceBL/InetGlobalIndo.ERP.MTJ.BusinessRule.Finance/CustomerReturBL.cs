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
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class CustomerReturBL : Base
    {
        public CustomerReturBL()
        {

        }

        #region FINCustReturHd
        public double RowsCountFINCustReturHd(string _prmCategory, string _prmKeyword)
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
                           from _FINCustReturHd in this.db.FINCustReturHds
                           join _msCust in this.db.MsCustomers
                               on _FINCustReturHd.CustCode equals _msCust.CustCode
                           where (SqlMethods.Like(_FINCustReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                              && (SqlMethods.Like((_FINCustReturHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                              && _FINCustReturHd.Status != CustomerNoteDataMapper.GetStatus(TransStatus.Deleted)
                           select _FINCustReturHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINCustReturHd> GetListFINCustReturHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINCustReturHd> _result = new List<FINCustReturHd>();

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
                                from _FINCustReturHd in this.db.FINCustReturHds
                                join _msCust in this.db.MsCustomers
                                    on _FINCustReturHd.CustCode equals _msCust.CustCode
                                where (SqlMethods.Like(_FINCustReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_FINCustReturHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _FINCustReturHd.Status != CustomerNoteDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _FINCustReturHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _FINCustReturHd.TransNmbr,
                                    FileNmbr = _FINCustReturHd.FileNmbr,
                                    TransDate = _FINCustReturHd.TransDate,
                                    CurrCode = _FINCustReturHd.CurrCode,
                                    Status = _FINCustReturHd.Status,
                                    CustCode = _FINCustReturHd.CustCode,
                                    CustName = _msCust.CustName,
                                    Term = _FINCustReturHd.Term,
                                    TermName = (
                                                    from _msTerm in this.db.MsTerms
                                                    where _FINCustReturHd.Term == _msTerm.TermCode
                                                    select _msTerm.TermName
                                                ).FirstOrDefault(),
                                    BillTo = _FINCustReturHd.BillTo,
                                    BillToName = (
                                                    from _msCustomer in this.db.MsCustomers
                                                    where _msCustomer.CustCode == _FINCustReturHd.BillTo
                                                    select _msCustomer.CustName
                                                 ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINCustReturHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.CustCode, _row.CustName, _row.Term, _row.TermName, _row.BillTo, _row.BillToName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCustReturHd GetSingleFINCustReturHd(string _prmCode)
        {
            FINCustReturHd _result = null;

            try
            {
                _result = this.db.FINCustReturHds.Single(_temp => _temp.TransNmbr.ToLower() == _prmCode.ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCustReturHd GetSingleFINCustReturHdView(string _prmCode)
        {
            FINCustReturHd _result = new FINCustReturHd();

            try
            {
                var _query = (
                               from _FINCustReturHd in this.db.FINCustReturHds
                               join _msCust in this.db.MsCustomers
                                    on _FINCustReturHd.CustCode equals _msCust.CustCode
                               join _msTerm in this.db.MsTerms
                                    on _FINCustReturHd.Term equals _msTerm.TermCode
                               orderby _FINCustReturHd.DatePrep descending
                               where _FINCustReturHd.TransNmbr == _prmCode
                               select new
                               {
                                   TransNmbr = _FINCustReturHd.TransNmbr,
                                   FileNmbr = _FINCustReturHd.FileNmbr,
                                   TransDate = _FINCustReturHd.TransDate,
                                   Status = _FINCustReturHd.Status,
                                   CustCode = _FINCustReturHd.CustCode,
                                   CustName = _msCust.CustName,
                                   CurrCode = _FINCustReturHd.CurrCode,
                                   ForexRate = _FINCustReturHd.ForexRate,
                                   //OtherForex = _FINCustReturHd.OtherForex,
                                   TotalForex = _FINCustReturHd.TotalForex,
                                   Term = _FINCustReturHd.Term,
                                   TermName = _msTerm.TermName,
                                   //DiscForex = _FINCustReturHd.DiscForex,
                                   BaseForex = _FINCustReturHd.BaseForex,
                                   Attn = _FINCustReturHd.Attn,
                                   Remark = _FINCustReturHd.Remark,
                                   PPN = _FINCustReturHd.PPN,
                                   PPNDate = _FINCustReturHd.PPNDate,
                                   PPNForex = _FINCustReturHd.PPNForex,
                                   PPNNo = _FINCustReturHd.PPNNo,
                                   PPNRate = _FINCustReturHd.PPNRate,
                                   BillTo = _FINCustReturHd.BillTo,
                                   BillToName = new CustomerBL().GetNameByCode(_FINCustReturHd.BillTo)
                               }
                           ).Single();

                _result.TransNmbr = _query.TransNmbr;
                _result.FileNmbr = _query.FileNmbr;
                _result.TransDate = _query.TransDate;
                _result.Status = _query.Status;
                _result.CustCode = _query.CustCode;
                _result.CustName = _query.CustName;
                _result.CurrCode = _query.CurrCode;
                _result.ForexRate = _query.ForexRate;
                //_result.OtherForex = _query.OtherForex;
                _result.TotalForex = _query.TotalForex;
                _result.Term = _query.Term;
                _result.TermName = _query.TermName;
                //_result.DiscForex = _query.DiscForex;
                _result.BaseForex = _query.BaseForex;
                _result.Attn = _query.Attn;
                _result.Remark = _query.Remark;
                _result.PPN = _query.PPN;
                _result.PPNDate = _query.PPNDate;
                _result.PPNForex = _query.PPNForex;
                _result.PPNNo = _query.PPNNo;
                _result.PPNRate = _query.PPNRate;
                _result.BillTo = _query.BillTo;
                _result.BillToName = _query.BillToName;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFileNmbrV_STSJForCIsBySJNo(string _prmCode)
        {
            string _result = null;

            try
            {
                _result = this.db.V_STSJForCIs.Single(_temp => _temp.SJ_No.ToLower() == _prmCode.ToLower()).FileNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINCustReturHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCustReturHd _FINCustReturHd = this.db.FINCustReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_FINCustReturHd != null)
                    {
                        if ((_FINCustReturHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINCustReturDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            //var _query1 = (from _detail1 in this.db.FINCustInvSJLists
                            //               where _detail1.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                            //               select _detail1);

                            //this.db.FINCustInvSJLists.DeleteAllOnSubmit(_query1);

                            this.db.FINCustReturDts.DeleteAllOnSubmit(_query);

                            this.db.FINCustReturHds.DeleteOnSubmit(_FINCustReturHd);

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

        public string AddFINCustReturHd(FINCustReturHd _prmFINCustReturHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINCustReturHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.FINCustReturHds.InsertOnSubmit(_prmFINCustReturHd);
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINCustReturHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINCustReturHd(FINCustReturHd _prmFINCustReturHd)
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
                int _success = this.db.S_FNCustReturGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.CustomerRetur);
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

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.S_FNCustReturApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINCustReturHd _FINCustReturHd = this.GetSingleFINCustReturHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_FINCustReturHd.TransDate.Year, _FINCustReturHd.TransDate.Month, AppModule.GetValue(TransactionType.CustomerRetur), this._companyTag, ""))
                        {
                            _FINCustReturHd.FileNmbr = _item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.CustomerRetur);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINCustReturHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();
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
                FINCustReturHd _FINCustReturHd = this.db.FINCustReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_FINCustReturHd.TransDate);

                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNCustReturPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.CustomerRetur);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINCustReturHd(_prmTransNmbr).FileNmbr;
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
                FINCustReturHd _FINCustReturHd = this.db.FINCustReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_FINCustReturHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNCustReturUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        //_result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.CustomerNote);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINCustReturHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINCustReturHd(_prmTransNmbr).TransDate;
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

        public bool GetSingleFINCustReturHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCustReturHd _FINCustReturHd = this.db.FINCustReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_FINCustReturHd != null)
                    {
                        if (_FINCustReturHd.Status != CustomerReturDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINCustReturHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCustReturHd _FINCustReturHd = this.db.FINCustReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_FINCustReturHd.Status == CustomerNoteDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _FINCustReturHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _FINCustReturHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_FINCustReturHd != null)
                    {
                        if ((_FINCustReturHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINCustReturDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            //var _query1 = (from _detail1 in this.db.FINCustInvSJLists
                            //               where _detail1.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                            //               select _detail1);

                            //this.db.FINCustInvSJLists.DeleteAllOnSubmit(_query1);

                            this.db.FINCustReturDts.DeleteAllOnSubmit(_query);

                            this.db.FINCustReturHds.DeleteOnSubmit(_FINCustReturHd);

                            _result = true;
                        }
                        else if (_FINCustReturHd.FileNmbr != "" && _FINCustReturHd.Status == CustomerReturDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _FINCustReturHd.Status = CustomerReturDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINCustReturDt

        public List<STCReturRRHd> GetListRRnoForDDL()
        {
            List<STCReturRRHd> _result = new List<STCReturRRHd>();
            try
            {
                var _query = (from _stcReturRRHds in this.db.STCReturRRHds
                              join _stcReturRRDts in this.db.STCReturRRDts
                              on _stcReturRRHds.TransNmbr equals _stcReturRRDts.TransNmbr
                              where _stcReturRRHds.FgDeliveryBack != 'Y'
                              && _stcReturRRHds.Status == STCReturDataMapper.GetStatus(TransStatus.Posted)
                              && (_stcReturRRDts.Qty - ((_stcReturRRDts.QtySJ) == null ? 0 : _stcReturRRDts.QtySJ)) != 0
                              select new
                              {
                                  RRno = _stcReturRRHds.TransNmbr,
                                  FileNmbr = _stcReturRRHds.FileNmbr
                              }
                              ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCReturRRHd(_row.RRno, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public String SaveFINCustReturDt(String _prmTransNmbr, String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (from _finCustReturDts in this.db.FINCustReturDts
                              where _finCustReturDts.RRNo == _prmTransNmbr
                              select _finCustReturDts
                                ).Count();

                if (_query == 0)
                {
                    List<STCReturRRDt> _stcReturRRDt = this.GetListSTCReturRRDt(_prmTransNmbr);
                    FINCustReturHd _finCustReturHd = this.GetSingleFINCustReturHd(_prmCode);
                    Decimal _totalForex = 0;

                    foreach (var _row in _stcReturRRDt)
                    {
                        String _reqReturNo = this.GetReqReturNoSTCReturRRHd(_prmTransNmbr);
                        //STCReturRRHd _stcReturRRHd = this.db.STCReturRRHds.Single(_temp => _temp.TransNmbr.ToLower() == _prmTransNmbr.ToLower());
                        MKTReqReturDt _mktReqReturDt = this.db.MKTReqReturDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _reqReturNo.Trim().ToLower() && _temp.ProductCode.Trim().ToLower() == _row.ProductCode.Trim().ToLower());

                        FINCustReturDt _finCustReturDt = new FINCustReturDt();
                        _finCustReturDt.TransNmbr = _prmCode;
                        _finCustReturDt.RRNo = _row.TransNmbr;
                        _finCustReturDt.ProductCode = _row.ProductCode;
                        _finCustReturDt.Qty = _row.Qty - Convert.ToDecimal(_row.QtySJ);
                        _finCustReturDt.Unit = _row.Unit;
                        _finCustReturDt.PriceForex = _mktReqReturDt.PriceForex;
                        _finCustReturDt.AmountForex = _finCustReturDt.Qty * _mktReqReturDt.PriceForex;
                        _finCustReturDt.Remark = "";
                        _finCustReturDt.RequestNo = _reqReturNo;
                        _totalForex += _finCustReturDt.AmountForex;

                        this.db.FINCustReturDts.InsertOnSubmit(_finCustReturDt);
                    }
                    _finCustReturHd.BaseForex = _totalForex + _finCustReturHd.BaseForex;
                    _finCustReturHd.PPNForex = Convert.ToDecimal((_finCustReturHd.BaseForex * _finCustReturHd.PPN / 100));
                    _finCustReturHd.TotalForex = _finCustReturHd.BaseForex + _finCustReturHd.PPNForex;

                    this.db.SubmitChanges();
                }
                else
                {
                    _result = "There Are Already Existing Data";
                }
            }
            catch (Exception ex)
            {
                _result = "You Failed Add Data";
            }

            return _result;
        }

        public List<FINCustReturDt> GetlistFINCustReturDt(int _prmReqPage, int _prmPageSize, String _prmTransNmbr)
        {
            List<FINCustReturDt> _result = new List<FINCustReturDt>();
            try
            {
                var _query = (from _finCustReturDts in this.db.FINCustReturDts
                              where _finCustReturDts.TransNmbr == _prmTransNmbr
                              //&& (_stcReturRRDt.Qty - _stcReturRRDt.QtySJ) != 0
                              select _finCustReturDts
                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;

        }

        public List<STCReturRRDt> GetListSTCReturRRDt(string _prmTransNmbr)
        {
            List<STCReturRRDt> _result = new List<STCReturRRDt>();

            try
            {
                var _query =
                            (
                                from _STCReturRRDt in this.db.STCReturRRDts
                                where _STCReturRRDt.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    TransNmbr = _STCReturRRDt.TransNmbr,
                                    ProductCode = _STCReturRRDt.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _STCReturRRDt.ProductCode
                                                    select _msProduct.ProductName
                                                  ).FirstOrDefault(),
                                    LocationCode = _STCReturRRDt.LocationCode,
                                    LocationName = (
                                                    from _msLocation in this.db.MsWrhsLocations
                                                    where _msLocation.WLocationCode == _STCReturRRDt.LocationCode
                                                    select _msLocation.WLocationName
                                                  ).FirstOrDefault(),
                                    Unit = _STCReturRRDt.Unit,
                                    Qty = _STCReturRRDt.Qty
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new STCReturRRDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.LocationCode, _row.LocationName, _row.Qty, _row.Unit));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetReqReturNoSTCReturRRHd(string _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                               from _stcReturRRHd in this.db.STCReturRRHds
                               where _stcReturRRHd.TransNmbr == _prmCode
                               select new
                               {
                                   ReqReturNo = _stcReturRRHd.ReqReturNo,
                               }
                           ).Single();

                _result = _query.ReqReturNo;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCReturRRDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            FINCustReturHd _finCustReturHd = new FINCustReturHd();

            decimal _total = 0;
            //Decimal _countAmount = 0;
            try
            {
                //for (int i = 0; i < _prmCode.Length; i++)
                //{
                //    string[] _tempSplit = _prmCode[i].Split('|');
                //    FINCustReturDt _finCustReturDt = this.db.FINCustReturDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.RRNo == _tempSplit[1] && _temp.ProductCode == _tempSplit[2]);
                //    _countAmount = _countAmount + Convert.ToDecimal(_finCustReturDt.AmountForex);
                //    this.db.FINCustReturDts.DeleteOnSubmit(_finCustReturDt);
                //}

                var _queryDt = (from _detail in this.db.FINCustReturDts
                                where _detail.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower()
                                select _detail);

                this.db.FINCustReturDts.DeleteAllOnSubmit(_queryDt);
                
                var _query = (
                                from _finCustReturDt in this.db.FINCustReturDts
                                where _finCustReturDt.TransNmbr == _prmTransNo
                                group _finCustReturDt by _finCustReturDt.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = Convert.ToDecimal(_obj.AmountForex);
                }

                _finCustReturHd = this.db.FINCustReturHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _finCustReturHd.BaseForex = _finCustReturHd.BaseForex - _total;

                if (_finCustReturHd.BaseForex != 0)
                {
                    _finCustReturHd.PPNForex = (Convert.ToDecimal(_finCustReturHd.BaseForex) * _finCustReturHd.PPN / 100);
                    _finCustReturHd.TotalForex = _finCustReturHd.BaseForex + _finCustReturHd.PPNForex;
                }
                else
                {
                    _finCustReturHd.PPNForex = 0;
                    _finCustReturHd.TotalForex = 0;
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

        public FINCustReturDt GetSingleFINCustReturDt(String _prmTransNmbr, String _prmRRno, String _prmProductCode)
        {
            FINCustReturDt _result = null;

            try
            {
                _result = this.db.FINCustReturDts.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.RRNo == _prmRRno && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINCustReturDt(FINCustReturDt _prmFINCustReturDt, Decimal _prmAmount)
        {
            bool _result = false;
            decimal _tempBaseAmount = 0;
            //decimal _tempTex = 0;
            decimal _tempPPNAmount = 0;
            decimal _tempTotalAmount = 0;

            try
            {
                FINCustReturHd _finSuppInvConsignHd = this.GetSingleFINCustReturHd(_prmFINCustReturDt.TransNmbr);

                _tempBaseAmount = Convert.ToDecimal(_finSuppInvConsignHd.BaseForex - _prmFINCustReturDt.AmountForex) + Convert.ToDecimal(_prmAmount);

                //_tempBaseAmount = _directSalesHd.BaseForex + Convert.ToDecimal(_prmAmount);
                _prmFINCustReturDt.AmountForex = _prmAmount;
                //_tempDiscAmount = _directSalesHd.DiscAmount;
                //if (_tempBaseAmount != 0)
                //    _tempDisc = (_finSuppInvConsignHd.DiscForex / _tempBaseAmount) * 100;
                //_tempTex = (_finSuppInvConsignHd.PPNForex / (_finSuppInvConsignHd.BaseForex - _finSuppInvConsignHd.DiscForex)) * 100;

                //if (_tempDisc == 0)
                //{
                //    _tempDiscAmount = _finSuppInvConsignHd.DiscForex;
                //}
                //else
                //{
                //    _tempDiscAmount = Math.Round((_tempBaseAmount * _tempDisc) / 100, 2);
                //}

                if (_finSuppInvConsignHd.PPN == 0)
                {
                    _tempPPNAmount = _finSuppInvConsignHd.PPNForex;
                }
                else
                {
                    if (_tempBaseAmount != 0)
                        _tempPPNAmount = Convert.ToDecimal(Math.Round((_tempBaseAmount * _finSuppInvConsignHd.PPN) / 100, 2));
                    else
                        _tempPPNAmount = 0;
                }
                if (_tempBaseAmount != 0)
                    _tempTotalAmount = _tempBaseAmount + _tempPPNAmount;

                _finSuppInvConsignHd.BaseForex = _tempBaseAmount;
                _finSuppInvConsignHd.PPNForex = _tempPPNAmount;
                _finSuppInvConsignHd.TotalForex = _tempTotalAmount;

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

        ~CustomerReturBL()
        {
        }
    }
}
