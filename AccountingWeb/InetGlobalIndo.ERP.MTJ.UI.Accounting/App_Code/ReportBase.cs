using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Report
{
    public class ReportBase : AccountingBase
    {
        protected short _menuIDBSheet = 262;
        protected short _menuIDBSheetMonthlyDetail = 263;
        protected short _menuIDChartOfAccount = 264;
        protected short _menuIDDailyTransactionJournal = 265;
        protected short _menuIDGLSubledger = 266;
        protected short _menuIDGLSummary = 267;
        protected short _menuIDPLMonthly = 268;
        protected short _menuIDPLSummary = 269;
        protected short _menuIDTransactionJournal = 270;
        protected short _menuIDTrialBalance = 271;
        protected short _menuIDGLSummaryByDate = 588;
        protected short _menuIDFixedAssetList = 972;
        protected short _menuIDCurrRate = 1441;
        protected short _menuIDBSAndPL = 1860;
        protected short _menuIDCashflowProjection = 2570;

        protected PermissionLevel _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        public ReportBase()
        {
        }

        ~ReportBase()
        {
        }
    }
}