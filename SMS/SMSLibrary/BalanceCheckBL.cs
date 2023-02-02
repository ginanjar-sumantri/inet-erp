using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSLibrary
{
    public class BalanceCheckBL : SMSLibBase
    {
        public BalanceCheckBL(){}

        ~BalanceCheckBL() { }

        public void BeginGetwayStatusCheck(String _prmOrgID) {
            MsOrganization _currOrg = this.db.MsOrganizations.Single(a => a.OrganizationID == Convert.ToInt32(_prmOrgID));
            _currOrg.GatewayStatus = false;
            this.db.SubmitChanges();
        }
        public Boolean GatewayStatusCheck(String _prmOrgID) {
            return Convert.ToBoolean ( this.db.MsOrganizations.Single(a => a.OrganizationID == Convert.ToInt32(_prmOrgID)).GatewayStatus );
        }
        public String BalanceCheckCode(String _prmSessionOrgID) {
            return this.db.MsOrganizations.Single(a => a.OrganizationID == Convert.ToInt32(_prmSessionOrgID)).BalanceCheckCode;
        }
        public void RequestBalanceInfo(String _prmSessionOrgID, String _prmBalanceCheckCode ) {
            try
            {
                MsOrganization _currOrg = this.db.MsOrganizations.Single(a => a.OrganizationID == Convert.ToInt32(_prmSessionOrgID));
                _currOrg.BalanceCheckCode = _prmBalanceCheckCode;
                _currOrg.BalanceCheckRequest = true;
                _currOrg.LastBalance = "";
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public String ReadBalanceResponse(String _prmSessionOrgID) {
            String _result = "";
            MsOrganization _msOrg = this.db.MsOrganizations.Single(a => a.OrganizationID == Convert.ToInt32(_prmSessionOrgID));
            _result = _msOrg.LastBalance;
            if (_result != "") {
                _msOrg.BalanceCheckRequest = false;
                this.db.SubmitChanges();
            }
            return _result;
        }

        public Decimal GetMaskingBalance(int _OrganizationID)
        {
            Decimal _result = 0;
            try
            {
                _result = Convert.ToDecimal( this.db.MsOrganizations.Single(a => a.OrganizationID == _OrganizationID).MaskingBalanceAccount );
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

    }
}
