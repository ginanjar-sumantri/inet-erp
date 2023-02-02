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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public sealed class TermBL : Base
    {
        public TermBL()
        {

        }

        #region TermDt
        public bool DeleteMultiTermDt(string[] _prmTermCode, int _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTermCode.Length; i++)
                {
                    MsTermDt _msTermDt = this.db.MsTermDts.Single(_termDt => _termDt.TermCode.Trim().ToLower() == _prmTermCode[i].Trim().ToLower() && _termDt.Period == _prmCode);

                    this.db.MsTermDts.DeleteOnSubmit(_msTermDt);
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

        public MsTermDt GetSingleTermDt(string _prmTermCode, int _prmCode)
        {
            MsTermDt _result = null;

            try
            {
                _result = this.db.MsTermDts.Single(_termDt => _termDt.TermCode == _prmTermCode && _termDt.Period == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int RowsCountTermDt
        {
            get
            {
                return this.db.MsTermDts.Count();
            }
        }

        public List<MsTermDt> GetListTermDt(string _prmTerm)
        {
            List<MsTermDt> _result = new List<MsTermDt>();

            try
            {
                var _query = (
                                from _msTermDt in this.db.MsTermDts
                                where _msTermDt.TermCode == _prmTerm
                                orderby _msTermDt.Period ascending
                                select new
                                {
                                    TermCode = _msTermDt.TermCode,
                                    Period = _msTermDt.Period,
                                    TypeRange = _msTermDt.TypeRange,
                                    XRange = _msTermDt.XRange,
                                    PercentBase = _msTermDt.PercentBase,
                                    PercentPPn = _msTermDt.PercentPPn
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsTermDt(_row.TermCode, _row.Period, _row.TypeRange, _row.XRange, _row.PercentBase, _row.PercentPPn));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditTermDt(MsTermDt _prmMsTermDt)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                var _query = (
                                from _termDt in this.db.MsTermDts
                                where _termDt.TermCode == _prmMsTermDt.TermCode
                                group _termDt by _termDt.TermCode into _grp
                                select new
                                {
                                    PercentBase = _grp.Sum(a => a.PercentBase),
                                    PercentPPn = _grp.Sum(a => a.PercentPPn)
                                }
                             );

                foreach (var _obj in _query)
                {
                    MsTerm _msTerm = this.GetSingle(_prmMsTermDt.TermCode);

                    if (_obj.PercentBase == 100 && _obj.PercentPPn == 100)
                    {
                        _msTerm.IsValid = TermDataMapper.IsValid(true);

                        this.db.SubmitChanges();
                    }
                    else
                    {
                        _msTerm.IsValid = TermDataMapper.IsValid(false);

                        this.db.SubmitChanges();
                    }
                }

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddMulti(List<MsTermDt> _prmListTerm)
        {
            bool _result = false;

            try
            {
                foreach (MsTermDt _obj in _prmListTerm)
                {
                    this.db.MsTermDts.InsertOnSubmit(_obj);
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

        #region Term
        public MsTerm GetSingle(string _prmTermCode)
        {
            MsTerm _result = null;

            try
            {
                _result = this.db.MsTerms.Single(_term => _term.TermCode == _prmTermCode);
            }
            catch (Exception ex)
            {
                //bikin object untuk handling error
            }

            return _result;
        }

        public string GetTermNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msTerm in this.db.MsTerms
                                where _msTerm.TermCode == _prmCode
                                select new
                                {
                                    TermName = _msTerm.TermName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.TermName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

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
                             from _term in this.db.MsTerms
                             where _term.IsValid == TermDataMapper.IsValid(true)
                               && (SqlMethods.Like(_term.TermCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_term.TermName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _term
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MsTerm> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsTerm> _result = new List<MsTerm>();

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
                            from _term in this.db.MsTerms
                            where _term.IsValid == TermDataMapper.IsValid(true)
                              && (SqlMethods.Like(_term.TermCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like(_term.TermName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            orderby _term.UserDate descending
                            select new
                            {
                                TermCode = _term.TermCode,
                                TermName = _term.TermName,
                                XPeriod = _term.XPeriod,
                                TypeRange = _term.TypeRange,
                                XRange = _term.XRange
                            }
                        ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

            foreach (object _obj in _query)
            {
                var _row = _obj.Template(new { TermCode = this._string, TermName = this._string, XPeriod = this._int, TypeRange = this._string, XRange = this._int });

                _result.Add(new MsTerm(_row.TermCode, _row.TermName, _row.XPeriod, _row.TypeRange, _row.XRange));
            }

            return _result;
        }

        public List<MsTerm> GetList()
        {
            List<MsTerm> _result = new List<MsTerm>();

            var _query = (
                            from term in this.db.MsTerms
                            where term.IsValid == 'Y'
                            orderby term.UserDate descending
                            select new
                            {
                                TermCode = term.TermCode,
                                TermName = term.TermName,
                                XPeriod = term.XPeriod,
                                TypeRange = term.TypeRange,
                                XRange = term.XRange
                            }
                        ).Distinct();

            foreach (object _obj in _query)
            {
                var _row = _obj.Template(new { TermCode = this._string, TermName = this._string, XPeriod = this._int, TypeRange = this._string, XRange = this._int });

                _result.Add(new MsTerm(_row.TermCode, _row.TermName, _row.XPeriod, _row.TypeRange, _row.XRange));
            }

            return _result;
        }

        public List<MsTerm> GetListTermForDDL()
        {
            List<MsTerm> _result = new List<MsTerm>();

            var _query = (
                            from term in this.db.MsTerms
                            where term.IsValid == 'Y'
                            orderby term.UserDate descending
                            select new
                            {
                                TermCode = term.TermCode,
                                TermName = term.TermName
                            }
                        ).Distinct();

            foreach (object _obj in _query)
            {
                var _row = _obj.Template(new { TermCode = this._string, TermName = this._string });

                _result.Add(new MsTerm(_row.TermCode, _row.TermName));
            }

            return _result;
        }

        public bool Edit(MsTerm _prmMsTerm)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //bikin object untuk handling error
            }

            return _result;
        }

        public bool Add(MsTerm _prmMsTerm)
        {
            bool _result = false;

            try
            {
                this.db.MsTerms.InsertOnSubmit(_prmMsTerm);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //bikin object untuk handling error
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmTermCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTermCode.Length; i++)
                {
                    var _query = (from _detail in this.db.MsTermDts
                                  where _detail.TermCode == _prmTermCode[i]
                                  select _detail);

                    this.db.MsTermDts.DeleteAllOnSubmit(_query);

                    MsTerm _msTerm = this.db.MsTerms.Single(_term => _term.TermCode.Trim().ToLower() == _prmTermCode[i].Trim().ToLower());

                    this.db.MsTerms.DeleteOnSubmit(_msTerm);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //bikin object untuk handling error
            }

            return _result;
        }

        public bool IsTermExists(String _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _term in this.db.MsTerms
                                where _term.TermCode == _prmCode
                                select _term
                             );

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

        ~TermBL()
        {

        }
    }
}