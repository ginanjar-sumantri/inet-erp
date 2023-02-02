using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ScreeningProcessDataMapper
    {
        public static string GetStatusText(byte _prmScreeningProcessStatus)
        {
            string _result = "";

            switch (_prmScreeningProcessStatus)
            {
                case 0:
                    _result = "Draft";
                    break;
                case 1:
                    _result = "Waiting For Approval";
                    break;
                case 2:
                    _result = "Approved";
                    break;
                default:
                    _result = "Draft";
                    break;
            }

            return _result;
        }

        public static ScreeningProcessStatus GetStatus(byte _prmScreeningProcessStatus)
        {
            ScreeningProcessStatus _result;

            switch (_prmScreeningProcessStatus)
            {
                case 0:
                    _result = ScreeningProcessStatus.Draft;
                    break;
                case 1:
                    _result = ScreeningProcessStatus.WaitingForApproval;
                    break;
                case 2:
                    _result = ScreeningProcessStatus.Approved;
                    break;
                default:
                    _result = ScreeningProcessStatus.Draft;
                    break;
            }

            return _result;
        }

        public static byte GetStatus(ScreeningProcessStatus _prmScreeningProcessStatus)
        {
            byte _result;

            switch (_prmScreeningProcessStatus)
            {
                case ScreeningProcessStatus.Draft:
                    _result = 0;
                    break;
                case ScreeningProcessStatus.WaitingForApproval:
                    _result = 1;
                    break;
                case ScreeningProcessStatus.Approved:
                    _result = 2;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }

        //public static char GetYesNo(YesNo _prmYesNo)
        //{
        //    char _result;

        //    switch (_prmYesNo)
        //    {
        //        case YesNo.Yes:
        //            _result = 'Y';
        //            break;
        //        case YesNo.No:
        //            _result = 'N';
        //            break;
        //        default:
        //            _result = 'N';
        //            break;
        //    }

        //    return _result;
        //}
    }
}