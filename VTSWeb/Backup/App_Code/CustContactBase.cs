using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class CustContactBase : System.Web.UI.Page
    {
        protected String _homePage = "CustContact.aspx";
        protected String _addPage = "CustContactAdd.aspx";
        protected String _editPage = "CustContactEdit.aspx";
        protected String _viewPage = "CustContactView.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Company Contact";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CustContactBase()
        {
        }

        ~CustContactBase()
        {
        }
    }
}
