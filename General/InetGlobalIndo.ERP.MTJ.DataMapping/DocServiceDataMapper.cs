using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class DocServiceDataMapper
    {
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
                default:
                    _result = "On Hold";
                    break;
            }

            return _result;
        }

        public static DocServiceStatus GetStatus(byte _prmStatus)
        {
            DocServiceStatus _result;

            switch (_prmStatus)
            {
                case 0:
                    _result = DocServiceStatus.OnHold;
                    break;
                case 1:
                    _result = DocServiceStatus.InProgress;
                    break;
                case 2:
                    _result = DocServiceStatus.Cancelled;
                    break;
                case 3:
                    _result = DocServiceStatus.Done;
                    break;
                default:
                    _result = DocServiceStatus.OnHold;
                    break;
            }

            return _result;
        }


        public static byte GetStatus(DocServiceStatus _prmStatus)
        {
            byte _result;

            switch (_prmStatus)
            {
                case DocServiceStatus.OnHold:
                    _result = 0;
                    break;
                case DocServiceStatus.InProgress:
                    _result = 1;
                    break;
                case DocServiceStatus.Cancelled:
                    _result = 2;
                    break;
                case DocServiceStatus.Done:
                    _result = 3;
                    break;
                default:
                    _result = 0;
                    break;
            }

            return _result;
        }
    }
}