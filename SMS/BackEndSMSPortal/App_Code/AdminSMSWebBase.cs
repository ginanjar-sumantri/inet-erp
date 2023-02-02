using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSLibrary;

/// <summary>
/// Summary description for AdminSMSWebBase
/// </summary>
namespace SMS
{
    public abstract class AdminSMSWebBase : System.Web.UI.Page
    {
        private string _pageTitle = "SMS Portal";

        public AdminSMSWebBase()
        {
            this.Title = this._pageTitle;
            //
            // TODO: Add constructor logic here
            //
        }
    }
}