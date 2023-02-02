using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class CompanyTypeBase : System.Web.UI.Page
    {
        protected String _homePage = "CompanyType.aspx";
        protected String _addPage = "CompanyTypeAdd.aspx";
        protected String _editPage = "CompanyTypeEdit.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Company Type";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CompanyTypeBase()
        {
        }

        ~CompanyTypeBase()
        {
        }
    }
}
