using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSLibrary;

/// <summary>
/// Summary description for ManageUserBase
/// </summary>
namespace SMS.SMSWeb
{
    public class ManageUserBase : SMSWebBase
    {
        protected string _ManageUserHome = "ManageUser.aspx";
        protected string _ManageUserEdit = "ManageUserEdit.aspx";
        protected string _ManageUserAdd = "ManageUserAdd.aspx";

        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ManageUserBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
