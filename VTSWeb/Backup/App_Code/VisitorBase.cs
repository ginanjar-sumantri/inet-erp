using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class VisitorBase : System.Web.UI.Page
    {
        protected String _homePage = "Visitor.aspx";
        protected String _addPage = "VisitorAdd.aspx";
        protected String _editPage = "VisitorEdit.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Company Area";

        protected NameValueCollectionExtractor _nvcExtractor;

        public VisitorBase()
        {
        }

        ~VisitorBase()
        {
        }
    }
}
