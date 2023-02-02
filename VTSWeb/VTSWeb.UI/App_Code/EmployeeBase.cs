using System;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public abstract class EmployeeBase : System.Web.UI.Page
    {
        protected String _homePage = "Employee.aspx";
        protected String _addPage = "EmployeeAdd.aspx";
        protected String _editPage = "EmployeeEdit.aspx";
        protected String _viewPage = "EmployeeView.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Employee";

        protected NameValueCollectionExtractor _nvcExtractor;

        public EmployeeBase()
        {
        }

        ~EmployeeBase()
        {
        }
    }
}
