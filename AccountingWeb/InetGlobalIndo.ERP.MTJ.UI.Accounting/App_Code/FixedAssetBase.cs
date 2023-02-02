using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAsset
{
    public abstract class FixedAssetBase : AccountingBase
    {
        protected short _menuID = 56;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permUnposting, _permPosting;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FixedAsset.aspx";
        protected string _addPage = "FixedAssetAdd.aspx";
        protected string _editPage = "FixedAssetEdit.aspx";
        protected string _viewPage = "FixedAssetView.aspx";
        protected string _summaryPage = "FixedAssetSummary.aspx";
        protected string _importPage = "FixedAssetImport.aspx";
        protected string _changePhotoPage = "FixedAssetChangePhoto.aspx";
        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Fixed Asset";

        public FixedAssetBase()
        {

        }

        ~FixedAssetBase()
        {

        }
    }
}