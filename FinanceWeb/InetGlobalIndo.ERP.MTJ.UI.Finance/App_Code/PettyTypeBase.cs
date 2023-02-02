using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyType
{
    public abstract class PettyTypeBase : FinanceBase
    {
        protected short _menuID = 58;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "PettyType.aspx";
        protected string _addPage = "PettyTypeAdd.aspx";
        protected string _editPage = "PettyTypeEdit.aspx";
        protected string _viewPage = "PettyTypeView.aspx";

        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Petty Type";

        public PettyTypeBase()
        {

        }

        ~PettyTypeBase()
        {

        }
    }
}