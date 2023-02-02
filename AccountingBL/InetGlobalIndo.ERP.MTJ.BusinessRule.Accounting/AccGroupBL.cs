using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class AccGroupBL : Base
    {

        public AccGroupBL()
        {

        }

        #region AccGroup
        public double RowsCountAccGroup(string _prmCategory, string _prmKeyword)
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
                            from _msAccGroup in this.db.MsAccGroups
                            where (SqlMethods.Like(_msAccGroup.AccGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_msAccGroup.AccGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msAccGroup.AccGroupCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool EditAccGroup(MsAccGroup _prmMsAccGroup)
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

        public bool AddAccGroup(MsAccGroup _prmMsAccGroup)
        {
            bool _result = false;

            try
            {

                this.db.MsAccGroups.InsertOnSubmit(_prmMsAccGroup);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiAccGroup(string[] _prmAccGrpCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmAccGrpCode.Length; i++)
                {
                    MsAccGroup _msAccgroup = this.db.MsAccGroups.Single(_accGrp => _accGrp.AccGroupCode.Trim().ToLower() == _prmAccGrpCode[i].Trim().ToLower());

                    this.db.MsAccGroups.DeleteOnSubmit(_msAccgroup);
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

        public MsAccGroup GetSingleAccGroup(string _prmCurrCode)
        {
            MsAccGroup _result = null;

            try
            {
                _result = this.db.MsAccGroups.Single(_accGrp => _accGrp.AccGroupCode == _prmCurrCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccGroup> GetListAccGroup(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MsAccGroup> _result = new List<MsAccGroup>();

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
                var _query1 = (
                                from _accGroup in this.db.MsAccGroups
                                where (SqlMethods.Like(_accGroup.AccGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_accGroup.AccGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    AccGroupCode = _accGroup.AccGroupCode,
                                    AccGroupName = _accGroup.AccGroupName,
                                    AccType = _accGroup.AccType,
                                    AccTypeName =
                                                (
                                                    from _msAccType in this.db.MsAccTypes
                                                    where _msAccType.AccTypeCode == _accGroup.AccType
                                                    select _msAccType.AccTypeName
                                                ).FirstOrDefault()
                                }
                            );

                if (_prmOrderBy == "Account Group Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccGroupCode)) : (_query1.OrderByDescending(a => a.AccGroupCode));
                if (_prmOrderBy == "Account Group Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccGroupName)) : (_query1.OrderByDescending(a => a.AccGroupName));
                if (_prmOrderBy == "Account Type")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccType)) : (_query1.OrderByDescending(a => a.AccType));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);


                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { AccGroupCode = this._string, AccGroupName = this._string, AccType = this._char, AccTypeName = this._string });

                    _result.Add(new MsAccGroup(_row.AccGroupCode, _row.AccGroupName, _row.AccType, _row.AccTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccGroup> GetListAccGroupForDDL()
        {
            List<MsAccGroup> _result = new List<MsAccGroup>();

            try
            {
                var _query = (
                                from AccGroup in this.db.MsAccGroups
                                orderby AccGroup.AccGroupName
                                select new
                                {
                                    AccGroupCode = AccGroup.AccGroupCode,
                                    AccGroupName = AccGroup.AccGroupName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { AccGroupCode = this._string, AccGroupName = this._string });

                    _result.Add(new MsAccGroup(_row.AccGroupCode, _row.AccGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAccGroupNameByCode(string _prmAccGroupCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msAccType in this.db.MsAccGroups
                                where _msAccType.AccGroupCode == _prmAccGroupCode
                                select new
                                {
                                    AccGroupName = _msAccType.AccGroupName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.AccGroupName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsAccGroupExists(String _prmAccGroup)
        {
            bool _result = false;

            try
            {
                var _query = from _msAccGroup in this.db.MsAccGroups
                             where _msAccGroup.AccGroupCode == _prmAccGroup
                             select new
                             {
                                 _msAccGroup.AccGroupCode
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

        ~AccGroupBL()
        {
        }
    }
}
