using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class CashFlowGroupBL : Base
    {
        public CashFlowGroupBL()
        {
        }

        #region Cash Flow Group
        public double RowsCountCashFlowGroup(string _prmCategory, string _prmKeyword)
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

            try
            {
                var _query =
                            (
                                from _finMsCashFlowGroup in this.db.FINMsCashFlowGroups
                                where (SqlMethods.Like(_finMsCashFlowGroup.CashFlowGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_finMsCashFlowGroup.CashFlowGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _finMsCashFlowGroup.CashFlowGroupCode
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINMsCashFlowGroup> GetListFINMsCashFlowGroup(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINMsCashFlowGroup> _result = new List<FINMsCashFlowGroup>();

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
                                from _finMsCashFlowGroup in this.db.FINMsCashFlowGroups
                                where (SqlMethods.Like(_finMsCashFlowGroup.CashFlowGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_finMsCashFlowGroup.CashFlowGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _finMsCashFlowGroup.EditDate descending
                                select new
                                {
                                    CashFlowGroupCode = _finMsCashFlowGroup.CashFlowGroupCode,
                                    CashFlowGroupName = _finMsCashFlowGroup.CashFlowGroupName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINMsCashFlowGroup(_row.CashFlowGroupCode, _row.CashFlowGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINMsCashFlowGroup GetSingleFINMsCashFlowGroup(string _prmCode)
        {
            FINMsCashFlowGroup _result = null;

            try
            {
                _result = this.db.FINMsCashFlowGroups.Single(_temp => _temp.CashFlowGroupCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINMsCashFlowGroup(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINMsCashFlowGroup _finMsCashFlowGroup = this.db.FINMsCashFlowGroups.Single(_temp => _temp.CashFlowGroupCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finMsCashFlowGroup != null)
                    {
                        var _query = (from _detail in this.db.FINMsCashFlowGroupDts
                                      where _detail.CashFlowGroupCode.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                      select _detail);

                        this.db.FINMsCashFlowGroupDts.DeleteAllOnSubmit(_query);

                        this.db.FINMsCashFlowGroups.DeleteOnSubmit(_finMsCashFlowGroup);

                        _result = true;
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

        public Boolean AddFINMsCashFlowGroup(FINMsCashFlowGroup _prmFINMsCashFlowGroup)
        {
            Boolean _result = false;

            try
            {
                this.db.FINMsCashFlowGroups.InsertOnSubmit(_prmFINMsCashFlowGroup);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINMsCashFlowGroup(FINMsCashFlowGroup _prmFINMsCashFlowGroup)
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

        #region Detail
        public double RowsCountCashFlowGroupDt(String _prmCode)
        {
            double _result = 0;
           
            try
            {
                var _query =
                            (
                                from _finMsCashFlowGroupDt in this.db.FINMsCashFlowGroupDts
                                where _finMsCashFlowGroupDt.CashFlowGroupCode == _prmCode
                                select _finMsCashFlowGroupDt.CashFlowGroupCode
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINMsCashFlowGroupDt> GetListFINMsCashFlowGroupDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<FINMsCashFlowGroupDt> _result = new List<FINMsCashFlowGroupDt>();

            try
            {
                var _query = (
                                from _finMsCashFlowGroupDt in this.db.FINMsCashFlowGroupDts
                                where _finMsCashFlowGroupDt.CashFlowGroupCode == _prmCode
                                orderby _finMsCashFlowGroupDt.Account ascending
                                select new
                                {
                                    CashFlowGroupCode = _finMsCashFlowGroupDt.CashFlowGroupCode,
                                    Account = _finMsCashFlowGroupDt.Account,
                                    AccountName = (
                                                    from _msAccount in this.db.MsAccounts
                                                    where _finMsCashFlowGroupDt.Account == _msAccount.Account
                                                    select _msAccount.AccountName
                                                 ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINMsCashFlowGroupDt(_row.CashFlowGroupCode, _row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINMsCashFlowGroupDt GetSingleFINMsCashFlowGroupDt(string _prmCode, string _prmAccount)
        {
            FINMsCashFlowGroupDt _result = null;

            try
            {
                _result = this.db.FINMsCashFlowGroupDts.Single(_temp => _temp.CashFlowGroupCode == _prmCode && _temp.Account == _prmAccount);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINMsCashFlowGroupDt(string[] _prmAccount, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmAccount.Length; i++)
                {
                    FINMsCashFlowGroupDt _finMsCashFlowGroupDt = this.db.FINMsCashFlowGroupDts.Single(_temp => _temp.Account == _prmAccount[i] && _temp.CashFlowGroupCode.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.FINMsCashFlowGroupDts.DeleteOnSubmit(_finMsCashFlowGroupDt);
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

        public bool AddFINMsCashFlowGroupDt(FINMsCashFlowGroupDt _prmFINMsCashFlowGroupDt)
        {
            bool _result = false;

            try
            {
                this.db.FINMsCashFlowGroupDts.InsertOnSubmit(_prmFINMsCashFlowGroupDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINMsCashFlowGroupDt(FINMsCashFlowGroupDt _prmFINMsCashFlowGroupDt)
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

        ~CashFlowGroupBL()
        {
        }
    }
}
