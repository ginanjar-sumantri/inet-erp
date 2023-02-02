using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;


namespace POS.POSInterface
{
    public class CloseShiftBase : POSInterfaceBase
    {
        protected string _codeKey = "code";
        //protected string _codeItemKey = "CodeItem";

        protected string _cashierPage = "Cashier.aspx";
        //protected string _cashierPage = "Monitoring.aspx";
        
        protected string _settlementPage = "Settlement.aspx";
        protected string _loginPage = "Login.aspx";
        
        protected string _pageTitleLiteral = "Retail";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CloseShiftBase()
        {
        }

        ~CloseShiftBase()
        {
        }
    }
}