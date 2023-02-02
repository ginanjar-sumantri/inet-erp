using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.JournalEntry
{
    public abstract class JournalEntryBase : AccountingBase
    {
        protected short _menuID = 34;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected String _homePage = "JournalEntry.aspx";
        protected String _addPage = "JournalEntryAdd.aspx";
        protected String _editPage = "JournalEntryEdit.aspx";
        protected String _viewPage = "JournalEntryDetail.aspx";
        protected String _detailPage = "JournalEntryDetail.aspx";
        protected String _addDetailPage = "JournalEntryDetailAdd.aspx";
        protected String _viewDetailPage = "JournalEntryDetailView.aspx";
        protected String _editDetailPage = "JournalEntryDetailEdit.aspx";
        protected String _advanceSearchPage = "JournalEntryAdvancedSearch.aspx";

        protected String _codeKey = "code";
        protected String _codeTransClass = "transclass";
        protected String _codeReference = "reference";

        protected string _pageTitleLiteral = "Journal Entry";

        protected NameValueCollectionExtractor _nvcExtractor;

        public JournalEntryBase()
        {
        }

        ~JournalEntryBase()
        {
        }
    }
}