using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class ReligionBase : System.Web.UI.Page
    {
        protected String _homePage = "Religion.aspx";
        protected String _addPage = "ReligionAdd.aspx";
        protected String _editPage = "ReligionEdit.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Religion";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ReligionBase()
        {
        }

        ~ReligionBase()
        {
        }
    }
}
