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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class FINDPCustomerListBL : Base
    {
        public FINDPCustomerListBL()
        {

        }

        #region FINDPCustList
        public double RowsCountFINDPCustList(string _prmCategory, string _prmKeyword)
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
                            from _finDPCustList in this.db.FINDPCustLists
                            join _msCust in this.db.MsCustomers
                                on _finDPCustList.CustCode equals _msCust.CustCode
                            where (SqlMethods.Like(_finDPCustList.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_finDPCustList.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _finDPCustList.Status != DPCustListDataMapper.GetStatus(TransStatus.Deleted)    
                            select _finDPCustList.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINDPCustList> GetListFINDPCustList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINDPCustList> _result = new List<FINDPCustList>();

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
                                from _finDPCustList in this.db.FINDPCustLists
                                join _msCust in this.db.MsCustomers
                                    on _finDPCustList.CustCode equals _msCust.CustCode
                                where (SqlMethods.Like(_finDPCustList.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finDPCustList.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finDPCustList.Status != DPCustListDataMapper.GetStatus(TransStatus.Deleted)    
                                orderby _finDPCustList.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finDPCustList.TransNmbr,
                                    FileNmbr = _finDPCustList.FileNmbr,
                                    TransDate = _finDPCustList.TransDate,
                                    CustCode = _finDPCustList.CustCode,
                                    CustName = _msCust.CustName,
                                    CurrCode = _finDPCustList.CurrCode,
                                    Status = _finDPCustList.Status
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPCustList(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CustCode, _row.CustName, _row.CurrCode, _row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDPCustList GetSingleFINDPCustList(string _prmCode)
        {
            FINDPCustList _result = null;

            try
            {
                _result = this.db.FINDPCustLists.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFileNmbrFINDPCustList(string _prmCode)
        {
            string _result = "";

            try
            {
                _result = this.db.FINDPCustLists.Single(_temp => _temp.TransNmbr == _prmCode).FileNmbr;
            }
            catch (Exception ex)
            {
                _result = "";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINDPCustList> GetListDPCustListForDDL(string _prmCustCode)
        {
            List<FINDPCustList> _result = new List<FINDPCustList>();

            var _query = (
                            from _finDPCustList in this.db.FINDPCustLists
                            where _finDPCustList.DoneDPReceipt == YesNoDataMapper.GetYesNo(YesNo.No)
                                && _finDPCustList.Status == DPCustListDataMapper.GetStatus(TransStatus.Posted)
                                && (_finDPCustList.FileNmbr ?? "").Trim() == _finDPCustList.FileNmbr.Trim()
                                && _finDPCustList.CustCode == _prmCustCode
                            orderby _finDPCustList.DatePrep descending
                            select new
                            {
                                TransNmbr = _finDPCustList.TransNmbr,
                                FileNmbr = _finDPCustList.FileNmbr
                            }
                        );

            foreach (object _obj in _query)
            {
                var _row = _obj.Template(new { TransNmbr = this._string, FileNmbr = this._string });

                _result.Add(new FINDPCustList(_row.TransNmbr, _row.FileNmbr));
            }

            return _result;
        }

        public string[] GetListSONo(string _prmCode)
        {
            string[] _result = new string[3];

            var _query = (
                            from _finDPCustList in this.db.FINDPCustLists
                            where _finDPCustList.TransNmbr == _prmCode
                            select new
                            {
                                SONo = _finDPCustList.SONo,
                                ForexRate = _finDPCustList.ForexRate,
                                CurrCode = _finDPCustList.CurrCode
                            }
                        );

            foreach (var _obj in _query)
            {
                _result[0] = _obj.SONo;
                _result[1] = _obj.ForexRate.ToString("#,###.##");
                _result[2] = _obj.CurrCode;
            }

            return _result;
        }

        //public FINDPCustList GetSingleFINDPCustListView(string _prmCode)
        //{
        //    FINDPCustList _result = new FINDPCustList();

        //    try
        //    {
        //        var _query = (
        //                       from _finDPCustList in this.db.FINDPCustLists
        //                       join _msCust in this.db.MsCustomers
        //                            on _finDPCustList.CustCode equals _msCust.CustCode
        //                       orderby _finDPCustList.DatePrep descending
        //                       where _finDPCustList.TransNmbr == _prmCode
        //                       select new
        //                       {
        //                           TransNmbr = _finDPCustList.TransNmbr,
        //                           TransDate = _finDPCustList.TransDate,
        //                           Status = _finDPCustList.Status,
        //                           CustCode = _finDPCustList.CustCode,
        //                           CustName = _msCust.CustName,
        //                           CurrCode = _finDPCustList.CurrCode,
        //                           ForexRate = _finDPCustList.ForexRate,
        //                           TotalForex = _finDPCustList.TotalForex,
        //                           Term = _finDPCustList.Term,
        //                           TermName = _msTerm.TermName,
        //                           DiscForex = _finDPCustList.DiscForex,
        //                           BaseForex = _finDPCustList.BaseForex,
        //                           Attn = _finDPCustList.Attn,
        //                           Remark = _finDPCustList.Remark,
        //                           PPN = _finDPCustList.PPN,
        //                           PPNDate = _finDPCustList.PPNDate,
        //                           PPNForex = _finDPCustList.PPNForex,
        //                           PPNNo = _finDPCustList.PPNNo,
        //                           PPNRate = _finDPCustList.PPNRate,
        //                           BillTo = _finDPCustList.BillTo,
        //                           BillToName = new CustomerBL().GetNameByCode(_finDPCustList.BillTo),
        //                           CustPONo = _finDPCustList.CustPONo
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
        //        _result.PPN = _query.PPN;
        //        _result.PPNDate = _query.PPNDate;
        //        _result.PPNForex = _query.PPNForex;
        //        _result.PPNNo = _query.PPNNo;
        //        _result.PPNRate = _query.PPNRate;
        //        _result.BillTo = _query.BillTo;
        //        _result.BillToName = _query.BillToName;
        //        _result.CustPONo = _query.CustPONo;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool DeleteMultiFINDPCustList(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPCustList _finDPCustList = this.db.FINDPCustLists.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPCustList != null)
                    {
                        if ((_finDPCustList.FileNmbr ?? "").Trim() == "")
                        {
                            this.db.FINDPCustLists.DeleteOnSubmit(_finDPCustList);

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

        public string AddFINDPCustList(FINDPCustList _prmFINDPCustList)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINDPCustList.TransDate.Year, _prmFINDPCustList.TransDate.Month, AppModule.GetValue(TransactionType.FINDPCustList), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINDPCustList.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINDPCustLists.InsertOnSubmit(_prmFINDPCustList);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINDPCustList.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDPCustList(FINDPCustList _prmFINDPCustList)
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

        public String GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            String _result = "";

            try
            {
                int _success = this.db.S_FNDPCustListGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FINDPCustList);
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

        public String Approve(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            String _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.S_FNDPCustListApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINDPCustList _finDPCustList = this.GetSingleFINDPCustList(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finDPCustList.TransDate.Year, _finDPCustList.TransDate.Month, AppModule.GetValue(TransactionType.FINDPCustList), this._companyTag, ""))
                        {
                            _finDPCustList.FileNmbr = item.Number;
                        }
                       
                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FINDPCustList);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINDPCustList(_prmTransNmbr).FileNmbr;
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
                FINDPCustList _finDPCustList = this.db.FINDPCustLists.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDPCustList.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNDPCustListPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FINDPCustList);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINDPCustList(_prmTransNmbr).FileNmbr;
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
                FINDPCustList _finDPCustList = this.db.FINDPCustLists.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDPCustList.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNDPCustListUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.FINDPCustList);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINDPCustList(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINDPCustList(_prmTransNmbr).TransDate;
                        //_transActivity.Reason = "";

                        //this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        //this.db.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public bool GetSingleFINDPCustListForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPCustList _finDPCustList = this.db.FINDPCustLists.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPCustList != null)
                    {
                        if (_finDPCustList.Status != DPCustListDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINDPCustList(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPCustList _finDPCustList = this.db.FINDPCustLists.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPCustList.Status == DPCustListDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finDPCustList.TransNmbr;
                        _unpostingActivity.FileNmbr = _finDPCustList.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finDPCustList != null)
                    {
                        if ((_finDPCustList.FileNmbr ?? "").Trim() == "")
                        {
                            this.db.FINDPCustLists.DeleteOnSubmit(_finDPCustList);

                            _result = true;
                        }
                        else if (_finDPCustList.FileNmbr != "" && _finDPCustList.Status == DPCustListDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finDPCustList.Status = DPCustListDataMapper.GetStatus(TransStatus.Deleted);
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

        ~FINDPCustomerListBL()
        {
        }
    }
}
