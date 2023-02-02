using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class CityBase : System.Web.UI.Page
    {
        protected String _homePage = "City.aspx";
        protected String _addPage = "CityAdd.aspx";
        protected String _editPage = "CityEdit.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "City";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CityBase()
        {
        }

        ~CityBase()
        {
        }
    }
}
