using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ContactStatus 
    {
        public static byte IsPrimary(YesNo _prmValue)
        {
            byte _result;

            switch (_prmValue)
            {
                case YesNo.Yes:
                    _result = 1;
                    break;
                case YesNo.No:
                    _result = 0;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static YesNo IsPrimary(byte? _prmValue)
        {
            YesNo _result;

            switch (_prmValue)
            {
                case 0:
                    _result = YesNo.No;
                    break;
                case 1:
                    _result = YesNo.Yes;
                    break;
                default:
                    _result = YesNo.No;
                    break;
            }

            return _result;
        }
    }
}