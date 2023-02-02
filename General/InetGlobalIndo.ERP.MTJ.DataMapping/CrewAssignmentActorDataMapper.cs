using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class CrewAssignmentActorDataMapper 
    {
        public static byte GetStatus(CrewAssignmentActorStatus _prmStatus)
        {
            byte _result = 0;

            switch (_prmStatus)
            {
                case CrewAssignmentActorStatus.Created:
                    _result = 0;
                    break;
                case CrewAssignmentActorStatus.GetApproval:
                    _result = 1;
                    break;
                case CrewAssignmentActorStatus.Approved:
                    _result = 2;
                    break;
                case CrewAssignmentActorStatus.Posted:
                    _result = 3;
                    break;
                case CrewAssignmentActorStatus.Unposted:
                    _result = 4;
                    break;
            }

            return _result;
        }

        public static CrewAssignmentActorStatus GetStatus(byte _prmStatus)
        {
            CrewAssignmentActorStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = CrewAssignmentActorStatus.Created;
                    break;
                case 1:
                    _result = CrewAssignmentActorStatus.GetApproval;
                    break;
                case 2:
                    _result = CrewAssignmentActorStatus.Approved;
                    break;
                case 3:
                    _result = CrewAssignmentActorStatus.Posted;
                    break;
                case 4:
                    _result = CrewAssignmentActorStatus.Unposted;
                    break;
                default:
                    _result = CrewAssignmentActorStatus.Created;
                    break;
            }

            return _result;
        }

        public static string GetStatusText(byte _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
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
            }

            return _result;
        }

        public static byte GetStatus(string _prmStatus)
        {
            byte _result = 0;

            switch (_prmStatus.Trim().ToLower())
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
                default :
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static CrewAssignmentActorStatus GetStatusActor(string _prmStatus)
        {
            CrewAssignmentActorStatus _result;

            switch (_prmStatus.Trim().ToLower())
            {
                case "0":
                    _result = CrewAssignmentActorStatus.Created;
                    break;
                case "1":
                    _result = CrewAssignmentActorStatus.GetApproval;
                    break;
                case "2":
                    _result = CrewAssignmentActorStatus.Approved;
                    break;
                case "3":
                    _result = CrewAssignmentActorStatus.Posted;
                    break;
                case "4":
                    _result = CrewAssignmentActorStatus.Unposted;
                    break;
                default:
                    _result = CrewAssignmentActorStatus.Created;
                    break;
            }

            return _result;
        }
    }
}