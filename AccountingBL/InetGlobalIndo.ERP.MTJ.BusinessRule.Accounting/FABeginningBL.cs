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
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class FABeginningBL : Base
    {
        public FABeginningBL()
        {
        }

        #region FABeginning
        public double RowsCountFABeginning(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _accFABeginning in this.db.Accounting_FABeginnings
                            where (SqlMethods.Like(_accFABeginning.TransNmbr.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_accFABeginning.FileNmbr.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (_accFABeginning.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted))
                            select _accFABeginning.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public int RowsCountFABeginningList(Guid _prmFABeginningCode)
        {
            int _result = 0;

            _result = this.db.Accounting_FABeginningLists.Where(_row => _row.FABeginningCode == _prmFABeginningCode).Count();

            return _result;
        }

        public double RowsCountFABeginningListAdd(Guid _prmFABeginningCode)
        {
            double _result = 0;

            _result = (
                            from _glFABegin in this.db.MsFixedAssets
                            where _glFABegin.CreatedFrom == FixedAssetsDataMapper.CreatedFrom(FixedAssetCreatedFrom.Manual)
                                && _glFABegin.CreateJournal == FixedAssetsDataMapper.CreateJournal(YesNo.No)
                                && !(
                                from _accFABeginningList in this.db.Accounting_FABeginningLists
                                where _accFABeginningList.FABeginningCode == _prmFABeginningCode
                                select _accFABeginningList.FACode
                            ).Contains(_glFABegin.FACode)

                            select new
                            {
                                _glFABegin.FACode
                            }
                      ).Count();

            return _result;
        }

        public Accounting_FABeginning GetSingleFABeginning(Guid _prmFABeginningCode)
        {
            Accounting_FABeginning _result = null;

            try
            {
                _result = this.db.Accounting_FABeginnings.Single(_fa => _fa.FABeginningCode == _prmFABeginningCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Accounting_FABeginningList GetSingleFABeginningList(Guid _prmFABeginningListCode)
        {
            Accounting_FABeginningList _result = null;

            try
            {
                _result = this.db.Accounting_FABeginningLists.Single(_fa => _fa.FABeginningListCode == _prmFABeginningListCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleGLAccounting_FABeginning(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Accounting_FABeginning _accounting_FABeginning = this.db.Accounting_FABeginnings.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_accounting_FABeginning != null)
                    {
                        if (_accounting_FABeginning.Status != TransactionDataMapper.GetStatus(TransStatus.Posted))
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


        public List<Accounting_FABeginning> GetListFABeginning(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<Accounting_FABeginning> _result = new List<Accounting_FABeginning>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                if (_prmKeyword != "") _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "FileNmbr")
            {
                if (_prmKeyword != "") _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query1 =
                            (
                                from _faBegin in this.db.Accounting_FABeginnings
                                where (SqlMethods.Like(_faBegin.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_faBegin.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (_faBegin.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted))
                                select new
                                {
                                    FABeginningCode = _faBegin.FABeginningCode,
                                    TransNmbr = _faBegin.TransNmbr,
                                    FileNmbr = _faBegin.FileNmbr,
                                    TransDate = _faBegin.TransDate,
                                    Status = _faBegin.Status,
                                    Remark = _faBegin.Remark
                                }
                            );

                if (_prmOrderBy == "Trans No")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TransNmbr)) : (_query1.OrderByDescending(a => a.TransNmbr));
                if (_prmOrderBy == "File No")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FileNmbr)) : (_query1.OrderByDescending(a => a.FileNmbr));
                if (_prmOrderBy == "Trans Date")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TransDate)) : (_query1.OrderByDescending(a => a.TransDate));
                if (_prmOrderBy == "Status")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Status)) : (_query1.OrderByDescending(a => a.Status));
                if (_prmOrderBy == "Remark")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Remark)) : (_query1.OrderByDescending(a => a.Remark));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Accounting_FABeginning(_row.FABeginningCode, _row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Accounting_FABeginningList> GetListFABeginningList(int _prmReqPage, int _prmPageSize, Guid _prmFABeginningCode, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<Accounting_FABeginningList> _result = new List<Accounting_FABeginningList>();

            try
            {
                var _query1 =
                            (
                                from _faBeginList in this.db.Accounting_FABeginningLists
                                join _faBegin in this.db.Accounting_FABeginnings
                                    on _faBeginList.FABeginningCode equals _faBegin.FABeginningCode
                                where _faBeginList.FABeginningCode == _prmFABeginningCode
                                select new
                                {
                                    FABeginningListCode = _faBeginList.FABeginningListCode,
                                    FABeginningCode = _faBeginList.FABeginningCode,
                                    FACode = _faBeginList.FACode,
                                    FAName = (
                                                from _msFA in this.db.MsFixedAssets
                                                where _msFA.FACode == _faBeginList.FACode
                                                select _msFA.FAName
                                             ).FirstOrDefault(),
                                    TotalLife = (
                                                    from _msFA in this.db.MsFixedAssets
                                                    where _msFA.FACode == _faBeginList.FACode
                                                    select _msFA.TotalLifeMonth
                                                ).FirstOrDefault(),
                                    TotalDepr = (
                                                    from _msFA in this.db.MsFixedAssets
                                                    where _msFA.FACode == _faBeginList.FACode
                                                    select _msFA.TotalLifeDepr
                                                ).FirstOrDefault(),
                                    AmountHome = (
                                                    from _msFA in this.db.MsFixedAssets
                                                    where _msFA.FACode == _faBeginList.FACode
                                                    select _msFA.AmountHome
                                                 ).FirstOrDefault(),
                                    AmountDepr = (
                                                    from _msFA in this.db.MsFixedAssets
                                                    where _msFA.FACode == _faBeginList.FACode
                                                    select _msFA.AmountDepr
                                                 ).FirstOrDefault(),
                                    AmountCurrent = (
                                                        from _msFA in this.db.MsFixedAssets
                                                        where _msFA.FACode == _faBeginList.FACode
                                                        select _msFA.AmountCurrent
                                                    ).FirstOrDefault(),
                                    InsertBy = _faBeginList.InsertBy,
                                    InsertDate = _faBeginList.InsertDate
                                }
                            );

                if (_prmOrderBy == "Fixed Asset Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FACode)) : (_query1.OrderByDescending(a => a.FACode));
                if (_prmOrderBy == "Fixed Asset Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAName)) : (_query1.OrderByDescending(a => a.FAName));
                if (_prmOrderBy == "Life Total")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TotalLife)) : (_query1.OrderByDescending(a => a.TotalLife));
                if (_prmOrderBy == "Life Depr")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TotalDepr)) : (_query1.OrderByDescending(a => a.TotalDepr));
                if (_prmOrderBy == "Amount Home")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AmountHome)) : (_query1.OrderByDescending(a => a.AmountHome));
                if (_prmOrderBy == "Amount Depr")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AmountDepr)) : (_query1.OrderByDescending(a => a.AmountDepr));
                if (_prmOrderBy == "Amount Current")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AmountCurrent)) : (_query1.OrderByDescending(a => a.AmountCurrent));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Accounting_FABeginningList(_row.FABeginningListCode, _row.FABeginningCode, _row.FACode, _row.FAName, _row.TotalLife, _row.TotalDepr, _row.AmountHome, _row.AmountDepr, _row.AmountCurrent, _row.InsertBy, _row.InsertDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Accounting_FABeginningList> GetListFABeginningListAdd(int _prmReqPage, int _prmPageSize, Guid _prmFABeginningCode)
        {
            List<Accounting_FABeginningList> _result = new List<Accounting_FABeginningList>();

            try
            {
                var _query = (
                                from _glFABegin in this.db.MsFixedAssets
                                where _glFABegin.CreatedFrom == FixedAssetsDataMapper.CreatedFrom(FixedAssetCreatedFrom.Manual)
                                    && _glFABegin.CreateJournal == FixedAssetsDataMapper.CreateJournal(YesNo.No)
                                    && !(
                                    from _accFABeginningList in this.db.Accounting_FABeginningLists
                                    where _accFABeginningList.FABeginningCode == _prmFABeginningCode
                                    select _accFABeginningList.FACode
                                ).Contains(_glFABegin.FACode)

                                select new
                                {
                                    FACode = _glFABegin.FACode,
                                    FAName = _glFABegin.FAName,
                                    TotalLife = _glFABegin.TotalLifeMonth,
                                    TotalDepr = _glFABegin.TotalLifeDepr,
                                    AmountHome = _glFABegin.AmountHome,
                                    AmountDepr = _glFABegin.AmountDepr,
                                    AmountCurrent = _glFABegin.AmountCurrent
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Accounting_FABeginningList(_row.FACode, _row.FAName, _row.TotalLife, _row.TotalDepr, _row.AmountHome, _row.AmountDepr, _row.AmountCurrent));
                }
            }

            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Accounting_FABeginningList> GetListFABeginningListAdd(Guid _prmFABeginningCode)
        {
            List<Accounting_FABeginningList> _result = new List<Accounting_FABeginningList>();

            try
            {
                var _query = (
                                from _glFABegin in this.db.MsFixedAssets
                                where _glFABegin.CreatedFrom == FixedAssetsDataMapper.CreatedFrom(FixedAssetCreatedFrom.Manual)
                                    && _glFABegin.CreateJournal == FixedAssetsDataMapper.CreateJournal(YesNo.No)
                                    && !(
                                    from _accFABeginningList in this.db.Accounting_FABeginningLists
                                    where _accFABeginningList.FABeginningCode == _prmFABeginningCode
                                    select _accFABeginningList.FACode
                                ).Contains(_glFABegin.FACode)

                                select new
                                {
                                    FACode = _glFABegin.FACode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Accounting_FABeginningList(_row.FACode));
                }
            }

            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<Accounting_FABeginning> FABeginningForListA(int _prmYear, int _prmPeriod)
        //{
        //    List<Accounting_FABeginning> _result = new List<Accounting_FABeginning>();

        //    try
        //    {
        //        var _query = (
        //                        from _glFABeginningForm in this.db.S_GLFABeginningFormula(_prmYear, _prmPeriod)
        //                        where !(
        //                            from _accFABeginningList in this.db.Accounting_FABeginningLists
        //                            where _accFABeginningList.FACode == _glFABeginningForm.FACode && _accFABeginningList.Year == _prmYear && _accFABeginningList.Period == _prmPeriod
        //                            select _accFABeginningList.FACode
        //                        ).Contains(_glFABeginningForm.FACode)

        //                        select new
        //                        {
        //                            _glFABeginningForm.FACode
        //                        }
        //                    );

        //        foreach (var _item in _query)
        //        {
        //            Accounting_FABeginning _beginning = new Accounting_FABeginning();

        //            _beginning.FACode = _item.FACode;

        //            _result.Add(_beginning);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<S_GLFABeginningFormulaResult> FABeginning(int _prmYear, int _prmPeriod)
        //{
        //    List<S_GLFABeginningFormulaResult> _result = new List<S_GLFABeginningFormulaResult>();

        //    try
        //    {
        //        var _query = (
        //                        from _glFABeginningForm in this.db.S_GLFABeginningFormula(_prmYear, _prmPeriod)
        //                        where !(
        //                            from _accFABeginningList in this.db.Accounting_FABeginningLists
        //                            where _accFABeginningList.FACode == _glFABeginningForm.FACode && _accFABeginningList.Year == _prmYear && _accFABeginningList.Period == _prmPeriod
        //                            select _accFABeginningList.FACode
        //                        ).Contains(_glFABeginningForm.FACode)

        //                        select new
        //                        {
        //                            _glFABeginningForm.FACode,
        //                            _glFABeginningForm.FAName,
        //                            _glFABeginningForm.BalanceLife,
        //                            _glFABeginningForm.BalanceAmount,
        //                            _glFABeginningForm.Amount
        //                        }
        //                    );

        //        foreach (var _item in _query)
        //        {
        //            S_GLFABeginningFormulaResult _beginning = new S_GLFABeginningFormulaResult();

        //            _beginning.FACode = _item.FACode;
        //            _beginning.FAName = _item.FAName;
        //            _beginning.BalanceLife = _item.BalanceLife;
        //            _beginning.BalanceAmount = _item.BalanceAmount;
        //            _beginning.Amount = _item.Amount;

        //            _result.Add(_beginning);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool AddListFA(List<Accounting_FABeginningList> _prmFixedAsset, Guid _prmFABeginningCode, string _prmUserName)
        {
            bool _result = false;

            String[] _faCode = new String[_prmFixedAsset.Count];

            for (int i = 0; i < _faCode.Count(); i++)
            {
                _faCode[i] = _prmFixedAsset[i].FACode;
            }

            this.AddList(_faCode, _prmFABeginningCode, _prmUserName);

            _result = true;

            return _result;
        }

        public List<Accounting_FABeginningList> AddList(string[] _prmCode, Guid _prmFABeginningCode, string _prmUserName)
        {
            List<Accounting_FABeginningList> _result = new List<Accounting_FABeginningList>();

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Accounting_FABeginningList _accFABeginningList = new Accounting_FABeginningList();

                    _accFABeginningList.FABeginningListCode = Guid.NewGuid();
                    _accFABeginningList.FABeginningCode = _prmFABeginningCode;
                    _accFABeginningList.FACode = _prmCode[i];
                    _accFABeginningList.InsertBy = _prmUserName;
                    _accFABeginningList.InsertDate = DateTime.Now;
                    _accFABeginningList.EditBy = _prmUserName;
                    _accFABeginningList.EditDate = DateTime.Now;

                    this.db.Accounting_FABeginningLists.InsertOnSubmit(_accFABeginningList);
                }

                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String AddAccounting_FABeginning(Accounting_FABeginning _prmFABeginning)
        {
            String _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmGLJournalHd.Year, _prmGLJournalHd.Period, AppModule.GetValue(TransactionType.JournalEntry), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFABeginning.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.Accounting_FABeginnings.InsertOnSubmit(_prmFABeginning);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFABeginning.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditAccounting_FABeginning(Accounting_FABeginning _prmFABeginning)
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

        public bool DeleteMultiAccounting_FABeginning(string[] _faBeginningCode)
        {
            bool _result = false;            

            try
            {
                for (int i = 0; i < _faBeginningCode.Length; i++)
                {
                    Accounting_FABeginning _accFABeginning = this.db.Accounting_FABeginnings.Single(_fa => _fa.FABeginningCode == new Guid(_faBeginningCode[i]));

                    if (_accFABeginning != null)
                    {
                        if (_accFABeginning.Status != TransactionDataMapper.GetStatus(TransStatus.Posted))
                        {
                            var _query = (from _detail in this.db.Accounting_FABeginningLists
                                          where _detail.FABeginningCode == new Guid(_faBeginningCode[i])
                                          select _detail);

                            this.db.Accounting_FABeginningLists.DeleteAllOnSubmit(_query);

                            this.db.Accounting_FABeginnings.DeleteOnSubmit(_accFABeginning);

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

        public bool DeleteMultiApproveAccounting_FABeginning(string[] _faBeginningCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _faBeginningCode.Length; i++)
                {
                    Accounting_FABeginning _accFABeginning = this.db.Accounting_FABeginnings.Single(_fa => _fa.FABeginningCode == new Guid(_faBeginningCode[i]));

                    if (_accFABeginning.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _accFABeginning.TransNmbr;
                        _unpostingActivity.FileNmbr = _accFABeginning.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_accFABeginning != null)
                    {
                        if (_accFABeginning.Status != TransactionDataMapper.GetStatus(TransStatus.Posted))
                        {
                            var _query = (from _detail in this.db.Accounting_FABeginningLists
                                          where _detail.FABeginningCode == new Guid(_faBeginningCode[i])
                                          select _detail);

                            this.db.Accounting_FABeginningLists.DeleteAllOnSubmit(_query);

                            this.db.Accounting_FABeginnings.DeleteOnSubmit(_accFABeginning);

                            _result = true;
                        }
                        else if (_accFABeginning.FileNmbr != "" && _accFABeginning.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _accFABeginning.Status = TransactionDataMapper.GetStatusByte(TransStatus.Deleted);
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

        public bool DeleteMultiAccounting_FABeginningList(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Accounting_FABeginningList _accFABeginningList = this.db.Accounting_FABeginningLists.Single(_fa => _fa.FABeginningListCode.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.Accounting_FABeginningLists.DeleteOnSubmit(_accFABeginningList);
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

        public bool DeleteAllFABeginningList(Guid _faBeginningCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _accFABeginningList in this.db.Accounting_FABeginningLists
                                where _accFABeginningList.FABeginningCode == _faBeginningCode
                                select _accFABeginningList
                              );

                this.db.Accounting_FABeginningLists.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAppr(Guid _prmFABeginningCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spAcc_GLFABeginningGetAppr(_prmFABeginningCode, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FABeginning);
                    _transActivity.TransNmbr = _prmFABeginningCode.ToString();
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

        public string Approve(Guid _prmFABeginningCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.spAcc_GLFABeginningApprove(_prmFABeginningCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        Accounting_FABeginning _accFABeginning = this.GetSingleFABeginning(_prmFABeginningCode);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_accFABeginning.TransDate.Year, _accFABeginning.TransDate.Month, AppModule.GetValue(TransactionType.FABeginning), this._companyTag, ""))
                        {
                            _accFABeginning.FileNmbr = _item.Number;
                        }


                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FABeginning);
                        _transActivity.TransNmbr = _prmFABeginningCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleFABeginning(_prmFABeginningCode).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

                        _result = "Approve Success";
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

        public string Posting(Guid _prmFABeginningCode, DateTime _prmTransDate, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spAcc_GLFABeginningPost(_prmFABeginningCode, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Posting Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FABeginning);
                    _transActivity.TransNmbr = _prmFABeginningCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleFABeginning(_prmFABeginningCode).FileNmbr;
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Unposting(Guid _prmFABeginningCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spAcc_GLFABeginningUnpost(_prmFABeginningCode, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "UnPosting Success";

                    //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    //_transActivity.ActivitiesCode = Guid.NewGuid();
                    //_transActivity.TransType = AppModule.GetValue(TransactionType.FABeginning);
                    //_transActivity.TransNmbr = _prmFABeginningCode.ToString();
                    //_transActivity.FileNmbr = this.GetSingleFABeginning(_prmFABeginningCode).FileNmbr;
                    //_transActivity.Username = _prmuser;
                    //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                    //_transActivity.ActivitiesDate = this.GetSingleFABeginning(_prmFABeginningCode).TransDate;
                    //_transActivity.Reason = "";

                    //this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    //this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }
        #endregion

        ~FABeginningBL()
        {
        }
    }
}
