using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ReprimandStatusDataMapper
    {
        public static String GetReprimandStatusText(ReprimandStatus _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case ReprimandStatus.Lisan:
                    _result = "Lisan";
                    break;
                case ReprimandStatus.Tertulis:
                    _result = "Tertulis";
                    break;
            }

            return _result;
        }
    }
}