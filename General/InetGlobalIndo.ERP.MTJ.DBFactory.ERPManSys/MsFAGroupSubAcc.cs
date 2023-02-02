using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsFAGroupSubAcc
    {
        public MsFAGroupSubAcc( string FAGroupSubAccCode, string CurrCode ,string AccFA , string AccDepr ,string AccAkumDepr, string AccSales, string AccTenancy )
        {
            this.FASubGroup = FAGroupSubAccCode;
            this.CurrCode = CurrCode;
            this.AccFA= AccFA;
            this.AccDepr = AccDepr;
            this.AccAkumDepr = AccAkumDepr;
            this.AccSales = AccSales;
            this.AccTenancy = AccTenancy;
        }


    }
}
