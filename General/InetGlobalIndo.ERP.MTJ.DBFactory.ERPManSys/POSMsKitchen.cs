using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSMsKitchen
    {
        public POSMsKitchen(String _prmKitchenCode, String _prmKitchenName, String _prmChef,
            String _prmLocation, String _prmKitchenPrinterIPAddress, String _prmKitchenPrinterName)
        {
            this.KitchenCode = _prmKitchenCode;
            this.KitchenName = _prmKitchenName;
            this.Chef = _prmChef;
            this.Location = _prmLocation;
            this.KitchenPrinterIPAddress = _prmKitchenPrinterIPAddress;
            this.KitchenPrinterName = _prmKitchenPrinterName;
        }
    }
}
