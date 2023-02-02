using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrClaimIssueDt
    {
        private string _claimName = "";

        public HRMTrClaimIssueDt(String _prmTransNmbr, String _prmClaimCode, String _prmClaimName, String _prmReceiptNo, DateTime _prmReceiptDate,
            Decimal _prmAmountClaim, Decimal? _prmReimburstPercentage, Decimal? _prmAmountReimburst, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ClaimCode = _prmClaimCode;
            this.ClaimName = _prmClaimName;
            this.ReceiptNo = _prmReceiptNo;
            this.ReceiptDate = _prmReceiptDate;
            this.AmountClaim = _prmAmountClaim;
            this.ReimburstPercentage = _prmReimburstPercentage;
            this.AmountReimburst = _prmAmountReimburst;
            this.Remark = _prmRemark;
        }

        public string ClaimName
        {
            get
            {
                return this._claimName;
            }
            set
            {
                this._claimName = value;
            }
        }
    }
}
