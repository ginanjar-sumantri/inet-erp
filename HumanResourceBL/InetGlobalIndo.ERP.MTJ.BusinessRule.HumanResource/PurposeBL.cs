using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class PurposeBL : Base
    {
        public PurposeBL()
        {

        }

        #region Purpose
        public double RowsCountPurpose(string _prmCategory, string _prmKeyword)
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

            try
            {
                var _query =
                            (
                                from _msPurpose in this.db.HRMMsPurposes
                                where (SqlMethods.Like(_msPurpose.PurposeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPurpose.PurposeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _msPurpose.PurposeCode
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public HRMMsPurpose GetSinglePurpose(String _prmPurposeCode)
        {
            HRMMsPurpose _result = null;

            try
            {
                _result = this.db.HRMMsPurposes.Single(_temp => _temp.PurposeCode == _prmPurposeCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetPurposeNameByCode(String _prmPurposeGrpCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _hrmMsPurpose in this.db.HRMMsPurposes
                                where _hrmMsPurpose.PurposeCode == _prmPurposeGrpCode
                                select new
                                {
                                    PurposeName = _hrmMsPurpose.PurposeName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.PurposeName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsPurpose> GetListPurpose(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsPurpose> _result = new List<HRMMsPurpose>();

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
                                from _hrmMsPurpose in this.db.HRMMsPurposes
                                where (SqlMethods.Like(_hrmMsPurpose.PurposeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_hrmMsPurpose.PurposeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _hrmMsPurpose.PurposeCode ascending
                                select new
                                {
                                    PurposeCode = _hrmMsPurpose.PurposeCode,
                                    PurposeName = _hrmMsPurpose.PurposeName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsPurpose(_row.PurposeCode, _row.PurposeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsPurpose> GetListPurposeForDDL()
        {
            List<HRMMsPurpose> _result = new List<HRMMsPurpose>();

            try
            {
                var _query = (
                                from _hrmMsPurpose in this.db.HRMMsPurposes
                                orderby _hrmMsPurpose.PurposeCode ascending
                                select new
                                {
                                    PurposeCode = _hrmMsPurpose.PurposeCode,
                                    PurposeName = _hrmMsPurpose.PurposeName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsPurpose(_row.PurposeCode, _row.PurposeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPurpose(HRMMsPurpose _prmHRMMsPurpose)
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

        public bool AddPurpose(HRMMsPurpose _prmHRMMsPurpose)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsPurposeName(_prmHRMMsPurpose.PurposeName, _prmHRMMsPurpose.PurposeCode) == false)
                {
                    this.db.HRMMsPurposes.InsertOnSubmit(_prmHRMMsPurpose);
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        private bool IsExistsPurposeName(String _prmPurposeName, String _prmPurposeCode)
        {
            bool _result = false;

            try
            {
                var _query = from _hrmMsPurpose in this.db.HRMMsPurposes
                             where _hrmMsPurpose.PurposeName == _prmPurposeName && _hrmMsPurpose.PurposeCode != _prmPurposeCode
                             select new
                             {
                                 _hrmMsPurpose.PurposeCode
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

        public bool DeleteMultiPurpose(string[] _prmPurposeCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmPurposeCode.Length; i++)
                {
                    HRMMsPurpose _HRMMsPurpose = this.db.HRMMsPurposes.Single(_temp => _temp.PurposeCode == _prmPurposeCode[i]);

                    this.db.HRMMsPurposes.DeleteOnSubmit(_HRMMsPurpose);
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
        #endregion

        ~PurposeBL()
        {

        }
    }
}
