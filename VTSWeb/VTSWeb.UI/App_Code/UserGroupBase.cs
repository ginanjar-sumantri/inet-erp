using System;
using VTSWeb.Enum;
using VTSWeb.Common;
using VTSWeb.SystemConfig;

namespace VTSWeb.UI
{
    public abstract class UserGroupBase : System.Web.UI.Page
    {
        protected bool _permission;
        protected bool _status;

        protected String _homePage = "UserGroup.aspx";
        protected String _addPage = "UserGroupAdd.aspx";
        protected String _editPage = "UserGroupEdit.aspx";
        protected String _detailPage = "UserGroupDetail.aspx";
        protected String _detailAddPage = "UserGroupDtAdd.aspx";
        protected String _detailEditPage = "UserGroupDtEdit.aspx";

        protected String _codeKey = "code";
        protected String _itemKey = "item";

        protected String _pageTitleLiteral = "User Group";

        protected NameValueCollectionExtractor _nvcExtractor;

        public UserGroupBase()
        {
        }

        ~UserGroupBase()
        {
        }
    }
}
