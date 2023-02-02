using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.IO;
using System.Drawing;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class TermTypeBL : Base
    {
        public TermTypeBL()
        {

        }

        #region TermType
        public double RowsCountTermType(string _prmCategory, string _prmKeyword)
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
                    from _msTermType in this.db.HRMMsTermTypes
                    where (SqlMethods.Like(_msTermType.TermType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msTermType.TermDescription.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msTermType.TermType
                ).Count();

            _result = _query;
            return _result;
        }

        public HRMMsTermType GetSingleTermType(string _prmTermTypeCode)
        {
            HRMMsTermType _result = null;

            try
            {
                _result = this.db.HRMMsTermTypes.Single(_temp => _temp.TermType == _prmTermTypeCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetTermTypeNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msTermType in this.db.HRMMsTermTypes
                                where _msTermType.TermType == _prmCode
                                select new
                                {
                                    TermDescription = _msTermType.TermDescription
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.TermDescription;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsTermType> GetListTermType(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsTermType> _result = new List<HRMMsTermType>();
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
                                from _msTermType in this.db.HRMMsTermTypes
                                where (SqlMethods.Like(_msTermType.TermType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msTermType.TermDescription.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msTermType.TermType ascending
                                select new
                                {
                                    TermType = _msTermType.TermType,
                                    TermDescription = _msTermType.TermDescription,
                                    Remark = _msTermType.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsTermType(_row.TermType, _row.TermDescription, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsTermType> GetListTermTypeForDDL()
        {
            List<HRMMsTermType> _result = new List<HRMMsTermType>();

            try
            {
                var _query = (
                                from _msTermType in this.db.HRMMsTermTypes
                                orderby _msTermType.TermType ascending
                                select new
                                {
                                    TermType = _msTermType.TermType,
                                    TermDescription = _msTermType.TermDescription
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsTermType(_row.TermType, _row.TermDescription));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiTermType(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMMsTermType _msTermType = this.db.HRMMsTermTypes.Single(_temp => _temp.TermType.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.HRMMsTermTypes.DeleteOnSubmit(_msTermType);
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

        public bool AddTermType(HRMMsTermType _prmMsTermType)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsTermTypes.InsertOnSubmit(_prmMsTermType);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditTermType(HRMMsTermType _prmMsTermType)
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

        ~TermTypeBL()
        {

        }
    }
}
