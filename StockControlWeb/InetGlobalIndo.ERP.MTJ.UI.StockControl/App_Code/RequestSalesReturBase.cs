using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.RequestSalesRetur
{
    public abstract class RequestSalesReturBase : StockControlBase
    {
        protected short _menuID = 1430;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";
        
        protected string _homePage = "RequestSalesRetur.aspx";
        protected string _addPage = "RequestSalesReturAdd.aspx";
        protected string _editPage = "RequestSalesReturEdit.aspx";
        protected string _detailPage = "RequestSalesReturDetail.aspx";
        protected string _addDetailPage = "RequestSalesReturDetailAdd.aspx";
        protected string _editDetailPage = "RequestSalesReturDetailEdit.aspx";
        protected string _viewDetailPage = "RequestSalesReturDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        
        protected string _pageTitleLiteral = "Stock - Sales Retur Request";

        protected NameValueCollectionExtractor _nvcExtractor;
        
        public RequestSalesReturBase()
        {
        }

        ~RequestSalesReturBase()
        {
        }
    }
}