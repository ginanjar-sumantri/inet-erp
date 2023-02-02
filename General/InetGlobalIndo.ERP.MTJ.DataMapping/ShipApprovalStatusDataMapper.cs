using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ShipApprovalStatusDataMapper
    {
        public static string GetStatusText(byte _prmShipApprovalStatusStatus)
        {
            string _result = "";
            
            switch (_prmShipApprovalStatusStatus)
            {
                case 0:
                    _result = "Operation Manager";
                    break;
                case 1:
                    _result = "Ordered By";
                    break;
                default:
                    _result = "Operation Manager";
                    break;
            }

            return _result;
        }

        public static ShipApprovalStatus GetStatus(byte _prmShipApprovalStatus)
        {
            ShipApprovalStatus _result;

            switch (_prmShipApprovalStatus)
            {
                case 0:
                    _result = ShipApprovalStatus.OperationManager;
                    break;
                case 1:
                    _result = ShipApprovalStatus.OrderedBy;
                    break;
                default:
                    _result = ShipApprovalStatus.OperationManager;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(ShipApprovalStatus _prmShipApprovalStatus)
        {
            byte _result;

            switch (_prmShipApprovalStatus)
            {
                case ShipApprovalStatus.OperationManager:
                    _result = 0;
                    break;
                case ShipApprovalStatus.OrderedBy:
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