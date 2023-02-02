using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class SAL_NCPSale
    {
        string _empName = "";

        public SAL_NCPSale(String _prmTransNmbr, String _prmFileNmbr, DateTime _prmTransDate, String _prmEmpNumb, String _prmEmpName, String _prmCustName,
            String _prmPaymentType, String _prmBankName, String _prmPaymentCode, String _prmCurrCode, Decimal _prmForexRate, Decimal _prmBaseForex,
            Decimal _prmTotalAmount, Guid? _prmDiscCode, Decimal? _prmDiscAmount, Byte? _prmPPNPercent, Decimal? _prmPPNAmount, Decimal? _prmAdditionalFee,
            Byte _prmStatus, String _prmRemark, String _prmSerialNumber, String _prmIMEI, int _prmQtyProduct, int _prmQtyGimmick, Decimal _prmProductPrice,
            Guid _prmPhoneTypeCode, String _prmProductCode, String _prmGimmickCode, Decimal _prmGimmickPrice)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.CustomerName = _prmCustName;
            this.PaymentType = _prmPaymentType;
            this.BankName = _prmBankName;
            this.PaymentCode = _prmPaymentCode;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.BaseForex = _prmBaseForex;
            this.Total = _prmTotalAmount;
            this.DiscountCode = _prmDiscCode;
            this.DiscAmount = _prmDiscAmount;
            this.PPnPercent = _prmPPNPercent;
            this.PPnAmount = _prmPPNAmount;
            this.AdditionalFee = _prmAdditionalFee;
            this.Status = _prmStatus;
            this.Remark = _prmRemark;
            this.PhoneTypeCode = _prmPhoneTypeCode;
            this.ProductCode = _prmProductCode;
            this.GimmickCode = _prmGimmickCode;
            this.SerialNumber = _prmSerialNumber;
            this.IMEI = _prmIMEI;
            this.QtyProduct = _prmQtyProduct;
            this.QtyGimmick = _prmQtyGimmick;
            this.ProductPrice = _prmProductPrice;
            this.GimmickPrice = _prmGimmickPrice;
        }

        public SAL_NCPSale(String _prmSerialNumber)
        {
            this.SerialNumber = _prmSerialNumber;
        }

        public SAL_NCPSale(String _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public string EmpName
        {
            get
            {
                return this._empName;
            }
            set
            {
                this._empName = value;
            }
        }
    }
}
