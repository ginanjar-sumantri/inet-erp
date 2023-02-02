using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class SetupDataMapper
    {
        public static string GetSetupGroup(SetupStatus _prmSetupGroup)
        {
            string _result = "";

            switch (_prmSetupGroup)
            {
                case SetupStatus.Account:
                    _result = "Account";
                    break;
                case SetupStatus.AutoNmbr:
                    _result = "AutoNmbr";
                    break;
            }

            return _result;
        }
    }
}