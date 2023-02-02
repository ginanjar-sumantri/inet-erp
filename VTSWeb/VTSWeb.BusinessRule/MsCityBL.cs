using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using VTSWeb.Database;
using VTSWeb.SystemConfig;

namespace VTSWeb.BusinessRule
{
    public sealed class MsCityBL : Base
    {

        public MsCityBL()
        {
        }
        ~MsCityBL()
        {
        }

        #region City
        public int RowsCount(String _prmCity, String _prmKeyword)
        {
            int _result = 0;

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmCity == "CityCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCity == "CityName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result = (from _msCity in this.db.MsCities
                       where (SqlMethods.Like(_msCity.CityCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                          && (SqlMethods.Like(_msCity.CityName.Trim().ToLower(), _pattern2.Trim().ToLower()))

                       select _msCity.CityCode).Count();
            return _result;
        }

        public List<MsCity> GetList(int _prmReqPage, int _prmPageSize, String _prmCity, String _prmKeyword)
        {
            List<MsCity> _result = new List<MsCity>();
            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmCity == "CityCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            if (_prmCity == "CityName")
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
                                   && (SqlMethods.Like(_msCity.CityName.Trim().ToLower(),_pattern2.Trim().ToLower()))
                                orderby _msCity.CityCode ascending
                                select new
                                {
                                    CityCode = _msCity.CityCode,
                                    CityName = _msCity.CityName,
                                    Regional = _msRegional.RegionalName,
                                    Country = _msCountry.CountryName,

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsCity(_row.CityCode,_row.CityName,_row.Regional,_row.Country));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public List<MsCity> GetList()
        {
            List<MsCity> _result = new List<MsCity>();

            try
            {
                var _query = (
                                from _msCity in this.db.MsCities
                                join _msRegional in this.db.MsRegionals 
                                on _msCity.Regional equals _msRegional.RegionalCode
                                join _msCountry in this.db.MsCountries
                                on _msCity.Country equals _msCountry.CountryCode
                                orderby _msCity.CityCode ascending
                                select new
                                {
                                    CityCode = _msCity.CityCode,
                                    CityName = _msCity.CityName,
                                    Regional = _msRegional.RegionalName,
                                    Country  = _msCountry.CountryName,
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsCity(_row.CityCode, _row.CityName, _row.Regional, _row.Country));
                }
            }
            catch (Exception )
            {
            }

            return _result;
        }

        public MsCity GetSingle(String _prmCode)
        {
            MsCity _result = null;

            try
            {
                _result = this.db.MsCities.Single(_temp => _temp.CityCode == _prmCode);
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public String GetCityNameByCode(String _prmCode)
        {
            String _result = "";

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
            catch (Exception)
            {
            }

            return _result;
        }

        public bool DeleteMulti(String[] _prmCode)
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
            catch (Exception)
            {
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
            catch (Exception)
            {
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
            catch (Exception)           {
            }

            return _result;
        }
        public List<MsCity> GetCityForDDL()
        {
            List<MsCity> _result = new List<MsCity>();

            try
            {
                var _query = (
                                from _msCity in this.db.MsCities
                                //where _msRegional.RegionalCode == _prmCode
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
            catch (Exception)
            {
            }

            return _result;
        }
        #endregion


    }
}
