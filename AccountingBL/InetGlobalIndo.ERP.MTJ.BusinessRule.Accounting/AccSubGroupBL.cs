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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class AccSubGroupBL : Base
    {
        public AccSubGroupBL()
        {

        }

        #region AccSubGroup

        public double RowsCountAccSubGroup(string _prmCategory, string _prmKeyword)
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
                            from _msAccSubGroup in this.db.MsAccSubGroups
                            where (SqlMethods.Like(_msAccSubGroup.AccSubGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_msAccSubGroup.AccSubGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msAccSubGroup.AccSubGroupCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool Edit(MsAccSubGroup _prmMsAccSubGroups)
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

        public bool Add(MsAccSubGroup _prmMsAccSubGroups)
        {
            bool _result = false;

            try
            {

                this.db.MsAccSubGroups.InsertOnSubmit(_prmMsAccSubGroups);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddAccSubGroupList(List<MsAccSubGroup> _prmMsAccSubGroupList)
        {
            bool _result = false;

            try
            {
                foreach (MsAccSubGroup _row in _prmMsAccSubGroupList)
                {
                    MsAccSubGroup _msAccSubGroup = new MsAccSubGroup();

                    _msAccSubGroup.AccSubGroupCode = _row.AccSubGroupCode;
                    _msAccSubGroup.AccSubGroupName = _row.AccSubGroupName;
                    _msAccSubGroup.AccGroup = _row.AccGroup;
                    _msAccSubGroup.UserID = _row.UserID;
                    _msAccSubGroup.UserDate = _row.UserDate;

                    this.db.MsAccSubGroups.InsertOnSubmit(_msAccSubGroup);
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

        public bool DeleteMulti(string[] _prmAccSubGroupCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmAccSubGroupCode.Length; i++)
                {
                    MsAccSubGroup _msAccSubGroup = this.db.MsAccSubGroups.Single(_accSubGroup => _accSubGroup.AccSubGroupCode == _prmAccSubGroupCode[i]);

                    this.db.MsAccSubGroups.DeleteOnSubmit(_msAccSubGroup);
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

        public MsAccSubGroup GetSingle(string _prmAccSubGroup)
        {
            MsAccSubGroup _result = null;

            try
            {
                _result = this.db.MsAccSubGroups.Single(_accSubGroup => _accSubGroup.AccSubGroupCode == _prmAccSubGroup);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MsAccSubGroup GetViewAccSubGroup(string _prmAccSubGroup)
        {
            MsAccSubGroup _result = new MsAccSubGroup();

            try
            {
                var _query = (
                                from _msAccSubGroup in this.db.MsAccSubGroups
                                where _msAccSubGroup.AccSubGroupCode == _prmAccSubGroup
                                select new
                                {
                                    AccSubGroupCode = _msAccSubGroup.AccSubGroupCode,
                                    AccSubGroupName = _msAccSubGroup.AccSubGroupName,
                                    AccGroupName = (
                                                    from _msAccGroup in this.db.MsAccGroups
                                                    where _msAccGroup.AccGroupCode == _msAccSubGroup.AccGroup
                                                    select _msAccGroup.AccGroupName
                                                  ).FirstOrDefault()
                                }
                            );
                foreach (var _row in _query)
                {
                    _result.AccSubGroupCode = _row.AccSubGroupCode;
                    _result.AccSubGroupName = _row.AccSubGroupName;
                    _result.AccGroupName = _row.AccGroupName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccSubGroup> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MsAccSubGroup> _result = new List<MsAccSubGroup>();

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
                var _query1 =
                            (
                                from _accSubGroup in this.db.MsAccSubGroups
                                where (SqlMethods.Like(_accSubGroup.AccSubGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_accSubGroup.AccSubGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    AccSubGroupCode = _accSubGroup.AccSubGroupCode,
                                    AccSubGroupName = _accSubGroup.AccSubGroupName,
                                    AccGroup = _accSubGroup.AccGroup,
                                    AccGroupName =
                                                    (
                                                        from _msAccGroup in this.db.MsAccGroups
                                                        where _msAccGroup.AccGroupCode == _accSubGroup.AccGroup
                                                        select _msAccGroup.AccGroupName
                                                    ).FirstOrDefault()
                                }
                            );

                if (_prmOrderBy == "Account Sub Group Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccSubGroupCode)) : (_query1.OrderByDescending(a => a.AccSubGroupCode));
                if (_prmOrderBy == "Account Sub Group Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccSubGroupName)) : (_query1.OrderByDescending(a => a.AccSubGroupName));
                if (_prmOrderBy == "Account Group")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccGroup)) : (_query1.OrderByDescending(a => a.AccGroup));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { AccSubGroupCode = this._string, AccSubGroupName = this._string, AccGroup = this._string, AccGroupName = this._string });

                    _result.Add(new MsAccSubGroup(_row.AccSubGroupCode, _row.AccSubGroupName, _row.AccGroup, _row.AccGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;

        }

        public List<MsAccSubGroup> GetList()
        {
            List<MsAccSubGroup> _result = new List<MsAccSubGroup>();

            try
            {
                var _query =
                            (
                                from _accSubGroup in this.db.MsAccSubGroups
                                select new
                                {
                                    AccSubGroupCode = _accSubGroup.AccSubGroupCode,
                                    AccSubGroupName = _accSubGroup.AccSubGroupName,
                                    AccGroup = _accSubGroup.AccGroup,
                                    AccGroupName =
                                                    (
                                                        from _msAccGroup in this.db.MsAccGroups
                                                        where _msAccGroup.AccGroupCode == _accSubGroup.AccGroup
                                                        select _msAccGroup.AccGroupName
                                                    ).FirstOrDefault()
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsAccSubGroup(_row.AccSubGroupCode, _row.AccSubGroupName, _row.AccGroup, _row.AccGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;

        }

        public List<MsAccSubGroup> GetListForDDL()
        {
            List<MsAccSubGroup> _result = new List<MsAccSubGroup>();

            try
            {
                var _query =
                            (
                                from _accSubGroup in this.db.MsAccSubGroups
                                orderby _accSubGroup.AccSubGroupName
                                select new
                                {
                                    AccSubGroupCode = _accSubGroup.AccSubGroupCode,
                                    AccSubGroupName = _accSubGroup.AccSubGroupName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { AccSubGroupCode = this._string, AccSubGroupName = this._string });

                    _result.Add(new MsAccSubGroup(_row.AccSubGroupCode, _row.AccSubGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;

        }

        public string GetAccSubGroupNameByCode(string _prmAccSubGroupCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msAccSubGroup in this.db.MsAccSubGroups
                                where _msAccSubGroup.AccSubGroupCode == _prmAccSubGroupCode
                                select new
                                {
                                    AccSubGroupName = _msAccSubGroup.AccSubGroupName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.AccSubGroupName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsAccSubGroupExists(String _prmAccSubGroup)
        {
            bool _result = false;

            try
            {
                var _query = from _msAccSubGroup in this.db.MsAccSubGroups
                             where _msAccSubGroup.AccSubGroupCode == _prmAccSubGroup
                             select new
                             {
                                 _msAccSubGroup.AccSubGroupCode
                             };

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

        ~AccSubGroupBL()
        {

        }

    }
}
