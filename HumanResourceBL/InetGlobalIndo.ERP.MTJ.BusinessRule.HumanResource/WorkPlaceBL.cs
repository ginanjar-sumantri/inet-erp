using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class WorkPlaceBL : Base
    {
        public WorkPlaceBL()
        {

        }

        public MsWorkPlace GetSingle(string _prmWorkPlaceCode)
        {
            MsWorkPlace _result = null;

            try
            {
                _result = this.db.MsWorkPlaces.Single(_workPlace => _workPlace.WorkPlaceCode == _prmWorkPlaceCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetWorkPlaceNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msWorkPLace in this.db.MsWorkPlaces
                                where _msWorkPLace.WorkPlaceCode == _prmCode
                                select new
                                {
                                    WorkPlaceName = _msWorkPLace.WorkPlaceName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.WorkPlaceName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

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
                    from _msWorkPlace in this.db.MsWorkPlaces
                    where (SqlMethods.Like(_msWorkPlace.WorkPlaceCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msWorkPlace.WorkPlaceName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msWorkPlace.WorkPlaceCode

                ).Count();

            _result = _query;
            return _result;
        }

        public double RowsCount()
        {

            double _result = 0;

            var _query =
                        (
                            from _msWorkPlace in this.db.MsWorkPlaces
                            select _msWorkPlace.WorkPlaceCode
                        ).Count();

            _result = _query;
            return _result;
        }

        public List<MsWorkPlace> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsWorkPlace> _result = new List<MsWorkPlace>();
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
                                from workPlace in this.db.MsWorkPlaces
                                where (SqlMethods.Like(workPlace.WorkPlaceCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(workPlace.WorkPlaceName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby workPlace.UserDate descending
                                select new
                                {
                                    WorkPlaceCode = workPlace.WorkPlaceCode,
                                    WorkPlaceName = workPlace.WorkPlaceName,
                                    JHTPercent = workPlace.JHTPercent,
                                    UMR = workPlace.UMR
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsWorkPlace(_row.WorkPlaceCode, _row.WorkPlaceName, _row.JHTPercent, _row.UMR));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsWorkPlace> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<MsWorkPlace> _result = new List<MsWorkPlace>();

            try
            {
                var _query = (
                                from workPlace in this.db.MsWorkPlaces
                                orderby workPlace.WorkPlaceCode ascending
                                select new
                                {
                                    WorkPlaceCode = workPlace.WorkPlaceCode,
                                    WorkPlaceName = workPlace.WorkPlaceName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WorkPlaceCode = this._string, WorkPlaceName = this._string });

                    _result.Add(new MsWorkPlace(_row.WorkPlaceCode, _row.WorkPlaceName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsWorkPlace> GetList()
        {
            List<MsWorkPlace> _result = new List<MsWorkPlace>();

            try
            {
                var _query = (
                                from workPlace in this.db.MsWorkPlaces
                                orderby workPlace.UserDate descending
                                select new
                                {
                                    WorkPlaceCode = workPlace.WorkPlaceCode,
                                    WorkPlaceName = workPlace.WorkPlaceName
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { WorkPlaceCode = this._string, WorkPlaceName = this._string });

                    _result.Add(new MsWorkPlace(_row.WorkPlaceCode, _row.WorkPlaceName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsWorkPlace _prmMsWorkPlace)
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

        public bool Add(MsWorkPlace _prmMsWorkPlace)
        {
            bool _result = false;

            try
            {
                this.db.MsWorkPlaces.InsertOnSubmit(_prmMsWorkPlace);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmWorkPlaceCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmWorkPlaceCode.Length; i++)
                {
                    MsWorkPlace _msWorkPlace = this.db.MsWorkPlaces.Single(_workPlace => _workPlace.WorkPlaceCode.Trim().ToLower() == _prmWorkPlaceCode[i].Trim().ToLower());

                    this.db.MsWorkPlaces.DeleteOnSubmit(_msWorkPlace);
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

        ~WorkPlaceBL()
        {

        }
    }
}
