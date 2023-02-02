using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

namespace POS.POSInterface
{
    public class POSShippingBase : POSInterfaceBase
    {
        protected short _menuID = 0;
        
        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _referenceNo = "referenceNo";

        protected string _homePage = "POSShipping.aspx";
        protected string _closeShiftPage = "POSShippingCloseShift.aspx";
        
        protected string _pageTitleLiteral = "Discount";

        protected NameValueCollectionExtractor _nvcExtractor;

        public POSShippingBase()
        {
        }

        ~POSShippingBase()
        {
        }
    }
}