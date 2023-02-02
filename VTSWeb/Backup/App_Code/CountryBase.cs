using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class CountryBase : System.Web.UI.Page
    {
        protected String _homePage = "Country.aspx";
        protected String _addPage = "CountryAdd.aspx";
        protected String _editPage = "CountryEdit.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Country";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CountryBase()
        {
        }

        ~CountryBase()
        {
        }
    }
}