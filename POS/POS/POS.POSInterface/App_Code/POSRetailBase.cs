using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;


namespace POS.POSInterface
{
    public class POSRetailBase : POSInterfaceBase
    {
        protected short _menuID = 0;
        
        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";
        protected string _referenceNo = "referenceNo";

        protected string _homePage = "POSRetail.aspx";
        //protected string _chooseTablePage = "POSInternetChooseTable.aspx";
        //protected string _reservationPage = "POSInternetReservation.aspx";
        //protected string _closeShiftPage = "POSInternetCloseShift.aspx";
        //protected string _tableTransferPage = "POSInternetTableTransfer.aspx";

        protected string _openHoldPage = "OpenHold.aspx";
        protected string _loginPage = "Login.aspx";
        
        protected string _pageTitleLiteral = "Retail";

        protected NameValueCollectionExtractor _nvcExtractor;

        public POSRetailBase()
        {
        }

        ~POSRetailBase()
        {
        }
    }
}