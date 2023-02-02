using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class CrewAssignmentDataMapper 
    {
        public static string GetJobLevelText(byte _prmJob)
        {
            string _result = "";

            switch (_prmJob)
            {
                case 0:
                    _result = "Nahkoda";
                    break;
                case 1:
                    _result = "Mualim";
                    break;
                case 2:
                    _result = "ABK";
                    break;
            }

            return _result;
        }

        public static byte GetJobLevel(JobLevel _prmJobLevel)
        {
            byte _result = 0;

            switch (_prmJobLevel)
            {
                case JobLevel.Nahkoda:
                    _result = 0;
                    break;
                case JobLevel.Mualim:
                    _result = 1;
                    break;
                case JobLevel.ABK:
                    _result = 2;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(CrewAssignmentStatus _prmStatus)
        {
            byte _result = 0;

            switch (_prmStatus)
            {
                case CrewAssignmentStatus.OnHold:
                    _result = 0;
                    break;
                case CrewAssignmentStatus.InProgress:
                    _result = 1;
                    break;
                case CrewAssignmentStatus.Cancelled:
                    _result = 2;
                    break;
                case CrewAssignmentStatus.Done:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static CrewAssignmentStatus GetStatus(byte _prmStatus)
        {
            CrewAssignmentStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = CrewAssignmentStatus.OnHold;
                    break;
                case 1:
                    _result = CrewAssignmentStatus.InProgress;
                    break;
                case 2:
                    _result = CrewAssignmentStatus.Cancelled;
                    break;
                case 3:
                    _result = CrewAssignmentStatus.Done;
                    break;
                default:
                    _result = CrewAssignmentStatus.OnHold;
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
                    _result = "On Hold";
                    break;
                case 1:
                    _result = "In Progress";
                    break;
                case 2:
                    _result = "Cancelled";
                    break;
                case 3:
                    _result = "Done";
                    break;
            }

            return _result;
        }
    }
}