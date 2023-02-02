using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class RackCustomerBase : System.Web.UI.Page
    {
        protected String _homePage = "RackCustomer.aspx";
        protected String _addPage = "RackCustomerAdd.aspx";
        protected String _editPage = "RackCustomerEdit.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Rack Customer";

        protected NameValueCollectionExtractor _nvcExtractor;

        public RackCustomerBase()
        {
        }

        ~RackCustomerBase()
        {
        }
    }
}
