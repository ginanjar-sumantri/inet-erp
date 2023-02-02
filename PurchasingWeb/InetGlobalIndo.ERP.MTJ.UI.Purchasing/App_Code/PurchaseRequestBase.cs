using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRequest
{
    public abstract class PurchaseRequestBase : PurchasingBase
    {
        protected short _menuID = 524;
        protected short _menuIDApproveXML = 1804;
        protected short _menuIDCompileXML = 1805;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "PurchaseRequest.aspx";
        protected string _detailPage = "PurchaseRequestDetail.aspx";
        protected string _editPage = "PurchaseRequestEdit.aspx";
        protected string _addPage = "PurchaseRequestAdd.aspx";
        protected string _addDetailPage = "PurchaseRequestDetailAdd.aspx";
        protected string _editDetailPage = "PurchaseRequestDetailEdit.aspx";
        protected string _viewDetailPage = "PurchaseRequestDetailView.aspx";
        protected string _importPage = "PurchaseRequestImport.aspx";
        protected string _approvePage = "PurchaseRequestXMLApprove.aspx";
        protected string _compilePage = "PurchaseRequestXMLCompile.aspx";
        protected string _detailCompilePage = "PurchaseRequestXMLCompileDetail.aspx";
        protected string _editCompilePage = "PurchaseRequestXMLCompileEdit.aspx";
        protected string _addCompilePage = "PurchaseRequestXMLCompileAdd.aspx";
        protected string _addCompileDetailPage = "PurchaseRequestXMLCompileDetailAdd.aspx";

        protected string _codeKey = "code";
        protected string _codeItem = "CodeItem";

        protected string _pageTitleLiteral = "Purchase Request";
        protected string _pageTitleLiteralXMLList = "Purchase Request XML List";
        protected string _pageCompileTitleLiteral = "Purchase Request XML Compile";

        protected NameValueCollectionExtractor _nvcExtractor;

        public PurchaseRequestBase()
        {

        }

        ~PurchaseRequestBase()
        {

        }
    }
}