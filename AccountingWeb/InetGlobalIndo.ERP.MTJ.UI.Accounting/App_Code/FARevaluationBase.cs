using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;


namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FARevaluation
{
    public abstract class FARevaluationBase : AccountingBase
    {
        protected short _menuID = 247;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected String _homePage = "FARevaluation.aspx";
        protected String _addPage = "FARevaluationAdd.aspx";
        protected String _editPage = "FARevaluationEdit.aspx";
        protected String _detailPage = "FARevaluationDetail.aspx";
        protected String _addDetailPage = "FARevaluationDetailAdd.aspx";
        protected String _editDetailPage = "FARevaluationDetailEdit.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected String _codeFA = "FACode";
        protected String _codeKey = "code";

        protected String _pageTitleLiteral = "Fixed Asset Revaluation";

        public FARevaluationBase()
        {

        }

        ~FARevaluationBase()
        {

        }
    }
}