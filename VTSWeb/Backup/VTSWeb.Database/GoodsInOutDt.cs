using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class GoodsInOutDt
    {
        private string _ContactName = "";
        private string _CustName = "";

        public GoodsInOutDt(String _prmTransNmbr, int _prmItemNo,String _prmContactName, String _prmItemCode,String _prmCustCode,String _prmCustName, String _prmProductName, String _prmSerialNumber,
        String _prmRemark, String _prmElectriCity)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ItemNo = _prmItemNo;
            this.ContactName = _prmContactName;
            this.ItemCode = _prmItemCode;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.ProductName = _prmProductName;
            this.SerialNumber = _prmSerialNumber;
            this.Remark = _prmRemark;
            this.ElectriCityNumerik = _prmElectriCity;

        }
        public string ContactName
        {
            get
            {
                return this._ContactName;
            }
            set
            {
                this._ContactName = value;
            }
        }
        public string CustName
        {
            get
            {
                return this._CustName;
            }
            set
            {
                this._CustName = value;
            }
        }
    }

}