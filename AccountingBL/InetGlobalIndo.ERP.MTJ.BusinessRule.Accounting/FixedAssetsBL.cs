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
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.IO;
using System.Web.UI.WebControls;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed partial class FixedAssetsBL : Base
    {
        public FixedAssetsBL()
        {
        }

        #region FAMaintenance

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
                            from _msFAMaintenance in this.db.MsFAMaintenances
                            where (SqlMethods.Like(_msFAMaintenance.FAMaintenanceCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msFAMaintenance.FAMaintenanceName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msFAMaintenance.FAMaintenanceCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool Edit(MsFAMaintenance _prmMsFAMaintenance)
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

        public bool Add(MsFAMaintenance _prmMsFAMaintenance)
        {
            bool _result = false;

            try
            {

                this.db.MsFAMaintenances.InsertOnSubmit(_prmMsFAMaintenance);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmMsFAMaintenanceCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmMsFAMaintenanceCode.Length; i++)
                {
                    //var _query = (
                    //               from _msFAMaintenanceDt in this.db.MsFAMaintenanceAccs
                    //               where _msFAMaintenanceDt.FAMaintenance == _prmMsFAMaintenanceCode[i]
                    //               select _msFAMaintenanceDt
                    //             );

                    //this.db.MsFAMaintenanceAccs.DeleteAllOnSubmit(_query);

                    MsFAMaintenance _msFAMaintenance = this.db.MsFAMaintenances.Single(_fa => _fa.FAMaintenanceCode == _prmMsFAMaintenanceCode[i]);

                    this.db.MsFAMaintenances.DeleteOnSubmit(_msFAMaintenance);
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

        public MsFAMaintenance GetSingle(string _prmMsFAMaintenanceCode)
        {
            MsFAMaintenance _result = null;

            try
            {
                _result = this.db.MsFAMaintenances.Single(_fa => _fa.FAMaintenanceCode == _prmMsFAMaintenanceCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFAMaintenance> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MsFAMaintenance> _result = new List<MsFAMaintenance>();

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
                                from _faMaintenance in this.db.MsFAMaintenances
                                where (SqlMethods.Like(_faMaintenance.FAMaintenanceCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_faMaintenance.FAMaintenanceName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    FACode = _faMaintenance.FAMaintenanceCode,
                                    FAName = _faMaintenance.FAMaintenanceName,
                                    FgAddValue = _faMaintenance.FgAddValue
                                }
                            );

                if (_prmOrderBy == "Fixed Asset Maintenance Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FACode)) : (_query1.OrderByDescending(a => a.FACode));
                if (_prmOrderBy == "Fixed Asset Maintenance Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAName)) : (_query1.OrderByDescending(a => a.FAName));
                if (_prmOrderBy == "Add Value")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FgAddValue)) : (_query1.OrderByDescending(a => a.FgAddValue));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);


                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FACode = this._string, FAName = this._string, FgAddValue = this._char });

                    _result.Add(new MsFAMaintenance(_row.FACode, _row.FAName, _row.FgAddValue));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFAMaintenance> GetList()
        {
            List<MsFAMaintenance> _result = new List<MsFAMaintenance>();

            try
            {
                var _query =
                            (
                                from _fixAssets in this.db.MsFAMaintenances
                                select new
                                {
                                    FACode = _fixAssets.FAMaintenanceCode,
                                    FAName = _fixAssets.FAMaintenanceName,
                                    FgAddValue = _fixAssets.FgAddValue
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FACode = this._string, FAName = this._string, FgAddValue = this._char });

                    _result.Add(new MsFAMaintenance(_row.FACode, _row.FAName, _row.FgAddValue));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFAMaintenance> GetListDDLFixedAssetMaintenance()
        {
            List<MsFAMaintenance> _result = new List<MsFAMaintenance>();

            var _query = (
                            from _msFAMaintenance in this.db.MsFAMaintenances
                            orderby _msFAMaintenance.FAMaintenanceName ascending
                            select new
                            {
                                FAMaintenanceCode = _msFAMaintenance.FAMaintenanceCode,
                                FAMaintenanceName = _msFAMaintenance.FAMaintenanceName
                            }
                        );

            foreach (var _row in _query)
            {
                _result.Add(new MsFAMaintenance(_row.FAMaintenanceCode, _row.FAMaintenanceName));
            }

            return _result;
        }

        public string GetFixedAssetMaintenanceNameByCode(string _prmCode)
        {
            string _result = "";

            _result = this.db.MsFAMaintenances.Single(_temp => _temp.FAMaintenanceCode.Trim().ToLower() == _prmCode.Trim().ToLower()).FAMaintenanceName;

            return _result;
        }

        #endregion

        #region FAStatus

        public double RowsCountFAStatus(string _prmCategory, string _prmKeyword)
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
                            from _msFAStatus in this.db.MsFAStatus
                            where (SqlMethods.Like(_msFAStatus.FAStatusCode.ToString(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msFAStatus.FAStatusName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msFAStatus.FAStatusCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool EditFAStatus(MsFAStatus _prmMsFAStatus)
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

        public bool AddFAStatus(MsFAStatus _prmMsFAStatus)
        {
            bool _result = false;

            try
            {

                this.db.MsFAStatus.InsertOnSubmit(_prmMsFAStatus);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFAStatus(string[] _prmMsFAStatusCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmMsFAStatusCode.Length; i++)
                {
                    MsFAStatus _MsFAStatus = this.db.MsFAStatus.Single(_fa => _fa.FAStatusCode == _prmMsFAStatusCode[i]);

                    this.db.MsFAStatus.DeleteOnSubmit(_MsFAStatus);
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

        public MsFAStatus GetSingleFAStatus(string _prmMsFAStatusCode)
        {
            MsFAStatus _result = null;

            try
            {
                _result = this.db.MsFAStatus.Single(_fa => _fa.FAStatusCode == _prmMsFAStatusCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFAStatusSubNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msFAStatus in this.db.MsFAStatus
                                where _msFAStatus.FAStatusCode == _prmCode
                                select new
                                {
                                    FAStatusName = _msFAStatus.FAStatusName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.FAStatusName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFAStatus> GetListFAStatus(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MsFAStatus> _result = new List<MsFAStatus>();

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
                                from _faStatus in this.db.MsFAStatus
                                where (SqlMethods.Like(_faStatus.FAStatusCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_faStatus.FAStatusName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    FACode = _faStatus.FAStatusCode,
                                    FAName = _faStatus.FAStatusName
                                }
                            );

                if (_prmOrderBy == "Fixed Asset Condition Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FACode)) : (_query1.OrderByDescending(a => a.FACode));
                if (_prmOrderBy == "Fixed Asset Condition Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAName)) : (_query1.OrderByDescending(a => a.FAName));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsFAStatus(_row.FACode, _row.FAName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFAStatus> GetListFAStatus()
        {
            List<MsFAStatus> _result = new List<MsFAStatus>();

            try
            {
                var _query =
                            (
                                from _faStatus in this.db.MsFAStatus
                                select new
                                {
                                    FACode = _faStatus.FAStatusCode,
                                    FAName = _faStatus.FAStatusName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsFAStatus(_row.FACode, _row.FAName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetTopFAStatusCode()
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msFAStatus in this.db.MsFAStatus
                                select new
                                {
                                    FAStatusCode = _msFAStatus.FAStatusCode
                                }
                              ).Take(1);

                foreach (var _obj in _query)
                {
                    _result = _obj.FAStatusCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsFAStatusExist(String _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _msFAStatus in this.db.MsFAStatus
                                where _msFAStatus.FAStatusCode == _prmCode
                                select _msFAStatus.FAStatusCode
                             );

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

        #region FAProcess
        //public double FAProcessForList(int _prmYear, int _prmPeriod)
        //{
        //    double _result = 0;

        //    try
        //    {
        //        _result = (
        //                        from _glFAProcessForm in this.db.S_GLFAProcessFormula(_prmYear, _prmPeriod)
        //                        where !(
        //                            from _glFAProcessDt in this.db.GLFAProcessDts
        //                            where _glFAProcessDt.FACode == _glFAProcessForm.FACode && _glFAProcessDt.Year == _prmYear && _glFAProcessDt.Period == _prmPeriod
        //                            select _glFAProcessDt.FACode
        //                        ).Contains(_glFAProcessForm.FACode)

        //                        select _glFAProcessForm.FACode
        //                    ).Count();
        //    }

        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public double RowsCountFAProcessForList(int _prmYear, int _prmPeriod, String _prmCategory, String _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "";
            string _pattern2 = "";

            if (_prmCategory == "Code")
            {
                _pattern1 = _prmKeyword.Trim();
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = _prmKeyword.Trim();
            }

            try
            {
                _result = (
                                from _glFAProcessForm in this.db.S_GLFAProcessFormula(_prmYear, _prmPeriod, _pattern1, _pattern2)
                                where !(
                                    from _glFAProcessDt in this.db.GLFAProcessDts
                                    where _glFAProcessDt.FACode == _glFAProcessForm.FACode && _glFAProcessDt.Year == _prmYear && _glFAProcessDt.Period == _prmPeriod
                                    select _glFAProcessDt.FACode
                                    ).Contains(_glFAProcessForm.FACode)
                                select _glFAProcessForm.FACode
                            ).Count();
            }

            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<MsFixedAsset> FAProcessForList(int _prmReqPage, int _prmPageSize, int _prmYear, int _prmPeriod)
        //{
        //    List<MsFixedAsset> _result = new List<MsFixedAsset>();

        //    try
        //    {
        //        var _query = (
        //                        from _glFAProcessForm in this.db.S_GLFAProcessFormula(_prmYear, _prmPeriod)
        //                        where !(
        //                            from _glFAProcessDt in this.db.GLFAProcessDts
        //                            where _glFAProcessDt.FACode == _glFAProcessForm.FACode && _glFAProcessDt.Year == _prmYear && _glFAProcessDt.Period == _prmPeriod
        //                            select _glFAProcessDt.FACode
        //                        ).Contains(_glFAProcessForm.FACode)

        //                        select new
        //                        {
        //                            _glFAProcessForm.FACode,
        //                            _glFAProcessForm.FAName,
        //                            _glFAProcessForm.BalanceLife,
        //                            _glFAProcessForm.BalanceAmount,
        //                            _glFAProcessForm.Amount
        //                        }
        //                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

        //        foreach (var _item in _query)
        //        {
        //            MsFixedAsset _process = new MsFixedAsset();

        //            _process.FACode = _item.FACode;
        //            _process.FAName = _item.FAName;
        //            _process.LifeAdd = _item.BalanceLife;
        //            _process.AmountProcess = _item.BalanceAmount;
        //            _process.AmountAdd = _item.Amount;

        //            _result.Add(_process);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public List<MsFixedAsset> FAProcessForList(int _prmReqPage, int _prmPageSize, int _prmYear, int _prmPeriod, String _prmCategory, String _prmKeyword)
        {
            List<MsFixedAsset> _result = new List<MsFixedAsset>();

            string _pattern1 = "";
            string _pattern2 = "";

            if (_prmCategory == "Code")
            {
                _pattern1 = _prmKeyword.Trim();
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = _prmKeyword.Trim();
            }

            try
            {
                var _query = (
                                from _glFAProcessForm in this.db.S_GLFAProcessFormula(_prmYear, _prmPeriod, _pattern1, _pattern2)
                                where !(
                                        from _glFAProcessDt in this.db.GLFAProcessDts
                                        where _glFAProcessDt.FACode == _glFAProcessForm.FACode && _glFAProcessDt.Year == _prmYear && _glFAProcessDt.Period == _prmPeriod
                                        select _glFAProcessDt.FACode
                                    ).Contains(_glFAProcessForm.FACode)
                                select new
                                {
                                    _glFAProcessForm.FACode,
                                    _glFAProcessForm.FAName,
                                    _glFAProcessForm.BalanceLife,
                                    _glFAProcessForm.BalanceAmount,
                                    _glFAProcessForm.Amount
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _item in _query)
                {
                    MsFixedAsset _process = new MsFixedAsset();

                    _process.FACode = _item.FACode;
                    _process.FAName = _item.FAName;
                    _process.LifeAdd = _item.BalanceLife;
                    _process.AmountProcess = _item.BalanceAmount;
                    _process.AmountAdd = _item.Amount;

                    _result.Add(_process);
                }
            }

            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<MsFixedAsset> FAProcessForListA(int _prmYear, int _prmPeriod)
        //{
        //    List<MsFixedAsset> _result = new List<MsFixedAsset>();

        //    try
        //    {
        //        var _query = (
        //                        from _glFAProcessForm in this.db.S_GLFAProcessFormula(_prmYear, _prmPeriod)
        //                        where !(
        //                            from _glFAProcessDt in this.db.GLFAProcessDts
        //                            where _glFAProcessDt.FACode == _glFAProcessForm.FACode && _glFAProcessDt.Year == _prmYear && _glFAProcessDt.Period == _prmPeriod
        //                            select _glFAProcessDt.FACode
        //                        ).Contains(_glFAProcessForm.FACode)

        //                        select new
        //                        {
        //                            _glFAProcessForm.FACode
        //                        }
        //                    );

        //        foreach (var _item in _query)
        //        {
        //            MsFixedAsset _process = new MsFixedAsset();

        //            _process.FACode = _item.FACode;

        //            _result.Add(_process);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public List<MsFixedAsset> FAProcessForListA(int _prmYear, int _prmPeriod, String _prmCategory, String _prmKeyword)
        {
            List<MsFixedAsset> _result = new List<MsFixedAsset>();

            string _pattern1 = "";
            string _pattern2 = "";

            if (_prmCategory == "Code")
            {
                _pattern1 = _prmKeyword.Trim();
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = _prmKeyword.Trim();
            }

            try
            {
                var _query = (
                                from _glFAProcessForm in this.db.S_GLFAProcessFormula(_prmYear, _prmPeriod, _pattern1, _pattern2)
                                where !(
                                    from _glFAProcessDt in this.db.GLFAProcessDts
                                    where _glFAProcessDt.FACode == _glFAProcessForm.FACode && _glFAProcessDt.Year == _prmYear && _glFAProcessDt.Period == _prmPeriod
                                    select _glFAProcessDt.FACode
                                ).Contains(_glFAProcessForm.FACode)
                                select new
                                {
                                    _glFAProcessForm.FACode
                                }
                            );

                foreach (var _item in _query)
                {
                    MsFixedAsset _process = new MsFixedAsset();

                    _process.FACode = _item.FACode;

                    _result.Add(_process);
                }
            }

            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddListFA(List<MsFixedAsset> _prmFixedAsset, int _prmYear, int _prmPeriod)
        {
            bool _result = false;

            String[] _faCode = new String[_prmFixedAsset.Count];

            for (int i = 0; i < _faCode.Count(); i++)
            {
                _faCode[i] = _prmFixedAsset[i].FACode;
            }

            this.AddList(_faCode, _prmYear, _prmPeriod);

            _result = true;

            return _result;
        }

        public List<GLFAProcessDt> AddList(string[] _prmCode, int _prmYear, int _prmPeriod)
        {
            List<GLFAProcessDt> _result = new List<GLFAProcessDt>();

            try
            {
                foreach (S_GLFAProcessFormulaResult _item in this.db.S_GLFAProcessFormula(_prmYear, _prmPeriod, "", ""))
                {
                    GLFAProcessDt _glFAProcessDt = new GLFAProcessDt();

                    _glFAProcessDt.Year = _prmYear;
                    _glFAProcessDt.Period = _prmPeriod;
                    _glFAProcessDt.FACode = _item.FACode;
                    _glFAProcessDt.AdjustDepr = 0;
                    _glFAProcessDt.AmountDepr = Convert.ToDecimal(_item.Amount);
                    _glFAProcessDt.BalanceAmount = _item.BalanceAmount;
                    _glFAProcessDt.BalanceLife = _item.BalanceLife;
                    _glFAProcessDt.TotalDepr = Convert.ToDecimal(_item.Amount);

                    _result.Add(_glFAProcessDt);
                }


                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAProcessDt _glFAProcessDt = new GLFAProcessDt();

                    _glFAProcessDt.FACode = _prmCode[i];

                    foreach (var item in _result)
                    {
                        if (_glFAProcessDt.FACode == item.FACode)
                        {
                            this.db.GLFAProcessDts.InsertOnSubmit(item);
                        }
                    }

                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public ISingleResult<GetListOfMonth2Result> GetPeriod()
        {
            ISingleResult<GetListOfMonth2Result> _result = null;

            try
            {
                _result = this.db.GetListOfMonth2(LanguageDataMapper.IsLanguage(Language.English));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int RowsCountFAProcessDt(int _prmYear, int _prmPeriod)
        {
            int _result = 0;

            _result = this.db.GLFAProcessDts.Where(_row => _row.Year == _prmYear && _row.Period == _prmPeriod).Count();

            return _result;
        }

        public int RowsCountFAProcessDt(int _prmYear, int _prmPeriod, String _prmCategory, String _prmKeyword)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query =
                            (
                                from _glFAProcessDt in this.db.GLFAProcessDts
                                join _msFixedAsset in this.db.MsFixedAssets
                                    on _glFAProcessDt.FACode equals _msFixedAsset.FACode
                                where _glFAProcessDt.Year == _prmYear && _glFAProcessDt.Period == _prmPeriod
                                    && (SqlMethods.Like(_glFAProcessDt.FACode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msFixedAsset.FAName.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _glFAProcessDt
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFAProcessDt(GLFAProcessDt _prmGLFAProcessDt)
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

        public GLFAProcessDt GetSingleFAProcessDt(int _prmYear, int _prmPeriod, string _prmFACode)
        {
            GLFAProcessDt _result = null;

            try
            {
                _result = this.db.GLFAProcessDts.Single(_fa => _fa.Year == _prmYear && _fa.Period == _prmPeriod && _fa.FACode == _prmFACode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLFAProcessDt> GetListFAProcessDt(int _prmReqPage, int _prmPageSize, int _prmYear, int _prmPeriod, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<GLFAProcessDt> _result = new List<GLFAProcessDt>();

            try
            {
                var _query1 =
                            (
                                from _faProcessDt in this.db.GLFAProcessDts
                                join _fixedAsset in this.db.MsFixedAssets
                                on _faProcessDt.FACode equals _fixedAsset.FACode
                                where _faProcessDt.Year == _prmYear && _faProcessDt.Period == _prmPeriod
                                select new
                                {
                                    FACode = _faProcessDt.FACode,
                                    FAName = _fixedAsset.FAName,
                                    AmountDepr = _faProcessDt.AmountDepr,
                                    AdjustDepr = _faProcessDt.AdjustDepr,
                                    TotalDepr = _faProcessDt.TotalDepr,
                                    BalanceAmount = _faProcessDt.BalanceAmount,
                                    BalanceLife = _faProcessDt.BalanceLife
                                }
                            );

                if (_prmOrderBy == "Fixed Asset Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FACode)) : (_query1.OrderByDescending(a => a.FACode));
                if (_prmOrderBy == "Fixed Asset Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAName)) : (_query1.OrderByDescending(a => a.FAName));
                if (_prmOrderBy == "Life")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.BalanceLife)) : (_query1.OrderByDescending(a => a.BalanceLife));
                if (_prmOrderBy == "Amount")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.BalanceAmount)) : (_query1.OrderByDescending(a => a.BalanceAmount));
                if (_prmOrderBy == "Amount1")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AmountDepr)) : (_query1.OrderByDescending(a => a.AmountDepr));
                if (_prmOrderBy == "Adjust")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AdjustDepr)) : (_query1.OrderByDescending(a => a.AdjustDepr));
                if (_prmOrderBy == "Total")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TotalDepr)) : (_query1.OrderByDescending(a => a.TotalDepr));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FACode = this._string, FAName = this._string, AmountDepr = this._decimal, AdjustDepr = this._decimal, TotalDepr = this._decimal, BalanceAmount = this._nullableDecimal, BalanceLife = this._nullableInt });

                    _result.Add(new GLFAProcessDt(_row.FACode, _row.FAName, _row.AmountDepr, _row.AdjustDepr, _row.TotalDepr, _row.BalanceAmount, _row.BalanceLife));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLFAProcessDt> GetListFAProcessDt(int _prmReqPage, int _prmPageSize, int _prmYear, int _prmPeriod, String _prmOrderBy, Boolean _prmAscDesc, String _prmCategory, String _prmKeyword)
        {
            List<GLFAProcessDt> _result = new List<GLFAProcessDt>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query1 =
                            (
                                from _faProcessDt in this.db.GLFAProcessDts
                                join _msFixedAsset in this.db.MsFixedAssets
                                    on _faProcessDt.FACode equals _msFixedAsset.FACode
                                where _faProcessDt.Year == _prmYear && _faProcessDt.Period == _prmPeriod
                                    && (SqlMethods.Like(_faProcessDt.FACode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msFixedAsset.FAName.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    FACode = _faProcessDt.FACode,
                                    FAName = _msFixedAsset.FAName,
                                    AmountDepr = _faProcessDt.AmountDepr,
                                    AdjustDepr = _faProcessDt.AdjustDepr,
                                    TotalDepr = _faProcessDt.TotalDepr,
                                    BalanceAmount = _faProcessDt.BalanceAmount,
                                    BalanceLife = _faProcessDt.BalanceLife
                                }
                            );

                if (_prmOrderBy == "Fixed Asset Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FACode)) : (_query1.OrderByDescending(a => a.FACode));
                if (_prmOrderBy == "Fixed Asset Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAName)) : (_query1.OrderByDescending(a => a.FAName));
                if (_prmOrderBy == "Life")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.BalanceLife)) : (_query1.OrderByDescending(a => a.BalanceLife));
                if (_prmOrderBy == "Amount")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.BalanceAmount)) : (_query1.OrderByDescending(a => a.BalanceAmount));
                if (_prmOrderBy == "Amount1")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AmountDepr)) : (_query1.OrderByDescending(a => a.AmountDepr));
                if (_prmOrderBy == "Adjust")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AdjustDepr)) : (_query1.OrderByDescending(a => a.AdjustDepr));
                if (_prmOrderBy == "Total")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TotalDepr)) : (_query1.OrderByDescending(a => a.TotalDepr));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FACode = this._string, FAName = this._string, AmountDepr = this._decimal, AdjustDepr = this._decimal, TotalDepr = this._decimal, BalanceAmount = this._nullableDecimal, BalanceLife = this._nullableInt });

                    _result.Add(new GLFAProcessDt(_row.FACode, _row.FAName, _row.AmountDepr, _row.AdjustDepr, _row.TotalDepr, _row.BalanceAmount, _row.BalanceLife));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAppr(int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _period = "0" + _prmPeriod.ToString();
            string _nmbr = _prmYear.ToString() + _period.Substring(_period.Length - 2, 2);

            try
            {
                this.db.S_GLFAProcessGetAppr(_nmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Posting(int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _period = "0" + _prmPeriod.ToString();
            string _nmbr = _prmYear.ToString() + _period.Substring(_period.Length - 2, 2);

            try
            {
                this.db.S_GLFAProcessPost(_nmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Posting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _period = "0" + _prmPeriod.ToString();
            string _nmbr = _prmYear.ToString() + _period.Substring(_period.Length - 2, 2);

            try
            {
                this.db.S_GLFAProcessApprove(_nmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Approve Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Unposting(int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _period = "0" + _prmPeriod.ToString();
            string _nmbr = _prmYear.ToString() + _period.Substring(_period.Length - 2, 2);

            try
            {
                this.db.S_GLFAProcessUnPost(_nmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "UnPosting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public double RowsCountFAProcessHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Year")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Period")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _glFAProcessHd in this.db.GLFAProcessHds
                            where (SqlMethods.Like(_glFAProcessHd.Year.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_glFAProcessHd.Period.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _glFAProcessHd.Year
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool EditFAProcessHd(GLFAProcessHd _prmGLFAProcessHd)
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

        public bool AddFAProcessHd(GLFAProcessHd _prmGLFAProcessHd)
        {
            bool _result = false;

            try
            {
                this.db.GLFAProcessHds.InsertOnSubmit(_prmGLFAProcessHd);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsDataProcessExist(int _prmYear, int _prmPeriod)
        {
            bool _result = false;

            try
            {
                var _query = from _glFAProcessHd in this.db.GLFAProcessHds
                             select new
                             {
                                 _glFAProcessHd.Year,
                                 _glFAProcessHd.Period
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

        public bool PostedPrevPeriodExist(int _prmYear, int _prmPeriod)
        {
            bool _result = false;

            try
            {
                // cek period yg year == year skr
                var _queryPrevPeriod1 = from _glFAProcessHd in this.db.GLFAProcessHds
                                        where _glFAProcessHd.Year == _prmYear && _glFAProcessHd.Period < _prmPeriod && _glFAProcessHd.Status == TransactionDataMapper.GetStatus(TransStatus.Posted)
                                        select new
                                        {
                                            _glFAProcessHd.Year,
                                            _glFAProcessHd.Period
                                        };

                if (_queryPrevPeriod1.Count() > 0)
                {
                    _result = true;
                }

                //cek period yg year lebih kecil
                var _queryPrevPeriod2 = from _glFAProcessHd in this.db.GLFAProcessHds
                                        where _glFAProcessHd.Year < _prmYear && _glFAProcessHd.Status == TransactionDataMapper.GetStatus(TransStatus.Posted)
                                        select new
                                        {
                                            _glFAProcessHd.Year,
                                            _glFAProcessHd.Period
                                        };

                if (_queryPrevPeriod2.Count() > 0)
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

        public bool PostedNextPeriodExist(int _prmYear, int _prmPeriod)
        {
            bool _result = false;

            try
            {
                // cek period yg year == year skr
                var _queryPrevPeriod1 = from _glFAProcessHd in this.db.GLFAProcessHds
                                        where _glFAProcessHd.Year == _prmYear && _glFAProcessHd.Period > _prmPeriod && _glFAProcessHd.Status == TransactionDataMapper.GetStatus(TransStatus.Posted)
                                        select new
                                        {
                                            _glFAProcessHd.Year,
                                            _glFAProcessHd.Period
                                        };

                if (_queryPrevPeriod1.Count() > 0)
                {
                    _result = true;
                }

                //cek period yg year lebih kecil
                var _queryPrevPeriod2 = from _glFAProcessHd in this.db.GLFAProcessHds
                                        where _glFAProcessHd.Year > _prmYear && _glFAProcessHd.Status == TransactionDataMapper.GetStatus(TransStatus.Posted)
                                        select new
                                        {
                                            _glFAProcessHd.Year,
                                            _glFAProcessHd.Period
                                        };

                if (_queryPrevPeriod2.Count() > 0)
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

        public bool DeleteMultiFAProcessHd(string _temp)
        {
            bool _result = false;

            string[] _tempsplit = _temp.Split(',');

            try
            {
                for (int i = 0; i < _tempsplit.Length; i++)
                {
                    string[] _tempsplit2 = _tempsplit[i].Split('-');

                    GLFAProcessHd _glFAProcessHd = this.db.GLFAProcessHds.Single(_fa => _fa.Year == Convert.ToInt32(_tempsplit2[0]) && _fa.Period == Convert.ToInt32(_tempsplit2[1]));

                    if (_glFAProcessHd != null)
                    {
                        if (_glFAProcessHd.Status != TransactionDataMapper.GetStatus(TransStatus.Posted))
                        {
                            var _query = (from _detail in this.db.GLFAProcessDts
                                          where _detail.Year == Convert.ToInt32(_tempsplit2[0]) && _detail.Period == Convert.ToInt32(_tempsplit2[1])
                                          select _detail);

                            this.db.GLFAProcessDts.DeleteAllOnSubmit(_query);

                            this.db.GLFAProcessHds.DeleteOnSubmit(_glFAProcessHd);

                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
                        }
                    }
                }

                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFAProcessDt(string[] _prmCode, int _prmYear, int _prmPeriod)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAProcessDt _glFAProcessDt = this.db.GLFAProcessDts.Single(_fa => _fa.FACode.Trim().ToLower() == _prmCode[i].Trim().ToLower() && _fa.Year == _prmYear && _fa.Period == _prmPeriod);

                    this.db.GLFAProcessDts.DeleteOnSubmit(_glFAProcessDt);
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

        public bool DeleteAllFAProcessDt(int _prmYear, int _prmPeriod)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _glFAProcessDt in this.db.GLFAProcessDts
                                where _glFAProcessDt.Year == _prmYear && _glFAProcessDt.Period == _prmPeriod
                                select _glFAProcessDt
                              );

                this.db.GLFAProcessDts.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFAProcessHd GetSingleFAProcessHd(int _prmYear, int _prmPeriod)
        {
            GLFAProcessHd _result = null;

            try
            {
                _result = this.db.GLFAProcessHds.Single(_fa => _fa.Year == _prmYear && _fa.Period == _prmPeriod);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLFAProcessHd> GetListFAProcessHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<GLFAProcessHd> _result = new List<GLFAProcessHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            //string _pattern3 = "%%";

            if (_prmCategory == "Year")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";
                //_pattern3 = "%%";
            }
            else if (_prmCategory == "Period")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
                //_pattern3 = "%%";
            }
            //else if (_prmCategory == "Status")
            //{
            //    _pattern3 = "%" + _prmKeyword + "%";
            //    _pattern1 = "%%";
            //    _pattern2 = "%%";
            //}

            try
            {
                var _query1 =
                            (
                                from _faProcessHd in this.db.GLFAProcessHds
                                where (SqlMethods.Like(_faProcessHd.Year.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_faProcessHd.Period.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                //&& (SqlMethods.Like(new FixedAssetStatus().StatusText(_faProcessHd.Status).Trim().ToLower(), _pattern3.Trim().ToLower()))
                                select new
                                {
                                    Year = _faProcessHd.Year,
                                    Period = _faProcessHd.Period,
                                    Status = _faProcessHd.Status,
                                    Remark = _faProcessHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                if (_prmOrderBy == "Year")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Year)) : (_query1.OrderByDescending(a => a.Year));
                if (_prmOrderBy == "Period")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Period)) : (_query1.OrderByDescending(a => a.Period));
                if (_prmOrderBy == "Status")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Status)) : (_query1.OrderByDescending(a => a.Status));
                if (_prmOrderBy == "Remark")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Remark)) : (_query1.OrderByDescending(a => a.Remark));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);


                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Year = this._int, Period = this._int, Status = this._char, Remark = this._string });

                    _result.Add(new GLFAProcessHd(_row.Year, _row.Period, _row.Status, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region FAMaintenanceAcc

        public bool AddAccount(MsFAMaintenanceAcc _prmMsFAMaintenanceAcc)
        {
            bool _result = false;

            try
            {

                this.db.MsFAMaintenanceAccs.InsertOnSubmit(_prmMsFAMaintenanceAcc);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiAcc(string[] _CurrCode, string _prmMsFAMaintenanceCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _CurrCode.Length; i++)
                {
                    MsFAMaintenanceAcc _msFAMaintenanceAcc = this.db.MsFAMaintenanceAccs.Single(_fa => (_fa.CurrCode == _CurrCode[i]) && (_fa.FAMaintenance == _prmMsFAMaintenanceCode));

                    this.db.MsFAMaintenanceAccs.DeleteOnSubmit(_msFAMaintenanceAcc);
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

        public List<MsFAMaintenanceAcc> GetListAcc(int _prmReqPage, int _prmPageSize)
        {
            List<MsFAMaintenanceAcc> _result = new List<MsFAMaintenanceAcc>();

            try
            {
                var _query =
                            (
                                from _fixAssets in this.db.MsFAMaintenanceAccs
                                select new
                                {
                                    FaCode = _fixAssets.FAMaintenance,
                                    CurrCode = _fixAssets.CurrCode,
                                    Account = _fixAssets.Account,
                                    AccountName = (
                                                        from _msAccount in this.db.MsAccounts
                                                        where _msAccount.Account == _fixAssets.Account
                                                        select _msAccount.AccountName
                                                    ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsFAMaintenanceAcc(_row.FaCode, _row.CurrCode, _row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFAMaintenanceAcc> GetListAccByCode(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<MsFAMaintenanceAcc> _result = new List<MsFAMaintenanceAcc>();

            try
            {
                var _query =
                            (
                                from _fixAssets in this.db.MsFAMaintenanceAccs
                                where _fixAssets.FAMaintenance == _prmCode
                                select new
                                {
                                    FaCode = _fixAssets.FAMaintenance,
                                    CurrCode = _fixAssets.CurrCode,
                                    Account = _fixAssets.Account,
                                    AccountName = (
                                                        from _msAccount in this.db.MsAccounts
                                                        where _msAccount.Account == _fixAssets.Account
                                                        select _msAccount.AccountName
                                                    ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsFAMaintenanceAcc(_row.FaCode, _row.CurrCode, _row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region Fixed Assets Location

        public MsFALocation GetSingleFALocation(string _prmFALocationCode)
        {
            MsFALocation _result = null;

            try
            {
                _result = this.db.MsFALocations.Single(_FALocation => _FALocation.FALocationCode == _prmFALocationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFALocationNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msFALocation in this.db.MsFALocations
                                where _msFALocation.FALocationCode == _prmCode
                                select new
                                {
                                    FALocationName = _msFALocation.FALocationName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.FALocationName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountFALocation(string _prmCategory, string _prmKeyword)
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
                            from _msFALocation in this.db.MsFALocations
                            where (SqlMethods.Like(_msFALocation.FALocationCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msFALocation.FALocationName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msFALocation.FALocationCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MsFALocation> GetListFALocation(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MsFALocation> _result = new List<MsFALocation>();

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
                var _query1 = (
                                from _faLocation in this.db.MsFALocations
                                where (SqlMethods.Like(_faLocation.FALocationCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_faLocation.FALocationName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _faLocation.UserDate descending
                                select new
                                {
                                    FALocationCode = _faLocation.FALocationCode,
                                    FALocationName = _faLocation.FALocationName
                                }
                            );

                if (_prmOrderBy == "Fixed Asset Location Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FALocationCode)) : (_query1.OrderByDescending(a => a.FALocationCode));
                if (_prmOrderBy == "Fixed Asset Location Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FALocationName)) : (_query1.OrderByDescending(a => a.FALocationName));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FALocationCode = this._string, FALocationName = this._string });

                    _result.Add(new MsFALocation(_row.FALocationCode, _row.FALocationName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFALocation> GetListFALocation()
        {
            List<MsFALocation> _result = new List<MsFALocation>();

            try
            {
                var _query = (
                                from FALocation in this.db.MsFALocations
                                orderby FALocation.UserDate descending
                                select new
                                {
                                    FALocationCode = FALocation.FALocationCode,
                                    FALocationName = FALocation.FALocationName
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FALocationCode = this._string, FALocationName = this._string });

                    _result.Add(new MsFALocation(_row.FALocationCode, _row.FALocationName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFALocation(MsFALocation _prmMsFALocation)
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

        public bool AddFALocation(MsFALocation _prmMsFALocation)
        {
            bool _result = false;

            try
            {
                this.db.MsFALocations.InsertOnSubmit(_prmMsFALocation);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFALocation(string[] _prmFALocationCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmFALocationCode.Length; i++)
                {
                    MsFALocation _msFALocation = this.db.MsFALocations.Single(_FALocation => _FALocation.FALocationCode.Trim().ToLower() == _prmFALocationCode[i].Trim().ToLower());

                    this.db.MsFALocations.DeleteOnSubmit(_msFALocation);
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

        #region FA Group
        public MsFAGroup GetSingleFAGroup(string _prmFAGroupCode)
        {
            MsFAGroup _result = null;

            try
            {
                _result = this.db.MsFAGroups.Single(_FAGroup => _FAGroup.FAGroupCode == _prmFAGroupCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFAGroupNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msGroup in this.db.MsFAGroups
                                where _msGroup.FAGroupCode == _prmCode
                                select new
                                {
                                    FAGroupName = _msGroup.FAGroupName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.FAGroupName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountFAGroup(string _prmCategory, string _prmKeyword)
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
                            from _msFAGroup in this.db.MsFAGroups
                            where (SqlMethods.Like(_msFAGroup.FAGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msFAGroup.FAGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msFAGroup.FAGroupCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MsFAGroup> GetListFAGroup(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MsFAGroup> _result = new List<MsFAGroup>();

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
                var _query1 = (
                                from _faGroup in this.db.MsFAGroups
                                where (SqlMethods.Like(_faGroup.FAGroupCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_faGroup.FAGroupName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _faGroup.UserDate descending
                                select new
                                {
                                    FAGroupCode = _faGroup.FAGroupCode,
                                    FAGroupName = _faGroup.FAGroupName
                                }
                            );

                if (_prmOrderBy == "Fixed Asset Group Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAGroupCode)) : (_query1.OrderByDescending(a => a.FAGroupCode));
                if (_prmOrderBy == "Fixed Asset Group Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAGroupName)) : (_query1.OrderByDescending(a => a.FAGroupName));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FAGroupCode = this._string, FAGroupName = this._string });

                    _result.Add(new MsFAGroup(_row.FAGroupCode, _row.FAGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFAGroup> GetListFAGroup()
        {
            List<MsFAGroup> _result = new List<MsFAGroup>();

            try
            {
                var _query = (
                                from FAGroup in this.db.MsFAGroups
                                orderby FAGroup.UserDate descending
                                select new
                                {
                                    FAGroupCode = FAGroup.FAGroupCode,
                                    FAGroupName = FAGroup.FAGroupName
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FAGroupCode = this._string, FAGroupName = this._string });

                    _result.Add(new MsFAGroup(_row.FAGroupCode, _row.FAGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetFAGroupByFASubGrpCode(string _prmFASubGroupCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _faSubGroup in this.db.MsFAGroupSubs
                                where _faSubGroup.FASubGrpCode == _prmFASubGroupCode
                                orderby _faSubGroup.UserDate descending
                                select _faSubGroup.FAGroup
                            ).FirstOrDefault();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFAGroup(MsFAGroup _prmMsFAGroup)
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

        public bool AddFAGroup(MsFAGroup _prmMsFAGroup)
        {
            bool _result = false;

            try
            {
                this.db.MsFAGroups.InsertOnSubmit(_prmMsFAGroup);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFAGroup(string[] _prmFAGroupCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmFAGroupCode.Length; i++)
                {
                    MsFAGroup _msFAGroup = this.db.MsFAGroups.Single(_FAGroup => _FAGroup.FAGroupCode.Trim().ToLower() == _prmFAGroupCode[i].Trim().ToLower());

                    this.db.MsFAGroups.DeleteOnSubmit(_msFAGroup);
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

        #region FAGroupSub
        public MsFAGroupSub GetSingleFAGroupSub(string _prmFAGroupSubCode)
        {
            MsFAGroupSub _result = null;

            try
            {
                _result = this.db.MsFAGroupSubs.Single(_FAGroupSub => _FAGroupSub.FASubGrpCode == _prmFAGroupSubCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFAGroupSubNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msGroupSub in this.db.MsFAGroupSubs
                                where _msGroupSub.FASubGrpCode == _prmCode
                                select new
                                {
                                    FASubGrpName = _msGroupSub.FASubGrpName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.FASubGrpName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountFAGroupSub(string _prmCategory, string _prmKeyword)
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
                            from _msFAGroupSub in this.db.MsFAGroupSubs
                            where (SqlMethods.Like(_msFAGroupSub.FASubGrpCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msFAGroupSub.FASubGrpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msFAGroupSub.FASubGrpCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MsFAGroupSub> GetListFAGroupSub(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MsFAGroupSub> _result = new List<MsFAGroupSub>();

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
                var _query1 = (
                                from _faGroupSub in this.db.MsFAGroupSubs
                                where (SqlMethods.Like(_faGroupSub.FASubGrpCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_faGroupSub.FASubGrpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _faGroupSub.UserDate descending
                                select new
                                {
                                    FAGroupSubCode = _faGroupSub.FASubGrpCode,
                                    FAGroupSubName = _faGroupSub.FASubGrpName,
                                    FAGroupCode = _faGroupSub.FAGroup,
                                    Moving = _faGroupSub.FgMoving,
                                    Process = _faGroupSub.FgProcess,
                                }
                            );

                if (_prmOrderBy == "Fixed Asset Sub Group Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAGroupSubCode)) : (_query1.OrderByDescending(a => a.FAGroupSubCode));
                if (_prmOrderBy == "Fixed Asset Sub Group Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAGroupSubName)) : (_query1.OrderByDescending(a => a.FAGroupSubName));
                if (_prmOrderBy == "Fixed Asset Group")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAGroupCode)) : (_query1.OrderByDescending(a => a.FAGroupCode));
                if (_prmOrderBy == "Moving")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Moving)) : (_query1.OrderByDescending(a => a.Moving));
                if (_prmOrderBy == "Process")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Process)) : (_query1.OrderByDescending(a => a.Process));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FAGroupSubCode = this._string, FAGroupSubName = this._string, FAGroupCode = this._string, Moving = this._char, Process = this._char });

                    _result.Add(new MsFAGroupSub(_row.FAGroupSubCode, _row.FAGroupSubName, _row.FAGroupCode, _row.Moving, _row.Process));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFAGroupSub> GetListFAGroupSub()
        {
            List<MsFAGroupSub> _result = new List<MsFAGroupSub>();

            var _query = (
                            from FAGroupSub in this.db.MsFAGroupSubs
                            orderby FAGroupSub.UserDate descending
                            select new
                            {
                                FAGroupSubCode = FAGroupSub.FASubGrpCode,
                                FAGroupSubName = FAGroupSub.FASubGrpName
                            }
                        );

            foreach (object _obj in _query)
            {
                var _row = _obj.Template(new { FAGroupSubCode = this._string, FAGroupSubName = this._string });

                _result.Add(new MsFAGroupSub(_row.FAGroupSubCode, _row.FAGroupSubName));
            }

            return _result;
        }

        public List<MsFAGroupSub> GetListFAGroupSubForDDLByFAGroupCode(string _prmFAGroupCode)
        {
            List<MsFAGroupSub> _result = new List<MsFAGroupSub>();

            var _query = (
                            from _faGroupSub in this.db.MsFAGroupSubs
                            where _faGroupSub.FAGroup == _prmFAGroupCode
                            orderby _faGroupSub.UserDate descending
                            select new
                            {
                                FAGroupSubCode = _faGroupSub.FASubGrpCode,
                                FAGroupSubName = _faGroupSub.FASubGrpName
                            }
                        );

            foreach (object _obj in _query)
            {
                var _row = _obj.Template(new { FAGroupSubCode = this._string, FAGroupSubName = this._string });

                _result.Add(new MsFAGroupSub(_row.FAGroupSubCode, _row.FAGroupSubName));
            }

            return _result;
        }

        public bool EditFAGroupSub(MsFAGroupSub _prmMsFAGroupSub)
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

        public bool AddFAGroupSub(MsFAGroupSub _prmMsFAGroupSub)
        {
            bool _result = false;

            try
            {
                this.db.MsFAGroupSubs.InsertOnSubmit(_prmMsFAGroupSub);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFAGroupSub(string[] _prmFAGroupSubCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmFAGroupSubCode.Length; i++)
                {
                    var _query = (
                                    from _msFAGroupSubDt in this.db.MsFAGroupSubAccs
                                    where _msFAGroupSubDt.FASubGroup == _prmFAGroupSubCode[i]
                                    select _msFAGroupSubDt
                                  );

                    this.db.MsFAGroupSubAccs.DeleteAllOnSubmit(_query);

                    MsFAGroupSub _msFAGroupSub = this.db.MsFAGroupSubs.Single(_FAGroupSub => _FAGroupSub.FASubGrpCode.Trim().ToLower() == _prmFAGroupSubCode[i].Trim().ToLower());

                    this.db.MsFAGroupSubs.DeleteOnSubmit(_msFAGroupSub);
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

        public bool IsFAGroupSubExist(String _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _msFAGroupSub in this.db.MsFAGroupSubs
                                where _msFAGroupSub.FASubGrpCode == _prmCode
                                select _msFAGroupSub.FASubGrpCode
                             );

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

        #region Fixed Assets Sub Group Acc
        public MsFAGroupSubAcc GetSingleFAGroupSubAcc(string _prmFAGroupSubAccCode, string _prmCurrCode)
        {
            MsFAGroupSubAcc _result = null;

            try
            {
                _result = this.db.MsFAGroupSubAccs.Single(_FAGroupSubAcc => (_FAGroupSubAcc.FASubGroup == _prmFAGroupSubAccCode) && (_FAGroupSubAcc.CurrCode == _prmCurrCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int RowsCountFAGroupSubAcc
        {
            get
            {
                return this.db.MsFAGroupSubAccs.Count();
            }
        }

        public List<MsFAGroupSubAcc> GetListFAGroupSubAcc(int _prmReqPage, int _prmPageSize)
        {
            List<MsFAGroupSubAcc> _result = new List<MsFAGroupSubAcc>();

            try
            {
                var _query = (
                                from FAGroupSubAcc in this.db.MsFAGroupSubAccs
                                orderby FAGroupSubAcc.UserDate descending
                                select new
                                {
                                    FAGroupSubAccCode = FAGroupSubAcc.FASubGroup,
                                    CurrCode = FAGroupSubAcc.CurrCode,
                                    AccFA = FAGroupSubAcc.AccFA,
                                    AccDepr = FAGroupSubAcc.AccDepr,
                                    AccAkumDepr = FAGroupSubAcc.AccAkumDepr,
                                    AccSales = FAGroupSubAcc.AccSales,
                                    AccTenancy = FAGroupSubAcc.AccTenancy,
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FAGroupSubAccCode = this._string, CurrCode = this._string, AccFA = this._string, AccDepr = this._string, AccAkumDepr = this._string, AccSales = this._string, AccTenancy = this._string });

                    _result.Add(new MsFAGroupSubAcc(_row.FAGroupSubAccCode, _row.CurrCode, _row.AccFA, _row.AccDepr, _row.AccAkumDepr, _row.AccSales, _row.AccTenancy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFAGroupSubAcc> GetListFAGroupSubAcc(string _prmFACode)
        {
            List<MsFAGroupSubAcc> _result = new List<MsFAGroupSubAcc>();

            try
            {
                var _query = (
                                from _msFAGroupSubAcc in this.db.MsFAGroupSubAccs
                                where _msFAGroupSubAcc.FASubGroup == _prmFACode
                                orderby _msFAGroupSubAcc.UserDate descending
                                select new
                                {
                                    FACode = _msFAGroupSubAcc.FASubGroup,
                                    CurrCode = _msFAGroupSubAcc.CurrCode,
                                    AccAkunName = (
                                                    from _msAccount in this.db.MsAccounts
                                                    where _msAccount.Account == _msFAGroupSubAcc.AccAkumDepr
                                                    select _msAccount.AccountName
                                                ).FirstOrDefault(),
                                    AccAssetName = (
                                                    from _msAccount in this.db.MsAccounts
                                                    where _msAccount.Account == _msFAGroupSubAcc.AccFA
                                                    select _msAccount.AccountName
                                                ).FirstOrDefault(),
                                    AccDPName = (
                                                    from _msAccount in this.db.MsAccounts
                                                    where _msAccount.Account == _msFAGroupSubAcc.AccDepr
                                                    select _msAccount.AccountName
                                                ).FirstOrDefault(),
                                    AccSalesName = (
                                                    from _msAccount in this.db.MsAccounts
                                                    where _msAccount.Account == _msFAGroupSubAcc.AccSales
                                                    select _msAccount.AccountName
                                                ).FirstOrDefault(),
                                    AccTanancyName = (
                                                    from _msAccount in this.db.MsAccounts
                                                    where _msAccount.Account == _msFAGroupSubAcc.AccTenancy
                                                    select _msAccount.AccountName
                                                ).FirstOrDefault()
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsFAGroupSubAcc(_row.FACode, _row.CurrCode, _row.AccAssetName, _row.AccDPName, _row.AccAkunName, _row.AccSalesName, _row.AccTanancyName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFAGroupSubAcc(MsFAGroupSubAcc _prmMsFAGroupSubAcc)
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

        public bool AddFAGroupSubAcc(MsFAGroupSubAcc _prmMsFAGroupSubAcc)
        {
            bool _result = false;

            try
            {
                this.db.MsFAGroupSubAccs.InsertOnSubmit(_prmMsFAGroupSubAcc);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFAGroupSubAcc(string[] _prmCurrCode, String _prmFaGroup)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCurrCode.Length; i++)
                {
                    MsFAGroupSubAcc _msFAGroupSubAcc = this.db.MsFAGroupSubAccs.Single(_FAGroupSubAcc => (_FAGroupSubAcc.CurrCode == _prmCurrCode[i]) && (_FAGroupSubAcc.FASubGroup == _prmFaGroup));

                    this.db.MsFAGroupSubAccs.DeleteOnSubmit(_msFAGroupSubAcc);
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

        #region Fixed Asset

        public double RowsCountFixedAsset(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _msFixedAsset in this.db.MsFixedAssets
                            where (SqlMethods.Like(_msFixedAsset.FACode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msFixedAsset.FAName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msFixedAsset.FACode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MsFixedAsset> GetListFixedAsset(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MsFixedAsset> _result = new List<MsFixedAsset>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword.Trim() + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword.Trim() + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query1 = (
                                from _msFixedAsset in this.db.MsFixedAssets
                                where (SqlMethods.Like(_msFixedAsset.FACode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msFixedAsset.FAName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msFixedAsset.FAName ascending
                                select new
                                {
                                    FACode = _msFixedAsset.FACode,
                                    FAName = _msFixedAsset.FAName,
                                    FAStatusName = (
                                                        from _msFAStatus in this.db.MsFAStatus
                                                        where _msFixedAsset.FAStatus == _msFAStatus.FAStatusCode
                                                        select _msFAStatus.FAStatusName
                                                    ).FirstOrDefault(),
                                    CurrCode = _msFixedAsset.CurrCode,
                                    ForexRate = _msFixedAsset.ForexRate,
                                    AmountForex = _msFixedAsset.AmountForex,
                                    AmountHome = _msFixedAsset.AmountHome,
                                    CreatedFrom = _msFixedAsset.CreatedFrom,
                                    FALocationCode = _msFixedAsset.FALocationCode
                                    //CreateJournal = _msFixedAsset.CreateJournal
                                }
                            );

                if (_prmOrderBy == "Fixed Asset Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FACode)) : (_query1.OrderByDescending(a => a.FACode));
                if (_prmOrderBy == "Fixed Asset Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAName)) : (_query1.OrderByDescending(a => a.FAName));
                if (_prmOrderBy == "Condition")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAStatusName)) : (_query1.OrderByDescending(a => a.FAStatusName));
                if (_prmOrderBy == "Currency")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.CurrCode)) : (_query1.OrderByDescending(a => a.CurrCode));
                if (_prmOrderBy == "Forex Rate")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.ForexRate)) : (_query1.OrderByDescending(a => a.ForexRate));
                if (_prmOrderBy == "Buy Price Forex")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AmountForex)) : (_query1.OrderByDescending(a => a.AmountForex));
                if (_prmOrderBy == "Buy Price Home")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AmountHome)) : (_query1.OrderByDescending(a => a.AmountHome));
                if (_prmOrderBy == "Created From")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.CreatedFrom)) : (_query1.OrderByDescending(a => a.CreatedFrom));
                if (_prmOrderBy == "Location")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FALocationCode)) : (_query1.OrderByDescending(a => a.FALocationCode));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsFixedAsset(_row.FACode, _row.FAName, _row.FAStatusName, _row.CurrCode, _row.ForexRate, _row.AmountForex, _row.AmountHome, _row.CreatedFrom, _row.FALocationCode));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFixedAsset> GetListFixedAsset()
        {
            List<MsFixedAsset> _result = new List<MsFixedAsset>();

            try
            {
                var _query = (
                                from _msFixedAsset in this.db.MsFixedAssets
                                orderby _msFixedAsset.FAName ascending
                                select new
                                {
                                    FACode = _msFixedAsset.FACode,
                                    FAName = _msFixedAsset.FAName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FACode = this._string, FAName = this._string });

                    _result.Add(new MsFixedAsset(_row.FACode, _row.FAName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFixedAsset> GetFAForDDLNonTenancyDetail()
        {
            List<MsFixedAsset> _result = new List<MsFixedAsset>();

            try
            {
                var _query = (
                                from _msFixedAsset in this.db.MsFixedAssets
                                orderby _msFixedAsset.FAName ascending
                                where !(
                                from _glFATenancyDt in this.db.GLFATenancyDts
                                where _glFATenancyDt.FACode == _msFixedAsset.FACode
                                select _glFATenancyDt.FACode
                                ).Contains(_msFixedAsset.FACode)
                                select new
                                {
                                    FACode = _msFixedAsset.FACode,
                                    FAName = _msFixedAsset.FAName + " - " + _msFixedAsset.FACode
                                }
                            );
                //from _glFATenancyDt2 in this.db.GLFATenancyDts
                //where !(
                //            from _glFATenancyDt3 in this.db.GLFATenancyDts
                //            where _glFATenancyDt3.FACode == _prmFACode[i] && _glFATenancyDt3.TransNmbr == _prmTransNmbr
                //            select _glFATenancyDt3.FACode
                //        ).Contains(_glFATenancyDt2.FACode)
                //        && _glFATenancyDt2.TransNmbr == _prmTransNmbr
                //group _glFATenancyDt2 by _glFATenancyDt2.TransNmbr into _grp
                //select new
                //{
                //    AmountForex = _grp.Sum(a => a.AmountForex)
                //}


                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FACode = this._string, FAName = this._string });

                    _result.Add(new MsFixedAsset(_row.FACode, _row.FAName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetGrandTotalFixedAsset()
        {
            decimal _result = 0;

            try
            {
                //var _query = (
                //                from _msFixedAsset in this.db.MsFixedAssets
                //                group _msFixedAsset by new { _msFixedAsset.FACode } into _grp
                //                select new
                //                {
                //                    GrandTotal = _grp.Sum(a => a.AmountHome)
                //                }
                //            );

                //var _query = this.db.MsFixedAssets.Sum(_temp => _temp.AmountHome);

                _result = this.db.MsFixedAssets.Sum(_temp => _temp.AmountHome);

                //foreach (var _row in _query)
                //{
                //    _result = _row.GrandTotal;
                //}
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFixedAsset(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsFixedAsset _msFixedAsset = this.db.MsFixedAssets.Single(_temp => _temp.FACode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsFixedAssets.DeleteOnSubmit(_msFixedAsset);
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

        public MsFixedAsset GetSingleFixedAsset(string _prmCode)
        {
            MsFixedAsset _result = null;

            _result = this.db.MsFixedAssets.Single(_temp => _temp.FACode == _prmCode);

            return _result;
        }

        public String GetFixedAssetNameByCode(string _prmCode)
        {
            //MsFixedAsset _result = new MsFixedAsset();
            String _result = "";

            _result = this.db.MsFixedAssets.Single(_temp => _temp.FACode == _prmCode).FAName;

            return _result;
        }

        public bool AddFixedAsset(MsFixedAsset _prmMsFixedAsset)
        {
            bool _result = false;

            try
            {
                CompanyConfiguration _compConfigAuto = new CompanyConfig().GetSingle(CompanyConfigure.FACodeAutoNumber);

                if (_compConfigAuto.SetValue == CompanyConfigureDataMapper.GetFACodeAutoNumber(FACodeAutoNumber.True))
                {
                    MsFAGroupSub _msFASubGroup = this.db.MsFAGroupSubs.Single(temp => temp.FASubGrpCode == _prmMsFixedAsset.FASubGroup);
                    CompanyConfiguration _compConfigDigit = new CompanyConfig().GetSingle(CompanyConfigure.FACodeDigitNumber);
                    String _autoNmbrLocForFA = new CompanyConfig().GetSingle(CompanyConfigure.AutoNmbrLocForFA).SetValue;
                    String _autoNmbrPeriodForFA = new CompanyConfig().GetSingle(CompanyConfigure.AutoNmbrPeriodForFA).SetValue;

                    if (_prmMsFixedAsset.FACode.Trim() == "")
                    {
                        String _strFACode = "";
                        _strFACode = _msFASubGroup.CodeCounter;
                        if (_autoNmbrPeriodForFA == "T")
                        {
                            DateTime _buyDate = Convert.ToDateTime(_prmMsFixedAsset.BuyingDate);
                            _strFACode += MonthMapper.GetMonthName3Letter(_buyDate.Month) + _buyDate.Year.ToString().Substring(2, 2).PadLeft(2, '0') + "/";
                        }
                        if (_autoNmbrLocForFA == "T")
                        {
                            _strFACode += _prmMsFixedAsset.FALocationCode + "/";
                        }
                        _strFACode += ((int)_msFASubGroup.LastCounterNo + 1).ToString().PadLeft(Convert.ToInt16(_compConfigDigit.SetValue), '0');

                        _prmMsFixedAsset.FACode = _strFACode;
                        _msFASubGroup.LastCounterNo = (int)_msFASubGroup.LastCounterNo + 1;
                    }
                }

                this.db.MsFixedAssets.InsertOnSubmit(_prmMsFixedAsset);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "AddFixedAsset", "Accounting");
            }

            return _result;
        }

        public bool AddFixedAssetList(List<MsFixedAsset> _prmMsFAList)
        {
            bool _result = false;

            try
            {
                foreach (MsFixedAsset _row in _prmMsFAList)
                {
                    MsFixedAsset _msFA = new MsFixedAsset();

                    _msFA.FACode = _row.FACode;
                    _msFA.FAName = _row.FAName;
                    _msFA.FAStatus = _row.FAStatus;
                    _msFA.FAOwner = _row.FAOwner;
                    _msFA.FASubGroup = _row.FASubGroup;
                    _msFA.BuyingDate = _row.BuyingDate;
                    _msFA.FALocationType = _row.FALocationType;
                    _msFA.FALocationCode = _row.FALocationCode;
                    _msFA.CurrCode = _row.CurrCode;
                    _msFA.ForexRate = _row.ForexRate;
                    _msFA.AmountForex = _row.AmountForex;
                    _msFA.AmountHome = _row.AmountHome;
                    _msFA.TotalLifeMonth = _row.TotalLifeMonth;
                    _msFA.LifeDepr = _row.LifeDepr;
                    _msFA.LifeProcess = _row.LifeProcess;
                    _msFA.TotalLifeDepr = _row.TotalLifeDepr;
                    _msFA.AmountDepr = _row.AmountDepr;
                    _msFA.AmountProcess = _row.AmountProcess;
                    _msFA.TotalAmountDepr = _row.TotalAmountDepr;
                    _msFA.AmountCurrent = _row.AmountCurrent;
                    _msFA.FgActive = _row.FgActive;
                    _msFA.FgSold = _row.FgSold;
                    _msFA.FgProcess = _row.FgProcess;
                    _msFA.CreatedFrom = _row.CreatedFrom;
                    _msFA.CreateJournal = _row.CreateJournal;
                    _msFA.Photo = _row.Photo;

                    this.db.MsFixedAssets.InsertOnSubmit(_msFA);
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

        public bool EditFixedAsset(MsFixedAsset _prmMsFixedAsset)
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

        //public string CreateJournal(string _prmFACode, string _prmuser)
        //{
        //    string _result = "";

        //    try
        //    {
        //        this.db.S_GLFABeginning(_prmFACode, _prmuser, ref _result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = "You Failed Create Journal";
        //        ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
        //    }

        //    return _result;
        //}

        //public string DeleteJournal(string _prmFACode, string _prmuser)
        //{
        //    string _result = "";

        //    try
        //    {
        //        this.db.S_GLFABeginningDelete(_prmFACode, _prmuser, ref _result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = "You Failed Delete Journal";
        //        ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
        //    }

        //    return _result;
        //}

        public List<V_MsFALocationType> GetListDDLFixedAssetLocation(char _prmFALocationType)
        {
            List<V_MsFALocationType> _result = new List<V_MsFALocationType>();

            try
            {
                var _query = (
                                from _vMsFALocationType in this.db.V_MsFALocationTypes
                                where _vMsFALocationType.Type == _prmFALocationType
                                orderby _vMsFALocationType.Name ascending
                                select new
                                {
                                    Code = _vMsFALocationType.Code,
                                    Name = _vMsFALocationType.Name
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Code = this._string, Name = this._string });

                    _result.Add(new V_MsFALocationType(_row.Code, _row.Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFixedAsset> GetListDDLFixedAsset()
        {
            List<MsFixedAsset> _result = new List<MsFixedAsset>();

            var _query = (
                            from _msFixedAsset in this.db.MsFixedAssets
                            orderby _msFixedAsset.FAName ascending
                            select new
                            {
                                FACode = _msFixedAsset.FACode,
                                FAName = _msFixedAsset.FAName
                            }
                        );

            foreach (var _row in _query)
            {
                _result.Add(new MsFixedAsset(_row.FACode, _row.FAName));
            }

            return _result;
        }

        public String ChangeFixedAssetPicture(String _prmFACode, FileUpload _prmFileUpload)
        {
            String _result = "";

            String _path = _prmFileUpload.PostedFile.FileName;
            FileInfo _file = new FileInfo(_path);
            String _imagepath = ApplicationConfig.FixedAssetPhotoPath + _prmFACode + _file.Extension;

            if (_path == "")
            {
                _result = "Invalid filename supplied";
            }
            if (_prmFileUpload.PostedFile.ContentLength == 0)
            {
                _result = "Invalid file content";
            }
            if (_result == "")
            {
                if (PictureHandler.IsPictureFile(_path, ApplicationConfig.ImageExtension) == true)
                {
                    System.Drawing.Image _uploadedImage = System.Drawing.Image.FromStream(_prmFileUpload.PostedFile.InputStream);

                    Decimal _width = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Width);
                    Decimal _height = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Height);

                    if (_width > Convert.ToDecimal(ApplicationConfig.ImageHeight) || _height > Convert.ToDecimal(ApplicationConfig.ImageHeight))
                    {
                        _result = "This image is too big - please resize it!";
                    }
                    else
                    {
                        if (_prmFileUpload.PostedFile.ContentLength <= Convert.ToDecimal(ApplicationConfig.ImageMaxSize))
                        {
                            MsFixedAsset _fixedAsset = this.GetSingleFixedAsset(_prmFACode);

                            if (_fixedAsset.Photo != ApplicationConfig.ProductImageDefault)
                            {
                                if (File.Exists(ApplicationConfig.FixedAssetPhotoPath + _fixedAsset.Photo) == true)
                                {
                                    File.Delete(ApplicationConfig.FixedAssetPhotoPath + _fixedAsset.Photo);
                                }
                            }

                            //_file.CopyTo(_imagepath, true);
                            _prmFileUpload.PostedFile.SaveAs(_imagepath);

                            _fixedAsset.Photo = _prmFACode + _file.Extension;
                            this.db.SubmitChanges();

                            _file.Refresh();

                            _result = "File uploaded successfully";
                        }
                        else
                        {
                            _result = "Unable to upload, file exceeds maximum limit";
                        }
                    }
                }
                else
                {
                    _result = "File type not supported";
                }
            }

            return _result;
        }

        #endregion

        #region Fixed Asset Moving

        public double RowsCountFixedAssetMoving(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "RefNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _glFAMoveHd in this.db.GLFAMoveHds
                            where (SqlMethods.Like(_glFAMoveHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_glFAMoveHd.FileNmbr.Trim().ToLower(), _pattern2.Trim().ToLower()) || ((_glFAMoveHd.FileNmbr ?? "") == ""))
                                && (_glFAMoveHd.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted))
                            select _glFAMoveHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<GLFAMoveHd> GetListFixedAssetMoving(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<GLFAMoveHd> _result = new List<GLFAMoveHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "RefNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query1 = (
                                from _glFAMoveHd in this.db.GLFAMoveHds
                                where (SqlMethods.Like(_glFAMoveHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_glFAMoveHd.FileNmbr.Trim().ToLower(), _pattern2.Trim().ToLower()) || ((_glFAMoveHd.FileNmbr ?? "") == ""))
                                    && (_glFAMoveHd.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted))
                                orderby _glFAMoveHd.UserPrep descending
                                select new
                                {
                                    TransNmbr = _glFAMoveHd.TransNmbr,
                                    FileNmbr = _glFAMoveHd.FileNmbr,
                                    TransDate = _glFAMoveHd.TransDate,
                                    Status = _glFAMoveHd.Status,
                                    FALocationTypeSrc = _glFAMoveHd.FALocationTypeSrc,
                                    FALocationCodeSrc = _glFAMoveHd.FALocationCodeSrc,
                                    FALocationNameSrc = (
                                                            from _vMsFALocationType in this.db.V_MsFALocationTypes
                                                            where _vMsFALocationType.Code == _glFAMoveHd.FALocationCodeSrc
                                                            select _vMsFALocationType.Name
                                                        ).FirstOrDefault(),
                                    FALocationTypeDest = _glFAMoveHd.FALocationTypeDest,
                                    FALocationCodeDest = _glFAMoveHd.FALocationCodeDest,
                                    FALocationNameDest = (
                                                            from _vMsFALocationType in this.db.V_MsFALocationTypes
                                                            where _vMsFALocationType.Code == _glFAMoveHd.FALocationCodeDest
                                                            select _vMsFALocationType.Name
                                                        ).FirstOrDefault()
                                }
                            );

                if (_prmOrderBy == "Trans No")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TransNmbr)) : (_query1.OrderByDescending(a => a.TransNmbr));
                if (_prmOrderBy == "File No")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FileNmbr)) : (_query1.OrderByDescending(a => a.FileNmbr));
                if (_prmOrderBy == "Trans Date")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TransDate)) : (_query1.OrderByDescending(a => a.TransDate));
                if (_prmOrderBy == "Status")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Status)) : (_query1.OrderByDescending(a => a.Status));
                if (_prmOrderBy == "Type Source")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FALocationTypeSrc)) : (_query1.OrderByDescending(a => a.FALocationTypeSrc));
                if (_prmOrderBy == "Code Source")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FALocationCodeSrc)) : (_query1.OrderByDescending(a => a.FALocationCodeSrc));
                if (_prmOrderBy == "Name Source")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FALocationNameSrc)) : (_query1.OrderByDescending(a => a.FALocationNameSrc));
                if (_prmOrderBy == "Type Dest")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FALocationTypeDest)) : (_query1.OrderByDescending(a => a.FALocationTypeDest));
                if (_prmOrderBy == "Code Dest")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FALocationCodeDest)) : (_query1.OrderByDescending(a => a.FALocationCodeDest));
                if (_prmOrderBy == "Name Dest")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FALocationNameDest)) : (_query1.OrderByDescending(a => a.FALocationNameDest));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GLFAMoveHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.FALocationTypeSrc, _row.FALocationCodeSrc, _row.FALocationNameSrc, _row.FALocationTypeDest, _row.FALocationCodeDest, _row.FALocationNameDest));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GLFAMoveDt> GetListFixedAssetMovingDt(string _prmCode, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<GLFAMoveDt> _result = new List<GLFAMoveDt>();

            try
            {
                var _query1 = (
                                from _glFAMoveDt in this.db.GLFAMoveDts
                                join _msFixedAsset in this.db.MsFixedAssets
                                    on _glFAMoveDt.FACode equals _msFixedAsset.FACode
                                where _glFAMoveDt.TransNmbr == _prmCode
                                orderby _msFixedAsset.FAName ascending
                                select new
                                {
                                    FACode = _glFAMoveDt.FACode,
                                    FAName = _msFixedAsset.FAName,
                                    Remark = _glFAMoveDt.Remark
                                }
                            );

                if (_prmOrderBy == "Fixed Asset Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FACode)) : (_query1.OrderByDescending(a => a.FACode));
                if (_prmOrderBy == "Fixed Asset Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FAName)) : (_query1.OrderByDescending(a => a.FAName));
                if (_prmOrderBy == "Remark")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Remark)) : (_query1.OrderByDescending(a => a.Remark));

                var _query = _query1;

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { FACode = this._string, FAName = this._string, Remark = this._string });

                    _result.Add(new GLFAMoveDt(_row.FACode, _row.FAName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFixedAssetMoving(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAMoveHd _glFAMoveHd = this.db.GLFAMoveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAMoveHd != null)
                    {
                        if ((_glFAMoveHd.FileNmbr ?? "") == "")
                        {
                            var _query = (from _detail in this.db.GLFAMoveDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLFAMoveDts.DeleteAllOnSubmit(_query);

                            this.db.GLFAMoveHds.DeleteOnSubmit(_glFAMoveHd);

                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
                        }
                    }
                    
                }
                if(_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFixedAssetMovingDt(string[] _prmCode, string _prmTransCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAMoveDt _glFAMoveDt = this.db.GLFAMoveDts.Single(_temp => _temp.FACode.Trim().ToLower() == _prmCode[i].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransCode.Trim().ToLower());

                    this.db.GLFAMoveDts.DeleteOnSubmit(_glFAMoveDt);
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

        public bool DeleteMultiApproveFixedAssetMoving(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAMoveHd _glFAMoveHd = this.db.GLFAMoveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAMoveHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _glFAMoveHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _glFAMoveHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_glFAMoveHd != null)
                    {
                        if ((_glFAMoveHd.FileNmbr ?? "") == "")
                        {
                            var _query = (from _detail in this.db.GLFAMoveDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLFAMoveDts.DeleteAllOnSubmit(_query);

                            this.db.GLFAMoveHds.DeleteOnSubmit(_glFAMoveHd);

                            _result = true;
                        }
                    }
                    else if (_glFAMoveHd.FileNmbr != "" && _glFAMoveHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                    {
                        _glFAMoveHd.Status = TransactionDataMapper.GetStatus(TransStatus.Deleted);
                        _result = true;
                    }
                }

                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleGLFAMoveHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAMoveHd _glFAMoveHd = this.db.GLFAMoveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAMoveHd != null)
                    {
                        if (_glFAMoveHd.Status != TransactionDataMapper.GetStatus(TransStatus.Posted))
                        {
                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFAMoveHd GetSingleFixedAssetMoving(string _prmCode)
        {
            GLFAMoveHd _result = null;

            try
            {
                _result = this.db.GLFAMoveHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFAMoveDt GetSingleFixedAssetMovingDt(string _prmCode, string _prmCodeTrans)
        {
            GLFAMoveDt _result = null;

            try
            {
                _result = this.db.GLFAMoveDts.Single(_temp => _temp.FACode == _prmCode && _temp.TransNmbr == _prmCodeTrans);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddFixedAssetMoving(GLFAMoveHd _prmGLFAMoveHd)
        {
            string _result = "";

            int _year = _prmGLFAMoveHd.TransDate.Year;
            int _period = _prmGLFAMoveHd.TransDate.Month;

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_year, _period, AppModule.GetValue(TransactionType.FAMoving), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmGLFAMoveHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.GLFAMoveHds.InsertOnSubmit(_prmGLFAMoveHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmGLFAMoveHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFixedAssetMovingDt(GLFAMoveDt _prmGLFAMoveDt)
        {
            bool _result = false;

            try
            {

                this.db.GLFAMoveDts.InsertOnSubmit(_prmGLFAMoveDt);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFixedAssetMoving(GLFAMoveHd _prmGLFAMoveHd)
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

        public bool EditFixedAssetMovingDt(GLFAMoveDt _prmGLFAMoveDt)
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

        public string GetApprovalFAMoving(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_GLFAMoveGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FAMoving);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string ApproveFAMoving(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_GLFAMoveApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        GLFAMoveHd _glFAMoveHd = this.GetSingleFixedAssetMoving(_prmCode);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_glFAMoveHd.TransDate.Year, _glFAMoveHd.TransDate.Month, AppModule.GetValue(TransactionType.FAMoving), this._companyTag, ""))
                        {
                            _glFAMoveHd.FileNmbr = _item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FAMoving);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleFixedAssetMoving(_prmCode).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

                        _result = "Approve Success";
                        _scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string PostingFAMoving(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLFAMoveHd _glFAMoveHd = this.db.GLFAMoveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFAMoveHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFAMovePost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FAMoving);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleFixedAssetMoving(_prmCode).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string UnpostingFAMoving(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLFAMoveHd _glFAMoveHd = this.db.GLFAMoveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFAMoveHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFAMoveUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.FAMoving);
                        //_transActivity.TransNmbr = _prmCode.ToString();
                        //_transActivity.FileNmbr = this.GetSingleFixedAssetMoving(_prmCode).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleFixedAssetMoving(_prmCode).TransDate;
                        //_transActivity.Reason = "";

                        //this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        //this.db.SubmitChanges();
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsFixedAsset> GetListDDLFAMoving(string _prmType, string _prmCode)
        {
            List<MsFixedAsset> _result = new List<MsFixedAsset>();

            try
            {
                var _query = from _sGLFAMoveGetDt in this.db.S_GLFAMoveGetDt(_prmType, _prmCode)
                             select new
                             {
                                 FACode = _sGLFAMoveGetDt.FA_Code,
                                 FAName = _sGLFAMoveGetDt.FA_Name + " - " + _sGLFAMoveGetDt.FA_Code
                             };

                foreach (var _row in _query)
                {
                    _result.Add(new MsFixedAsset(_row.FACode, _row.FAName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public V_MsFALocationType GetFALocNameByLocTypeAndCode(char _prmFALocationType, string _prmFALocationCode)
        {
            V_MsFALocationType _result = new V_MsFALocationType();

            try
            {
                _result = this.db.V_MsFALocationTypes.Single(_temp => _temp.Type == _prmFALocationType && _temp.Code == _prmFALocationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~FixedAssetsBL()
        {
        }
    }
}
