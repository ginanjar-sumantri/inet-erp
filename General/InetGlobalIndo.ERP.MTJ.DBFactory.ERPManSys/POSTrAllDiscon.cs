using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSTrAllDiscon
    {
        public POSTrAllDiscon(String _prmTransNmbr, DateTime _prmTransDate, String _prmTransType, String _prmProductCode, Decimal _prmTotalDisconItem, Decimal _prmTotalDisconSubtotal, String _prmTypeDiscon)
        {
            this.TransNmbr = _prmTransNmbr;
            this.TransDate = _prmTransDate;
            this.TransType = _prmTransType;
            this.ProductCode = _prmProductCode;
            this.TotalDisconItem = _prmTotalDisconItem;
            this.TotalDisconSubtotal = _prmTotalDisconSubtotal;
            this.TypeDiscon = _prmTypeDiscon;            
        }

        //public String TransNmbr { get; set; }
        //public DateTime TransDate { get; set; }
        //public String TransType { get; set; }
        //public String ProductCode { get; set; }
        //public Decimal TotalDisconItem { get; set; }
        //public Decimal TotalDisconSubtotal { get; set; }
        //public String TypeDiscon { get; set; }
    }
}
