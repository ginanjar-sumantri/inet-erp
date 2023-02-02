using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;


namespace VTSWeb.UI
{
    public abstract class UserBase : System.Web.UI.Page
    {
        protected bool _permission;

        protected String _homePage = "User.aspx";
        protected String _addPage = "UserAdd.aspx";
        protected String _editPage = "UserEdit.aspx";
        protected String _viewPage = "UserView.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "User";

        protected NameValueCollectionExtractor _nvcExtractor;

        public UserBase()
        {
        }

        ~UserBase()
        {
        }
    }
}
