using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class SAL_TrRetail
    {
        string _empName = "";

        public SAL_TrRetail(String _prmTransNmbr, String _prmFileNmbr, DateTime _prmTransDate, String _prmEmpNumb, String _prmEmpName, String _prmCustName,
            String _prmPaymentType, String _prmBankName, String _prmPaymentCode, String _prmCurrCode, Decimal _prmForexRate, Decimal _prmBaseForex,
            Decimal _prmTotalAmount, Decimal _prmDiscPercent, Decimal _prmDiscAmount, Byte _prmPPNPercent, Decimal _prmPPNAmount, Decimal _prmAdditionalFee,
            Byte _prmStatus, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.CustName = _prmCustName;
            this.PaymentType = _prmPaymentType;
            this.BankName = _prmBankName;
            this.PaymentCode = _prmPaymentCode;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.BaseForex = _prmBaseForex;
            this.TotalAmount = _prmTotalAmount;
            this.DiscPercent = _prmDiscPercent;
            this.DiscAmount = _prmDiscAmount;
            this.PPNPercent = _prmPPNPercent;
            this.PPNAmount = _prmPPNAmount;
            this.AdditionalFee = _prmAdditionalFee;
            this.Status = _prmStatus;
            this.Remark = _prmRemark;
        }

        public SAL_TrRetail(String _prmTransNmbr, String _prmFileNmbr)
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
