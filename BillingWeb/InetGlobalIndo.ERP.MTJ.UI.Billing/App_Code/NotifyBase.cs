using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Notification
{
    public abstract class NotifyBase : System.Web.UI.Page
    {
        private string _pageTitle = ApplicationConfig.MembershipAppName + " :: Notification";

        protected short _moduleID = 1824;
        protected short _menuIDBeritaAcara = 1825;
        protected short _menuIDBASoftBlock = 1826;
        protected short _menuIDCL = 1836;
        protected short _menuIDCLInternal = 1838;
        protected short _menuIDCLSoftBlock = 1837;
        protected short _menuIDPayment = 1840;
        protected short _menuIDPaymentSoftBlock = 1841;
        protected short _menuIDBilInvEmailNYSend = 2257;
        protected PermissionLevel _permAccess, _permView;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _homePage = "EmailNotificationSetup.aspx";
        protected string _editPage = "EmailNotificationSetupEdit.aspx";
        protected string _viewPage = "EmailNotificationSetupView.aspx";
        
        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Notification";
        protected string _pageTitleNotifyBeritaAcaraNotApprovedLiteral = "Berita Acara Not Yet Approved Notification";
        protected string _pageTitleNotifyContractLetterLiteral = "Contract Letter Notification";
        protected string _pageTitleNotifyContractLetterInternalLiteral = "Contract Letter Notification (Legal)";
        protected string _pageTitleBASoftBlockLiteral = "Berita Acara Soft Block Notification";
        protected string _pageTitleContractSoftBlockLiteral = "Contract Soft Block Notification";
        protected string _pageTitlePembayaranLiteral = "Payment Notification";
        protected string _pageTitlePembayaranSoftBlockLiteral = "Payment SoftBlock Notification";
        protected string _pageTitleBilInvEmailNYSendLiteral = "Billing Invoice Email Not Yet Send Notification";
        
        protected NameValueCollectionExtractor _nvcExtractor;

        public NotifyBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);

            this.Title = this._pageTitle;
        }
    }
}