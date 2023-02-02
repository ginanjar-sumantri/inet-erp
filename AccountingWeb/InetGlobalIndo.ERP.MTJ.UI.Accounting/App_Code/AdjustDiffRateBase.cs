using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.AdjustDiffRate
{
    public class AdjustDiffRateBase : AccountingBase
    {
        protected short _menuID = 2357;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "AdjustDiffRate.aspx";
        protected string _addPage = "AdjustDiffRateAdd.aspx";
        protected string _editPage = "AdjustDiffRateEdit.aspx";
        protected string _detailPage = "AdjustDiffRateDetail.aspx";
        protected string _addDetailPage = "AdjustDiffRateDtAdd.aspx";
        protected string _viewDetailPage = "AdjustDiffRateDtView.aspx";
        protected string _editDetailPage = "AdjustDiffRateDtEdit.aspx";
        protected string _addDetailPage2 = "AdjustDiffRateDt2Add.aspx";
        protected string _viewDetailPage2 = "AdjustDiffRateDt2View.aspx";
        protected string _editDetailPage2 = "AdjustDiffRateDt2Edit.aspx";
        
        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";
        protected string _codeCurrKey = "CurrCode";

        protected string _pageTitleLiteral = "Adjust Diff Rate";

        protected NameValueCollectionExtractor _nvcExtractor;
        
        public AdjustDiffRateBase()
        {
        }

        ~AdjustDiffRateBase()
        {
        }
    }
}