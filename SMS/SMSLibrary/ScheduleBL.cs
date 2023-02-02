using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;

namespace SMSLibrary
{
    public class ScheduleBL : SMSLibBase
    {
        public double RowsCountSchedule(String _prmCategory, String _prmKeyword,String _prmOrgID, String _prmUserID)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Message")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "DestinationPhoneNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                            from _trSchedule in this.db.TrSchedules
                            where (SqlMethods.Like(_trSchedule.Message.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_trSchedule.DestinationPhoneNumber.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && _trSchedule.OrganizationID == Convert.ToInt32 (_prmOrgID )
                               && _trSchedule.UserID == _prmUserID
                            select _trSchedule
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<TrSchedule> GetListSchedule(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrgID, String _prmUserID)
        {
            List<TrSchedule> _result = new List<TrSchedule>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Message")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "DestinationPhoneNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _trSchedule in this.db.TrSchedules
                                where (SqlMethods.Like(_trSchedule.Message.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_trSchedule.DestinationPhoneNumber.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _trSchedule.OrganizationID == Convert.ToInt32 (_prmOrgID )
                                   && _trSchedule.UserID == _prmUserID
                                select _trSchedule
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

        public TrSchedule GetSingleSchedule(String _prmCode)
        {
            return this.db.TrSchedules.Single(a => a.id == Convert.ToInt32( _prmCode) );
        }

        public Boolean AddSchedule(TrSchedule _newData, SMSGatewaySend _newQueue)
        {
            Boolean _result = false;
            try
            {
                this.db.TrSchedules.InsertOnSubmit(_newData);
                this.db.SMSGatewaySends.InsertOnSubmit(_newQueue);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean DeleteMultiSchedule(String[] _prmId)
        {
            Boolean _result = false;
            try
            {
                for (int i = 0; i < _prmId.Length; i++)
                {
                    TrSchedule _deleteData = this.db.TrSchedules.Single(a => a.id == Convert.ToInt32(_prmId[i]));
                    this.db.TrSchedules.DeleteOnSubmit(_deleteData);
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
