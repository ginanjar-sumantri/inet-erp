using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Web;

namespace SMSLibrary
{
    public class RegistrationBL : SMSLibBase
    {
        public RegistrationBL()
        {
        }

        ~RegistrationBL()
        {
        }

        public int GetOrganizationID(String _prmOrganizationName)
        {
            int _result = 0;

            try
            {
                var _query = (from _msOrganization in this.db.MsOrganizations
                              where _msOrganization.OrganizationName == _prmOrganizationName
                              orderby _msOrganization.OrganizationID descending
                              select _msOrganization.OrganizationID
                           ).FirstOrDefault();
                _result = Convert.ToInt32(_query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public int GetSingle(String _prmOrganizationName)
        {
            int _result = 0;

            try
            {
                _result = this.db.MsOrganizations.Count(_temp => _temp.OrganizationName == _prmOrganizationName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public bool AddOrganization(MsOrganization _prmOrganization)
        {
            bool _result = false;
            try
            {
                this.db.MsOrganizations.InsertOnSubmit(_prmOrganization);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public bool AddUser(String _prmCorporateName, MsUser _prmUser, String _prmApplicationPath)
        {
            bool _result = false;
            try
            {
                this.db.MsUsers.InsertOnSubmit(_prmUser);
                this.db.SubmitChanges();

                MailMessage _msgMail = new MailMessage();
                _msgMail.To.Add(new MailAddress(_prmUser.Email));
                _msgMail.From = new MailAddress("websms@webaccess.co.id");
                _msgMail.Subject = "WEBSMS Account Registration Activation";
                _msgMail.IsBodyHtml = true;

                _msgMail.Body = "Thank you for your support. Your account has been created, <br>";
                _msgMail.Body = _msgMail.Body + "Corporate Name : " + _prmCorporateName + "<br>";
                _msgMail.Body = _msgMail.Body + "UserID : " + _prmUser.UserID + "<br>";
                _msgMail.Body = _msgMail.Body + "Password : " + Rijndael.Decrypt(_prmUser.password, ApplicationConfig.EncryptionKey) + "<br>";
                _msgMail.Body = _msgMail.Body + "Please activate it from the following link <a href='" + _prmApplicationPath + "Registrasi/ActivationPage.aspx" + "?" + "code" + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_prmUser.password, ApplicationConfig.EncryptionKey)) + "&" + "user" + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_prmUser.UserID, ApplicationConfig.EncryptionKey)) + "&" + "org" + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_prmUser.OrganizationID.ToString(), ApplicationConfig.EncryptionKey)) + "'>" + 
                    _prmApplicationPath + "Registrasi/ActivationPage.aspx" + "?" + "code" + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_prmUser.password, ApplicationConfig.EncryptionKey)) + "&" + "user" + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_prmUser.UserID, ApplicationConfig.EncryptionKey)) + "&" + "org" + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_prmUser.OrganizationID.ToString(), ApplicationConfig.EncryptionKey)) + "</a>";

                SmtpClient _smtp = new SmtpClient();
                _smtp.Send(_msgMail);

                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public bool RegisterNewAdminUsers(MsUser _prmMsUserWebAdmin, MsUser _prmMsUserAdminApps)
        {
            bool _result = false;
            try
            {
                this.db.MsUsers.InsertOnSubmit(_prmMsUserWebAdmin);
                this.db.MsUsers.InsertOnSubmit(_prmMsUserAdminApps);

                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }


        public Boolean CheckActivationEmail(string _prmOrg, string _prmUserID, string _prmCode)
        {
            Boolean _result = false;

            try
            {
                var _query = (
                                from _msUser in this.db.MsUsers
                                where _msUser.OrganizationID.ToString().Trim() == _prmOrg.Trim()
                                    && _msUser.UserID.Trim() == _prmUserID.Trim()
                                    && _msUser.password.Trim() == _prmCode.Trim()
                                select new
                                {
                                    OrganizationID = _msUser.OrganizationID,
                                    UserID = _msUser.UserID
                                }
                              );

                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public Boolean ActivateUser(string _prmOrg, string _prmUserID)
        {
            Boolean _result = false;

            try
            {
                MsUser _user = this.db.MsUsers.Single(_temp => _temp.OrganizationID.ToString() == _prmOrg && _temp.UserID == _prmUserID);
                _user.fgActive = true;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
    }
}
