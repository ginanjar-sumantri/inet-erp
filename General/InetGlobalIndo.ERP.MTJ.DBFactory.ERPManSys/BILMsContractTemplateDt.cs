using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class BILMsContractTemplateDt
    {
        //string _custName = "";

        public BILMsContractTemplateDt(int _prmTemplateID, String _prmVariable)
        {
            this.TemplateID = _prmTemplateID;
            this.Variable = _prmVariable;
        }

        //public string CustName
        //{
        //    get
        //    {
        //        return this._custName;
        //    }
        //    set
        //    {
        //        this._custName = value;
        //    }
        //}
    }
}
