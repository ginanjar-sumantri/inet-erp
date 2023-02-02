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
    public sealed class FINReceiptTradeBL : Base
    {
        public FINReceiptTradeBL()
        {
        }

        private FINARRateBL _finARRateBL = new FINARRateBL();

        #region FINARPosting
        public List<FINARPosting> GetListFINARPostingForDDL(string _prmCurrCode, decimal _prmNewRate)
        {
            List<FINARPosting> _result = new List<FINARPosting>();

            try
            {
                var _query = (
                                from _finARPosting in this.db.FINARPostings
                                where _finARPosting.CurrCode == _prmCurrCode
                                && _finARPosting.ForexRate != _prmNewRate
                                && _finARPosting.Amount > _finARPosting.Balance
                                && (_finARPosting.FileNmbr ?? "").Trim() == _finARPosting.FileNmbr.Trim()
                                orderby _finARPosting.InvoiceNo ascending
                                select new
                                {
                                    InvoiceNo = _finARPosting.InvoiceNo,
                                    FileNmbr = _finARPosting.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINARPosting(_row.InvoiceNo, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINARPosting GetSingleFINARPosting(string _prmInvoiceNo)
        {
            FINARPosting _result = null;

            try
            {
                _result = this.db.FINARPostings.Single(_temp => _temp.InvoiceNo == _prmInvoiceNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public bool IsCustCodeExists(string _prmCustCode)
        {
            bool _result = false;

            try
            {
                var _query = from _finARPosting in this.db.FINARPostings
                             where (_finARPosting.CustCode == _prmCustCode)
                             select new
                             {
                                 _finARPosting.CustCode
                             };

                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region FINReceiptTradeHd
        public double RowsCountFINReceiptTradeHd(string _prmCategory, string _prmKeyword)
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
                            from _finReceiptTradeHd in this.db.FINReceiptTradeHds
                            join _msCust in this.db.MsCustomers
                                on _finReceiptTradeHd.CustCode equals _msCust.CustCode
                            where (SqlMethods.Like(_finReceiptTradeHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_finReceiptTradeHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _finReceiptTradeHd.Status != ReceiptTradeDataMapper.GetStatus(TransStatus.Deleted)
                            select _finReceiptTradeHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public int GetMaxNoItemFINReceiptTradeDb(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINReceiptTradeDbs.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public V_FNARPosting GetSingleV_FNARPosting(string _prmInvoiceNo)
        {
            V_FNARPosting _result = null;

            try
            {
                _result = this.db.V_FNARPostings.Single(_temp => _temp.Invoice_No == _prmInvoiceNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFileNmbrFINARPostingByInvoiceNo(string _prmInvoiceNo)
        {
            string _result = null;

            try
            {
                _result = this.db.FINARPostings.Single(_temp => _temp.InvoiceNo == _prmInvoiceNo).FileNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINReceiptTradeHd> GetListFINReceiptTradeHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINReceiptTradeHd> _result = new List<FINReceiptTradeHd>();

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
                                from _finReceiptTradeHd in this.db.FINReceiptTradeHds
                                join _customer in this.db.MsCustomers
                                    on _finReceiptTradeHd.CustCode equals _customer.CustCode
                                where (SqlMethods.Like(_finReceiptTradeHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_customer.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finReceiptTradeHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finReceiptTradeHd.Status != ReceiptTradeDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finReceiptTradeHd.DatePrep descending
                                select new
                                {
                                    ReceiptNo = _finReceiptTradeHd.TransNmbr,
                                    FileNmbr = _finReceiptTradeHd.FileNmbr,
                                    TransDate = _finReceiptTradeHd.TransDate,
                                    Status = _finReceiptTradeHd.Status,
                                    CustCode = _finReceiptTradeHd.CustCode,
                                    CustName = _customer.CustName,
                                    Remark = _finReceiptTradeHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINReceiptTradeHd(_row.ReceiptNo, _row.FileNmbr, _row.TransDate, _row.Status, _row.CustCode, _row.CustName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINReceiptTradeHd GetSingleFINReceiptTradeHd(string _prmCode)
        {
            FINReceiptTradeHd _result = null;

            try
            {
                _result = this.db.FINReceiptTradeHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINReceiptTradeHd GetSingleFINReceiptTradeHdView(string _prmCode)
        {
            FINReceiptTradeHd _result = new FINReceiptTradeHd();

            try
            {
                var _query = (
                               from _finReceiptTradeHd in this.db.FINReceiptTradeHds
                               join _customer in this.db.MsCustomers
                               on _finReceiptTradeHd.CustCode equals _customer.CustCode
                               where _finReceiptTradeHd.TransNmbr == _prmCode
                               orderby _finReceiptTradeHd.DatePrep descending
                               select new
                               {
                                   ReceiptNo = _finReceiptTradeHd.TransNmbr,
                                   FileNmbr = _finReceiptTradeHd.FileNmbr,
                                   TransDate = _finReceiptTradeHd.TransDate,
                                   Status = _finReceiptTradeHd.Status,
                                   CustCode = _finReceiptTradeHd.CustCode,
                                   CustName = _customer.CustName,
                                   Remark = _finReceiptTradeHd.Remark
                               }
                           ).Single();

                _result.TransNmbr = _query.ReceiptNo;
                _result.FileNmbr = _query.FileNmbr;
                _result.TransDate = _query.TransDate;
                _result.Status = _query.Status;
                _result.CustCode = _query.CustCode;
                _result.CustomerName = _query.CustName;
                _result.Remark = _query.Remark;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINReceiptTradeHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINReceiptTradeHd _finReceiptTradeHd = this.db.FINReceiptTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finReceiptTradeHd != null)
                    {
                        if ((_finReceiptTradeHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINReceiptTradeDbs
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINReceiptTradeDbs.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINReceiptTradeCrs
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINReceiptTradeCrs.DeleteAllOnSubmit(_query2);

                            this.db.FINReceiptTradeHds.DeleteOnSubmit(_finReceiptTradeHd);

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

        public string AddFINReceiptTradeHd(FINReceiptTradeHd _prmFINReceiptTradeHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINReceiptTradeHd.TransDate.Year, _prmFINReceiptTradeHd.TransDate.Month, AppModule.GetValue(TransactionType.ReceiptTrade), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINReceiptTradeHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINReceiptTradeHds.InsertOnSubmit(_prmFINReceiptTradeHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                //_result = true;
                _result = _prmFINReceiptTradeHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINReceiptTradeHd(FINReceiptTradeHd _prmFINReceiptTradeHd)
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
                //TEMPORARY SOLUTION
                //BY : DEWI
                //DATE : 3/2/2009
                //TUJUAN : UNTUK MEMENUHI REQUEST INET (PRINT)

                //using (TransactionScope _scope = new TransactionScope())
                //{
                this.db.S_FNReceiptTradeGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    //FINReceiptTradeHd _finReceiptTradeHd = this.GetSingleFINReceiptTradeHd(_prmTransNmbr);
                    //foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_finReceiptTradeHd.TransDate.Year, _finReceiptTradeHd.TransDate.Month, AppModule.GetValue(TransactionType.ReceiptTrade), this._companyTag, ""))
                    //{
                    //    _finReceiptTradeHd.FileNmbr = _item.Number;
                    //}

                    //this.db.SubmitChanges();

                    //_scope.Complete();

                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ReceiptTrade);
                    _transActivity.TransNmbr = _prmTransNmbr;
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
                //}
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
                //TEMPORARY SOLUTION
                //BY : DEWI
                //DATE : 3/2/2009
                //TUJUAN : UNTUK MEMENUHI REQUEST INET (PRINT)

                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_FNReceiptTradeApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINReceiptTradeHd _finReceiptTradeHd = this.GetSingleFINReceiptTradeHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_finReceiptTradeHd.TransDate.Year, _finReceiptTradeHd.TransDate.Month, AppModule.GetValue(TransactionType.ReceiptTrade), this._companyTag, ""))
                        {
                            _finReceiptTradeHd.FileNmbr = _item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ReceiptTrade);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINReceiptTradeHd(_prmTransNmbr).FileNmbr;
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
                FINReceiptTradeHd _finReceiptTradeHd = this.db.FINReceiptTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finReceiptTradeHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNReceiptTradePost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ReceiptTrade);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINReceiptTradeHd(_prmTransNmbr).FileNmbr;
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
                FINReceiptTradeHd _finReceiptTradeHd = this.db.FINReceiptTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finReceiptTradeHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNReceiptTradeUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.ReceiptTrade);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINReceiptTradeHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINReceiptTradeHd(_prmTransNmbr).TransDate;
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

        public char GetFgMode(string _prmPaymentCode)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where _vMsPayType.Payment_Code == _prmPaymentCode
                                select new
                                {
                                    FgMode = _vMsPayType.FgMode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.FgMode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCustCodeHeader(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finReceiptTradeHds in this.db.FINReceiptTradeHds
                                where _finReceiptTradeHds.TransNmbr == _prmCode
                                select new
                                {
                                    CustCode = _finReceiptTradeHds.CustCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.CustCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCurrCodeByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msPayType in this.db.V_MsPayTypes
                                where _msPayType.Payment_Code == _prmCode
                                select new
                                {
                                    CurrCode = _msPayType.CurrCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CurrCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLByViewPayment()
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Receipt))
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleFINReceiptTradeHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINReceiptTradeHd _finReceiptTradeHd = this.db.FINReceiptTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finReceiptTradeHd != null)
                    {
                        if (_finReceiptTradeHd.Status != ReceiptTradeDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINReceiptTradeHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINReceiptTradeHd _finReceiptTradeHd = this.db.FINReceiptTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finReceiptTradeHd.Status == ReceiptTradeDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finReceiptTradeHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finReceiptTradeHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finReceiptTradeHd != null)
                    {
                        if ((_finReceiptTradeHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINReceiptTradeDbs
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINReceiptTradeDbs.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINReceiptTradeCrs
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINReceiptTradeCrs.DeleteAllOnSubmit(_query2);

                            this.db.FINReceiptTradeHds.DeleteOnSubmit(_finReceiptTradeHd);

                            _result = true;
                        }
                        else if (_finReceiptTradeHd.FileNmbr != "" && _finReceiptTradeHd.Status == ReceiptTradeDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finReceiptTradeHd.Status = ReceiptTradeDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINReceiptTradeCr
        public int RowsCountFINReceiptTradeCr
        {
            get
            {
                return this.db.FINReceiptTradeCrs.Count();
            }
        }

        public List<FINReceiptTradeCr> GetListFINReceiptTradeCr(string _prmCode)
        {
            List<FINReceiptTradeCr> _result = new List<FINReceiptTradeCr>();

            try
            {
                var _query = (
                                from _finReceiptTradeCr in this.db.FINReceiptTradeCrs
                                //join _msAccount in this.db.MsAccounts
                                //    on _finReceiptTradeCr.Account equals _msAccount.Account
                                where _finReceiptTradeCr.TransNmbr == _prmCode
                                orderby _finReceiptTradeCr.TransNmbr descending
                                select new
                                {
                                    TransNmbr = _finReceiptTradeCr.TransNmbr,
                                    InvoiceNo = _finReceiptTradeCr.InvoiceNo,
                                    FileNmbr = this.GetFileNmbrFINARPostingByInvoiceNo(_finReceiptTradeCr.InvoiceNo),
                                    CurrCode = _finReceiptTradeCr.CurrCode,
                                    ForexRate = _finReceiptTradeCr.ForexRate,
                                    ARBalance = _finReceiptTradeCr.ARBalance,
                                    ARInvoice = _finReceiptTradeCr.ARInvoice,
                                    ARPaid = _finReceiptTradeCr.ARPaid,
                                    PPnBalance = _finReceiptTradeCr.PPnBalance,
                                    PPnInvoice = _finReceiptTradeCr.PPnInvoice,
                                    PPnPaid = _finReceiptTradeCr.PPnPaid,
                                    PPnRate = _finReceiptTradeCr.PPnRate,
                                    AmountInvoice = _finReceiptTradeCr.AmountInvoice,
                                    AmountBalance = _finReceiptTradeCr.AmountBalance,
                                    AmountForex = _finReceiptTradeCr.AmountForex,
                                    AmountHome = _finReceiptTradeCr.AmountHome,
                                    Remark = _finReceiptTradeCr.Remark,
                                    Account = _finReceiptTradeCr.Account,
                                    AccountName = (
                                                        from _msAccount in this.db.MsAccounts
                                                        where _msAccount.Account == _finReceiptTradeCr.Account
                                                        select _msAccount.AccountName
                                                      ).FirstOrDefault(),
                                    FgSubLed = _finReceiptTradeCr.FgSubLed,
                                    FgValue = _finReceiptTradeCr.FgValue
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINReceiptTradeCr(
                                    _row.TransNmbr,
                                    _row.InvoiceNo,
                                    _row.FileNmbr,
                                    _row.CurrCode,
                                    _row.ForexRate,
                                    _row.ARBalance,
                                    _row.ARInvoice,
                                    _row.ARPaid,
                                    _row.PPnBalance,
                                    _row.PPnInvoice,
                                    _row.PPnPaid,
                                    _row.PPnRate,
                                    _row.AmountInvoice,
                                    _row.AmountBalance,
                                    _row.AmountForex,
                                    _row.AmountHome,
                                    _row.Remark,
                                    _row.Account,
                                    _row.AccountName,
                                    _row.FgSubLed,
                                    _row.FgValue

                    ));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINReceiptTradeCr GetSingleFINReceiptTradeCr(string _prmCode, string _prmInvoiceNo)
        {
            FINReceiptTradeCr _result = null;

            try
            {
                _result = this.db.FINReceiptTradeCrs.Single(_temp => _temp.TransNmbr == _prmCode && _temp.InvoiceNo == _prmInvoiceNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public FINCNCustHd GetSingleFINCNCustHdView(string _prmCode)
        //{
        //    FINCNCustHd _result = new FINCNCustHd();

        //    try
        //    {
        //        var _query = (
        //                       from _finCNCustHd in this.db.FINCNCustHds
        //                       join _msCust in this.db.MsCustomers
        //                            on _finCNCustHd.CustCode equals _msCust.CustCode
        //                       join _msTerm in this.db.MsTerms
        //                            on _finCNCustHd.Term equals _msTerm.TermCode
        //                       orderby _finCNCustHd.DatePrep descending
        //                       where _finCNCustHd.TransNmbr == _prmCode
        //                       select new
        //                       {
        //                           TransNmbr = _finCNCustHd.TransNmbr,
        //                           TransDate = _finCNCustHd.TransDate,
        //                           Status = _finCNCustHd.Status,
        //                           CustCode = _finCNCustHd.CustCode,
        //                           CustName = _msCust.CustName,
        //                           CurrCode = _finCNCustHd.CurrCode,
        //                           ForexRate = _finCNCustHd.ForexRate,
        //                           TotalForex = _finCNCustHd.TotalForex,
        //                           Term = _finCNCustHd.Term,
        //                           TermName = _msTerm.TermName,
        //                           DiscForex = _finCNCustHd.DiscForex,
        //                           BaseForex = _finCNCustHd.BaseForex,
        //                           Attn = _finCNCustHd.Attn,
        //                           Remark = _finCNCustHd.Remark,
        //                           CNCustNo = _finCNCustHd.CNCustNo
        //                       }
        //                   ).Single();

        //        _result.TransNmbr = _query.TransNmbr;
        //        _result.TransDate = _query.TransDate;
        //        _result.Status = _query.Status;
        //        _result.CustCode = _query.CustCode;
        //        _result.CustName = _query.CustName;
        //        _result.CurrCode = _query.CurrCode;
        //        _result.ForexRate = _query.ForexRate;
        //        _result.TotalForex = _query.TotalForex;
        //        _result.Term = _query.Term;
        //        _result.TermName = _query.TermName;
        //        _result.DiscForex = _query.DiscForex;
        //        _result.BaseForex = _query.BaseForex;
        //        _result.Attn = _query.Attn;
        //        _result.Remark = _query.Remark;
        //        _result.CNCustNo = _query.CNCustNo;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool DeleteMultiFINReceiptTradeCr(string[] _prmInvoiceNo, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmInvoiceNo.Length; i++)
                {
                    FINReceiptTradeCr _finReceiptTradeCr = this.db.FINReceiptTradeCrs.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower() && _temp.InvoiceNo == _prmInvoiceNo[i].Trim().ToLower());

                    this.db.FINReceiptTradeCrs.DeleteOnSubmit(_finReceiptTradeCr);
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

        public Boolean AddFINReceiptTradeCr(FINReceiptTradeCr _prmFINReceiptTradeCr)
        {
            Boolean _result = false;

            try
            {
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINCNCustHd.TransDate.Year, _prmFINCNCustHd.TransDate.Month, new AppModule().GetValue(TransactionType.ReceiptTrade), ""))
                //{
                //    _prmFINCNCustHd.TransNmbr = item.Number;
                //}

                this.db.FINReceiptTradeCrs.InsertOnSubmit(_prmFINReceiptTradeCr);
                this.db.SubmitChanges();

                //_result = _prmFINReceiptTradeCr.TransNmbr;
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINReceiptTradeCr(FINReceiptTradeCr _prmFINReceiptTradeCr)
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

        #region FINReceiptTradeDb
        public int RowsCountFINReceiptTradeDb
        {
            get
            {
                return this.db.FINReceiptTradeDbs.Count();
            }
        }

        public List<FINReceiptTradeDb> GetListFINReceiptTradeDb(string _prmCode)
        {
            List<FINReceiptTradeDb> _result = new List<FINReceiptTradeDb>();

            try
            {
                var _query = (
                                from _finReceiptTradeDb in this.db.FINReceiptTradeDbs
                                join _payType in this.db.MsPayTypes
                                on _finReceiptTradeDb.ReceiptType equals _payType.PayCode
                                where _finReceiptTradeDb.TransNmbr == _prmCode
                                orderby _finReceiptTradeDb.TransNmbr descending
                                select new
                                {
                                    TransNmbr = _finReceiptTradeDb.TransNmbr,
                                    ItemNo = _finReceiptTradeDb.ItemNo,
                                    ReceiptType = _finReceiptTradeDb.ReceiptType,
                                    PayName = _payType.PayName,
                                    DocumentNo = _finReceiptTradeDb.DocumentNo,
                                    CurrCode = _finReceiptTradeDb.CurrCode,
                                    ForexRate = _finReceiptTradeDb.ForexRate,
                                    AmountForex = _finReceiptTradeDb.AmountForex,
                                    AmountHome = _finReceiptTradeDb.AmountHome,
                                    Remark = _finReceiptTradeDb.Remark,
                                    FgGiro = _finReceiptTradeDb.FgGiro,
                                    FgDP = _finReceiptTradeDb.FgDP,
                                    BankGiro = _finReceiptTradeDb.BankGiro,
                                    DueDate = _finReceiptTradeDb.DueDate,
                                    BankExpense = _finReceiptTradeDb.BankExpense,
                                    CustRevenue = _finReceiptTradeDb.CustRevenue,
                                    AccBank = _finReceiptTradeDb.AccBank,
                                    AccBankName = (from _a in this.db.MsAccounts
                                                   where _a.Account == _finReceiptTradeDb.AccBank
                                                   select _a.AccountName).FirstOrDefault(),
                                    FgBank = _finReceiptTradeDb.FgBank,
                                    AccCharges = _finReceiptTradeDb.AccCharges,
                                    AccChargeName = (from _b in this.db.MsAccounts
                                                     where _b.Account == _finReceiptTradeDb.AccCharges
                                                     select _b.AccountName).FirstOrDefault(),
                                    FgCharges = _finReceiptTradeDb.FgCharges,
                                    ReceiptForex = _finReceiptTradeDb.ReceiptForex,
                                    ReceiptHome = _finReceiptTradeDb.ReceiptHome

                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINReceiptTradeDb(
                                    _row.TransNmbr,
                                    _row.ItemNo,
                                    _row.ReceiptType,
                                    _row.PayName,
                                    _row.DocumentNo,
                                    _row.CurrCode,
                                    _row.ForexRate,
                                    _row.AmountForex,
                                    _row.AmountHome,
                                    _row.Remark,
                                    _row.FgGiro,
                                    _row.FgDP,
                                    _row.BankGiro,
                                    _row.DueDate,
                                    _row.BankExpense,
                                    _row.CustRevenue,
                                    _row.AccBank,
                                    _row.AccBankName,
                                    _row.FgBank,
                                    _row.AccCharges,
                                    _row.AccChargeName,
                                    _row.FgCharges,
                                    _row.ReceiptForex,
                                    _row.ReceiptHome
                        ));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINReceiptTradeDb GetSingleFINReceiptTradeDb(string _prmCode, string _prmItemNo)
        {
            FINReceiptTradeDb _result = null;

            try
            {
                _result = this.db.FINReceiptTradeDbs.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == Convert.ToInt32(_prmItemNo));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public FINCNCustHd GetSingleFINCNCustHdView(string _prmCode)
        //{
        //    FINCNCustHd _result = new FINCNCustHd();

        //    try
        //    {
        //        var _query = (
        //                       from _finCNCustHd in this.db.FINCNCustHds
        //                       join _msCust in this.db.MsCustomers
        //                            on _finCNCustHd.CustCode equals _msCust.CustCode
        //                       join _msTerm in this.db.MsTerms
        //                            on _finCNCustHd.Term equals _msTerm.TermCode
        //                       orderby _finCNCustHd.DatePrep descending
        //                       where _finCNCustHd.TransNmbr == _prmCode
        //                       select new
        //                       {
        //                           TransNmbr = _finCNCustHd.TransNmbr,
        //                           TransDate = _finCNCustHd.TransDate,
        //                           Status = _finCNCustHd.Status,
        //                           CustCode = _finCNCustHd.CustCode,
        //                           CustName = _msCust.CustName,
        //                           CurrCode = _finCNCustHd.CurrCode,
        //                           ForexRate = _finCNCustHd.ForexRate,
        //                           TotalForex = _finCNCustHd.TotalForex,
        //                           Term = _finCNCustHd.Term,
        //                           TermName = _msTerm.TermName,
        //                           DiscForex = _finCNCustHd.DiscForex,
        //                           BaseForex = _finCNCustHd.BaseForex,
        //                           Attn = _finCNCustHd.Attn,
        //                           Remark = _finCNCustHd.Remark,
        //                           CNCustNo = _finCNCustHd.CNCustNo
        //                       }
        //                   ).Single();

        //        _result.TransNmbr = _query.TransNmbr;
        //        _result.TransDate = _query.TransDate;
        //        _result.Status = _query.Status;
        //        _result.CustCode = _query.CustCode;
        //        _result.CustName = _query.CustName;
        //        _result.CurrCode = _query.CurrCode;
        //        _result.ForexRate = _query.ForexRate;
        //        _result.TotalForex = _query.TotalForex;
        //        _result.Term = _query.Term;
        //        _result.TermName = _query.TermName;
        //        _result.DiscForex = _query.DiscForex;
        //        _result.BaseForex = _query.BaseForex;
        //        _result.Attn = _query.Attn;
        //        _result.Remark = _query.Remark;
        //        _result.CNCustNo = _query.CNCustNo;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool DeleteMultiFINReceiptTradeDb(string[] _prmItemNo, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItemNo.Length; i++)
                {
                    FINReceiptTradeDb _finReceiptTradeDb = this.db.FINReceiptTradeDbs.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmItemNo[i]) && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.FINReceiptTradeDbs.DeleteOnSubmit(_finReceiptTradeDb);
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

        public bool AddFINReceiptTradeDb(FINReceiptTradeDb _prmFINReceiptTradeDb)
        {
            bool _result = false;

            try
            {
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINCNCustHd.TransDate.Year, _prmFINCNCustHd.TransDate.Month, new AppModule().GetValue(TransactionType.ReceiptTrade), ""))
                //{
                //    _prmFINCNCustHd.TransNmbr = item.Number;
                //}

                this.db.FINReceiptTradeDbs.InsertOnSubmit(_prmFINReceiptTradeDb);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINReceiptTradeDb(FINReceiptTradeDb _prmFINReceiptTradeDb)
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

        public List<FINReceiptTradeCr> GetListInvoiceNoForDDL(string _prmCode)
        {
            List<FINReceiptTradeCr> _result = new List<FINReceiptTradeCr>();

            try
            {
                var _query = (
                                from _v_FNARPosting in this.db.V_FNARPostings
                                where _v_FNARPosting.Cust_Code == _prmCode
                                    && _v_FNARPosting.Amount_Saldo > 0
                                    && (_v_FNARPosting.FileNmbr ?? "").Trim() == _v_FNARPosting.FileNmbr.Trim()
                                orderby _v_FNARPosting.Invoice_No descending
                                select new
                                {
                                    InvoiceNo = _v_FNARPosting.Invoice_No,
                                    FileNmbr = _v_FNARPosting.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINReceiptTradeCr(_row.InvoiceNo, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~FINReceiptTradeBL()
        {
        }
    }
}
