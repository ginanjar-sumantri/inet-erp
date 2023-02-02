using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMS;
using SMSLibrary;

namespace SMS.BackEndSMSPortal
{
    public abstract class PackageBase : AdminSMSWebBase
    {
        protected string _homePage = "Package.aspx";
        protected string _addPage = "PackageAdd.aspx";
        protected string _editPage = "PackageEdit.aspx";

        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        public PackageBase()
        {
        }
        ~PackageBase()
        {
        }
    }
}
