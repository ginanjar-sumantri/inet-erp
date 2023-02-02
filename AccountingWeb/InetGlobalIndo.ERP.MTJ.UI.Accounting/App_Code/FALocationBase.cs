using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FALocation
{
    public abstract class FALocationBase : AccountingBase
    {
        protected short _menuID = 40;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FALocation.aspx";
        protected string _addPage = "FALocationAdd.aspx";
        protected string _editPage = "FALocationEdit.aspx";
        protected string _viewPage = "FALocationView.aspx";

        protected string _FALocationCodeKey = "FALocationCode";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Fixed Asset Location";

        public FALocationBase()
        {
        }

        ~FALocationBase()
        {
        }
    }
}