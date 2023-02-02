using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class PlanShipApprovalStatusDataMapper
    {
        public static string GetStatusText(byte _prmPlanShipApprovalStatusStatus)
        {
            string _result = "";
            
            switch (_prmPlanShipApprovalStatusStatus)
            {
                case 0:
                    _result = "Port Chief Unit";
                    break;
                case 1:
                    _result = "Operation Manager";
                    break;
                default:
                    _result = "Port Chief Unit";
                    break;
            }

            return _result;
        }

        public static PlanShipApprovalStatus GetStatus(byte _prmPlanShipApprovalStatus)
        {
            PlanShipApprovalStatus _result;

            switch (_prmPlanShipApprovalStatus)
            {
                case 0:
                    _result = PlanShipApprovalStatus.PortChiefUnit;
                    break;
                case 1:
                    _result = PlanShipApprovalStatus.OperationManager;
                    break;
                default:
                    _result = PlanShipApprovalStatus.PortChiefUnit;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(PlanShipApprovalStatus _prmPlanShipApprovalStatus)
        {
            byte _result;

            switch (_prmPlanShipApprovalStatus)
            {
                case PlanShipApprovalStatus.PortChiefUnit:
                    _result = 0;
                    break;
                case PlanShipApprovalStatus.OperationManager:
                    _result = 1;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }
    }
}