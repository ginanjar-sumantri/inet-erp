using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsProductGroup
    {
        public MsProductGroup(string _prmProductGrpCode, string _prmProductGrpName, string _prmUserId, DateTime? _prmUserDate)
        {
            this.ProductGrpCode = _prmProductGrpCode;
            this.ProductGrpName = _prmProductGrpName;
            this.CreatedBy = _prmUserId;
            this.CreatedDate = _prmUserDate;
        }

        public MsProductGroup(string _prmProductGrpCode, string _prmProductGrpName)
        {
            this.ProductGrpCode = _prmProductGrpCode;
            this.ProductGrpName = _prmProductGrpName;
        }
    }
}
