using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class V_ProductInformation
    {
        public V_ProductInformation(DateTime _prmTransDate, string _prmTransNmbr, String _prmTransType,
            String _prmFileNo, String _prmReffInsName, String _prmWrhsCode, decimal _prmQtyIn, decimal? _prmQtyOut)
        {
            this.TransDate = _prmTransDate;
            this.TransNmbr = _prmTransNmbr;
            this.TransType = _prmTransType;
            this.FileNmbr = _prmFileNo;
            this.ReffInsName = _prmReffInsName;
            this.WrhsCode = _prmWrhsCode;
            this.QtyIn = _prmQtyIn;
            this.QtyOut = _prmQtyOut;
        }
    }
}
