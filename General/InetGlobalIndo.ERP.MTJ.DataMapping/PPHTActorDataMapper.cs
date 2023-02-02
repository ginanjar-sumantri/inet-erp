using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class PPHTActorDataMapper
    {
        public static string GetStatusText(byte _prmValue)
        {
            string _result = "";

            switch (_prmValue)
            {
                case 0:
                    _result = "Create";
                    break;
                case 1:
                    _result = "Waiting For Approval";
                    break;
                case 2:
                    _result = "Approve";
                    break;
                case 3:
                    _result = "Posting";
                    break;
                case 4:
                    _result = "Unposting";
                    break;
                default:
                    _result = "Create";
                    break;
            }

            return _result;
        }

        public static byte GetStatus(PPHTActorStatus _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case PPHTActorStatus.Created:
                    _result = 0;
                    break;
                case PPHTActorStatus.GetApproval:
                    _result = 1;
                    break;
                case PPHTActorStatus.Approved:
                    _result = 2;
                    break;
                case PPHTActorStatus.Posted:
                    _result = 3;
                    break;
                case PPHTActorStatus.Unposted:
                    _result = 4;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static PPHTActorStatus GetStatus(byte _prmValue)
        {
            PPHTActorStatus _result;

            switch (_prmValue)
            {
                case 0:
                    _result = PPHTActorStatus.Created;
                    break;
                case 1:
                    _result = PPHTActorStatus.GetApproval;
                    break;
                case 2:
                    _result = PPHTActorStatus.Approved;
                    break;
                case 3:
                    _result = PPHTActorStatus.Posted;
                    break;
                case 4:
                    _result = PPHTActorStatus.Unposted;
                    break;
                default:
                    _result = PPHTActorStatus.Created;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(string _prmValue)
        {
            byte _result;

            switch (_prmValue.Trim().ToLower())
            {
                case "0":
                    _result = 0;
                    break;
                case "1":
                    _result = 1;
                    break;
                case "2":
                    _result = 2;
                    break;
                case "3":
                    _result = 3;
                    break;
                case "4":
                    _result = 4;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }
    }
}