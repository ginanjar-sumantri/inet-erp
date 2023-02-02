using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class RackServerBase : System.Web.UI.Page
    {
        protected String _homePage = "RackServer.aspx";
        protected String _addPage = "RackServerAdd.aspx";
        protected String _editPage = "RackServerEdit.aspx";
        protected String _viewPage = "RackServerView.aspx";
        protected String _AddDetail = "RackServerDetailAdd.aspx";


        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "RackServer";

        protected NameValueCollectionExtractor _nvcExtractor;

        public RackServerBase()
        {
         }

        ~RackServerBase()
        {
        }
    }
}
