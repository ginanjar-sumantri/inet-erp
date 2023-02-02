using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FASales
{
    public class FASalesBase : AccountingBase
    {
        protected short _menuID = 274;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FASales.aspx";
        protected string _addPage = "FASalesAdd.aspx";
        protected string _editPage = "FASalesEdit.aspx";
        protected string _detailPage = "FASalesDetail.aspx";
        protected string _addDetailPage = "FASalesDetailAdd.aspx";
        protected string _editDetailPage = "FASalesDetailEdit.aspx";
        protected string _viewDetailPage = "FASalesDetailView.aspx";

        protected string _codeKey = "code";
        protected string _codeFA = "FACode";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Fixed Asset Sales";

        public FASalesBase()
        {

        }

        ~FASalesBase()
        {

        }
    }
}