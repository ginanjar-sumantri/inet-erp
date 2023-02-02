using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSLibrary;

namespace SMS.SMSWeb
{
    public abstract class ScheduleBase : SMSWebBase
    {
        protected string _homePage = "Schedule.aspx";
        protected string _addPage = "ScheduleAdd.aspx";
        protected string _editPage = "ScheduleEdit.aspx";
        protected string _uploadPage = "ScheduleUpload.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;
        protected string _codeKey = "code";

        public ScheduleBase() { }
        ~ScheduleBase() { }
    }
}