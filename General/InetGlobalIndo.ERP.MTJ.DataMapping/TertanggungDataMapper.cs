using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class TertanggungDataMapper
    {
        public static String GetTertanggungText(Tertanggung _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case Tertanggung.Employee:
                    _result = "Employee";
                    break;
                case Tertanggung.Company:
                    _result = "Company";
                    break;
            }

            return _result;
        }
    }
}