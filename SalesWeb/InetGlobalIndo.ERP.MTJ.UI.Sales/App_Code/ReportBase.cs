using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Report
{
    public abstract class ReportBase : SalesBase
    {
        protected short _menuIDCustomerList = 1390;
        protected short _menuIDProFormaInvPerTrans = 1407;
        protected short _menuIDProFormaInvPerCust = 1408;
        protected short _menuIDProFormaInvPerProduct = 1410;
        protected short _menuIDProFormaInvSummaryPerCust = 1411;
        protected short _menuIDProFormaInvSummaryPerProduct = 1412;
        protected short _menuIDOutstandingProFormaInvPerTrans = 1414;
        protected short _menuIDOutstandingProFormaInvPerCust = 1415;
        protected short _menuIDOutstandingProFormaInvPerProduct = 1416;
        protected short _menuIDDOPerTrans = 1419;
        protected short _menuIDDOPerCust = 1420;
        protected short _menuIDDOPerDelivery = 1421;
        protected short _menuIDDOPerProduct = 1422;
        protected short _menuIDDOSummaryPerCust = 1423;
        protected short _menuIDDOSummaryPerProduct = 1424;
        protected short _menuIDOutstandingDOPerTrans = 1426;
        protected short _menuIDOutstandingDOPerCust = 1427;
        protected short _menuIDOutstandingDOPerProduct = 1428;
        protected short _menuIDClosingDO = 1429;
        protected short _menuIDClosingProFormaInv = 1417;
        protected short _menuIDReqSalesReturTrans = 1431;
        protected short _menuIDReqSalesReturCust = 1432;
        protected short _menuIDReqSalesReturProd = 1433;
        protected short _menuIDReqSalesReturOutTrans = 1435;
        protected short _menuIDReqSalesReturOutCust = 1436;
        protected short _menuIDReqSalesReturOutProd = 1437;
        protected short _menuIDPFI_BLSUM = 1439;
        protected short _menuIDPFI_BLDetail = 1440;
        protected short _menuIDPOSReport = 1799;
        protected short _menuIDDirectSalesPerTransaction = 2076;
        protected short _menuIDDirectSalesPerProduct = 2077;
        protected short _menuIDDirectSalesPerCustomer = 2078;

        protected PermissionLevel _permAccess;

        protected string _errorPermissionPage = ApplicationConfig.SalesWebAppURL + "ErrorPermission.aspx";

        public ReportBase()
        {

        }
        ~ReportBase()
        {

        }

    }
}