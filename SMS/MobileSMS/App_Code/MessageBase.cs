using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSLibrary;
using System.Web.UI.WebControls;

namespace SMS.SMSWeb
{
    public abstract class MessageBase : SMSWebBase
    {
        protected string _composePage = "Compose.aspx";
        protected string _replyViewPage = "ReplyView.aspx";

        protected string _junkPage = "Junk.aspx";
        protected string _junkViewPage = "JunkView.aspx";
        protected string _inboxPage = "Inbox.aspx";
        protected string _inboxViewPage = "InboxView.aspx";
        protected string _outboxPage = "Outbox.aspx";
        protected string _outboxViewPage = "OutboxView.aspx";
        protected string _sentItemsPage = "SentItems.aspx";
        protected string _sentItemsViewPage = "SentItemsView.aspx";

        protected string _contactsPage = "Contacts.aspx";
        protected string _contactsAddPage = "ContactsAdd.aspx";
        protected string _contactsViewPage = "ContactsView.aspx";
        protected string _contactsEditPage = "ContactsEdit.aspx";
        protected string _contactsUploadPage = "ContactsUpload.aspx";

        protected string _phoneGroupPage = "Groups.aspx";
        protected string _phoneGroupAddPage = "GroupsAdd.aspx";
        protected string _phoneGroupViewPage = "GroupsView.aspx";
        protected string _phoneGroupEditPage = "GroupsEdit.aspx";

        //protected string _errorPermissionPage = ApplicationConfig.SMSWebAppURL + "ErrorPermission.aspx";

        protected string _pageTitleLiteral = "SMS Portal";

        protected string _dataKey = "data";
        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        public MessageBase()
        {
        }
        ~MessageBase()
        {
        }
    }
}