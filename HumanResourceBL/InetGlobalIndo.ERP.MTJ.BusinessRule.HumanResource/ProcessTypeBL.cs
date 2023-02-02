using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class ProcessTypeBL : Base
    {
        public ProcessTypeBL()
        {
        }

        #region ProcessType
        public double RowsCountProcessType(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "ProcessTypeName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                            from _hrm_Master_ProcessType in this.db.HRM_Master_ProcessTypes
                            where (SqlMethods.Like(_hrm_Master_ProcessType.ProcessTypeName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            select _hrm_Master_ProcessType.ProcessTypeCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public HRM_Master_ProcessType GetSingle(Guid _prmProcessTypeCode)
        {
            HRM_Master_ProcessType _result = null;

            try
            {
                _result = this.db.HRM_Master_ProcessTypes.Single(_temp => _temp.ProcessTypeCode == _prmProcessTypeCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetProcessTypeNameByCode(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _hrm_Master_ProcessType in this.db.HRM_Master_ProcessTypes
                                where _hrm_Master_ProcessType.ProcessTypeCode == _prmCode
                                select new
                                {
                                    ProcessTypeName = _hrm_Master_ProcessType.ProcessTypeName
                                }
                            );

                foreach (var _obj in _query)
                {
                    _result = _obj.ProcessTypeName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_Master_ProcessType> GetListProcessType(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_Master_ProcessType> _result = new List<HRM_Master_ProcessType>();

            string _pattern1 = "%%";

            if (_prmCategory == "ProcessTypeName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _processType in this.db.HRM_Master_ProcessTypes
                                orderby _processType.EditDate descending
                                where (SqlMethods.Like(_processType.ProcessTypeName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                select new
                                {
                                    ProcessTypeCode = _processType.ProcessTypeCode,
                                    ProcessTypeName = _processType.ProcessTypeName,
                                    IsActive = _processType.IsActive,
                                    Description = _processType.Description
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_Master_ProcessType(_row.ProcessTypeCode, _row.ProcessTypeName, _row.IsActive, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_Master_ProcessType> GetListProcessTypeForDDL()
        {
            List<HRM_Master_ProcessType> _result = new List<HRM_Master_ProcessType>();

            try
            {
                var _query = (
                                from _processType in this.db.HRM_Master_ProcessTypes
                                where _processType.IsActive == true
                                orderby _processType.EditDate descending
                                select new
                                {
                                    ProcessTypeCode = _processType.ProcessTypeCode,
                                    ProcessTypeName = _processType.ProcessTypeName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_Master_ProcessType(_row.ProcessTypeCode, _row.ProcessTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(HRM_Master_ProcessType _prmHRM_Master_ProcessType)
        {
            bool _result = false;

            try
            {
                if (this.IsProcessTypeNameExists(_prmHRM_Master_ProcessType.ProcessTypeName, _prmHRM_Master_ProcessType.ProcessTypeCode) == false)
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

        private bool IsProcessTypeNameExists(string _prmProcessTypeName, Guid _prmProcessTypeCode)
        {
            bool _result = false;

            try
            {
                var _query = from _processType in this.db.HRM_Master_ProcessTypes
                             where (_processType.ProcessTypeName == _prmProcessTypeName) && (_processType.ProcessTypeCode != _prmProcessTypeCode)
                             select new
                             {
                                 _processType.ProcessTypeName
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

        public bool Add(HRM_Master_ProcessType _prmHRM_Master_ProcessType)
        {
            bool _result = false;

            try
            {
                if (this.IsProcessTypeNameExists(_prmHRM_Master_ProcessType.ProcessTypeName, _prmHRM_Master_ProcessType.ProcessTypeCode) == false)
                {
                    this.db.HRM_Master_ProcessTypes.InsertOnSubmit(_prmHRM_Master_ProcessType);

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

        public bool DeleteMulti(string[] _prmProcessTypeCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmProcessTypeCode.Length; i++)
                {
                    HRM_Master_ProcessType _hrm_Master_ProcessType = this.db.HRM_Master_ProcessTypes.Single(_processType => _processType.ProcessTypeCode == new Guid(_prmProcessTypeCode[i]));

                    this.db.HRM_Master_ProcessTypes.DeleteOnSubmit(_hrm_Master_ProcessType);
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

        ~ProcessTypeBL()
        {
        }
    }
}
