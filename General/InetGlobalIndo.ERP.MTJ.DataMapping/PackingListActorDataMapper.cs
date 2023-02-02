using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class PackingListActorDataMapper
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

        public static byte GetStatus(PackingListActorStatus _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case PackingListActorStatus.Created:
                    _result = 0;
                    break;
                case PackingListActorStatus.GetApproval:
                    _result = 1;
                    break;
                case PackingListActorStatus.Approved:
                    _result = 2;
                    break;
                case PackingListActorStatus.Posted:
                    _result = 3;
                    break;
                case PackingListActorStatus.Unposted:
                    _result = 4;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static PackingListActorStatus GetStatus(byte _prmValue)
        {
            PackingListActorStatus _result;

            switch (_prmValue)
            {
                case 0:
                    _result = PackingListActorStatus.Created;
                    break;
                case 1:
                    _result = PackingListActorStatus.GetApproval;
                    break;
                case 2:
                    _result = PackingListActorStatus.Approved;
                    break;
                case 3:
                    _result = PackingListActorStatus.Posted;
                    break;
                case 4:
                    _result = PackingListActorStatus.Unposted;
                    break;
                default:
                    _result = PackingListActorStatus.Created;
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
