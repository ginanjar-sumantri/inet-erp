using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsCustType
    {
        public MsCustType(String _prmCompanyTypeCode, String _prmCompanyTypeName)
        {
            this._CustTypeCode = _prmCompanyTypeCode;
            this._CustTypeName = _prmCompanyTypeName;

        }
    }
}