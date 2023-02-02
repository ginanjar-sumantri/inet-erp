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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class SetupBL : Base
    {
        public SetupBL()
        {
        }

        #region Setup

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
                            from _msSetup in this.db.MsSetups
                            where _msSetup.SetGroup == SetupDataMapper.GetSetupGroup(SetupStatus.Account)
                               && (SqlMethods.Like(_msSetup.SetCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSetup.SetDescription.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msSetup
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool Add(MsSetup _prmMsSetup)
        {
            bool _result = false;

            try
            {

                this.db.MsSetups.InsertOnSubmit(_prmMsSetup);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsSetup _prmMsSetup)
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

        public bool DeleteMulti(string[] _prmSetupCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmSetupCode.Length; i++)
                {
                    MsSetup _msSetup = this.db.MsSetups.Single(_temp => _temp.SetGroup == SetupDataMapper.GetSetupGroup(SetupStatus.Account) && _temp.SetCode == _prmSetupCode[i]);

                    this.db.MsSetups.DeleteOnSubmit(_msSetup);
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

        public MsSetup GetSingle(string _prmSetupCode)
        {
            MsSetup _result = null;

            try
            {
                _result = this.db.MsSetups.Single(_temp => _temp.SetGroup == SetupDataMapper.GetSetupGroup(SetupStatus.Account) && _temp.SetCode == _prmSetupCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MsSetup GetSingleAutoNumber(string _prmSetupCode)
        {
            MsSetup _result = null;

            try
            {
                _result = this.db.MsSetups.Single(_temp => _temp.SetGroup == SetupDataMapper.GetSetupGroup(SetupStatus.AutoNmbr) && _temp.SetCode == _prmSetupCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsSetup> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsSetup> _result = new List<MsSetup>();

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
                var _query =
                            (
                                from _msSetup in this.db.MsSetups
                                where _msSetup.SetGroup == SetupDataMapper.GetSetupGroup(SetupStatus.Account)
                                   && (SqlMethods.Like(_msSetup.SetCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSetup.SetDescription.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    SetGroup = _msSetup.SetGroup,
                                    SetCode = _msSetup.SetCode,
                                    SetValue = _msSetup.SetValue,
                                    SetupName = (
                                                    from _msAccount in this.db.MsAccounts
                                                    where _msAccount.Account == _msSetup.SetValue
                                                    select _msAccount.AccountName
                                                ).FirstOrDefault(),
                                    SetDescription = _msSetup.SetDescription
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsSetup(_row.SetGroup, _row.SetCode, _row.SetValue, _row.SetupName, _row.SetDescription));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<MsSetup> GetListDDL()
        //{
        //    List<MsSetup> _result = new List<MsSetup>();

        //    try
        //    {
        //        var _query =
        //                    (
        //                        from _accClass in this.db.MsSetups
        //                        select new
        //                        {
        //                            SetGroup = _accClass.SetGroup,
        //                            SetupName = _accClass.SetupName,
        //                            AccSubGroup = _accClass.AccSubGroup
        //                        }
        //                    );

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { SetGroup = this._accClassCode, SetupName = this._accClassName, AccSubGroup = this._accSubGroup });

        //            _result.Add(new MsSetup(_row.SetGroup, _row.SetupName, _row.AccSubGroup));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        #endregion


        #region MasterDefault

        public double RowsCountMasterDefault(string _prmCategory, string _prmKeyword)
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

            var _query = (
                            from _msDefault in this.db.Master_Defaults
                            where SqlMethods.Like(_msDefault.SetCode.Trim().ToLower(), _pattern1.Trim().ToLower())
                               && (SqlMethods.Like(_msDefault.SetDescription.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msDefault
                         ).Count();

            _result = _query;

            return _result;
        }

        public bool AddMasterDefault(Master_Default _prmMsDefault)
        {
            bool _result = false;

            try
            {

                this.db.Master_Defaults.InsertOnSubmit(_prmMsDefault);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditMasterDefault(Master_Default _prmMsDefault)
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

        public bool DeleteMultiMasterDefault(string[] _prmSetupCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmSetupCode.Length; i++)
                {
                    Master_Default _msDefault = this.db.Master_Defaults.Single(_temp => _temp.SetCode == _prmSetupCode[i]);

                    if (_msDefault != null)
                    {
                        var _query = (from _detail in this.db.Master_DefaultAccs
                                      where _detail.SetCode.Trim().ToLower() == _prmSetupCode[i].Trim().ToLower()
                                      select _detail);

                        this.db.Master_DefaultAccs.DeleteAllOnSubmit(_query);

                        this.db.Master_Defaults.DeleteOnSubmit(_msDefault);
                    }
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

        public Master_Default GetSingleMasterDefault(string _prmSetupCode)
        {
            Master_Default _result = null;

            try
            {
                _result = this.db.Master_Defaults.Single(_temp => _temp.SetCode == _prmSetupCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Default> GetListMasterDefault(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_Default> _result = new List<Master_Default>();

            CurrencyBL _currBL = new CurrencyBL();

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
                var _query =
                            (
                                from _msDefault in this.db.Master_Defaults
                                //join _msDefAcc in this.db.Master_DefaultAccs
                                // on _msDefault.SetCode equals _msDefAcc.SetCode
                                //join _msAccount in this.db.MsAccounts
                                // on _msDefAcc.Account equals _msAccount.Account
                                where SqlMethods.Like(_msDefault.SetCode.Trim().ToLower(), _pattern1.Trim().ToLower())
                                   && (SqlMethods.Like(_msDefault.SetDescription.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   //&& _msAccount.CurrCode == _currBL.GetCurrDefault()
                                select new
                                {
                                    SetCode = _msDefault.SetCode,
                                    SetDescription = _msDefault.SetDescription,
                                    Account = (
                                                from _msDefAcc in this.db.Master_DefaultAccs                                                     
                                                join _msAccount in this.db.MsAccounts
                                                    on _msDefAcc.Account equals _msAccount.Account
                                                where _msDefAcc.SetCode == _msDefault.SetCode
                                                    && _msAccount.CurrCode == _currBL.GetCurrDefault()
                                                select _msAccount.Account
                                              ).FirstOrDefault(),
                                    //_msDefAcc.Account,
                                    AccountName = (
                                                from _msDefAcc in this.db.Master_DefaultAccs
                                                join _msAccount in this.db.MsAccounts
                                                    on _msDefAcc.Account equals _msAccount.Account
                                                where _msDefAcc.SetCode == _msDefault.SetCode
                                                    && _msAccount.CurrCode == _currBL.GetCurrDefault()
                                                select _msAccount.AccountName
                                              ).FirstOrDefault(),
                                    //_msAccount.AccountName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Default(_row.SetCode, _row.SetDescription, _row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region MasterAcc

        public List<Master_DefaultAcc> GetListMasterDefAcc(int _prmReqPage, int _prmPageSize, string _prmSetCode)
        {
            List<Master_DefaultAcc> _result = new List<Master_DefaultAcc>();

            try
            {
                var _query =
                            (
                                from _msDefAcc in this.db.Master_DefaultAccs
                                where _msDefAcc.SetCode == _prmSetCode
                                select new
                                {
                                    SetCode = _msDefAcc.SetCode,
                                    Account = _msDefAcc.Account
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_DefaultAcc(_row.SetCode, _row.Account));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int RowsCountMasterDefAcc(string _prmSetCode)
        {
            int _result = 0;

            _result = this.db.Master_DefaultAccs.Where(_row => _row.SetCode == _prmSetCode).Count();

            return _result;
        }

        public bool DeleteMultiMasterDefAcc(string[] _prmCode, string _prmSetupCode)
        {
            bool _result = false;

            Master_Default _masterDefault = new Master_Default();

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Master_DefaultAcc _masterDefAcc = this.db.Master_DefaultAccs.Single(_defAcc => _defAcc.Account.Trim().ToLower() == _prmCode[i].Trim().ToLower() && _defAcc.SetCode.Trim().ToLower() == _prmSetupCode.Trim().ToLower());

                    this.db.Master_DefaultAccs.DeleteOnSubmit(_masterDefAcc);
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

        public bool AddMasterDefAcc(Master_DefaultAcc _prmMsDefAcc)
        {
            bool _result = false;

            try
            {

                this.db.Master_DefaultAccs.InsertOnSubmit(_prmMsDefAcc);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsCurrCodeExist(string _prmSetCode, string _prmCurr)
        {
            bool _result = false;

            try
            {
                var _query =
                    (
                        from _msDefaultAcc in this.db.Master_DefaultAccs
                        join _msAccount in this.db.MsAccounts
                        on _msDefaultAcc.Account equals _msAccount.Account
                        where _msDefaultAcc.SetCode == _prmSetCode
                        && _msAccount.CurrCode == _prmCurr
                        select new
                        {
                            SetCode = _msDefaultAcc.SetCode,
                            Account = _msDefaultAcc.Account,
                            CurrCode = _msAccount.CurrCode

                        }
                    ).Count();

                if (_query > 0)
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

        public Master_DefaultAcc GetSingleMasterDefaultAcc(string _prmSetupCode, string _prmAccount)
        {
            Master_DefaultAcc _result = null;

            try
            {
                _result = this.db.Master_DefaultAccs.Single(_temp => _temp.SetCode == _prmSetupCode.Trim().ToLower() && _temp.Account == _prmAccount.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditMasterDefaultAcc(Master_DefaultAcc _prmMsDefaultAcc)
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

        ~SetupBL()
        {
        }

    }
}
