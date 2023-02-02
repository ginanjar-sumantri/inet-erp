using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFATenancyDt
    {
        private string _FAName = "";

        public GLFATenancyDt(string _prmTransNmbr, string _prmFACode, string _prmFAName, decimal _prmQty, string _prmUnit, decimal _prmPrice, decimal _prmAmountForex, DateTime _prmStartT, DateTime _prmEndT, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.Price = _prmPrice;
            this.AmountForex = _prmAmountForex;
            this.StartTenancy = _prmStartT;
            this.EndTenancy = _prmEndT;
            this.Remark = _prmRemark;
        }

        public string FAName
        {
            get
            {
                return this._FAName;
            }
            set
            {
                this._FAName = value;
            }
        }
    }
}
