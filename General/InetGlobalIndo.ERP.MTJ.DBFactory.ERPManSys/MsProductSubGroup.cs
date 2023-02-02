using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsProductSubGroup
    {
        public MsProductSubGroup(string _prmProductSubGrpCode, string _prmProductSubGrpName, string _prmProductGroup, string _prmUserId, DateTime? _prmUserDate)
        {
            this.ProductSubGrpCode = _prmProductSubGrpCode;
            this.ProductSubGrpName = _prmProductSubGrpName;
            this.ProductGroup = _prmProductGroup;
            this.CreatedBy = _prmUserId;
            this.CreatedDate = _prmUserDate;
        }

        public MsProductSubGroup(string _prmProductSubGrpCode, string _prmProductSubGrpName)
        {
            this.ProductSubGrpCode = _prmProductSubGrpCode;
            this.ProductSubGrpName = _prmProductSubGrpName;
        }
    }
}
