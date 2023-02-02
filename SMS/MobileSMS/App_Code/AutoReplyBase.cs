using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSLibrary;

namespace SMS.SMSWeb
{
    public abstract class AutoReplyBase : SMSWebBase
    {
        protected string _homePage = "AutoReply.aspx";
        protected string _addPage = "AutoReplyAdd.aspx";
        protected string _editPage = "AutoReplyEdit.aspx";
        protected string _uploadPage = "AutoReplyUpload.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;
        protected string _codeKey = "code";

        public AutoReplyBase() { }
        ~AutoReplyBase() { }
    }
}