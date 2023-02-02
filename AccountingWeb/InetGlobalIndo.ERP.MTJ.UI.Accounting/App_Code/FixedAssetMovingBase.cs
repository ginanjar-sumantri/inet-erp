using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAssetMoving
{
    public abstract class FixedAssetMovingBase : AccountingBase
    {
        protected short _menuID = 246;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        //utama
        protected string _homePage = "FixedAssetMoving.aspx";
        protected string _addPage = "FixedAssetMovingAdd.aspx";
        protected string _editPage = "FixedAssetMovingEdit.aspx";
        protected string _detailPage = "FixedAssetMovingDetail.aspx";
        protected string _addDetailPage = "FixedAssetMovingDetailAdd.aspx";
        protected string _viewDetailPage = "FixedAssetMovingDetailView.aspx";
        protected string _editDetailPage = "FixedAssetMovingDetailEdit.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _codeKey = "code";
        protected string _codeTrans = "CodeTrans";

        protected string _pageTitleLiteral = "Fixed Asset Moving";
        //add

        //edit

        //detail

        //detail add

        //detail edit

        //detail view

        public FixedAssetMovingBase()
        {

        }

        ~FixedAssetMovingBase()
        {

        }
    }
}