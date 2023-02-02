using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ContractTemplateDataMapper
    {
        public static String GetFgActiveText(Boolean _prmContractTemplateStatus)
        {
            string _result = "";

            switch (_prmContractTemplateStatus)
            {
                case true:
                    _result = "True";
                    break;
                case false:
                    _result = "False";
                    break;                
                default:
                    _result = "False";
                    break;
            }

            return _result;
        }
    }
}