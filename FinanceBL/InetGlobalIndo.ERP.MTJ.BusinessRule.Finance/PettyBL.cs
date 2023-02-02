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
    public sealed class PettyBL : Base
    {
        public PettyBL()
        {
        }

        private MembershipService _service = new MembershipService();

        #region Petty

        public double RowsCount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                           from _msPetty in this.db.MsPetties
                           where (SqlMethods.Like(_msPetty.PettyCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like(_msPetty.PettyName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                           select _msPetty.PettyCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountForReport()
        {
            double _result = 0;

            var _query =
                        (
                            from _msPetty in this.db.MsPetties
                            select _msPetty.PettyCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool Edit(MsPetty _prmMsPetty)
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

        public bool Add(MsPetty _prmMsPetty)
        {
            bool _result = false;

            try
            {
                this.db.MsPetties.InsertOnSubmit(_prmMsPetty);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmPetty)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmPetty.Length; i++)
                {
                    MsPetty _mspetty = this.db.MsPetties.Single(_temp => _temp.PettyCode.Trim().ToLower() == _prmPetty[i].Trim().ToLower());

                    this.db.MsPetties.DeleteOnSubmit(_mspetty);
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

        public MsPetty GetSingle(string _prmPetty)
        {
            MsPetty _result = null;

            try
            {
                _result = this.db.MsPetties.Single(_temp => _temp.PettyCode == _prmPetty);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPetty> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsPetty> _result = new List<MsPetty>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _msPetty in this.db.MsPetties
                                join _msAccount in this.db.MsAccounts
                                on _msPetty.Account equals _msAccount.Account
                                where (SqlMethods.Like(_msPetty.PettyCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPetty.PettyName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msPetty.UserDate descending
                                select new
                                {
                                    PettyCode = _msPetty.PettyCode,
                                    PettyName = _msPetty.PettyName,
                                    //FgType = _msPetty.FgType,
                                    Account = _msPetty.Account,
                                    AccountName = _msAccount.AccountName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    //_result.Add(new MsPetty(_row.PettyCode, _row.PettyName, _row.FgType, _row.Account, _row.AccountName));
                    _result.Add(new MsPetty(_row.PettyCode, _row.PettyName, _row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPetty> GetList()
        {
            List<MsPetty> _result = new List<MsPetty>();

            try
            {
                var _query = (
                                from _msPetty in this.db.MsPetties
                                join _msAccount in this.db.MsAccounts
                                on _msPetty.Account equals _msAccount.Account
                                orderby _msPetty.UserDate descending
                                select new
                                {
                                    PettyCode = _msPetty.PettyCode,
                                    PettyName = _msPetty.PettyName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { PettyCode = this._string, PettyName = this._string });
                    _result.Add(new MsPetty(_row.PettyCode, _row.PettyName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPetty> GetListPettyWithoutCurrCode(String _prmCurrCode)
        {
            List<MsPetty> _result = new List<MsPetty>();

            try
            {
                var _query = (
                                from _msPetty in this.db.MsPetties
                                join _msAccount in this.db.MsAccounts
                                on _msPetty.Account equals _msAccount.Account
                                where _msAccount.CurrCode != _prmCurrCode
                                orderby _msPetty.UserDate descending
                                select new
                                {
                                    PettyCode = _msPetty.PettyCode,
                                    PettyName = _msPetty.PettyName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { PettyCode = this._string, PettyName = this._string });
                    _result.Add(new MsPetty(_row.PettyCode, _row.PettyName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPetty> GetListForReport(int _prmReqPage, int _prmPageSize)
        {
            List<MsPetty> _result = new List<MsPetty>();

            try
            {
                var _query = (
                                from _msPetty in this.db.MsPetties
                                orderby _msPetty.PettyCode ascending
                                select new
                                {
                                    PettyCode = _msPetty.PettyCode,
                                    PettyName = _msPetty.PettyName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsPetty(_row.PettyCode, _row.PettyName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public byte GetTypePetty(string _prmCode)
        //{
        //    byte _result = 0;

        //    try
        //    {
        //        var _query = (
        //                        from _msPetty in this.db.MsPetties
        //                        where _msPetty.PettyCode.Trim().ToLower() == _prmCode.Trim().ToLower()
        //                        select new
        //                        {
        //                            FgType = _msPetty.FgType
        //                        }
        //                     );

        //        foreach (var _row in _query)
        //        {
        //            _result = _row.FgType;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public string GetPettyNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msPetty in this.db.MsPetties
                                where _msPetty.PettyCode.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    PettyName = _msPetty.PettyName
                                }
                             );

                foreach (var _row in _query)
                {
                    _result = _row.PettyName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region PettyCashHd
        public double RowsCountCashHd(string _prmCategory, string _prmKeyword)
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
                            from _msPettyHd in this.db.FINPettyHds
                            where (SqlMethods.Like(_msPettyHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            && (SqlMethods.Like((_msPettyHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                            && _msPettyHd.Status != PettyCashDataMapper.GetStatus(TransStatus.Deleted)
                            select _msPettyHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountAdvancedSearch(string _prmTransNmbr, string _prmFileNo, DateTime _prmDateFrom, DateTime _prmDateTo, string _prmPetty, string _prmPayTo, string _prmStatus)
        {
            double _result = 0;

            string _pattern1 = "%" + _prmTransNmbr + "%";
            string _pattern2 = "%" + _prmPayTo + "%";
            string _pattern3 = "%" + _prmStatus + "%";
            string _pattern4 = "%" + _prmPetty + "%";
            string _pattern5 = "%" + _prmFileNo + "%";

            var _query =
                        (
                            from _msPettyHd in this.db.FINPettyHds
                            where (SqlMethods.Like(_msPettyHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like(_msPettyHd.PayTo.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && _msPettyHd.TransDate >= _prmDateFrom && _msPettyHd.TransDate <= _prmDateTo
                                  && (SqlMethods.Like(_msPettyHd.Petty.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                  && (SqlMethods.Like(_msPettyHd.Status.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                  && (SqlMethods.Like((_msPettyHd.FileNmbr ?? "").ToString().Trim().ToLower(), _pattern5.Trim().ToLower()))
                                  && _msPettyHd.Status != PettyCashDataMapper.GetStatus(TransStatus.Deleted)
                            select _msPettyHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINPettyHd> GetListHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINPettyHd> _result = new List<FINPettyHd>();

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
                                from _msPettyHd in this.db.FINPettyHds
                                where (SqlMethods.Like(_msPettyHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_msPettyHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _msPettyHd.Status != PettyCashDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _msPettyHd.TransDate descending, _msPettyHd.TransNmbr descending
                                select new
                                {
                                    TransNumber = _msPettyHd.TransNmbr,
                                    FileNmbr = _msPettyHd.FileNmbr,
                                    TransDate = _msPettyHd.TransDate,
                                    Status = _msPettyHd.Status,
                                    Petty = (
                                                from _msPetty in this.db.MsPetties
                                                where _msPettyHd.Petty == _msPetty.PettyCode
                                                select _msPetty.PettyName
                                            ).FirstOrDefault(),
                                    Currency = _msPettyHd.CurrCode,
                                    ForexRate = _msPettyHd.ForexRate,
                                    PayTo = _msPettyHd.PayTo,
                                    Remark = _msPettyHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINPettyHd(_row.TransNumber, _row.FileNmbr, _row.Status, _row.TransDate, _row.Petty,
                        _row.Currency, _row.ForexRate, _row.PayTo, _row.Remark
                        ));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINPettyHd> GetListAdvancedSearch(int _prmReqPage, int _prmPageSize, string _prmTransNmbr, string _prmFileNo, DateTime _prmDateFrom, DateTime _prmDateTo, string _prmPetty, string _prmPayTo, string _prmStatus)
        {
            List<FINPettyHd> _result = new List<FINPettyHd>();

            string _pattern1 = "%" + _prmTransNmbr + "%";
            string _pattern2 = "%" + _prmPayTo + "%";
            string _pattern3 = "%" + _prmStatus + "%";
            string _pattern4 = "%" + _prmPetty + "%";
            string _pattern5 = "%" + _prmFileNo + "%";

            try
            {
                var _query = (
                               from _msPettyHd in this.db.FINPettyHds
                               where (SqlMethods.Like(_msPettyHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msPettyHd.PayTo.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                     && _msPettyHd.TransDate >= _prmDateFrom && _msPettyHd.TransDate <= _prmDateTo
                                     && (SqlMethods.Like(_msPettyHd.Petty.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                     && (SqlMethods.Like(_msPettyHd.Status.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                     && (SqlMethods.Like((_msPettyHd.FileNmbr ?? "").ToString().Trim().ToLower(), _pattern5.Trim().ToLower()))
                               orderby _msPettyHd.TransDate descending, _msPettyHd.TransNmbr descending
                               select new
                               {
                                   TransNumber = _msPettyHd.TransNmbr,
                                   FileNmbr = _msPettyHd.FileNmbr,
                                   TransDate = _msPettyHd.TransDate,
                                   Status = _msPettyHd.Status,
                                   Petty = (
                                                from _msPetty in this.db.MsPetties
                                                where _msPetty.PettyCode == _msPettyHd.Petty
                                                select _msPetty.PettyName
                                            ).FirstOrDefault(),
                                   Currency = _msPettyHd.CurrCode,
                                   ForexRate = _msPettyHd.ForexRate,
                                   PayTo = _msPettyHd.PayTo,
                                   Remark = _msPettyHd.Remark
                               }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINPettyHd(_row.TransNumber, _row.FileNmbr, _row.Status, _row.TransDate, _row.Petty,
                        _row.Currency, _row.ForexRate, _row.PayTo, _row.Remark
                        ));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int RowsCountPettyCashHdPrintPreview(DateTime _prmBeginDate, DateTime _prmEndDate)
        {
            int _result = 0;

            try
            {
                _result = (
                            from _msPettyHd in this.db.FINPettyHds
                            //join _msPetty in this.db.MsPetties
                            //on _msPettyHd.Petty equals _msPetty.PettyCode
                            //where (_msPettyHd.TransDate >= _prmBeginDate) && (_msPettyHd.TransDate <= _prmEndDate) && (_msPetty.FgType == Convert.ToByte(_prmType))
                            where (_msPettyHd.TransDate >= _prmBeginDate) && (_msPettyHd.TransDate <= _prmEndDate) && (_msPettyHd.Status == PettyCashDataMapper.GetStatus(TransStatus.Posted))
                            orderby _msPettyHd.TransDate ascending
                            select _msPettyHd
                          ).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINPettyHd> GetListPettyCashHdPrintPreview(int _prmReqPage, int _prmPageSize, DateTime _prmBeginDate, DateTime _prmEndDate)
        {
            List<FINPettyHd> _result = new List<FINPettyHd>();

            try
            {
                var _query = (
                                from _msPettyHd in this.db.FINPettyHds
                                //join _msPetty in this.db.MsPetties
                                //on _msPettyHd.Petty equals _msPetty.PettyCode
                                //where (_msPettyHd.TransDate >= _prmBeginDate) && (_msPettyHd.TransDate <= _prmEndDate) && (_msPetty.FgType == Convert.ToByte(_prmType))
                                where (_msPettyHd.TransDate >= _prmBeginDate) && (_msPettyHd.TransDate <= _prmEndDate) && (_msPettyHd.Status == PettyCashDataMapper.GetStatus(TransStatus.Posted))
                                orderby _msPettyHd.TransDate ascending
                                select new
                                {
                                    TransNumber = _msPettyHd.TransNmbr,
                                    TransDate = _msPettyHd.TransDate,
                                    Status = _msPettyHd.Status,
                                    PayTo = _msPettyHd.PayTo,
                                    Remark = _msPettyHd.Remark
                                }
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINPettyHd(_row.TransNumber, _row.Status, _row.TransDate, _row.PayTo, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddCashHd(FINPettyHd _prmPettyHd)
        {
            bool _result = false;

            try
            {
                this.db.FINPettyHds.InsertOnSubmit(_prmPettyHd);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditCashHd(FINPettyHd _prmPettyHd)
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

        public bool DeleteMultiCashHd(string[] _prmPettyHd)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmPettyHd.Length; i++)
                {
                    FINPettyHd _msPettyHd = this.db.FINPettyHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmPettyHd[i].Trim().ToLower());

                    if (_msPettyHd != null)
                    {
                        if ((_msPettyHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINPettyDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmPettyHd[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINPettyDts.DeleteAllOnSubmit(_query);

                            this.db.FINPettyHds.DeleteOnSubmit(_msPettyHd);

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

        public FINPettyHd GetSingleHd(string _prmPettyHd)
        {
            FINPettyHd _result = null;

            try
            {
                _result = this.db.FINPettyHds.Single(_temp => _temp.TransNmbr == _prmPettyHd);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCurrHd(string _prmPettyHd)
        {
            string _result = "";

            try
            {
                _result = this.db.FINPettyHds.Single(_temp => _temp.TransNmbr == _prmPettyHd).CurrCode;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddFINPettyCashHd(FINPettyHd _prmFINPettyCashHd)
        {
            string _result = "";
            //_prmFINPettyCashHd.TransDate.Year

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINPettyCashHd.TransDate.Year, _prmFINPettyCashHd.TransDate.Month, AppModule.GetValue(TransactionType.PettyCash),this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINPettyCashHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINPettyHds.InsertOnSubmit(_prmFINPettyCashHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINPettyCashHd.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetReqPrint(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINPettyPrintHistories.Where(_row => _row.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()).Count();

            return _result;
        }

        public string GetPettyCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finPettyHd in this.db.FINPettyHds
                                where _finPettyHd.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    Petty = _finPettyHd.Petty
                                }
                             );

                foreach (var _row in _query)
                {
                    _result = _row.Petty;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(string _prmCode, string _prmuser)
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
                int _success = this.db.S_FNCashPettyGetAppr(_prmCode, DateTime.Now.Year, DateTime.Now.Month, _prmuser, ref _result);

                if (_result == "")
                {
                    //FINPettyHd _finPettyHd = this.GetSingleHd(_prmCode);
                    //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finPettyHd.TransDate.Year, _finPettyHd.TransDate.Month, AppModule.GetValue(TransactionType.PettyCash), this._companyTag, ""))
                    //{
                    //    _finPettyHd.FileNmbr = item.Number;
                    //}

                    //this.db.SubmitChanges();

                    //_scope.Complete();

                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.PettyCash);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
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

        public string Approve(string _prmCode, string _prmuser)
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
                    this.db.S_FNCashPettyApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINPettyHd _finPettyHd = this.GetSingleHd(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finPettyHd.TransDate.Year, _finPettyHd.TransDate.Month, AppModule.GetValue(TransactionType.PettyCash), this._companyTag, ""))
                        {
                            _finPettyHd.FileNmbr = item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.PettyCash);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleHd(_prmCode).FileNmbr;
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

        public string Posting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINPettyHd _finPettyHd = this.db.FINPettyHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finPettyHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNCashPettyPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.PettyCash);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleHd(_prmCode).FileNmbr;
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

        public string UnPosting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINPettyHd _finPettyHd = this.db.FINPettyHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finPettyHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNCashPettyUnPost(_prmCode, 0, 0, _prmuser, ref _result);

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

        public bool AddMultiFINPettyPrintHistory(string[] _prmCode, string _prmRemark, string _prmUser)
        {
            bool _result = false;
            int _tempRow = 0;
            List<FINPettyPrintHistory> _finPettyPrintHistory = new List<FINPettyPrintHistory>();

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    _tempRow = this.RowsCountCashDt(_prmCode[i]);

                    if (_tempRow > 0)
                    {
                        _finPettyPrintHistory.Add(new FINPettyPrintHistory(Guid.NewGuid(), _prmCode[i], DateTime.Now, _prmRemark, _prmUser));
                    }
                }

                this.db.FINPettyPrintHistories.InsertAllOnSubmit(_finPettyPrintHistory);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public char GetStatusFINPettyCashHd(string _prmCode)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _finPettyHd in this.db.FINPettyHds
                                where _finPettyHd.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    Status = _finPettyHd.Status
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

        public bool GetSingleFINPettyCashHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINPettyHd _msPettyHd = this.db.FINPettyHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_msPettyHd != null)
                    {
                        if (_msPettyHd.Status != PettyCashDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINPettyCashHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINPettyHd _msPettyHd = this.db.FINPettyHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_msPettyHd.Status == PettyCashDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _msPettyHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _msPettyHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_msPettyHd != null)
                    {
                        if ((_msPettyHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINPettyDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINPettyDts.DeleteAllOnSubmit(_query);

                            this.db.FINPettyHds.DeleteOnSubmit(_msPettyHd);

                            _result = true;
                        }
                        else if (_msPettyHd.FileNmbr != "" && _msPettyHd.Status == PettyCashDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _msPettyHd.Status = PettyCashDataMapper.GetStatus(TransStatus.Deleted);
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

        #region PettyCashDt

        public int RowsCountCashDt(string _prmTransactionNumber)
        {
            int _result = 0;

            try
            {
                var _query = (
                                from _msPettyDt in this.db.FINPettyDts
                                orderby _msPettyDt.ItemNo descending
                                where (_msPettyDt.TransNmbr.Trim().ToLower() == _prmTransactionNumber.Trim().ToLower())

                                select new
                                {
                                    TransNumber = _msPettyDt.TransNmbr

                                }
                            ).Count();

                _result = _query;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditCashDt(FINPettyDt _prmPettyDt)
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

        public bool AddCashDt(FINPettyDt _prmPettyDt)
        {
            bool _result = false;

            try
            {
                this.db.FINPettyDts.InsertOnSubmit(_prmPettyDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiCashDt(string[] _prmPettyDt, string _prmTransactionNumber)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmPettyDt.Length; i++)
                {
                    FINPettyDt _msPettyDt = this.db.FINPettyDts.Single(_temp => _temp.ItemNo == Convert.ToInt16(_prmPettyDt[i]) && _temp.TransNmbr.Trim().ToLower() == _prmTransactionNumber.Trim().ToLower());

                    this.db.FINPettyDts.DeleteOnSubmit(_msPettyDt);
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



        public FINPettyDt GetSingleDt(string _prmPettyDt, string _prmNumber)
        {
            FINPettyDt _result = null;

            try
            {
                _result = this.db.FINPettyDts.Single(_temp => _temp.TransNmbr == _prmPettyDt && _temp.ItemNo == Convert.ToInt16(_prmNumber));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINPettyDt> GetListDt(string _prmTransactionNumber, int _prmReqPage, int _prmPageSize)
        {
            List<FINPettyDt> _result = new List<FINPettyDt>();

            try
            {
                var _query = (from _msPettyDt in this.db.FINPettyDts
                              where _msPettyDt.TransNmbr == _prmTransactionNumber
                              select new
                              {
                                  TransNumber = _msPettyDt.TransNmbr,
                                  ItemNo = _msPettyDt.ItemNo,
                                  Account = _msPettyDt.Account,
                                  AccountName = (
                                                    from _msAccount in this.db.MsAccounts
                                                    where _msAccount.Account == _msPettyDt.Account
                                                    select _msAccount.AccountName
                                               ).FirstOrDefault(),
                                  FgSubled = _msPettyDt.FgSubLed,
                                  Subled = _msPettyDt.SubLed,
                                  SubledName = (
                                                    from _viewMsSubled in this.db.V_MsSubleds
                                                    where _viewMsSubled.SubLed_No == _msPettyDt.SubLed
                                                    select _viewMsSubled.SubLed_Name
                                               ).FirstOrDefault(),
                                  Remark = _msPettyDt.Remark,
                                  Forex = _msPettyDt.AmountForex
                              }
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINPettyDt(
                        _row.TransNumber, _row.ItemNo, _row.Account, _row.AccountName, _row.FgSubled, _row.Subled, _row.SubledName,
                        _row.Remark, _row.Forex
                        ));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxNoItem(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINPettyDts.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region PettyCashReceiveHd

        public double RowsCountFINPettyReceiveHd(string _prmCategory, string _prmKeyword)
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
                            from _finPettyReceiveHd in this.db.FINPettyReceiveHds
                            where (SqlMethods.Like(_finPettyReceiveHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            && (SqlMethods.Like((_finPettyReceiveHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                            && _finPettyReceiveHd.Status != PettyCashDataMapper.GetStatus(TransStatus.Deleted)
                            select _finPettyReceiveHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountReceiveAdvancedSearch(string _prmTransNmbr, string _prmFileNo, DateTime _prmDateFrom, DateTime _prmDateTo, string _prmFgType, string _prmPayTo, string _prmStatus)
        {
            double _result = 0;

            string _pattern1 = "%" + _prmTransNmbr + "%";
            string _pattern2 = "%" + _prmPayTo + "%";
            string _pattern3 = "%" + _prmStatus + "%";
            string _pattern4 = "%" + _prmFgType + "%";
            string _pattern5 = "%" + _prmFileNo + "%";

            var _query =
                        (
                            from _msPettyHd in this.db.FINPettyReceiveHds
                            where (SqlMethods.Like(_msPettyHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like(_msPettyHd.PayTo.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && _msPettyHd.TransDate >= _prmDateFrom && _msPettyHd.TransDate <= _prmDateTo
                                  && (SqlMethods.Like(_msPettyHd.FgType.ToString().Trim().ToLower(), _pattern4.Trim().ToLower()))
                                  && (SqlMethods.Like(_msPettyHd.Status.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                  && (SqlMethods.Like((_msPettyHd.FileNmbr ?? "").ToString().Trim().ToLower(), _pattern5.Trim().ToLower()))
                                  && _msPettyHd.Status != PettyCashDataMapper.GetStatus(TransStatus.Deleted)
                            select _msPettyHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINPettyReceiveHd> GetListFINPettyReceiveHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINPettyReceiveHd> _result = new List<FINPettyReceiveHd>();

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
                                from _finPettyReceiveHd in this.db.FINPettyReceiveHds
                                where (SqlMethods.Like(_finPettyReceiveHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_finPettyReceiveHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _finPettyReceiveHd.Status != PettyCashDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finPettyReceiveHd.TransDate descending, _finPettyReceiveHd.TransNmbr descending
                                select new
                                {
                                    TransNumber = _finPettyReceiveHd.TransNmbr,
                                    FileNmbr = _finPettyReceiveHd.FileNmbr,
                                    TransDate = _finPettyReceiveHd.TransDate,
                                    Status = _finPettyReceiveHd.Status,
                                    Currency = _finPettyReceiveHd.CurrCode,
                                    ForexRate = _finPettyReceiveHd.ForexRate,
                                    PayTo = _finPettyReceiveHd.PayTo,
                                    Remark = _finPettyReceiveHd.Remark,
                                    FgType = _finPettyReceiveHd.FgType
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINPettyReceiveHd(_row.TransNumber, _row.FileNmbr, _row.Status, _row.TransDate, _row.Currency, _row.ForexRate, _row.PayTo, _row.Remark, _row.FgType));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINPettyReceiveHd> GetListReceiveAdvancedSearch(int _prmReqPage, int _prmPageSize, string _prmTransNmbr, string _prmFileNo, DateTime _prmDateFrom, DateTime _prmDateTo, string _prmFgType, string _prmPayTo, string _prmStatus)
        {
            List<FINPettyReceiveHd> _result = new List<FINPettyReceiveHd>();

            string _pattern1 = "%" + _prmTransNmbr + "%";
            string _pattern2 = "%" + _prmPayTo + "%";
            string _pattern3 = "%" + _prmStatus + "%";
            string _pattern4 = "%" + _prmFgType + "%";
            string _pattern5 = "%" + _prmFileNo + "%";

            try
            {
                var _query = (
                               from _msPettyHd in this.db.FINPettyReceiveHds
                               where (SqlMethods.Like(_msPettyHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msPettyHd.PayTo.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                     && _msPettyHd.TransDate >= _prmDateFrom && _msPettyHd.TransDate <= _prmDateTo
                                     && (SqlMethods.Like(_msPettyHd.FgType.ToString().Trim().ToLower(), _pattern4.Trim().ToLower()))
                                     && (SqlMethods.Like(_msPettyHd.Status.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                     && (SqlMethods.Like((_msPettyHd.FileNmbr ?? "").ToString().Trim().ToLower(), _pattern5.Trim().ToLower()))
                                     && _msPettyHd.Status != PettyCashDataMapper.GetStatus(TransStatus.Deleted)
                               orderby _msPettyHd.TransDate descending, _msPettyHd.TransNmbr descending
                               select new
                               {
                                   TransNumber = _msPettyHd.TransNmbr,
                                   FileNmbr = _msPettyHd.FileNmbr,
                                   TransDate = _msPettyHd.TransDate,
                                   Status = _msPettyHd.Status,
                                   Currency = _msPettyHd.CurrCode,
                                   ForexRate = _msPettyHd.ForexRate,
                                   PayTo = _msPettyHd.PayTo,
                                   Remark = _msPettyHd.Remark,
                                   FgType = _msPettyHd.FgType
                               }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINPettyReceiveHd(_row.TransNumber, _row.FileNmbr, _row.Status, _row.TransDate, _row.Currency, _row.ForexRate, _row.PayTo, _row.Remark, _row.FgType));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int RowsCountPettyCashReceiveHdPrintPreview(DateTime _prmBeginDate, DateTime _prmEndDate)
        {
            int _result = 0;

            try
            {
                _result = (
                            from _finPettyReceiveHd in this.db.FINPettyReceiveHds
                            //join _msPetty in this.db.MsPetties
                            //on _msPettyHd.Petty equals _msPetty.PettyCode
                            //where (_msPettyHd.TransDate >= _prmBeginDate) && (_msPettyHd.TransDate <= _prmEndDate) && (_msPetty.FgType == Convert.ToByte(_prmType))
                            where (_finPettyReceiveHd.TransDate >= _prmBeginDate) && (_finPettyReceiveHd.TransDate <= _prmEndDate) && (_finPettyReceiveHd.Status == PettyCashDataMapper.GetStatus(TransStatus.Posted))
                            orderby _finPettyReceiveHd.TransDate ascending
                            select _finPettyReceiveHd
                          ).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINPettyReceiveHd> GetListPettyCashReceiveHdPrintPreview(int _prmReqPage, int _prmPageSize, DateTime _prmBeginDate, DateTime _prmEndDate)
        {
            List<FINPettyReceiveHd> _result = new List<FINPettyReceiveHd>();

            try
            {
                var _query = (
                                from _finPettyReceiveHd in this.db.FINPettyReceiveHds
                                //join _msPetty in this.db.MsPetties
                                //on _msPettyHd.Petty equals _msPetty.PettyCode
                                //where (_msPettyHd.TransDate >= _prmBeginDate) && (_msPettyHd.TransDate <= _prmEndDate) && (_msPetty.FgType == Convert.ToByte(_prmType))
                                where (_finPettyReceiveHd.TransDate >= _prmBeginDate) && (_finPettyReceiveHd.TransDate <= _prmEndDate) && (_finPettyReceiveHd.Status == PettyCashDataMapper.GetStatus(TransStatus.Approved) || _finPettyReceiveHd.Status == PettyCashDataMapper.GetStatus(TransStatus.Posted))
                                orderby _finPettyReceiveHd.TransDate ascending
                                select new
                                {
                                    TransNumber = _finPettyReceiveHd.TransNmbr,
                                    TransDate = _finPettyReceiveHd.TransDate,
                                    Status = _finPettyReceiveHd.Status,
                                    PayTo = _finPettyReceiveHd.PayTo,
                                    Remark = _finPettyReceiveHd.Remark
                                }
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINPettyReceiveHd(_row.TransNumber, _row.Status, _row.TransDate, _row.PayTo, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPettyReceiveHd GetSingleFINPettyReceiveHd(string _prmCode)
        {
            FINPettyReceiveHd _result = null;

            try
            {
                _result = this.db.FINPettyReceiveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPettyReceiveHdPetty GetSingleFINPettyReceiveHdPetty(string _prmCode)
        {
            FINPettyReceiveHdPetty _result = null;

            try
            {
                _result = this.db.FINPettyReceiveHdPetties.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPettyReceiveHdPayType GetSingleFINPettyReceiveHdPayType(string _prmCode)
        {
            FINPettyReceiveHdPayType _result = null;

            try
            {
                _result = this.db.FINPettyReceiveHdPayTypes.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddFINPettyReceiveHd(FINPettyReceiveHd _prmFINPettyReceiveHd, FINPettyReceiveHdPetty _prmFINPettyReceiveHdPetty, FINPettyReceiveHdPayType _prmFINPettyReceiveHdPayType)
        {
            string _result = "";
            //_prmFINPettyCashHd.TransDate.Year

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINPettyReceiveHd.TransDate.Year, _prmFINPettyReceiveHd.TransDate.Month, AppModule.GetValue(TransactionType.PettyCashReceive), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINPettyReceiveHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINPettyReceiveHds.InsertOnSubmit(_prmFINPettyReceiveHd);

                if (_prmFINPettyReceiveHdPetty != null)
                {
                    _prmFINPettyReceiveHdPetty.TransNmbr = _prmFINPettyReceiveHd.TransNmbr;
                    this.db.FINPettyReceiveHdPetties.InsertOnSubmit(_prmFINPettyReceiveHdPetty);
                }
                else if (_prmFINPettyReceiveHdPayType != null)
                {
                    _prmFINPettyReceiveHdPayType.TransNmbr = _prmFINPettyReceiveHd.TransNmbr;
                    this.db.FINPettyReceiveHdPayTypes.InsertOnSubmit(_prmFINPettyReceiveHdPayType);
                }

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINPettyReceiveHd.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINPettyReceiveHd(FINPettyReceiveHd _prmFINPettyReceiveHd, string _prmPettyCode, string _prmPayCode)
        {
            bool _result = false;
            FINPettyReceiveHdPetty _finPettyReceiveHdPetty = null;
            FINPettyReceiveHdPayType _finPettyReceiveHdPayType = null;
            try
            {
                _finPettyReceiveHdPetty = this.GetSingleFINPettyReceiveHdPetty(_prmFINPettyReceiveHd.TransNmbr);
                if (_finPettyReceiveHdPetty != null)
                {
                    this.db.FINPettyReceiveHdPetties.DeleteOnSubmit(_finPettyReceiveHdPetty);
                }
                _finPettyReceiveHdPayType = this.GetSingleFINPettyReceiveHdPayType(_prmFINPettyReceiveHd.TransNmbr);
                if (_finPettyReceiveHdPayType != null)
                {
                    this.db.FINPettyReceiveHdPayTypes.DeleteOnSubmit(_finPettyReceiveHdPayType);
                }

                if (_prmFINPettyReceiveHd.FgType == PettyCashDataMapper.GetType(PettyCashReceiveType.Petty))
                {
                    _finPettyReceiveHdPetty = new FINPettyReceiveHdPetty();
                    _finPettyReceiveHdPetty.TransNmbr = _prmFINPettyReceiveHd.TransNmbr;
                    _finPettyReceiveHdPetty.PettyCode = _prmPettyCode;

                    this.db.FINPettyReceiveHdPetties.InsertOnSubmit(_finPettyReceiveHdPetty);
                }
                else if (_prmFINPettyReceiveHd.FgType == PettyCashDataMapper.GetType(PettyCashReceiveType.Payment))
                {
                    _finPettyReceiveHdPayType = new FINPettyReceiveHdPayType();
                    _finPettyReceiveHdPayType.TransNmbr = _prmFINPettyReceiveHd.TransNmbr;
                    _finPettyReceiveHdPayType.PayCode = _prmPayCode;

                    this.db.FINPettyReceiveHdPayTypes.InsertOnSubmit(_finPettyReceiveHdPayType);
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

        public bool DeleteMultiFINPettyReceiveHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINPettyReceiveHd _finPettyReceiveHd = this.db.FINPettyReceiveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finPettyReceiveHd != null)
                    {
                        if ((_finPettyReceiveHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINPettyReceiveDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINPettyReceiveDts.DeleteAllOnSubmit(_query);

                            FINPettyReceiveHdPetty _finPettyReceiveHdPetty = this.GetSingleFINPettyReceiveHdPetty(_prmCode[i]);
                            if (_finPettyReceiveHdPetty != null)
                            {
                                this.db.FINPettyReceiveHdPetties.DeleteOnSubmit(_finPettyReceiveHdPetty);
                            }

                            FINPettyReceiveHdPayType _finPettyReceiveHdPayType = this.GetSingleFINPettyReceiveHdPayType(_prmCode[i]);
                            if (_finPettyReceiveHdPayType != null)
                            {
                                this.db.FINPettyReceiveHdPayTypes.DeleteOnSubmit(_finPettyReceiveHdPayType);
                            }

                            this.db.FINPettyReceiveHds.DeleteOnSubmit(_finPettyReceiveHd);

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

        public int GetReqPrintReceipt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINPettyReceivePrintHistories.Where(_row => _row.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()).Count();

            return _result;
        }

        public string GetApprovalReceipt(string _prmCode, string _prmuser)
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
                this.db.S_FNCashPettyReceiptGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    //FINPettyReceiveHd _finPettyReceiveHd = this.GetSingleFINPettyReceiveHd(_prmCode);
                    //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finPettyReceiveHd.TransDate.Year, _finPettyReceiveHd.TransDate.Month, AppModule.GetValue(TransactionType.PettyCashReceive), this._companyTag, ""))
                    //{
                    //    _finPettyReceiveHd.FileNmbr = item.Number;
                    //}

                    //this.db.SubmitChanges();

                    //_scope.Complete();

                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.PettyCashReceive);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
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

        public string ApproveReceipt(string _prmCode, string _prmuser)
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
                    this.db.S_FNCashPettyReceiptApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINPettyReceiveHd _finPettyReceiveHd = this.GetSingleFINPettyReceiveHd(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finPettyReceiveHd.TransDate.Year, _finPettyReceiveHd.TransDate.Month, AppModule.GetValue(TransactionType.PettyCashReceive), this._companyTag, ""))
                        {
                            _finPettyReceiveHd.FileNmbr = item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.PettyCashReceive);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleFINPettyReceiveHd(_prmCode).FileNmbr;
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

        public string PostingReceipt(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINPettyReceiveHd _finPettyReceiveHd = this.db.FINPettyReceiveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finPettyReceiveHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNCashPettyReceiptPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.PettyCashReceive);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleFINPettyReceiveHd(_prmCode).FileNmbr;
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

        public string UnPostingReceipt(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINPettyReceiveHd _finPettyReceiveHd = this.db.FINPettyReceiveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finPettyReceiveHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNCashPettyReceiptUnPost(_prmCode, 0, 0, _prmuser, ref _result);

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

        public bool AddMultiFINPettyReceivePrintHistory(string[] _prmCode, string _prmRemark, string _prmUser)
        {
            bool _result = false;
            int _tempRow = 0;
            List<FINPettyReceivePrintHistory> _finPettyReceivePrintHistory = new List<FINPettyReceivePrintHistory>();

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    _tempRow = this.RowsCountFINPettyReceiveDt(_prmCode[i]);

                    if (_tempRow > 0)
                    {
                        _finPettyReceivePrintHistory.Add(new FINPettyReceivePrintHistory(Guid.NewGuid(), _prmCode[i], DateTime.Now, _prmRemark, _prmUser));
                    }
                }

                this.db.FINPettyReceivePrintHistories.InsertAllOnSubmit(_finPettyReceivePrintHistory);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCurrPettyReceiveHd(string _prmPettyReceiveHd)
        {
            string _result = "";

            try
            {
                _result = this.db.FINPettyReceiveHds.Single(_temp => _temp.TransNmbr == _prmPettyReceiveHd).CurrCode;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public char GetStatusFINPettyReceiveHd(string _prmCode)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _finPettyReceiveHd in this.db.FINPettyReceiveHds
                                where _finPettyReceiveHd.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    Status = _finPettyReceiveHd.Status
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

        public string GetPettyCodeFINPettyReceive(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finPettyReceiveHdPetty in this.db.FINPettyReceiveHdPetties
                                where _finPettyReceiveHdPetty.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    PettyCode = _finPettyReceiveHdPetty.PettyCode
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

        public string GetPayCodeFINPettyReceive(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finPettyReceiveHdPayType in this.db.FINPettyReceiveHdPayTypes
                                where _finPettyReceiveHdPayType.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    PayCode = _finPettyReceiveHdPayType.PayCode
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

        public bool GetSingleFINPettyReceiveHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINPettyReceiveHd _finPettyReceiveHd = this.db.FINPettyReceiveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finPettyReceiveHd != null)
                    {
                        if (_finPettyReceiveHd.Status != PettyCashDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINPettyReceiveHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINPettyReceiveHd _finPettyReceiveHd = this.db.FINPettyReceiveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finPettyReceiveHd.Status == PettyCashDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finPettyReceiveHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finPettyReceiveHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finPettyReceiveHd != null)
                    {
                        if ((_finPettyReceiveHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINPettyReceiveDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINPettyReceiveDts.DeleteAllOnSubmit(_query);

                            FINPettyReceiveHdPetty _finPettyReceiveHdPetty = this.GetSingleFINPettyReceiveHdPetty(_prmCode[i]);
                            if (_finPettyReceiveHdPetty != null)
                            {
                                this.db.FINPettyReceiveHdPetties.DeleteOnSubmit(_finPettyReceiveHdPetty);
                            }

                            FINPettyReceiveHdPayType _finPettyReceiveHdPayType = this.GetSingleFINPettyReceiveHdPayType(_prmCode[i]);
                            if (_finPettyReceiveHdPayType != null)
                            {
                                this.db.FINPettyReceiveHdPayTypes.DeleteOnSubmit(_finPettyReceiveHdPayType);
                            }

                            this.db.FINPettyReceiveHds.DeleteOnSubmit(_finPettyReceiveHd);

                            _result = true;
                        }
                        else if (_finPettyReceiveHd.FileNmbr != "" && _finPettyReceiveHd.Status == PettyCashDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finPettyReceiveHd.Status = PettyCashDataMapper.GetStatus(TransStatus.Deleted);
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

        #region PettyCashReceiveDt

        public int RowsCountFINPettyReceiveDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                from _finPettyReceiveDt in this.db.FINPettyReceiveDts
                                orderby _finPettyReceiveDt.ItemNo descending
                                where (_finPettyReceiveDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower())

                                select new
                                {
                                    TransNumber = _finPettyReceiveDt.TransNmbr

                                }
                            ).Count();

                _result = _query;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINPettyReceiveDt> GetListFINPettyReceiveDt(string _prmCode, int _prmReqPage, int _prmPageSize)
        {
            List<FINPettyReceiveDt> _result = new List<FINPettyReceiveDt>();

            try
            {
                var _query = (from _finPettyReceiveDt in this.db.FINPettyReceiveDts
                              where _finPettyReceiveDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                              select new
                              {
                                  TransNmbr = _finPettyReceiveDt.TransNmbr,
                                  ItemNo = _finPettyReceiveDt.ItemNo,
                                  Account = _finPettyReceiveDt.Account,
                                  AccountName = (
                                                    from _msAccount in this.db.MsAccounts
                                                    where _msAccount.Account == _finPettyReceiveDt.Account
                                                    select _msAccount.AccountName
                                               ).FirstOrDefault(),
                                  FgSubled = _finPettyReceiveDt.FgSubLed,
                                  Subled = _finPettyReceiveDt.SubLed,
                                  SubledName = (
                                                    from _viewMsSubled in this.db.V_MsSubleds
                                                    where _viewMsSubled.SubLed_No == _finPettyReceiveDt.SubLed
                                                    select _viewMsSubled.SubLed_Name
                                               ).FirstOrDefault(),
                                  Remark = _finPettyReceiveDt.Remark,
                                  AmountForex = _finPettyReceiveDt.AmountForex
                              }
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINPettyReceiveDt(
                        _row.TransNmbr, _row.ItemNo, _row.Account, _row.AccountName, _row.FgSubled, _row.Subled, _row.SubledName,
                        _row.Remark, _row.AmountForex
                        ));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINPettyReceiveDt(string[] _prmItem, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItem.Length; i++)
                {
                    FINPettyReceiveDt _finPettyReceiveDt = this.db.FINPettyReceiveDts.Single(_temp => _temp.ItemNo == Convert.ToInt16(_prmItem[i]) && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.FINPettyReceiveDts.DeleteOnSubmit(_finPettyReceiveDt);
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

        public int GetMaxNoItemFINPettyReceiveDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINPettyReceiveDts.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPettyReceiveDt GetSingleFINPettyReceiveDt(string _prmCode, string _prmItem)
        {
            FINPettyReceiveDt _result = null;

            try
            {
                _result = this.db.FINPettyReceiveDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.ItemNo == Convert.ToInt16(_prmItem));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINPettyReceiveDt(FINPettyReceiveDt _prmFINPettyReceiveDt)
        {
            bool _result = false;

            try
            {
                this.db.FINPettyReceiveDts.InsertOnSubmit(_prmFINPettyReceiveDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINPettyReceiveDt(FINPettyReceiveDt _prmFINPettyReceiveDt)
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

        public Boolean CheckLimitAuthorization(string _prmTranstype, string _prmUserName, decimal _prmAmount)
        {
            Boolean _result = false;

            try
            {
                //string[] _tempRole;

                //_tempRole = _service.GetRolesForUser(_prmUserName);

                var _query =
                                (
                                    from _msLimitAuthorization in this.db.Master_LimitAuthorizations
                                    where (
                                              new RoleBL().GetRolesIDByUserName(_prmUserName)
                                            ).Contains(_msLimitAuthorization.RoleID.ToString())
                                            && (_msLimitAuthorization.TransTypeCode == _prmTranstype)
                                    select
                                        //new
                                        //{
                                        _msLimitAuthorization.Limit
                    //}
                                );

                if (_query.Count() > 0)
                {
                    if (_prmAmount / 1000 <= _query.Max())
                    {
                        _result = true;
                    }
                }
                else
                {
                    _result = true;
                }


                //for (int i = 0; i < _tempRole.Length; i++)
                //{
                //    Guid _roleID = new RoleBL().GetRoleIDByName(_tempRole[i]);

                //    decimal _query = (
                //                    from _msLimitAuthorization in this.db.Master_LimitAuthorizations
                //                    where _msLimitAuthorization.RoleID == _roleID
                //                        && _msLimitAuthorization.TransTypeCode == _prmTranstype
                //                    select _msLimitAuthorization.Limit
                //                 ).Max();

                //    if (_query >= _prmAmount)
                //    {
                //        _result = true;

                //        break;
                //    }
                //}
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        ~PettyBL()
        {
        }

    }
}
