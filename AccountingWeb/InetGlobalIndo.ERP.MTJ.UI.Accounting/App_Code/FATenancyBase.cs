using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FATenancy
{
    public abstract class FATenancyBase : AccountingBase
    {
        protected short _menuID = 279;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FATenancy.aspx";
        protected string _addPage = "FATenancyAdd.aspx";
        protected string _editPage = "FATenancyEdit.aspx";
        protected string _viewPage = "FATenancyDetail.aspx";
        protected string _detailPage = "FATenancyDetail.aspx";
        protected string _addDetailPage = "FATenancyDetailAdd.aspx";
        protected string _editDetailPage = "FATenancyDetailEdit.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _codeKey = "code";
        protected string _codeFA = "FACode";

        protected string _pageTitleLiteral = "Fixed Asset Lease Out";

        public FATenancyBase()
        {

        }

        ~FATenancyBase()
        {

        }
    }
}