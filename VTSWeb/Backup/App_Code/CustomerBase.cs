using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class CustomerBase : System.Web.UI.Page
    {
        protected String _homePage = "Customer.aspx";
        protected String _addPage = "CustomerAdd.aspx";
        protected String _editPage = "CustomerEdit.aspx";
        protected String _viewPage = "CustomerView.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Company";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CustomerBase()
        {
        }

        ~CustomerBase()
        {
        }
    }
}
