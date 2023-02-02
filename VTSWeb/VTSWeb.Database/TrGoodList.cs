using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class TrGoodList
    {
        public TrGoodList(String _prmCustCode, String _prmItemCode, String _prmProductName, String _prmSerialNumber, String _prmRemark, String _prmElectriCityNumerik)
        {
            this.CustCode = _prmCustCode;
            this.ItemCode = _prmItemCode;
            this.ProductName = _prmProductName;
            this.SerialNumber = _prmSerialNumber;
            this.Remark = _prmRemark;
            this.ElectriCityNumerik = _prmElectriCityNumerik;

        }

        public TrGoodList(String _prmCustCode, String _prmItemCode)
        {
            this.CustCode = _prmCustCode;
            this.ItemCode = _prmItemCode;
        }

    }

}