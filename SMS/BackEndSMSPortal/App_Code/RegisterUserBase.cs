using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMS;
using SMSLibrary;

namespace SMS.BackEndSMSPortal
{
    public abstract class RegisterUserBase : AdminSMSWebBase
    {
        protected string _registerUserPage = "RegisterUser.aspx";
        protected string _registerUserAddPage = "RegisterUserAdd.aspx";
        protected string _registerUserEditPage = "RegisterUserEdit.aspx";

        protected string _dataKey = "data";
        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        public RegisterUserBase()
        {
        }
        ~RegisterUserBase()
        {
        }
    }
}
