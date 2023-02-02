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
    public sealed class MsCountryBL : Base
    {
        public MsCountryBL()
        {
        }
        ~MsCountryBL()
        {
        }

        #region Country
        public int RowsCount(String _prmCountry, String _prmKeyword)
        {
            int _result = 0;

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmCountry == "CountryCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCountry == "CountryName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result = (from _msCountry in this.db.MsCountries
                       where (SqlMethods.Like(_msCountry.CountryCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msCountry.CountryName.Trim().ToLower(), _pattern2.Trim().ToLower()))

                       select _msCountry.CountryCode).Count();
            return _result;
        }

        public List<MsCountry> GetList(int _prmReqPage, int _prmPageSize, String _prmCountry, String _prmKeyword)
        {
            List<MsCountry> _result = new List<MsCountry>();

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmCountry == "CountryCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            if (_prmCountry == "CountryName")
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
                                orderby _msCountry.CountryCode ascending
                                select new
                                {
                                    CountryCode = _msCountry.CountryCode,
                                    CountryName = _msCountry.CountryName,

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsCountry(_row.CountryCode, _row.CountryName));
                }
            }
            catch (Exception )
            {
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
                                orderby _msCountry.CountryCode ascending
                                select new
                                {
                                    CountryCode = _msCountry.CountryCode,
                                    CountryName = _msCountry.CountryName,

                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsCountry(_row.CountryCode, _row.CountryName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public MsCountry GetSingle(String _prmCode)
        {
            MsCountry _result = null;

            try
            {
                _result = this.db.MsCountries.Single(_temp => _temp.CountryCode == _prmCode);
            }
            catch (Exception)           {
            }

            return _result;
        }

        public String GetCountryNameByCode(String _prmCode)
        {
            String _result = "";

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
                    MsCountry _msCountry = this.db.MsCountries.Single(_temp => _temp.CountryCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsCountries.DeleteOnSubmit(_msCountry);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
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
            }

            return _result;
        }
        public List<MsCountry> GetCountryForDDL()
        {
            List<MsCountry> _result = new List<MsCountry>();

            try
            {
                var _query = (
                                from _msCountry in this.db.MsCountries
                                //where _msRegional.RegionalCode == _prmCode
                                select new
                                {
                                    CountryCode = _msCountry.CountryCode,
                                    CountryName = _msCountry.CountryName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsCountry(_row.CountryCode , _row.CountryName ));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        #endregion

        }
    }
}
