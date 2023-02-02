using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.ContractTemplate
{
    public abstract class ContractTemplateBase : BillingBase
    {
        protected short _menuID = 2252;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Contract Template";

        protected string _codeKey = "code";
        
        protected string _homePage = "ContractTemplate.aspx";
        protected string _addPage = "ContractTemplateAdd.aspx";
        protected string _editPage = "ContractTemplateEdit.aspx";
        protected string _detailPage = "ContractTemplateDetail.aspx";
        protected string _addDetailPage = "ContractTemplateDetailAdd.aspx";

        public ContractTemplateBase()
        {
        }

        ~ContractTemplateBase()
        {
        }
    }
}