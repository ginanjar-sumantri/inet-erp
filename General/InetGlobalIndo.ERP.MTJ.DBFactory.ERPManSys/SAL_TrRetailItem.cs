using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class SAL_TrRetailItem
    {
        public string _productName = "";
        public string _phoneTypeName = "";
        public Int32 _no = 0;

        public SAL_TrRetailItem(Guid _prmPOSItemCode, String _prmTransNmbr, Guid? _prmPhoneTypeCode, String _prmPhoneTypeName, String _prmProductCode,
            String _prmProductName, String _prmSerialNumber, String _prmIMEI, Int32 _prmQty, Decimal _prmPrice, Guid? _prmDiscCode, Decimal _prmDiscAmount, Decimal _prmTotal,
            String _prmRemark)
        {
            this.POSItemCode = _prmPOSItemCode;
            this.TransNmbr = _prmTransNmbr;
            this.PhoneTypeCode = _prmPhoneTypeCode;
            this.PhoneTypeName = _prmPhoneTypeName;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.SerialNumber = _prmSerialNumber;
            this.IMEI = _prmIMEI;
            this.Qty = _prmQty;
            this.Price = _prmPrice;
            this.DiscountCode = _prmDiscCode;
            this.DiscountAmount = _prmDiscAmount;
            this.Total = _prmTotal;
            this.Remark = _prmRemark;
        }

        public SAL_TrRetailItem(Int32 _prmNo, String _prmProductCode, Guid _prmPhoneTypeCode, String _prmSerialNumber, String _prmIMEI,
            Int32 _prmQty, Decimal _prmPrice, Guid? _prmDiscCode, Decimal _prmDiscAmount, Decimal _prmTotal, String _prmRemark)
        {
            this.No = _prmNo;
            this.PhoneTypeCode = _prmPhoneTypeCode;
            this.ProductCode = _prmProductCode;
            this.SerialNumber = _prmSerialNumber;
            this.IMEI = _prmIMEI;
            this.Qty = _prmQty;
            this.Price = _prmPrice;
            this.DiscountCode = _prmDiscCode;
            this.DiscountAmount = _prmDiscAmount;
            this.Total = _prmTotal;
            this.Remark = _prmRemark;
        }

        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                this._productName = value;
            }
        }

        public Int32 No
        {
            get
            {
                return this._no;
            }
            set
            {
                this._no = value;
            }
        }

        public string PhoneTypeName
        {
            get
            {
                return this._phoneTypeName;
            }
            set
            {
                this._phoneTypeName = value;
            }
        }
    }
}
