using System;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Web.Security;

namespace InetGlobalIndo.ERP.MTJ.Authentication
{
    public sealed class MembershipService : Base
    {
        private String _appName = ApplicationConfig.MembershipAppName;

        public MembershipService()
        {
            Membership.ApplicationName = this._appName;
        }

        public Boolean RemoveUserFromRoles(String _prmUserName, String[] _prmRoleNames)
        {
            Boolean _result = false;

            try
            {
                //Membership.ApplicationName = this._appName;

                Roles.RemoveUserFromRoles(_prmUserName, _prmRoleNames);

                _result = true;
            }
            catch (Exception ex)
            {
                _result = false;
            }

            return _result;
        }

        public Boolean RemoveUsersFromRole(String[] _prmUserNames, String _prmRoleName)
        {
            Boolean _result = false;

            try
            {
                //Membership.ApplicationName = this._appName;

                Roles.RemoveUsersFromRole(_prmUserNames, _prmRoleName);

                _result = true;
            }
            catch (Exception ex)
            {
                _result = false;
            }

            return _result;
        }

        public Boolean RemoveUsersFromRoles(String[] _prmUserNames, String[] _prmRoleNames)
        {
            Boolean _result = false;

            try
            {
                //Membership.ApplicationName = this._appName;

                Roles.RemoveUsersFromRoles(_prmUserNames, _prmRoleNames);

                _result = true;
            }
            catch (Exception ex)
            {
                _result = false;
            }

            return _result;
        }

        public Boolean RemoveUserFromRole(String _prmUserName, String _prmRoleName)
        {
            Boolean _result = false;

            try
            {
                //Membership.ApplicationName = this._appName;

                Roles.RemoveUserFromRole(_prmUserName, _prmRoleName);

                _result = true;
            }
            catch (Exception ex)
            {
                _result = false;
            }

            return _result;
        }

        public Boolean AssignUserToRole(String _prmUserName, String _prmRoleName)
        {
            Boolean _result = false;

            try
            {
                //Membership.ApplicationName = this._appName;

                Roles.AddUserToRole(_prmUserName, _prmRoleName);

                _result = true;
            }
            catch (Exception ex)
            {
                _result = false;
            }

            return _result;
        }

        public Boolean AssignUserToRoles(String _prmUserName, String[] _prmRoleNames)
        {
            Boolean _result = false;
            try
            {
                //Membership.ApplicationName = this._appName;

                Roles.AddUserToRoles(_prmUserName, _prmRoleNames);

                _result = true;
            }
            catch (Exception ex)
            {
                _result = false;
            }
            return _result;
        }

        public Boolean AssignUsersToRole(String[] _prmUserNames, String _prmRoleName)
        {
            Boolean _result = false;

            //Membership.ApplicationName = this._appName;

            Roles.AddUsersToRole(_prmUserNames, _prmRoleName);

            _result = true;

            return _result;
        }

        public Boolean RemoveRole(String _prmRoleName)
        {
            Boolean _result = false;

            try
            {
                //Membership.ApplicationName = this._appName;

                Roles.DeleteRole(_prmRoleName, true);

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public Boolean CreateRole(String _prmRoleName)
        {
            Boolean _result = false;

            //Membership.ApplicationName = this._appName;

            if (Roles.RoleExists(_prmRoleName) == false)
            {
                Roles.CreateRole(_prmRoleName);

                _result = true;
            }

            return _result;
        }

        public Boolean ChangeEmail(String _prmUserName, String _prmNewEmail)
        {
            Boolean _result = false;

            try
            {
                //Membership.ApplicationName = this._appName;

                MembershipUser _user = Membership.GetUser(_prmUserName);
                _user.Email = _prmNewEmail;

                Membership.UpdateUser(_user);

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String[] GetRolesForUser(string _prmUserName)
        {
            String[] _result;

            //Membership.ApplicationName = this._appName;

            _result = Roles.GetRolesForUser(_prmUserName);

            return _result;
        }

        public Boolean ChangePassword(String _prmUserName, String _prmOldPassword, String _prmNewPassword)
        {
            Boolean _result = false;

            //Membership.ApplicationName = _prmAppName;

            try
            {                
                _result = Membership.Provider.ChangePassword(_prmUserName, _prmOldPassword, _prmNewPassword);                
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public Boolean ChangePasswordQuestionAndAnswer(String _prmUserName, String _prmPassword, String _prmNewPasswordQuestion, String _prmNewPasswordAnswer)
        {
            Boolean _result = false;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.Provider.ChangePasswordQuestionAndAnswer(_prmUserName, _prmPassword, _prmNewPasswordQuestion, _prmNewPasswordAnswer);

            return _result;
        }

        public MembershipUser CreateUser(String _prmUserName, String _prmPassword)
        {
            MembershipUser _result = null;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.CreateUser(_prmUserName, _prmPassword);

            return _result;
        }

        public MembershipUser CreateUser(String _prmUserName, String _prmPassword, String _prmEmail)
        {
            MembershipUser _result = null;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.CreateUser(_prmUserName, _prmPassword, _prmEmail);

            return _result;
        }

        //public MembershipUser CreateUser(String _prmAppName, String _prmUserName, String _prmPassword, String _prmEmail, String _prmPasswordQuestion, String _prmPasswordAnswer, Boolean _prmIsApproved, Object providerUserKey, out MembershipCreateStatus _prmStatus)
        public MembershipUser CreateUser(String _prmUserName, String _prmPassword, String _prmEmail, String _prmPasswordQuestion, String _prmPasswordAnswer, Boolean _prmIsApproved, out MembershipCreateStatus _prmStatus)
        {
            MembershipUser _result = null;

            _result = Membership.CreateUser(_prmUserName, _prmPassword, _prmEmail, _prmPasswordQuestion, _prmPasswordAnswer, _prmIsApproved, out _prmStatus);

            return _result;
        }

        public Boolean DeleteUser(String _prmUsername, Boolean _prmDeleteAllRelatedData)
        {
            Boolean _result = false;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.DeleteUser(_prmUsername, _prmDeleteAllRelatedData);

            return _result;
        }

        public MembershipUserCollection FindUsersByEmail(String _prmEmailToMatch, Int32 _prmPageIndex, Int32 _prmPageSize, out Int32 _prmTotalRecords)
        {
            MembershipUserCollection _result;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.FindUsersByEmail(_prmEmailToMatch, _prmPageIndex, _prmPageSize, out _prmTotalRecords);

            return _result;
        }

        public MembershipUserCollection FindUsersByName(String _prmUserNameToMatch, Int32 _prmPageIndex, Int32 _prmPageSize, out Int32 _prmTotalRecords)
        {
            MembershipUserCollection _result;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.FindUsersByName(_prmUserNameToMatch, _prmPageIndex, _prmPageSize, out _prmTotalRecords);

            return _result;
        }

        public MembershipUserCollection GetAllUsers(Int32 _prmPageIndex, Int32 _prmPageSize, out Int32 _prmTotalRecords)
        {
            MembershipUserCollection _result;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.GetAllUsers(_prmPageIndex, _prmPageSize, out _prmTotalRecords);

            return _result;
        }

        public Int32 GetNumberOfUsersOnline()
        {
            Int32 _result;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.GetNumberOfUsersOnline();

            return _result;
        }

        public String GetPassword(String _prmUsername, String _prmAnswer)
        {
            String _result;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.Provider.GetPassword(_prmUsername, _prmAnswer);

            return _result;
        }

        public MembershipUser GetUser(String _prmUsername, Boolean _prmUserIsOnline)
        {
            MembershipUser _result;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.GetUser(_prmUsername, _prmUserIsOnline);

            return _result;
        }

        public MembershipUser GetUser(Object _prmProviderUserKey, Boolean _prmUserIsOnline)
        {
            MembershipUser _result;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.GetUser(_prmProviderUserKey, _prmUserIsOnline);

            return _result;
        }

        public String GetUserNameByEmail(String _prmEmail)
        {
            String _result;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.GetUserNameByEmail(_prmEmail);

            return _result;
        }

        public String ResetPassword(String _prmUserName, String _prmAnswer)
        {
            String _result;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.Provider.ResetPassword(_prmUserName, _prmAnswer);

            return _result;
        }

        public Boolean UnlockUser(String _prmUserName)
        {
            Boolean _result;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.Provider.UnlockUser(_prmUserName);

            return _result;
        }

        public void UpdateUser(MembershipUser _prmUser)
        {
            //Membership.ApplicationName = _prmAppName;

            Membership.UpdateUser(_prmUser);
        }

        public Boolean ValidateUser(String _prmUserName, String _prmPassword)
        {
            Boolean _result;

            //Membership.ApplicationName = _prmAppName;

            _result = Membership.ValidateUser(_prmUserName, _prmPassword);

            return _result;
        }

        ~MembershipService()
        {
        }
    }
}