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
    public sealed class MsCustTypeBL : Base
    {

        public MsCustTypeBL()
        {
        }
        ~MsCustTypeBL()
        {
        }

        #region CompanyType
        public int RowsCount(String _prmCustType, String _prmKeyword)
        {
            int _result = 0;

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmCustType == "CustTypeCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCustType == "CustTypeName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result = (from _msCustType in this.db.MsCustTypes
                       where (SqlMethods.Like(_msCustType.CustTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                          && (SqlMethods.Like(_msCustType.CustTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))

                       select _msCustType.CustTypeCode).Count();
            return _result;
        }

        public List<MsCustType> GetList(int _prmReqPage, int _prmPageSize, String _prmCustType, String _Keyword)
        {
            List<MsCustType> _result = new List<MsCustType>();

            String _pattern1 = "%%";
            String _pattern2 = "%%";

            if (_prmCustType == "CustTypeCode")
            {
                _pattern1 = "%" + _Keyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCustType == "CustTypeName")
            {
                _pattern2 = "%" + _Keyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _msCustType in this.db.MsCustTypes
                                where (SqlMethods.Like(_msCustType.CustTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCustType.CustTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msCustType.CustTypeCode ascending
                                select new
                                {
                                    CustTypeCode = _msCustType.CustTypeCode,
                                    CustTypeName = _msCustType.CustTypeName,

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustType(_row.CustTypeCode, _row.CustTypeName));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public List<MsCustType> GetList()
        {
            List<MsCustType> _result = new List<MsCustType>();

            try
            {
                var _query = (
                                from _msCustType in this.db.MsCustTypes
                                orderby _msCustType.CustTypeCode ascending
                                select new
                                {
                                    CustTypeCode = _msCustType.CustTypeCode,
                                    CustTypeName = _msCustType.CustTypeName,
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustType(_row.CustTypeCode, _row.CustTypeName));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public MsCustType GetSingle(String _prmCode)
        {
            MsCustType _result = null;

            try
            {
                _result = this.db.MsCustTypes.Single(_temp => _temp.CustTypeCode == _prmCode);
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public String GetCustTypeNameByCode(String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msCustType in this.db.MsCustTypes
                                where _msCustType.CustTypeCode == _prmCode
                                select new
                                {
                                    CustTypeName = _msCustType.CustTypeName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CustTypeName;
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
                    MsCustType _msCompanyType = this.db.MsCustTypes.Single(_temp => _temp.CustTypeCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsCustTypes.DeleteOnSubmit(_msCompanyType);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Add(MsCustType _prmMsCustType)
        {
            bool _result = false;

            try
            {
                this.db.MsCustTypes.InsertOnSubmit(_prmMsCustType);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Edit(MsCustType _prmMsArea)
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
        public List<MsCustType> GetCustTypeForDDL()
        {
            List<MsCustType> _result = new List<MsCustType>();

            try
            {
                var _query = (
                                from _msCustType in this.db.MsCustTypes
                                //where _msRegional.RegionalCode == _prmCode
                                select new
                                {
                                    CustTypeCode = _msCustType.CustTypeCode,
                                    CustTypeName = _msCustType.CustTypeName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustType(_row.CustTypeCode, _row.CustTypeName));
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
