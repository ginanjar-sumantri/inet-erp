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
    public sealed class ShiftGroupBL : Base
    {
        public ShiftGroupBL()
        {

        }

        #region ShiftGroup
        public double RowsCountShiftGroup(string _prmCategory, string _prmKeyword)
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
                                from _msShiftGroup in this.db.HRMMsShiftGroups
                                join _msShift in this.db.HRMMsShifts
                                    on _msShiftGroup.ShiftCode equals _msShift.ShiftCode
                                where (SqlMethods.Like(_msShiftGroup.ShiftGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msShift.ShiftName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _msShiftGroup.ShiftGroup
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public double RowsCountShiftGroup(String _prmShiftGroupCode)
        {
            double _result = 0;

            try
            {
                var _query =
                            (
                                from _msShiftGroup in this.db.HRMMsShiftGroups
                                join _msShift in this.db.HRMMsShifts
                                    on _msShiftGroup.ShiftCode equals _msShift.ShiftCode
                                where _msShiftGroup.ShiftGroup == _prmShiftGroupCode
                                select _msShiftGroup.ShiftGroup
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMMsShiftGroup GetSingleShiftGroup(string _prmShiftGroupCode)
        {
            HRMMsShiftGroup _result = null;

            try
            {
                _result = this.db.HRMMsShiftGroups.Single(_emp => _emp.ShiftGroup == _prmShiftGroupCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMMsShiftGroup GetTop1ShiftGroup()
        {
            HRMMsShiftGroup _result = null;

            try
            {
                _result = this.db.HRMMsShiftGroups.Take(1).Single();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public string GetShiftGroupNameByCode(string _prmCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        var _query = (
        //                        from _msShiftGroup in this.db.HRMMsShiftGroups
        //                        where _msShiftGroup.ShiftGroup == _prmCode
        //                        select new
        //                        {
        //                            ShiftGroupName = _msShiftGroup.ShiftGroupName
        //                        }
        //                      );

        //        foreach (var _obj in _query)
        //        {
        //            _result = _obj.ShiftGroupName;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public List<HRMMsShiftGroup> GetListShiftGroup()
        {
            List<HRMMsShiftGroup> _result = new List<HRMMsShiftGroup>();

            try
            {
                var _query = (
                                from _msShiftGroup in this.db.HRMMsShiftGroups
                                orderby _msShiftGroup.ShiftGroup ascending
                                select new
                                {
                                    ShiftGroup = _msShiftGroup.ShiftGroup
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsShiftGroup(_row.ShiftGroup));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsShiftGroup> GetListShiftGroup(int _prmReqPage, int _prmPageSize, String _prmShiftGroupCode)
        {
            List<HRMMsShiftGroup> _result = new List<HRMMsShiftGroup>();

            try
            {
                var _query = (
                                from _msShiftGroup in this.db.HRMMsShiftGroups
                                join _msShift in this.db.HRMMsShifts
                                    on _msShiftGroup.ShiftCode equals _msShift.ShiftCode
                                where _msShiftGroup.ShiftGroup == _prmShiftGroupCode
                                orderby _msShiftGroup.ShiftGroup ascending
                                select new
                                {
                                    ShiftGroup = _msShiftGroup.ShiftGroup,
                                    ShiftCode = _msShiftGroup.ShiftCode,
                                    ShiftName = _msShift.ShiftName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsShiftGroup(_row.ShiftGroup, _row.ShiftCode, _row.ShiftName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<HRMMsShiftGroup> GetListShiftGroupForDDL()
        //{
        //    List<HRMMsShiftGroup> _result = new List<HRMMsShiftGroup>();
           
        //    try
        //    {
        //        var _query = (
        //                        from _msShiftGroup in this.db.HRMMsShiftGroups
        //                        orderby _msShiftGroup.ShiftGroup ascending
        //                        select new
        //                        {
        //                            ShiftGroupCode = _msShiftGroup.ShiftGroupCode,
        //                            ShiftGroupName = _msShiftGroup.ShiftGroupName
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new HRMMsShiftGroup(_row.ShiftGroupCode, _row.ShiftGroupName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool DeleteMultiShiftGroup(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    String[] _tempSplit = _prmCode[i].Split('=');

                    HRMMsShiftGroup _msShiftGroup = this.db.HRMMsShiftGroups.Single(_temp => _temp.ShiftGroup.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.ShiftCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower());

                    this.db.HRMMsShiftGroups.DeleteOnSubmit(_msShiftGroup);
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

        public bool AddShiftGroup(HRMMsShiftGroup _prmMsShiftGroup)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsShiftGroups.InsertOnSubmit(_prmMsShiftGroup);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditShiftGroup(HRMMsShiftGroup _prmMsShiftGroup)
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

        ~ShiftGroupBL()
        {

        }
    }
}
