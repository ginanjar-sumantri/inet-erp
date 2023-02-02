using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSLibrary;

namespace SMS
{
    public abstract class SMSWebBase : System.Web.UI.Page
    {
        private string _pageTitle = "SMS Portal";
        //protected string _errorPermissionPage = "../ErrorPermission.aspx";

        public SMSWebBase()
        {
            this.Title = this._pageTitle;
        }
    }
}