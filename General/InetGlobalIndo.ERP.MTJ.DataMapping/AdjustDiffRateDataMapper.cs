using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class AdjustDiffRateDataMapper
    {
        public static string GetStatusText(char _prmAdjustDiffRateStatus)
        {
            string _result = "";

            switch (_prmAdjustDiffRateStatus)
            {
                case 'H':
                    _result = "On Hold";
                    break;
                case 'P':
                    _result = "Posted";
                    break;
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }

        public static AdjustDiffRateStatus GetStatus(char _prmAdjustDiffRateStatus)
        {
            AdjustDiffRateStatus _result;

            switch (_prmAdjustDiffRateStatus)
            {
                case 'H':
                    _result = AdjustDiffRateStatus.OnHold;
                    break;
                case 'P':
                    _result = AdjustDiffRateStatus.Posting;
                    break;
                default:
                    _result = AdjustDiffRateStatus.OnHold;
                    break;
            }

            return _result;
        }

        public static char GetStatus(AdjustDiffRateStatus _prmAdjustDiffRateStatus)
        {
            char _result;

            switch (_prmAdjustDiffRateStatus)
            {
                case AdjustDiffRateStatus.OnHold:
                    _result = 'H';
                    break;
                case AdjustDiffRateStatus.Posting:
                    _result = 'P';
                    break;
                default:
                    _result = 'H';
                    break;
            }

            return _result;
        }

        public static char GetYesNo(YesNo _prmYesNo)
        {
            char _result;

            switch (_prmYesNo)
            {
                case YesNo.Yes:
                    _result = 'Y';
                    break;
                case YesNo.No:
                    _result = 'N';
                    break;
                default:
                    _result = 'N';
                    break;
            }

            return _result;
        }
    }
}