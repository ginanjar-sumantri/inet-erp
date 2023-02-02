using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;

namespace BusinessRule.POS
{
    public sealed class POSConfigurationBL : Base
    {
        public POSConfigurationBL()
        {
        }

        #region Member

        public CompanyConfiguration GetSingle(string _prmCode)
        {
            CompanyConfiguration _result = null;

            try
            {
                _result = this.db.CompanyConfigurations.Single(_temp => _temp.ConfigCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(CompanyConfiguration _prmCompanyConfiguration)
        {
            bool _result = false;

            try
            {
                this.db.CompanyConfigurations.InsertOnSubmit(_prmCompanyConfiguration);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public double RowsCount(string _prmCategory, string _prmKeyword)
        //{
        //    double _result = 0;

        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "Code")
        //    {
        //        _pattern1 = "%" + _prmKeyword + "%";
        //        _pattern2 = "%%";

        //    }
        //    else if (_prmCategory == "Name")
        //    {
        //        _pattern2 = "%" + _prmKeyword + "%";
        //        _pattern1 = "%%";
        //    }

        //    var _query =
        //                (
        //                     from _msMemberType in this.db.MsMemberTypes
        //                     where (SqlMethods.Like(_msMemberType.MemberTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                        && (SqlMethods.Like(_msMemberType.MemberTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
        //                     select _msMemberType
        //                ).Count();

        //    _result = _query;

        //    return _result;
        //}
        
        //public List<MsMemberType> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        //{
        //    List<MsMemberType> _result = new List<MsMemberType>();

        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "Code")
        //    {
        //        _pattern1 = "%" + _prmKeyword + "%";
        //        _pattern2 = "%%";

        //    }
        //    else if (_prmCategory == "Name")
        //    {
        //        _pattern2 = "%" + _prmKeyword + "%";
        //        _pattern1 = "%%";
        //    }

        //    try
        //    {
        //        var _query = (
        //                        from _msMemberType in this.db.MsMemberTypes
        //                        where (SqlMethods.Like(_msMemberType.MemberTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                           && (SqlMethods.Like(_msMemberType.MemberTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
        //                        orderby _msMemberType.MemberTypeName descending
        //                        select new
        //                        {
        //                            MemberTypeCode = _msMemberType.MemberTypeCode,
        //                            MemberTypeName = _msMemberType.MemberTypeName
        //                        }
        //                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsMemberType(_row.MemberTypeCode, _row.MemberTypeName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsMemberType> GetList()
        //{
        //    List<MsMemberType> _result = new List<MsMemberType>();

        //    try
        //    {
        //        var _query = (
        //                        from _msMemberType in this.db.MsMemberTypes
        //                        orderby _msMemberType.MemberTypeName ascending
        //                        select new
        //                        {
        //                            MemberTypeCode = _msMemberType.MemberTypeCode,
        //                            MemberTypeName = _msMemberType.MemberTypeName
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsMemberType(_row.MemberTypeCode, _row.MemberTypeName));

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool DeleteMulti(string[] _prmCode)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        for (int i = 0; i < _prmCode.Length; i++)
        //        {
        //            MsMemberType _msMemberType = this.db.MsMemberTypes.Single(_temp => _temp.MemberTypeCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

        //            this.db.MsMemberTypes.DeleteOnSubmit(_msMemberType);
        //        }

        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}       

        //public string GetMemberNameByCode(string _prmCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        var _query = (
        //                        from _msMemberType in this.db.MsMemberTypes
        //                        where _msMemberType.MemberTypeCode == _prmCode
        //                        select new
        //                        {
        //                            MemberTypeName = _msMemberType.MemberTypeName
        //                        }
        //                      );

        //        foreach (var _obj in _query)
        //        {
        //            _result = _obj.MemberTypeName;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}        

        public bool Edit(CompanyConfiguration _prmCompanyConfiguration)
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

        #endregion

        ~POSConfigurationBL()
        {
        }
    }
}
