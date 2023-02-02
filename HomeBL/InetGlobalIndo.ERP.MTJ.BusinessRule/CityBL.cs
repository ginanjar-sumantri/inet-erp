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
    public sealed class CityBL : Base
    {
        public CityBL()
        {
        }

        #region City
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
                            from _msCity in this.db.MsCities
                            join _msRegional in this.db.MsRegionals
                                    on _msCity.Regional equals _msRegional.RegionalCode
                            join _msCountry in this.db.MsCountries
                                on _msCity.Country equals _msCountry.CountryCode
                            where (SqlMethods.Like(_msCity.CityCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msCity.CityName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msCity
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MsCity> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsCity> _result = new List<MsCity>();

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
                                from _msCity in this.db.MsCities
                                join _msRegional in this.db.MsRegionals
                                    on _msCity.Regional equals _msRegional.RegionalCode
                                join _msCountry in this.db.MsCountries
                                    on _msCity.Country equals _msCountry.CountryCode
                                where (SqlMethods.Like(_msCity.CityCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCity.CityName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msCity.UserDate descending
                                select new
                                {
                                    CityCode = _msCity.CityCode,
                                    CityName = _msCity.CityName,
                                    RegionalName = _msRegional.RegionalName,
                                    CountryName = _msCountry.CountryName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsCity(_row.CityCode, _row.CityName, _row.RegionalName, _row.CountryName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsCity> GetListCityForDDL()
        {
            List<MsCity> _result = new List<MsCity>();

            try
            {
                var _query = (
                                from _msCity in this.db.MsCities
                                orderby _msCity.CityName
                                select new
                                {
                                    CityCode = _msCity.CityCode,
                                    CityName = _msCity.CityName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsCity(_row.CityCode, _row.CityName));
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
                    MsCity _msCity = this.db.MsCities.Single(_temp => _temp.CityCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsCities.DeleteOnSubmit(_msCity);
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

        public MsCity GetSingle(string _prmCode)
        {
            MsCity _result = null;

            try
            {
                _result = this.db.MsCities.Single(_temp => _temp.CityCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCityNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msCity in this.db.MsCities
                                where _msCity.CityCode == _prmCode
                                select new
                                {
                                    CityName = _msCity.CityName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CityName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(MsCity _prmMsCity)
        {
            bool _result = false;

            try
            {
                this.db.MsCities.InsertOnSubmit(_prmMsCity);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsCity _prmMsCity)
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

        public bool IsCityExists(String _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _city in this.db.MsCities
                                where _city.CityCode == _prmCode
                                select _city
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


        ~CityBL()
        {
        }
    }
}
