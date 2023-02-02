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
    public sealed class AccTypeBL : Base
    {
        public AccTypeBL()
        {

        }

        #region AccType

        public double RowsCountAccType(string _prmCategory, string _prmKeyword)
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
                            from _msAccType in this.db.MsAccTypes
                            where (SqlMethods.Like(_msAccType.AccTypeCode.ToString(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msAccType.AccTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msAccType.AccTypeCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool Edit(MsAccType _prmMsAccType)
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

        public bool Add(MsAccType _prmMsAccType)
        {
            bool _result = false;

            try
            {

                this.db.MsAccTypes.InsertOnSubmit(_prmMsAccType);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmAccTypeCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmAccTypeCode.Length; i++)
                {
                    MsAccType _msAccType = this.db.MsAccTypes.Single(_accType => _accType.AccTypeCode == Convert.ToChar(_prmAccTypeCode[i]));

                    this.db.MsAccTypes.DeleteOnSubmit(_msAccType);
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

        public MsAccType GetSingle(string _prmCurrType)
        {
            MsAccType _result = null;

            try
            {
                _result = this.db.MsAccTypes.Single(_accType => _accType.AccTypeCode == Convert.ToChar(_prmCurrType));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccType> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MsAccType> _result = new List<MsAccType>();

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
                                from _accType in this.db.MsAccTypes
                                where (SqlMethods.Like(_accType.AccTypeCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_accType.AccTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    AccTypeCode = _accType.AccTypeCode,
                                    AccTypeName = _accType.AccTypeName,
                                    FgType = _accType.FgType
                                }
                            );

                if (_prmOrderBy == "Account Type Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccTypeCode)) : (_query1.OrderByDescending(a => a.AccTypeCode));
                if (_prmOrderBy == "Account Type Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccTypeName)) : (_query1.OrderByDescending(a => a.AccTypeName));
                if (_prmOrderBy == "Type")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FgType)) : (_query1.OrderByDescending(a => a.FgType));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { AccTypeCode = this._char, AccTypeName = this._string, FgType = this._string });

                    _result.Add(new MsAccType(_row.AccTypeCode, _row.AccTypeName, _row.FgType));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccType> GetList()
        {
            List<MsAccType> _result = new List<MsAccType>();

            try
            {
                var _query =
                            (
                                from _accType in this.db.MsAccTypes
                                orderby _accType.AccTypeName
                                select new
                                {
                                    AccTypeCode = _accType.AccTypeCode,
                                    AccTypeName = _accType.AccTypeName,
                                    FgType = _accType.FgType
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { AccTypeCode = this._char, AccTypeName = this._string, FgType = this._string });

                    _result.Add(new MsAccType(_row.AccTypeCode, _row.AccTypeName, _row.FgType));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAccTypeNameByCode(char _prmAccTypeCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msAccType in this.db.MsAccTypes
                                where _msAccType.AccTypeCode == _prmAccTypeCode
                                select new
                                {
                                    AccTypeName = _msAccType.AccTypeName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.AccTypeName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~AccTypeBL()
        {
        }
    }
}
