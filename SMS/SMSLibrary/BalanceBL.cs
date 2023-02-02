using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Web;
using System.Data.Linq.SqlClient;

namespace SMSLibrary
{
    public class BalanceBL : SMSLibBase
    {
        public BalanceBL()
        {
        }

        ~BalanceBL()
        {
        }

        public double rowCountOrganization(String _prmCategory, String _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "OrganizationName")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Email")
                _pattern2 = "%" + _prmKeyword + "%";

            try
            {
                var _query =
                               (
                              from _msOrganization in this.db.MsOrganizations                              
                              where (SqlMethods.Like(_msOrganization.OrganizationName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like((_msOrganization.Email.Trim() ?? "").ToLower(), _pattern2.Trim().ToLower()))
                              select _msOrganization
                               );
                _result = _query.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public Boolean SubmitEdit() {
            Boolean _result = false;
            try
            {
                this.db.SubmitChanges();
                _result = true ;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public List<MsOrganization> getListOrganization(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsOrganization> _result = new List<MsOrganization>();

            string _pattern1 = "%%", _pattern2 = "%%";

            if (_prmCategory == "OrganizationName")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Email")
                _pattern2 = "%" + _prmKeyword + "%";

            try
            {
                var _query = (from _msOrganization in this.db.MsOrganizations                              
                              where (SqlMethods.Like(_msOrganization.OrganizationName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msOrganization.Email.Trim().ToLower(), _pattern2.Trim().ToLower()))
                              orderby _msOrganization.OrganizationName ascending
                              select _msOrganization
                              ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

    }
}
