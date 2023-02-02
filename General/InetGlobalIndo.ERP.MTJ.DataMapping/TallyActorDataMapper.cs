using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class TallyActorDataMapper
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

        public static byte GetStatus(TallyActorStatus _prmValue)
        {
            byte _result = 0;

            switch (_prmValue)
            {
                case TallyActorStatus.Created:
                    _result = 0;
                    break;
                case TallyActorStatus.GetApproval:
                    _result = 1;
                    break;
                case TallyActorStatus.Approved:
                    _result = 2;
                    break;
                case TallyActorStatus.Posted:
                    _result = 3;
                    break;
                case TallyActorStatus.Unposted:
                    _result = 4;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static TallyActorStatus GetStatus(byte _prmValue)
        {
            TallyActorStatus _result;

            switch (_prmValue)
            {
                case 0:
                    _result = TallyActorStatus.Created;
                    break;
                case 1:
                    _result = TallyActorStatus.GetApproval;
                    break;
                case 2:
                    _result = TallyActorStatus.Approved;
                    break;
                case 3:
                    _result = TallyActorStatus.Posted;
                    break;
                case 4:
                    _result = TallyActorStatus.Unposted;
                    break;
                default:
                    _result = TallyActorStatus.Created;
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