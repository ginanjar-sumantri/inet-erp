using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Web;

namespace SMSLibrary
{
    public class ConfigureBL : SMSLibBase
    {
        public ConfigureBL()
        {
        }

        ~ConfigureBL()
        {
        }

        public String getAdminPhoneNumber(String _prmOrgId)
        {
            String _result = "";
            var _qry = (from _msOrganization in this.db.MsOrganizations where _msOrganization.OrganizationID == Convert.ToInt32(_prmOrgId ) select _msOrganization ) ;
            if (_qry.Count() > 0)
                _result = _qry.FirstOrDefault().GatewayStatusNoticeNumber;
            return _result;
        }

        public String getAdminEmail(String _prmOrgId)
        {
            String _result = "";
            var _qry = (from _msOrganization in this.db.MsOrganizations where _msOrganization.OrganizationID == Convert.ToInt32(_prmOrgId) select _msOrganization);
            if (_qry.Count() > 0)
                _result = _qry.FirstOrDefault().Email;
            return _result;
        }


        public void EditSubmit()
        {
            try
            {                
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public String getGlobalAutoReply(String _orgID)
        {
            String _result = "";
            try
            {
                var _qry = (
                        from _msOrganization in this.db.MsOrganizations 
                        where _msOrganization.OrganizationID == Convert.ToInt32(_orgID)
                        select _msOrganization
                    );
                if (_qry.Count() > 0)
                    _result = _qry.FirstOrDefault().GlobalReplyMessage;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public String getFooterAdditionalMessage(String _orgID)
        {
            String _result = "";
            try
            {
                var _qry = (
                        from _msOrganization in this.db.MsOrganizations 
                        where _msOrganization.OrganizationID == Convert.ToInt32(_orgID)
                        select _msOrganization
                    );
                if (_qry.Count() > 0)
                    _result = _qry.FirstOrDefault().FooterAdditionalMessage;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public MsOrganization getSingleOrganization(String _orgID)
        {
            MsOrganization _result = null ;
            var _qry = (
                    from _msOrganization in this.db.MsOrganizations 
                    where _msOrganization.OrganizationID == Convert.ToInt32 ( _orgID ) 
                    select _msOrganization
                );
            if (_qry.Count() > 0)
                _result = this.db.MsOrganizations.Single(a=>a.OrganizationID == Convert.ToInt32(_orgID));
            return _result;
        }
    }
}
