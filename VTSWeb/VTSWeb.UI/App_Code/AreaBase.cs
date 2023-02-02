using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class AreaBase : System.Web.UI.Page
    {
        protected String _homePage = "Area.aspx";
        protected String _addPage = "AreaAdd.aspx";
        protected String _editPage = "AreaEdit.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Area";

        protected NameValueCollectionExtractor _nvcExtractor;

        public AreaBase()
        {
        }

        ~AreaBase()
        {
        }
    }
}
