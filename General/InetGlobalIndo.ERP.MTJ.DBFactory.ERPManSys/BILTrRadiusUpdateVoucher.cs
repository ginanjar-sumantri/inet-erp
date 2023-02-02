using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class BILTrRadiusUpdateVoucher
    {
        String _expiredDateStr = "";

        public BILTrRadiusUpdateVoucher(String _prmTransNmbr, String _prmFileNmbr, Char _prmStatus, DateTime _prmTransDate,
            String _prmRadiusCode, String _prmExpiredDate, String _prmSeries, String _prmSerialNoFrom, String _prmSerialNoTo,
            Decimal _prmSellingAmount, String _prmAssociatedService, int _prmExpireTime, int _prmExpireTimeUnit)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.RadiusCode = _prmRadiusCode;
            this.ExpiredDateStr = _prmExpiredDate;
            this.Series = _prmSeries;
            this.SerialNoFrom = _prmSerialNoFrom;
            this.SerialNoTo = _prmSerialNoTo;
            this.SellingAmount = _prmSellingAmount;
            this.AssociatedService = _prmAssociatedService;
            this.ExpireTime = _prmExpireTime;
            this.ExpireTimeUnit = _prmExpireTimeUnit;
        }

        public string ExpiredDateStr
        {
            get
            {
                return this._expiredDateStr;
            }
            set
            {
                this._expiredDateStr = value;
            }
        }
    }
}
