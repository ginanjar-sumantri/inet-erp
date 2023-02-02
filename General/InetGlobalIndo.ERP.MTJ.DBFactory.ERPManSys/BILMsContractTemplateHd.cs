using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class BILMsContractTemplateHd
    {
        //string _custName = "";

        public BILMsContractTemplateHd(int _prmTemplateID, String _prmTemplateName, String _prmTemplateFileName, Boolean _prmFgActive)
        {
            this.TemplateID = _prmTemplateID;
            this.TemplateName = _prmTemplateName;
            this.TemplateFileName = _prmTemplateFileName;
            this.FgActive = _prmFgActive;
        }

        public BILMsContractTemplateHd(int _prmTemplateID, String _prmTemplateName)
        {
            this.TemplateID = _prmTemplateID;
            this.TemplateName = _prmTemplateName;
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
