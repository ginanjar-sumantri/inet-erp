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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class ReasonBL : Base
    {
        public ReasonBL()
        {

        }

        #region Reason
        public double RowsCountReason(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";


            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                (
                    from _master_Reason in this.db.HRMMsReasons
                    where (SqlMethods.Like(_master_Reason.ReasonName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    select _master_Reason.ReasonCode
                ).Count();

            _result = _query;
            return _result;

        }

        public HRMMsReason GetSingleReason(String _prmReasonCode)
        {
            HRMMsReason _result = null;

            try
            {
                _result = this.db.HRMMsReasons.Single(_temp => _temp.ReasonCode == _prmReasonCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetReasonNameByCode(String _prmReasonCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _master_Reason in this.db.HRMMsReasons
                                where _master_Reason.ReasonCode == _prmReasonCode
                                select new
                                {
                                    ReasonName = _master_Reason.ReasonName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ReasonName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsReason> GetListReason(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsReason> _result = new List<HRMMsReason>();

            string _pattern1 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _master_Reason in this.db.HRMMsReasons
                                where (SqlMethods.Like(_master_Reason.ReasonName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _master_Reason.ReasonName ascending
                                select new
                                {
                                    ReasonCode = _master_Reason.ReasonCode,
                                    ReasonName = _master_Reason.ReasonName,
                                    Description = _master_Reason.Description
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsReason(_row.ReasonCode, _row.ReasonName, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsReason> GetListReasonForDDL()
        {
            List<HRMMsReason> _result = new List<HRMMsReason>();

            try
            {
                var _query = (
                                from _master_Reason in this.db.HRMMsReasons
                                //where _master_Reason.re
                                orderby _master_Reason.ReasonName ascending
                                select new
                                {
                                    ReasonCode = _master_Reason.ReasonCode,
                                    ReasonName = _master_Reason.ReasonName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsReason(_row.ReasonCode, _row.ReasonName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditReason(HRMMsReason _prmMaster_Reason)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsReasonName(_prmMaster_Reason.ReasonName, _prmMaster_Reason.ReasonCode) == false)
                {
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

        private bool IsExistsReasonName(String _prmReasonName, String _prmReasonCode)
        {
            bool _result = false;

            try
            {
                var _query = from _master_Reason in this.db.HRMMsReasons
                             where _master_Reason.ReasonName == _prmReasonName && _master_Reason.ReasonCode != _prmReasonCode
                             select new
                             {
                                 _master_Reason.ReasonCode
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

        public bool AddReason(HRMMsReason _prmMaster_Reason)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsReasonName(_prmMaster_Reason.ReasonName, _prmMaster_Reason.ReasonCode) == false)
                {
                    this.db.HRMMsReasons.InsertOnSubmit(_prmMaster_Reason);
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

        public bool DeleteMultiReason(string[] _prmReasonCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmReasonCode.Length; i++)
                {
                    HRMMsReason _master_Reason = this.db.HRMMsReasons.Single(_temp => _temp.ReasonCode == _prmReasonCode[i]);

                    this.db.HRMMsReasons.DeleteOnSubmit(_master_Reason);
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

        ~ReasonBL()
        {

        }
    }
}
