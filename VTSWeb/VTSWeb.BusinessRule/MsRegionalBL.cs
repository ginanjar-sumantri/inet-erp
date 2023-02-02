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
    public sealed class MsRegionalBL : Base
    {

        public MsRegionalBL()
        {
        }
        ~MsRegionalBL()
        {
        }

        #region Regional
        public int RowsCount(String _prmRegional, String _prmKeyword)
        {
            int _result = 0;

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmRegional == "RegionalCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmRegional == "RegionalName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result = (from _msRegional in this.db.MsRegionals
                       where (SqlMethods.Like(_msRegional.RegionalCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                          && (SqlMethods.Like(_msRegional.RegionalName.Trim().ToLower(), _pattern2.Trim().ToLower()))

                       select _msRegional.RegionalCode).Count();
            return _result;
        }


        public List<MsRegional> GetList(int _prmReqPage, int _prmPageSize, String _prmRegional, String _prmKeyword)
        {
            List<MsRegional> _result = new List<MsRegional>();
            
            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmRegional == "RegionalCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            if (_prmRegional == "RegionalName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";

            }
            try
            {
                var _query = (from _msRegional in this.db.MsRegionals
                              where (SqlMethods.Like(_msRegional.RegionalCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_msRegional.RegionalName.Trim().ToLower(), _pattern2.Trim().ToLower()))

                              orderby _msRegional.RegionalCode ascending
                              select new
                                {
                                    RegionalCode = _msRegional.RegionalCode,
                                    RegionalName = _msRegional.RegionalName,

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsRegional(_row.RegionalCode, _row.RegionalName));
                }
            }
            catch (Exception )
            {
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
                                orderby _msRegional.RegionalCode ascending
                                select new
                                {
                                    RegionalCode = _msRegional.RegionalCode,
                                    RegionalName = _msRegional.RegionalName,
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsRegional(_row.RegionalCode, _row.RegionalName));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public MsRegional GetSingle(String _prmCode)
        {
            MsRegional _result = null;

            try
            {
                _result = this.db.MsRegionals.Single(_temp => _temp.RegionalCode == _prmCode);
            }
            catch (Exception )
            {
            }

            return _result;
        }

        public String GetRegionalNameByCode(String _prmCode)
        {
            String _result = "";

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
                    MsRegional _msRegional = this.db.MsRegionals.Single(_temp => _temp.RegionalCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsRegionals.DeleteOnSubmit(_msRegional);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Edit(MsRegional _prmMsArea)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }
        public List<MsRegional> GetRegionalForDDL()
        {
            List<MsRegional> _result = new List<MsRegional>();

            try
            {
                var _query = (
                                from _msRegional in this.db.MsRegionals
                                //where _msRegional.RegionalCode == _prmCode
                                select new
                                {
                                    RegionalCode = _msRegional.RegionalCode,
                                    RegionalName = _msRegional.RegionalName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsRegional(_row.RegionalCode, _row.RegionalName ));
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
