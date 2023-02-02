using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFAPurchaseDt
    {
        public GLFAPurchaseDt(string _prmTransNmbr, int _prmItemNo, string _prmFAStatus, char? _prmFAOwner, string _prmFASubGroup, string _prmFAName, string _prmFALocationType, decimal? _prmAmountForex)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ItemNo = _prmItemNo;
            this.FAStatus = _prmFAStatus;
            this.FAOwner = _prmFAOwner;
            this.FASubGroup = _prmFASubGroup;
            this.FAName = _prmFAName;
            this.FALocationType = _prmFALocationType;
            this.AmountForex = _prmAmountForex;
        }

        public GLFAPurchaseDt(string _prmTransNmbr, int _prmItemNo, string _prmFAStatus, char? _prmFAOwner, string _prmFASubGroup, string _prmFACode, string _prmFAName, string _prmFALocationType, decimal? _prmAmountForex, decimal? _prmQty, decimal? _prmPriceForex)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ItemNo = _prmItemNo;
            this.FAStatus = _prmFAStatus;
            this.FAOwner = _prmFAOwner;
            this.FASubGroup = _prmFASubGroup;
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.FALocationType = _prmFALocationType;
            this.AmountForex = _prmAmountForex;
            this.Qty = _prmQty;
            this.PriceForex = _prmPriceForex;
        }
    }
}
