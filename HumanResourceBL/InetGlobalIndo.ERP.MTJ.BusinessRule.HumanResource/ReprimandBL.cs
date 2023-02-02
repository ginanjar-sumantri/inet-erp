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
    public sealed class ReprimandBL : Base
    {
        public ReprimandBL()
        {

        }

        #region Reprimand
        public double RowsCountReprimand(string _prmCategory, string _prmKeyword)
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

            var _query = (
                            from _msReprimand in this.db.HRMMsReprimands
                            where (SqlMethods.Like(_msReprimand.ReprimandCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msReprimand.ReprimandName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msReprimand.ReprimandCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public HRMMsReprimand GetSingleReprimand(string _prmReprimandCode)
        {
            HRMMsReprimand _result = null;

            try
            {
                _result = this.db.HRMMsReprimands.Single(_temp => _temp.ReprimandCode == _prmReprimandCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetReprimandNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msReprimand in this.db.HRMMsReprimands
                                where _msReprimand.ReprimandCode == _prmCode
                                select new
                                {
                                    ReprimandName = _msReprimand.ReprimandName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ReprimandName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsReprimand> GetListReprimand(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsReprimand> _result = new List<HRMMsReprimand>();
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
                                from _msReprimand in this.db.HRMMsReprimands
                                where (SqlMethods.Like(_msReprimand.ReprimandCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msReprimand.ReprimandName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msReprimand.EditDate descending
                                select new
                                {
                                    ReprimandCode = _msReprimand.ReprimandCode,
                                    ReprimandName = _msReprimand.ReprimandName,
                                    ReprimandStatus = _msReprimand.ReprimandStatus,
                                    ReprimandLevel = _msReprimand.ReprimandLevel,
                                    FgMaxTaken = _msReprimand.FgMaxTaken,
                                    MaxTaken = _msReprimand.MaxTaken,
                                    RangeEffective = _msReprimand.RangeEffective
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsReprimand(_row.ReprimandCode, _row.ReprimandName, _row.ReprimandStatus, _row.ReprimandLevel, _row.FgMaxTaken, _row.MaxTaken, _row.RangeEffective));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsReprimand> GetListReprimandForDDL()
        {
            List<HRMMsReprimand> _result = new List<HRMMsReprimand>();

            try
            {
                var _query = (
                                from _msReprimand in this.db.HRMMsReprimands
                                orderby _msReprimand.ReprimandCode ascending
                                select new
                                {
                                    ReprimandCode = _msReprimand.ReprimandCode,
                                    ReprimandName = _msReprimand.ReprimandName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsReprimand(_row.ReprimandCode, _row.ReprimandName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiReprimand(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMMsReprimand _msReprimand = this.db.HRMMsReprimands.Single(_temp => _temp.ReprimandCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.HRMMsReprimands.DeleteOnSubmit(_msReprimand);
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

        public bool AddReprimand(HRMMsReprimand _prmMsReprimand)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsReprimands.InsertOnSubmit(_prmMsReprimand);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditReprimand(HRMMsReprimand _prmMsReprimand)
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

        ~ReprimandBL()
        {

        }
    }
}
