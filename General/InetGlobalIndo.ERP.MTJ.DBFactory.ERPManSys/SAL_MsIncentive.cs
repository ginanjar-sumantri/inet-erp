using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class SAL_MsIncentive
    {
        public SAL_MsIncentive(Guid _prmIncentiveCode, String _prmIncetiveNo, String _prmIncentiveName, String _prodCode, decimal _prmTarget, Int32 _prmPersons, DateTime _prmStart, DateTime _prmEnd)
        {
            this.IncentiveCode = _prmIncentiveCode;
            this.IncentiveNo = _prmIncetiveNo;
            this.IncentiveName = _prmIncentiveName;
            this.ProductCode = _prodCode;
            this.Persons = _prmPersons;
            this.SalesTarget = _prmTarget;
            this.StartDate = _prmStart;
            this.EndDate = _prmEnd;
        }

        public SAL_MsIncentive(Guid _prmIncentiveCode, String _prmIncetiveNo, String _prmIncentiveName, decimal _prmTarget, Int32 _prmPersons, DateTime _prmStart, DateTime _prmEnd)
        {
            this.IncentiveCode = _prmIncentiveCode;
            this.IncentiveNo = _prmIncetiveNo;
            this.IncentiveName = _prmIncentiveName;
            this.Persons = _prmPersons;
            this.SalesTarget = _prmTarget;
            this.StartDate = _prmStart;
            this.EndDate = _prmEnd;
        }


    }
}
