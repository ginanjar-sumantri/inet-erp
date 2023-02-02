using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.Report
{
    public abstract class ReportBase : FinanceBase
    {
        protected short _menuIDAPAging = 936;
        protected short _menuIDAPListing = 935;
        protected short _menuIDARListing = 938;
        protected short _menuIDARAging = 939;
        protected short _menuIDCashAndBank = 1013;
        protected short _menuIDGiroCustAging = 914;
        protected short _menuIDGiroCustListBankGiro = 897;
        protected short _menuIDGiroCustListCust = 896;
        protected short _menuIDGiroCustListDueDate = 894;
        protected short _menuIDGiroCustListReceiptDate = 895;
        protected short _menuIDGiroCustODBankGiro = 902;
        protected short _menuIDGiroCustODDueDate = 900;
        protected short _menuIDGiroCustODCust = 901;
        protected short _menuIDGiroCustCairBankGiro = 906;
        protected short _menuIDGiroCustCairDate = 904;
        protected short _menuIDGiroCustCairCust = 905;
        protected short _menuIDGiroCustCairBankSetor = 907;
        protected short _menuIDGiroCustOutstanding = 913;
        protected short _menuIDGiroCustTolakBankGiro = 911;
        protected short _menuIDGiroCustTolakDate = 909;
        protected short _menuIDGiroCustTolakCust = 910;
        protected short _menuIDGiroCustTolakBankSetor = 912;
        protected short _menuIDGiroSuppAging = 933;
        protected short _menuIDGiroSuppOutstanding = 932;
        protected short _menuIDGiroSuppListDueDate = 916;
        protected short _menuIDGiroSuppListPaymentDate = 917;
        protected short _menuIDGiroSuppListSupp = 918;
        protected short _menuIDGiroSuppListBankPayment = 919;
        protected short _menuIDGiroSuppODDueDate = 921;
        protected short _menuIDGiroSuppODSupp = 922;
        protected short _menuIDGiroSuppODBankPayment = 923;
        protected short _menuIDGiroSuppCairDate = 925;
        protected short _menuIDGiroSuppCairSupp = 926;
        protected short _menuIDGiroSuppCairBankPayment = 927;
        protected short _menuIDGiroSuppTolakDate = 929;
        protected short _menuIDGiroSuppTolakSupp = 930;
        protected short _menuIDGiroSuppTolakBankPayment = 931;
        protected short _menuIDARAnalysis = 50;
        protected short _menuIDPPNListingSales = 1055;
        protected short _menuIDPPNListingPurchase = 1054;
        protected short _menuIDARPaidFull = 1048;
        protected short _menuIDAPPaidFull = 1049;
        protected short _menuIDSupplierHistory = 1050; 
        protected short _menuIDCustomerHistory = 1052;
        protected short _menuIDPettyCashPerTransaction = 1091;
        protected short _menuIDPettyCashPerAccount = 1092;
        protected short _menuIDPettyCashSummary = 1093;
        protected short _menuIDPaymentInvoice = 1095;
        protected short _menuIDPaymentPerSupplier = 1096;
        protected short _menuIDPaymentPerTransaction = 1097;
        protected short _menuIDPaymentPerDate = 1098;
        protected short _menuIDPaymentPerPaymentType = 1099;
        protected short _menuIDPaymentSummaryPerSupplier = 1100;
        protected short _menuIDPaymentSummaryPerPaymentType = 1101;
        protected short _menuIDARStatementOfAccount = 1051;
        protected short _menuIDPurchaseInvoicePerTrans = 1068;
        protected short _menuIDPurchaseInvoicePerSupplier = 1069;
        protected short _menuIDPurchaseInvoicePerProduct = 1070;
        protected short _menuIDPurchaseInvoiceTopPurchasePerProduct = 1071;
        protected short _menuIDPurchaseInvoiceTopPurchasePerSupplier = 1072;
        protected short _menuIDPurchaseInvoiceBottomPurchasePerProduct = 1073;
        protected short _menuIDPurchaseInvoiceBottomPurchasePerSupplier = 1074;
        protected short _menuIDPurchaseInvoiceSummaryPerSupplier = 1075;
        protected short _menuIDPurchaseInvoiceSummaryPerProduct = 1076;
        protected short _menuIDPurchaseInvoicePurchasePriceHistory = 1077; 
        protected short _menuIDSalesInvoicePerTrans = 1057;
        protected short _menuIDSalesInvoicePerCustomer = 1058;
        protected short _menuIDSalesInvoicePerProduct = 1059;
        protected short _menuIDSalesInvoiceTopSalesPerProduct = 1060;
        protected short _menuIDSalesInvoiceTopSalesPerCustomer = 1061;
        protected short _menuIDSalesInvoiceBottomSalesPerProduct = 1062;
        protected short _menuIDSalesInvoiceBottomSalesPerCustomer = 1063;
        protected short _menuIDSalesInvoiceSummaryPerCustomer = 1064;
        protected short _menuIDSalesInvoiceSummaryPerProduct = 1065;
        protected short _menuIDSalesInvoiceSalesPriceHistory = 1066;
        protected short _menuIDPurchaseReturPerTrans = 1085;
        protected short _menuIDPurchaseReturPerSupp = 1086;
        protected short _menuIDPurchaseReturPerProduct = 1087;
        protected short _menuIDPurchaseReturSummaryPerSupplier = 1089;
        protected short _menuIDPurchaseReturSummaryPerProduct = 1088;
        protected short _menuIDDifferenceRateSupplierPerTransaction = 1147;
        protected short _menuIDDifferenceRateSupplierPerDate = 1148;
        protected short _menuIDDifferenceRateSupplierHistory = 1149;
        protected short _menuIDSalesReturPerTrans = 1079;
        protected short _menuIDSalesReturPerCust = 1080;
        protected short _menuIDSalesReturPerProduct = 1081;
        protected short _menuIDSalesReturSummaryPerCustomer = 1082;
        protected short _menuIDSalesReturSummaryPerProduct = 1083;
        protected short _menuIDPaymentAndReceiptAnalysisSummary = 1115;
        protected short _menuIDPaymentAndReceiptAnalysisDetail = 1116;
        protected short _menuIDPaymentAndReceiptAnalysisMonthly = 1117;
        protected short _menuIDDPSupplierPayInvoicePerTransaction = 1122;
        protected short _menuIDDPSupplierReturPerTransaction = 1124;
        protected short _menuIDDPSupplierReturPerSupplier = 1125;
        protected short _menuIDSalesProfitSummaryPerProduct = 1135;
        protected short _menuIDSalesProfitDetailPerTransaction = 1136;
        protected short _menuIDSalesProfitDetailPerCustomer = 1137;
        protected short _menuIDSalesProfitDetailPerProduct = 1138;
        protected short _menuIDReceiptPerInvoice = 1103;
        protected short _menuIDReceiptPerCustomer = 1104;
        protected short _menuIDReceiptPerTransaction = 1105;
        protected short _menuIDReceiptPerDate = 1106;
        protected short _menuIDReceiptPerReceiptType = 1107;
        protected short _menuIDReceiptSummaryPerCustomer = 1108;
        protected short _menuIDReceiptSummaryPerReceiptType = 1109;
        protected short _menuIDCashFlowSummary = 1111;
        protected short _menuIDCashFlowDetail = 1112;
        protected short _menuIDCashFlowForecast = 1113;
        protected short _menuIDDPCustomerIssue = 1127;
        protected short _menuIDDPCustomerOutstanding = 1128;
        protected short _menuIDDPCustomerPayInvoicePerTrans = 1129;
        protected short _menuIDDPCustomerReturPerTrans = 1132;
        protected short _menuIDDPCustomerReturPerCust = 1133;
        protected short _menuIDSalesProfitSummaryPerCust = 1139;
        protected short _menuIDSalesProfitSummaryPerProductGroup = 1140;
        protected short _menuIDDiffRateCustomerPerTrans = 1143;
        protected short _menuIDDiffRateCustomerPerDate = 1144;
        protected short _menuIDDiffRateCustomerPerHistory = 1145;
        protected short _menuIDIncomeGraphPerYear = 1146;
        protected short _menuIDSupplierInvoiceConsignmentPerTrans = 2567;
        protected short _menuIDSupplierInvoiceConsignmentPerProductSummary = 2568;
        protected short _menuIDSupplierInvoiceConsignmentPerSupplier = 2569;

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