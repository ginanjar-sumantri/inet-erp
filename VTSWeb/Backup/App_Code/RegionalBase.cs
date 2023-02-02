using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class RegionalBase : System.Web.UI.Page
    {
        protected String _homePage = "Regional.aspx";
        protected String _addPage = "RegionalAdd.aspx";
        protected String _editPage = "RegionalEdit.aspx";
        
        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Regional";

        protected NameValueCollectionExtractor _nvcExtractor;

        public RegionalBase()
        {
        }

        ~RegionalBase()
        {
        }
    }
}
