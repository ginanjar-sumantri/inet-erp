using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Web;
using System.Data.Linq.SqlClient;

namespace SMSLibrary
{
    public class OrganizationSettingBL : SMSLibBase
    {
        public OrganizationSettingBL()
        {
        }

        ~OrganizationSettingBL()
        {
        }

        public double rowCountOrganization(String _prmCategory, String _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "OrganizationName")
                _pattern1 = "%" + _prmKeyword + "%";

            try
            {
                var _query =
                               (
                              from _msOrganization in this.db.MsOrganizations                              
                              where (SqlMethods.Like(_msOrganization.OrganizationName.Trim().ToLower(), _pattern1.Trim().ToLower()))
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

        public MsOrganization GetSingle(String _prmOrganizationID)
        {
            MsOrganization _result = new MsOrganization();

            try
            {
                _result = this.db.MsOrganizations.Single (_temp => _temp.OrganizationID == Convert.ToInt32(_prmOrganizationID ));
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

            string _pattern1 = "%%";

            if (_prmCategory == "OrganizationName")
                _pattern1 = "%" + _prmKeyword + "%";

            try
            {
                var _query = (from _msOrganization in this.db.MsOrganizations                              
                              where (SqlMethods.Like(_msOrganization.OrganizationName.Trim().ToLower(), _pattern1.Trim().ToLower()))
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

        public List<MsOrganization> getListOrganization() {
            List<MsOrganization> _result = new List<MsOrganization>();
            try
            {
                var _qry = (
                        from _msOrganization in this.db.MsOrganizations                        
                        orderby _msOrganization.GatewayStatus
                        orderby _msOrganization.FgHosted descending
                        select _msOrganization
                    );
                foreach (var _row in _qry)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }
        public void Leverage(string p)
        {
            try
            {
                if (p == "ATTENTION")
                {
                    var _qry = (from _msOrganization in this.db.MsOrganizations select _msOrganization);
                    foreach (var _row in _qry)
                    {
                        String _tmpOrgName = _row.OrganizationName;
                        MsOrganization _updateData = this.db.MsOrganizations.Single(a => a.OrganizationID == _row.OrganizationID);
                        _updateData.GatewayStatus = false;
                        _updateData.OrganizationName = "";
                    }
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean NeedAttention() {
            Boolean _result = false;
            try
            {
                var _qry = (
                        from _msOrg in this.db.MsOrganizations
                        where _msOrg.GatewayStatus == false
                            && _msOrg.FgHosted == true
                        select _msOrg
                    );
                if ( _qry.Count() > 0 )
                    _result = true ;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }
        public void SweepGateway() {
            try
            {
                var _qry = (
                        from _msOrganization in this.db.MsOrganizations
                        select _msOrganization
                    );
                foreach (MsOrganization _row in _qry)
                {
                    MsOrganization _updateDate = this.db.MsOrganizations.Single (a=>a.OrganizationID == _row.OrganizationID );
                    _updateDate.GatewayStatus = false;
                }
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}
