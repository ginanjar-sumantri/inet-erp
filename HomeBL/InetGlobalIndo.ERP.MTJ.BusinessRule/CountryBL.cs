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
    public sealed class CountryBL : Base
    {
        public CountryBL()
        {

        }

        #region Country

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
                             from _msCountry in this.db.MsCountries
                             where (SqlMethods.Like(_msCountry.CountryCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_msCountry.CountryName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _msCountry
                        ).Count();

            _result = _query;

            return _result;
        }


        public List<MsCountry> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsCountry> _result = new List<MsCountry>();

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
                                from _msCountry in this.db.MsCountries
                                where (SqlMethods.Like(_msCountry.CountryCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCountry.CountryName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msCountry.UserDate descending
                                select new
                                {
                                    CountryCode = _msCountry.CountryCode,
                                    CountryName = _msCountry.CountryName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CountryCode = this._string, CountryName = this._string });

                    _result.Add(new MsCountry(_row.CountryCode, _row.CountryName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsCountry> GetList()
        {
            List<MsCountry> _result = new List<MsCountry>();

            try
            {
                var _query = (
                                from _msCountry in this.db.MsCountries
                                orderby _msCountry.CountryName ascending
                                select new
                                {
                                    CountryCode = _msCountry.CountryCode,
                                    CountryName = _msCountry.CountryName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CountryCode = this._string, CountryName = this._string });

                    _result.Add(new MsCountry(_row.CountryCode, _row.CountryName));
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
                    MsCountry _msCountry = this.db.MsCountries.Single(_temp => _temp.CountryCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsCountries.DeleteOnSubmit(_msCountry);
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

        public MsCountry GetSingle(string _prmCode)
        {
            MsCountry _result = null;

            try
            {
                _result = this.db.MsCountries.Single(_temp => _temp.CountryCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCountryNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msCountry in this.db.MsCountries
                                where _msCountry.CountryCode == _prmCode
                                select new
                                {
                                    CountryName = _msCountry.CountryName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CountryName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(MsCountry _prmMsCountry)
        {
            bool _result = false;

            try
            {
                this.db.MsCountries.InsertOnSubmit(_prmMsCountry);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsCountry _prmMsCountry)
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

        ~CountryBL()
        {

        }
    }
}
