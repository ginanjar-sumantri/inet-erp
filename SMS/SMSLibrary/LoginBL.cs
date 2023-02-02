using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSLibrary
{
    public class LoginBL : SMSLibBase
    {
        public LoginBL()
        {
        }

        ~LoginBL()
        {
        }

        public Int16 GetNewInbox(String _prmOrgID, String _prmUserID, String _prmFgAdmin)
        {
            if (Convert.ToBoolean(_prmFgAdmin))
                return Convert.ToInt16(this.db.SMSGatewayReceives.Where(a => a.flagRead == 0 && a.OrganizationID == Convert.ToInt32(_prmOrgID)).Count());
            else
                return Convert.ToInt16(
                    (
                    from _smsGateway in this.db.SMSGatewayReceives
                    join _phoneBook in this.db.MsPhoneBooks
                    on _smsGateway.SenderPhoneNo equals _phoneBook.PhoneNumber
                    where _smsGateway.flagRead == 0
                        && _smsGateway.OrganizationID == Convert.ToInt32(_prmOrgID)
                        && _phoneBook.UserID == _prmUserID
                    select _smsGateway
                    ).Count());
        }

        public Int16 GetNewOutbox(String _prmOrgID, String _prmUserID, String _prmFgAdmin)
        {
            if (Convert.ToBoolean(_prmFgAdmin))
                return Convert.ToInt16(this.db.SMSGatewaySends.Where(a => a.flagSend == 0 && a.OrganizationID == Convert.ToInt32(_prmOrgID)).Count());
            else
                return Convert.ToInt16(this.db.SMSGatewaySends.Where(a => a.flagSend == 0 && a.OrganizationID == Convert.ToInt32(_prmOrgID) && a.userID == _prmUserID).Count());
        }

        public Double GetSingleCorpID(String _prmOrganizationName)
        {
            Double _result = 0;
            try
            {
                var _query = (from _msOrganization in this.db.MsOrganizations
                              where _msOrganization.OrganizationName == _prmOrganizationName
                              select _msOrganization.OrganizationID);

                _result = _query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public bool toLogin(Int32 _prmCorpID, String _prmUserID, String _prmPassword, DateTime _prmDateTime, ref String _prmErrorMsg)
        {
            bool _result = false;

            try
            {
                var _query = (from _MsUser in this.db.MsUsers
                              where _MsUser.UserID == _prmUserID
                              && _MsUser.OrganizationID == _prmCorpID
                              && _MsUser.fgWebAdmin == true
                              select _MsUser).FirstOrDefault();
                if (_query == null)
                {
                    _prmErrorMsg = "Invalid Username or not registered";
                    return _result;
                    //ini sudah kelempar keluar belum
                }
                else
                {
                    var _query1 = (from _MsUser in this.db.MsUsers
                                   where _MsUser.OrganizationID == _prmCorpID
                                   && _MsUser.UserID == _prmUserID
                                   && _MsUser.password == _prmPassword
                                   && _MsUser.fgActive == true
                                   && _MsUser.fgWebAdmin == true
                                   select _MsUser.ExpiredDate).FirstOrDefault();
                    if (_query1 == null)
                    {
                        _prmErrorMsg = "Invalid Password";
                        return _result;
                    }
                    else
                    {
                        if (_query1 <= _prmDateTime)
                        {
                            _prmErrorMsg = "User ID already Expired";
                            return _result;
                        }
                        else
                        {
                            _result = true; //login success
                            return _result;

                        }
                    }

                }


                //var _query1 = (from _msUser in this.db.MsUsers
                //               where _msUser.ExpiredDate >= _prmDateTime
                //               && _msUser.OrganizationID == _prmCorpID
                //               && _msUser.UserID == _prmUserID
                //               && _msUser.password == _prmPassword
                //               && _msUser.fgActive == true
                //               && _msUser.fgAdmin != true
                //               select _msUser.UserID
                //    ).FirstOrDefault();

                //if (_query != null)
                //    _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public bool GetFgWebAdmin(Double _prmCorpID, String _prmUserID, String _prmPassword)
        {
            bool _result = false;

            try
            {
                _result = this.db.MsUsers.Single(_temp => _temp.OrganizationID == _prmCorpID && _temp.UserID == _prmUserID && _temp.password == _prmPassword).fgWebAdmin;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public String getOrganizationName(String _prmOrgID)
        {
            return this.db.MsOrganizations.Single(a => a.OrganizationID == Convert.ToInt32(_prmOrgID)).OrganizationName;
        }
        public String getPackageName(String _prmOrgID, String _prmUserID)
        {
            return this.db.MsUsers.Single(a => a.OrganizationID == Convert.ToInt32(_prmOrgID) && a.UserID == _prmUserID).PackageName;
        }

        public String getDomainName()
        {
            String _result = "";
            var _qry = this.db.MsBackEndSettings.Where(a => a.Description == "webDomainName");
            if (_qry.Count() > 0) _result = _qry.FirstOrDefault().Value;
            return _result;
        }


        public bool SweepSound()
        {
            Boolean _result = false;
            try
            {
                var _qry = (from _smsReceive in this.db.SMSGatewayReceives
                            where _smsReceive.fgSound == null || _smsReceive.fgSound == false
                            select _smsReceive);
                if (_qry.Count() > 0) _result = true;
                foreach (var _row in _qry)
                {
                    SMSGatewayReceive _updateData = this.db.SMSGatewayReceives.Single(a => a.id == _row.id);
                    _updateData.fgSound = true;
                }
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        //public bool toLogin(int p, string p_2, string _password, DateTime dateTime, string _ErrorMsg)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
