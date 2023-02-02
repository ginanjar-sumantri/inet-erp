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
    public sealed class ClaimBL : Base
    {
        public ClaimBL()
        {

        }

        #region ClaimGroup
        public double RowsCountClaimGroup(string _prmCategory, string _prmKeyword)
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
                    from _msClaimGroup in this.db.HRMMsClaimGroups
                    where (SqlMethods.Like(_msClaimGroup.ClaimGrpCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msClaimGroup.ClaimGrpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msClaimGroup.ClaimGrpCode

                ).Count();

            _result = _query;
            return _result;
        }

        public HRMMsClaimGroup GetSingleClaimGroup(string _prmClaimGroupCode)
        {
            HRMMsClaimGroup _result = null;

            try
            {
                _result = this.db.HRMMsClaimGroups.Single(_emp => _emp.ClaimGrpCode == _prmClaimGroupCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetClaimGroupNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msClaimGroup in this.db.HRMMsClaimGroups
                                where _msClaimGroup.ClaimGrpCode == _prmCode
                                select new
                                {
                                    ClaimGroupName = _msClaimGroup.ClaimGrpName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ClaimGroupName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsClaimGroup> GetListClaimGroup(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsClaimGroup> _result = new List<HRMMsClaimGroup>();
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
                                from _msClaimGroup in this.db.HRMMsClaimGroups
                                where (SqlMethods.Like(_msClaimGroup.ClaimGrpCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msClaimGroup.ClaimGrpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msClaimGroup.EditDate descending
                                select new
                                {
                                    ClaimGroupCode = _msClaimGroup.ClaimGrpCode,
                                    ClaimGroupName = _msClaimGroup.ClaimGrpName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsClaimGroup(_row.ClaimGroupCode, _row.ClaimGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsClaimGroup> GetListClaimGroupForDDL()
        {
            List<HRMMsClaimGroup> _result = new List<HRMMsClaimGroup>();

            try
            {
                var _query = (
                                from _msClaimGroup in this.db.HRMMsClaimGroups
                                orderby _msClaimGroup.EditDate descending
                                select new
                                {
                                    ClaimGroupCode = _msClaimGroup.ClaimGrpCode,
                                    ClaimGroupName = _msClaimGroup.ClaimGrpName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsClaimGroup(_row.ClaimGroupCode, _row.ClaimGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiClaimGroup(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMMsClaimGroup _msClaimGroup = this.db.HRMMsClaimGroups.Single(_temp => _temp.ClaimGrpCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.HRMMsClaimGroups.DeleteOnSubmit(_msClaimGroup);
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

        public bool AddClaimGroup(HRMMsClaimGroup _prmMsClaimGroup)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsClaimGroups.InsertOnSubmit(_prmMsClaimGroup);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditClaimGroup(HRMMsClaimGroup _prmMsClaimGroup)
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

        #region Claim
        public double RowsCountClaim(string _prmCategory, string _prmKeyword)
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
                    from _msClaim in this.db.HRMMsClaims
                    where (SqlMethods.Like(_msClaim.ClaimCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msClaim.ClaimName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msClaim.ClaimCode

                ).Count();

            _result = _query;
            return _result;
        }

        public HRMMsClaim GetSingleClaim(string _prmClaimCode)
        {
            HRMMsClaim _result = null;

            try
            {
                _result = this.db.HRMMsClaims.Single(_emp => _emp.ClaimCode == _prmClaimCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetClaimNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msClaim in this.db.HRMMsClaims
                                where _msClaim.ClaimCode == _prmCode
                                select new
                                {
                                    ClaimName = _msClaim.ClaimName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ClaimName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsClaim> GetListClaim(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsClaim> _result = new List<HRMMsClaim>();
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
                                from _msClaim in this.db.HRMMsClaims
                                where (SqlMethods.Like(_msClaim.ClaimCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msClaim.ClaimName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msClaim.EditDate descending
                                select new
                                {
                                    ClaimCode = _msClaim.ClaimCode,
                                    ClaimName = _msClaim.ClaimName,
                                    ClaimGroup = _msClaim.ClaimGroup,
                                    ClaimGroupName = (
                                                        from _claimGroup in this.db.HRMMsClaimGroups
                                                        where _claimGroup.ClaimGrpCode == _msClaim.ClaimGroup
                                                        select _claimGroup.ClaimGrpName
                                                      ).FirstOrDefault(),
                                    MaxTaken = _msClaim.MaxTaken,
                                    fgCheckPlafon = _msClaim.fgCheckPlafon,
                                    EmployeePercentage = _msClaim.EmployeePercentage,
                                    FamilyPercentage = _msClaim.FamilyPercentage,
                                    ReimbustEmployeePercentage = _msClaim.ReimbustEmployeePercentage,
                                    ReimbustFamilyPercentage = _msClaim.ReimbustFamilyPercentage,
                                    CheckPlafonType = _msClaim.CheckPlafonType
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsClaim(_row.ClaimCode, _row.ClaimName, _row.ClaimGroup, _row.ClaimGroupName, _row.MaxTaken, _row.fgCheckPlafon, _row.EmployeePercentage, _row.FamilyPercentage, _row.ReimbustEmployeePercentage, _row.ReimbustFamilyPercentage, _row.CheckPlafonType));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsClaim> GetListClaimForDDL()
        {
            List<HRMMsClaim> _result = new List<HRMMsClaim>();
           
            try
            {
                var _query = (
                                from _msClaim in this.db.HRMMsClaims
                                orderby _msClaim.EditDate ascending
                                select new
                                {
                                    ClaimCode = _msClaim.ClaimCode,
                                    ClaimName = _msClaim.ClaimName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsClaim(_row.ClaimCode, _row.ClaimName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsClaim> GetListClaimForDDLByGroupBy(String _prmGroupBy)
        {
            List<HRMMsClaim> _result = new List<HRMMsClaim>();

            try
            {
                var _query = (
                                from _msClaim in this.db.HRMMsClaims
                                where _msClaim.ClaimFor == _prmGroupBy || _msClaim.ClaimFor == ClaimForDataMapper.GetClaimFor(ClaimForStatus.All)
                                orderby _msClaim.EditDate ascending
                                select new
                                {
                                    ClaimCode = _msClaim.ClaimCode,
                                    ClaimName = _msClaim.ClaimName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsClaim(_row.ClaimCode, _row.ClaimName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiClaim(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMMsClaim _msClaim = this.db.HRMMsClaims.Single(_temp => _temp.ClaimCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.HRMMsClaims.DeleteOnSubmit(_msClaim);
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

        public bool AddClaim(HRMMsClaim _prmMsClaim)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsClaims.InsertOnSubmit(_prmMsClaim);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditClaim(HRMMsClaim _prmMsClaim)
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

        ~ClaimBL()
        {

        }
    }
}
