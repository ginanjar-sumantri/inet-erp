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
    public sealed class GLBudgetBL : Base
    {
        public GLBudgetBL()
        {
        }

        #region GLBudget

        public double RowsCountGLBudget(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "OrgUnit")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query = (
                                from _glBudget in this.db.GLBudgets
                                join _orgUnit in this.db.Master_OrganizationUnits
                                    on _glBudget.OrgUnit equals _orgUnit.OrgUnit
                                where (SqlMethods.Like(_orgUnit.Description.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                select _glBudget.BudgetCode
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<GLBudget> GetListGLBudget(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<GLBudget> _result = new List<GLBudget>();

            string _pattern1 = "%%";

            if (_prmCategory == "OrgUnit")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _glBudget in this.db.GLBudgets
                                join _orgUnit in this.db.Master_OrganizationUnits
                                   on _glBudget.OrgUnit equals _orgUnit.OrgUnit
                                where (SqlMethods.Like(_orgUnit.Description.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _glBudget.EditDate descending
                                select new
                                {
                                    BudgetCode = _glBudget.BudgetCode,
                                    StartDate = _glBudget.StartDate,
                                    EndDate = _glBudget.EndDate,
                                    OrgUnit = _glBudget.OrgUnit,
                                    OrgUnitName = _orgUnit.Description,
                                    Status = _glBudget.Status,
                                    Remark = _glBudget.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GLBudget(_row.BudgetCode, _row.StartDate, _row.EndDate, _row.OrgUnit, _row.OrgUnitName, _row.Status, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiGLBudget(string[] _prmHeaderCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmHeaderCode.Length; i++)
                {
                    GLBudget _glBudget = this.db.GLBudgets.Single(_temp => _temp.BudgetCode == new Guid(_prmHeaderCode[i]));

                    if (_glBudget != null)
                    {
                        if (_glBudget.Status != GLBudgetDataMapper.GetStatus(GLBudgetStatus.Approved))
                        {
                            var _query = (from _detail in this.db.GLBudgetAccs
                                          where _detail.BudgetCode == new Guid(_prmHeaderCode[i])
                                          select _detail);

                            this.db.GLBudgetAccs.DeleteAllOnSubmit(_query);

                            this.db.GLBudgets.DeleteOnSubmit(_glBudget);

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

        public string AddGLBudget(GLBudget _prmGLBudget)
        {
            string _result = "";

            try
            {
                String _exist = IsExist(_prmGLBudget.StartDate, _prmGLBudget.EndDate, _prmGLBudget.OrgUnit);
                if (_exist == "")
                {
                    this.db.GLBudgets.InsertOnSubmit(_prmGLBudget);
                    this.db.SubmitChanges();

                    _result = _prmGLBudget.BudgetCode.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string IsExist(DateTime _prmStartDate, DateTime _prmEndDate, String _prmOrgUnit)
        {
            string _result = "";

            try
            {
                var _query = (from _glBudget in this.db.GLBudgets
                              select new
                                          {
                                              StartDate = _glBudget.StartDate,
                                              EndDate = _glBudget.EndDate,
                                              OrgUnit = _glBudget.OrgUnit
                                          }
                              );

                foreach (var _item in _query)
                {
                    if (_prmStartDate >= _item.StartDate && _prmStartDate <= _item.EndDate && _item.OrgUnit == _prmOrgUnit)
                    {
                        _result = "Invalid Date Range";
                        break;
                    }
                    if (_prmEndDate >= _item.StartDate && _prmEndDate <= _item.EndDate && _item.OrgUnit == _prmOrgUnit)
                    {
                        _result = "Invalid Date Range";
                        break;
                    }
                    if (_prmStartDate <= _item.StartDate && _prmEndDate >= _item.EndDate && _item.OrgUnit == _prmOrgUnit)
                    {
                        _result = "Invalid Date Range";
                        break;
                    }
                    if (_prmStartDate > _prmEndDate && _item.OrgUnit == _prmOrgUnit)
                    {
                        _result = "Invalid Date Range";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLBudget GetSingleHd(Guid _prmGLBudgetCode)
        {
            GLBudget _result = null;

            try
            {
                _result = this.db.GLBudgets.Single(_temp => _temp.BudgetCode == _prmGLBudgetCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGLBudget(GLBudget _prmGLBudget)
        {
            bool _result = false;

            try
            {
                String _exist = IsExist(_prmGLBudget.StartDate, _prmGLBudget.EndDate, _prmGLBudget.OrgUnit);
                if (_exist == "")
                {
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAppr(Guid _prmBudgetCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.S_GLBudgetGetAppr(_prmBudgetCode, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(Guid _prmBudgetCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.S_GLBudgetApprove(_prmBudgetCode, ref _result);

                if (_result == "")
                {
                    _result = "Approve Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public String GetBudgetCodeByOrgUnitAndDate(String _prmOrgUnit, DateTime _prmTransDate)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _glbudget in this.db.GLBudgets
                                where _glbudget.OrgUnit == _prmOrgUnit
                                    && (_glbudget.StartDate <= _prmTransDate && _glbudget.EndDate >= _prmTransDate)
                                    && _glbudget.Status == GLBudgetDataMapper.GetStatus(GLBudgetStatus.Approved)
                                select _glbudget.BudgetCode
                              ).FirstOrDefault();

                if (_query.ToString() != "")
                {
                    _result = _query.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region GLBudgetAcc
        public double RowsCountGLBudgetAcc(Guid _prmBudgetCode)
        {
            double _result = 0;

            var _query = (
                                from _glBudgetAcc in this.db.GLBudgetAccs
                                where _glBudgetAcc.BudgetCode == _prmBudgetCode
                                select _glBudgetAcc.BudgetDetailCode
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<GLBudgetAcc> GetListDt(int _prmReqPage, int _prmPageSize, Guid _prmBudgetCode)
        {
            List<GLBudgetAcc> _result = new List<GLBudgetAcc>();

            try
            {
                var _query = (
                                from _glBudgetAcc in this.db.GLBudgetAccs
                                where _glBudgetAcc.BudgetCode == _prmBudgetCode
                                select new
                                {
                                    BudgetDetailCode = _glBudgetAcc.BudgetDetailCode,
                                    BudgetCode = _glBudgetAcc.BudgetCode,
                                    Account = _glBudgetAcc.Account,
                                    AccountName = (
                                                    from _msAccount in this.db.MsAccounts
                                                    where _glBudgetAcc.Account == _msAccount.Account
                                                    select _msAccount.AccountName
                                                   ).FirstOrDefault(),
                                    AmountBudgetRate = _glBudgetAcc.AmountBudgetRate,
                                    AmountBudgetForex = _glBudgetAcc.AmountBudgetForex,
                                    AmountBudgetHome = _glBudgetAcc.AmountBudgetHome,
                                    AmountActual = _glBudgetAcc.AmountActual
                                }
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GLBudgetAcc(_row.BudgetDetailCode, _row.BudgetCode, _row.Account, _row.AccountName, _row.AmountBudgetRate, _row.AmountBudgetForex, _row.AmountBudgetHome, _row.AmountActual));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiGLBudgetAcc(Guid _prmBudgetCode, string[] _prmBudgetDetailCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmBudgetDetailCode.Length; i++)
                {
                    GLBudgetAcc _GLBudgetAcc = this.db.GLBudgetAccs.Single(_temp => _temp.BudgetDetailCode == new Guid(_prmBudgetDetailCode[i]) && _temp.BudgetCode == _prmBudgetCode);

                    this.db.GLBudgetAccs.DeleteOnSubmit(_GLBudgetAcc);
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

        public bool AddGLBudgetAcc(GLBudgetAcc _prmGLBudgetAcc)
        {
            bool _result = false;

            try
            {
                this.db.GLBudgetAccs.InsertOnSubmit(_prmGLBudgetAcc);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLBudgetAcc GetSingleGLBudgetAcc(Guid _prmBudgetCode, Guid _prmBudgetDetailCode)
        {
            GLBudgetAcc _result = null;

            try
            {
                _result = this.db.GLBudgetAccs.Single(_temp => (_temp.BudgetDetailCode == _prmBudgetDetailCode) && (_temp.BudgetCode == _prmBudgetCode));

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGLBudgetAcc(GLBudgetAcc _prmGLBudgetAcc)
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

        public Guid GetBudgetDetailCodeByAccountAndBudgetCode(Guid _prmBudgetCode, String _prmAccount)
        {
            Guid _result = new Guid();

            try
            {
                _result = this.db.GLBudgetAccs.Single(_temp => _temp.BudgetCode == _prmBudgetCode && _temp.Account == _prmAccount).BudgetDetailCode;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region GLBudgetYearHd

        public double RowsCountGLBudgetYearHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Revisi")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "Remark")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }


            var _query = (
                                from _glBudgetYearHds in this.db.GLBudgetYearHds
                                where (SqlMethods.Like(_glBudgetYearHds.Revisi.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_glBudgetYearHds.Remark.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _glBudgetYearHds.Revisi
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<GLBudgetYearHd> GetListGLBudgetYearHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<GLBudgetYearHd> _result = new List<GLBudgetYearHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Revisi")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "Remark")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "Year")
            {
                _pattern3 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (from _glBudgetYearHd in this.db.GLBudgetYearHds
                              orderby _glBudgetYearHd.Year, _glBudgetYearHd.Revisi
                              where (SqlMethods.Like(_glBudgetYearHd.Revisi.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like(_glBudgetYearHd.Remark.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                              && (SqlMethods.Like(_glBudgetYearHd.Year.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                              select _glBudgetYearHd
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiGLBudgetYearHd(string[] _prmHeaderCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmHeaderCode.Length; i++)
                {
                    string[] _yearRevisi = _prmHeaderCode[i].Split('|');

                    GLBudgetYearHd _glBudgetYearHd = this.db.GLBudgetYearHds.Single(_temp => _temp.Year.ToString() == _yearRevisi[0] & _temp.Revisi.ToString() == _yearRevisi[1]);

                    if (_glBudgetYearHd != null)
                    {
                        //if (_glBudgetYearHd.Status != GLBudgetDataMapper.GetStatus(GLBudgetStatus.Approved))
                        //{
                        this.db.GLBudgetYearHds.DeleteOnSubmit(_glBudgetYearHd);

                        _result = true;
                        //}
                        //else
                        //{
                        //    _result = false;
                        //    break;
                        //}
                    }
                    else
                    {
                        _result = false;
                        break;
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

        public string AddGLBudgetYearHd(GLBudgetYearHd _prmGLBudgetYearHd)
        {
            string _result = "";

            try
            {
                this.db.GLBudgetYearHds.InsertOnSubmit(_prmGLBudgetYearHd);
                this.db.SubmitChanges();
                _result = _prmGLBudgetYearHd.Year.ToString() + "|" + _prmGLBudgetYearHd.Revisi.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLBudgetYearHd GetSingleGLBudgetYearHd(Int32 _prmYear, Int32 _prmRevisi)
        {
            GLBudgetYearHd _result = null;

            try
            {
                _result = this.db.GLBudgetYearHds.Single(_temp => _temp.Year == _prmYear & _temp.Revisi == _prmRevisi);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGLBudgetYearHd(GLBudgetYearHd _prmGLBudgetYearHd)
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

        public string GetApprGLBudgetYearHd(Int32 _prmYear, Int32 _prmRevisi, String _prmUser)
        {
            string _result = "";

            try
            {
                int _success = this.db.S_GLBudgetYearGetAppr(_prmYear, _prmRevisi, _prmUser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string ApproveGLBudgetYearHd(Int32 _prmYear, Int32 _prmRevisi, String _prmUser)
        {
            string _result = "";

            try
            {
                int _success = this.db.S_GLBudgetYearApprove(_prmYear, _prmRevisi, _prmUser, ref _result);

                if (_result == "")
                {
                    _result = "Approve Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string PostingGLBudgetYearHd(Int32 _prmYear, Int32 _prmRevisi, String _prmUser)
        {
            string _result = "";

            try
            {
                int _success = this.db.S_GLBudgetYearPost(_prmYear, _prmRevisi, _prmUser, ref _result);

                if (_result == "")
                {
                    _result = "Posting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string UnpostGLBudgetYearHd(Int32 _prmYear, Int32 _prmRevisi, String _prmUser)
        {
            string _result = "";

            try
            {
                int _success = this.db.S_GLBudgetYearUnPost(_prmYear, _prmRevisi, _prmUser, ref _result);

                if (_result == "")
                {
                    _result = "Unpost Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Unpost Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string GLBudgetYearCreateRevisi(Int32 _prmYear, string _prmUser)
        {
            string _result = "";
            try
            {
                this.db.S_GLBudgetYearCreateRevisi(_prmYear, _prmUser, ref _result);
            }
            catch
            {
                _result = "Error Create Revisi GLBudgetYear.";
            }
            return _result;
        }

        #endregion

        #region GLBudgetYearDt

        public int RowsCountGLBudgetYearDt(Int32 _prmYear, Int32 _prmRevisi)
        {
            int _result = 0;

            _result = this.db.GLBudgetYearDts.Where(_row => _row.Year == _prmYear & _row.Revisi == _prmRevisi).Count();

            return _result;
        }

        public List<GLBudgetYearDt> GetListGLBudgetYearDt(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            string[] _pattern1 = _prmKeyword.Split('|');
            List<GLBudgetYearDt> _result = new List<GLBudgetYearDt>();

            try
            {
                var _query = (from _glBudgetYearDts in this.db.GLBudgetYearDts
                              where _glBudgetYearDts.Year == Convert.ToInt32(_pattern1[0])
                              && _glBudgetYearDts.Revisi == Convert.ToInt32(_pattern1[1])
                              select _glBudgetYearDts
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLBudgetYearDt GetSingleGLBudgetYearDt(Int32 _prmYear, Int32 _prmRevisi, Int32 _prmItemNo)
        {
            GLBudgetYearDt _result = null;

            try
            {
                _result = this.db.GLBudgetYearDts.FirstOrDefault(_temp => _temp.Year == _prmYear & _temp.Revisi == _prmRevisi & _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiGLBudgetYearDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _prmItem = _prmCode[i].Split('|');
                    GLBudgetYearDt _glBudgetYearDt = this.db.GLBudgetYearDts.FirstOrDefault(_temp => _temp.Year.ToString() == _prmItem[0] && _temp.Revisi.ToString() == _prmItem[1] && _temp.ItemNo.ToString() == _prmItem[2]);
                    this.db.GLBudgetYearDts.DeleteOnSubmit(_glBudgetYearDt);
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

        public bool AddGLBudgetYearDt(GLBudgetYearDt _prmGLBudgetYearDt)
        {
            bool _result = false;

            try
            {
                this.db.GLBudgetYearDts.InsertOnSubmit(_prmGLBudgetYearDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditGLBudgetYearDt(GLBudgetYearDt _prmGLBudgetYearDt)
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

        ~GLBudgetBL()
        {
        }
    }
}