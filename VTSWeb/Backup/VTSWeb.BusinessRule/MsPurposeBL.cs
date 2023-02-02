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
    public sealed class MsPurposeBL : Base
    {

        public MsPurposeBL()
        {
        }
        ~MsPurposeBL()
        {
        }

        #region Purpose
        public int RowsCount(String _prmPurpose, String _prmKeyword)
        {
            int _result = 0;

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmPurpose == "PurposeCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmPurpose == "PurposeName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result = (from _msPurpose in this.db.MsPurposes
                       where (SqlMethods.Like(_msPurpose.PurposeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msPurpose.PurposeName.Trim().ToLower(), _pattern2.Trim().ToLower()))

                       select _msPurpose.PurposeCode).Count();
            return _result;
        }


        public List<MsPurpose> GetList(int _prmReqPage, int _prmPageSize, String _prmPurpose, String _prmKeyword)
        {
            List<MsPurpose> _result = new List<MsPurpose>();
            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmPurpose == "PurposeCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            if (_prmPurpose == "PurposeName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";

            }
            try
            {
                var _query = (
                                from _msPurpose in this.db.MsPurposes
                                where (SqlMethods.Like(_msPurpose.PurposeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    &&(SqlMethods.Like(_msPurpose.PurposeName.Trim().ToLower(), _pattern2.Trim().ToLower()))

                                orderby _msPurpose.PurposeCode ascending
                                select new
                                {
                                    PurposeCode = _msPurpose.PurposeCode,
                                    PurposeName = _msPurpose.PurposeName,
                                    Remark      = _msPurpose.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsPurpose(_row.PurposeCode, _row.PurposeName, _row.Remark));
                }
            }
            catch (Exception )
            {
            }

            return _result;
        }

        public List<MsPurpose> GetList()
        {
            List<MsPurpose> _result = new List<MsPurpose>();

            try
            {
                var _query = (
                                from _msPurpose in this.db.MsPurposes
                                orderby _msPurpose.PurposeCode ascending
                                select new
                                {
                                    PurposeCode = _msPurpose.PurposeCode,
                                    PurposeName = _msPurpose.PurposeName,
                                    Remark = _msPurpose.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPurpose(_row.PurposeCode, _row.PurposeName, _row.Remark));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public MsPurpose GetSingle(String _prmCode)
        {
            MsPurpose _result = null;

            try
            {
                _result = this.db.MsPurposes.Single(_temp => _temp.PurposeCode == _prmCode);
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public String GetPurposeNameByCode(String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msPurpose in this.db.MsPurposes
                                where _msPurpose.PurposeCode == _prmCode
                                select new
                                {
                                    PurposeName = _msPurpose.PurposeName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.PurposeName;
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        public List<MsPurpose> GetPurposeForClearance()
        {
            List<MsPurpose> _result = new List<MsPurpose>();

            try
            {
                var _query = (
                                from _msPurpose in this.db.MsPurposes
                                //where _msRegional.RegionalCode == _prmCode
                                select new
                                {
                                    PurposeCode = _msPurpose.PurposeCode,
                                    PurposeName = _msPurpose.PurposeName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsPurpose(_row.PurposeCode, _row.PurposeName));
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
                    MsPurpose _msPurpose = this.db.MsPurposes.Single(_temp => _temp.PurposeCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsPurposes.DeleteOnSubmit(_msPurpose);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Add(MsPurpose _prmMsPurpose)
        {
            bool _result = false;

            try
            {
                this.db.MsPurposes.InsertOnSubmit(_prmMsPurpose);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Edit(MsPurpose _prmMsArea)
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
        #endregion





    }
}
