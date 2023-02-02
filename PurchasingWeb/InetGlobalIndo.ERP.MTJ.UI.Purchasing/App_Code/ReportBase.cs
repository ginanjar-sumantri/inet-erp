using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.Report
{
    public abstract class ReportBase : PurchasingBase
    {
        protected short _menuIDSupplierList = 1151;
        protected short _menuIDPurchaseRequestPerTrans = 1153;
        protected short _menuIDPurchaseRequestPerProduct = 1154;
        protected short _menuIDPurchaseRequestPerProductGroup = 1155;
        protected short _menuIDOutstandingPurchaseRequestPerTrans = 1157;
        protected short _menuIDOutstandingPurchaseRequestPerProduct = 1158;
        protected short _menuIDOutstandingPurchaseRequestPerProductGroup = 1159;
        protected short _menuIDClosingPurchaseRequest = 1160;
        protected short _menuIDPurchaseOrderStatus = 1162;
        protected short _menuIDPurchaseOrderPerTrans = 1163;
        protected short _menuIDPurchaseOrderPerSupp = 1164;
        protected short _menuIDPurchaseOrderPerProduct = 1165;
        protected short _menuIDPurchaseOrderPerBuyer = 1166;
        protected short _menuIDPurchaseOrderSummaryPerProduct = 1167;
        protected short _menuIDPurchaseOrderSummaryPerSupp = 1168;
        protected short _menuIDOutstandingPurchaseOrderPerTrans = 1170;
        protected short _menuIDOutstandingPurchaseOrderPerSupp = 1171;
        protected short _menuIDOutstandingPurchaseOrderPerProduct = 1172;
        protected short _menuIDClosingPurchaseOrder = 1173;
        protected short _menuIDPurchaseCostPerTrans = 1175;
        protected short _menuIDPurchaseCostPerSupp  = 1176;
        protected short _menuIDPurchaseCostSummaryPerSupp = 1177;
        protected short _menuIDPurchaseReturPerTrans = 1179;
        protected short _menuIDPurchaseReturPerSupp = 1180;
        protected short _menuIDPurchaseReturPerProduct = 1181;
        protected short _menuIDPurchaseReturPerWrhs = 1182;
        protected short _menuIDPurchaseReturSummaryPerProduct = 1183;
        protected short _menuIDPurchaseReturSummaryPerSupp = 1184;
        protected short _menuIDProgressRequestReceiveSummary = 1186;
        protected short _menuIDProgressRequestReceiveDetail = 1187;
        protected short _menuIDProgressRequestInvoice = 1188;
        protected short _menuIDProgressRequestPayment = 1191;
        protected short _menuIDDirectPurchasePerTransaction = 2448;
        protected short _menuIDDirectPurchasePerProduct = 2449;
        protected short _menuIDDirectPurchasePerSupplier = 2450;

        protected PermissionLevel _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        public ReportBase()
        {
        }

        ~ReportBase()
        {
        }

    }
}