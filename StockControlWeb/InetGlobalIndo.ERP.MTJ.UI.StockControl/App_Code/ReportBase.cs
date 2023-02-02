using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Report
{
    public abstract class ReportBase : StockControlBase
    {
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected short _menuIDProductListing = 1193;
        protected short _menuIDBeginningStock = 1194;
        protected short _menuIDRRGoodsSummaryPerSupplier = 1198;
        protected short _menuIDStockUsage = 1197;
        protected short _menuIDStockReceive = 1199;
        protected short _menuIDStockAvailable = 1368;
        protected short _menuIDSTCSalesReturPerTrans = 1234;
        protected short _menuIDStockBalance = 1363;
        protected short _menuIDSTCSalesReturPerCust = 1235;
        protected short _menuIDSTCSalesReturPerWrhs = 1236;
        protected short _menuIDStockActivitiesMonthly = 1360;
        protected short _menuIDStockActivitiesListing = 1357;
        protected short _menuIDStockCardValue = 1355;
        protected short _menuIDStockCardValuePerDate = 1356;
        protected short _menuIDStockCard = 1352;
        protected short _menuIDStockCardPerDate = 1353;
        protected short _menuIDStockOpnamePerTrans = 1344;
        protected short _menuIDStockOpnamePerProduct = 1346;
        protected short _menuIDStockOpnamePerWrhs = 1345;
        protected short _menuIDStockOpnameSummaryPerProduct = 1347;
        protected short _menuIDStockAdjustPerTrans = 1339;
        protected short _menuIDStockAdjustPerProduct = 1340;
        protected short _menuIDStockAdjustPerWrhs = 1341;
        protected short _menuIDStockAdjustSummaryPerProduct = 1342;
        protected short _menuIDStockChangeGoodsPerTrans = 1333;
        protected short _menuIDStockChangeGoodsPerProductSrc = 1334;
        protected short _menuIDStockChangeGoodsPerProductDest = 1335;
        protected short _menuIDStockChangeGoodsSummaryPerProductSrc = 1336;
        protected short _menuIDStockChangeGoodsSummaryPerProductDest = 1337;
        protected short _menuIDSuratJalanPerTrans = 1260;
        protected short _menuIDSuratJalanPerCust = 1261;
        protected short _menuIDSuratJalanPerWrhs = 1262;
        protected short _menuIDSuratJalanPerProd = 1263;
        protected short _menuIDSuratJalanSumCust = 1264;
        protected short _menuIDSuratJalanSumProd = 1265;
        protected short _menuIDSuratJalanUnInvoicePerTrans = 1267;
        protected short _menuIDSuratJalanUnInvoicePerCust = 1268;
        protected short _menuIDSuratJalanUnInvoicePerWrhs = 1269;
        protected short _menuIDSuratJalanUnInvoicePerProd = 1270;
        protected short _menuIDStockMutation = 1715;
        protected short _menuIDNCPSoldListByCategory = 1781;
        protected short _menuIDRRReportListPerTransaction = 1988;
        protected short _menuIDRRReportListPerProduct = 1995;
        protected short _menuIDStockBalanceByProductValue = 2014;

        protected PermissionLevel _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";

        public ReportBase()
        {
            CompanyConfig _companyConfig = new CompanyConfig();
            this._theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            this._rowColor = _companyConfig.GetThemeComponent(ThemeComponent.RowColor, _theme);
            this._rowColorAlternate = _companyConfig.GetThemeComponent(ThemeComponent.RowColorAlternate, _theme);
            this._rowColorHover = _companyConfig.GetThemeComponent(ThemeComponent.RowColorHover, _theme);
        }

        ~ReportBase()
        {
        }
    }
}