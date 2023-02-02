using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class MemberDataMapper
    {
        public static Byte GetMemberStatus(Boolean _prmMemberStatus)
        {
            Byte _result = 0;

            switch (_prmMemberStatus)
            {
                case false:
                    _result = 0;
                    break;
                case true:
                    _result = 1;
                    break;                
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static Boolean GetMemberStatus(Byte _prmMemberStatus)
        {
            Boolean _result = false;

            switch (_prmMemberStatus)
            {
                case 0:
                    _result = false;
                    break;
                case 1:
                    _result = true;
                    break;
                default:
                    _result = false;
                    break;
            }

            return _result;
        }

        public static String GetMemberStatusText(Byte _prmMemberStatus)
        {
            string _result = "";

            switch (_prmMemberStatus)
            {
                case 0:
                    _result = "Not Active";
                    break;
                case 1:
                    _result = "Active";
                    break;
                default:
                    _result = "Not Active";
                    break;
            }

            return _result;
        }
    }
}