using System;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFAServiceDt
    {
        string _faMaintenanceName = "";

        public GLFAServiceDt(string _prmTransNmbr, int _prmItemNo, string _prmFAMaintenance, string _prmFAMaintenanceName, char _prmFgAddValue, string _prmRemark, decimal? _prmQty, string _prmUnit, decimal? _prmPriceForex, decimal? _prmAmountForex)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ItemNo = _prmItemNo;
            this.FAMaintenance = _prmFAMaintenance;
            this.FAMaintenanceName = _prmFAMaintenanceName;
            this.FgAddValue = _prmFgAddValue;
            this.Remark = _prmRemark;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.PriceForex = _prmPriceForex;
            this.AmountForex = _prmAmountForex;
        }

        public string FAMaintenanceName
        {
            get
            {
                return this._faMaintenanceName;
            }
            set
            {
                this._faMaintenanceName = value;
            }
        }
    }
}
