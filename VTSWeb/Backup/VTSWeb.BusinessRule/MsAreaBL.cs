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
    public sealed class MsAreaBL : Base
    {
        public MsAreaBL()
        {
        }
        ~MsAreaBL()
        {
        }

        #region Area
        public int RowsCount(String _prmArea, String _prmKeyword)
        {
            int _result = 0;

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmArea == "AreaCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmArea == "AreaName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result = (from _msArea in this.db.MsAreas
                       where (SqlMethods.Like(_msArea.AreaCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msArea.AreaName.Trim().ToLower(), _pattern2.Trim().ToLower()))


                       select _msArea.AreaCode).Count();
            return _result;
        }

        //public List<MsArea> GetList(int _prmReqPage, int _prmPageSize)
        //{
        //    List<MsArea> _result = new List<MsArea>();

        //    try
        //    {
        //        var _query = (
        //                        from _msArea in this.db.MsAreas
        //                        orderby _msArea.AreaCode ascending
        //                        select new
        //                        {
        //                            AreaCode = _msArea.AreaCode,
        //                            AreaName = _msArea.AreaName,
        //                            Remark = _msArea.Remark
        //                        }
        //                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsArea (_row.AreaCode, _row.AreaName, _row.Remark));
        //        }
        //    }
        //    catch (Exception )
        //    {
        //    }

        //    return _result;
        //}

        public List<MsArea> GetList(int _prmReqPage, int _prmPageSize,String _prmArea, String _prmKeyword)
        {
            List<MsArea> _result = new List<MsArea>();

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmArea == "AreaCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            if (_prmArea == "AreaName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";

            }

            try
            {
                var _query = (
                                from _msArea in this.db.MsAreas
                                where (SqlMethods.Like(_msArea.AreaCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    &&(SqlMethods.Like(_msArea.AreaName.Trim().ToLower(), _pattern2.Trim().ToLower()))

                                orderby _msArea.AreaCode ascending
                                select new
                                {
                                    AreaCode = _msArea.AreaCode,
                                    AreaName = _msArea.AreaName,
                                    Remark = _msArea.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsArea(_row.AreaCode, _row.AreaName, _row.Remark));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public MsArea GetSingle(String _prmCode)
        {
            MsArea _result = null;

            try
            {
                _result = this.db.MsAreas.Single(_temp => _temp.AreaCode == _prmCode);
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public String GetAreaNameByCode(String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msArea in this.db.MsAreas
                                where _msArea.AreaCode == _prmCode
                                select new
                                {
                                    AreaName = _msArea.AreaName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.AreaName;
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
                    MsArea _msArea = this.db.MsAreas.Single(_temp => _temp.AreaCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsAreas.DeleteOnSubmit(_msArea);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Add(MsArea _prmMsArea)
        {
            bool _result = false;

            try
            {
                this.db.MsAreas.InsertOnSubmit(_prmMsArea);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Edit(MsArea _prmMsArea)
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
        public List<MsArea> GetAreaForDDL()
        {
            List<MsArea> _result = new List<MsArea>();

            try
            {
                var _query = (
                                from _msArea in this.db.MsAreas
                                //where _msRegional.RegionalCode == _prmCode
                                select new
                                {
                                    AreaCode = _msArea.AreaCode,
                                    ArealName = _msArea.AreaName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsArea(_row.AreaCode, _row.ArealName));
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
