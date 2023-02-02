using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class BILMsRegistrationConfig
    {
        public BILMsRegistrationConfig(String _prmRegCode, String _prmRegName, String _prmDescription, String _prmPaymentStatus,
            String _prmRegistrationProductCode, Decimal? _prmRegistrationFee, String _prmInstalationProductCode, Decimal? _prmInstalationFee,
            String _prmDepositProductCode, Decimal? _prmDeposit, int? _prmRecurringFirstCharge)
        {
            this.RegCode = _prmRegCode;
            this.RegName = _prmRegName;
            this.Description = _prmDescription;
            this.PaymentStatus = _prmPaymentStatus;
            this.RegistrationProductCode = _prmRegistrationProductCode;
            this.RegistrationFee = _prmRegistrationFee;
            this.InstalationProductCode = _prmInstalationProductCode;
            this.InstalationFee = _prmInstalationFee;
            this.DepositProductCode = _prmDepositProductCode;
            this.Deposit = _prmDeposit;
            this.RecurringFirstCharge = _prmRecurringFirstCharge;
        }

        public BILMsRegistrationConfig(String _prmRegCode, String _prmRegName)
        {
            this.RegCode = _prmRegCode;
            this.RegName = _prmRegName;
        }
    }
}
