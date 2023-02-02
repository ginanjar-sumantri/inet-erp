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
        protected String _userID, _orgID, _fgAdmin;

        public SMSWebBase()
        {
            this.Title = this._pageTitle;
        }

        public void CekSession()
        {
            if (HttpContext.Current.Session["UserID"] != null && HttpContext.Current.Session["Organization"] != null && HttpContext.Current.Session["FgWebAdmin"] != null)
            {
                _orgID = Session["Organization"].ToString();
                _userID = Session["UserID"].ToString();
                _fgAdmin = Session["FgWebAdmin"].ToString();
                if ((_userID == "") || (_orgID == "") || (_fgAdmin == ""))
                    Response.Redirect("../Login/Login.aspx");

                //if ((HttpContext.Current.Session["UserID"].ToString() == "") || (HttpContext.Current.Session["Organization"].ToString() == "") || (HttpContext.Current.Session["FgWebAdmin"].ToString() == ""))
                //    Response.Redirect("../Login/Login.aspx");
            }
            else Response.Redirect("../Login/Login.aspx");
        }
    }
}