﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsPurpose
    {
        public HRMMsPurpose(string _prmPurposeCode, string _prmPurposeName)
        {
            this.PurposeCode = _prmPurposeCode;
            this.PurposeName = _prmPurposeName;
        }
    }
}
