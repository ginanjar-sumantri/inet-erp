using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.MoneyChanger
{
    public class MoneyChangerBase : FinanceBase
    {
        protected short _menuID = 807;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "MoneyChanger.aspx";
        protected string _addPage = "MoneyChangerAdd.aspx";
        protected string _editPage = "MoneyChangerEdit.aspx";
        protected string _detailPage = "MoneyChangerView.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Money Changer";

        protected NameValueCollectionExtractor _nvcExtractor;

        public MoneyChangerBase()
        {

        }

        ~MoneyChangerBase()
        {

        }
    }
}