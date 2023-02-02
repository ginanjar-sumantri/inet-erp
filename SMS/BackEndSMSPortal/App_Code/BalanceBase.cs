using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMS;
using SMSLibrary;

namespace SMS.BackEndSMSPortal
{
    public abstract class BalanceBase : AdminSMSWebBase
    {
        protected string _homePage = "Balance.aspx";
        protected string _editPage = "BalanceEdit.aspx";

        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        public BalanceBase()
        {
        }
        ~BalanceBase()
        {
        }
    }
}
