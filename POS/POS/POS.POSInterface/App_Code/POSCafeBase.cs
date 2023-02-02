using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace POS.POSInterface
{
    public class POSCafeBase : POSInterfaceBase
    {
        protected short _menuID = 0;
         
        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _selectedTableKey = "selectedTable";
        protected string _selectedJoinTableKey = "selectedJoinTable";
        protected string _selectedFloorKey = "selectedFloor";
        protected string _referenceNo = "referenceNo";

        protected string _homePage = "POSCafe.aspx";
        protected string _chooseTablePage = "POSCafeChooseTable.aspx";
        protected string _reservationPage = "POSCafeReservation.aspx";
        protected string _closeShiftPage = "POSCafeCloseShift.aspx";
        protected string _tableTransferPage = "POSCafeTableTransfer.aspx";
        protected string _joinTablePage = "JoinTable.aspx";

        protected string _pageTitleLiteral = "Discount";

        protected NameValueCollectionExtractor _nvcExtractor;

        public POSCafeBase()
        {
        }

        ~POSCafeBase()
        {
        }
    }
}