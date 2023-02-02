using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class CommentStatusBL : Base
    {
        public CommentStatusBL()
        {
        }

        #region CommentStatus
        public double RowsCountCommentStatus(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "CommentStatusName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                            from _hrm_Master_CommentStatus in this.db.HRM_Master_CommentStatus
                            where (SqlMethods.Like(_hrm_Master_CommentStatus.CommentStatusName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            select _hrm_Master_CommentStatus.CommentStatusCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public HRM_Master_CommentStatus GetSingle(Guid _prmCommentStatusCode)
        {
            HRM_Master_CommentStatus _result = null;

            try
            {
                _result = this.db.HRM_Master_CommentStatus.Single(_temp => _temp.CommentStatusCode == _prmCommentStatusCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCommentStatusNameByCode(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _hrm_Master_CommentStatus in this.db.HRM_Master_CommentStatus
                                where _hrm_Master_CommentStatus.CommentStatusCode == _prmCode
                                select new
                                {
                                    CommentStatusName = _hrm_Master_CommentStatus.CommentStatusName
                                }
                            );

                foreach (var _obj in _query)
                {
                    _result = _obj.CommentStatusName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_Master_CommentStatus> GetListCommentStatus(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_Master_CommentStatus> _result = new List<HRM_Master_CommentStatus>();

            string _pattern1 = "%%";

            if (_prmCategory == "CommentStatusName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _processType in this.db.HRM_Master_CommentStatus
                                orderby _processType.EditDate descending
                                where (SqlMethods.Like(_processType.CommentStatusName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                select new
                                {
                                    CommentStatusCode = _processType.CommentStatusCode,
                                    CommentStatusName = _processType.CommentStatusName,
                                    Description = _processType.Description
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_Master_CommentStatus(_row.CommentStatusCode, _row.CommentStatusName, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_Master_CommentStatus> GetListCommentStatusForDDL()
        {
            List<HRM_Master_CommentStatus> _result = new List<HRM_Master_CommentStatus>();

            try
            {
                var _query = (
                                from _processType in this.db.HRM_Master_CommentStatus
                                orderby _processType.EditDate descending
                                select new
                                {
                                    CommentStatusCode = _processType.CommentStatusCode,
                                    CommentStatusName = _processType.CommentStatusName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_Master_CommentStatus(_row.CommentStatusCode, _row.CommentStatusName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(HRM_Master_CommentStatus _prmHRM_Master_CommentStatus)
        {
            bool _result = false;

            try
            {
                if (this.IsCommentStatusNameExists(_prmHRM_Master_CommentStatus.CommentStatusName, _prmHRM_Master_CommentStatus.CommentStatusCode) == false)
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

        private bool IsCommentStatusNameExists(string _prmCommentStatusName, Guid _prmCommentStatusCode)
        {
            bool _result = false;

            try
            {
                var _query = from _processType in this.db.HRM_Master_CommentStatus
                             where (_processType.CommentStatusName == _prmCommentStatusName) && (_processType.CommentStatusCode != _prmCommentStatusCode)
                             select new
                             {
                                 _processType.CommentStatusName
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

        public bool Add(HRM_Master_CommentStatus _prmHRM_Master_CommentStatus)
        {
            bool _result = false;

            try
            {
                if (this.IsCommentStatusNameExists(_prmHRM_Master_CommentStatus.CommentStatusName, _prmHRM_Master_CommentStatus.CommentStatusCode) == false)
                {
                    this.db.HRM_Master_CommentStatus.InsertOnSubmit(_prmHRM_Master_CommentStatus);

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

        public bool DeleteMulti(string[] _prmCommentStatusCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCommentStatusCode.Length; i++)
                {
                    HRM_Master_CommentStatus _hrm_Master_CommentStatus = this.db.HRM_Master_CommentStatus.Single(_processType => _processType.CommentStatusCode == new Guid(_prmCommentStatusCode[i]));

                    this.db.HRM_Master_CommentStatus.DeleteOnSubmit(_hrm_Master_CommentStatus);
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

        ~CommentStatusBL()
        {
        }
    }
}
