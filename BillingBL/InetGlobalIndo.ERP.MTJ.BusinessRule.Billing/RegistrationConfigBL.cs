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
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.IO;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class RegistrationConfigBL : Base
    {
        public RegistrationConfigBL()
        {

        }

        #region RegistrationConfig
        public double RowsCountRegistrationConfig(string _prmCategory, string _prmKeyword)
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
                    from _msRegistrationConfig in this.db.BILMsRegistrationConfigs
                    where (SqlMethods.Like(_msRegistrationConfig.RegCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msRegistrationConfig.RegName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msRegistrationConfig.RegCode
                ).Count();

            _result = _query;
            return _result;
        }

        public BILMsRegistrationConfig GetSingleRegistrationConfig(string _prmRegCode)
        {
            BILMsRegistrationConfig _result = null;

            try
            {
                _result = this.db.BILMsRegistrationConfigs.Single(_temp => _temp.RegCode == _prmRegCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetRegNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msRegistrationConfig in this.db.BILMsRegistrationConfigs
                                where _msRegistrationConfig.RegCode == _prmCode
                                select new
                                {
                                    RegName = _msRegistrationConfig.RegName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.RegName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<BILMsRegistrationConfig> GetListRegistrationConfig(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<BILMsRegistrationConfig> _result = new List<BILMsRegistrationConfig>();

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
                                from _msRegistrationConfig in this.db.BILMsRegistrationConfigs
                                where (SqlMethods.Like(_msRegistrationConfig.RegCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msRegistrationConfig.RegName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msRegistrationConfig.EditDate descending
                                select new
                                {
                                    RegCode = _msRegistrationConfig.RegCode,
                                    RegName = _msRegistrationConfig.RegName,
                                    Description = _msRegistrationConfig.Description,
                                    PaymentStatus = _msRegistrationConfig.PaymentStatus,
                                    RegistrationProductCode = _msRegistrationConfig.RegistrationProductCode,
                                    RegistrationFee = _msRegistrationConfig.RegistrationFee,
                                    InstalationProductCode = _msRegistrationConfig.InstalationProductCode,
                                    InstalationFee = _msRegistrationConfig.InstalationFee,
                                    DepositProductCode = _msRegistrationConfig.DepositProductCode,
                                    Deposit = _msRegistrationConfig.Deposit,
                                    RecurringFirstCharge = _msRegistrationConfig.RecurringFirstCharge
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILMsRegistrationConfig(_row.RegCode, _row.RegName, _row.Description, _row.PaymentStatus, _row.RegistrationProductCode, _row.RegistrationFee, _row.InstalationProductCode, _row.InstalationFee, _row.DepositProductCode, _row.Deposit, _row.RecurringFirstCharge));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<BILMsRegistrationConfig> GetListRegistrationConfigForDDL()
        {
            List<BILMsRegistrationConfig> _result = new List<BILMsRegistrationConfig>();

            try
            {
                var _query = (
                                from _msRegistrationConfig in this.db.BILMsRegistrationConfigs
                                orderby _msRegistrationConfig.RegName ascending
                                select new
                                {
                                    RegCode = _msRegistrationConfig.RegCode,
                                    RegName = _msRegistrationConfig.RegName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new BILMsRegistrationConfig(_row.RegCode, _row.RegName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiRegistrationConfig(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    BILMsRegistrationConfig _msRegistrationConfig = this.db.BILMsRegistrationConfigs.Single(_temp => _temp.RegCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.BILMsRegistrationConfigs.DeleteOnSubmit(_msRegistrationConfig);
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

        public bool AddRegistrationConfig(BILMsRegistrationConfig _prmMsRegistrationConfig)
        {
            bool _result = false;

            try
            {
                this.db.BILMsRegistrationConfigs.InsertOnSubmit(_prmMsRegistrationConfig);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditRegistrationConfig(BILMsRegistrationConfig _prmMsRegistrationConfig)
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

        ~RegistrationConfigBL()
        {

        }
    }
}
