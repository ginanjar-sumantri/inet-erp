using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class ChangeTypeDataMapper
    {
        public static string GetChangeTypeText(ChangeType _prmChangeType)
        {
            string _result = "";

            switch (_prmChangeType)
            {
                case ChangeType.Add:
                    _result = "Add New Shift";
                    break;
                case ChangeType.Change:
                    _result = "Change Shift";
                    break;
                case ChangeType.Transfer:
                    _result = "Transfer Shift";
                    break;
                case ChangeType.Remove:
                    _result = "Remove Shift";
                    break;
                default:
                    _result = "Add New Shift";
                    break;
            }

            return _result;
        }

        public static string GetChangeTypeValue(ChangeType _prmChangeType)
        {
            string _result = "";

            switch (_prmChangeType)
            {
                case ChangeType.Add:
                    _result = "Add";
                    break;
                case ChangeType.Change:
                    _result = "Change";
                    break;
                case ChangeType.Transfer:
                    _result = "Transfer";
                    break;
                case ChangeType.Remove:
                    _result = "Remove";
                    break;
                default:
                    _result = "Add";
                    break;
            }

            return _result;
        }

        public static String GetChangeType(String _prmChangeType)
        {
            String _result = "";

            switch (_prmChangeType)
            {
                case "Add":
                    _result = "Add New Shift";
                    break;
                case "Change":
                    _result = "Change Shift";
                    break;
                case "Transfer":
                    _result = "Transfer Shift";
                    break;
                case "Remove":
                    _result = "Remove Shift";
                    break;
                default:
                    _result = "Add New Shift";
                    break;
            }

            return _result;
        }
    }
}