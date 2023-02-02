using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class GLBudgetDataMapper
    {
        public static byte GetStatus(GLBudgetStatus _prmStatus)
        {
            byte _result;

            switch (_prmStatus)
            {
                case GLBudgetStatus.OnHold:
                    _result = 0;
                    break;
                case GLBudgetStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case GLBudgetStatus.Approved:
                    _result = 2;
                    break;
                case GLBudgetStatus.Posted:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        public static string GetStatusText(GLBudgetStatus _prmStatus)
        {
            string _result = "";

            switch (_prmStatus)
            {
                case GLBudgetStatus.OnHold:
                    _result = "On Hold";
                    break;
                case GLBudgetStatus.WaitingForApproval:
                    _result = "Waiting For Approval";
                    break;
                case GLBudgetStatus.Approved:
                    _result = "Approved";
                    break;
                case GLBudgetStatus.Posted:
                    _result = "Posted";
                    break;
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }

        public static string GetStatusText(byte _prmGLBudgetStatus)
        {
            string _result = "";

            switch (_prmGLBudgetStatus)
            {
                case 0:
                    _result = "On Hold";
                    break;
                case 1:
                    _result = "Waiting For Approval";
                    break;
                case 2:
                    _result = "Approved";
                    break;
                case 3:
                    _result = "Posted";
                    break;
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }
    }
}