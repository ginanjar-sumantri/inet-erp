using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Home.Religion
{
    public abstract class ReligionBase : HomeBase
    {
        protected short _menuID = 14;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";

        protected string _homePage = "Religion.aspx";
        protected string _addPage = "ReligionAdd.aspx";
        protected string _editPage = "ReligionEdit.aspx";
        protected string _viewPage = "ReligionView.aspx";

        protected string _pageTitleLiteral = "Religion";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ReligionBase()
        {

        }

        ~ReligionBase()
        {

        }
    }
}