using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class YesNoDataMapper
    {
        public static String GetYesNo(Boolean _prmValue)
        {
            String _result = "";

            switch (_prmValue)
            {
                case true:
                    _result = "Yes";
                    break;
                case false:
                    _result = "No";
                    break;
            }

            return _result;
        }

        public static char GetYesNo(YesNo _prmYesNo)
        {
            char _result;

            switch (_prmYesNo)
            {
                case YesNo.Yes:
                    _result = 'Y';
                    break;
                case YesNo.No:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }

        public static YesNo GetYesNo(char _prmYesNo)
        {
            YesNo _result = YesNo.No;

            switch (_prmYesNo)
            {
                case 'Y':
                    _result = YesNo.Yes;
                    break;
                case 'N':
                    _result = YesNo.No;
                    break;
                default:
                    _result = YesNo.No;
                    break;
            }

            return _result;
        }
    }
}