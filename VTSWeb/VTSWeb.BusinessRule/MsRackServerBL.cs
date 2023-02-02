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
    public sealed class MsRackServerBL : Base
    {

        public MsRackServerBL()
        {
        }
        ~MsRackServerBL()
        {
        }

        #region MsRackServer
        public int RowsCount(String _prmRackServer, String _prmKeyword)
        {
            int _result = 0;

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmRackServer == "RackServerCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmRackServer == "RackServerName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result = (from _msRackServer in this.db.MsRackServers
                       where (SqlMethods.Like(_msRackServer.RackCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msRackServer.RackName.Trim().ToLower(), _pattern2.Trim().ToLower()))

                       select _msRackServer.RackCode).Count();
            return _result;
        }


        public List<MsRackServer> GetList(int _prmReqPage, int _prmPageSize, String _prmRackServer, String _prmKeyword)
        {
            List<MsRackServer> _result = new List<MsRackServer>();
            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmRackServer == "RackCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            if (_prmRackServer == "RackName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";

            }
            try
            {
                var _query = (
                                from _msRackServer in this.db.MsRackServers
                                where (SqlMethods.Like(_msRackServer.RackCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msRackServer.RackName.Trim().ToLower(), _pattern2.Trim().ToLower()))

                                orderby _msRackServer.RackCode ascending
                                select new
                                {
                                    RackCode = _msRackServer.RackCode,
                                    RackName = _msRackServer.RackName,
                                    Remark = _msRackServer.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsRackServer(_row.RackCode, _row.RackName, _row.Remark));
                }
            }
            catch (Exception )
            {
            }

            return _result;
        }

        public List<MsRackServer> GetList()
        {
            List<MsRackServer> _result = new List<MsRackServer>();

            try
            {
                var _query = (
                                from _msRackServer in this.db.MsRackServers
                                orderby _msRackServer.RackCode ascending
                                select new
                                {
                                    RackCode = _msRackServer.RackCode,
                                    RackName = _msRackServer.RackName,
                                    Remark = _msRackServer.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsRackServer(_row.RackCode, _row.RackName, _row.Remark));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public MsRackServer GetSingle(String _prmCode)
        {
            MsRackServer _result = null;

            try
            {
                _result = this.db.MsRackServers.Single(_temp => _temp.RackCode == _prmCode);
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public String GetRackNameByCode(String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msRackServer in this.db.MsRackServers
                                where _msRackServer.RackCode == _prmCode
                                select new
                                {
                                    RackServerName = _msRackServer.RackName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.RackServerName;
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        public List<MsRackServer> GetPurposeForClearance()
        {
            List<MsRackServer> _result = new List<MsRackServer>();

            try
            {
                var _query = (
                                from _msRackServer in this.db.MsRackServers
                                //where _msRegional.RegionalCode == _prmCode
                                select new
                                {
                                    RackServerCode = _msRackServer.RackCode,
                                    RackServerName = _msRackServer.RackName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsRackServer(_row.RackServerCode, _row.RackServerName));
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
                    MsRackServer _msRackServer = this.db.MsRackServers.Single(_temp => _temp.RackCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsRackServers.DeleteOnSubmit(_msRackServer);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Add(MsRackServer _prmMsRackServer)
        {
            bool _result = false;

            try
            {
                this.db.MsRackServers.InsertOnSubmit(_prmMsRackServer);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Edit(MsRackServer _prmMsRackServer)
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

        public List<MsRackServer> GetListForDDL()
        {
            List<MsRackServer> _result = new List<MsRackServer>();
            try
            {
                var _query = (
                                from _msRackServer in this.db.MsRackServers
                                orderby _msRackServer.RackCode ascending
                                select new
                                {
                                    RackCode = _msRackServer.RackCode,
                                    RackName = _msRackServer.RackName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsRackServer(_row.RackCode, _row.RackName));
                }
            }
            catch (Exception)
            {
            }
            return _result;
        }
        #endregion

        #region MsRackServerDt
        public bool Add(MsRackBox _prmMsRackBox)
        {
            bool _result = false;

            try
            {
                this.db.MsRackBoxes.InsertOnSubmit(_prmMsRackBox);
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
