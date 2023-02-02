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
    public sealed class CustomerNoteBL : Base
    {
        public CustomerNoteBL()
        {

        }

        #region FINCustInvHd
        public double RowsCountFINCustInvHd(string _prmCategory, string _prmKeyword)
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
                           from _finCustInvHd in this.db.FINCustInvHds
                           join _msCust in this.db.MsCustomers
                               on _finCustInvHd.CustCode equals _msCust.CustCode
                           where (SqlMethods.Like(_finCustInvHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                              && (SqlMethods.Like((_finCustInvHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                              && _finCustInvHd.Status != CustomerNoteDataMapper.GetStatus(TransStatus.Deleted)
                           select _finCustInvHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINCustInvHd> GetListFINCustInvHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINCustInvHd> _result = new List<FINCustInvHd>();

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
                                from _finCustInvHd in this.db.FINCustInvHds
                                join _msCust in this.db.MsCustomers
                                    on _finCustInvHd.CustCode equals _msCust.CustCode
                                where (SqlMethods.Like(_finCustInvHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finCustInvHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finCustInvHd.Status != CustomerNoteDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finCustInvHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finCustInvHd.TransNmbr,
                                    FileNmbr = _finCustInvHd.FileNmbr,
                                    TransDate = _finCustInvHd.TransDate,
                                    CurrCode = _finCustInvHd.CurrCode,
                                    Status = _finCustInvHd.Status,
                                    CustCode = _finCustInvHd.CustCode,
                                    CustName = _msCust.CustName,
                                    Term = _finCustInvHd.Term,
                                    TermName = (
                                                    from _msTerm in this.db.MsTerms
                                                    where _finCustInvHd.Term == _msTerm.TermCode
                                                    select _msTerm.TermName
                                                ).FirstOrDefault(),
                                    BillTo = _finCustInvHd.BillTo,
                                    BillToName = (
                                                    from _msCustomer in this.db.MsCustomers
                                                    where _msCustomer.CustCode == _finCustInvHd.BillTo
                                                    select _msCustomer.CustName
                                                 ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINCustInvHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.CustCode, _row.CustName, _row.Term, _row.TermName, _row.BillTo, _row.BillToName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCustInvHd GetSingleFINCustInvHd(string _prmCode)
        {
            FINCustInvHd _result = null;

            try
            {
                _result = this.db.FINCustInvHds.Single(_temp => _temp.TransNmbr.ToLower() == _prmCode.ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCustInvHd GetSingleFINCustInvHdView(string _prmCode)
        {
            FINCustInvHd _result = new FINCustInvHd();

            try
            {
                var _query = (
                               from _finCustInvHd in this.db.FINCustInvHds
                               join _msCust in this.db.MsCustomers
                                    on _finCustInvHd.CustCode equals _msCust.CustCode
                               join _msTerm in this.db.MsTerms
                                    on _finCustInvHd.Term equals _msTerm.TermCode
                               orderby _finCustInvHd.DatePrep descending
                               where _finCustInvHd.TransNmbr == _prmCode
                               select new
                               {
                                   TransNmbr = _finCustInvHd.TransNmbr,
                                   FileNmbr = _finCustInvHd.FileNmbr,
                                   TransDate = _finCustInvHd.TransDate,
                                   Status = _finCustInvHd.Status,
                                   CustCode = _finCustInvHd.CustCode,
                                   CustName = _msCust.CustName,
                                   CurrCode = _finCustInvHd.CurrCode,
                                   ForexRate = _finCustInvHd.ForexRate,
                                   OtherForex = _finCustInvHd.OtherForex,
                                   TotalForex = _finCustInvHd.TotalForex,
                                   Term = _finCustInvHd.Term,
                                   TermName = _msTerm.TermName,
                                   DiscForex = _finCustInvHd.DiscForex,
                                   BaseForex = _finCustInvHd.BaseForex,
                                   Attn = _finCustInvHd.Attn,
                                   Remark = _finCustInvHd.Remark,
                                   PPN = _finCustInvHd.PPN,
                                   PPNDate = _finCustInvHd.PPNDate,
                                   PPNForex = _finCustInvHd.PPNForex,
                                   PPNNo = _finCustInvHd.PPNNo,
                                   PPNRate = _finCustInvHd.PPNRate,
                                   BillTo = _finCustInvHd.BillTo,
                                   BillToName = new CustomerBL().GetNameByCode(_finCustInvHd.BillTo)
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
                _result.OtherForex = _query.OtherForex;
                _result.TotalForex = _query.TotalForex;
                _result.Term = _query.Term;
                _result.TermName = _query.TermName;
                _result.DiscForex = _query.DiscForex;
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

        public bool DeleteMultiFINCustInvHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCustInvHd _finCustInvHd = this.db.FINCustInvHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finCustInvHd != null)
                    {
                        if ((_finCustInvHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINCustInvDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            var _query1 = (from _detail1 in this.db.FINCustInvSJLists
                                           where _detail1.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail1);

                            this.db.FINCustInvSJLists.DeleteAllOnSubmit(_query1);

                            this.db.FINCustInvDts.DeleteAllOnSubmit(_query);

                            this.db.FINCustInvHds.DeleteOnSubmit(_finCustInvHd);

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

        public string AddFINCustInvHd(FINCustInvHd _prmFINCustInvHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINCustInvHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.FINCustInvHds.InsertOnSubmit(_prmFINCustInvHd);
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINCustInvHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINCustInvHd(FINCustInvHd _prmFINCustInvHd)
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
                int _success = this.db.S_FNCustInvGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.CustomerNote);
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
                    int _success = this.db.S_FNCustInvApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINCustInvHd _finCustInvHd = this.GetSingleFINCustInvHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_finCustInvHd.TransDate.Year, _finCustInvHd.TransDate.Month, AppModule.GetValue(TransactionType.CustomerNote), this._companyTag, ""))
                        {
                            _finCustInvHd.FileNmbr = _item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.CustomerNote);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINCustInvHd(_prmTransNmbr).FileNmbr;
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
                FINCustInvHd _finCustInvHd = this.db.FINCustInvHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finCustInvHd.TransDate);

                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNCustInvPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.CustomerNote);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINCustInvHd(_prmTransNmbr).FileNmbr;
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
                FINCustInvHd _finCustInvHd = this.db.FINCustInvHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finCustInvHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNCustInvUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        //_result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.CustomerNote);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINCustInvHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINCustInvHd(_prmTransNmbr).TransDate;
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

        public bool GetSingleFINCustInvHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCustInvHd _finCustInvHd = this.db.FINCustInvHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finCustInvHd != null)
                    {
                        if (_finCustInvHd.Status != CustomerNoteDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINCustInvHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINCustInvHd _finCustInvHd = this.db.FINCustInvHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finCustInvHd.Status == CustomerNoteDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finCustInvHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finCustInvHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finCustInvHd != null)
                    {
                        if ((_finCustInvHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINCustInvDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            var _query1 = (from _detail1 in this.db.FINCustInvSJLists
                                           where _detail1.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail1);

                            this.db.FINCustInvSJLists.DeleteAllOnSubmit(_query1);

                            this.db.FINCustInvDts.DeleteAllOnSubmit(_query);

                            this.db.FINCustInvHds.DeleteOnSubmit(_finCustInvHd);

                            _result = true;
                        }
                        else if (_finCustInvHd.FileNmbr != "" && _finCustInvHd.Status == CustomerNoteDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finCustInvHd.Status = CustomerNoteDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINCustInvDt
        public int RowsCountFINCustInvDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINCustInvDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINCustInvDt> GetListFINCustInvDt(string _prmCode)
        {
            List<FINCustInvDt> _result = new List<FINCustInvDt>();

            try
            {
                var _query = (
                                from _finCustInvDt in this.db.FINCustInvDts
                                join _msProduct in this.db.MsProducts
                                    on _finCustInvDt.ProductCode equals _msProduct.ProductCode
                                join _msUnit in this.db.MsUnits
                                    on _finCustInvDt.Unit equals _msUnit.UnitCode
                                where _finCustInvDt.TransNmbr == _prmCode
                                orderby _finCustInvDt.SJNo ascending
                                select new
                                {
                                    SJNo = _finCustInvDt.SJNo,
                                    FileNmbr = this.GetFileNmbrV_STSJForCIsBySJNo(_finCustInvDt.SJNo),
                                    ProductCode = _finCustInvDt.ProductCode,
                                    ProductName = _msProduct.ProductName,
                                    SONo = _finCustInvDt.SONo,
                                    Qty = _finCustInvDt.Qty,
                                    Unit = _finCustInvDt.Unit,
                                    UnitName = _msUnit.UnitName,
                                    PriceForex = _finCustInvDt.PriceForex,
                                    AmountForex = _finCustInvDt.AmountForex,
                                    Remark = _finCustInvDt.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINCustInvDt(_row.SJNo, _row.FileNmbr, _row.ProductCode, _row.ProductName, _row.SONo, _row.Qty, _row.Unit, _row.UnitName, _row.PriceForex, _row.AmountForex, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCustInvDt GetSingleFINCustInvDt(string _prmCode, string _prmSJNo, string _prmProductCode)
        {
            FINCustInvDt _result = null;

            try
            {
                _result = this.db.FINCustInvDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.SJNo == _prmSJNo && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINCustInvDt GetSingleFINCustInvDtView(string _prmCode, string _prmSJNo, string _prmProductCode)
        {
            FINCustInvDt _result = new FINCustInvDt();

            try
            {
                var _query = (
                               from _finCustInvDt in this.db.FINCustInvDts
                               join _msProduct in this.db.MsProducts
                                    on _finCustInvDt.ProductCode equals _msProduct.ProductCode
                               join _msUnit in this.db.MsUnits
                                    on _finCustInvDt.Unit equals _msUnit.UnitCode
                               orderby _finCustInvDt.SJNo ascending
                               where _finCustInvDt.TransNmbr == _prmCode && _finCustInvDt.SJNo == _prmSJNo && _finCustInvDt.ProductCode == _prmProductCode
                               select new
                               {
                                   SJNo = _finCustInvDt.SJNo,
                                   ProductCode = _finCustInvDt.ProductCode,
                                   ProductName = _msProduct.ProductName,
                                   SONo = _finCustInvDt.SONo,
                                   Qty = _finCustInvDt.Qty,
                                   Unit = _finCustInvDt.Unit,
                                   UnitName = _msUnit.UnitName,
                                   PriceForex = _finCustInvDt.PriceForex,
                                   AmountForex = _finCustInvDt.AmountForex,
                                   Remark = _finCustInvDt.Remark
                               }
                           ).Single();

                _result.SJNo = _query.SJNo;
                _result.ProductCode = _query.ProductCode;
                _result.ProductName = _query.ProductName;
                _result.SONo = _query.SONo;
                _result.Qty = _query.Qty;
                _result.Unit = _query.Unit;
                _result.UnitName = _query.UnitName;
                _result.PriceForex = _query.PriceForex;
                _result.AmountForex = _query.AmountForex;
                _result.Remark = _query.Remark;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINCustInvDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            FINCustInvHd _finCustInvHd = new FINCustInvHd();

            decimal _total = 0;

            try
            {

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    FINCustInvDt _finCustInvDt = this.db.FINCustInvDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.SJNo.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.FINCustInvDts.DeleteOnSubmit(_finCustInvDt);
                }

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    var _query = (
                                    from _finCustInvDt2 in this.db.FINCustInvDts
                                    where !(
                                                from _finCustInvDt3 in this.db.FINCustInvDts
                                                where _finCustInvDt3.ProductCode == _tempSplit[0] && _finCustInvDt3.SJNo == _tempSplit[1] && _finCustInvDt3.TransNmbr == _prmTransNo
                                                select new { _finCustInvDt3.SJNo, _finCustInvDt3.ProductCode }
                                       ).Contains(new { _finCustInvDt2.SJNo, _finCustInvDt2.ProductCode })
                                       && _finCustInvDt2.TransNmbr == _prmTransNo
                                    group _finCustInvDt2 by _finCustInvDt2.TransNmbr into _grp
                                    select new
                                    {
                                        AmountForex = _grp.Sum(a => a.AmountForex)
                                    }
                                  );

                    foreach (var _obj in _query)
                    {
                        _total = _obj.AmountForex;
                    }
                }

                _finCustInvHd = this.db.FINCustInvHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _finCustInvHd.BaseForex = _total;
                _finCustInvHd.PPNForex = Convert.ToDecimal((_finCustInvHd.BaseForex - _finCustInvHd.DiscForex) * _finCustInvHd.PPN / 100);
                _finCustInvHd.TotalForex = _finCustInvHd.BaseForex - _finCustInvHd.DiscForex + _finCustInvHd.PPNForex + _finCustInvHd.OtherForex;

                if (_finCustInvHd.BaseForex == 0)
                {
                    _finCustInvHd.PPNForex = 0;
                    _finCustInvHd.TotalForex = 0;
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

        public bool DeleteMultiFINCustInvSJList(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    var _queryDt2 = (
                                        from _finCustInvSJList in this.db.FINCustInvSJLists
                                        where _finCustInvSJList.TransNmbr == _tempSplit[0]
                                            && _finCustInvSJList.SJNo == _tempSplit[1]
                                        select _finCustInvSJList
                                    );

                    this.db.FINCustInvSJLists.DeleteAllOnSubmit(_queryDt2);

                    var _queryDt = (
                                        from _finCustInvDt in this.db.FINCustInvDts
                                        where _finCustInvDt.TransNmbr == _tempSplit[0]
                                        select _finCustInvDt
                                    );

                    this.db.FINCustInvDts.DeleteAllOnSubmit(_queryDt);

                    FINCustInvHd _finCustInvHd = this.db.FINCustInvHds.Single(_temp => _temp.TransNmbr == _tempSplit[0]);
                    _finCustInvHd.BaseForex = 0;
                    _finCustInvHd.DiscForex = 0;
                    _finCustInvHd.PPNForex = 0;
                    _finCustInvHd.OtherForex = 0;
                    _finCustInvHd.TotalForex = 0;
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


        public bool AddFINCustInvDt(FINCustInvDt _prmFINCustInvDt)
        {
            bool _result = false;

            FINCustInvHd _finCustInvHd = new FINCustInvHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finCustInvDt in this.db.FINCustInvDts
                               where !(
                                           from _finCustInvDt2 in this.db.FINCustInvDts
                                           where _finCustInvDt2.SJNo == _prmFINCustInvDt.SJNo && _finCustInvDt2.ProductCode == _prmFINCustInvDt.ProductCode && _finCustInvDt2.TransNmbr == _prmFINCustInvDt.TransNmbr
                                           select new { _finCustInvDt2.SJNo, _finCustInvDt2.ProductCode }
                                       ).Contains(new { _finCustInvDt.SJNo, _finCustInvDt.ProductCode })
                                       && _finCustInvDt.TransNmbr == _prmFINCustInvDt.TransNmbr
                               group _finCustInvDt by _finCustInvDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finCustInvHd = this.db.FINCustInvHds.Single(_fa => _fa.TransNmbr == _prmFINCustInvDt.TransNmbr);

                _finCustInvHd.BaseForex = _total + _prmFINCustInvDt.AmountForex;
                _finCustInvHd.PPNForex = Convert.ToDecimal((_finCustInvHd.BaseForex - _finCustInvHd.DiscForex) * _finCustInvHd.PPN / 100);
                _finCustInvHd.TotalForex = _finCustInvHd.BaseForex - _finCustInvHd.DiscForex + _finCustInvHd.PPNForex + _finCustInvHd.OtherForex;

                this.db.FINCustInvDts.InsertOnSubmit(_prmFINCustInvDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINCustInvDt(FINCustInvDt _prmFINCustInvDt)
        {
            bool _result = false;

            FINCustInvHd _finCustInvHd = new FINCustInvHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _finCustInvDt in this.db.FINCustInvDts
                               where !(
                                           from _finCustInvDt2 in this.db.FINCustInvDts
                                           where _finCustInvDt2.SJNo == _prmFINCustInvDt.SJNo && _finCustInvDt2.ProductCode == _prmFINCustInvDt.ProductCode && _finCustInvDt2.TransNmbr == _prmFINCustInvDt.TransNmbr
                                           select new { _finCustInvDt2.SJNo, _finCustInvDt2.ProductCode }
                                       ).Contains(new { _finCustInvDt.SJNo, _finCustInvDt.ProductCode })
                                       && _finCustInvDt.TransNmbr == _prmFINCustInvDt.TransNmbr
                               group _finCustInvDt by _finCustInvDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _finCustInvHd = this.db.FINCustInvHds.Single(_fa => _fa.TransNmbr == _prmFINCustInvDt.TransNmbr);

                _finCustInvHd.BaseForex = _total + _prmFINCustInvDt.AmountForex;
                _finCustInvHd.PPNForex = Convert.ToDecimal((_finCustInvHd.BaseForex - _finCustInvHd.DiscForex) * _finCustInvHd.PPN / 100);
                _finCustInvHd.TotalForex = _finCustInvHd.BaseForex - _finCustInvHd.DiscForex + _finCustInvHd.PPNForex + _finCustInvHd.OtherForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public int GetMaxNoItemFINCustInvDt(string _prmCode, string _prmSJNo)
        //{
        //    int _result = 0;

        //    try
        //    {
        //        _result = this.db.FINCustInvDts.Where(_a => _a.TransNmbr == _prmCode && _a.SJNo == _prmSJNo).Max(_max => _max.SJNo);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool GenerateFINCustInvDt(string _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                var _custInvDt = (
                                        from _finCustInvDt in this.db.FINCustInvDts
                                        where _finCustInvDt.TransNmbr == _prmTransNmbr
                                        select _finCustInvDt
                                   );

                this.db.FINCustInvDts.DeleteAllOnSubmit(_custInvDt);

                var _querySJList = (
                                from _finCustInvSJList in this.db.FINCustInvSJLists
                                join _stcSJHd in this.db.STCSJHds
                                    on _finCustInvSJList.SJNo equals _stcSJHd.TransNmbr
                                join _stcSJDt in this.db.STCSJDts
                                    on _stcSJHd.TransNmbr equals _stcSJDt.TransNmbr
                                //join _mktSOHd in this.db.MKTSOHds
                                //    on _stcSJHd.SONo equals _mktSOHd.TransNmbr
                                //join _mktSODt in this.db.MKTSODts
                                //    on _mktSOHd.TransNmbr equals _mktSODt.TransNmbr
                                where _finCustInvSJList.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    TransNmbr = _finCustInvSJList.TransNmbr,
                                    SJNo = _finCustInvSJList.SJNo,
                                    SJNoFileNmbr = _stcSJHd.FileNmbr,
                                    Remark = _finCustInvSJList.Remark,
                                    SONo = _stcSJHd.SONo,
                                    ProductCode = _stcSJDt.ProductCode,
                                    Qty = _stcSJDt.Qty,
                                    Unit = _stcSJDt.Unit,
                                    RemarkDt = _stcSJDt.Remark,
                                    Price = new SalesOrderBL().GetPriceMKTSODtLastRevision(_stcSJHd.SONo, _stcSJDt.ProductCode, _stcSJDt.ItemID)
                                }
                             );

                FINCustInvHd _finCustInvHd = new FINCustInvHd();

                decimal _total = 0;
                decimal _total2 = 0;

                foreach (var _item in _querySJList)
                {
                    FINCustInvDt _finCustInvDtTab = new FINCustInvDt();

                    _finCustInvDtTab.TransNmbr = _prmTransNmbr;
                    _finCustInvDtTab.SJNo = _item.SJNo;
                    _finCustInvDtTab.SONo = _item.SONo;
                    _finCustInvDtTab.ProductCode = _item.ProductCode;
                    _finCustInvDtTab.Qty = _item.Qty;
                    _finCustInvDtTab.Unit = _item.Unit;
                    _finCustInvDtTab.Remark = _item.RemarkDt;
                    _finCustInvDtTab.PriceForex = Convert.ToDecimal(_item.Price);
                    _finCustInvDtTab.AmountForex = Convert.ToDecimal(_item.Price) * _item.Qty;

                    var _query = (
                               from _finCustInvDt in this.db.FINCustInvDts
                               where !(
                                           from _finCustInvDt2 in this.db.FINCustInvDts
                                           where _finCustInvDt2.SJNo == _finCustInvDtTab.SJNo && _finCustInvDt.ProductCode == _finCustInvDtTab.ProductCode && _finCustInvDt2.TransNmbr == _finCustInvDtTab.TransNmbr
                                           select new { _finCustInvDt2.SJNo, _finCustInvDt2.ProductCode }
                                       ).Contains(new { _finCustInvDt.SJNo, _finCustInvDt.ProductCode })
                                       && _finCustInvDt.TransNmbr == _finCustInvDtTab.TransNmbr
                               group _finCustInvDt by _finCustInvDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                    foreach (var _obj in _query)
                    {
                        _total = _obj.AmountForex;
                    }

                    _finCustInvHd = this.db.FINCustInvHds.Single(_fa => _fa.TransNmbr == _finCustInvDtTab.TransNmbr);

                    _total2 = _total2 + _finCustInvDtTab.AmountForex;
                    _finCustInvHd.BaseForex = _total2;
                    _finCustInvHd.PPNForex = Convert.ToDecimal((_finCustInvHd.BaseForex - _finCustInvHd.DiscForex) * _finCustInvHd.PPN / 100);
                    _finCustInvHd.TotalForex = _finCustInvHd.BaseForex - _finCustInvHd.DiscForex + _finCustInvHd.PPNForex + _finCustInvHd.OtherForex;

                    this.db.FINCustInvDts.InsertOnSubmit(_finCustInvDtTab);
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

        #endregion

        #region FINCustInvSJList
        public List<FINCustInvSJList> GetListFINCustInvSJList(String _prmTransNo)
        {
            List<FINCustInvSJList> _result = new List<FINCustInvSJList>();

            try
            {
                var _query = (
                                from _finCustInvSJList in this.db.FINCustInvSJLists
                                join _stcSJHd in this.db.STCSJHds
                                    on _finCustInvSJList.SJNo equals _stcSJHd.TransNmbr
                                where _finCustInvSJList.TransNmbr == _prmTransNo
                                orderby _stcSJHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _finCustInvSJList.TransNmbr,
                                    SJNo = _finCustInvSJList.SJNo,
                                    FileNmbr = _stcSJHd.FileNmbr,
                                    Customer = (
                                                   from _msCust in this.db.MsCustomers
                                                   where _msCust.CustCode == _stcSJHd.CustCode
                                                   select _msCust.CustName
                                               ).FirstOrDefault(),
                                    SONo = (
                                                   from _so in this.db.MKTSOHds
                                                   where _so.TransNmbr == _stcSJHd.SONo
                                                   select _so.FileNmbr
                                               ).FirstOrDefault(),
                                    Remark = _finCustInvSJList.Remark
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new FINCustInvSJList(_row.TransNmbr, _row.SJNo, _row.FileNmbr, _row.Customer, _row.SONo, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINCustInvSJList(FINCustInvSJList _prmFINCustInvSJList)
        {
            bool _result = false;

            try
            {
                this.db.FINCustInvSJLists.InsertOnSubmit(_prmFINCustInvSJList);

                var _query = (
                                from _finCustInvDt in this.db.FINCustInvDts
                                where _finCustInvDt.TransNmbr == _prmFINCustInvSJList.TransNmbr
                                select _finCustInvDt
                             );
                this.db.FINCustInvDts.DeleteAllOnSubmit(_query);

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

        ~CustomerNoteBL()
        {
        }
    }
}
