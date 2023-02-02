using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSLibrary;

namespace SMS.SMSWeb
{
    public abstract class BlockPhoneNumberBase : SMSWebBase
    {
        protected string _homePage = "BlockPhoneNumber.aspx";
        protected string _addPage = "BlockPhoneNumberAdd.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;
        protected string _codeKey = "code";

        public BlockPhoneNumberBase() { }
        ~BlockPhoneNumberBase() { }
    }
}