using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SMSLibrary
{
    public sealed class TreeViewBL
    {
        public TreeNode Render(String _homeURL)
        {
            TreeNode _result = new TreeNode();

            //_result.ImageUrl = "../images/home_small.jpg";
            //_result.Text = "&nbsp;SMS";
            
            _result.ChildNodes.Add(new TreeNode("Compose", "Compose", _homeURL + "/Images/compose_small.gif", _homeURL + "/Message/Compose.aspx", "_self"));
            _result.ChildNodes.Add(new TreeNode("Contacts", "Contacts", _homeURL + "/Images/contacts_small.gif", _homeURL + "/Message/Contacts.aspx", "_self"));
            _result.ChildNodes.Add(new TreeNode("Groups", "Groups", _homeURL + "/Images/groups_small.gif", _homeURL + "/Message/Groups.aspx", "_self"));
            _result.ChildNodes.Add(new TreeNode("Inbox", "Inbox", _homeURL + "/Images/inbox_small.gif", _homeURL + "/Message/Inbox.aspx", "_self"));
            _result.ChildNodes.Add(new TreeNode("Outbox", "Outbox", _homeURL + "/Images/outbox_small.gif", _homeURL + "/Message/Outbox.aspx", "_self"));
            _result.ChildNodes.Add(new TreeNode("Sent", "Sent", _homeURL + "/Images/sent_small.gif", _homeURL + "/Message/SentItems.aspx", "_self"));
            _result.ChildNodes.Add(new TreeNode("Junk", "Junk", _homeURL + "/Images/junk_small.gif", _homeURL + "/Message/Junk.aspx", "_self"));
            
            return _result;
        }
    }
}
