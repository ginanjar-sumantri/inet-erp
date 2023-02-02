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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public sealed class RegionalBL : Base
    {
        public RegionalBL()
        {

        }

        #region Regional

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
                            from _msRegional in this.db.MsRegionals
                            where (SqlMethods.Like(_msRegional.RegionalCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msRegional.RegionalName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msRegional
                        ).Count();

            _result = _query;

            return _result;
        }


        public List<MsRegional> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsRegional> _result = new List<MsRegional>();

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
                                from _msRegional in this.db.MsRegionals
                                where (SqlMethods.Like(_msRegional.RegionalCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msRegional.RegionalName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msRegional.UserDate descending
                                select new
                                {
                                    RegionalCode = _msRegional.RegionalCode,
                                    RegionalName = _msRegional.RegionalName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { RegionalCode = this._string, RegionalName = this._string });

                    _result.Add(new MsRegional(_row.RegionalCode, _row.RegionalName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsRegional> GetList()
        {
            List<MsRegional> _result = new List<MsRegional>();

            try
            {
                var _query = (
                                from _msRegional in this.db.MsRegionals
                                orderby _msRegional.RegionalName ascending
                                select new
                                {
                                    RegionalCode = _msRegional.RegionalCode,
                                    RegionalName = _msRegional.RegionalName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { RegionalCode = this._string, RegionalName = this._string });

                    _result.Add(new MsRegional(_row.RegionalCode, _row.RegionalName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsRegional _msRegional = this.db.MsRegionals.Single(_temp => _temp.RegionalCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsRegionals.DeleteOnSubmit(_msRegional);
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

        public MsRegional GetSingle(string _prmCode)
        {
            MsRegional _result = null;

            try
            {
                _result = this.db.MsRegionals.Single(_temp => _temp.RegionalCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetRegionalNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msRegional in this.db.MsRegionals
                                where _msRegional.RegionalCode == _prmCode
                                select new
                                {
                                    RegionalName = _msRegional.RegionalName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.RegionalName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(MsRegional _prmMsRegional)
        {
            bool _result = false;

            try
            {
                this.db.MsRegionals.InsertOnSubmit(_prmMsRegional);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsRegional _prmMsRegional)
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

        ~RegionalBL()
        {

        }
    }
}
