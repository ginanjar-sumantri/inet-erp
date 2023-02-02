using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class JournalEntryBL : Base
    {
        string _transClass = "";
        int _itemNo = 0;
        string _account = "";
        string _accountName = "";
        string _subLedger = "";
        string _currCode = "";
        decimal _forexRate = 0;
        decimal _debitForex = 0;
        decimal _creditForex = 0;
        decimal _debitHome = 0;
        decimal _creditHome = 0;

        public JournalEntryBL()
        {

        }

        #region JournalEntry
        public double RowsCountGLJournalHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "RefNo")
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
                            from _glJournalHd in this.db.GLJournalHds
                            where (SqlMethods.Like(_glJournalHd.Reference.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like((_glJournalHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && _glJournalHd.TransClass == AppModule.GetValue(TransactionType.JournalEntry)
                            select _glJournalHd.Reference
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountAdvancedSearch(string _prmTransNmbr, string _prmFileNo, DateTime _prmDateFrom, DateTime _prmDateTo, String _prmStatus, String _prmRemark)
        {
            double _result = 0;

            string _pattern1 = "%" + _prmTransNmbr + "%";
            string _pattern2 = "%" + _prmStatus + "%";
            string _pattern3 = "%" + _prmFileNo + "%";
            string _pattern4 = "%" + _prmRemark + "%";

            var _query =
                        (
                            from _glJournalHd in this.db.GLJournalHds
                            where (SqlMethods.Like(_glJournalHd.Reference.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && _glJournalHd.TransDate >= _prmDateFrom && _glJournalHd.TransDate <= _prmDateTo
                                && (SqlMethods.Like(_glJournalHd.Status.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && (SqlMethods.Like((_glJournalHd.FileNmbr ?? "").ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                && (_glJournalHd.TransClass == AppModule.GetValue(TransactionType.JournalEntry))
                                && (SqlMethods.Like(_glJournalHd.Remark.Trim().ToLower(), _pattern4.Trim().ToLower()))
                            select _glJournalHd.Reference
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<GLJournalHd> GetListGLJournalHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<GLJournalHd> _result = new List<GLJournalHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "RefNo")
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
                var _query1 = (
                               from _glJournalHd in this.db.GLJournalHds
                               where (SqlMethods.Like(_glJournalHd.Reference.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like((_glJournalHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && _glJournalHd.TransClass == AppModule.GetValue(TransactionType.JournalEntry)
                               orderby _glJournalHd.TransDate descending
                               select new
                               {
                                   Reference = _glJournalHd.Reference,
                                   TransClass = _glJournalHd.TransClass,
                                   FileNmbr = _glJournalHd.FileNmbr,
                                   TransDate = _glJournalHd.TransDate,
                                   Status = _glJournalHd.Status,
                                   Remark = _glJournalHd.Remark,
                                   Year = _glJournalHd.Year,
                                   Period = _glJournalHd.Period
                               }
                            );

                if (_prmOrderBy == "Reference No.")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Reference)) : (_query1.OrderByDescending(a => a.Reference));
                if (_prmOrderBy == "File No.")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FileNmbr)) : (_query1.OrderByDescending(a => a.FileNmbr));
                if (_prmOrderBy == "Reference Date")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TransDate)) : (_query1.OrderByDescending(a => a.TransDate));
                if (_prmOrderBy == "Status")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Status)) : (_query1.OrderByDescending(a => a.Status));
                if (_prmOrderBy == "Remark")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Remark)) : (_query1.OrderByDescending(a => a.Remark));
                if (_prmOrderBy == "Year")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Year)) : (_query1.OrderByDescending(a => a.Year));
                if (_prmOrderBy == "Period")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Period)) : (_query1.OrderByDescending(a => a.Period));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GLJournalHd(_row.Reference, _row.TransClass, _row.FileNmbr, _row.TransDate, _row.Status, _row.Remark, _row.Year, _row.Period));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLJournalHd> GetListAdvancedSearch(int _prmReqPage, int _prmPageSize, string _prmTransNmbr, string _prmFileNo, DateTime _prmDateFrom, DateTime _prmDateTo, String _prmStatus, String _prmRemark)
        {
            List<GLJournalHd> _result = new List<GLJournalHd>();

            string _pattern1 = "%" + _prmTransNmbr + "%";
            string _pattern2 = "%" + _prmStatus + "%";
            string _pattern3 = "%" + _prmFileNo + "%";
            string _pattern4 = "%" + _prmRemark + "%";

            try
            {
                var _query = (
                               from _glJournalHd in this.db.GLJournalHds
                               where (SqlMethods.Like(_glJournalHd.Reference.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && _glJournalHd.TransDate >= _prmDateFrom && _glJournalHd.TransDate <= _prmDateTo
                                   && (SqlMethods.Like(_glJournalHd.Status.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_glJournalHd.FileNmbr ?? "").ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && (_glJournalHd.TransClass == AppModule.GetValue(TransactionType.JournalEntry))
                                   && (SqlMethods.Like(_glJournalHd.Remark.Trim().ToLower(), _pattern4.Trim().ToLower()))
                               orderby _glJournalHd.TransDate descending
                               select new
                               {
                                   Reference = _glJournalHd.Reference,
                                   TransClass = _glJournalHd.TransClass,
                                   FileNmbr = _glJournalHd.FileNmbr,
                                   TransDate = _glJournalHd.TransDate,
                                   Status = _glJournalHd.Status,
                                   Remark = _glJournalHd.Remark,
                                   Year = _glJournalHd.Year,
                                   Period = _glJournalHd.Period
                               }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {

                    _result.Add(new GLJournalHd(_row.Reference, _row.TransClass, _row.FileNmbr, _row.TransDate, _row.Status, _row.Remark, _row.Year, _row.Period));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLJournalHd GetSingleGLJournalHd(string _prmCode, String _prmTransClass)
        {
            GLJournalHd _result = null;

            try
            {
                _result = this.db.GLJournalHds.Single(_temp => _temp.Reference == _prmCode && _temp.TransClass == _prmTransClass);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean DeleteMultiGLJournalHd(string[] _prmCode)
        {
            Boolean _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    String[] _code = _prmCode[i].Split('-');

                    GLJournalHd _glJournalHd = this.db.GLJournalHds.Single(_temp => _temp.Reference.Trim().ToLower() == _code[0].Trim().ToLower() && _temp.TransClass.ToLower() == _code[1].Trim().ToLower());

                    if (_glJournalHd != null)
                    {
                        if ((_glJournalHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLJournalDts
                                          where _detail.Reference.Trim().ToLower() == _code[0].Trim().ToLower()
                                            && _detail.TransClass.Trim().ToLower() == _code[1].Trim().ToLower()
                                          select _detail);

                            this.db.GLJournalDts.DeleteAllOnSubmit(_query);

                            this.db.GLJournalHds.DeleteOnSubmit(_glJournalHd);

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

        public string AddGLJournalHd(GLJournalHd _prmGLJournalHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmGLJournalHd.Year, _prmGLJournalHd.Period, AppModule.GetValue(TransactionType.JournalEntry), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmGLJournalHd.Reference = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.GLJournalHds.InsertOnSubmit(_prmGLJournalHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmGLJournalHd.Reference;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string EditGLJournalHd(GLJournalHd _prmGLJournalHd)
        {
            string _result = "";

            try
            {
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                _result = "You Failed Edit Data";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(string _prmReference, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_GLJournalGetAppr(_prmReference, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";

                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string Approve(String _prmReference, String _prmTransClass, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_GLJournalApprove(_prmReference, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        GLJournalHd _glJournalHd = this.GetSingleGLJournalHd(_prmReference, _prmTransClass);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_glJournalHd.Year, _glJournalHd.Period, AppModule.GetValue(TransactionType.JournalEntry), this._companyTag, ""))
                        {
                            _glJournalHd.FileNmbr = _item.Number;
                        }

                        this.db.SubmitChanges();

                        _scope.Complete();

                        _result = "Approve Success";
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string Posting(string _prmReference, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLJournalHd _glJournalHd = this.db.GLJournalHds.Single(_temp => _temp.Reference.Trim().ToLower() == _prmReference.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glJournalHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLJournalPost(_prmReference, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";
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
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string Unposting(string _prmReference, string _prmuser)
        {
            string _result = "";
            TransactionCloseBL _transCloseBL = new TransactionCloseBL();

            try
            {
                GLJournalHd _glJournalHd = this.db.GLJournalHds.Single(_temp => _temp.Reference.Trim().ToLower() == _prmReference.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glJournalHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLJournalUnPost(_prmReference, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";
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
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLJournalHd> GetListForReport()
        {
            List<GLJournalHd> _result = new List<GLJournalHd>();

            try
            {
                var _query =
                            (
                                from _glJourbalHd in this.db.GLJournalHds                                
                                select new
                                {
                                    TransClass = _glJourbalHd.TransClass
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new GLJournalHd(_row.TransClass));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLJournalHd> GetTranstypeCodeAndNameListForReport()
        {
            List<GLJournalHd> _result = new List<GLJournalHd>();

            try
            {
                var _query =
                            (
                                from _glJourbalHd in this.db.GLJournalHds
                                join _msTranstype in this.db.MsTransTypes
                                   on _glJourbalHd.TransClass equals _msTranstype.TransTypeCode
                                orderby _glJourbalHd.TransClass ascending
                                select new
                                {
                                    TransClass = _glJourbalHd.TransClass,
                                    TransTypeName = _glJourbalHd.TransClass + " - " + _msTranstype.TransTypeName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new GLJournalHd(_row.TransClass, _row.TransTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion GLJournalHd

        #region JournalEntryDetail

        public List<GLJournalDt> GetListGLJournalDt(string _prmReference, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<GLJournalDt> _result = new List<GLJournalDt>();

            try
            {
                var _query1 = (
                               from _glJournalDt in this.db.GLJournalDts
                               join _msAccount in this.db.MsAccounts
                                   on _glJournalDt.Account equals _msAccount.Account
                               orderby _glJournalDt.ItemNo ascending
                               where _glJournalDt.Reference == _prmReference
                               select new
                               {
                                   TransClass = _glJournalDt.TransClass,
                                   ItemNo = _glJournalDt.ItemNo,
                                   Account = _glJournalDt.Account,
                                   AccountName = _msAccount.AccountName,
                                   SubLedger = _glJournalDt.SubLed,
                                   SubLed_Name = (
                                                    from _viewMsSubled in this.db.V_MsSubleds
                                                    where _viewMsSubled.SubLed_No == _glJournalDt.SubLed
                                                    select _viewMsSubled.SubLed_Name
                                                 ).FirstOrDefault(),
                                   Currency = _glJournalDt.CurrCode,
                                   ForexRate = _glJournalDt.ForexRate,
                                   DebitForex = _glJournalDt.DebitForex,
                                   CreditForex = _glJournalDt.CreditForex,
                                   DebitHome = _glJournalDt.DebitHome,
                                   CreditHome = _glJournalDt.CreditHome
                               }
                            );

                if (_prmOrderBy == "Account")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Account)) : (_query1.OrderByDescending(a => a.Account));
                if (_prmOrderBy == "Account Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccountName)) : (_query1.OrderByDescending(a => a.AccountName));
                if (_prmOrderBy == "Sub Ledger")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.SubLedger)) : (_query1.OrderByDescending(a => a.SubLedger));
                if (_prmOrderBy == "Sub Ledger Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.SubLed_Name)) : (_query1.OrderByDescending(a => a.SubLed_Name));
                if (_prmOrderBy == "Currency")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Currency)) : (_query1.OrderByDescending(a => a.Currency));
                if (_prmOrderBy == "Forex Rate")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.ForexRate)) : (_query1.OrderByDescending(a => a.ForexRate));
                if (_prmOrderBy == "Debit Forex")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.DebitForex)) : (_query1.OrderByDescending(a => a.DebitForex));
                if (_prmOrderBy == "Credit Forex")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.CreditForex)) : (_query1.OrderByDescending(a => a.CreditForex));
                if (_prmOrderBy == "Debit")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.DebitHome)) : (_query1.OrderByDescending(a => a.DebitHome));
                if (_prmOrderBy == "Credit")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.CreditHome)) : (_query1.OrderByDescending(a => a.CreditHome));

                var _query = _query1;

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { TransClass = this._transClass, ItemNo = this._itemNo, Account = this._account, AccountName = this._accountName, SubLedger = this._subLedger, SubLed_Name = this._string, Currency = this._currCode, ForexRate = this._forexRate, DebitForex = this._debitForex, CreditForex = this._creditForex, DebitHome = this._debitHome, CreditHome = this._creditHome });

                    _result.Add(new GLJournalDt(_row.TransClass, _row.ItemNo, _row.Account, _row.AccountName, _row.SubLedger, _row.SubLed_Name, _row.Currency, _row.ForexRate, _row.DebitForex, _row.CreditForex, _row.DebitHome, _row.CreditHome));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxNoItem(string _prmCode, string _prmTransClassCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.GLJournalDts.Where(_a => _a.Reference == _prmCode && _a.TransClass == _prmTransClassCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLJournalDt GetSingleGLJournalDt(string _prmCode, string _prmReference, string _prmTransClass)
        {
            GLJournalDt _result = null;

            try
            {
                _result = this.db.GLJournalDts.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmCode) && _temp.Reference == _prmReference && _temp.TransClass == _prmTransClass);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiGLJournalDt(string[] _prmItemNo, string _prmReference, string _prmTransClass)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItemNo.Length; i++)
                {
                    GLJournalDt _glJournalDt = this.db.GLJournalDts.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmItemNo[i]) && _temp.Reference.Trim().ToLower() == _prmReference.Trim().ToLower() && _temp.TransClass.Trim().ToLower() == _prmTransClass.Trim().ToLower());

                    this.db.GLJournalDts.DeleteOnSubmit(_glJournalDt);
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

        public bool AddGLJournalDt(GLJournalDt _prmGLJournalDt)
        {
            bool _result = false;

            try
            {

                this.db.GLJournalDts.InsertOnSubmit(_prmGLJournalDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGLJournalDt(GLJournalDt _prmGLJournalDt)
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

        public bool CheckBalance(string _prmReference)
        {
            bool _result = false;

            var _query = (
                               from _glJournalDt in this.db.GLJournalDts
                               where _glJournalDt.Reference == _prmReference
                               group _glJournalDt by _glJournalDt.Reference into _grp
                               select new
                               {
                                   DebitHome = _grp.Sum(a => a.DebitHome), //_glJournalDt.DebitHome,
                                   CreditHome = _grp.Sum(a => a.CreditHome)//_glJournalDt.CreditHome
                               }
                            );

            foreach (var _obj in _query)
            {
                if (_obj.CreditHome == _obj.DebitHome)
                {
                    _result = true;
                }
            }

            return _result;
        }

        public string GetSubledNameBySubledCode(string _prmSubledCode)
        {
            string _result = "";

            var _query = (from _viewMsSubled in this.db.V_MsSubleds
                          where _viewMsSubled.SubLed_No == _prmSubledCode
                          select new
                          {
                              Subled_Name = _viewMsSubled.SubLed_Name
                          }
                          );
            foreach (var _obj in _query)
            {
                _result = _obj.Subled_Name;
            }

            return _result;
        }

        public decimal GetAmountByPeriodAndAccount(int _prmYear, int _prmPeriod, string _prmAccount)
        {
            decimal _result = 0;

            var _query = (
                              from _glJournalHd in this.db.GLJournalHds
                              join _glJournalDt in this.db.GLJournalDts
                                on _glJournalHd.Reference equals _glJournalDt.Reference
                              where _glJournalHd.Year == _prmYear
                                && _glJournalHd.Period == _prmPeriod
                                && _glJournalDt.Account == _prmAccount
                              group _glJournalDt by _glJournalHd.Period into _grp
                              select new
                              {
                                  DebitForex = _grp.Sum(a => a.DebitForex),
                                  CreditForex = _grp.Sum(a => a.CreditForex)
                              }
                          );
            foreach (var _row in _query)
            {
                _result = _row.DebitForex - _row.CreditForex;
            }

            return _result;
        }
        #endregion

        ~JournalEntryBL()
        {

        }
    }
}
