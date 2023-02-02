using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Authentication;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class MoneyChangerBL : Base
    {
        public MoneyChangerBL()
        {
        }

        #region Money Changer

        public double RowsCountMoneyChanger(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _finance_MoneyChanger in this.db.Finance_MoneyChangers
                            where (SqlMethods.Like(_finance_MoneyChanger.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            && (SqlMethods.Like((_finance_MoneyChanger.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                            && _finance_MoneyChanger.Status != MoneyChangerDataMapper.GetStatusByte(TransStatus.Deleted)
                            select _finance_MoneyChanger.MoneyChangerCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<Finance_MoneyChanger> GetListMoneyChanger(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Finance_MoneyChanger> _result = new List<Finance_MoneyChanger>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _finance_MoneyChanger in this.db.Finance_MoneyChangers
                                where (SqlMethods.Like(_finance_MoneyChanger.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_finance_MoneyChanger.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _finance_MoneyChanger.Status != MoneyChangerDataMapper.GetStatusByte(TransStatus.Deleted)
                                orderby _finance_MoneyChanger.EditDate descending
                                select new
                                {
                                    MoneyChangerCode = _finance_MoneyChanger.MoneyChangerCode,
                                    TransNmbr = _finance_MoneyChanger.TransNmbr,
                                    FileNmbr = _finance_MoneyChanger.FileNmbr,
                                    Status = _finance_MoneyChanger.Status,
                                    TransDate = _finance_MoneyChanger.TransDate,
                                    CurrCode = _finance_MoneyChanger.CurrCode,
                                    CurrExchange = _finance_MoneyChanger.CurrExchange,
                                    ForexRateExchange = _finance_MoneyChanger.ForexRateExchange,
                                    AmountExchange = _finance_MoneyChanger.AmountExchange,
                                    Amount = _finance_MoneyChanger.Amount,
                                    FgType = _finance_MoneyChanger.FgType,
                                    FgTypeExchange = _finance_MoneyChanger.FgTypeExchange
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Finance_MoneyChanger(_row.MoneyChangerCode, _row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.CurrCode, _row.CurrExchange, _row.ForexRateExchange, _row.AmountExchange, _row.Amount, _row.FgType, _row.FgTypeExchange));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Finance_MoneyChanger GetSingleMoneyChanger(Guid _prmCode)
        {
            Finance_MoneyChanger _result = null;

            try
            {
                _result = this.db.Finance_MoneyChangers.Single(_temp => _temp.MoneyChangerCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Finance_MoneyChangerPetty GetSingleMoneyChangerPetty(Guid _prmCode)
        {
            Finance_MoneyChangerPetty _result = null;

            try
            {
                _result = this.db.Finance_MoneyChangerPetties.Single(_temp => _temp.MoneyChangerCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Finance_MoneyChangerPayment GetSingleMoneyChangerPayment(Guid _prmCode)
        {
            Finance_MoneyChangerPayment _result = null;

            try
            {
                _result = this.db.Finance_MoneyChangerPayments.Single(_temp => _temp.MoneyChangerCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Finance_MoneyChangerPettyExchange GetSingleMoneyChangerPettyExchange(Guid _prmCode)
        {
            Finance_MoneyChangerPettyExchange _result = null;

            try
            {
                _result = this.db.Finance_MoneyChangerPettyExchanges.Single(_temp => _temp.MoneyChangerCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Finance_MoneyChangerPaymentExchange GetSingleMoneyChangerPaymentExchange(Guid _prmCode)
        {
            Finance_MoneyChangerPaymentExchange _result = null;

            try
            {
                _result = this.db.Finance_MoneyChangerPaymentExchanges.Single(_temp => _temp.MoneyChangerCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddMoneyChanger(Finance_MoneyChanger _prmFinance_MoneyChanger, Finance_MoneyChangerPetty _prmFinance_MoneyChangerPetty, Finance_MoneyChangerPayment _prmFinance_MoneyChangerPayType, Finance_MoneyChangerPettyExchange _prmFinance_MoneyChangerPettyExchange, Finance_MoneyChangerPaymentExchange _prmFinance_MoneyChangerPayTypeExchange)
        {
            string _result = "";
            //_prmFINPettyCashHd.TransDate.Year

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFinance_MoneyChanger.TransDate.Year, _prmFinance_MoneyChanger.TransDate.Month, AppModule.GetValue(TransactionType.PettyCashReceive), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFinance_MoneyChanger.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.Finance_MoneyChangers.InsertOnSubmit(_prmFinance_MoneyChanger);

                if (_prmFinance_MoneyChangerPetty != null)
                {
                    _prmFinance_MoneyChangerPetty.MoneyChangerCode = _prmFinance_MoneyChanger.MoneyChangerCode;
                    this.db.Finance_MoneyChangerPetties.InsertOnSubmit(_prmFinance_MoneyChangerPetty);
                }
                else if (_prmFinance_MoneyChangerPayType != null)
                {
                    _prmFinance_MoneyChangerPayType.MoneyChangerCode = _prmFinance_MoneyChanger.MoneyChangerCode;
                    this.db.Finance_MoneyChangerPayments.InsertOnSubmit(_prmFinance_MoneyChangerPayType);
                }

                if (_prmFinance_MoneyChangerPettyExchange != null)
                {
                    _prmFinance_MoneyChangerPettyExchange.MoneyChangerCode = _prmFinance_MoneyChanger.MoneyChangerCode;
                    this.db.Finance_MoneyChangerPettyExchanges.InsertOnSubmit(_prmFinance_MoneyChangerPettyExchange);
                }
                else if (_prmFinance_MoneyChangerPayTypeExchange != null)
                {
                    _prmFinance_MoneyChangerPayTypeExchange.MoneyChangerCode = _prmFinance_MoneyChanger.MoneyChangerCode;
                    this.db.Finance_MoneyChangerPaymentExchanges.InsertOnSubmit(_prmFinance_MoneyChangerPayTypeExchange);
                }

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFinance_MoneyChanger.MoneyChangerCode.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditMoneyChanger(Finance_MoneyChanger _prmFinance_MoneyChanger)
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

        public bool DeleteMultiMoneyChanger(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Finance_MoneyChanger _finance_MoneyChanger = this.db.Finance_MoneyChangers.Single(_temp => _temp.MoneyChangerCode == new Guid(_prmCode[i]));

                    if (_finance_MoneyChanger != null)
                    {
                        if ((_finance_MoneyChanger.FileNmbr ?? "").Trim() == "")
                        {
                            Finance_MoneyChangerPetty _finance_MoneyChangerPetty = this.GetSingleMoneyChangerPetty(new Guid(_prmCode[i]));
                            if (_finance_MoneyChangerPetty != null)
                            {
                                this.db.Finance_MoneyChangerPetties.DeleteOnSubmit(_finance_MoneyChangerPetty);
                            }

                            Finance_MoneyChangerPayment _finance_MoneyChangerPayType = this.GetSingleMoneyChangerPayment(new Guid(_prmCode[i]));
                            if (_finance_MoneyChangerPayType != null)
                            {
                                this.db.Finance_MoneyChangerPayments.DeleteOnSubmit(_finance_MoneyChangerPayType);
                            }

                            Finance_MoneyChangerPettyExchange _finance_MoneyChangerPettyExchange = this.GetSingleMoneyChangerPettyExchange(new Guid(_prmCode[i]));
                            if (_finance_MoneyChangerPettyExchange != null)
                            {
                                this.db.Finance_MoneyChangerPettyExchanges.DeleteOnSubmit(_finance_MoneyChangerPettyExchange);
                            }

                            Finance_MoneyChangerPaymentExchange _finance_MoneyChangerPayTypeExchange = this.GetSingleMoneyChangerPaymentExchange(new Guid(_prmCode[i]));
                            if (_finance_MoneyChangerPayTypeExchange != null)
                            {
                                this.db.Finance_MoneyChangerPaymentExchanges.DeleteOnSubmit(_finance_MoneyChangerPayTypeExchange);
                            }

                            this.db.Finance_MoneyChangers.DeleteOnSubmit(_finance_MoneyChanger);

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

        public string GetApproval(Guid _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spFinance_MoneyChangerGetAppr(_prmCode, 0, 0, _prmuser, ref _result);
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(Guid _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.spFinance_MoneyChangerApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        Finance_MoneyChanger _finance_MoneyChanger = this.GetSingleMoneyChanger(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finance_MoneyChanger.TransDate.Year, _finance_MoneyChanger.TransDate.Month, AppModule.GetValue(TransactionType.MoneyChanger), this._companyTag, ""))
                        {
                            _finance_MoneyChanger.FileNmbr = item.Number;
                        }
                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.MoneyChanger);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = "";
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
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

        public string Posting(Guid _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                Finance_MoneyChanger _finance_MoneyChanger = this.db.Finance_MoneyChangers.Single(_temp => _temp.MoneyChangerCode == _prmCode);
                String _locked = _transCloseBL.IsExistAndLocked(_finance_MoneyChanger.TransDate);

                if (_locked == "")
                {
                    int _success = this.db.spFinance_MoneyChangerPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.MoneyChanger);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleMoneyChanger(_prmCode).FileNmbr;
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

        public string UnPosting(Guid _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                Finance_MoneyChanger _finance_MoneyChanger = this.db.Finance_MoneyChangers.Single(_temp => _temp.MoneyChangerCode == _prmCode);
                String _locked = _transCloseBL.IsExistAndLocked(_finance_MoneyChanger.TransDate);
                if (_locked == "")
                {
                    this.db.spFinance_MoneyChangerUnPost(_prmCode, 0, 0, _prmuser, ref _result);

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
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string GetCurrPettyReceiveHd(string _prmPettyReceiveHd)
        {
            string _result = "";

            try
            {
                _result = this.db.Finance_MoneyChangers.Single(_temp => _temp.TransNmbr == _prmPettyReceiveHd).CurrCode;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public byte GetStatusMoneyChanger(Guid _prmCode)
        {
            byte _result = 0;

            try
            {
                var _query = (
                                from _finance_MoneyChanger in this.db.Finance_MoneyChangers
                                where _finance_MoneyChanger.MoneyChangerCode == _prmCode
                                select new
                                {
                                    Status = _finance_MoneyChanger.Status
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.Status;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetPettyCodeMoneyChanger(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finance_MoneyChangerPetty in this.db.Finance_MoneyChangerPetties
                                where _finance_MoneyChangerPetty.MoneyChangerCode == _prmCode
                                select new
                                {
                                    PettyCode = _finance_MoneyChangerPetty.PettyCode
                                }
                             );

                foreach (var _row in _query)
                {
                    _result = _row.PettyCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetPayCodeMoneyChanger(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finance_MoneyChangerPayType in this.db.Finance_MoneyChangerPayments
                                where _finance_MoneyChangerPayType.MoneyChangerCode == _prmCode
                                select new
                                {
                                    PayCode = _finance_MoneyChangerPayType.PayCode
                                }
                             );

                foreach (var _row in _query)
                {
                    _result = _row.PayCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetPettyCodeExchangeMoneyChanger(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finance_MoneyChangerPetty in this.db.Finance_MoneyChangerPettyExchanges
                                where _finance_MoneyChangerPetty.MoneyChangerCode == _prmCode
                                select new
                                {
                                    PettyCodeExchange = _finance_MoneyChangerPetty.PettyCodeExchange
                                }
                             );

                foreach (var _row in _query)
                {
                    _result = _row.PettyCodeExchange;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetPayCodeExchangeMoneyChanger(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finance_MoneyChangerPayType in this.db.Finance_MoneyChangerPaymentExchanges
                                where _finance_MoneyChangerPayType.MoneyChangerCode == _prmCode
                                select new
                                {
                                    PayCodeExchange = _finance_MoneyChangerPayType.PayCodeExchange
                                }
                             );

                foreach (var _row in _query)
                {
                    _result = _row.PayCodeExchange;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleMoneyChangerForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Finance_MoneyChanger _finance_MoneyChanger = this.db.Finance_MoneyChangers.Single(_temp => _temp.MoneyChangerCode == new Guid(_prmCode[i]));

                    if (_finance_MoneyChanger != null)
                    {
                        if (_finance_MoneyChanger.Status != MoneyChangerDataMapper.GetStatusByte(TransStatus.Posted))
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

        public bool DeleteMultiApproveMoneyChanger(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Finance_MoneyChanger _finance_MoneyChanger = this.db.Finance_MoneyChangers.Single(_temp => _temp.MoneyChangerCode == new Guid(_prmCode[i]));

                    if (_finance_MoneyChanger.Status == MoneyChangerDataMapper.GetStatusByte(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finance_MoneyChanger.TransNmbr;
                        _unpostingActivity.FileNmbr = _finance_MoneyChanger.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finance_MoneyChanger != null)
                    {
                        if ((_finance_MoneyChanger.FileNmbr ?? "").Trim() == "")
                        {
                            Finance_MoneyChangerPetty _finance_MoneyChangerPetty = this.GetSingleMoneyChangerPetty(new Guid(_prmCode[i]));
                            if (_finance_MoneyChangerPetty != null)
                            {
                                this.db.Finance_MoneyChangerPetties.DeleteOnSubmit(_finance_MoneyChangerPetty);
                            }

                            Finance_MoneyChangerPayment _finance_MoneyChangerPayType = this.GetSingleMoneyChangerPayment(new Guid(_prmCode[i]));
                            if (_finance_MoneyChangerPayType != null)
                            {
                                this.db.Finance_MoneyChangerPayments.DeleteOnSubmit(_finance_MoneyChangerPayType);
                            }

                            Finance_MoneyChangerPettyExchange _finance_MoneyChangerPettyExchange = this.GetSingleMoneyChangerPettyExchange(new Guid(_prmCode[i]));
                            if (_finance_MoneyChangerPettyExchange != null)
                            {
                                this.db.Finance_MoneyChangerPettyExchanges.DeleteOnSubmit(_finance_MoneyChangerPettyExchange);
                            }

                            Finance_MoneyChangerPaymentExchange _finance_MoneyChangerPayTypeExchange = this.GetSingleMoneyChangerPaymentExchange(new Guid(_prmCode[i]));
                            if (_finance_MoneyChangerPayTypeExchange != null)
                            {
                                this.db.Finance_MoneyChangerPaymentExchanges.DeleteOnSubmit(_finance_MoneyChangerPayTypeExchange);
                            }

                            this.db.Finance_MoneyChangers.DeleteOnSubmit(_finance_MoneyChanger);

                            _result = true;
                        }
                        else if (_finance_MoneyChanger.FileNmbr != "" && _finance_MoneyChanger.Status == MoneyChangerDataMapper.GetStatusByte(TransStatus.Approved))
                        {
                            _finance_MoneyChanger.Status = MoneyChangerDataMapper.GetStatusByte(TransStatus.Deleted);
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

        ~MoneyChangerBL()
        {
        }

    }
}
