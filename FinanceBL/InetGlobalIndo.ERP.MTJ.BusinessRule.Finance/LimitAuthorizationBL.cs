using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class LimitAuthorizationBL : Base
    {
        public LimitAuthorizationBL()
        {

        }

        #region LimitAuthorization

        public int RowsCountLimitAuthor
        {
            get
            {
                return this.db.Master_LimitAuthorizations.Count();
            }
        }

        public List<Master_LimitAuthorization> GetListLimitAuthor(int _prmReqPage, int _prmPageSize)
        {
            List<Master_LimitAuthorization> _result = new List<Master_LimitAuthorization>();

            try
            {
                var _query = (
                                from _msLimitAuthor in this.db.Master_LimitAuthorizations
                                join _transType in this.db.MsTransTypes
                                    on _msLimitAuthor.TransTypeCode equals _transType.TransTypeCode
                                select new
                                {
                                    RoleID = _msLimitAuthor.RoleID,
                                    RoleName = new RoleBL().GetRoleNameByCode(_msLimitAuthor.RoleID.ToString()),
                                    TransTypeCode = _msLimitAuthor.TransTypeCode,
                                    TransTypeName = _transType.TransTypeName,
                                    Limit = _msLimitAuthor.Limit * 1000,
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    //var _row = _obj.Template(new { RoleID = this._guid, RoleName = this._string, TransTypeCode = this._string, TransTypeName = this._string, Limit = this._decimal });

                    _result.Add(new Master_LimitAuthorization(_row.RoleID, _row.RoleName, _row.TransTypeCode, _row.TransTypeName, _row.Limit));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Master_LimitAuthorization GetSingleLimitAuthor(Guid _prmRoleID, string _prmTransType)
        {
            Master_LimitAuthorization _result = null;

            try
            {
                var _query = from _limitAuth in this.db.Master_LimitAuthorizations
                             where (_limitAuth.RoleID == _prmRoleID)
                               && (_limitAuth.TransTypeCode == _prmTransType)
                             select new
                             {
                                 Limit = (_limitAuth.Limit * 1000),
                                 RoleID = _limitAuth.RoleID,
                                 TransTypeCode = _limitAuth.TransTypeCode
                             };

                foreach (var _row in _query)
                {
                    _result = new Master_LimitAuthorization();

                    _result.Limit = _row.Limit;
                    _result.RoleID = _row.RoleID;
                    _result.TransTypeCode = _row.TransTypeCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiLimitAuthor(string[] _prmCode)
        {
            bool _result = false;
            string[] _tempSplit = new string[2];

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    _tempSplit = _prmCode[i].Split('=');

                    Master_LimitAuthorization _msLimitAuthor = this.db.Master_LimitAuthorizations.Single(_temp => _temp.RoleID == new Guid(_tempSplit[0]) && _temp.TransTypeCode == _tempSplit[1]);

                    this.db.Master_LimitAuthorizations.DeleteOnSubmit(_msLimitAuthor);
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

        public bool AddLimitAuthor(Master_LimitAuthorization _prmMasterLimitAuthor)
        {
            bool _result = false;

            try
            {
                _prmMasterLimitAuthor.Limit = _prmMasterLimitAuthor.Limit / 1000;

                this.db.Master_LimitAuthorizations.InsertOnSubmit(_prmMasterLimitAuthor);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditLimitAuthor(Master_LimitAuthorization _prmMasterLimitAuthor)
        {
            bool _result = false;

            try
            {
                _prmMasterLimitAuthor.Limit = _prmMasterLimitAuthor.Limit / 1000;

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

        ~LimitAuthorizationBL()
        {

        }
    }
}
