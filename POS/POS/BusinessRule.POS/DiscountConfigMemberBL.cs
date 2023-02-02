using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;

namespace BusinessRule.POS
{
    public sealed class DiscountConfigMemberBL : Base
    {
        public DiscountConfigMemberBL()
        {
        }

        #region DiscountConfigMemberBL

        public double RowsCount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Member")
                _pattern2 = "%" + _prmKeyword + "%";

            var _query =
                        (
                             from _POSMsDiscountConfigMembers in this.db.POSMsDiscountConfigMembers
                             where (SqlMethods.Like(_POSMsDiscountConfigMembers.DiscountConfigCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_POSMsDiscountConfigMembers.MemberType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _POSMsDiscountConfigMembers
                        ).Count();

            _result = _query;
            return _result;
        }


        public List<POSMsDiscountConfigMember> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsDiscountConfigMember> _result = new List<POSMsDiscountConfigMember>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Member")
                _pattern2 = "%" + _prmKeyword + "%";

            try
            {
                var _query = (
                                from _POSMsDiscountConfigMembers in this.db.POSMsDiscountConfigMembers
                                where (SqlMethods.Like(_POSMsDiscountConfigMembers.DiscountConfigCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_POSMsDiscountConfigMembers.MemberType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _POSMsDiscountConfigMembers
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                foreach (var _row in _query)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<POSMsDiscountConfigMember> GetList()
        {
            List<POSMsDiscountConfigMember> _result = new List<POSMsDiscountConfigMember>();
            try
            {
                var _query = (
                                from _POSMsDiscountConfigMember in this.db.POSMsDiscountConfigMembers
                                select _POSMsDiscountConfigMember
                            );
                foreach (var _row in _query)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmCode)
        {
            bool _result = false;
            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    POSMsDiscountConfigMember _deleteData = this.db.POSMsDiscountConfigMembers.Single(_temp => _temp.DiscountConfigCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());
                    this.db.POSMsDiscountConfigMembers.DeleteOnSubmit(_deleteData);
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

        public POSMsDiscountConfigMember GetSingle(string _prmCode)
        {
            POSMsDiscountConfigMember _result = null;
            try
            {
                _result = this.db.POSMsDiscountConfigMembers.Single(_temp => _temp.DiscountConfigCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public bool Add(POSMsDiscountConfigMember _prmNewDiscountConfigMember)
        {
            bool _result = false;
            try
            {
                this.db.POSMsDiscountConfigMembers.InsertOnSubmit(_prmNewDiscountConfigMember);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public bool SubmitEdit()
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

        ~DiscountConfigMemberBL()
        {
        }

        public List<MsMemberType> GetListMemberType()
        {
            List<MsMemberType> _result = new List<MsMemberType>();
            var _qry = (
                    from _msMemberType in this.db.MsMemberTypes
                    where _msMemberType.FgActive == 'Y'
                    select _msMemberType
                );
            foreach (var _row in _qry)
                _result.Add(_row);
            return _result;
        }
    }
}
