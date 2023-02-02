using System;
using System.Collections.Generic;
using VTSWeb.Enum;
using VTSWeb.Common;
using VTSWeb.SystemConfig;

namespace VTSWeb.UI
{
    public abstract class ReportBase: System.Web.UI.Page
    {
        protected String _homePageCustomer = "ReportCustomerLogVisitByCustomer.aspx";
        protected String _homePageCustomerPermission = "ReportCustomerListVisitorPermission.aspx";
        protected String _homePageCustomerCardId = "ReportCustomerVisitorCard.aspx";
        protected String _homePageCustomerLogVisitByDate = "ReportCustomerLogVisitByDate.aspx";
        protected String _homePageCustomerGoodsIn  = "ReportCustomerGoodInTransactionByCustomer.aspx";
        protected String _homePageCustomerEquipmentList = "ReportCustomerEquipmentList.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteralCustomer = "Customer Log Visit ByCustomer";
        protected String _pageTitleLiteralCustomerPermission = "Customer List Visitor Permission";
        protected String _pageTitleLiteralPageCustomerCardId = "Customer Visitor Card";
        protected String _pageTitleLiteralCustomerLogVisitByDate = "Customer Log Visit ByDate ";
        protected String _pageTitleLiteralCustomerGoodsIn = "Customer Good In Transaction By Customer";
        protected String _pageTitleLiteralSummaryCustomerLogVisitByDate = "Summary Customer Log Visit By Customer";
        protected String _pageTitleLiteralPageCustomerEquipmentList = "Customer Equipment List";


        protected NameValueCollectionExtractor _nvcExtractor;

        public ReportBase()
        {
        }

        ~ReportBase()
        {
        }
    }
}
