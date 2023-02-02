using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_Master_ProcessType
    {
        public HRM_Master_ProcessType(Guid _prmProcessTypeCode, string _prmProcessTypeName, bool _prmIsActive, string _prmDescription)
        {
            this.ProcessTypeCode = _prmProcessTypeCode;
            this.ProcessTypeName = _prmProcessTypeName;
            this.IsActive = _prmIsActive;
            this.Description = _prmDescription;
        }

        public HRM_Master_ProcessType(Guid _prmProcessTypeCode, string _prmProcessTypeName)
        {
            this.ProcessTypeCode = _prmProcessTypeCode;
            this.ProcessTypeName = _prmProcessTypeName;
        }
    }
}
