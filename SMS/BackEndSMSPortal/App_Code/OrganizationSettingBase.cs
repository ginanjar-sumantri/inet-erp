using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMS;
using SMSLibrary;

namespace SMS.BackEndSMSPortal
{
    public abstract class OrganizationSettingBase : AdminSMSWebBase
    {
        protected string _homePage = "OrganizationSetting.aspx";
        protected string _editPage = "OrganizationSettingEdit.aspx";

        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        public OrganizationSettingBase()
        {
        }
        ~OrganizationSettingBase()
        {
        }
    }
}
