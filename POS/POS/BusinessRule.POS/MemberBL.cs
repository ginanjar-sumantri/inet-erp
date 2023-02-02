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
    public sealed class MemberBL : Base
    {
        public MemberBL()
        {
        }

        #region Member

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
                             from _msMember in this.db.MsMembers
                             where (SqlMethods.Like(_msMember.MemberCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_msMember.MemberName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _msMember
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MsMember> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsMember> _result = new List<MsMember>();

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
                                from _msMember in this.db.MsMembers
                                where (SqlMethods.Like(_msMember.MemberCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msMember.MemberName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msMember.MemberName descending
                                select new
                                {
                                    MemberCode = _msMember.MemberCode,
                                    MemberTypeCode = _msMember.MemberTypeCode,
                                    MemberTypeName = (
                                                        from _msMemberType in this.db.MsMemberTypes
                                                        where _msMemberType.MemberTypeCode == _msMember.MemberTypeCode
                                                        select _msMemberType.MemberTypeName
                                                    ).FirstOrDefault(),

                                    MemberName = _msMember.MemberName,
                                    Gender = _msMember.Gender,
                                    HandPhone1 = _msMember.HandPhone1,
                                    Email = _msMember.Email,
                                    Status = _msMember.Status
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsMember(_row.MemberCode, _row.MemberTypeCode, _row.MemberTypeName, _row.MemberName, _row.Gender, _row.HandPhone1, _row.Email, (Byte)_row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsMember> GetList()
        {
            List<MsMember> _result = new List<MsMember>();

            try
            {
                var _query = (
                                from _msMember in this.db.MsMembers
                                orderby _msMember.MemberName ascending
                                select new
                                {
                                    MemberCode = _msMember.MemberCode,
                                    MemberTypeCode = _msMember.MemberTypeCode,
                                    MemberTypeName = (
                                                        from _msMemberType in this.db.MsMemberTypes
                                                        where _msMemberType.MemberTypeCode == _msMember.MemberTypeCode
                                                        select _msMemberType.MemberTypeName
                                                    ).FirstOrDefault(),
                                    MemberTitle = _msMember.MemberTitle,
                                    MemberName = _msMember.MemberName,
                                    Gender = _msMember.Gender,
                                    Company = _msMember.Company,
                                    JobTtlCode = _msMember.JobTtlCode,
                                    JobLvlCode = _msMember.JobLvlCode,
                                    Salary = _msMember.Salary,
                                    EducationCode = _msMember.EducationCode,
                                    Address = _msMember.Address,
                                    CityCode = _msMember.CityCode,
                                    ZipCode = _msMember.ZipCode,
                                    Telephone1 = _msMember.Telephone1,
                                    HandPhone1 = _msMember.HandPhone1,
                                    Email = _msMember.Email,
                                    Status = _msMember.Status
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsMember(_row.MemberCode, _row.MemberTypeCode, _row.MemberTypeName, (Byte)_row.MemberTitle, _row.MemberName, _row.Gender, _row.Company, _row.JobTtlCode, _row.JobLvlCode, (Byte)_row.Salary, (Guid)_row.EducationCode, _row.Address, _row.CityCode, _row.ZipCode, _row.Telephone1, _row.HandPhone1, _row.Email, (Byte)_row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsMember> GetListForMonitoring(String _prmSearchBy, String _prmSearchText)
        {
            String _pattern1 = "%";
            String _pattern2 = "%";
            String _pattern3 = "%";
            String _pattern4 = "%";
            String _pattern5 = "%";

            if (_prmSearchBy.Trim() == "MemberName")
            {
                _pattern1 = "%" + _prmSearchText + "%";
            }
            else if (_prmSearchBy.Trim() == "Telephone")
            {
                _pattern2 = "%" + _prmSearchText + "%";
            }
            else if (_prmSearchBy.Trim() == "HandPhone")
            {
                _pattern3 = "%" + _prmSearchText + "%";
            }
            else if (_prmSearchBy.Trim() == "Barcode")
            {
                _pattern4 = "%" + _prmSearchText + "%";
            }
            else if (_prmSearchBy.Trim() == "Address")
            {
                _pattern5 = "%" + _prmSearchText + "%";
            }

            List<MsMember> _result = new List<MsMember>();

            try
            {
                var _query = (
                                from _msMember in this.db.MsMembers
                                orderby _msMember.MemberName ascending
                                where SqlMethods.Like((_msMember.MemberName ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_msMember.Telephone1 ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && SqlMethods.Like((_msMember.HandPhone1 ?? "").Trim().ToLower(), _pattern3.Trim().ToLower())
                                    && SqlMethods.Like((_msMember.Barcode ?? "").Trim().ToLower(), _pattern4.Trim().ToLower())
                                    && SqlMethods.Like((_msMember.Address ?? "").Trim().ToLower(), _pattern5.Trim().ToLower())
                                select _msMember
                            );

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsMember _msMember = this.db.MsMembers.Single(_temp => _temp.MemberCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsMembers.DeleteOnSubmit(_msMember);
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

        public MsMember GetSingle(string _prmCode)
        {
            MsMember _result = null;

            try
            {
                _result = this.db.MsMembers.Single(_temp => _temp.MemberCode.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MsMember GetSingleByBarcode(string _prmBarcode)
        {
            MsMember _result = null;

            try
            {
                //_result = this.db.MsMembers.Single(_temp => _temp.Barcode.Trim().ToLower() == _prmBarcode.Trim().ToLower());
                var _query = (
                                from _member in this.db.MsMembers
                                where _member.Barcode.Trim().ToLower() == _prmBarcode.Trim().ToLower()
                                && _member.ExpiredDate >= DateTime.Now
                                && _member.FgActive == 'Y'
                                select _member
                             );

                if (_query.Count() > 0)
                {
                    _result = _query.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetMemberNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msMember in this.db.MsMembers
                                where _msMember.MemberCode == _prmCode
                                select new
                                {
                                    MemberName = _msMember.MemberName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.MemberName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(MsMember _prmMsMember)
        {
            bool _result = false;

            try
            {
                this.db.MsMembers.InsertOnSubmit(_prmMsMember);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsMember _prmMsMember)
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

        public string GetSetValueByCode(string _prmCode)
        {
            string _result = "";
            try
            {
                var _query = (from _msCompanyConfig in this.db.CompanyConfigurations
                              where _msCompanyConfig.ConfigCode.Trim().ToLower() == _prmCode.Trim().ToLower()
                              select new
                              {
                                  SetValue = _msCompanyConfig.SetValue
                              }
                    );
                foreach (var _obj in _query)
                {
                    _result = _obj.SetValue;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public int GetMaxMemberCode()
        {
            int _result = 0;
            try
            {
                var _query = (
                                from _msMember in this.db.MsMembers
                                select _msMember.MemberCode
                              ).Count();
                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        #endregion

        ~MemberBL()
        {
        }
    }
}
