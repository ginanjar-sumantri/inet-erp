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
    public sealed class AccClassBL : Base
    {
        public AccClassBL()
        {

        }

        #region AccClass
        public double RowsCountAccClass(string _prmCategory, string _prmKeyword)
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
                            from _msAccClass in this.db.MsAccClasses
                            where (SqlMethods.Like(_msAccClass.AccClassCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_msAccClass.AccClassName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msAccClass.AccClassCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool EditAccClass(MsAccClass _prmMsAccClass)
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

        public bool AddAccClass(MsAccClass _prmMsAccClass)
        {
            bool _result = false;

            try
            {

                this.db.MsAccClasses.InsertOnSubmit(_prmMsAccClass);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddAccClassList(List<MsAccClass> _prmMsAccClassList)
        {
            bool _result = false;

            try
            {
                foreach (MsAccClass _row in _prmMsAccClassList)
                {
                    MsAccClass _msAccClass = new MsAccClass();

                    _msAccClass.AccClassCode = _row.AccClassCode;
                    _msAccClass.AccClassName = _row.AccClassName;
                    _msAccClass.AccSubGroup = _row.AccSubGroup;
                    _msAccClass.UserID = _row.UserID;
                    _msAccClass.UserDate = _row.UserDate;

                    this.db.MsAccClasses.InsertOnSubmit(_msAccClass);
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

        public bool DeleteMultiAccClass(string[] _prmAccClassCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmAccClassCode.Length; i++)
                {
                    MsAccClass _msAccClass = this.db.MsAccClasses.Single(_accClass => _accClass.AccClassCode == _prmAccClassCode[i]);

                    this.db.MsAccClasses.DeleteOnSubmit(_msAccClass);
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

        public MsAccClass GetSingleAccClass(string _prmAccClass)
        {
            MsAccClass _result = null;

            try
            {
                _result = this.db.MsAccClasses.Single(_accClass => _accClass.AccClassCode == _prmAccClass);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccClass> GetListAccClass(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MsAccClass> _result = new List<MsAccClass>();

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
                                from _accClass in this.db.MsAccClasses
                                where (SqlMethods.Like(_accClass.AccClassCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_accClass.AccClassName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    AccClassCode = _accClass.AccClassCode,
                                    AccClassName = _accClass.AccClassName,
                                    AccSubGroup = _accClass.AccSubGroup,
                                    AccSubGroupName =
                                                    (
                                                        from _msAccSubGroup in this.db.MsAccSubGroups
                                                        where _msAccSubGroup.AccSubGroupCode == _accClass.AccSubGroup
                                                        select _msAccSubGroup.AccSubGroupName
                                                    ).FirstOrDefault()
                                }
                            );

                if (_prmOrderBy == "Account Class Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccClassCode)) : (_query1.OrderByDescending(a => a.AccClassCode));
                if (_prmOrderBy == "Account Class Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccClassName)) : (_query1.OrderByDescending(a => a.AccClassName));
                if (_prmOrderBy == "Account Sub Group")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccSubGroup)) : (_query1.OrderByDescending(a => a.AccSubGroup));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { AccClassCode = this._string, AccClassName = this._string, AccSubGroup = this._string, AccSubGroupName = this._string });

                    _result.Add(new MsAccClass(_row.AccClassCode, _row.AccClassName, _row.AccSubGroup, _row.AccSubGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccClass> GetListAccClass()
        {
            List<MsAccClass> _result = new List<MsAccClass>();

            try
            {
                var _query =
                            (
                                from _accClass in this.db.MsAccClasses
                                orderby _accClass.AccClassCode ascending
                                select new
                                {
                                    AccClassCode = _accClass.AccClassCode,
                                    AccClassName = _accClass.AccClassName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsAccClass(_row.AccClassCode, _row.AccClassName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAccClassNameByCode(string _prmAccClassCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msAccClass in this.db.MsAccClasses
                                where _msAccClass.AccClassCode == _prmAccClassCode
                                select new
                                {
                                    AccClassName = _msAccClass.AccClassName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.AccClassName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsAccountClassExists(String _prmAccountClass)
        {
            bool _result = false;

            try
            {
                var _query = from _msAccountClass in this.db.MsAccClasses
                             where _msAccountClass.AccClassCode == _prmAccountClass
                             select new
                             {
                                 _msAccountClass.AccClassCode
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

        ~AccClassBL()
        {

        }

    }
}
