using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTSWeb.Enum;

namespace VTSweb.DataMapping
{
    public static class CustomerContactDataMapping
    {
        public static Char GetChekCustContact(Boolean _prmValue)
        {
            Char _result = ' ';

            switch (_prmValue)
            {
                case true:
                    _result = 'Y';
                    break;
                case false:
                    _result = 'N';
                    break;
                default:
                    _result = 'Y';
                    break;
            }
            return _result;
        }

        public static Boolean GetChekCustContact(Char _prmValue)
        {
            Boolean _result = false;
            switch (_prmValue)
            {
                case 'N':
                    _result = false;
                    break;
                case 'Y':
                    _result = true;
                    break;
                default:
                    _result = false;
                    break;
            }
            return _result;
        }
    }
}
