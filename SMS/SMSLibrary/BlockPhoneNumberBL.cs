using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;

namespace SMSLibrary
{
    public class BlockPhoneNumberBL : SMSLibBase
    {
        public BlockPhoneNumberBL() { }
        ~BlockPhoneNumberBL() { }

        public double RowsCountBlockPhoneNumber(String _prmCategory, String _prmKeyword, String _prmOrgID, String _prmUserID)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "PhoneNumber")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                            from _BlockPhoneNumber in this.db.MsBlockPhoneBooks
                            where (SqlMethods.Like(_BlockPhoneNumber.phoneNumber.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && _BlockPhoneNumber.OrganizationID == Convert.ToInt32(_prmOrgID)
                                && _BlockPhoneNumber.UserID == _prmUserID
                            select _BlockPhoneNumber
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MsBlockPhoneBook> GetListBlockPhoneNumber(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrgID, String _prmUserID)
        {
            List<MsBlockPhoneBook> _result = new List<MsBlockPhoneBook>();

            string _pattern1 = "%%";
            
            if (_prmCategory == "PhoneNumber")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _BlockPhoneNumber in this.db.MsBlockPhoneBooks
                                where (SqlMethods.Like(_BlockPhoneNumber.phoneNumber.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && _BlockPhoneNumber.OrganizationID == Convert.ToInt32 ( _prmOrgID )
                                    && _BlockPhoneNumber.UserID == _prmUserID
                                select _BlockPhoneNumber
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

        public MsBlockPhoneBook GetSingleBlockPhoneNumber(String _prmCode)
        {
            return this.db.MsBlockPhoneBooks.Single(a => a.BlockID == Convert.ToInt32( _prmCode) );
        }

        public Boolean AddBlockPhoneNumber(MsBlockPhoneBook _newData)
        {
            Boolean _result = false;
            try
            {
                this.db.MsBlockPhoneBooks.InsertOnSubmit(_newData);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean DeleteMultiBlockPhoneNumber(String[] _prmId) {
            Boolean _result = false;
            try
            {
                for (int i = 0; i < _prmId.Length; i++)
                {
                    MsBlockPhoneBook _deleteData = this.db.MsBlockPhoneBooks.Single(a => a.BlockID == Convert.ToInt32(_prmId[i]));
                    this.db.MsBlockPhoneBooks.DeleteOnSubmit(_deleteData);
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
