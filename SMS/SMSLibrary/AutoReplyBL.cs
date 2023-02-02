using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;

namespace SMSLibrary
{
    public class AutoReplyBL : SMSLibBase
    {
        public double RowsCountAutoReply(String _prmCategory, String _prmKeyword, String _prmOrgID)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "KeyWord")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "ReplyMessage")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "PhoneNumber")
            {
                _pattern3 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                            from _trAutoReply in this.db.TrAutoReplies
                            where (SqlMethods.Like(_trAutoReply.KeyWord.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_trAutoReply.ReplyMessage.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like(_trAutoReply.PhoneNumber.Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _trAutoReply.OrganizationID == Convert.ToInt32 ( _prmOrgID)
                            select _trAutoReply
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<TrAutoReply> GetListAutoReply(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrgID)
        {
            List<TrAutoReply> _result = new List<TrAutoReply>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "KeyWord")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "ReplyMessage")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "PhoneNumber")
            {
                _pattern3 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _trAutoReply in this.db.TrAutoReplies
                                where (SqlMethods.Like(_trAutoReply.KeyWord.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_trAutoReply.ReplyMessage.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_trAutoReply.PhoneNumber.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _trAutoReply.OrganizationID == Convert.ToInt32 (_prmOrgID )
                                select _trAutoReply
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public TrAutoReply GetSingleAutoReply(String _prmCode)
        {
            return this.db.TrAutoReplies.Single(a => a.id == Convert.ToInt32( _prmCode) );
        }

        public Boolean AddAutoReply(TrAutoReply _newData)
        {
            Boolean _result = false;
            try
            {
                this.db.TrAutoReplies.InsertOnSubmit(_newData);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean DeleteMultiAutoReply(String[] _prmId) {
            Boolean _result = false;
            try
            {
                for (int i = 0; i < _prmId.Length; i++)
                {
                    TrAutoReply _deleteData = this.db.TrAutoReplies.Single(a => a.id == Convert.ToInt32(_prmId[i]));
                    this.db.TrAutoReplies.DeleteOnSubmit(_deleteData);
                }
                this.db.SubmitChanges();
                _result = true ;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        } 

        public Boolean EditSubmit()
        {
            bool _result = false;
            try
            {
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

    }
}
