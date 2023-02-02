using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class PurposeBase : System.Web.UI.Page
    {
        protected String _homePage = "Purpose.aspx";
        protected String _addPage = "PurposeAdd.aspx";
        protected String _editPage = "PurposeEdit.aspx";
        protected String _viewPage = "PurposeView.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Purpose";

        protected NameValueCollectionExtractor _nvcExtractor;

        public PurposeBase()
        {
         }

        ~PurposeBase()
        {
        }
    }
}
