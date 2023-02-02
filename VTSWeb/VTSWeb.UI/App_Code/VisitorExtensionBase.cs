using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class VisitorExtensionBase : System.Web.UI.Page
    {
        protected String _homePage = "VisitorExtension.aspx";
        protected String _addPage = "VisitorExtensionAdd.aspx";
        protected String _editPage = "VisitorExtensionEdit.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Visitor Contact Extension";

        protected NameValueCollectionExtractor _nvcExtractor;

        public  VisitorExtensionBase()
        {
        }

        ~VisitorExtensionBase()
        {
        }
    }
}
