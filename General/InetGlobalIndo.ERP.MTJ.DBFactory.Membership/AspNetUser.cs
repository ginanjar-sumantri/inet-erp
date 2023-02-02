using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class aspnet_User
    {
        DateTime _lastLoginDate;
        string _password = "";
        string _email = "";
        string _pwdquestion = "";
        string _pwdanswer = "";

        public aspnet_User(Guid _prmUserId, string _prmUserName, string _prmPassword, string _prmEmail, string _prmPasswordQuestion, string _prmPasswordAnswer, DateTime _prmLastLoginDate)
        {
            this.UserId = _prmUserId;
            this.UserName = _prmUserName;
            this.Password = _prmPassword;
            this.Email = _prmEmail;
            this.PasswordQuestion = _prmPasswordQuestion;
            this.PasswordAnswer = _prmPasswordAnswer;
            this.LastLoginDate = _prmLastLoginDate;
        }

        public aspnet_User(Guid _prmUserId, string _prmUserName, string _prmEmail, DateTime _prmLastLoginDate)
        {
            this.UserId = _prmUserId;
            this.UserName = _prmUserName;
            this.Email = _prmEmail;
            this.LastLoginDate = _prmLastLoginDate;
        }

        public aspnet_User(Guid _prmUserId, string _prmUserName, string _prmEmail, DateTime _prmLastLoginDate, Boolean _prmIsLockedOut)
        {
            this.UserId = _prmUserId;
            this.UserName = _prmUserName;
            this.Email = _prmEmail;
            this.LastLoginDate = _prmLastLoginDate;
            this.IsLockedOut = _prmIsLockedOut;
        }

        public aspnet_User(Guid _prmUserId, string _prmUserName)
        {
            this.UserId = _prmUserId;
            this.UserName = _prmUserName;
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public string PasswordQuestion
        {
            get
            {
                return this._pwdquestion;
            }
            set
            {
                this._pwdquestion = value;
            }
        }

        public string PasswordAnswer
        {
            get
            {
                return this._pwdanswer;
            }
            set
            {
                this._pwdanswer = value;
            }
        }

        public DateTime LastLoginDate
        {
            get
            {
                return this._lastLoginDate;
            }
            set
            {
                this._lastLoginDate = value;
            }
        }

        public Boolean IsLockedOut { get; set; }
    }
}
